// <copyright file="SyntaxTriviaTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax;

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Text;
using InternalSyntax = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

public class SyntaxTriviaTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Kind)} property must have {nameof(SyntaxKind.None)} value as default.")]
    public void KindProperty_NullTriviaGreenNode_MustBeSetToNone()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode? triviaNode = null;
        const int position = 0;
        const int index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        SyntaxKind actualKind = token.Kind;

        // Assert
        Assert.Equal(SyntaxKind.None, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Node)} property must be null as default.")]
    public void NodeProperty_MustBeNullByDefault()
    {
        // Arrange
        SyntaxToken syntaxToken = default; // TODO: change when implemented
        InternalSyntax.GreenNode? triviaNode = null;
        const int position = 0;
        const int index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        InternalSyntax.GreenNode? node = token.Node;

        // Assert
        Assert.Null(node);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Node)} property must be assigned from constructor.")]
    public void NodeProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
        const int position = 0;
        const int index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        InternalSyntax.GreenNode? node = token.Node;

        // Assert
        Assert.Equal(triviaNode, node);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Kind)} property must be assigned from {nameof(SyntaxTrivia.Node)} property.")]
    public void KindProperty_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        const SyntaxKind expectedSyntaxKind = SyntaxKind.EndOfLineTrivia;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(expectedSyntaxKind, "\n");
        const int position = 0;
        const int index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        SyntaxKind actualSyntaxKind = token.Kind;

        // Assert
        Assert.Equal(expectedSyntaxKind, actualSyntaxKind);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Position)} property must be assigned from constructor.")]
    public void PositionProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
        const int position = 150;
        const int index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        int actualPosition = token.Position;

        // Assert
        Assert.Equal(position, actualPosition);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Index)} property must be assigned from constructor.")]
    public void IndexProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
        const int position = 0;
        const int index = 3;

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        int actualIndex = token.Index;

        // Assert
        Assert.Equal(index, actualIndex);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Width)} property must be assigned from {nameof(SyntaxTrivia.Node)} property.")]
    public void WidthProperty_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int expectedWidth = 2;
        const int position = 0;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        int actualWidth = token.Width;

        // Assert
        Assert.Equal(expectedWidth, actualWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.FullWidth)} property must be assigned from {nameof(SyntaxTrivia.Node)} property.")]
    public void FullWidthProperty_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int expectedFullWidth = 2;
        const int position = 0;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        int actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(expectedFullWidth, actualFullWidth);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.SyntaxToken)} property must be assigned from constructor and passed by reference.")]
    public void SyntaxTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
        const int position = 0;
        const int index = 3;

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        SyntaxToken actualSyntaxToken = token.SyntaxToken;

        // Assert
        Assert.Equal(syntaxToken, actualSyntaxToken);
    }

    [Fact(DisplayName = $"The ToString() method must be assigned from {nameof(SyntaxTrivia.Node)} property.")]
    public void ToStringMethod_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position = 0;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        string actualStringValue = token.ToString();

        // Assert
        Assert.Equal(triviaText, actualStringValue);
    }

    [Fact(DisplayName = $"The SyntaxTrivia must implement the {nameof(IEquatable<SyntaxTrivia>)} interface.")]
    public void SyntaxTrivia_MustImplementIEquatableInterface()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position = 0;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        // Act
        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Assert
        Assert.IsAssignableFrom<IEquatable<SyntaxTrivia>>(token);
    }

    [Fact(DisplayName = $"The Equals() method must return true by comparing their {nameof(SyntaxTrivia.Node)}, {nameof(SyntaxTrivia.Position)} and {nameof(SyntaxTrivia.Index)} properties.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position = 0;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, position, index);
        SyntaxTrivia token2 = new(syntaxToken, triviaNode, position, index);

        // Act
        bool equals = token1.Equals(token2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_DifferentPositions_MustReturnFalse()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position1 = 0;
        const int position2 = 1;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, position1, index);
        SyntaxTrivia token2 = new(syntaxToken, triviaNode, position2, index);

        // Act
        bool equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Index)} properties.")]
    public void EqualsMethod_DifferentIndices_MustReturnFalse()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position = 0;
        const int index1 = 0;
        const int index2 = 1;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, position, index1);
        SyntaxTrivia token2 = new(syntaxToken, triviaNode, position, index2);

        // Act
        bool equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Node)} properties.")]
    public void EqualsMethod_DifferentNodes_MustReturnFalse()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position = 0;
        const int index = 0;
        SyntaxToken syntaxToken1 = new(null, null, 0, 0);
        SyntaxToken syntaxToken2 = new(null, null, 3, 0);
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token1 = new(syntaxToken1, triviaNode, position, index);
        SyntaxTrivia token2 = new(syntaxToken2, triviaNode, position, index);

        // Act
        bool equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method must return true by comparing their {nameof(SyntaxTrivia.Node)} properties.")]
    public void EqualsMethod_EqualNodes_MustReturnTrue()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position = 0;
        const int index = 0;
        SyntaxToken syntaxToken1 = new(null, null, 3, 0);
        SyntaxToken syntaxToken2 = new(null, null, 3, 0);
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token1 = new(syntaxToken1, triviaNode, position, index);
        SyntaxTrivia token2 = new(syntaxToken2, triviaNode, position, index);

        // Act
        bool equals = token1.Equals(token2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The Equals() method (object overload) must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_ObjectOverload_DifferentPosition_MustReturnFalse()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position1 = 0;
        const int position2 = 5;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, position1, index);
        object token2 = new SyntaxTrivia(syntaxToken, triviaNode, position2, index);

        // Act
        bool equals = token1.Equals(token2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The Equals() method (object overload) must return true by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_ObjectOverload_SamePosition_MustReturnTrue()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position1 = 5;
        const int position2 = 5;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, position1, index);
        object token2 = new SyntaxTrivia(syntaxToken, triviaNode, position2, index);

        // Act
        bool equals = token1.Equals(token2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = $"The '==' operator must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsOperator_DifferentPosition_MustReturnFalse()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position1 = 0;
        const int position2 = 5;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, position1, index);
        SyntaxTrivia token2 = new(syntaxToken, triviaNode, position2, index);

        // Act
        bool equals = token1 == token2;

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = $"The '!=' operator must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void NotEqualsOperator_SamePosition_MustReturnFalse()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position1 = 5;
        const int position2 = 5;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token1 = new(syntaxToken, triviaNode, position1, index);
        SyntaxTrivia token2 = new(syntaxToken, triviaNode, position2, index);

        // Act
        bool equals = token1 != token2;

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName = "The GetHashCode() method must be computed.")]
    public void GetHashCodeMethod_MustBeComputed()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position = 0;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);
        int hashCode = HashCode.Combine(syntaxToken, triviaNode, position, index);

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        int actualHashCode = token.GetHashCode();

        // Assert
        Assert.Equal(hashCode, actualHashCode);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.FullSpan)} must be a default struct if {nameof(SyntaxTrivia.Node)} is null.")]
    public void FullSpanProperty_NullNode_MustReturnDefaultTextSpan()
    {
        // Arrange
        const int position = 3;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode? triviaNode = null;

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        TextSpan actualTextSpan = token.FullSpan;

        // Assert
        Assert.Equal(default, actualTextSpan);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.FullSpan)} must be computed from {nameof(SyntaxTrivia.Node)}")]
    public void FullSpanProperty_MustBeComputedFromNode()
    {
        // Arrange
        const string triviaText = "\r\n";
        const int position = 3;
        const int index = 0;
        SyntaxToken syntaxToken = default;
        InternalSyntax.GreenNode triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, triviaText);

        SyntaxTrivia token = new(syntaxToken, triviaNode, position, index);

        // Act
        TextSpan actualTextSpan = token.FullSpan;

        // Assert
        Assert.Equal(3, actualTextSpan.Start);
        Assert.Equal(2, actualTextSpan.Length);
    }
}
