// <copyright file="LexemeTestsExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Parser;
using OffDotNet.Pdf.CodeAnalysis.Text;

internal static class LexemeTestsExtensions
{
    internal static SlidingTextWindow ToTextWindow(this ReadOnlySpan<byte> text)
    {
        var sourceText = SourceText.From(text);
        return new SlidingTextWindow(sourceText);
    }
}
