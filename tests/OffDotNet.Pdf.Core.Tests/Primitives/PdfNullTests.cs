// <copyright file="PdfNullTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Primitives;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.Primitives;

public class PdfNullTests
{
    [Fact(DisplayName = "Check the length of the PDF null primitive")]
    public void PdfNull_Length_CheckValue()
    {
        // Arrange
        PdfNull pdfNull = default(PdfNull);

        // Act
        float actualLength = pdfNull.Length;

        // Assert
        Assert.Equal(4, actualLength);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfNull_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfNull pdfNull1 = default(PdfNull);

        // Act
        bool actualResult = pdfNull1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method with object type as an argument")]
    public void PdfNull_Equality_CheckEquals()
    {
        // Arrange
        PdfNull pdfNull1 = default(PdfNull);
        PdfNull pdfNull2 = default(PdfNull);

        // Act
        // ReSharper disable once RedundantCast
        bool actualResult = pdfNull1.Equals((object)pdfNull2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check if Bytes property returns valid data")]
    public void PdfNull_Bytes_CheckValidity()
    {
        // Arrange
        byte[] expectedBytes = { 110, 117, 108, 108 };
        PdfNull pdfNull1 = default(PdfNull);

        // Act
        ReadOnlyMemory<byte> actualBytes = pdfNull1.Bytes;

        // Assert
        Assert.True(actualBytes.Span.SequenceEqual(expectedBytes));
    }

    [Fact(DisplayName = "Check if GetHashCode method returns valid value")]
    public void PdfNull_GetHashCode_CheckValidity()
    {
        // Arrange
        PdfNull pdfNull1 = default(PdfNull);
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
        PdfNull pdfNull1 = default(PdfNull);
        PdfNull pdfNull2 = default(PdfNull);

        // Act
        int actualHashCode1 = pdfNull1.GetHashCode();
        int actualHashCode2 = pdfNull2.GetHashCode();
        bool areHashCodeEquals = actualHashCode1 == actualHashCode2;

        // Assert
        Assert.True(areHashCodeEquals);
    }

    [Fact(DisplayName = "Check Content property for equality")]
    public void PdfNull_Content_CheckEquality()
    {
        // Arrange
        PdfNull pdfNull1 = default(PdfNull);
        const string expectedStringValue = "null";

        // Act
        string actualPdfNullStringValue = pdfNull1.Content;

        // Assert
        Assert.Equal(expectedStringValue, actualPdfNullStringValue);
    }
}
