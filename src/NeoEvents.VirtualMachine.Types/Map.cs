// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NeoEvents.VirtualMachine.Types;

public class Map : CompoundType, IReadOnlyDictionary<PrimitiveType, PrimitiveType>, IReadOnlyCollection<KeyValuePair<PrimitiveType, PrimitiveType>>
{
    public const int MaxKeySize = 64;

    public IEnumerable<PrimitiveType> Keys => _dictionary.Keys;
    public IEnumerable<PrimitiveType> Values => _dictionary.Values;
    public override int Count => _dictionary.Count;
    public override ICollection<PrimitiveType> Items => [.. Keys, .. Values];
    public override StackItemType Type => StackItemType.Map;
    public override ReadOnlyMemory<byte> Memory => GetMemory();

    private readonly Dictionary<PrimitiveType, PrimitiveType> _dictionary = new();

    public PrimitiveType this[PrimitiveType key]
    {
        get
        {
            if (key.Size > MaxKeySize)
                throw new ArgumentException($"Key size is too large: {key.Size} > {MaxKeySize}", nameof(key));
            return _dictionary[key];
        }
        set
        {
            if (key.Size > MaxKeySize)
                throw new ArgumentException($"Key size is too large: {key.Size} > {MaxKeySize}", nameof(key));
            if (IsReadOnly)
                throw new NotSupportedException();
            _dictionary[key] = value;
        }
    }

    public override void Clear()
    {
        if (IsReadOnly)
            throw new NotSupportedException();
        _dictionary.Clear();
    }

    public bool ContainsKey(PrimitiveType key)
    {
        if (key.Size > MaxKeySize)
            throw new ArgumentException($"Key size is too large: {key.Size} > {MaxKeySize}", nameof(key));
        return _dictionary.ContainsKey(key);
    }

    public bool TryGetValue(PrimitiveType key, [MaybeNullWhen(false)] out PrimitiveType value)
    {
        if (key.Size > MaxKeySize)
            throw new ArgumentException($"Key size is too large: {key.Size} > {MaxKeySize}", nameof(key));
        return _dictionary.TryGetValue(key, out value);
    }

    public IEnumerator<KeyValuePair<PrimitiveType, PrimitiveType>> GetEnumerator() =>
        _dictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private byte[] GetMemory()
    {
        byte[] memory = [];
        foreach (var item in Items)
        {
            if (item is null) continue;
            memory = [.. memory, .. item.Memory.Span];
        }

        return memory;
    }
}
