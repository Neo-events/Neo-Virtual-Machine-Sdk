// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System.Numerics;

namespace NeoEvents.VirtualMachine.Types.Tests;

public class UT_Integer
{
    [Fact]
    public void Test_IEquatable()
    {
        var integer1 = new Integer(BigInteger.One);
        var integer2 = new Integer(1);
        var integer3 = new Integer(2);
        Integer integer4 = default!;

        Assert.True(integer1 == integer2);
        Assert.True(integer1.Equals(integer2));
        Assert.Equal(integer1, integer2);
        Assert.Equal(integer1.GetInteger(), integer2.GetInteger());

        Assert.False(integer1 == integer3);
        Assert.False(integer1.Equals(integer3));
        Assert.NotEqual(integer1, integer3);
        Assert.NotEqual(integer1.GetInteger(), integer3.GetInteger());

        Assert.Null(integer4);
        Assert.True(integer4 is null);
        Assert.False(integer1.Equals(integer4));
        Assert.NotEqual(integer1, integer4);
    }

    [Fact]
    public void Test_Implicit_Cast()
    {
        var integer1 = (Integer)BigInteger.One;
        var integer2 = (Integer)sbyte.MaxValue;
        var integer3 = (Integer)byte.MaxValue;
        var integer4 = (Integer)short.MaxValue;
        var integer5 = (Integer)ushort.MaxValue;
        var integer6 = (Integer)int.MaxValue;
        var integer7 = (Integer)uint.MaxValue;
        var integer8 = (Integer)long.MaxValue;
        var integer9 = (Integer)ulong.MaxValue;
    }

    [Fact]
    public void Test_Size()
    {
        var integer1 = new Integer(1);
        var integer2 = new Integer(ulong.MaxValue);

        Assert.Equal(1, integer1.Size);
        Assert.Equal(9, integer2.Size);
    }

    [Fact]
    public void Test_Type()
    {
        var integer1 = new Integer(1);

        Assert.Equal(StackItemType.Integer, integer1.Type);
    }

    [Fact]
    public void Test_GetSpan()
    {
        var integer1 = new Integer(1);
        byte[] expectedBytes = [0x01];

        Assert.Equal(expectedBytes, integer1.Memory.Span.ToArray());
        Assert.Equal(expectedBytes, integer1.GetSpan().ToArray());
    }

    [Fact]
    public void Test_GetInteger()
    {
        var integer1 = new Integer(1);

        Assert.Equal(BigInteger.One, integer1.GetInteger());
        Assert.Equal(1, integer1.GetInteger());
    }

    [Fact]
    public void Test_GetBoolean()
    {
        var integer1 = new Integer(1);
        var integer2 = new Integer(0);
        var integer3 = new Integer(999);

        Assert.True(integer1.GetBoolean());
        Assert.False(integer2.GetBoolean());
        Assert.True(integer3.GetBoolean());
    }

    [Fact]
    public void Test_GetString()
    {
        var integer1 = new Integer(1);

        Assert.Equal("1", integer1.GetString());
    }
}
