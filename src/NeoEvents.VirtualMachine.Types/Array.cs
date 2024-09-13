// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Collections;
using System.Collections.Generic;

namespace NeoEvents.VirtualMachine.Types;

public class Array(IEnumerable<PrimitiveType>? items) : CompoundType, IReadOnlyCollection<PrimitiveType>
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

    public IEnumerator<PrimitiveType> GetEnumerator() =>
        _array.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private byte[] GetMemory()
    {
        byte[] memory = [];
        _array.ForEach(f => memory = [.. memory, .. f.Memory.Span]);

        return memory;
    }
}
