// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

namespace NeoEvents.VirtualMachine.Types.Tests;

public class UT_ByteString
{
    [Fact]
    public void Test_Constructor()
    {
        var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var bs1 = new ByteString(bytes);

        Assert.Equal(bytes, bs1.Memory);
    }

    [Fact]
    public void Test_IEquatable()
    {
        var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var bs1 = new ByteString(bytes);
        var bs2 = new ByteString(bytes);

        Assert.True(bs1 == bs2);
        Assert.True(bs1.Equals(bs2));
        Assert.Equal(bs1, bs2);
    }

    [Fact]
    public void Test_Size()
    {
        var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var bs1 = new ByteString(bytes);

        Assert.Equal(4, bs1.Size);
    }

    [Fact]
    public void Test_Type()
    {
        var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var bs1 = new ByteString(bytes);

        Assert.Equal(StackItemType.ByteString, bs1.Type);
    }

    [Fact]
    public void Test_GetSpan()
    {
        var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var bs1 = new ByteString(bytes);

        Assert.Equal(bytes, bs1.Memory);
        Assert.Equal(bytes, bs1.GetSpan().ToArray());
    }

    [Fact]
    public void Test_GetInteger()
    {
        var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var bs1 = new ByteString(bytes);

        Assert.Equal(0x04030201, bs1.GetInteger());
    }

    [Fact]
    public void Test_GetBoolean()
    {
        var bs1 = new ByteString(new byte[] { 0x01, 0x02, 0x03, 0x04 });
        var bs2 = new ByteString(new byte[] { 0x00, 0x00, 0x00, 0x00 });

        Assert.True(bs1.GetBoolean());
        Assert.False(bs2.GetBoolean());
    }

    [Fact]
    public void Test_GetString()
    {
        var bs1 = (ByteString)"Hello World!";

        Assert.Equal("Hello World!", bs1.GetString());
    }
}
