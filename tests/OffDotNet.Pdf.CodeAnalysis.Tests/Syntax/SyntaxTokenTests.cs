// <copyright file="SyntaxTokenTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax;

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.SyntaxToken;
using SyntaxTrivia = OffDotNet.Pdf.CodeAnalysis.Syntax.SyntaxTrivia;

public class SyntaxTokenTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.UnderlyingNode)} property must be null as default.")]
    public void NodeProperty_MustBeNullByDefault()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode? underlyingNode = null;
        const int Position = 0;
        const int Index = 0;

        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualNode = token.Node;

        // Assert
        Assert.Null(actualNode);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Node)} property must be set from the constructor.")]
    public void NodeProperty_MustBeSetFromConstructor()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 0;
        const int Index = 0;

        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualNode = token.Node;

        // Assert
        Assert.Equal(underlyingNode, actualNode);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Parent)} property must be set from the constructor.")]
    public void ParentProperty_MustBeSetFromConstructor()
    {
        // Arrange
        SyntaxNode parent = new(SyntaxFactory.Token(SyntaxKind.TrueKeyword), null, 0);
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 150;
        const int Index = 0;

        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualParent = token.Parent;

        // Assert
        Assert.Equal(parent, actualParent);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Kind)} property must have {nameof(SyntaxKind.None)} value as default.")]
    public void KindProperty_NullGreenNode_MustBeSetToNone()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode? underlyingToken = null;
        const int Position = 0;
        const int Index = 0;

        SyntaxToken token = new(parent, underlyingToken, Position, Index);

        // Act
        var actualKind = token.Kind;

        // Assert
        Assert.Equal(SyntaxKind.None, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Position)} property must be set from the constructor.")]
    public void PositionProperty_MustBeSetFromConstructor()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 150;
        const int Index = 0;

        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualPosition = token.Position;

        // Assert
        Assert.Equal(150, actualPosition);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Index)} property must be set from the constructor.")]
    public void IndexProperty_MustBeSetFromConstructor()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 0;
        const int Index = 3;

        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualIndex = token.Index;

        // Assert
        Assert.Equal(3, actualIndex);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.Width)} property must be computed from the {nameof(SyntaxToken.Node)} property.")]
    public void WidthProperty_MustBeComputedFromNodeProperty()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 0;
        const int Index = 0;

        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualWidth = token.Width;

        // Assert
        Assert.Equal(4, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.FullWidth)} property must be computed from the {nameof(SyntaxToken.Node)} property.")]
    public void FullWidthProperty_MustBeComputedFromNodeProperty()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 0;
        const int Index = 0;

        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(4, actualFullWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxToken.FullSpan)} must be computed from {nameof(SyntaxToken.Node)} and {nameof(SyntaxToken.Position)} properties.")]
    public void FullSpanProperty_MustBeComputedFromNode()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 3;
        const int Index = 0;

        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualTextSpan = token.FullSpan;

        // Assert
        Assert.Equal(3, actualTextSpan.Start);
        Assert.Equal(4, actualTextSpan.Length);
    }

    [Fact(DisplayName = "The ToString() method must return string empty if the underlying node is null.")]
    public void ToStringMethod_NullUnderlyingNode_MustReturnStringEmpty()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode? underlyingNode = null;
        const int Position = 3;
        const int Index = 0;

        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualString = token.ToString();

        // Assert
        Assert.Equal(string.Empty, actualString);
    }

    [Fact(DisplayName = "The ToString() method must return the underlying node string.")]
    public void ToStringMethod_MustReturnTheUnderlyingNodeString()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 3;
        const int Index = 0;

        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualString = token.ToString();

        // Assert
        Assert.Equal("true", actualString);
    }

    [Fact(DisplayName = $"The SyntaxTrivia must implement the {nameof(IEquatable<SyntaxToken>)} interface.")]
    public void SyntaxToken_MustImplementIEquatableInterface()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 3;
        const int Index = 0;

        // Act
        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Assert
        Assert.IsAssignableFrom<IEquatable<SyntaxToken>>(token);
    }

    [Fact(DisplayName = $"The Equals() method must return true by comparing their {nameof(SyntaxTrivia.UnderlyingNode)}, {nameof(SyntaxTrivia.Position)} and {nameof(SyntaxTrivia.Index)} properties.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 3;
        const int Index = 0;

        SyntaxToken token1 = new(parent, underlyingNode, Position, Index);
        SyntaxToken token2 = new(parent, underlyingNode, Position, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_DifferentPositions_MustReturnFalse()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position1 = 0;
        const int Position2 = 3;
        const int Index = 0;

        SyntaxToken token1 = new(parent, underlyingNode, Position1, Index);
        SyntaxToken token2 = new(parent, underlyingNode, Position2, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Index)} properties.")]
    public void EqualsMethod_DifferentIndices_MustReturnFalse()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 3;
        const int Index1 = 0;
        const int Index2 = 5;

        SyntaxToken token1 = new(parent, underlyingNode, Position, Index1);
        SyntaxToken token2 = new(parent, underlyingNode, Position, Index2);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.UnderlyingNode)} properties.")]
    public void EqualsMethod_DifferentNodes_MustReturnFalse()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode1 = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        GreenNode underlyingNode2 = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        const int Position = 3;
        const int Index = 0;

        SyntaxToken token1 = new(parent, underlyingNode1, Position, Index);
        SyntaxToken token2 = new(parent, underlyingNode2, Position, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method (object overload) must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_ObjectOverload_DifferentPosition_MustReturnFalse()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position1 = 0;
        const int Position2 = 3;
        const int Index = 0;

        SyntaxToken token1 = new(parent, underlyingNode, Position1, Index);
        object token2 = new SyntaxToken(parent, underlyingNode, Position2, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method (object overload) must return true by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_ObjectOverload_SamePosition_MustReturnTrue()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position1 = 3;
        const int Position2 = 3;
        const int Index = 0;

        SyntaxToken token1 = new(parent, underlyingNode, Position1, Index);
        object token2 = new SyntaxToken(parent, underlyingNode, Position2, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The '==' operator must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsOperator_DifferentPosition_MustReturnFalse()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position1 = 0;
        const int Position2 = 3;
        const int Index = 0;

        SyntaxToken token1 = new(parent, underlyingNode, Position1, Index);
        SyntaxToken token2 = new(parent, underlyingNode, Position2, Index);

        // Act
        var equals = token1 == token2;

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The '!=' operator must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void NotEqualsOperator_SamePosition_MustReturnFalse()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position1 = 3;
        const int Position2 = 3;
        const int Index = 0;

        SyntaxToken token1 = new(parent, underlyingNode, Position1, Index);
        SyntaxToken token2 = new(parent, underlyingNode, Position2, Index);

        // Act
        var equals = token1 != token2;

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = "The GetHashCode() method must be computed.")]
    public void GetHashCodeMethod_MustBeComputed()
    {
        // Arrange
        SyntaxNode? parent = null;
        GreenNode underlyingNode = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        const int Position = 3;
        const int Index = 0;

        var hashCode = HashCode.Combine(parent, underlyingNode, Position, Index);
        SyntaxToken token = new(parent, underlyingNode, Position, Index);

        // Act
        var actualHashCode = token.GetHashCode();

        // Assert
        Assert.Equal(hashCode, actualHashCode);
    }
}
