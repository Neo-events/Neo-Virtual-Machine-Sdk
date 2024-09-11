// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using NeoEvents.VirtualMachine.Types.Interfaces;
using System.Numerics;

namespace NeoEvents.VirtualMachine.Types;

public abstract class PrimitiveType : IPrimitiveType<PrimitiveType>, IType
{
    public abstract PrimitiveItemType Type { get; }

    public abstract ReadOnlySpan<byte> Memory { get; }

    public virtual int Size => Memory.Length;

    public bool IsNull => false;

    public virtual bool Equals(PrimitiveType? other) =>
        ReferenceEquals(this, other);

    public abstract bool GetBoolean();

    public virtual BigInteger GetInteger() =>
        throw new InvalidCastException();

    public virtual ReadOnlySpan<byte> GetSpan() =>
        throw new InvalidCastException();

    public virtual string? GetString() =>
        throw new InvalidCastException();

    public static implicit operator PrimitiveType(BigInteger value) =>
        (Integer)value;

    public static implicit operator PrimitiveType(sbyte value) =>
        (Integer)value;

    public static implicit operator PrimitiveType(byte value) =>
        (Integer)value;

    public static implicit operator PrimitiveType(short value) =>
        (Integer)value;

    public static implicit operator PrimitiveType(ushort value) =>
        (Integer)value;

    public static implicit operator PrimitiveType(int value) =>
        (Integer)value;

    public static implicit operator PrimitiveType(uint value) =>
        (Integer)value;

    public static implicit operator PrimitiveType(long value) =>
        (Integer)value;

    public static implicit operator PrimitiveType(ulong value) =>
        (Integer)value;

    public static implicit operator PrimitiveType(bool value) =>
        throw new NotImplementedException();
}
