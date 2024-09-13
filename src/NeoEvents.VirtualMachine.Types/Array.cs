// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeoEvents.VirtualMachine.Types;

[DebuggerDisplay("Type={Type}, Count={Count}")]
public class Array(IEnumerable<PrimitiveType>? items = default) : CompoundType, ICollection<PrimitiveType>, IReadOnlyCollection<PrimitiveType>
{
    public override int Count => _array.Count;
    public override ICollection<PrimitiveType> Items => _array;
    public override StackItemType Type => StackItemType.Array;
    public override ReadOnlyMemory<byte> Memory => GetMemory();

    private readonly List<PrimitiveType> _array = items switch
    {
        null => [],
        List<PrimitiveType> list => list,
        _ => [.. items],
    };

    public PrimitiveType this[int index]
    {
        get => _array[index];
        set
        {
            if (IsReadOnly) throw new InvalidOperationException();
            _array[index] = value;
        }
    }

    public override void Clear()
    {
        if (IsReadOnly) throw new InvalidOperationException();
        _array.Clear();
    }

    public void Add(PrimitiveType item)
    {
        if (IsReadOnly) throw new InvalidOperationException();
        _array.Add(item);
    }

    public bool Contains(PrimitiveType item) =>
        _array.Contains(item);

    public void CopyTo(PrimitiveType[] array, int arrayIndex) =>
        _array.CopyTo(array, arrayIndex);

    public bool Remove(PrimitiveType item)
    {
        if (IsReadOnly) throw new InvalidOperationException();
        return _array.Remove(item);
    }

    public IEnumerator<PrimitiveType> GetEnumerator() =>
        _array.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private byte[] GetMemory()
    {
        byte[] memory = [];
        _array.ForEach(f =>
        {
            if (f is null) return;
            memory = [.. memory, .. f.Memory.Span];
        });

        return memory;
    }
}
