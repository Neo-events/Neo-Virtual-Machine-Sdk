// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System.Diagnostics;
using System.Numerics;

namespace NeoEvents.VirtualMachine.Types;

[DebuggerDisplay("Type={Type}, Value={_value}")]
public class Integer : PrimitiveType
{
    public const int MaxSize = 32;

    public override int Size { get; }

    public override PrimitiveItemType Type => PrimitiveItemType.Integer;
    public override ReadOnlySpan<byte> Memory => _value.IsZero ? [] : _value.ToByteArray();

    private readonly BigInteger _value;

    public Integer(
        BigInteger value)
    {
        if (value.IsZero)
            Size = 0;
        else
        {
            Size = value.GetByteCount();
            if (Size > MaxSize)
                throw new ArgumentException($"MaxSize exceed: {Size}");
        }
        _value = value;
    }

    public override bool Equals(PrimitiveType? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null) return false;
        if (other.Type != Type) return false;
        return _value == ((Integer)other)._value;
    }

    public override int GetHashCode() =>
        _value.GetHashCode();

    public override bool GetBoolean() =>
        !_value.IsZero;

    public override BigInteger GetInteger() =>
        _value;

    public static implicit operator Integer(BigInteger value) =>
        new(value);

    public static implicit operator Integer(sbyte value) =>
        (BigInteger)value;

    public static implicit operator Integer(byte value) =>
        (BigInteger)value;

    public static implicit operator Integer(short value) =>
        (BigInteger)value;

    public static implicit operator Integer(ushort value) =>
        (BigInteger)value;

    public static implicit operator Integer(int value) =>
        (BigInteger)value;

    public static implicit operator Integer(uint value) =>
        (BigInteger)value;

    public static implicit operator Integer(long value) =>
        (BigInteger)value;

    public static implicit operator Integer(ulong value) =>
        (BigInteger)value;
}
