// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

namespace NeoEvents.VirtualMachine.Types.Tests;

public class UT_Map
{
    [Fact]
    public void Test_Constructor()
    {
        Map map1 = new()
        {
            [1] = 2,
            [3] = 4,
            [5] = 6,
            [7] = 8,
            [9] = 10,
        };

        Assert.Equal(5, map1.Count);
        Assert.Equal(StackItemType.Map, map1.Type);
        Assert.Equal(10, map1.Memory.Length);
        Assert.Equal(2, map1[1]);
        Assert.Equal(4, map1[3]);
        Assert.Equal(6, map1[5]);
        Assert.Equal(8, map1[7]);
        Assert.Equal(10, map1[9]);
    }

    [Fact]
    public void Test_IEquatable()
    {
        Map map1 = new()
        {
            [1] = 2,
            [3] = 4,
            [5] = 6,
            [7] = 8,
            [9] = 10,
            [true] = false,
            [new byte[] { 1, 2, 3 }] = new byte[] { 4, 5, 6 },
        };

        Map map2 = new()
        {
            [1] = 2,
            [3] = 4,
            [5] = 6,
            [7] = 8,
            [9] = 10,
            [true] = false,
            [new byte[] { 1, 2, 3 }] = new byte[] { 4, 5, 6 },
        };


        Assert.True(map1 == map2);
        Assert.True(map1.Equals(map2));
        Assert.Equal(map1, map2);
        Assert.Equal(map1.GetHashCode(), map2.GetHashCode());
    }
}
