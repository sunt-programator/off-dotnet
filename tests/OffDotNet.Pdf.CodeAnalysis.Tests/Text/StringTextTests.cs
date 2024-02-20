// <copyright file="StringTextTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Text;

using OffDotNet.Pdf.CodeAnalysis.Text;

public class StringTextTests
{
    private readonly byte[] _source;
    private readonly StringText _stringText;

    public StringTextTests()
    {
        _source = "This is a test string"u8.ToArray();
        _stringText = new StringText(_source);
    }

    [Fact(DisplayName =
        $"The {nameof(StringText.Source)} property should return the same string that was passed to the constructor")]
    public void SourceProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var result = _stringText.Source.Span;

        // Assert
        Assert.Equal(_source, result);
    }

    [Fact(DisplayName = $"The {nameof(StringText.Length)} property must be computed from the source string")]
    public void LengthProperty_MustBeComputedFromSourceString()
    {
        // Arrange

        // Act
        var result = _stringText.Length;

        // Assert
        Assert.Equal(_source.Length, result);
    }

    [Fact(DisplayName = "The indexer getter must return the character at the specified position")]
    public void IndexerGetter_MustReturnCharacterAtSpecifiedPosition()
    {
        // Arrange
        const int Position = 5;

        // Act
        var result = _stringText[Position];

        // Assert
        Assert.Equal(_source[Position], result);
    }

    [Fact(DisplayName =
        "The CopyTo(int, ReadOnlySpan<byte>, int, int) method must copy a specified number of characters " +
        "from a specified position in the source text to a specified position in a destination span")]
    public void CopyToMethod_MustCopyTheCharacters()
    {
        // Arrange
        const int SourceIndex = 10;
        const int DestinationIndex = 0;
        const int Count = 4;
        var expected = "test"u8.ToArray();
        var destination = new byte[Count];

        // Act
        _stringText.CopyTo(SourceIndex, destination, DestinationIndex, Count);

        // Assert
        Assert.Equal(expected, destination);
    }
}
