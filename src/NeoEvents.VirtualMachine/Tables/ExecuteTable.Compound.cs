// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using NeoEvents.VirtualMachine.Types;
using System.Linq;

namespace NeoEvents.VirtualMachine.Tables;

public partial class ExecuteTable
{
    public virtual void PackStruct(Engine engine, Instruction instruction, ILogger logger)
    {
        var size = checked((int)engine.Stack.Pop().GetInteger());
        Struct structure = [];

        for (var i = 0; i < size; i++)
            structure.Add(engine.Stack.Pop());
        engine.Stack.Push(structure);

        logger.LogTrace("Position={Position}, OpCode={Op}, Size={Result}", instruction.Position, instruction.OpCode, structure.Count);
        logger.LogDebug("Created: Type=Struct, Count={Count}, Value=[{Item}]", structure.Count, string.Join(", ", structure.Select(s => s.GetString())));
    }

    public virtual void Pack(Engine engine, Instruction instruction, ILogger logger)
    {
        var size = checked((int)engine.Stack.Pop().GetInteger());
        Array array = [];

        for (var i = 0; i < size; i++)
            array.Add(engine.Stack.Pop());
        engine.Stack.Push(array);

        logger.LogTrace("Position={Position}, OpCode={Op}, Size={Result}", instruction.Position, instruction.OpCode, array.Count);
        logger.LogDebug("Created: Type=Array, Count={Count}, Value=[{Item}]", array.Count, string.Join(", ", array.Select(s => s.GetString())));
    }
}
