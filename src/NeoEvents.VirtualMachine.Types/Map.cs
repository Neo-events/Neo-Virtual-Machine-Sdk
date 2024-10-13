// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace NeoEvents.VirtualMachine.Types;

[DebuggerDisplay("Type={Type}, Count={Count}")]
public class Map : CompoundType, IReadOnlyDictionary<PrimitiveType, PrimitiveType>, IReadOnlyCollection<KeyValuePair<PrimitiveType, PrimitiveType>>, IDictionary<PrimitiveType, PrimitiveType>, ICollection<KeyValuePair<PrimitiveType, PrimitiveType>>
{
    public const int MaxKeySize = 64;

    public IEnumerable<PrimitiveType> Keys => _dictionary.Keys;
    public IEnumerable<PrimitiveType> Values => _dictionary.Values;
    public override int Count => _dictionary.Count;
    public override ICollection<PrimitiveType> Items => [.. Keys, .. Values];
    public override StackItemType Type => StackItemType.Map;
    public override ReadOnlyMemory<byte> Memory => GetMemory();

    ICollection<PrimitiveType> IDictionary<PrimitiveType, PrimitiveType>.Keys => _dictionary.Keys;

    ICollection<PrimitiveType> IDictionary<PrimitiveType, PrimitiveType>.Values => _dictionary.Values;

    private readonly Dictionary<PrimitiveType, PrimitiveType> _dictionary = new(EqualityComparer<PrimitiveType>.Default);

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

    public override bool Equals(PrimitiveType? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null or not Map) return false;
        return _dictionary.SequenceEqual(((Map)other)._dictionary);
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

    public void Add(PrimitiveType key, PrimitiveType value) =>
        _dictionary.Add(key, value);

    public bool Remove(PrimitiveType key) =>
        _dictionary.Remove(key);

    public void Add(KeyValuePair<PrimitiveType, PrimitiveType> item) =>
        Add(item.Key, item.Value);

    public bool Contains(KeyValuePair<PrimitiveType, PrimitiveType> item) =>
        _dictionary.ToArray().Contains(item);

    public void CopyTo(KeyValuePair<PrimitiveType, PrimitiveType>[] array, int arrayIndex) =>
        _dictionary.ToArray().CopyTo(array, arrayIndex);

    public bool Remove(KeyValuePair<PrimitiveType, PrimitiveType> item) =>
        _dictionary.Remove(item.Key);

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
