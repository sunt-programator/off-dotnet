// <copyright file="RawSyntaxTriviaTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Tests.Syntax;

using OffDotNet.CodeAnalysis.Pdf.Syntax;
using OffDotNet.CodeAnalysis.Syntax;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
public class RawSyntaxTriviaTests
{
    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should inherit from {nameof(RawSyntaxTrivia)}")]
    public void Class_ShouldInheritFromAbstractNode1()
    {
        // Arrange

        // Act
        var actual = new RawSyntaxTrivia(SyntaxKind.WhitespaceTrivia, " "u8);

        // Assert
        Assert.IsAssignableFrom<AbstractNode>(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should inherit from {nameof(RawSyntaxNode)}")]
    public void Class_ShouldInheritFromAbstractNode2()
    {
        // Arrange

        // Act
        var actual = new RawSyntaxTrivia(SyntaxKind.WhitespaceTrivia, " "u8);

        // Assert
        Assert.IsAssignableFrom<RawSyntaxNode>(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxTrivia.RawKind)} property should the value passed to the constructor")]
    [InlineData(SyntaxKind.EndOfLineTrivia)]
    [InlineData(SyntaxKind.WhitespaceTrivia)]
    [InlineData(SyntaxKind.SingleLineCommentTrivia)]
    public void RawKindProperty_ShouldReturnTheValuePassedToTheConstructor(SyntaxKind rawKind)
    {
        // Arrange
        var rawNode = new RawSyntaxTrivia(rawKind, " "u8);

        // Act
        var kind = rawNode.RawKind;

        // Assert
        Assert.Equal((int)rawKind, kind);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.IsToken)} property should return false by default")]
    public void IsTokenProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        var rawNode = new RawSyntaxTrivia(SyntaxKind.WhitespaceTrivia, " "u8);

        // Act
        var isToken = rawNode.IsToken;

        // Assert
        Assert.False(isToken);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.IsTrivia)} property should return true by default")]
    public void IsTriviaProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        var rawNode = new RawSyntaxTrivia(SyntaxKind.WhitespaceTrivia, " "u8);

        // Act
        var isTrivia = rawNode.IsTrivia;

        // Assert
        Assert.True(isTrivia);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxTrivia.FullWidth)} property should return the value passed to the constructor")]
    [InlineData("\r")]
    [InlineData("\n")]
    [InlineData("\r\n")]
    public void FullWidthProperty_ShouldReturnTheValuePassedToTheConstructor(string text)
    {
        // Arrange
        ReadOnlySpan<byte> textSpan = Encoding.ASCII.GetBytes(text);
        var rawNode = new RawSyntaxTrivia(SyntaxKind.EndOfLineTrivia, textSpan);

        // Act
        var width = rawNode.FullWidth;

        // Assert
        Assert.Equal(text.Length, width);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxTrivia.Kind)} property should return the value passed to the constructor")]
    [InlineData(SyntaxKind.EndOfLineTrivia)]
    [InlineData(SyntaxKind.WhitespaceTrivia)]
    [InlineData(SyntaxKind.SingleLineCommentTrivia)]
    public void KindProperty_ShouldReturnTheValuePassedToTheConstructor(SyntaxKind kind)
    {
        // Arrange
        var rawNode = new RawSyntaxTrivia(kind, " "u8);

        // Act
        var actual = rawNode.Kind;

        // Assert
        Assert.Equal(kind, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxTrivia.KindText)} property should return the name of the syntax kind")]
    [InlineData(SyntaxKind.EndOfLineTrivia, "EndOfLineTrivia")]
    [InlineData(SyntaxKind.WhitespaceTrivia, "WhitespaceTrivia")]
    [InlineData(SyntaxKind.SingleLineCommentTrivia, "SingleLineCommentTrivia")]
    public void KindTextProperty_ShouldReturnTheNameOfTheSyntaxKind(SyntaxKind kind, string expected)
    {
        // Arrange
        var rawNode = new RawSyntaxTrivia(kind, " "u8);

        // Act
        var kindText = rawNode.KindText;

        // Assert
        Assert.Equal(expected, kindText);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.Language)} property should return PDF")]
    public void LanguageProperty_ShouldReturnPDF()
    {
        // Arrange
        var rawNode = new RawSyntaxTrivia(SyntaxKind.WhitespaceTrivia, " "u8);

        // Act
        var language = rawNode.Language;

        // Assert
        Assert.Equal("PDF", language);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.Text)} property should return the value passed to the constructor")]
    public void TextProperty_ShouldReturnTheValuePassedToTheConstructor()
    {
        // Arrange
        var text = " "u8;
        var rawNode = new RawSyntaxTrivia(SyntaxKind.WhitespaceTrivia, text);

        // Act
        var actual = rawNode.Text;

        // Assert
        Assert.Equal(" ", actual);
    }
}
