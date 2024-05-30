// <copyright file="SourceLocationTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Text;

public class SourceLocationTests
{
    private const string FilePath = @"C:\test.pdf";
    private static readonly TextSpan s_span = new(0, 2);
    private static readonly AbstractSyntaxTree s_abstractSyntaxTree = Substitute.For<AbstractSyntaxTree>();
    private readonly SourceLocation _location = (SourceLocation)Location.Create(s_abstractSyntaxTree, s_span);

    public SourceLocationTests()
    {
        s_abstractSyntaxTree.FilePath.Returns(FilePath);
    }

    [Fact(DisplayName = $"The {nameof(SourceLocation.Kind)} property must return {nameof(LocationKind.SourceFile)}.")]
    public void KindProperty_MustReturnExternalFile()
    {
        // Arrange

        // Act
        var actualKind = _location.Kind;

        // Assert
        Assert.Equal(LocationKind.SourceFile, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(SourceLocation.SourceSpan)} property must be assigned from constructor.")]
    public void SourceSpanProperty_MustBeAssignedFromConstructor()
    {
        // Arrange

        // Act
        var actualSourceSpan = _location.SourceSpan;

        // Assert
        Assert.Equal(s_span, actualSourceSpan);
    }

    [Fact(DisplayName = $"The {nameof(SourceLocation.AbstractSyntaxTree)} property must be assigned from constructor.")]
    public void SyntaxTreeProperty_MustBeAssignedFromConstructor()
    {
        // Arrange

        // Act
        var actualSyntaxTree = _location.AbstractSyntaxTree;

        // Assert
        Assert.Equal(s_abstractSyntaxTree, actualSyntaxTree);
    }

    [Fact(DisplayName = "The Equals() method must return true.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        var location1 = (SourceLocation)Location.Create(s_abstractSyntaxTree, s_span);
        var location2 = (SourceLocation)Location.Create(s_abstractSyntaxTree, s_span);

        // Act
        var actualEquals1 = location1.Equals(location2);
        var actualEquals2 = location1.Equals((object?)location2);
        var actualEquals3 = location1 == location2;
        var actualEquals4 = location1 != location2;

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
        var location1 = (SourceLocation)Location.Create(s_abstractSyntaxTree, s_span);
        var location2 = location1;

        // Act
        var actualEquals1 = location1.Equals(location2);
        var actualEquals2 = location1.Equals((object?)location2);
        var actualEquals3 = location1 == location2;
        var actualEquals4 = location1 != location2;

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
        var location1 = (SourceLocation)Location.Create(s_abstractSyntaxTree, s_span);
        var location2 = (SourceLocation)Location.Create(s_abstractSyntaxTree, span2);

        // Act
        var actualEquals1 = location1.Equals(location2);
        var actualEquals2 = location1.Equals((object?)location2);
        var actualEquals3 = location1 == location2;
        var actualEquals4 = location1 != location2;

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
        Assert.IsAssignableFrom<IEquatable<SourceLocation>>(_location);
    }

    [Fact(DisplayName = $"The GetHashCode() method must include the {nameof(SourceLocation.SourceSpan)} and {nameof(SourceLocation.LineSpan)} properties.")]
    public void GetHashCodeMethod_MustIncludeLineAndSourceSpan()
    {
        // Arrange
        var expectedHashCode = HashCode.Combine(_location.AbstractSyntaxTree, _location.LineSpan);

        // Act
        var actualHashCode = _location.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = $"The ToString() method must include the {nameof(SourceLocation.Kind)}, {nameof(AbstractSyntaxTree.FilePath)} and {nameof(SourceLocation.SourceSpan)} properties.")]
    public void ToStringMethod_MustIncludeKindAndFileLinePositionSpan()
    {
        // Arrange
        const string ExpectedLocation = @"SourceFile (C:\test.pdf[0..2))";

        // Act
        var actualString = _location.ToString();

        // Assert
        Assert.Equal(ExpectedLocation, actualString);
    }
}
