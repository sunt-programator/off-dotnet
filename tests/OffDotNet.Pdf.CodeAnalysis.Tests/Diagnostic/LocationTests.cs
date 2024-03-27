// <copyright file="LocationTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Text;

public class LocationTests
{
    private readonly Location _location = Substitute.For<Location>();

    [Fact(DisplayName = $"The {nameof(Location.Kind)} property must return {nameof(LocationKind.None)}.")]
    public void KindProperty_MustReturnNone()
    {
        // Arrange

        // Act
        var actualKind = _location.Kind;

        // Assert
        Assert.Equal(LocationKind.None, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(Location.SourceSpan)} property must return the default value.")]
    public void SourceSpanProperty_MustReturnDefaultValue()
    {
        // Arrange

        // Act
        var actualSourceSpan = _location.SourceSpan;

        // Assert
        Assert.Equal(default, actualSourceSpan);
    }

    [Fact(DisplayName = $"The {nameof(Location.LineSpan)} property must return the default value.")]
    public void LineSpanProperty_MustReturnDefaultValue()
    {
        // Arrange

        // Act
        var actualFileLinePositionSpan = _location.LineSpan;

        // Assert
        Assert.Equal(default, actualFileLinePositionSpan);
    }

    [Fact(DisplayName = $"The {nameof(Location.None)} field must return the static instance.")]
    public void NoneField_MustReturnStaticInstance()
    {
        // Arrange

        // Act
        var actualLocation = Location.None;

        // Assert
        Assert.Equal(Location.NoLocation.Instance, actualLocation);
    }

    [Fact(DisplayName = "The Equals() method must return true.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        var location1 = Location.None;
        var location2 = Location.None;

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
        var location1 = Location.None;
        Location? location2 = null;

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

    [Fact(DisplayName = $"The ToString() method with a {nameof(SyntaxTree)} must include the {nameof(Location.Kind)}, {nameof(SyntaxTree.FilePath)} and {nameof(Location.SourceSpan)} properties.")]
    public void ToStringMethod_WithSyntaxTree_MustIncludeKind_PathAndSourceSpan()
    {
        // Arrange
        const string ExpectedLocation = @"None (C:\1.pdf[1..2))";
        var syntaxTree = Substitute.For<SyntaxTree>();
        syntaxTree.FilePath.Returns(@"C:\1.pdf");
        _location.SyntaxTree.Returns(syntaxTree);
        _location.SourceSpan.Returns(new TextSpan(1, 1));

        // Act
        var actualString = _location.ToString();

        // Assert
        Assert.Equal(ExpectedLocation, actualString);
    }

    [Fact(DisplayName = $"The ToString() method with {nameof(FileLinePositionSpan.Path)} must include the {nameof(Location.Kind)} and {nameof(FileLinePositionSpan)} properties.")]
    public void ToStringMethod_WithPathInFileLinePositionSpan_MustIncludeKindAndFileLinePositionSpan()
    {
        // Arrange
        const string ExpectedLocation = @"None (C:\1.pdf@2:3)";
        _location.SyntaxTree.Returns((SyntaxTree?)null);
        _location.LineSpan.Returns(new FileLinePositionSpan(@"C:\1.pdf", new LinePositionSpan(new LinePosition(1, 2), new LinePosition(2, 2))));

        // Act
        var actualString = _location.ToString();

        // Assert
        Assert.Equal(ExpectedLocation, actualString);
    }

    [Fact(DisplayName = $"The {nameof(LocalizableString)} class must implement the {nameof(IEquatable<Location>)} interface.")]
    public void Class_MustImplementIEquatableInterface()
    {
        // Arrange

        // Act
        var actualLocation = Location.None;

        // Assert
        Assert.IsAssignableFrom<IEquatable<Location>>(actualLocation);
    }
}
