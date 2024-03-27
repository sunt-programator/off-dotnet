// <copyright file="SyntaxTriviaTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax;

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using InternalSyntax = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

public class SyntaxTriviaTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Kind)} property must have {nameof(SyntaxKind.None)} value as default.")]
    public void KindProperty_NullTriviaGreenNode_MustBeSetToNone()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode? triviaNode = null;
        const int Position = 0;
        const int Index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualKind = token.Kind;

        // Assert
        Assert.Equal(SyntaxKind.None, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.UnderlyingNode)} property must be null as default.")]
    public void NodeProperty_MustBeNullByDefault()
    {
        // Arrange
        SyntaxToken syntaxToken = default; // TODO: change when implemented
        InternalSyntax.GreenNode? triviaNode = null;
        const int Position = 0;
        const int Index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var node = token.UnderlyingNode;

        // Assert
        Assert.Null(node);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.UnderlyingNode)} property must be assigned from constructor.")]
    public void NodeProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
        const int Position = 0;
        const int Index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var node = token.UnderlyingNode;

        // Assert
        Assert.Equal(triviaNode, node);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Kind)} property must be assigned from {nameof(SyntaxTrivia.UnderlyingNode)} property.")]
    public void KindProperty_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        const SyntaxKind ExpectedSyntaxKind = SyntaxKind.EndOfLineTrivia;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(ExpectedSyntaxKind, "\n");
        const int Position = 0;
        const int Index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualSyntaxKind = token.Kind;

        // Assert
        Assert.Equal(ExpectedSyntaxKind, actualSyntaxKind);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Position)} property must be assigned from constructor.")]
    public void PositionProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
        const int Position = 150;
        const int Index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualPosition = token.Position;

        // Assert
        Assert.Equal(Position, actualPosition);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Index)} property must be assigned from constructor.")]
    public void IndexProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
        const int Position = 0;
        const int Index = 3;

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualIndex = token.Index;

        // Assert
        Assert.Equal(Index, actualIndex);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Width)} property must be assigned from {nameof(SyntaxTrivia.UnderlyingNode)} property.")]
    public void WidthProperty_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int ExpectedWidth = 2;
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualWidth = token.Width;

        // Assert
        Assert.Equal(ExpectedWidth, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.FullWidth)} property must be assigned from {nameof(SyntaxTrivia.UnderlyingNode)} property.")]
    public void FullWidthProperty_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int ExpectedFullWidth = 2;
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(ExpectedFullWidth, actualFullWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.SyntaxToken)} property must be assigned from constructor and passed by reference.")]
    public void SyntaxTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
        const int Position = 0;
        const int Index = 3;

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualSyntaxToken = token.SyntaxToken;

        // Assert
        Assert.Equal(syntaxToken, actualSyntaxToken);
    }

    [Fact(DisplayName = $"The ToString() method must be assigned from {nameof(SyntaxTrivia.UnderlyingNode)} property.")]
    public void ToStringMethod_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualStringValue = token.ToString();

        // Assert
        Assert.Equal(TriviaText, actualStringValue);
    }

    [Fact(DisplayName = $"The SyntaxTrivia must implement the {nameof(IEquatable<SyntaxTrivia>)} interface.")]
    public void SyntaxTrivia_MustImplementIEquatableInterface()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        // Act
        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Assert
        Assert.IsAssignableFrom<IEquatable<SyntaxTrivia>>(token);
    }

    [Fact(DisplayName = $"The Equals() method must return true by comparing their {nameof(SyntaxTrivia.UnderlyingNode)}, {nameof(SyntaxTrivia.Position)} and {nameof(SyntaxTrivia.Index)} properties.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, Position, Index);
        SyntaxTrivia token2 = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_DifferentPositions_MustReturnFalse()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position1 = 0;
        const int Position2 = 1;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, Position1, Index);
        SyntaxTrivia token2 = new(syntaxToken, triviaNode, Position2, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Index)} properties.")]
    public void EqualsMethod_DifferentIndices_MustReturnFalse()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index1 = 0;
        const int Index2 = 1;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, Position, Index1);
        SyntaxTrivia token2 = new(syntaxToken, triviaNode, Position, Index2);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.UnderlyingNode)} properties.")]
    public void EqualsMethod_DifferentNodes_MustReturnFalse()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken1 = new(null, null, 0, 0);
        SyntaxToken syntaxToken2 = new(null, null, 3, 0);
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token1 = new(syntaxToken1, triviaNode, Position, Index);
        SyntaxTrivia token2 = new(syntaxToken2, triviaNode, Position, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return true by comparing their {nameof(SyntaxTrivia.UnderlyingNode)} properties.")]
    public void EqualsMethod_EqualNodes_MustReturnTrue()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken1 = new(null, null, 3, 0);
        SyntaxToken syntaxToken2 = new(null, null, 3, 0);
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token1 = new(syntaxToken1, triviaNode, Position, Index);
        SyntaxTrivia token2 = new(syntaxToken2, triviaNode, Position, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The Equals() method (object overload) must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_ObjectOverload_DifferentPosition_MustReturnFalse()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position1 = 0;
        const int Position2 = 5;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, Position1, Index);
        object token2 = new SyntaxTrivia(syntaxToken, triviaNode, Position2, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method (object overload) must return true by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_ObjectOverload_SamePosition_MustReturnTrue()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position1 = 5;
        const int Position2 = 5;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, Position1, Index);
        object token2 = new SyntaxTrivia(syntaxToken, triviaNode, Position2, Index);

        // Act
        var equals = token1.Equals(token2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The '==' operator must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsOperator_DifferentPosition_MustReturnFalse()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position1 = 0;
        const int Position2 = 5;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, Position1, Index);
        SyntaxTrivia token2 = new(syntaxToken, triviaNode, Position2, Index);

        // Act
        var equals = token1 == token2;

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The '!=' operator must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void NotEqualsOperator_SamePosition_MustReturnFalse()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position1 = 5;
        const int Position2 = 5;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, Position1, Index);
        SyntaxTrivia token2 = new(syntaxToken, triviaNode, Position2, Index);

        // Act
        var equals = token1 != token2;

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = "The GetHashCode() method must be computed.")]
    public void GetHashCodeMethod_MustBeComputed()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);
        var hashCode = HashCode.Combine(syntaxToken, triviaNode, Position, Index);

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualHashCode = token.GetHashCode();

        // Assert
        Assert.Equal(hashCode, actualHashCode);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.FullSpan)} must be a default struct if {nameof(SyntaxTrivia.UnderlyingNode)} is null.")]
    public void FullSpanProperty_NullNode_MustReturnDefaultTextSpan()
    {
        // Arrange
        const int Position = 3;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode? triviaNode = null;

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualTextSpan = token.FullSpan;

        // Assert
        Assert.Equal(default, actualTextSpan);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.FullSpan)} must be computed from {nameof(SyntaxTrivia.UnderlyingNode)}")]
    public void FullSpanProperty_MustBeComputedFromNode()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 3;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualTextSpan = token.FullSpan;

        // Assert
        Assert.Equal(3, actualTextSpan.Start);
        Assert.Equal(2, actualTextSpan.Length);
    }
}
