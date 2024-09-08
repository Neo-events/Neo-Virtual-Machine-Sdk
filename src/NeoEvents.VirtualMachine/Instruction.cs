// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NeoEvents.VirtualMachine;

[DebuggerDisplay("OpCode={OpCode}, Size={Size}")]
public class Instruction : IEnumerable<Instruction>
{
    private const int OpCodeSize = 1;

    internal static readonly int[] OperandSizeTable = new int[256];
    internal static readonly int[] OperandPrefixSizeTable = new int[256];

    private readonly byte[] _script;
    private readonly int _operandSize;

    public OpCode OpCode { get; }
    public Operand Operand { get; }
    public int Position { get; }
    public int Size { get; }


    public Instruction(
        ReadOnlySpan<byte> script,
        int start = 0)
    {
        if (script.IsEmpty)
            throw new ArgumentException("Script is empty.", nameof(script));

        var opcode = (OpCode)script[start];

        if (Enum.IsDefined(opcode) == false)
            throw new InvalidDataException($"Invalid opcode at Position: {start}.");

        var operandPrefixSize = OperandPrefixSizeTable[(byte)opcode];
        var operandSize = operandPrefixSize switch
        {
            0 => OperandSizeTable[(byte)opcode],
            1 => script[start + OpCodeSize],
            2 => BinaryPrimitives.ReadUInt16LittleEndian(script[(start + OpCodeSize)..]),
            4 => unchecked((int)BinaryPrimitives.ReadUInt32LittleEndian(script[(start + OpCodeSize)..])),
            _ => throw new InvalidDataException($"Invalid opcode prefix at Position: {start}."),
        };

        operandSize += operandPrefixSize;

        if (start + operandSize + OpCodeSize > script.Length)
            throw new IndexOutOfRangeException("Operand size exceeds end of script.");

        var operand = script.Slice(start + OpCodeSize, operandSize);
        _script = script.ToArray();
        _operandSize = operandPrefixSize;

        var prefixOperand = operandPrefixSize switch
        {
            1 => AsToken<byte>(operand),
            2 => AsToken<ushort>(operand),
            4 => checked((int)AsToken<uint>(operand)),
            _ => 0,
        };

        Operand = new Operand(operand, prefixOperand, operandPrefixSize);
        Size = operandSize + OpCodeSize;
        OpCode = opcode;
        Position = start;
    }

    static Instruction()
    {
        foreach (var field in typeof(OpCode).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attr = field.GetCustomAttribute<OperandSizeAttribute>();
            if (attr == null) continue;

            var value = field.GetValue(null);
            if (value is null) continue;

            var index = (byte)value;
            OperandSizeTable[index] = attr.Size;
            OperandPrefixSizeTable[index] = attr.SizePrefix;
        }
    }

    public IEnumerator<Instruction> GetEnumerator()
    {
        var nip = Position + Size;
        yield return this;

        Instruction? instruct;
        for (var ip = nip; ip < _script.Length; ip += instruct.Size)
            yield return instruct = new(_script, ip);
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private T AsToken<T>(ReadOnlySpan<byte> data, uint index = 0)
            where T : unmanaged
    {
        var size = Unsafe.SizeOf<T>();

        if (size > _operandSize)
            throw new ArgumentOutOfRangeException(nameof(T), $"SizeOf {typeof(T).FullName} is too big for operand. OpCode: {OpCode}.");
        if (size + index > _operandSize)
            throw new ArgumentOutOfRangeException(nameof(index), $"SizeOf {typeof(T).FullName} + {index} is too big for operand. OpCode: {OpCode}.");

        var bytes = data[.._operandSize].ToArray();
        return Unsafe.As<byte, T>(ref bytes[index]);
    }
}
