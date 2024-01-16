// <copyright file="SourceLocationTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Text;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

public class SourceLocationTests
{
    private const string FilePath = @"C:\test.pdf";
    private static readonly TextSpan Span = new(0, 2);
    private static readonly SyntaxTree SyntaxTree = Substitute.For<SyntaxTree>();
    private readonly SourceLocation location = (SourceLocation)Location.Create(SyntaxTree, Span);

    public SourceLocationTests()
    {
        SyntaxTree.FilePath.Returns(FilePath);
    }

    [Fact(DisplayName = $"The {nameof(SourceLocation.Kind)} property must return {nameof(LocationKind.SourceFile)}.")]
    public void KindProperty_MustReturnExternalFile()
    {
        // Arrange

        // Act
        LocationKind actualKind = this.location.Kind;

        // Assert
        Assert.Equal(LocationKind.SourceFile, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(SourceLocation.SourceSpan)} property must be assigned from constructor.")]
    public void SourceSpanProperty_MustBeAssignedFromConstructor()
    {
        // Arrange

        // Act
        TextSpan actualSourceSpan = this.location.SourceSpan;

        // Assert
        Assert.Equal(Span, actualSourceSpan);
    }

    [Fact(DisplayName = $"The {nameof(SourceLocation.SyntaxTree)} property must be assigned from constructor.")]
    public void SyntaxTreeProperty_MustBeAssignedFromConstructor()
    {
        // Arrange

        // Act
        SyntaxTree? actualSyntaxTree = this.location.SyntaxTree;

        // Assert
        Assert.Equal(SyntaxTree, actualSyntaxTree);
    }

    [Fact(DisplayName = "The Equals() method must return true.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        SourceLocation location1 = (SourceLocation)Location.Create(SyntaxTree, Span);
        SourceLocation location2 = (SourceLocation)Location.Create(SyntaxTree, Span);

        // Act
        bool actualEquals1 = location1.Equals(location2);
        bool actualEquals2 = location1.Equals((object?)location2);
        bool actualEquals3 = location1 == location2;
        bool actualEquals4 = location1 != location2;

        // Assert
        Assert.True(actualEquals1);
        Assert.True(actualEquals2);
        Assert.True(actualEquals3);
        Assert.False(actualEquals4);
    }

    [Fact(DisplayName = "The Equals() method must return true.")]
    public void EqualsMethod_SameReference_MustReturnTrue()
    {
        // Arrange
        SourceLocation location1 = (SourceLocation)Location.Create(SyntaxTree, Span);
        SourceLocation location2 = location1;

        // Act
        bool actualEquals1 = location1.Equals(location2);
        bool actualEquals2 = location1.Equals((object?)location2);
        bool actualEquals3 = location1 == location2;
        bool actualEquals4 = location1 != location2;

        // Assert
        Assert.True(actualEquals1);
        Assert.True(actualEquals2);
        Assert.True(actualEquals3);
        Assert.False(actualEquals4);
    }

    [Fact(DisplayName = "The Equals() method must return false.")]
    public void EqualsMethod_MustReturnFalse()
    {
        // Arrange
        TextSpan span2 = new(3, 3);
        SourceLocation location1 = (SourceLocation)Location.Create(SyntaxTree, Span);
        SourceLocation location2 = (SourceLocation)Location.Create(SyntaxTree, span2);

        // Act
        bool actualEquals1 = location1.Equals(location2);
        bool actualEquals2 = location1.Equals((object?)location2);
        bool actualEquals3 = location1 == location2;
        bool actualEquals4 = location1 != location2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The {nameof(LocalizableString)} class must implement the {nameof(IEquatable<SourceLocation>)} interface.")]
    public void Class_MustImplementIEquatableInterface()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsAssignableFrom<IEquatable<SourceLocation>>(this.location);
    }

    [Fact(DisplayName = $"The GetHashCode() method must include the {nameof(SourceLocation.SourceSpan)} and {nameof(SourceLocation.LineSpan)} properties.")]
    public void GetHashCodeMethod_MustIncludeLineAndSourceSpan()
    {
        // Arrange
        int expectedHashCode = HashCode.Combine(this.location.SyntaxTree, this.location.LineSpan);

        // Act
        int actualHashCode = this.location.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = $"The ToString() method must include the {nameof(SourceLocation.Kind)}, {nameof(CodeAnalysis.Syntax.SyntaxTree.FilePath)} and {nameof(SourceLocation.SourceSpan)} properties.")]
    public void ToStringMethod_MustIncludeKindAndFileLinePositionSpan()
    {
        // Arrange
        const string expectedLocation = @"SourceFile (C:\test.pdf[0..2))";

        // Act
        string actualString = this.location.ToString();

        // Assert
        Assert.Equal(expectedLocation, actualString);
    }
}
