// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;

namespace NeoEvents.VirtualMachine;

/// <summary>
/// Indicates the operand length of an <see cref="OpCode"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class OperandSizeAttribute : Attribute
{
    /// <summary>
    /// When it is greater than 0, indicates the size of the operand.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// When it is greater than 0, indicates the size prefix of the operand.
    /// </summary>
    public int SizePrefix { get; set; }
}
