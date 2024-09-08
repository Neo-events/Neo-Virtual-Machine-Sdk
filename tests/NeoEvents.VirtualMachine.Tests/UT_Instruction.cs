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
        Assert.Equal(3u, instruct.Operand);
        Assert.Equal(0, instruct.Position);
        Assert.Equal(5, instruct.Size);
        Assert.Equal([0x01, 0x02, 0x03], instruct.Data);
    }

    [Fact]
    public void Test_DataIsSetCorrectly2()
    {
        byte[] script = [(byte)OpCode.NOP];

        var instruct = new Instruction(script);

        Assert.Equal(OpCode.NOP, instruct.OpCode);
        Assert.Equal(0u, instruct.Operand);
        Assert.Equal(0, instruct.Position);
        Assert.Equal(1, instruct.Size);
        Assert.Equal([], instruct.Data);
    }
}
