// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using System;

namespace NeoEvents.VirtualMachine.Tables;

public delegate void OpCodeHandler(Engine engine, Instruction instruction, ILogger logger);

public partial class ExecuteTable
{
    public static readonly ExecuteTable Default = new();

    protected readonly OpCodeHandler[] Table = new OpCodeHandler[byte.MaxValue];

    public OpCodeHandler this[OpCode opcode]
    {
        get => Table[(byte)opcode];
        set => Table[(byte)opcode] = value;
    }

    public ExecuteTable()
    {
        foreach (var method in GetType().GetMethods())
        {
            if (Enum.TryParse<OpCode>(method.Name, ignoreCase: true, out var opcode))
            {
                if (this[opcode] is not null)
                    throw new InvalidOperationException($"Duplicated opcode: {opcode}");

                this[opcode] = (OpCodeHandler)Delegate.CreateDelegate(typeof(OpCodeHandler), this, method);
            }
        }

        for (var i = 0; i < Table.Length; i++)
        {
            if (Table[i] is not null) continue;

            Table[i] = DefaultHandler;
        }
    }

    protected virtual void DefaultHandler(Engine engine, Instruction instruction, ILogger logger)
    {
        throw new InvalidOperationException($"Opcode {instruction.OpCode} is undefined.");
    }
}
