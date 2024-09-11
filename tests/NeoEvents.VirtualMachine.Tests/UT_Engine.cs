// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using NeoEvents.VirtualMachine.Builders;
using NeoEvents.VirtualMachine.Tests.TestHelpers.Logging;
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
            builder.AddProvider(new TestLoggerProvider(testOutputHelper));
            builder.SetMinimumLevel(LogLevel.Trace);
        });
        _logger = _loggerFactory.CreateLogger<UT_Engine>();
    }

    [Fact]
    public void TestAdd()
    {
        using var sb = ScriptBuilder.Empty()
            .Push(1)
            .Push(2)
            .Emit(OpCode.ADD);

        var engine = new Engine(new Instruction(sb.Build()), _loggerFactory);

        var state = engine.Run();

        Assert.Equal(VMState.HALT, state);
        Assert.Single(engine.Stack);
        Assert.Equal(3, engine.Stack.Pop().GetInteger());
    }
}
