// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NeoEvents.VirtualMachine.Types;

[DebuggerDisplay("Type={Type}, Count={Count}")]
public class Struct(IEnumerable<PrimitiveType>? items = default) : Array(items)
{
    public override StackItemType Type => StackItemType.Struct;

    public override bool Equals(PrimitiveType? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null) return false;
        if (other is not Struct str) return false;
        return ArrayItems.SequenceEqual(str.ArrayItems);
    }

    public override int GetHashCode() =>
        HashCode.Combine(Type, ArrayItems);
}
