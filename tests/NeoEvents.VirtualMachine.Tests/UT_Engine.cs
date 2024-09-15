// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using NeoEvents.TDD.Logging;
using NeoEvents.VirtualMachine.Builders;
using NeoEvents.VirtualMachine.Types;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace NeoEvents.VirtualMachine.Tests;

public class UT_Engine
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<UT_Engine> _logger;

    public UT_Engine(
        ITestOutputHelper testOutputHelper)
    {
        _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.ClearProviders();
            builder.AddDebug();
            builder.AddProvider(new TestLoggerProvider(testOutputHelper));
            builder.SetMinimumLevel(LogLevel.Trace);
        });
        _logger = _loggerFactory.CreateLogger<UT_Engine>();
    }

    [Fact]
    public void Test_Add()
    {
        using var sb = ScriptBuilder.Empty()
            .Push(1) // a
            .Push(2) // b
            .Emit(OpCode.ADD); // a + b = 1 + 2 = 3

        var engine = new Engine(new Instruction(sb.Build()), _loggerFactory);

        var state = engine.Run();

        Assert.Equal(VMState.HALT, state);
        Assert.Single(engine.Stack);
        Assert.Equal(3, engine.Stack.Pop().GetInteger());
    }

    [Fact]
    public void Test_Subtract()
    {
        using var sb = ScriptBuilder.Empty()
            .Push(1) // a
            .Push(2) // b
            .Emit(OpCode.SUB); // a - b = 1 - 2 = -1

        var engine = new Engine(new Instruction(sb.Build()), _loggerFactory);

        var state = engine.Run();

        Assert.Equal(VMState.HALT, state);
        Assert.Single(engine.Stack);
        Assert.Equal(-1, engine.Stack.Pop().GetInteger());
    }

    [Fact]
    public void Test_Multiply()
    {
        using var sb = ScriptBuilder.Empty()
            .Push(1) // a
            .Push(2) // b
            .Emit(OpCode.MUL); // a * b = 1 * 2 = 2

        var engine = new Engine(new Instruction(sb.Build()), _loggerFactory);

        var state = engine.Run();

        Assert.Equal(VMState.HALT, state);
        Assert.Single(engine.Stack);
        Assert.Equal(2, engine.Stack.Pop().GetInteger());
    }

    [Fact]
    public void Test_Divide()
    {
        using var sb = ScriptBuilder.Empty()
            .Push(1) // a
            .Push(2) // b
            .Emit(OpCode.DIV); // a / b = 1 / 2 = 0

        var engine = new Engine(new Instruction(sb.Build()), _loggerFactory);

        var state = engine.Run();

        Assert.Equal(VMState.HALT, state);
        Assert.Single(engine.Stack);
        Assert.Equal(0, engine.Stack.Pop().GetInteger());
    }

    [Fact]
    public void Test_Sign()
    {
        using var sb = ScriptBuilder.Empty()
            .Push(-2)
            .Emit(OpCode.SIGN)
            .Push(0)
            .Emit(OpCode.SIGN)
            .Push(2)
            .Emit(OpCode.SIGN);

        var engine = new Engine(new Instruction(sb.Build()), _loggerFactory);

        var state = engine.Run();

        Assert.Equal(VMState.HALT, state);
        Assert.Equal(3, engine.Stack.Count);
        Assert.Equal(1, engine.Stack.Pop().GetInteger());
        Assert.Equal(0, engine.Stack.Pop().GetInteger());
        Assert.Equal(-1, engine.Stack.Pop().GetInteger());
    }

    [Fact]
    public void Test_Pack()
    {
        using var sb = ScriptBuilder.Empty()
            .CreateArray([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);

        var engine = new Engine(new Instruction(sb.Build()), _loggerFactory);

        var state = engine.Run();

        Assert.Equal(VMState.HALT, state);
        Assert.Single(engine.Stack);

        var item = engine.Stack.Pop();

        Assert.IsType<Array>(item);
        Assert.Equal(10, ((Array)item).Count);
        Assert.Equal([1, 2, 3, 4, 5, 6, 7, 8, 9, 10], (Array)item);
    }

    [Fact]
    public void Test_PackStruct()
    {
        using var sb = ScriptBuilder.Empty()
            .CreateStruct([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);

        var engine = new Engine(new Instruction(sb.Build()), _loggerFactory);

        var state = engine.Run();

        Assert.Equal(VMState.HALT, state);
        Assert.Single(engine.Stack);

        var item = engine.Stack.Pop();

        Assert.IsType<Struct>(item);
        Assert.Equal(10, ((Struct)item).Count);
        Assert.Equal([1, 2, 3, 4, 5, 6, 7, 8, 9, 10], (Struct)item);
    }

    [Fact]
    public void Test_PackMap()
    {
        var dic = new Dictionary<bool, byte[]> { [true] = [1, 2, 3] };

        using var sb = ScriptBuilder.Empty()
            .CreateMap(dic);

        var engine = new Engine(new Instruction(sb.Build()), _loggerFactory);

        var state = engine.Run();

        Assert.Equal(VMState.HALT, state);
        Assert.Single(engine.Stack);

        var item = engine.Stack.Pop();

        Assert.IsType<Map>(item);
        Assert.Single(((Map)item));

        foreach (var (key, value) in dic)
            Assert.Equal(value, ((Map)item)[key]);
    }
}
