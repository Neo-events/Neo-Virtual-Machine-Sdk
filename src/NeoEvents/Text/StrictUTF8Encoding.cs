// Licensed to the "Neo Events" under one or more agreements.
// The "Neo Events" licenses this file to you under the GPL-3.0 license.

using System.Text;

namespace NeoEvents.Text;

/// <summary>
/// No BOM, throw on invalid bytes.
/// </summary>
public class StrictUTF8Encoding : UTF8Encoding
{
    public StrictUTF8Encoding() : base(false, true) { }
}
