// <copyright file="PdfArrayTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.Primitives;

using Common;
using OffDotNet.Pdf.Core.Primitives;

public class PdfArrayTests
{
    [Fact(DisplayName = "Create an instance using parametrized constructor and check the Value property")]
    public void PdfArray_ParameterizedConstructor_CheckValue()
    {
        // Arrange
        IReadOnlyCollection<IPdfObject> items = new List<IPdfObject>
        {
            new PdfInteger(549),
            new PdfReal(3.14f),
            new PdfBoolean(),
            new PdfString("Ralph"),
            new PdfName("SomeName"),
        };

        IPdfArray<IPdfObject> pdfArray = items.ToPdfArray();

        // Act

        // Assert
        Assert.Equal(items, pdfArray.Value); // Checks if reference is equal
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfArray_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        IPdfArray<PdfName> pdfArray1 = new PdfName("Name1").ToPdfArray(); // Use the ToPdfArray extension method to initialize an PdfArray instance

        // Act
        bool actualResult = pdfArray1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfArray_Equals2_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        IPdfArray<IPdfObject> pdfArray1 = new PdfString("901FA", true).ToPdfArray();

        // Act
        bool actualResult = pdfArray1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has the same reference as a value")]
    public void PdfArray_Equals_SameReference_ShouldReturnTrue()
    {
        // Arrange
        IReadOnlyCollection<IPdfObject> objects1 = new List<IPdfObject> { new PdfInteger(-65), new PdfName("#ABC") };
        IPdfArray<IPdfObject> pdfArray1 = objects1.ToPdfArray(); // Use the ToPdfArray extension method to initialize an PdfArray instance
        IPdfArray<IPdfObject> pdfArray2 = pdfArray1; // Use the ToPdfArray extension method to initialize an PdfArray instance

        // Act
        bool actualResult = pdfArray1.Equals(pdfArray2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has different references as values")]
    public void PdfArray_Equals_DifferentReferences_ShouldReturnFalse()
    {
        // Arrange
        IReadOnlyCollection<IPdfObject> objects1 = new List<IPdfObject> { new PdfInteger(-65), new PdfName("#ABC") };
        IPdfArray<IPdfObject> pdfArray1 = objects1.ToPdfArray(); // Use the ToPdfArray extension method to initialize an PdfArray instance
        IPdfArray<IPdfObject> pdfArray2 = objects1.ToPdfArray(); // Use the ToPdfArray extension method to initialize an PdfArray instance

        // Act
        bool actualResult = pdfArray1.Equals(pdfArray2);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check if Bytes property returns valid data")]
    public void PdfArray_Bytes_CheckValidity()
    {
        // Arrange
        byte[] expectedBytes =
        {
            0x5B, 0x35, 0x34, 0x39, 0x20, 0x33, 0x2E, 0x31, 0x34, 0x20, 0x66, 0x61, 0x6C, 0x73, 0x65, 0x20, 0x28, 0x52, 0x61, 0x6C, 0x70, 0x68, 0x29, 0x20, 0x2F, 0x53, 0x6F, 0x6D, 0x65, 0x4E, 0x61,
            0x6D, 0x65, 0x5D,
        };

        IReadOnlyCollection<IPdfObject> items = new List<IPdfObject>
        {
            new PdfInteger(549),
            new PdfReal(3.14f),
            new PdfBoolean(),
            new PdfString("Ralph"),
            new PdfName("SomeName"),
        };

        IPdfArray<IPdfObject> pdfArray1 = items.ToPdfArray();

        // Act
        byte[] actualBytes = pdfArray1.Bytes.ToArray();

        // Assert
        Assert.True(actualBytes.SequenceEqual(expectedBytes));
    }

    [Fact(DisplayName = "Compare the hash codes of two array objects.")]
    public void PdfArray_GetHashCode_CompareHashes_ShouldBeEqual()
    {
        // Arrange
        List<IPdfObject> value1 = new()
        {
            new PdfInteger(549),
            new PdfReal(3.14f),
            new PdfBoolean(),
            new PdfString("Ralph"),
            new PdfName("SomeName"),
        };
        IPdfArray<IPdfObject> pdfArray1 = new PdfArray<IPdfObject>(value1);
        IPdfArray<IPdfObject> pdfArray2 = new PdfArray<IPdfObject>(value1);
        int expectedHashCode = HashCode.Combine(nameof(PdfArray<IPdfObject>), value1);

        // Act
        int actualHashCode1 = pdfArray1.GetHashCode();
        int actualHashCode2 = pdfArray2.GetHashCode();
        bool areHashCodeEquals = actualHashCode1 == actualHashCode2;

        // Assert
        Assert.True(areHashCodeEquals);
        Assert.Equal(expectedHashCode, actualHashCode1);
        Assert.Equal(expectedHashCode, actualHashCode2);
    }

    [Fact(DisplayName = "Compare the hash codes of two array objects.")]
    public void PdfArray_GetHashCode_CompareHashes_ShouldNotBeEqual()
    {
        // Arrange
        List<IPdfObject> value1 = new()
        {
            new PdfInteger(549),
            new PdfReal(3.14f),
            new PdfBoolean(),
            new PdfString("Ralph"),
            new PdfName("SomeName"),
        };
        List<IPdfObject> value2 = new()
        {
            new PdfInteger(549),
            new PdfReal(3.14f),
            new PdfBoolean(),
            new PdfString("Ralph"),
            new PdfName("SomeName"),
        };
        IPdfArray<IPdfObject> pdfArray1 = new PdfArray<IPdfObject>(value1);
        IPdfArray<IPdfObject> pdfArray2 = new PdfArray<IPdfObject>(value2);

        // Act
        int actualHashCode1 = pdfArray1.GetHashCode();
        int actualHashCode2 = pdfArray2.GetHashCode();
        bool areHashCodeEquals = actualHashCode1 == actualHashCode2;

        // Assert
        Assert.False(areHashCodeEquals);
    }

    [Fact(DisplayName = "Check the Value property.")]
    public void PdfArray_Value_Count_ShouldReturn1()
    {
        // Arrange
        IPdfArray<PdfInteger> pdfArray1 = new PdfInteger(549).ToPdfArray();

        // Act
        int actualValueCount = pdfArray1.Value.Count;

        // Assert
        Assert.Equal(1, actualValueCount);
    }

    [Fact(DisplayName = "Check the Content property.")]
    public void PdfArray_Content_Check()
    {
        const string expectedContentValue = "[549 3.14 false (Ralph) /SomeName]";
        IPdfArray<IPdfObject> pdfArray1 = new List<IPdfObject>
        {
            new PdfInteger(549),
            new PdfReal(3.14f),
            new PdfBoolean(),
            new PdfString("Ralph"),
            new PdfName("SomeName"),
        }.ToPdfArray();

        // Act
        string actualContentValue = pdfArray1.Content;
        string actualContentValue2 = pdfArray1.Content;

        // Assert
        Assert.Equal(expectedContentValue, actualContentValue);
        Assert.Equal(actualContentValue, actualContentValue2);
    }
}
