// <copyright file="SyntaxTriviaTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using InternalSyntax = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

public class SyntaxTriviaTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.UnderlyingNode)} property must be assigned from constructor.")]
    public void NodeProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
        const int Position = 0;
        const int Index = 0;

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var node = token.UnderlyingNode;

        // Assert
        Assert.Equal(triviaNode, node);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxTrivia.Kind)} property must be assigned from {nameof(SyntaxTrivia.UnderlyingNode)} property.")]
    public void KindProperty_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        const SyntaxKind ExpectedSyntaxKind = SyntaxKind.EndOfLineTrivia;
        var trivia = SyntaxFactory.SyntaxTrivia(ExpectedSyntaxKind, "\n");

        // Act
        var actualSyntaxKind = trivia.Kind;

        // Assert
        Assert.Equal(ExpectedSyntaxKind, actualSyntaxKind);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Position)} property must be assigned from constructor.")]
    public void PositionProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        SyntaxToken syntaxToken = default;
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
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
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
        const int Position = 0;
        const int Index = 3;

        SyntaxTrivia token = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualIndex = token.Index;

        // Assert
        Assert.Equal(Index, actualIndex);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxTrivia.Width)} property must be assigned from {nameof(SyntaxTrivia.UnderlyingNode)} property.")]
    public void WidthProperty_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int ExpectedWidth = 2;
        var trivia = SyntaxFactory.SyntaxTrivia(SyntaxKind.EndOfLineTrivia, TriviaText);

        // Act
        var actualWidth = trivia.Width;

        // Assert
        Assert.Equal(ExpectedWidth, actualWidth);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxTrivia.FullWidth)} property must be assigned from {nameof(SyntaxTrivia.UnderlyingNode)} property.")]
    public void FullWidthProperty_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int ExpectedFullWidth = 2;
        var trivia = SyntaxFactory.SyntaxTrivia(SyntaxKind.EndOfLineTrivia, TriviaText);

        // Act
        var actualFullWidth = trivia.FullWidth;

        // Assert
        Assert.Equal(ExpectedFullWidth, actualFullWidth);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxTrivia.SyntaxToken)} property must be assigned from constructor and passed by reference.")]
    public void SyntaxTokenProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        var syntaxToken = new SyntaxToken(null, null, 0, 0);
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
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
        var trivia = SyntaxFactory.SyntaxTrivia(SyntaxKind.EndOfLineTrivia, TriviaText);

        // Act
        var actualStringValue = trivia.ToString();

        // Assert
        Assert.Equal(TriviaText, actualStringValue);
    }

    [Fact(DisplayName =
        $"The ToFullString() method must be assigned from {nameof(SyntaxTrivia.UnderlyingNode)} property.")]
    public void ToFullStringMethod_MustBeAssignedFromNodeProperty()
    {
        // Arrange
        const string TriviaText = "\r\n";
        var trivia = SyntaxFactory.SyntaxTrivia(SyntaxKind.EndOfLineTrivia, TriviaText);

        // Act
        var actualStringValue = trivia.ToFullString();

        // Assert
        Assert.Equal(TriviaText, actualStringValue);
    }

    [Fact(DisplayName = $"The SyntaxTrivia must implement the {nameof(IEquatable<SyntaxTrivia>)} interface.")]
    public void SyntaxTrivia_MustImplementIEquatableInterface()
    {
        // Arrange
        const string TriviaText = "\r\n";

        // Act
        var trivia = SyntaxFactory.SyntaxTrivia(SyntaxKind.EndOfLineTrivia, TriviaText);

        // Assert
        Assert.IsAssignableFrom<IEquatable<SyntaxTrivia>>(trivia);
    }

    [Fact(DisplayName =
        $"The Equals() method must return true by comparing their {nameof(SyntaxTrivia.UnderlyingNode)}, {nameof(SyntaxTrivia.Position)} and {nameof(SyntaxTrivia.Index)} properties.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia trivia1 = new(syntaxToken, triviaNode, Position, Index);
        SyntaxTrivia trivia2 = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var equals1 = trivia1.Equals(trivia2);
        var equals2 = trivia1 == trivia2;
        var equals3 = trivia1 != trivia2;

        // Assert
        Assert.True(equals1);
        Assert.True(equals2);
        Assert.False(equals3);
    }

    [Fact(DisplayName =
        $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_DifferentPositions_MustReturnFalse()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position1 = 0;
        const int Position2 = 1;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia trivia1 = new(syntaxToken, triviaNode, Position1, Index);
        SyntaxTrivia trivia2 = new(syntaxToken, triviaNode, Position2, Index);

        // Act
        var equals1 = trivia1.Equals(trivia2);
        var equals2 = trivia1 == trivia2;
        var equals3 = trivia1 != trivia2;

        // Assert
        Assert.False(equals1);
        Assert.False(equals2);
        Assert.True(equals3);
    }

    [Fact(DisplayName =
        $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.Index)} properties.")]
    public void EqualsMethod_DifferentIndices_MustReturnFalse()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index1 = 0;
        const int Index2 = 1;
        SyntaxToken syntaxToken = default;
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia trivia1 = new(syntaxToken, triviaNode, Position, Index1);
        SyntaxTrivia trivia2 = new(syntaxToken, triviaNode, Position, Index2);

        // Act
        var equals1 = trivia1.Equals(trivia2);
        var equals2 = trivia1 == trivia2;
        var equals3 = trivia1 != trivia2;

        // Assert
        Assert.False(equals1);
        Assert.False(equals2);
        Assert.True(equals3);
    }

    [Fact(DisplayName =
        $"The Equals() method must return false by comparing their {nameof(SyntaxTrivia.UnderlyingNode)} properties.")]
    public void EqualsMethod_DifferentNodes_MustReturnFalse()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken1 = new(null, null, 0, 0);
        SyntaxToken syntaxToken2 = new(null, null, 3, 0);
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia trivia1 = new(syntaxToken1, triviaNode, Position, Index);
        SyntaxTrivia trivia2 = new(syntaxToken2, triviaNode, Position, Index);

        // Act
        var equals1 = trivia1.Equals(trivia2);
        var equals2 = trivia1 == trivia2;
        var equals3 = trivia1 != trivia2;

        // Assert
        Assert.False(equals1);
        Assert.False(equals2);
        Assert.True(equals3);
    }

    [Fact(DisplayName =
        $"The Equals() method must return true by comparing their {nameof(SyntaxTrivia.UnderlyingNode)} properties.")]
    public void EqualsMethod_EqualNodes_MustReturnTrue()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken1 = new(null, null, 3, 0);
        SyntaxToken syntaxToken2 = new(null, null, 3, 0);
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia trivia1 = new(syntaxToken1, triviaNode, Position, Index);
        SyntaxTrivia trivia2 = new(syntaxToken2, triviaNode, Position, Index);

        // Act
        var equals1 = trivia1.Equals(trivia2);
        var equals2 = trivia1 == trivia2;
        var equals3 = trivia1 != trivia2;

        // Assert
        Assert.True(equals1);
        Assert.True(equals2);
        Assert.False(equals3);
    }

    [Fact(DisplayName =
        $"The Equals() method (object overload) must return false by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_ObjectOverload_DifferentPosition_MustReturnFalse()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position1 = 0;
        const int Position2 = 5;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        var trivia1 = new SyntaxTrivia(syntaxToken, triviaNode, Position1, Index);
        object trivia2 = new SyntaxTrivia(syntaxToken, triviaNode, Position2, Index);

        // Act
        var equals = trivia1.Equals(trivia2);

        // Assert
        Assert.False(equals);
    }

    [Fact(DisplayName =
        $"The Equals() method (object overload) must return true by comparing their {nameof(SyntaxTrivia.Position)} properties.")]
    public void EqualsMethod_ObjectOverload_SamePosition_MustReturnTrue()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position1 = 5;
        const int Position2 = 5;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);

        SyntaxTrivia trivia1 = new(syntaxToken, triviaNode, Position1, Index);
        object trivia2 = new SyntaxTrivia(syntaxToken, triviaNode, Position2, Index);

        // Act
        var equals = trivia1.Equals(trivia2);

        // Assert
        Assert.True(equals);
    }

    [Fact(DisplayName = "The GetHashCode() method must be computed.")]
    public void GetHashCodeMethod_MustBeComputed()
    {
        // Arrange
        const string TriviaText = "\r\n";
        const int Position = 0;
        const int Index = 0;
        SyntaxToken syntaxToken = default;
        var triviaNode = InternalSyntax.SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, TriviaText);
        var hashCode = HashCode.Combine(syntaxToken, triviaNode, Position, Index);

        SyntaxTrivia trivia = new(syntaxToken, triviaNode, Position, Index);

        // Act
        var actualHashCode = trivia.GetHashCode();

        // Assert
        Assert.Equal(hashCode, actualHashCode);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxTrivia.FullSpan)} must be computed from {nameof(SyntaxTrivia.UnderlyingNode)}")]
    public void FullSpanProperty_MustBeComputedFromNode()
    {
        // Arrange
        const string TriviaText = "    ";
        var trivia = SyntaxFactory.SyntaxTrivia(SyntaxKind.WhitespaceTrivia, TriviaText);

        // Act
        var actualTextSpan = trivia.FullSpan;

        // Assert
        Assert.Equal(0, actualTextSpan.Start);
        Assert.Equal(4, actualTextSpan.Length);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxTrivia.Span)} must be computed from {nameof(SyntaxTrivia.UnderlyingNode)}")]
    public void SpanProperty_MustBeComputedFromNode()
    {
        // Arrange
        const string TriviaText = "% comment";
        var trivia = SyntaxFactory.SyntaxTrivia(SyntaxKind.SingleLineCommentTrivia, TriviaText);

        // Act
        var actualTextSpan = trivia.Span;

        // Assert
        Assert.Equal(0, actualTextSpan.Start);
        Assert.Equal(9, actualTextSpan.Length);
    }

    [Fact(DisplayName =
        $"The {nameof(SyntaxTrivia.SyntaxTree)} property must be null if {nameof(SyntaxToken)} is null.")]
    public void SyntaxTreeProperty_NullToken_MustBeNull()
    {
        // Arrange
        const string TriviaText = "% comment";
        var trivia = SyntaxFactory.SyntaxTrivia(SyntaxKind.SingleLineCommentTrivia, TriviaText);

        // Act
        var actualSyntaxTree = trivia.SyntaxTree;

        // Assert
        Assert.Null(actualSyntaxTree);
    }

    [Fact(DisplayName = "The GetDiagnostics() method must return an empty collection by default.")]
    public void GetDiagnosticsMethod_MustReturnEmptyCollection()
    {
        // Arrange
        const string TriviaText = "% comment";
        var trivia = SyntaxFactory.SyntaxTrivia(SyntaxKind.SingleLineCommentTrivia, TriviaText);

        // Act
        var actualDiagnostics = trivia.GetDiagnostics();

        // Assert
        Assert.Empty(actualDiagnostics);
    }

    [Fact(DisplayName = "The GetLocation() method must return None if SyntaxTree is null.")]
    public void GetLocationMethod_NullSyntaxTree_MustReturnNone()
    {
        // Arrange
        const string TriviaText = "% comment";
        var trivia = SyntaxFactory.SyntaxTrivia(SyntaxKind.SingleLineCommentTrivia, TriviaText);

        // Act
        var actualLocation = trivia.GetLocation();

        // Assert
        Assert.Equal(Location.None, actualLocation);
    }
}
