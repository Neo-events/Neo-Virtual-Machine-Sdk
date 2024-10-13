// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Diagnostics;
using System.Numerics;

namespace NeoEvents.VirtualMachine.Types;

[DebuggerDisplay("Type={Type}, Value={_value}")]
public class Boolean(bool value) : PrimitiveType()
{
    public static readonly Boolean True = new(true);
    public static readonly Boolean False = new(false);

    public override StackItemType Type => StackItemType.Boolean;
    public override ReadOnlyMemory<byte> Memory => _value ? (byte[])[1] : [0];
    public override int Size => sizeof(bool);

    private readonly bool _value = value;

    public override bool Equals(PrimitiveType? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is Boolean b) return _value == b._value;
        return false;
    }

    public override int GetHashCode() =>
        HashCode.Combine(Type, _value);

    public override bool GetBoolean() => _value;

    public override BigInteger GetInteger() =>
        _value ? BigInteger.One : BigInteger.Zero;

    public override string? GetString() =>
        _value ? bool.TrueString : bool.FalseString;

    public static implicit operator Boolean(bool value) =>
        value ? True : False;


}
