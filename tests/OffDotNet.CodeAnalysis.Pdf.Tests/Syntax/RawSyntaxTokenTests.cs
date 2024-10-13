// <copyright file="RawSyntaxTokenTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Tests.Syntax;

using OffDotNet.CodeAnalysis.Pdf.Syntax;
using OffDotNet.CodeAnalysis.Syntax;
using Utils;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
public class RawSyntaxTokenTests
{
    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should inherit from {nameof(RawSyntaxTrivia)}")]
    public void Class_ShouldInheritFromAbstractNode1()
    {
        // Arrange

        // Act
        var actual = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Assert
        Assert.IsAssignableFrom<AbstractNode>(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should inherit from {nameof(RawSyntaxNode)}")]
    public void Class_ShouldInheritFromAbstractNode2()
    {
        // Arrange

        // Act
        var actual = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Assert
        Assert.IsAssignableFrom<RawSyntaxNode>(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.IsToken)} property should return true by default")]
    public void IsTokenProperty_ShouldReturnTrueByDefault()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Act
        var isToken = rawNode.IsToken;

        // Assert
        Assert.True(isToken);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.IsTrivia)} property should return false by default")]
    public void IsTriviaProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Act
        var isTrivia = rawNode.IsTrivia;

        // Assert
        Assert.False(isTrivia);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.IsList)} property should return false by default")]
    public void IsListProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Act
        var isList = rawNode.IsList;

        // Assert
        Assert.False(isList);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.GetSlot)} method should return None")]
    public void GetSlotMethod_ShouldReturnNone()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Act
        var actual = rawNode.GetSlot(0);

        // Assert
        Assert.Equal(Option<AbstractNode>.None, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxToken.Text)} property should return the default value")]
    [InlineData(SyntaxKind.BadToken, "")]
    [InlineData(SyntaxKind.MinusToken, "-")]
    [InlineData(SyntaxKind.TrueKeyword, "true")]
    [InlineData(SyntaxKind.FalseKeyword, "false")]
    [InlineData(SyntaxKind.NullKeyword, "null")]
    public void TextProperty_ShouldReturnTheDefaultValue(SyntaxKind kind, string expected)
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(kind);

        // Act
        var actual = rawNode.Text;

        // Assert
        Assert.Equal(expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.Value)} property should return the {nameof(Option<object>.None)} by default")]
    public void ValueProperty_ShouldReturnTheNone()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.NullKeyword);

        // Act
        var actual = rawNode.Value;

        // Assert
        Assert.Equal(Option<object>.None, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxToken.Value)} property should return the default value")]
    [InlineData(SyntaxKind.MinusToken, "-")]
    [InlineData(SyntaxKind.TrueKeyword, true)]
    [InlineData(SyntaxKind.FalseKeyword, false)]
    public void ValueProperty_ShouldReturnTheDefaultValue(SyntaxKind kind, object expected)
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(kind);

        // Act
        var actual = rawNode.Value;
        var isSome = actual.TryGetValue(out var value);

        // Assert
        Assert.True(isSome);
        Assert.Equal(expected, value);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.Value)} property should return the text from the constructor")]
    public void ValueProperty_ShouldReturnTheTextFromTheConstructor()
    {
        // Arrange
        const string Expected = "abc";
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken, Expected);

        // Act
        var actual = rawNode.Value;
        var isSome = actual.TryGetValue(out var value);

        // Assert
        Assert.True(isSome);
        Assert.Equal(rawNode.Text, value);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.ValueText)} property should return the empty string by default")]
    public void ValueTextProperty_ShouldReturnTheEmptyStringByDefault()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Act
        var actual = rawNode.ValueText;

        // Assert
        Assert.Equal(string.Empty, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.ValueText)} property should return the text from the constructor")]
    public void ValueTextProperty_ShouldReturnTheTextFromTheConstructor()
    {
        // Arrange
        const string Expected = "abc";
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken, Expected);

        // Act
        var actual = rawNode.ValueText;

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxToken.ValueText)} property should return the default value")]
    [InlineData(SyntaxKind.BadToken, "")]
    [InlineData(SyntaxKind.MinusToken, "-")]
    [InlineData(SyntaxKind.TrueKeyword, "true")]
    [InlineData(SyntaxKind.FalseKeyword, "false")]
    [InlineData(SyntaxKind.NullKeyword, "null")]
    public void ValueTextProperty_ShouldReturnTheDefaultValue(SyntaxKind kind, string expected)
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(kind);

        // Act
        var actual = rawNode.ValueText;

        // Assert
        Assert.Equal(expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.Width)} property should return the length of the text")]
    public void WidthProperty_ShouldReturnTheLengthOfTheText()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword);

        // Act
        var actual = rawNode.Width;

        // Assert
        Assert.Equal(4, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.LeadingTrivia)} property should return the default value")]
    public void LeadingTriviaProperty_ShouldReturnTheDefaultValue()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Act
        var actual = rawNode.LeadingTrivia;

        // Assert
        Assert.Equal(Option<AbstractNode>.None, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.LeadingTrivia)} property should return the value from the constructor")]
    public void LeadingTriviaProperty_ShouldReturnValueFromTheConstructor()
    {
        // Arrange
        var leadingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken, leadingTrivia: leadingTrivia, trailingTrivia: Option<AbstractNode>.None);

        // Act
        var actual = rawNode.LeadingTrivia;
        var isSome = actual.TryGetValue(out var value);

        // Assert
        Assert.True(isSome);
        Assert.Same(leadingTrivia, value);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.TrailingTrivia)} property should return the default value")]
    public void TrailingTriviaProperty_ShouldReturnTheDefaultValue()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Act
        var actual = rawNode.TrailingTrivia;

        // Assert
        Assert.Equal(Option<AbstractNode>.None, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.TrailingTrivia)} property should return the value from the constructor")]
    public void TrailingTriviaProperty_ShouldReturnValueFromTheConstructor()
    {
        // Arrange
        var trailingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken, leadingTrivia: Option<AbstractNode>.None, trailingTrivia: trailingTrivia);

        // Act
        var actual = rawNode.TrailingTrivia;
        var isSome = actual.TryGetValue(out var value);

        // Assert
        Assert.True(isSome);
        Assert.Same(trailingTrivia, value);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.LeadingTriviaWidth)} property should return 0 by default")]
    public void LeadingTriviaWidthProperty_ShouldReturnZeroByDefault()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Act
        var actual = rawNode.LeadingTriviaWidth;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.LeadingTriviaWidth)} property should return the length of the leading trivia")]
    public void LeadingTriviaWidthProperty_ShouldReturnTheLengthOfTheLeadingTrivia()
    {
        // Arrange
        var leadingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken, leadingTrivia: leadingTrivia, trailingTrivia: Option<AbstractNode>.None);

        // Act
        var actual = rawNode.LeadingTriviaWidth;

        // Assert
        Assert.Equal(2, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.TrailingTriviaWidth)} property should return 0 by default")]
    public void TrailingTriviaWidthProperty_ShouldReturnZeroByDefault()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken);

        // Act
        var actual = rawNode.TrailingTriviaWidth;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.TrailingTriviaWidth)} property should return the length of the trailing trivia")]
    public void TrailingTriviaWidthProperty_ShouldReturnTheLengthOfTheTrailingTrivia()
    {
        // Arrange
        var trailingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.BadToken, leadingTrivia: Option<AbstractNode>.None, trailingTrivia: trailingTrivia);

        // Act
        var actual = rawNode.TrailingTriviaWidth;

        // Assert
        Assert.Equal(2, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.ToString)} method should return the text of the token excluding the trivia")]
    public void ToStringMethod_ShouldReturnTheTextOfTheTokenExcludingTheTrivia()
    {
        // Arrange
        var leadingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var trailingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword, leadingTrivia: leadingTrivia, trailingTrivia: trailingTrivia);

        // Act
        var actual = rawNode.ToString();

        // Assert
        Assert.Equal("true", actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.ToFullString)} method should return the text of the token including the leading trivia")]
    public void ToFullStringMethod_ShouldReturnTheTextOfTheTokenIncludingTheLeadingTrivia()
    {
        // Arrange
        var leadingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword, leadingTrivia: leadingTrivia, trailingTrivia: Option<AbstractNode>.None);

        // Act
        var actual = rawNode.ToFullString();

        // Assert
        Assert.Equal("  true", actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.ToFullString)} method should return the text of the token including the trailing trivia")]
    public void ToFullStringMethod_ShouldReturnTheTextOfTheTokenIncludingTheTrailingTrivia()
    {
        // Arrange
        var trailingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        const string Text = "true";
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword, text: Text, leadingTrivia: Option<AbstractNode>.None, trailingTrivia: trailingTrivia);

        // Act
        var actual = rawNode.ToFullString();

        // Assert
        Assert.Equal("true  ", actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.Kind)} property should return the value from the constructor")]
    public void KindProperty_ShouldReturnValueFromTheConstructor()
    {
        // Arrange
        const SyntaxKind Expected = SyntaxKind.BadToken;
        var rawNode = RawSyntaxToken.Create(Expected);

        // Act
        var actual = rawNode.Kind;

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxToken.RawKind)} property should return the value from the constructor")]
    public void RawKindProperty_ShouldReturnValueFromTheConstructor()
    {
        // Arrange
        const SyntaxKind Expected = SyntaxKind.BadToken;
        var rawNode = RawSyntaxToken.Create(Expected);

        // Act
        var actual = rawNode.RawKind;

        // Assert
        Assert.Equal((ushort)Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.HasLeadingTrivia)} property should return false by default")]
    public void HasLeadingTriviaProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword);

        // Act
        var actual = rawNode.HasLeadingTrivia;

        // Assert
        Assert.False(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.HasLeadingTrivia)} property should return true if the leading trivia is not None")]
    public void HasLeadingTriviaProperty_ShouldReturnTrueIfTheLeadingTriviaIsNotNone()
    {
        // Arrange
        var leadingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword, leadingTrivia: leadingTrivia, trailingTrivia: Option<AbstractNode>.None);

        // Act
        var actual = rawNode.HasLeadingTrivia;

        // Assert
        Assert.True(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.HasTrailingTrivia)} property should return false by default")]
    public void HasTrailingTriviaProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword);

        // Act
        var actual = rawNode.HasTrailingTrivia;

        // Assert
        Assert.False(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.HasTrailingTrivia)} property should return true if the trailing trivia is not None")]
    public void HasTrailingTriviaProperty_ShouldReturnTrueIfTheTrailingTriviaIsNotNone()
    {
        // Arrange
        var trailingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword, leadingTrivia: Option<AbstractNode>.None, trailingTrivia: trailingTrivia);

        // Act
        var actual = rawNode.HasTrailingTrivia;

        // Assert
        Assert.True(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.FullWidth)} property should return the sum of the width of the token and the trivia")]
    public void FullWidthProperty_ShouldReturnTheSumOfTheWidthOfTheTokenAndTheTrivia()
    {
        // Arrange
        var leadingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var trailingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword, leadingTrivia: leadingTrivia, trailingTrivia: trailingTrivia);

        // Act
        var actual = rawNode.FullWidth;

        // Assert
        Assert.Equal(8, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.Width)} property should return the length of the token")]
    public void WidthProperty_ShouldReturnTheLengthOfTheToken()
    {
        // Arrange
        var leadingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var trailingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword, leadingTrivia: leadingTrivia, trailingTrivia: trailingTrivia);

        // Act
        var actual = rawNode.Width;

        // Assert
        Assert.Equal(4, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.Value)} property should return the text of the token")]
    public void ValueProperty_ShouldReturnTheTextOfTheToken()
    {
        // Arrange
        var leadingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var trailingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword, leadingTrivia: leadingTrivia, trailingTrivia: trailingTrivia);

        // Act
        var actual = rawNode.Value;
        var isSome = actual.TryGetValue(out var value);

        // Assert
        Assert.True(isSome);
        Assert.True(Assert.IsType<bool>(value));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.ValueText)} property should return the text of the token")]
    public void ValueTextProperty_ShouldReturnTheTextOfTheToken()
    {
        // Arrange
        const string Expected = "true";
        var leadingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var trailingTrivia = RawSyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, "  "u8);
        var rawNode = RawSyntaxToken.Create(SyntaxKind.TrueKeyword, leadingTrivia: leadingTrivia, trailingTrivia: trailingTrivia);

        // Act
        var actual = rawNode.ValueText;

        // Assert
        Assert.Equal(Expected, actual);
    }
}
