// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.IO;

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
}
