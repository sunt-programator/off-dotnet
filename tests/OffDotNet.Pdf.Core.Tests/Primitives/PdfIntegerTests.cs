// <copyright file="PdfIntegerTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.Tests.Primitives;

public class PdfIntegerTests
{
    [Fact(DisplayName = "Ensure that the parameterless constructor will set 0 to the Value property.")]
    public void PdfInteger_ParameterlessConstructor_ShouldSetZeroToValueProperty()
    {
        // Arrange
        PdfInteger pdfInteger1 = new();

        // Act

        // Assert
        Assert.Equal(0, pdfInteger1.Value);
    }

    [Theory(DisplayName = "Create an instance using parametrized constructor and check the Value property")]
    [InlineData(123)]
    [InlineData(43445)]
    [InlineData(+17)]
    [InlineData(-98)]
    [InlineData(0)]
    public void PdfInteger_ParameterizedConstructor_CheckValue(int value)
    {
        // Arrange
        PdfInteger pdfInteger = value; // Use an implicit conversion from int to PdfInteger

        // Act
        int expectedValue = pdfInteger.Value;

        // Assert
        Assert.Equal(expectedValue, pdfInteger.Value);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfInteger_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfInteger pdfInteger1 = -98; // Use an implicit conversion from int to PdfInteger

        // Act
        bool actualResult = pdfInteger1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Theory(DisplayName = "Check Equals method with object type as an argument")]
    [InlineData(123, 123, true)]
    [InlineData(+17, 17, true)]
    [InlineData(+0, -0, true)]
    [InlineData(-98, -98, true)]
    [InlineData(43445, 43445, true)]
    [InlineData(123, 43445, false)]
    [InlineData(123, +17, false)]
    [InlineData(-98, 0, false)]
    public void PdfInteger_Equality_CheckEquals(int value1, int value2, bool expectedResult)
    {
        // Arrange
        PdfInteger pdfInteger1 = value1; // Use an implicit conversion from int to PdfInteger
        PdfInteger pdfInteger2 = value2; // Use an implicit conversion from int to PdfInteger

        // Act
        bool actualResult = pdfInteger1.Equals((object)pdfInteger2);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory(DisplayName = "Check if Bytes property returns valid data")]
    [InlineData(123, new byte[] { 49, 50, 51 })]
    [InlineData(43445, new byte[] { 52, 51, 52, 52, 53 })]
    [InlineData(+17, new byte[] { 49, 55 })]
    [InlineData(-98, new byte[] { 45, 57, 56 })]
    [InlineData(0, new byte[] { 48 })]
    public void PdfInteger_Bytes_CheckValidity(int value1, byte[] expectedBytes)
    {
        // Arrange
        PdfInteger pdfInteger1 = value1; // Use an implicit conversion from int to PdfInteger

        // Act
        ReadOnlyMemory<byte> actualBytes = pdfInteger1.Bytes;

        // Assert
        Assert.True(actualBytes.Span.SequenceEqual(expectedBytes));
    }

    [Theory(DisplayName = "Check if GetHashCode method returns valid value")]
    [InlineData(123)]
    [InlineData(43445)]
    [InlineData(+17)]
    [InlineData(-98)]
    [InlineData(0)]
    public void PdfInteger_GetHashCode_CheckValidity(int value1)
    {
        // Arrange
        PdfInteger pdfInteger1 = value1; // Use an implicit conversion from int to PdfInteger
        int expectedHashCode = HashCode.Combine(nameof(PdfInteger), value1);

        // Act
        int actualHashCode = pdfInteger1.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Theory(DisplayName = "Compare the hash codes of two integer objects.")]
    [InlineData(123, 123, true)]
    [InlineData(+17, 17, true)]
    [InlineData(+0, -0, true)]
    [InlineData(-98, -98, true)]
    [InlineData(43445, 43445, true)]
    [InlineData(123, 43445, false)]
    [InlineData(123, +17, false)]
    [InlineData(-98, 0, false)]
    public void PdfInteger_GetHashCode_CompareHashes(int value1, int value2, bool expectedResult)
    {
        // Arrange
        PdfInteger pdfInteger1 = value1; // Use an implicit conversion from int to PdfInteger
        PdfInteger pdfInteger2 = value2; // Use an implicit conversion from int to PdfInteger

        // Act
        int actualHashCode1 = pdfInteger1;
        int actualHashCode2 = pdfInteger2;
        bool areHashCodeEquals = actualHashCode1 == actualHashCode2;

        // Assert
        Assert.Equal(expectedResult, areHashCodeEquals);
    }

    [Theory(DisplayName = "Check if comparison operators works properly")]
    [InlineData(123, 123, true, false, true, false, true, false)]
    [InlineData(+17, 17, true, false, true, false, true, false)]
    [InlineData(+0, -0, true, false, true, false, true, false)]
    [InlineData(-98, -98, true, false, true, false, true, false)]
    [InlineData(43445, 43445, true, false, true, false, true, false)]
    [InlineData(123, 43445, false, true, false, false, true, true)]
    [InlineData(123, +17, false, true, true, true, false, false)]
    [InlineData(-98, 0, false, true, false, false, true, true)]
    public void PdfInteger_Operators_CheckValidity(int value1, int value2, bool expectedEq, bool expectedNe, bool expectedGe, bool expectedGt, bool expectedLe, bool expectedLt)
    {
        // Arrange
        PdfInteger pdfInteger1 = value1; // Use an implicit conversion from int to PdfInteger
        PdfInteger pdfInteger2 = value2; // Use an implicit conversion from int to PdfInteger

        // Act
        bool actualEqual = pdfInteger1 == pdfInteger2;
        bool actualNotEqual = pdfInteger1 != pdfInteger2;
        bool actualGreaterOrEqualThan = pdfInteger1 >= pdfInteger2;
        bool actualGreaterThan = pdfInteger1 > pdfInteger2;
        bool actualLessOrEqualThan = pdfInteger1 <= pdfInteger2;
        bool actualLessThan = pdfInteger1 < pdfInteger2;

        // Assert
        Assert.Equal(actualEqual, expectedEq);
        Assert.Equal(actualNotEqual, expectedNe);
        Assert.Equal(actualGreaterOrEqualThan, expectedGe);
        Assert.Equal(actualGreaterThan, expectedGt);
        Assert.Equal(actualLessOrEqualThan, expectedLe);
        Assert.Equal(actualLessThan, expectedLt);
    }

    [Theory(DisplayName = "Check if implicit operator works")]
    [InlineData(123)]
    [InlineData(43445)]
    [InlineData(+17)]
    [InlineData(-98)]
    [InlineData(0)]
    public void PdfInteger_CheckImplicitOperator(int value1)
    {
        // Arrange
        PdfInteger pdfInteger1 = new(value1);

        // Act
        int actualValue = pdfInteger1; // Use an implicit conversion from PdfInteger to int
        int expectedValue = pdfInteger1.Value;

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Theory(DisplayName = "Check CompareTo method")]
    [InlineData(123, 123, 0)]
    [InlineData(+17, 17, 0)]
    [InlineData(+0, -0, 0)]
    [InlineData(-98, -98, 0)]
    [InlineData(43445, 43445, 0)]
    [InlineData(123, 43445, -1)]
    [InlineData(123, +17, 1)]
    [InlineData(-98, 0, -1)]
    public void PdfInteger_CompareTo_CheckValidity(int value1, int value2, int expectedValue)
    {
        // Arrange
        PdfInteger pdfInteger1 = new(value1);
        object pdfInteger2 = new PdfInteger(value2);

        // Act
        int actualValue = pdfInteger1.CompareTo(pdfInteger2);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact(DisplayName = "Check CompareTo method should throw an exception")]
    public void PdfInteger_CompareTo_ThrowsException()
    {
        // Arrange
        PdfInteger pdfInteger1 = new();
        object pdfInteger2 = 5;

        // Act
        // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
        Action actualValueDelegate = () => pdfInteger1.CompareTo(pdfInteger2);

        // Assert
        Assert.Throws<ArgumentException>(actualValueDelegate);
    }

    [Theory(DisplayName = "Check Content property for equality")]
    [InlineData(123, "123")]
    [InlineData(43445, "43445")]
    [InlineData(+17, "17")] // Plus sign is automatically removed by C#.
    [InlineData(-98, "-98")] // Minus sign + two digits.
    [InlineData(0, "0")]
    public void PdfInteger_Content_CheckEquality(int value1, string expectedPdfIntegerStringValue)
    {
        // Arrange
        PdfInteger pdfInteger1 = value1; // Use an implicit conversion from int to PdfInteger

        // Act
        string actualPdfIntegerStringValue = pdfInteger1.Content;

        // Assert
        Assert.Equal(expectedPdfIntegerStringValue, actualPdfIntegerStringValue);
    }
}
