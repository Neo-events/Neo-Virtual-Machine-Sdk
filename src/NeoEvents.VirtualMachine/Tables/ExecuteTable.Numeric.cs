// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using NeoEvents.VirtualMachine.Extensions;
using System.Numerics;

namespace NeoEvents.VirtualMachine.Tables;

public partial class ExecuteTable
{
    public virtual void Add(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = a + b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Sign(Engine engine, Instruction instruction, ILogger logger)
    {
        var a = engine.Stack.Pop().GetInteger();
        var result = a.Sign;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Abs(Engine engine, Instruction instruction, ILogger logger)
    {
        var a = engine.Stack.Pop().GetInteger();
        var result = BigInteger.Abs(a);

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Negate(Engine engine, Instruction instruction, ILogger logger)
    {
        var a = engine.Stack.Pop().GetInteger();
        var result = -a;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Inc(Engine engine, Instruction instruction, ILogger logger)
    {
        var a = engine.Stack.Pop().GetInteger();
        var result = a + BigInteger.One;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Dec(Engine engine, Instruction instruction, ILogger logger)
    {
        var a = engine.Stack.Pop().GetInteger();
        var result = a - BigInteger.One;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Sub(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = a - b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Mul(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = a * b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Div(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = a / b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Mod(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = a % b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Pow(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = checked((int)engine.Stack.Pop().GetInteger());
        var a = engine.Stack.Pop().GetInteger();
        var result = BigInteger.Pow(a, b);

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Sqrt(Engine engine, Instruction instruction, ILogger logger)
    {
        var a = engine.Stack.Pop().GetInteger();
        var result = a.Sqrt();

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void ModMul(Engine engine, Instruction instruction, ILogger logger)
    {
        var c = engine.Stack.Pop().GetInteger();
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = a * b % c;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void ModPow(Engine engine, Instruction instruction, ILogger logger)
    {
        var c = engine.Stack.Pop().GetInteger();
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = b == -1
            ? a.ModInverse(c)
            : BigInteger.ModPow(a, b, c);

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Shl(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = checked((int)engine.Stack.Pop().GetInteger());
        if (b == 0) return;

        var a = engine.Stack.Pop().GetInteger();
        var result = a << b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Shr(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = checked((int)engine.Stack.Pop().GetInteger());
        if (b == 0) return;

        var a = engine.Stack.Pop().GetInteger();
        var result = a >> b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Not(Engine engine, Instruction instruction, ILogger logger)
    {
        var a = engine.Stack.Pop().GetBoolean();
        var result = !a;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void BoolAnd(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetBoolean();
        var a = engine.Stack.Pop().GetBoolean();
        var result = a && b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void BoolOr(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetBoolean();
        var a = engine.Stack.Pop().GetBoolean();
        var result = a || b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Nz(Engine engine, Instruction instruction, ILogger logger)
    {
        var a = engine.Stack.Pop().GetInteger();
        var result = !a.IsZero;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void NumEqual(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetBoolean();
        var a = engine.Stack.Pop().GetBoolean();
        var result = a == b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void NumNotEqual(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetBoolean();
        var a = engine.Stack.Pop().GetBoolean();
        var result = a != b;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Lt(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop();
        var a = engine.Stack.Pop();
        bool result;

        if (a.IsNull || b.IsNull)
            result = false;
        else
            result = a.GetInteger() < b.GetInteger();

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Le(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop();
        var a = engine.Stack.Pop();
        bool result;

        if (a.IsNull || b.IsNull)
            result = false;
        else
            result = a.GetInteger() <= b.GetInteger();

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Gt(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop();
        var a = engine.Stack.Pop();
        bool result;

        if (a.IsNull || b.IsNull)
            result = false;
        else
            result = a.GetInteger() > b.GetInteger();

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Ge(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop();
        var a = engine.Stack.Pop();
        bool result;

        if (a.IsNull || b.IsNull)
            result = false;
        else
            result = a.GetInteger() >= b.GetInteger();

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Min(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = BigInteger.Min(a, b);

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Max(Engine engine, Instruction instruction, ILogger logger)
    {
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = BigInteger.Max(a, b);

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }

    public virtual void Within(Engine engine, Instruction instruction, ILogger logger)
    {
        var c = engine.Stack.Pop().GetInteger();
        var b = engine.Stack.Pop().GetInteger();
        var a = engine.Stack.Pop().GetInteger();
        var result = b <= a && a < c;

        engine.Stack.Push(result);

        logger.LogTrace("Position={Position}, OpCode={Op}, Value={Result}", instruction.Position, instruction.OpCode, result);
    }
}
