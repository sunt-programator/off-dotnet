// <copyright file="PdfArrayTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;
using Xunit;

namespace OffDotNet.Pdf.Core.Tests.Primitives;

public class PdfArrayTests
{
    [Theory(DisplayName = "Create an instance using parametrized constructor and check the Value property")]
    [MemberData(nameof(PdfArrayTestDataGenerator.PdfArray_ParameterizedConstructor_TestCases), MemberType = typeof(PdfArrayTestDataGenerator))]
    public void PdfArray_ParameterizedConstructor_CheckValue(List<IPdfObject> inputValue)
    {
        // Arrange
        PdfArray<IPdfObject> pdfArray = PdfArray<IPdfObject>.CreateRange(inputValue); // Use the CreateRange static method to initialize an PdfArray instance

        // Act

        // Assert
        Assert.Equal(inputValue, pdfArray.Value); // Checks if reference is equal
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfArray_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfArray<PdfName> pdfArray1 = new PdfName("Name1").ToPdfArray(); // Use the ToPdfArray extension method to initialize an PdfArray instance

        // Act
        bool actualResult = pdfArray1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfArray_Equals2_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfArray<IPdfObject> pdfArray1 = PdfArray<IPdfObject>.Create(new PdfString("901FA", true)); // Use the Create static method to initialize an PdfArray instance

        // Act
        bool actualResult = pdfArray1.Equals((object?)null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has the same reference as a value")]
    public void PdfArray_Equals_SameReference_ShouldReturnTrue()
    {
        // Arrange
        IReadOnlyCollection<IPdfObject> objects1 = new List<IPdfObject> { new PdfInteger(-65), new PdfName("#ABC") };
        PdfArray<IPdfObject> pdfArray1 = objects1.ToPdfArray(); // Use the ToPdfArray extension method to initialize an PdfArray instance
        PdfArray<IPdfObject> pdfArray2 = pdfArray1; // Use the ToPdfArray extension method to initialize an PdfArray instance

        // Act
        bool actualResult = pdfArray1.Equals((object)pdfArray2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has different references as values")]
    public void PdfArray_Equals_DifferentReferences_ShouldReturnFalse()
    {
        // Arrange
        IReadOnlyCollection<IPdfObject> objects1 = new List<IPdfObject> { new PdfInteger(-65), new PdfName("#ABC") };
        PdfArray<IPdfObject> pdfArray1 = objects1.ToPdfArray(); // Use the ToPdfArray extension method to initialize an PdfArray instance
        PdfArray<IPdfObject> pdfArray2 = objects1.ToPdfArray(); // Use the ToPdfArray extension method to initialize an PdfArray instance

        // Act
        bool actualResult = pdfArray1.Equals((object)pdfArray2);

        // Assert
        Assert.False(actualResult);
    }

    [Theory(DisplayName = "Check if Bytes property returns valid data")]
    [MemberData(nameof(PdfArrayTestDataGenerator.PdfArray_Bytes_TestCases), MemberType = typeof(PdfArrayTestDataGenerator))]
    public void PdfArray_Bytes_CheckValidity(IReadOnlyCollection<IPdfObject> value1, byte[] expectedBytes)
    {
        // Arrange
        PdfArray<IPdfObject> pdfArray1 = value1.ToPdfArray(); // Use the ToPdfArray extension method to initialize an PdfArray instance

        // Act
        ReadOnlyMemory<byte> actualBytes = pdfArray1.Bytes;

        // Assert
        Assert.True(actualBytes.Span.SequenceEqual(expectedBytes));
    }

    [Theory(DisplayName = "Check if GetHashCode method returns valid value")]
    [MemberData(nameof(PdfArrayTestDataGenerator.PdfArray_GetHashCode_TestCases), MemberType = typeof(PdfArrayTestDataGenerator))]
    public void PdfArray_GetHashCode_CheckValidity(IReadOnlyCollection<IPdfObject> value1)
    {
        // Arrange
        PdfArray<IPdfObject> pdfArray1 = new(value1);
        int expectedHashCode = HashCode.Combine(nameof(PdfArray<IPdfObject>), value1);

        // Act
        int actualHashCode = pdfArray1.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
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
        PdfArray<IPdfObject> pdfArray1 = new(value1);
        PdfArray<IPdfObject> pdfArray2 = new(value1);
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
        PdfArray<IPdfObject> pdfArray1 = new(value1);
        PdfArray<IPdfObject> pdfArray2 = new(value2);

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
        PdfArray<IPdfObject> pdfArray1 = PdfArray<IPdfObject>.Create(new PdfInteger(549));

        // Act
        int actualValueCount = pdfArray1.Value.Count;

        // Assert
        Assert.Equal(1, actualValueCount);
    }

    [Theory(DisplayName = "Check the Content property.")]
    [MemberData(nameof(PdfArrayTestDataGenerator.PdfArray_Content_TestCases), MemberType = typeof(PdfArrayTestDataGenerator))]
    public void PdfArray_Content_Check(IReadOnlyCollection<IPdfObject> inputValues, string expectedContentValue)
    {
        // Arrange
        PdfArray<IPdfObject> pdfArray1 = inputValues.ToPdfArray(); // Use the ToPdfArray extension method to initialize an PdfArray instance

        // Act
        string actualContentValue = pdfArray1.Content;
        string actualContentValue2 = pdfArray1.Content;

        // Assert
        Assert.Equal(expectedContentValue, actualContentValue);
        Assert.Equal(actualContentValue, actualContentValue2);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "TestData generator class can be in the same file")]
internal static class PdfArrayTestDataGenerator
{
    public static IEnumerable<object[]> PdfArray_ParameterizedConstructor_TestCases()
    {
        yield return new object[] { new List<IPdfObject> { new PdfInteger(549), new PdfReal(3.14f), new PdfBoolean(), new PdfName("SomeName") } };
    }

    public static IEnumerable<object[]> PdfArray_Bytes_TestCases()
    {
        yield return new object[]
        {
            new List<IPdfObject>
            {
                new PdfInteger(549),
                new PdfReal(3.14f),
                new PdfBoolean(),
                new PdfString("Ralph"),
                new PdfName("SomeName"),
            },
            new byte[] { 91, 53, 52, 57, 32, 51, 46, 49, 52, 32, 102, 97, 108, 115, 101, 32, 40, 82, 97, 108, 112, 104, 41, 32, 47, 83, 111, 109, 101, 78, 97, 109, 101, 93 },
        };
        yield return new object[]
        {
            new List<IPdfObject> { new PdfBoolean(true), new PdfArray<IPdfObject>(new List<IPdfObject> { default(PdfNull) }) }, new byte[] { 91, 116, 114, 117, 101, 32, 91, 110, 117, 108, 108, 93, 93 },
        };
    }

    public static IEnumerable<object[]> PdfArray_GetHashCode_TestCases()
    {
        yield return new object[]
        {
            new List<IPdfObject>
            {
                new PdfInteger(549),
                new PdfReal(3.14f),
                new PdfBoolean(),
                new PdfString("Ralph"),
                new PdfName("SomeName"),
            },
        };
        yield return new object[] { new List<IPdfObject> { new PdfBoolean(true), new PdfArray<IPdfObject>(new List<IPdfObject> { default(PdfNull) }) } };
    }

    public static IEnumerable<object[]> PdfArray_Content_TestCases()
    {
        yield return new object[]
        {
            new List<IPdfObject>
            {
                new PdfInteger(549),
                new PdfReal(3.14f),
                new PdfBoolean(),
                new PdfString("Ralph"),
                new PdfName("SomeName"),
            },
            "[549 3.14 false (Ralph) /SomeName]",
        };
        yield return new object[] { new List<IPdfObject> { new PdfBoolean(true), new PdfArray<IPdfObject>(new List<IPdfObject> { default(PdfNull) }) }, "[true [null]]" };
        yield return new object[] { new List<IPdfObject>(0), "[]" };
    }
}
