// <copyright file="RawSyntaxTriviaTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Tests.Syntax;

using OffDotNet.CodeAnalysis.Pdf.Syntax;
using OffDotNet.CodeAnalysis.Syntax;
using Utils;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
public class RawSyntaxTriviaTests
{
    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should inherit from {nameof(RawSyntaxTrivia)}")]
    public void Class_ShouldInheritFromAbstractNode1()
    {
        // Arrange

        // Act
        var actual = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " "u8);

        // Assert
        Assert.IsAssignableFrom<AbstractNode>(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should inherit from {nameof(RawSyntaxNode)}")]
    public void Class_ShouldInheritFromAbstractNode2()
    {
        // Arrange

        // Act
        var actual = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " "u8);

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
        var rawNode = RawSyntaxTrivia.Create(rawKind, " "u8);

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
        var rawNode = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " "u8);

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
        var rawNode = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " "u8);

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
        var rawNode = RawSyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, textSpan);

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
        var rawNode = RawSyntaxTrivia.Create(kind, " "u8);

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
        var rawNode = RawSyntaxTrivia.Create(kind, " "u8);

        // Act
        var kindText = rawNode.KindText;

        // Assert
        Assert.Equal(expected, kindText);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.Language)} property should return PDF")]
    public void LanguageProperty_ShouldReturnPDF()
    {
        // Arrange

        // Act
        var language = RawSyntaxNode.Language;

        // Assert
        Assert.Equal("PDF", language);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.Text)} property should return the value passed to the constructor")]
    public void TextProperty_ShouldReturnTheValuePassedToTheConstructor()
    {
        // Arrange
        var text = " "u8;
        var rawNode = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, text);

        // Act
        var actual = rawNode.Text;

        // Assert
        Assert.Equal(" ", actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.GetSlot)} method should return None")]
    public void GetSlotMethod_ShouldReturnNone()
    {
        // Arrange
        var rawNode = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " "u8);

        // Act
        var actual = rawNode.GetSlot(0);

        // Assert
        Assert.Equal(Option<AbstractNode>.None, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.LeadingTriviaWidth)} property should return 0")]
    public void LeadingTriviaWidthProperty_ShouldReturn0()
    {
        // Arrange
        var rawNode = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " "u8);

        // Act
        var actual = rawNode.LeadingTriviaWidth;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.TrailingTriviaWidth)} property should return 0")]
    public void TrailingTriviaWidthProperty_ShouldReturn0()
    {
        // Arrange
        var rawNode = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " "u8);

        // Act
        var actual = rawNode.TrailingTriviaWidth;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.Width)} property should be the same as {nameof(RawSyntaxTrivia.FullWidth)}")]
    public void WidthProperty_ShouldBeTheSameAsFullWidth()
    {
        // Arrange
        const int ExpectedWidth = 1;
        var rawNode = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, " "u8);

        // Act
        var width = rawNode.Width;

        // Assert
        Assert.Equal(ExpectedWidth, width);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.ToString)} method should return the value of the {nameof(RawSyntaxTrivia.Text)} property")]
    public void ToStringMethod_ShouldReturnTheValueOfTheTextProperty()
    {
        // Arrange
        var text = " "u8;
        var rawNode = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, text);

        // Act
        var actual = rawNode.ToString();

        // Assert
        Assert.Equal(" ", actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxTrivia.ToFullString)} method should return the value of the {nameof(RawSyntaxTrivia.Text)} property")]
    public void ToFullStringMethod_ShouldReturnTheValueOfTheTextProperty()
    {
        // Arrange
        var text = " "u8;
        var rawNode = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, text);

        // Act
        var actual = rawNode.ToFullString();

        // Assert
        Assert.Equal(" ", actual);
    }
}
