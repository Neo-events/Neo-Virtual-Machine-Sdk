// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using NeoEvents.Text;
using System;
using System.Diagnostics;
using System.Numerics;

namespace NeoEvents.VirtualMachine.Types;

[DebuggerDisplay("Type={Type}, Size={Size}")]
public class ByteString(
    ReadOnlyMemory<byte> memory) : PrimitiveType
{
    public override StackItemType Type => StackItemType.ByteString;

    public override ReadOnlyMemory<byte> Memory { get; } = memory;

    public override bool Equals(PrimitiveType? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null or not ByteString) return false;
        return GetSpan().SequenceEqual(other.GetSpan());
    }

    public override int GetHashCode() =>
        HashCode.Combine(Type, Memory.ToArray());

    public override bool GetBoolean()
    {
        if (Size > Integer.MaxSize) throw new InvalidCastException();

        return Memory.Span.ContainsAnyExcept((byte)0);
    }

    public override BigInteger GetInteger()
    {
        if (Size > Integer.MaxSize) throw new InvalidCastException();
        return new BigInteger(Memory.Span);
    }

    public static implicit operator ReadOnlyMemory<byte>(ByteString value) =>
        value.Memory;

    public static implicit operator ReadOnlySpan<byte>(ByteString value) =>
        value.Memory.Span;

    public static implicit operator ByteString(byte[] value) =>
        new(value);

    public static implicit operator ByteString(ReadOnlyMemory<byte> value) =>
        new(value);

    public static implicit operator ByteString(string value) =>
        new(new StrictUTF8Encoding().GetBytes(value));
}
