// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using Xunit.Abstractions;

namespace NeoEvents.VirtualMachine.Tests.TestHelpers.Logging;

internal sealed class TestLoggerProvider(ITestOutputHelper testOutputHelper)
    : ILoggerProvider
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;
    private readonly ConcurrentDictionary<string, TestLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new TestLogger(_testOutputHelper, name));

    public void Dispose()
    {
        _loggers.Clear();
    }
}
