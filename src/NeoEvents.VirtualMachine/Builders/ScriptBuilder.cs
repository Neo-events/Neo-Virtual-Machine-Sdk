// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using NeoEvents.VirtualMachine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace NeoEvents.VirtualMachine.Builders;

public class ScriptBuilder : IDisposable
{
    private readonly MemoryStream _ms = new();
    private readonly BinaryWriter _writer;

    public int Length => checked((int)_ms.Position);

    private ScriptBuilder()
    {
        _writer = new BinaryWriter(_ms);
    }

    public static ScriptBuilder Empty() => new();

    public void Dispose()
    {
        _writer.Dispose();
        _ms.Dispose();
        GC.SuppressFinalize(this);
    }

    public byte[] Build()
    {
        _writer.Flush();
        return _ms.ToArray();
    }

    public ScriptBuilder Emit(OpCode opCode, byte[]? operand = default)
    {
        _writer.Write((byte)opCode);

        if (operand is not null)
            _writer.Write(operand);

        return this;
    }

    public ScriptBuilder Call(int offset)
    {
        if (offset < sbyte.MinValue || offset > sbyte.MaxValue)
            return Emit(OpCode.CALL_L, BitConverter.GetBytes(offset));
        return Emit(OpCode.CALL, [(byte)offset]);
    }

    public ScriptBuilder SysCall(uint api) =>
        Emit(OpCode.SYSCALL, BitConverter.GetBytes(api));

    public ScriptBuilder Push(bool value) =>
        Emit(value ? OpCode.PUSHT : OpCode.PUSHF);

    public ScriptBuilder Push(byte[] data)
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

    public ScriptBuilder Push(string value)
    {
        StrictUTF8Encoding strictUTF8 = new();
        return Push(strictUTF8.GetBytes(value));
    }

    public ScriptBuilder Push(BigInteger value)
    {
        if (value >= -1 && value <= 16)
            return Emit(OpCode.PUSH0 + (byte)(sbyte)value);

        Span<byte> buffer = stackalloc byte[32];

        if (value.TryWriteBytes(buffer, out var bytesWritten) == false)
            throw new ArgumentOutOfRangeException(nameof(value));

        return bytesWritten switch
        {
            1 => Emit(OpCode.PUSHINT8, PadRight(buffer, bytesWritten, 1, value.Sign < 0)),
            2 => Emit(OpCode.PUSHINT16, PadRight(buffer, bytesWritten, 2, value.Sign < 0)),
            <= 4 => Emit(OpCode.PUSHINT32, PadRight(buffer, bytesWritten, 4, value.Sign < 0)),
            <= 8 => Emit(OpCode.PUSHINT64, PadRight(buffer, bytesWritten, 8, value.Sign < 0)),
            <= 16 => Emit(OpCode.PUSHINT128, PadRight(buffer, bytesWritten, 16, value.Sign < 0)),
            _ => Emit(OpCode.PUSHINT256, PadRight(buffer, bytesWritten, 32, value.Sign < 0)),
        };

        static byte[] PadRight(Span<byte> buffer, int dataLength, int padLength, bool isNegative)
        {
            var padByte = isNegative ? (byte)0xff : (byte)0x00;
            for (var i = dataLength; i < padLength; i++)
                buffer[i] = padByte;
            return buffer[..padLength].ToArray();
        }
    }

    public ScriptBuilder Push(object? value) =>
        value switch
        {
            null => PushNull(),
            byte[] data => Push(data),
            string str => Push(str),
            bool b => Push(b),
            BigInteger bi => Push(bi),
            sbyte s => Push(s),
            byte b => Push(b),
            short s => Push(s),
            ushort us => Push(us),
            int i => Push(i),
            uint ui => Push(ui),
            long l => Push(l),
            ulong ul => Push(ul),
            char c => Push((ushort)c),
            Enum e => Push(BigInteger.Parse(e.ToString("d"))),
            _ => throw new ArgumentException($"{value.GetType().FullName} can't be Serialized.", nameof(value)),
        };

    public ScriptBuilder PushNull() =>
        Emit(OpCode.PUSHNULL);

    public ScriptBuilder CreateArray<T>(IReadOnlyList<T> array)
        where T : notnull
    {
        if (array.Count == 0)
            return Emit(OpCode.NEWARRAY0);
        for (var i = array.Count - 1; i >= 0; i--)
            Push(array[i]);
        Push(array.Count);
        return Emit(OpCode.PACK);
    }

    public ScriptBuilder CreateMap<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> map)
    {
        Emit(OpCode.NEWMAP);
        foreach (var (key, value) in map)
        {
            Emit(OpCode.DUP);
            Push(key);
            Push(value);
            Emit(OpCode.SETITEM);
        }
        return this;
    }
}
