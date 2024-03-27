// <copyright file="PdfNullTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.Primitives;

using OffDotNet.Pdf.Core.Primitives;

public class PdfNullTests
{
    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfNull_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        var pdfNull1 = default(PdfNull);

        // Act
        var actualResult = pdfNull1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method with object type as an argument")]
    public void PdfNull_Equality_CheckEquals()
    {
        // Arrange
        var pdfNull1 = default(PdfNull);
        var pdfNull2 = default(PdfNull);

        // Act
        // ReSharper disable once RedundantCast
        var actualResult = pdfNull1.Equals(pdfNull2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check if Bytes property returns valid data")]
    public void PdfNull_Bytes_CheckValidity()
    {
        // Arrange
        byte[] expectedBytes = [110, 117, 108, 108];
        var pdfNull1 = default(PdfNull);

        // Act
        var actualBytes = pdfNull1.Bytes;

        // Assert
        Assert.True(actualBytes.Span.SequenceEqual(expectedBytes));
    }

    [Fact(DisplayName = "Check if GetHashCode method returns valid value")]
    public void PdfNull_GetHashCode_CheckValidity()
    {
        // Arrange
        var pdfNull1 = default(PdfNull);
        float expectedHashCode = HashCode.Combine(nameof(PdfNull), "null");

        // Act
        float actualHashCode = pdfNull1.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "Compare the hash codes of two integer objects.")]
    public void PdfNull_GetHashCode_CompareHashes()
    {
        // Arrange
        var pdfNull1 = default(PdfNull);
        var pdfNull2 = default(PdfNull);

        // Act
        var actualHashCode1 = pdfNull1.GetHashCode();
        var actualHashCode2 = pdfNull2.GetHashCode();
        var areHashCodeEquals = actualHashCode1 == actualHashCode2;

        // Assert
        Assert.True(areHashCodeEquals);
    }

    [Fact(DisplayName = "Check Content property for equality")]
    public void PdfNull_Content_CheckEquality()
    {
        // Arrange
        var pdfNull1 = default(PdfNull);
        const string ExpectedStringValue = "null";

        // Act
        var actualPdfNullStringValue = pdfNull1.Content;

        // Assert
        Assert.Equal(ExpectedStringValue, actualPdfNullStringValue);
    }
}
