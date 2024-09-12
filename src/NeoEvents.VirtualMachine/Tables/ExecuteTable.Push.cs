// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using System;
using System.Numerics;

namespace NeoEvents.VirtualMachine.Tables;

public partial class ExecuteTable
{
    public virtual void PushInt8(Engine engine, Instruction instruction, ILogger logger)
    {
        var result = new BigInteger(instruction.Operand.Value);

        engine.Stack.Push(result);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void PushInt16(Engine engine, Instruction instruction, ILogger logger)
    {
        var result = new BigInteger(instruction.Operand.Value);

        engine.Stack.Push(result);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void PushInt32(Engine engine, Instruction instruction, ILogger logger)
    {
        var result = new BigInteger(instruction.Operand.Value);

        engine.Stack.Push(result);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void PushInt64(Engine engine, Instruction instruction, ILogger logger)
    {
        var result = new BigInteger(instruction.Operand.Value);

        engine.Stack.Push(result);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void PushInt128(Engine engine, Instruction instruction, ILogger logger)
    {
        var result = new BigInteger(instruction.Operand.Value);

        engine.Stack.Push(result);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void PushInt256(Engine engine, Instruction instruction, ILogger logger)
    {
        var result = new BigInteger(instruction.Operand.Value);

        engine.Stack.Push(result);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void PushT(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(true);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, true);
    }

    public virtual void PushF(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(false);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, false);
    }

    public virtual void PushA(Engine engine, Instruction instruction, ILogger logger)
    {
        throw new NotImplementedException();
    }

    public virtual void PushNull(Engine engine, Instruction instruction, ILogger logger)
    {
        throw new NotImplementedException();
    }

    public virtual void PushData1(Engine engine, Instruction instruction, ILogger logger)
    {
        throw new NotImplementedException();
    }

    public virtual void PushData2(Engine engine, Instruction instruction, ILogger logger)
    {
        throw new NotImplementedException();
    }

    public virtual void PushData4(Engine engine, Instruction instruction, ILogger logger)
    {
        throw new NotImplementedException();
    }

    public virtual void PushM1(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(-1);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, -1);
    }

    public virtual void Push0(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(0);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 0);
    }

    public virtual void Push1(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(1);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 1);
    }

    public virtual void Push2(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(2);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 2);
    }

    public virtual void Push3(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(3);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 3);
    }

    public virtual void Push4(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(4);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 4);
    }

    public virtual void Push5(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(5);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 5);
    }

    public virtual void Push6(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(6);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 6);
    }

    public virtual void Push7(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(7);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 7);
    }

    public virtual void Push8(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(8);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 8);
    }

    public virtual void Push9(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(9);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 9);
    }

    public virtual void Push10(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(10);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 10);
    }

    public virtual void Push11(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(11);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 11);
    }

    public virtual void Push12(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(12);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 12);
    }

    public virtual void Push13(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(13);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 13);
    }

    public virtual void Push14(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(14);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 14);
    }

    public virtual void Push15(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(15);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 15);
    }

    public virtual void Push16(Engine engine, Instruction instruction, ILogger logger)
    {
        engine.Stack.Push(16);
        logger.LogDebug("Position={1}, OpCode={op}, Value={result}", instruction.Position, instruction.OpCode, 16);
    }
}
