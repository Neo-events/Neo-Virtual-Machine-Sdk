// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;

namespace NeoEvents.VirtualMachine.Tests.TestHelpers.Logging;

internal class TestLogger(
    ITestOutputHelper testOutputHelper,
    string categoryName) : ILogger
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;
    private readonly string _categoryName = categoryName;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull =>
        default!;

    public bool IsEnabled(LogLevel logLevel) =>
        true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _testOutputHelper.WriteLine($"[{DateTime.Now}] {logLevel}: {_categoryName}[{eventId.Id}] {formatter(state, exception)}");
        if (exception is not null)
            _testOutputHelper.WriteLine($"{exception}");
    }
}
