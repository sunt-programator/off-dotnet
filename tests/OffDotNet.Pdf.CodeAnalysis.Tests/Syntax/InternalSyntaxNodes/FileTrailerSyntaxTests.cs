// <copyright file="FileTrailerSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxTrivia;

public class FileTrailerSyntaxTests
{
    private readonly SyntaxToken trailerKeyword;
    private readonly DictionaryExpressionSyntax trailerDictionary;
    private readonly SyntaxToken startXRefKeyword;
    private readonly LiteralExpressionSyntax byteOffset;

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

        SyntaxToken openBracketToken = SyntaxFactory.Token(SyntaxKind.LessThanLessThanToken, trivia, trivia);
        SyntaxToken closeBracketToken = SyntaxFactory.Token(SyntaxKind.GreaterThanGreaterThanToken, trivia, trivia);
        DictionaryExpressionSyntax dictionaryExpressionSyntax = SyntaxFactory.DictionaryExpression(openBracketToken, elements, closeBracketToken);

        this.trailerKeyword = SyntaxFactory.Token(SyntaxKind.TrailerKeyword, trivia, trivia);
        this.trailerDictionary = dictionaryExpressionSyntax;
        this.startXRefKeyword = SyntaxFactory.Token(SyntaxKind.StartXRefKeyword, trivia, trivia);
        this.byteOffset = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Token(SyntaxKind.NameLiteralToken, "123", 123, trivia, trivia));
    }

    [Fact(DisplayName = $"The {nameof(FileTrailerSyntax.Kind)} property must be {nameof(SyntaxKind.FileTrailer)}")]
    public void KindProperty_MustBeFileTrailer()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        SyntaxKind actualKind = fileTrailerSyntax.Kind;

        // Assert
        Assert.Equal(SyntaxKind.FileTrailer, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(FileTrailerSyntax.TrailerKeyword)} property must be assigned from constructor.")]
    public void TrailerKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        SyntaxToken actualTrailerKeyword = fileTrailerSyntax.TrailerKeyword;

        // Assert
        Assert.Equal(this.trailerKeyword, actualTrailerKeyword);
    }

    [Fact(DisplayName = $"The {nameof(FileTrailerSyntax.TrailerDictionary)} property must be assigned from constructor.")]
    public void TrailerDictionaryProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        CollectionExpressionSyntax actualTrailerDictionary = fileTrailerSyntax.TrailerDictionary;

        // Assert
        Assert.Equal(this.trailerDictionary, actualTrailerDictionary);
    }

    [Fact(DisplayName = $"The {nameof(FileTrailerSyntax.StartXRefKeyword)} property must be assigned from constructor.")]
    public void StartXRefKeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        SyntaxToken actualStartXRefKeyword = fileTrailerSyntax.StartXRefKeyword;

        // Assert
        Assert.Equal(this.startXRefKeyword, actualStartXRefKeyword);
    }

    [Fact(DisplayName = $"The {nameof(FileTrailerSyntax.ByteOffset)} property must be assigned from constructor.")]
    public void ByteOffsetProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        LiteralExpressionSyntax actualByteOffset = fileTrailerSyntax.ByteOffset;

        // Assert
        Assert.Equal(this.byteOffset, actualByteOffset);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.SlotCount)} property must be equal to 5.")]
    public void SlotCountProperty_MustBeEqualTo4()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        int actualSlotCount = fileTrailerSyntax.SlotCount;

        // Assert
        Assert.Equal(4, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(FileTrailerSyntax.TrailerKeyword)} property.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        GreenNode? actualSlot = fileTrailerSyntax.GetSlot(0);

        // Assert
        Assert.Equal(this.trailerKeyword, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 1 must return {nameof(FileTrailerSyntax.TrailerDictionary)} property.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        GreenNode? actualSlot = fileTrailerSyntax.GetSlot(1);

        // Assert
        Assert.Equal(this.trailerDictionary, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 2 must return {nameof(FileTrailerSyntax.StartXRefKeyword)} property.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        GreenNode? actualSlot = fileTrailerSyntax.GetSlot(2);

        // Assert
        Assert.Equal(this.startXRefKeyword, actualSlot);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 3 must return {nameof(FileTrailerSyntax.ByteOffset)} property.")]
    public void GetSlotMethod_Index3_MustReturnFourthObject()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        GreenNode? actualSlot = fileTrailerSyntax.GetSlot(3);

        // Assert
        Assert.Equal(this.byteOffset, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 4 must return null.")]
    public void GetSlotMethod_Index4_MustReturnNull()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        GreenNode? actualSlot = fileTrailerSyntax.GetSlot(4);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        int actualWidth = fileTrailerSyntax.Width;

        // Assert
        Assert.Equal(59, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        int actualFullWidth = fileTrailerSyntax.FullWidth;

        // Assert
        Assert.Equal(61, actualFullWidth);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const string expectedString = "trailer  <<  /SomeName  123  /SomeName  123  >>  startxref  123";
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        string actualString = fileTrailerSyntax.ToString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const string expectedString = " trailer  <<  /SomeName  123  /SomeName  123  >>  startxref  123 ";
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);

        // Act
        string actualString = fileTrailerSyntax.ToFullString();

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetTheDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        FileTrailerSyntax fileTrailerSyntax = SyntaxFactory.FileTrailer(this.trailerKeyword, this.trailerDictionary, this.startXRefKeyword, this.byteOffset);
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = { expectedDiagnostic };

        // Act
        FileTrailerSyntax actualFileTrailerSyntax = (FileTrailerSyntax)fileTrailerSyntax.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(fileTrailerSyntax, actualFileTrailerSyntax);
        Assert.Equal(diagnostics, actualFileTrailerSyntax.GetDiagnostics());
        Assert.True(actualFileTrailerSyntax.ContainsFlags(GreenNode.NodeFlags.ContainsDiagnostics));
    }
}
