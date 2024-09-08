// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System.Text;

namespace NeoEvents.VirtualMachine;

public class StrictUTF8 : UTF8Encoding
{
    public StrictUTF8()
    {
        DecoderFallback = DecoderFallback.ExceptionFallback;
        EncoderFallback = EncoderFallback.ExceptionFallback;
    }
}
