// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

namespace NeoEvents.VirtualMachine.Types.Interfaces;

public interface IType
{
    ReadOnlyMemory<byte> Memory { get; }
    int Size { get; }
}
