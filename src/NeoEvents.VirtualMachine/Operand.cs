// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace NeoEvents.VirtualMachine;

public class Operand
{
    public int Prefix { get; }
    public byte[] Value { get; }
    public int Size { get; }

    public Operand(
        ReadOnlySpan<byte> data,
        int prefix,
        int prefixSize)
    {
        if (prefixSize < 0 && prefixSize > data.Length)
            throw new InvalidDataException($"Invalid size prefix: {prefixSize}.");

        Prefix = prefix;
        Value = data[prefixSize..].ToArray();
        Size = data.Length;
    }

    public T AsValue<T>(uint index = 0)
            where T : unmanaged
    {
        var size = Unsafe.SizeOf<T>();

        if (size > Size)
            throw new ArgumentOutOfRangeException(nameof(T), $"SizeOf {typeof(T).FullName} is too big for operand.");
        if (size + index > Size)
            throw new ArgumentOutOfRangeException(nameof(index), $"SizeOf {typeof(T).FullName} + {index} is too big for operand.");

        var bytes = Value[..Size];
        return Unsafe.As<byte, T>(ref bytes[index]);
    }
}
