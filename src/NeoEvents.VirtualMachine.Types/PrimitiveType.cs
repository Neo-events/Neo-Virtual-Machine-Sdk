// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using NeoEvents.Text;
using NeoEvents.VirtualMachine.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;

namespace NeoEvents.VirtualMachine.Types;

[DebuggerDisplay("Type={Type}")]
public abstract class PrimitiveType : IPrimitiveType<PrimitiveType>, IType
{
    private int _referenceCount;

    public static readonly Null Null = new();

    protected PrimitiveType()
    {
        _referenceCount = 1;
    }

    public abstract StackItemType Type { get; }

    public abstract ReadOnlyMemory<byte> Memory { get; }

    public virtual int Size => Memory.Length;

    public bool IsNull => this is Null;

    public override bool Equals(object? obj) =>
        Equals(obj as PrimitiveType);

    public abstract bool Equals(PrimitiveType? other);

    public abstract override int GetHashCode();

    public abstract bool GetBoolean();

    public virtual BigInteger GetInteger() =>
        throw new InvalidCastException();

    public virtual ReadOnlySpan<byte> GetSpan() =>
        Memory.Span;

    public virtual string? GetString() =>
        new StrictUTF8Encoding().GetString(GetSpan());

    public virtual void AddStackReference() => _referenceCount++;

    public virtual void RemoveStackReference() =>
        _referenceCount--;


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
        (Boolean)value;

    public static implicit operator PrimitiveType(byte[] value) =>
        (ByteString)value;

    public static implicit operator PrimitiveType(ReadOnlyMemory<byte> value) =>
        (ByteString)value;

    public static implicit operator PrimitiveType(string value) =>
        (ByteString)value;

    public static implicit operator PrimitiveType(Dictionary<PrimitiveType, PrimitiveType> value) =>
        (Map)value;

    public static implicit operator PrimitiveType(Collection<KeyValuePair<PrimitiveType, PrimitiveType>> value) =>
        (Map)value;

    public static bool operator ==(PrimitiveType left, PrimitiveType right)
    {
        if (((object)left) == null || ((object)right) == null)
            return Equals(left, right);

        return left.Equals(right);
    }

    public static bool operator !=(PrimitiveType left, PrimitiveType right)
    {
        if (((object)left) == null || ((object)right) == null)
            return !Equals(left, right);

        return !left.Equals(right);
    }
}
