// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NeoEvents.VirtualMachine.Tables;
using NeoEvents.VirtualMachine.Types;
using System;
using System.Collections.Generic;

namespace NeoEvents.VirtualMachine;

public class Engine(
    Instruction instructions,
    ILoggerFactory? loggerFactory = default,
    ExecuteTable? executeTable = default)
{
    public Exception? Exception { get; protected set; }
    public VMState State { get; protected set; } = VMState.NONE;
    public Stack<PrimitiveType> EvaluationStack => _evalStack;

    private readonly Stack<PrimitiveType> _evalStack = [];
    private readonly List<Instruction> _instructions = [.. instructions];
    private readonly ExecuteTable _executeTable = executeTable ?? ExecuteTable.Default;
    private readonly ILogger _logger = (ILogger?)loggerFactory?.CreateLogger<Engine>() ?? NullLogger.Instance;

    protected int _instructionPointer = 0;

    public VMState Run()
    {
        while (State != VMState.HALT && State != VMState.FAULT)
            StepOne();

        return State;
    }

    private void StepOne()
    {
        if (_instructionPointer == _instructions.Count)
            State = VMState.HALT;
        else
        {
            var instruction = _instructions[_instructionPointer];
            try
            {
                _executeTable[instruction.OpCode](this, instruction, _logger);
            }
            catch (Exception ex)
            {
                OnFault(ex, instruction);
            }
            _instructionPointer++;
        }
    }

    protected virtual void OnFault(Exception ex, Instruction instruction)
    {
        Exception = ex;
        State = VMState.FAULT;

        _logger.LogTrace(ex, "Failed to execute instruction: {OpCode}", instruction.OpCode);
    }
}
