// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

namespace NeoEvents.VirtualMachine.Tests;

public class UT_Instruction
{
    [Fact]
    public void Test_DataIsSetCorrectly1()
    {
        byte[] script = [(byte)OpCode.PUSHDATA1, 3, .. new byte[] { 0x01, 0x02, 0x03 }];

        var instruct = new Instruction(script);

        Assert.Equal(OpCode.PUSHDATA1, instruct.OpCode);
        Assert.Equal([0x01, 0x02, 0x03], instruct.Operand.Value);
        Assert.Equal(3, instruct.Operand.Prefix);
        Assert.Equal(4, instruct.Operand.Size);
        Assert.Equal(0, instruct.Position);
        Assert.Equal(5, instruct.Size);
    }

    [Fact]
    public void Test_DataIsSetCorrectly2()
    {
        byte[] script = [(byte)OpCode.PUSHINT8, 10];

        var instruct = new Instruction(script);

        Assert.Equal(OpCode.PUSHINT8, instruct.OpCode);
        Assert.Equal([10], instruct.Operand.Value);
        Assert.Equal(0, instruct.Operand.Prefix);
        Assert.Equal(1, instruct.Operand.Size);
        Assert.Equal(0, instruct.Position);
        Assert.Equal(2, instruct.Size);
    }

    [Fact]
    public void Test_DataIsSetCorrectly3()
    {
        byte[] script = [(byte)OpCode.NOP];

        var instruct = new Instruction(script);

        Assert.Equal(OpCode.NOP, instruct.OpCode);
        Assert.Equal([], instruct.Operand.Value);
        Assert.Equal(0, instruct.Operand.Prefix);
        Assert.Equal(0, instruct.Operand.Size);
        Assert.Equal(0, instruct.Position);
        Assert.Equal(1, instruct.Size);
    }
}
