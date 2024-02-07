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
    private static readonly TextSpan SourceSpan = new(0, 2);
    private static readonly LinePositionSpan LineSpan = new(new LinePosition(1, 1), new LinePosition(2, 2));
    private readonly ExternalFileLocation location = (ExternalFileLocation)Location.Create(FilePath, SourceSpan, LineSpan);

    [Fact(DisplayName = $"The {nameof(ExternalFileLocation.Kind)} property must return {nameof(LocationKind.ExternalFile)}.")]
    public void KindProperty_MustReturnExternalFile()
    {
        // Arrange

        // Act
        LocationKind actualKind = this.location.Kind;

        // Assert
        Assert.Equal(LocationKind.ExternalFile, actualKind);
    }

    [Fact(DisplayName = $"The {nameof(SourceLocation.SourceSpan)} property must be assigned from constructor.")]
    public void SourceSpanProperty_MustBeAssignedFromConstructor()
    {
        // Arrange

        // Act
        TextSpan actualSourceSpan = this.location.SourceSpan;

        // Assert
        Assert.Equal(SourceSpan, actualSourceSpan);
    }

    [Fact(DisplayName = $"The {nameof(SourceLocation.LineSpan)} property must be assigned from constructor.")]
    public void LineSpanProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        FileLinePositionSpan expectedLineSpan = new(FilePath, LineSpan);

        // Act
        FileLinePositionSpan actualLineSpan = this.location.LineSpan;

        // Assert
        Assert.Equal(expectedLineSpan, actualLineSpan);
    }

    [Fact(DisplayName = "The Equals() method must return true.")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange
        ExternalFileLocation location1 = (ExternalFileLocation)Location.Create(FilePath, SourceSpan, LineSpan);
        ExternalFileLocation location2 = (ExternalFileLocation)Location.Create(FilePath, SourceSpan, LineSpan);

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
        ExternalFileLocation location1 = (ExternalFileLocation)Location.Create(FilePath, SourceSpan, LineSpan);
        ExternalFileLocation location2 = location1;

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
        const string filePath2 = @"C:\test2.pdf";
        ExternalFileLocation location1 = (ExternalFileLocation)Location.Create(FilePath, SourceSpan, LineSpan);
        ExternalFileLocation location2 = (ExternalFileLocation)Location.Create(filePath2, SourceSpan, LineSpan);

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

    [Fact(DisplayName = $"The {nameof(LocalizableString)} class must implement the {nameof(IEquatable<ExternalFileLocation>)} interface.")]
    public void Class_MustImplementIEquatableInterface()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsAssignableFrom<IEquatable<ExternalFileLocation>>(this.location);
    }

    [Fact(DisplayName = $"The GetHashCode() method must include the {nameof(SourceLocation.SourceSpan)} and {nameof(SourceLocation.LineSpan)} properties.")]
    public void GetHashCodeMethod_MustIncludeLineAndSourceSpan()
    {
        // Arrange
        int expectedHashCode = HashCode.Combine(this.location.SourceSpan, this.location.LineSpan);

        // Act
        int actualHashCode = this.location.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = $"The ToString() method must include the {nameof(SourceLocation.Kind)} and {nameof(FileLinePositionSpan)} properties.")]
    public void ToStringMethod_MustIncludeKindAndFileLinePositionSpan()
    {
        // Arrange
        const string expectedLocation = @"ExternalFile (C:\test.pdf@2:2)";

        // Act
        string actualString = this.location.ToString();

        // Assert
        Assert.Equal(expectedLocation, actualString);
    }
}
