// <copyright file="GreenNodeExtensionsTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;

public class GreenNodeExtensionsTests
{
    [Fact(DisplayName = "The GetFirstTerminal() extensions method must return null if providing a null input node.")]
    public void GetFirstTerminal_NullInputNode_ShouldReturnNull()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        SyntaxToken token = SyntaxFactory.Token(kind);

        // Act
        GreenNode? actualFirstTerminal = token.GetFirstTerminal();

        // Assert
        Assert.Null(actualFirstTerminal);
    }

    [Fact(DisplayName = "The GetLastTerminal() extensions method must return null if providing a null input node.")]
    public void GetLastTerminal_NullInputNode_ShouldReturnNull()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        SyntaxToken token = SyntaxFactory.Token(kind);

        // Act
        GreenNode? actualFirstTerminal = token.GetLastTerminal();

        // Assert
        Assert.Null(actualFirstTerminal);
    }

    [Fact(DisplayName = "The GetFirstTerminal() extensions method must return the first terminal node.")]
    public void GetFirstTerminal_ShouldFirstTerminalSyntaxToken()
    {
        // Arrange
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxToken objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, null, trailingTrivia);
        SyntaxToken generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "32", 32, null, trailingTrivia);

        LiteralExpressionSyntax objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
        LiteralExpressionSyntax generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        SyntaxToken referenceKeyword = SyntaxFactory.Token(SyntaxKind.IndirectReferenceKeyword, null, trailingTrivia);

        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(objectNumber, generationNumber, referenceKeyword);

        // Act
        GreenNode? actualFirstTerminal = indirectReference.GetFirstTerminal();

        // Assert
        Assert.Equal(objectNumberToken, actualFirstTerminal);
    }

    [Fact(DisplayName = "The GetLastTerminal() extensions method must return the last terminal node.")]
    public void GetLastTerminal_ShouldFirstTerminalSyntaxToken()
    {
        // Arrange
        GreenNode trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        SyntaxToken objectNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "123", 123, null, trailingTrivia);
        SyntaxToken generationNumberToken = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, "32", 32, null, trailingTrivia);

        LiteralExpressionSyntax objectNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, objectNumberToken);
        LiteralExpressionSyntax generationNumber = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, generationNumberToken);
        SyntaxToken referenceKeyword = SyntaxFactory.Token(SyntaxKind.IndirectReferenceKeyword, null, trailingTrivia);

        IndirectReferenceSyntax indirectReference = SyntaxFactory.IndirectReference(objectNumber, generationNumber, referenceKeyword);

        // Act
        GreenNode? actualLastTerminal = indirectReference.GetLastTerminal();

        // Assert
        Assert.Equal(referenceKeyword, actualLastTerminal);
    }
}
