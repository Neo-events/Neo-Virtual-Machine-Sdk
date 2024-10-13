// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using NeoEvents.VirtualMachine.Types;
using System.Linq;

namespace NeoEvents.VirtualMachine.Tables;

public partial class ExecuteTable
{
    public virtual void PackMap(Engine engine, Instruction instruction, ILogger logger)
    {
        var size = checked((int)engine.EvaluationStack.Pop().GetInteger());
        Map map = [];

        for (var i = 0; i < size; i++)
        {
            var key = engine.EvaluationStack.Pop();
            var value = engine.EvaluationStack.Pop();
            map.Add(key, value);
        }
        engine.EvaluationStack.Push(map);

        logger.LogTrace("Position={Position}, OpCode={Op}, Size={Result}", instruction.Position, instruction.OpCode, map.Count);
        logger.LogDebug("Created: Type=Map, Count={Count}", map.Count);
    }

    public virtual void PackStruct(Engine engine, Instruction instruction, ILogger logger)
    {
        var size = checked((int)engine.EvaluationStack.Pop().GetInteger());
        Struct structure = [];

        for (var i = 0; i < size; i++)
            structure.Add(engine.EvaluationStack.Pop());
        engine.EvaluationStack.Push(structure);

        logger.LogTrace("Position={Position}, OpCode={Op}, Size={Result}", instruction.Position, instruction.OpCode, structure.Count);
        logger.LogDebug("Created: Type=Struct, Count={Count}, Value=[{Item}]", structure.Count, string.Join(", ", structure.Select(s => s.GetString())));
    }

    public virtual void Pack(Engine engine, Instruction instruction, ILogger logger)
    {
        var size = checked((int)engine.EvaluationStack.Pop().GetInteger());
        Array array = [];

        for (var i = 0; i < size; i++)
            array.Add(engine.EvaluationStack.Pop());
        engine.EvaluationStack.Push(array);

        logger.LogTrace("Position={Position}, OpCode={Op}, Size={Result}", instruction.Position, instruction.OpCode, array.Count);
        logger.LogDebug("Created: Type=Array, Count={Count}, Value=[{Item}]", array.Count, string.Join(", ", array.Select(s => s.GetString())));
    }
}
