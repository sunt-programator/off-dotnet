// <copyright file="ExternalFileLocationTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Text;

public class ExternalFileLocationTests
{
    private const string FilePath = @"C:\test.pdf";
    private static readonly TextSpan s_sourceSpan = new(0, 2);
    private static readonly LinePositionSpan s_lineSpan = new(new LinePosition(1, 1), new LinePosition(2, 2));
    private readonly ExternalFileLocation _location = (ExternalFileLocation)Location.Create(FilePath, s_sourceSpan, s_lineSpan);

    [Fact(DisplayName = $"The {nameof(ExternalFileLocation.Kind)} property must return {nameof(LocationKind.ExternalFile)}.")]
    public void KindProperty_MustReturnExternalFile()
    {
        // Arrange

        // Act
        var actualKind = _location.Kind;

        // Assert
        Assert.Equal(LocationKind.ExternalFile, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(SourceLocation.SourceSpan)} property must be assigned from constructor.")]
    public void SourceSpanProperty_MustBeAssignedFromConstructor()
    {
        // Arrange

        // Act
        var actualSourceSpan = _location.SourceSpan;

        // Assert
        Assert.Equal(s_sourceSpan, actualSourceSpan);
    }

    [Fact(DisplayName = $"The {nameof(SourceLocation.LineSpan)} property must be assigned from constructor.")]
    public void LineSpanProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        FileLinePositionSpan expectedLineSpan = new(FilePath, s_lineSpan);

        // Act
        var actualLineSpan = _location.LineSpan;

        // Assert
        Assert.Equal(expectedLineSpan, actualLineSpan);
    }

    [Fact(DisplayName = "The Equals() method must return true.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        var location1 = (ExternalFileLocation)Location.Create(FilePath, s_sourceSpan, s_lineSpan);
        var location2 = (ExternalFileLocation)Location.Create(FilePath, s_sourceSpan, s_lineSpan);

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
        var location1 = (ExternalFileLocation)Location.Create(FilePath, s_sourceSpan, s_lineSpan);
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
        const string FilePath2 = @"C:\test2.pdf";
        var location1 = (ExternalFileLocation)Location.Create(FilePath, s_sourceSpan, s_lineSpan);
        var location2 = (ExternalFileLocation)Location.Create(FilePath2, s_sourceSpan, s_lineSpan);

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

    [Fact(DisplayName = $"The {nameof(LocalizableString)} class must implement the {nameof(IEquatable<ExternalFileLocation>)} interface.")]
    public void Class_MustImplementIEquatableInterface()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsAssignableFrom<IEquatable<ExternalFileLocation>>(_location);
    }

    [Fact(DisplayName = $"The GetHashCode() method must include the {nameof(SourceLocation.SourceSpan)} and {nameof(SourceLocation.LineSpan)} properties.")]
    public void GetHashCodeMethod_MustIncludeLineAndSourceSpan()
    {
        // Arrange
        var expectedHashCode = HashCode.Combine(_location.SourceSpan, _location.LineSpan);

        // Act
        var actualHashCode = _location.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = $"The ToString() method must include the {nameof(SourceLocation.Kind)} and {nameof(FileLinePositionSpan)} properties.")]
    public void ToStringMethod_MustIncludeKindAndFileLinePositionSpan()
    {
        // Arrange
        const string ExpectedLocation = @"ExternalFile (C:\test.pdf@2:2)";

        // Act
        var actualString = _location.ToString();

        // Assert
        Assert.Equal(ExpectedLocation, actualString);
    }
}
