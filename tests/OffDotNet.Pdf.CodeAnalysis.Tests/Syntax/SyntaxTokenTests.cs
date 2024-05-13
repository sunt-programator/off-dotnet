// <copyright file="SyntaxTokenTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax;

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.SyntaxTrivia;

public class SyntaxTokenTests
{
    private const int Position = 5;
    private const int Index = 2;
    private const string Text = "123";
    private const int Value = 123;
    private const string LeadingTriviaText = " ";
    private const string TrailingTriviaText = "\r\n";
    private readonly SyntaxToken _token;
    private readonly GreenNode _underlyingNode;
    private readonly AbstractSyntaxNode _parent;

    public SyntaxTokenTests()
    {
        var leadingTrivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, LeadingTriviaText);
        var trailingTrivia = SyntaxFactory.Trivia(SyntaxKind.EndOfLineTrivia, TrailingTriviaText);
        _underlyingNode = SyntaxFactory.Token(SyntaxKind.NumericLiteralToken, Text, Value, leadingTrivia, trailingTrivia);

        _parent = Substitute.For<AbstractSyntaxNode>();
        _parent.UnderlyingNode.Returns(_underlyingNode);

        _token = new SyntaxToken(_parent, _underlyingNode, Position, Index);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.UnderlyingNode)} property must be set from the constructor.")]
    public void NodeProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualNode = _token.UnderlyingNode;

        // Assert
        Assert.Equal(_underlyingNode, actualNode);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Parent)} property must be set from the constructor.")]
    public void ParentProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualParent = _token.Parent;

        // Assert
        Assert.Equal(_parent, actualParent);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Position)} property must be set from the constructor.")]
    public void PositionProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualPosition = _token.Position;

        // Assert
        Assert.Equal(Position, actualPosition);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Index)} property must be set from the constructor.")]
    public void IndexProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualIndex = _token.Index;

        // Assert
        Assert.Equal(Index, actualIndex);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxToken.Width)} property must be computed from the {nameof(SyntaxToken.UnderlyingNode)} property.")]
    public void WidthProperty_MustBeComputedFromNodeProperty()
    {
        // Arrange

        // Act
        var actualWidth = _token.Width;

        // Assert
        Assert.Equal(Text.Length, actualWidth);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxToken.FullWidth)} property must be computed from the {nameof(SyntaxToken.UnderlyingNode)} property.")]
    public void FullWidthProperty_MustBeComputedFromNodeProperty()
    {
        // Arrange
        var expectedFullWidth = Text.Length + LeadingTriviaText.Length + TrailingTriviaText.Length;

        // Act
        var actualFullWidth = _token.FullWidth;

        // Assert
        Assert.Equal(expectedFullWidth, actualFullWidth);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxToken.FullSpan)} must be computed from {nameof(SyntaxToken.UnderlyingNode)} and {nameof(SyntaxToken.Position)} properties.")]
    public void FullSpanProperty_MustBeComputedFromNode()
    {
        // Arrange
        var expectedSpanLength = Text.Length + LeadingTriviaText.Length + TrailingTriviaText.Length;

        // Act
        var actualTextSpan = _token.FullSpan;

        // Assert
        Assert.Equal(Position, actualTextSpan.Start);
        Assert.Equal(expectedSpanLength, actualTextSpan.Length);
    }

    [Fact(DisplayName = "The ToString() method must return the underlying node string.")]
    public void ToStringMethod_MustReturnTheUnderlyingNodeString()
    {
        // Arrange

        // Act
        var actualString = _token.ToString();

        // Assert
        Assert.Equal(Text, actualString);
    }

    [Fact(DisplayName = $"The SyntaxTrivia must implement the {nameof(IEquatable<SyntaxToken>)} interface.")]
    public void SyntaxToken_MustImplementIEquatableInterface()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsAssignableFrom<IEquatable<SyntaxToken>>(_token);
    }

    [Fact(DisplayName =
        $"The Equals() method must return true by comparing their {nameof(SyntaxTrivia.UnderlyingNode)}, {nameof(SyntaxTrivia.Position)} and {nameof(SyntaxTrivia.Index)} properties.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        SyntaxToken token1 = new(_parent, _underlyingNode, Position, Index);
        SyntaxToken token2 = new(_parent, _underlyingNode, Position, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName =
        $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_DifferentPositions_MustReturnFalse()
    {
        // Arrange
        const int Position1 = 0;
        const int Position2 = 3;

        SyntaxToken token1 = new(_parent, _underlyingNode, Position1, Index);
        SyntaxToken token2 = new(_parent, _underlyingNode, Position2, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName =
        $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Index)} properties.")]
    public void EqualsMethod_DifferentIndices_MustReturnFalse()
    {
        // Arrange
        const int Index1 = 0;
        const int Index2 = 5;

        SyntaxToken token1 = new(_parent, _underlyingNode, Position, Index1);
        SyntaxToken token2 = new(_parent, _underlyingNode, Position, Index2);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName =
        $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.UnderlyingNode)} properties.")]
    public void EqualsMethod_DifferentNodes_MustReturnFalse()
    {
        // Arrange
        GreenNode underlyingNode1 = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        GreenNode underlyingNode2 = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        SyntaxToken token1 = new(_parent, underlyingNode1, Position, Index);
        SyntaxToken token2 = new(_parent, underlyingNode2, Position, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName =
        $"The Equals() method (object overload) must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_ObjectOverload_DifferentPosition_MustReturnFalse()
    {
        // Arrange
        const int Position1 = 0;
        const int Position2 = 3;

        SyntaxToken token1 = new(_parent, _underlyingNode, Position1, Index);
        object token2 = new SyntaxToken(_parent, _underlyingNode, Position2, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName =
        $"The Equals() method (object overload) must return true by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_ObjectOverload_SamePosition_MustReturnTrue()
    {
        // Arrange
        const int Position1 = 3;
        const int Position2 = 3;

        SyntaxToken token1 = new(_parent, _underlyingNode, Position1, Index);
        object token2 = new SyntaxToken(_parent, _underlyingNode, Position2, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName =
        $"The '==' operator must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsOperator_DifferentPosition_MustReturnFalse()
    {
        // Arrange
        const int Position1 = 0;
        const int Position2 = 3;

        SyntaxToken token1 = new(_parent, _underlyingNode, Position1, Index);
        SyntaxToken token2 = new(_parent, _underlyingNode, Position2, Index);

        // Act
        var equals = token1 == token2;

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName =
        $"The '!=' operator must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void NotEqualsOperator_SamePosition_MustReturnFalse()
    {
        // Arrange
        const int Position1 = 3;
        const int Position2 = 3;

        SyntaxToken token1 = new(_parent, _underlyingNode, Position1, Index);
        SyntaxToken token2 = new(_parent, _underlyingNode, Position2, Index);

        // Act
        var equals = token1 != token2;

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = "The GetHashCode() method must be computed.")]
    public void GetHashCodeMethod_MustBeComputed()
    {
        // Arrange
        var hashCode = HashCode.Combine(_parent, _underlyingNode, Position, Index);

        // Act
        var actualHashCode = _token.GetHashCode();

        // Assert
        Assert.Equal(hashCode, actualHashCode);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxToken.Kind)} property must be set from the {nameof(SyntaxToken.UnderlyingNode)} property.")]
    public void KindProperty_MustBeSetFromNodeProperty()
    {
        // Arrange
        const SyntaxKind ExpectedKind = SyntaxKind.NumericLiteralToken;

        // Act
        var actualKind = _token.Kind;

        // Assert
        Assert.Equal(ExpectedKind, actualKind);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxToken.Span)} must be computed from {nameof(SyntaxToken.UnderlyingNode)} and {nameof(SyntaxToken.Position)} properties.")]
    public void SpanProperty_MustBeComputedFromNode()
    {
        // Arrange
        var expectedSpanStart = Position + LeadingTriviaText.Length;
        var expectedSpanLength = Text.Length;

        // Act
        var actualTextSpan = _token.Span;

        // Assert
        Assert.Equal(expectedSpanStart, actualTextSpan.Start);
        Assert.Equal(expectedSpanLength, actualTextSpan.Length);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxToken.Value)} property must be computed from {nameof(SyntaxToken.UnderlyingNode)} property.")]
    public void ValueProperty_MustBeComputedFromNodeProperty()
    {
        // Arrange

        // Act
        var actualValue = _token.Value;

        // Assert
        Assert.Equal(Value, actualValue);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxToken.Text)} property must be computed from {nameof(SyntaxToken.UnderlyingNode)} property.")]
    public void TextProperty_MustBeComputedFromNodeProperty()
    {
        // Arrange

        // Act
        var actualText = _token.Text;

        // Assert
        Assert.Equal(Text, actualText);
    }

    [Fact(DisplayName =
        $"The ToFullString() method must be computed from the {nameof(SyntaxToken.UnderlyingNode)} property.")]
    public void ToFullStringMethod_MustBeComputedFromNodeProperty()
    {
        // Arrange
        const string ExpectedFullText = $"{LeadingTriviaText}{Text}{TrailingTriviaText}";

        // Act
        var actualText = _token.ToFullString();
        var actualFullSpan = _token.FullSpan;

        // Assert
        Assert.Equal(ExpectedFullText, actualText);
        Assert.Equal(ExpectedFullText.Length, actualFullSpan.Length);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.LeadingTriviaWidth)} property must be computed from the {nameof(SyntaxToken.UnderlyingNode)} property.")]
    public void LeadingWidthProperty_MustBeComputedFromNodeProperty()
    {
        // Arrange

        // Act
        var actualLeadingWidth = _token.LeadingTriviaWidth;

        // Assert
        Assert.Equal(LeadingTriviaText.Length, actualLeadingWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.TrailingTriviaWidth)} property must be computed from the {nameof(SyntaxToken.UnderlyingNode)} property.")]
    public void TrailingWidthProperty_MustBeComputedFromNodeProperty()
    {
        // Arrange

        // Act
        var actualTrailingWidth = _token.TrailingTriviaWidth;

        // Assert
        Assert.Equal(TrailingTriviaText.Length, actualTrailingWidth);
    }
}
