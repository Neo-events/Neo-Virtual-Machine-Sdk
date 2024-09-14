// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using NeoEvents.VirtualMachine.Types;

namespace NeoEvents.VirtualMachine.Tables;

public partial class ExecuteTable
{
    public virtual void Pack(Engine engine, Instruction instruction, ILogger logger)
    {
        var size = (int)engine.Stack.Pop().GetInteger();
        Array array = [];

        for (var i = 0; i < size; i++)
            array.Add(engine.Stack.Pop());
        engine.Stack.Push(array);

        logger.LogDebug("Position={1}, OpCode={op}, Size={result}", instruction.Position, instruction.OpCode, array.Count);
    }
}
