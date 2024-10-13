// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;

namespace NeoEvents.VirtualMachine.Types.Tests;

public class UT_Null
{
    [Fact]
    public void Test_IEquatable()
    {
        var null1 = new Null();
        var null2 = PrimitiveType.Null;

        Assert.True(null1 == null2);
        Assert.True(null1.Equals(null2));
        Assert.Equal(null1, null2);
        Assert.Equal(null1.GetHashCode(), null2.GetHashCode());
    }

    [Fact]
    public void Test_Size()
    {
        var null1 = new Null();

        Assert.Equal(0, null1.Size);
    }

    [Fact]
    public void Test_Type()
    {
        var null1 = new Null();

        Assert.Equal(StackItemType.Any, null1.Type);
    }

    [Fact]
    public void Test_GetSpan()
    {
        var null1 = new Null();
        byte[] expectedBytes = [];

        Assert.Equal(expectedBytes, null1.Memory.Span.ToArray());
        Assert.Equal(expectedBytes, null1.GetSpan().ToArray());
    }

    [Fact]
    public void Test_GetInteger()
    {
        var null1 = new Null();

        Assert.Throws<InvalidCastException>(() => null1.GetInteger());
    }

    [Fact]
    public void Test_GetBoolean()
    {
        var null1 = new Null();

        Assert.False(null1.GetBoolean());
    }

    [Fact]
    public void Test_GetString()
    {
        var null1 = new Null();

        Assert.Null(null1.GetString());
    }
}
