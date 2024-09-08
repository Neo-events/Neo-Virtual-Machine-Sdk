// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.IO;

namespace NeoEvents.VirtualMachine;

public class ScriptBuilder : IDisposable
{
    private readonly MemoryStream _ms = new();
    private readonly BinaryWriter _writer;

    public int Length => checked((int)_ms.Position);

    public ScriptBuilder()
    {
        _writer = new BinaryWriter(_ms);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _writer.Dispose();
        _ms.Dispose();
    }

    public byte[] Build()
    {
        return _ms.ToArray();
    }

    public ScriptBuilder Emit(OpCode opCode, byte[]? operand = default)
    {
        _writer.Write((byte)opCode);

        if (operand is not null)
            _writer.Write(operand);

        return this;
    }

    public ScriptBuilder EmitCall(int offset)
    {
        if (offset < sbyte.MinValue || offset > sbyte.MaxValue)
            return Emit(OpCode.CALL_L, BitConverter.GetBytes(offset));
        return Emit(OpCode.CALL, [(byte)offset]);
    }

    public ScriptBuilder EmitPush(bool value) =>
        Emit(value ? OpCode.PUSHT : OpCode.PUSHF);

    public ScriptBuilder EmitPush(byte[] data)
    {
        if (data.Length == 0)
            return Emit(OpCode.PUSHDATA1, [0, 0]);
        if (data.Length < byte.MaxValue)
            return Emit(OpCode.PUSHDATA1, [(byte)data.Length, .. data]);
        else if (data.Length < ushort.MaxValue)
            return Emit(OpCode.PUSHDATA2, [.. BitConverter.GetBytes((ushort)data.Length), .. data]);
        else
            return Emit(OpCode.PUSHDATA4, [.. BitConverter.GetBytes(data.Length), .. data]);
    }

    public ScriptBuilder EmitPush(string data)
    {
        StrictUTF8 strictUTF8 = new();
        return EmitPush(strictUTF8.GetBytes(data));
    }

    public ScriptBuilder EmitSysCall(uint api) =>
        Emit(OpCode.SYSCALL, BitConverter.GetBytes(api));
}
