// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using NeoEvents.VirtualMachine.Builders;
using System;
using System.Linq;

namespace NeoEvents.VirtualMachine.Tests.Builders;
public class UT_ScriptBuilder
{
    [Fact]
    public void TestEmpty()
    {
        using var sb = ScriptBuilder.Empty();

        Assert.NotNull(sb);
        Assert.Equal(0, sb.Length);
        Assert.Empty(sb.Build());
    }

    [Fact]
    public void TestEmit()
    {
        using var sb = ScriptBuilder.Empty();

        sb.Emit(OpCode.NOP, [0x00]);

        Assert.Equal(2, sb.Length);
        Assert.Equal([0x21, 0x00], sb.Build());
    }

    [Fact]
    public void TestCall()
    {
        using var sb = ScriptBuilder.Empty();

        sb.Call(0x01);
        sb.Call(0xf4240); // 1_000_000

        Assert.Equal(7, sb.Length);
        Assert.Equal([0x34, 0x01, 0x35, 0x40, 0x42, 0x0f, 0x00], sb.Build());
    }

    [Fact]
    public void TestSysCall()
    {
        using var sb = ScriptBuilder.Empty();

        sb.SysCall(0xf4240); // 1_000_000

        Assert.Equal(5, sb.Length);
        Assert.Equal([0x41, 0x40, 0x42, 0x0f, 0x00], sb.Build());
    }

    [Fact]
    public void TestPushBoolean()
    {
        using var sb = ScriptBuilder.Empty();

        sb.Push(true);
        sb.Push(false);

        Assert.Equal(2, sb.Length);
        Assert.Equal([0x08, 0x09], sb.Build());
    }

    [Fact]
    public void TestPushBytes()
    {
        using var sb = ScriptBuilder.Empty();

        byte[] expectedBytes1 = []; // PUSHDATA1
        byte[] expectedBytes2 = [0x01]; // PUSHDATA1

        var expectedBytes3 = Enumerable
            .Range(0, byte.MaxValue + 1)
            .Select(i => (byte)0x01)
            .ToArray();// PUSHDATA2
        var expectedBytes4 = Enumerable
            .Range(0, ushort.MaxValue + 1)
            .Select(i => (byte)0x01)
            .ToArray(); // PUSHDATA4

        sb.Push(expectedBytes1);
        sb.Push(expectedBytes2);
        sb.Push(expectedBytes3);
        sb.Push(expectedBytes4);

        Assert.Equal(
            expectedBytes1.Length + expectedBytes2.Length +
            expectedBytes3.Length + expectedBytes4.Length +
            4 + 8 + 1,
            sb.Length);

        Assert.Equal([
            0x0c, 0x00, 0x00, // PUSHDATA1
            0x0c, 0x01, 0x01, // PUSHDATA1
            0x0d, .. BitConverter.GetBytes((ushort)expectedBytes3.Length), .. expectedBytes3, // PUSHDATA2
            0x0e, .. BitConverter.GetBytes(expectedBytes4.Length), .. expectedBytes4], // PUSHDATA4
            sb.Build());
    }

    [Fact]
    public void TestPushString()
    {
        using var sb = ScriptBuilder.Empty();
        var expectedString = "Hello";

        sb.Push(expectedString);

        Assert.Equal(expectedString.Length + 1 + 1, sb.Length);
        Assert.Equal([0x0c, (byte)expectedString.Length, 0x48, 0x65, 0x6c, 0x6c, 0x6f], sb.Build());
    }
}
