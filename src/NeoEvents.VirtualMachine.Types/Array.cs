// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NeoEvents.VirtualMachine.Types;

[DebuggerDisplay("Type={Type}, Count={Count}")]
public class Array(
    IEnumerable<PrimitiveType>? items = default)
    : CompoundType(), ICollection<PrimitiveType>, IReadOnlyCollection<PrimitiveType>, IReadOnlyList<PrimitiveType>
{
    public override int Count => ArrayItems.Count;
    public override ICollection<PrimitiveType> Items => ArrayItems;
    public override StackItemType Type => StackItemType.Array;
    public override ReadOnlyMemory<byte> Memory => GetMemory();

    protected private readonly List<PrimitiveType> ArrayItems = items switch
    {
        null => [],
        List<PrimitiveType> list => list,
        _ => [.. items],
    };

    public PrimitiveType this[int index]
    {
        get => ArrayItems[index];
        set
        {
            if (IsReadOnly) throw new NotSupportedException();
            ArrayItems[index] = value;
        }
    }

    public override bool Equals(PrimitiveType? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null) return false;
        if (other is not Array array) return false;
        return ArrayItems.SequenceEqual(array.ArrayItems);
    }

    public override int GetHashCode()
    {
        var h = new HashCode();
        h.Add(Type);
        h.AddBytes(Memory.Span);
        return h.ToHashCode();
    }

    public override void Clear()
    {
        if (IsReadOnly) throw new NotSupportedException();
        ArrayItems.Clear();
    }

    public void Add(PrimitiveType item)
    {
        if (IsReadOnly) throw new NotSupportedException();
        ArrayItems.Add(item);
    }

    public bool Contains(PrimitiveType item) =>
        ArrayItems.Contains(item);

    public void CopyTo(PrimitiveType[] array, int arrayIndex) =>
        ArrayItems.CopyTo(array, arrayIndex);

    public bool Remove(PrimitiveType item)
    {
        if (IsReadOnly) throw new NotSupportedException();
        return ArrayItems.Remove(item);
    }

    public IEnumerator<PrimitiveType> GetEnumerator() =>
        ArrayItems.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private byte[] GetMemory()
    {
        byte[] memory = [];
        ArrayItems.ForEach(f =>
        {
            if (f is null) return;
            memory = [.. memory, .. f.Memory.Span];
        });

        return memory;
    }
}
