// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Collections.Generic;

namespace NeoEvents.VirtualMachine.Types;

public abstract class CompoundType : PrimitiveType
{
    public abstract int Count { get; }
    public abstract ICollection<PrimitiveType> Items { get; }
    public bool IsReadOnly { get; protected set; }

    public abstract void Clear();

    public sealed override bool GetBoolean() => true;

    public override int GetHashCode() =>
        throw new NotSupportedException();
}
