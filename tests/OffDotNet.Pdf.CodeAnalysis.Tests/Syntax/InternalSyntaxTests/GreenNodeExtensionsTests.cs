// <copyright file="GreenNodeExtensionsTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;

public class GreenNodeExtensionsTests
{
    [Fact(DisplayName = "The GetFirstTerminal() extensions method must return null if providing a null input node.")]
    public void GetFirstTerminal_NullInputNode_ShouldReturnNull()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        var token = SyntaxFactory.Token(Kind);

        // Act
        var actualFirstTerminal = token.GetFirstTerminal();

        // Assert
        Assert.Null(actualFirstTerminal);
    }

    [Fact(DisplayName = "The GetLastTerminal() extensions method must return null if providing a null input node.")]
    public void GetLastTerminal_NullInputNode_ShouldReturnNull()
    {
        // Arrange
        const SyntaxKind Kind = SyntaxKind.TrueKeyword;
        var token = SyntaxFactory.Token(Kind);

        // Act
        var actualFirstTerminal = token.GetLastTerminal();

        // Assert
        Assert.Null(actualFirstTerminal);
    }

    [Fact(DisplayName = "The GetFirstTerminal() extensions method must return the first terminal node.")]
    public void GetFirstTerminal_ShouldFirstTerminalSyntaxToken()
    {
        // Arrange
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, null, trailingTrivia);
        var generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "32", 32, null, trailingTrivia);

        var objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
        var generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        var referenceKeyword = SyntaxFactory.Token(SyntaxKind.IndirectReferenceKeyword, null, trailingTrivia);

        var indirectReference = SyntaxFactory.IndirectReference(objectNumber, generationNumber, referenceKeyword);

        // Act
        var actualFirstTerminal = indirectReference.GetFirstTerminal();

        // Assert
        Assert.Equal(objectNumberToken, actualFirstTerminal);
    }

    [Fact(DisplayName = "The GetLastTerminal() extensions method must return the last terminal node.")]
    public void GetLastTerminal_ShouldFirstTerminalSyntaxToken()
    {
        // Arrange
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, null, trailingTrivia);
        var generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "32", 32, null, trailingTrivia);

        var objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
        var generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        var referenceKeyword = SyntaxFactory.Token(SyntaxKind.IndirectReferenceKeyword, null, trailingTrivia);

        var indirectReference = SyntaxFactory.IndirectReference(objectNumber, generationNumber, referenceKeyword);

        // Act
        var actualLastTerminal = indirectReference.GetLastTerminal();

        // Assert
        Assert.Equal(referenceKeyword, actualLastTerminal);
    }
}
