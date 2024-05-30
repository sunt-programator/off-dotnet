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
        Assert.Same(objectNumberToken, actualFirstTerminal);
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
        Assert.Same(referenceKeyword, actualLastTerminal);
    }

    [Fact(DisplayName = "The CreateList() method must return null if the list is empty.")]
    public void CreateList_EmptyList_ShouldReturnNull()
    {
        // Arrange
        var list = new SyntaxTriviaList(default, null, 56, 10);

        // Act
        var actualList = GreenNodeExtensions.CreateList(list, static x => x.UnderlyingNode);

        // Assert
        Assert.Null(actualList);
    }

    [Fact(DisplayName = "The CreateList() method must return a new list with 1 element.")]
    public void CreateList_OneElementList_ShouldReturnList()
    {
        // Arrange
        var triviaGreenNode = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var triviaListGreenNode = SyntaxListBuilder.Create().Add(triviaGreenNode).ToListNode();

        var list = new SyntaxTriviaList(default, triviaListGreenNode, 56, 10);

        // Act
        var actualList = GreenNodeExtensions.CreateList(list, static x => x.UnderlyingNode);

        // Assert
        Assert.NotNull(actualList);
        Assert.NotEqual(SyntaxKind.List, actualList.Kind);
        Assert.Same(triviaGreenNode, actualList);
    }

    [Fact(DisplayName = "The CreateList() method must return a new list with 2 elements.")]
    public void CreateList_TwoElementsList_ShouldReturnList()
    {
        // Arrange
        var trivia = SyntaxListBuilder.Create()
            .Add(SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " "))
            .Add(SyntaxFactory.Trivia(SyntaxKind.SingleLineCommentTrivia, "% comment"))
            .ToListNode();

        var list = new SyntaxTriviaList(default, trivia, 56, 10);

        // Act
        var actualList = GreenNodeExtensions.CreateList(list, static x => x.UnderlyingNode);

        // Assert
        Assert.NotNull(actualList);
        Assert.Equal(SyntaxKind.List, actualList.Kind);
        Assert.Equal(2, actualList.Count);
    }

    [Fact(DisplayName = "The CreateList() method must return a new list with 3 elements.")]
    public void CreateList_ThreeElementsList_ShouldReturnList()
    {
        // Arrange
        var trivia = SyntaxListBuilder.Create()
            .Add(SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " "))
            .Add(SyntaxFactory.Trivia(SyntaxKind.SingleLineCommentTrivia, "% comment"))
            .Add(SyntaxFactory.Trivia(SyntaxKind.EndOfLineTrivia, "\n"))
            .ToListNode();

        var list = new SyntaxTriviaList(default, trivia, 56, 10);

        // Act
        var actualList = GreenNodeExtensions.CreateList(list, static x => x.UnderlyingNode);

        // Assert
        Assert.NotNull(actualList);
        Assert.Equal(SyntaxKind.List, actualList.Kind);
        Assert.Equal(3, actualList.Count);
    }

    [Fact(DisplayName = "The CreateList() method must return a new list with more than 3 elements.")]
    public void CreateList_MoreThanThreeElementsList_ShouldReturnList()
    {
        // Arrange
        var trivia = SyntaxListBuilder.Create()
            .Add(SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " "))
            .Add(SyntaxFactory.Trivia(SyntaxKind.SingleLineCommentTrivia, "% comment"))
            .Add(SyntaxFactory.Trivia(SyntaxKind.EndOfLineTrivia, "\n"))
            .Add(SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " "))
            .Add(SyntaxFactory.Trivia(SyntaxKind.SingleLineCommentTrivia, "% comment"))
            .Add(SyntaxFactory.Trivia(SyntaxKind.EndOfLineTrivia, "\n"))
            .ToListNode();

        var list = new SyntaxTriviaList(default, trivia, 56, 10);

        // Act
        var actualList = GreenNodeExtensions.CreateList(list, static x => x.UnderlyingNode);

        // Assert
        Assert.NotNull(actualList);
        Assert.Equal(SyntaxKind.List, actualList.Kind);
        Assert.Equal(6, actualList.Count);
    }
}
