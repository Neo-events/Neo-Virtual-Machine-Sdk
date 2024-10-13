// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

namespace NeoEvents.VirtualMachine.Types.Tests;

public class UT_Array
{
    [Fact]
    public void Test_Constructor()
    {
        var array1 = new Array();
        var array2 = new Array([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);

        Array array3 =
        [
            true,
            1,
            new byte[] { 1 },
            PrimitiveType.Null,
            array2,
        ];

        Assert.Empty(array1);
        Assert.Equal(StackItemType.Array, array1.Type);
        Assert.Equal(0, array1.Memory.Length);

        Assert.Equal([1, 2, 3, 4, 5, 6, 7, 8, 9, 10], array2);
        Assert.Equal(StackItemType.Array, array2.Type);
        Assert.Equal([1, 2, 3, 4, 5, 6, 7, 8, 9, 10], array2.Memory.ToArray());
        Assert.Equal(10, array2.Memory.Length);

        Assert.Equal([true, 1, new byte[] { 1 }, PrimitiveType.Null, array2], array3);
        Assert.Equal([1, 1, 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10], array3.Memory.ToArray());
        Assert.Equal(StackItemType.Array, array3.Type);
        Assert.Equal(13, array3.Memory.Length);
    }

    [Fact]
    public void Test_IEquatable()
    {
        Array array1 =
        [
            true,
            1,
            new byte[] { 1 },
            PrimitiveType.Null,
            new Array([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]),
        ];

        Array array2 =
        [
            true,
            1,
            new byte[] { 1 },
            PrimitiveType.Null,
            new Array([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]),
        ];

        Assert.True(array1 == array2);
        Assert.True(array1.Equals(array2));
        Assert.Equal(array1, array2);
        Assert.Equal(array1.GetHashCode(), array2.GetHashCode());
    }
}
