// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;

namespace NeoEvents.VirtualMachine.Tables;

public partial class ExecuteTable
{
    public virtual void Add(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = a + b;

        engine.Stack.Push(result);

        logger.LogTrace("ADD: {a} + {b} = {result}", a, b, result);
    }
}
