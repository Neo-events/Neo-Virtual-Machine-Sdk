// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System;
using System.Numerics;

namespace NeoEvents.VirtualMachine.Extensions;

internal static class BigIntegerExtensions
{
    public static BigInteger Sqrt(this BigInteger value)
    {
        if (value < 0) throw new InvalidOperationException("value can not be negative");
        if (value.IsZero) return BigInteger.Zero;
        if (value < 4) return BigInteger.One;

        var z = value;
        var x = BigInteger.One << (int)(((value - 1).GetBitLength() + 1) >> 1);
        while (x < z)
        {
            z = x;
            x = (value / x + x) / 2;
        }

        return z;
    }

    public static BigInteger ModInverse(this BigInteger value, BigInteger modulus)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
        ArgumentOutOfRangeException.ThrowIfLessThan(modulus, 2);

        BigInteger r = value, old_r = modulus, s = 1, old_s = 0;
        while (r > 0)
        {
            var q = old_r / r;
            (old_r, r) = (r, old_r % r);
            (old_s, s) = (s, old_s - q * s);
        }
        var result = old_s % modulus;
        if (result < 0) result += modulus;
        if (!(value * result % modulus).IsOne) throw new InvalidOperationException();
        return result;
    }
}
