// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;

namespace NeoEvents.VirtualMachine.Types;

public class Null : PrimitiveType
{
    public override StackItemType Type => StackItemType.Any;

    public override ReadOnlyMemory<byte> Memory => Memory<byte>.Empty;

    internal Null() { }

    public override bool Equals(PrimitiveType? other)
    {
        if (ReferenceEquals(this, other)) return true;
        return other is Null;
    }

    public override bool GetBoolean() => false;

    public override int GetHashCode() => 0;

    public override string? GetString() => null;

    public override string ToString() => "null";
}
