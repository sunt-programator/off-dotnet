// <copyright file="KeywordStateLexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

public class KeywordStateLexicalTests : BaseTests
{
    [Theory(DisplayName = "The keyword state must parse the PDF keyword token.")]
    [InlineData("true", SyntaxKind.TrueKeyword)]
    [InlineData("false", SyntaxKind.FalseKeyword)]
    public void KeywordState_MustParseKeywordToken(string input, SyntaxKind expectedKind)
    {
        Test(input: input, expectedKind: expectedKind, expectedText: input);
    }

    [Theory(DisplayName = $"The keyword that cannot be recognized must be parsed as {nameof(SyntaxKind.UnknownKeyword)}.")]
    [InlineData("xyztoken")]
    [InlineData("startxreflongname")]
    public void KeywordState_NotRecognized_MustParseAsUnknownKeyword(string input)
    {
        Test(
            input: input,
            expectedKind: SyntaxKind.UnknownKeyword,
            expectedText: input,
            expectedErrors: [DiagnosticCode.ERR_InvalidKeyword]);
    }
}
