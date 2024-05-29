﻿// <copyright file="FileTrailerSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxNodes;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntaxNodes;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;
using SyntaxToken = CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

public class FileTrailerSyntaxTests
{
    private readonly SyntaxToken _trailerKeyword;
    private readonly DictionaryExpressionSyntax _trailerDictionary;
    private readonly SyntaxToken _startXRefKeyword;
    private readonly LiteralExpressionSyntax _byteOffset;

    public FileTrailerSyntaxTests()
    {
        GreenNode trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " ");

        var key = SyntaxFactory.LiteralExpression(
            SyntaxKind.NameLiteralExpression,
            SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "/SomeName", trivia, trivia));

        var value = SyntaxFactory.LiteralExpression(
            SyntaxKind.NumericLiteralExpression,
            SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "123", 123, trivia, trivia));

        CollectionElementSyntax keyValue = SyntaxFactory.DictionaryElement(key, value);

        SyntaxList<DictionaryElementSyntax> elements = new SyntaxListBuilder(2)
            .AddRange(new GreenNode[] { keyValue, keyValue })
            .ToList<DictionaryElementSyntax>();

        var openBracketToken = SyntaxFactory.Token(SyntaxKind.LessThanLessThanToken, trivia, trivia);
        var closeBracketToken = SyntaxFactory.Token(SyntaxKind.GreaterThanGreaterThanToken, trivia, trivia);
        var dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(openBracketToken, elements, closeBracketToken);

        _trailerKeyword = SyntaxFactory.Token(SyntaxKind.TrailerKeyword, trivia, trivia);
        _trailerDictionary = dictionaryExpressionSyntax;
        _startXRefKeyword = SyntaxFactory.Token(SyntaxKind.StartXRefKeyword, trivia, trivia);
        _byteOffset = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "123", 123, trivia, trivia));
    }

    [Fact(DisplayName = $"The {nameof(FileTrailerSyntax.Kind)} property must be {nameof(SyntaxKind.FileTrailer)}")]
    public void KindProperty_MustBeFileTrailer()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualKind = fileTrailerSyntax.Kind;

        // Assert
        Assert.Equal(SyntaxKind.FileTrailer, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(FileTrailerSyntax.TrailerKeyword)} property must be assigned from constructor.")]
    public void TrailerKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualTrailerKeyword = fileTrailerSyntax.TrailerKeyword;

        // Assert
        Assert.Same(_trailerKeyword, actualTrailerKeyword);
    }

    [Fact(DisplayName = $"The {nameof(FileTrailerSyntax.TrailerDictionary)} property must be assigned from constructor.")]
    public void TrailerDictionaryProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        CollectionExpressionSyntax actualTrailerDictionary = fileTrailerSyntax.TrailerDictionary;

        // Assert
        Assert.Same(_trailerDictionary, actualTrailerDictionary);
    }

    [Fact(DisplayName = $"The {nameof(FileTrailerSyntax.StartXRefKeyword)} property must be assigned from constructor.")]
    public void StartXRefKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualStartXRefKeyword = fileTrailerSyntax.StartXRefKeyword;

        // Assert
        Assert.Same(_startXRefKeyword, actualStartXRefKeyword);
    }

    [Fact(DisplayName = $"The {nameof(FileTrailerSyntax.ByteOffset)} property must be assigned from constructor.")]
    public void ByteOffsetProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualByteOffset = fileTrailerSyntax.ByteOffset;

        // Assert
        Assert.Same(_byteOffset, actualByteOffset);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Count)} property must be equal to 5.")]
    public void SlotCountProperty_MustBeEqualTo4()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualSlotCount = fileTrailerSyntax.Count;

        // Assert
        Assert.Equal(4, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(FileTrailerSyntax.TrailerKeyword)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualSlot = fileTrailerSyntax.GetSlot(0);

        // Assert
        Assert.Same(_trailerKeyword, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(FileTrailerSyntax.TrailerDictionary)} property.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualSlot = fileTrailerSyntax.GetSlot(1);

        // Assert
        Assert.Same(_trailerDictionary, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(FileTrailerSyntax.StartXRefKeyword)} property.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualSlot = fileTrailerSyntax.GetSlot(2);

        // Assert
        Assert.Same(_startXRefKeyword, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 3 must return {nameof(FileTrailerSyntax.ByteOffset)} property.")]
    public void GetSlotMethod_Index3_MustReturnFourthObject()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualSlot = fileTrailerSyntax.GetSlot(3);

        // Assert
        Assert.Same(_byteOffset, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 4 must return null.")]
    public void GetSlotMethod_Index4_MustReturnNull()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualSlot = fileTrailerSyntax.GetSlot(4);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualWidth = fileTrailerSyntax.Width;

        // Assert
        Assert.Equal(59, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualFullWidth = fileTrailerSyntax.FullWidth;

        // Assert
        Assert.Equal(61, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = "trailer  <<  /SomeName  123  /SomeName  123  >>  startxref  123";
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualString = fileTrailerSyntax.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string ExpectedString = " trailer  <<  /SomeName  123  /SomeName  123  >>  startxref  123 ";
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);

        // Act
        var actualString = fileTrailerSyntax.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetTheDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        var fileTrailerSyntax = SyntaxFactory.FileTrailer(_trailerKeyword, _trailerDictionary, _startXRefKeyword, _byteOffset);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualFileTrailerSyntax = (FileTrailerSyntax)fileTrailerSyntax.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(fileTrailerSyntax, actualFileTrailerSyntax);
        Assert.Equal(diagnostics, actualFileTrailerSyntax.GetDiagnostics());
        Assert.True(actualFileTrailerSyntax.ContainsFlags(NodeFlags.ContainsDiagnostics));
    }
}
