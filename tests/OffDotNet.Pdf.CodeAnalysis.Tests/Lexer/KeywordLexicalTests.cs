// <copyright file="KeywordLexicalTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Lexer;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

public class KeywordLexicalTests : BaseTests
{
    [Theory(DisplayName = "The keyword state must parse the PDF keyword token.")]
    [InlineData("true", SyntaxKind.TrueKeyword)]
    [InlineData("false", SyntaxKind.FalseKeyword)]
    [InlineData("null", SyntaxKind.NullKeyword)]
    [InlineData("obj", SyntaxKind.StartObjectKeyword)]
    [InlineData("endobj", SyntaxKind.EndObjectKeyword)]
    [InlineData("R", SyntaxKind.IndirectReferenceKeyword)]
    [InlineData("stream", SyntaxKind.StartStreamKeyword)]
    [InlineData("endstream", SyntaxKind.EndStreamKeyword)]
    [InlineData("xref", SyntaxKind.XRefKeyword)]
    [InlineData("trailer", SyntaxKind.TrailerKeyword)]
    [InlineData("startxref", SyntaxKind.StartXRefKeyword)]
    [InlineData("PDF", SyntaxKind.PdfKeyword)]
    [InlineData("f", SyntaxKind.FreeXRefEntryKeyword)]
    [InlineData("n", SyntaxKind.InUseXRefEntryKeyword)]
    public void KeywordState_MustLexKeywordToken(string input, SyntaxKind expectedKind)
    {
        Test(input: input, expectedKind: expectedKind, expectedText: input);
    }

    [Theory(DisplayName = $"The keyword that cannot be recognized must be lexed as {nameof(SyntaxKind.UnknownKeyword)}.")]
    [InlineData("xyztoken")]
    [InlineData("startxreflongname")]
    public void KeywordState_NotRecognized_MustLexAsUnknownKeyword(string input)
    {
        Test(
            input: input,
            expectedKind: SyntaxKind.UnknownKeyword,
            expectedText: input,
            expectedErrors: [DiagnosticCode.ERR_InvalidKeyword]);
    }
}
