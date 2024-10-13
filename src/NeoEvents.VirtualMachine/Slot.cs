// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using NeoEvents.VirtualMachine.Types;
using System.Collections;
using System.Collections.Generic;

namespace NeoEvents.VirtualMachine;

public class Slot : IReadOnlyList<PrimitiveType>, IReadOnlyCollection<PrimitiveType>, IEnumerable<PrimitiveType>
{
    private readonly PrimitiveType[] _values;

    public PrimitiveType this[int index]
    {
        get => _values[index];
        internal set
        {
            ref var oldValue = ref _values[index];
            oldValue.RemoveStackReference();
            oldValue = value;
            oldValue.AddStackReference();
        }
    }

    public int Count => _values.Length;

    public Slot(PrimitiveType[] items)
    {
        _values = items;
    }

    public Slot(int count)
    {
        _values = new PrimitiveType[count];
        System.Array.Fill(_values, PrimitiveType.Null);
    }

    public void ClearReferences()
    {
        foreach (var item in _values)
            item.RemoveStackReference();
    }

    public IEnumerator<PrimitiveType> GetEnumerator()
    {
        foreach (var item in _values)
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
