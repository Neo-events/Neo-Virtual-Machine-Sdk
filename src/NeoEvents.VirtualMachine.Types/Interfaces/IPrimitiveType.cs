// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Numerics;

namespace NeoEvents.VirtualMachine.Types.Interfaces;

public interface IPrimitiveType<TClass> : IEquatable<TClass>
    where TClass : class
{
    StackItemType Type { get; }
    bool IsNull { get; }

    bool GetBoolean();
    BigInteger GetInteger();
    ReadOnlySpan<byte> GetSpan();
    string? GetString();
}
