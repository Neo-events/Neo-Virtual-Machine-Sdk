// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

namespace NeoEvents.VirtualMachine;

public enum VMState : byte
{
    /// <summary>
    /// Indicates that the execution is in progress or has not yet begun.
    /// </summary>
    NONE = 0,

    /// <summary>
    /// Indicates that the execution has been completed successfully.
    /// </summary>
    HALT = 1 << 0,

    /// <summary>
    /// Indicates that the execution has ended, and an exception that cannot be caught is thrown.
    /// </summary>
    FAULT = 1 << 1,

    /// <summary>
    /// Indicates that a breakpoint is currently being hit.
    /// </summary>
    BREAK = 1 << 2,
}
