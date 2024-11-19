// <copyright file="StringTextTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Tests.Lexer;

using OffDotNet.CodeAnalysis.Lexer;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
public class StringTextTests
{
    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should implement {nameof(ISourceText)} interface")]
    public void Class_ShouldImplementISourceText()
    {
        // Arrange
        var text = "123"u8;

        // Act
        var actual = new StringText(text);

        // Assert
        Assert.IsAssignableFrom<ISourceText>(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(StringText.Source)} property should return the value passed to the constructor")]
    public void SourceProperty_ShouldReturnTheValuePassedToTheConstructor()
    {
        // Arrange
        var text = "123"u8;
        var source = text.ToArray();
        var stringText = new StringText(text);

        // Act
        var actual = stringText.Source.ToArray();

        // Assert
        Assert.Equal(source, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(StringText.Length)} property should return the length of the source text")]
    public void LengthProperty_ShouldReturnTheLengthOfTheSourceText()
    {
        // Arrange
        var text = "123"u8;
        var stringText = new StringText(text);

        // Act
        var actual = stringText.Length;

        // Assert
        Assert.Equal(text.Length, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = "Indexer should return the byte at the specified position")]
    public void Indexer_ShouldReturnTheByteAtTheSpecifiedPosition()
    {
        // Arrange
        var text = "123"u8;
        const byte Expected = (byte)'2';
        var stringText = new StringText(text);

        // Act
        var actual = stringText[1];

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(StringText.CopyTo)} method should copy the specified number of bytes from the source text to the destination")]
    [InlineData(0, 0, 3, "123")]
    [InlineData(0, 0, 2, "12\0")]
    [InlineData(1, 0, 2, "23\0")]
    [InlineData(0, 1, 2, "\012")]
    [InlineData(1, 1, 1, "\02\0")]
    public void CopyToMethod_ShouldCopyTheSpecifiedNumberOfBytesFromTheSourceTextToTheDestination(
        int sourceIndex,
        int destinationIndex,
        int count,
        string expected)
    {
        // Arrange
        var text = "123"u8;
        var destination = new byte[3];
        var stringText = new StringText(text);

        // Act
        stringText.CopyTo(sourceIndex, destination, destinationIndex, count);

        // Assert
        Assert.Equal(expected, Encoding.ASCII.GetString(destination));
    }
}
