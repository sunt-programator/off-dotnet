using System;
using System.Collections.Generic;
using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;
using Xunit;

namespace Off.Net.Pdf.Core.Tests.Primitives;

public class PdfDictionaryTests
{
    [Theory(DisplayName = "Create an instance using parametrized constructor and check the Value property")]
    [MemberData(nameof(TestDataGenerator.PdfDictionary_ParameterizedConstructor_TestCases), MemberType = typeof(TestDataGenerator))]
    public void PdfDictionary_ParameterizedConstructor_CheckValue(Dictionary<PdfName, IPdfObject> inputValue)
    {
        // Arrange
        PdfDictionary pdfArray = PdfDictionary.CreateRange(inputValue); // Use the CreateRange static method to initialize an PdfDictionary instance

        // Act

        // Assert
        Assert.Equal(inputValue, pdfArray.Value); // Checks if reference is equal
    }

    [Theory(DisplayName = "Check the length of the PDF dictionary primitive")]
    [MemberData(nameof(TestDataGenerator.PdfDictionary_Length_TestCases), MemberType = typeof(TestDataGenerator))]
    public void PdfDictionary_Length_CheckValue(Dictionary<PdfName, IPdfObject> inputValue, int expectedLength)
    {
        // Arrange
        PdfDictionary pdfArray = inputValue.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance

        // Act
        int actualLength = pdfArray.Length;

        // Assert
        Assert.Equal(expectedLength, actualLength);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfDictionary_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfDictionary pdfArray1 = new KeyValuePair<PdfName, IPdfObject>(new PdfName("Name1"), new PdfBoolean()).ToPdfDictionary();

        // Act
        bool actualResult = pdfArray1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfDictionary_Equals2_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        PdfDictionary pdfArray1 = PdfDictionary.Create(new KeyValuePair<PdfName, IPdfObject>(new PdfName("Name1"), new PdfString("Value1")));

        // Act
        bool actualResult = pdfArray1.Equals((object?)null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has the same reference as a value")]
    public void PdfDictionary_Equals_SameReference_ShouldReturnTrue()
    {
        // Arrange
        Dictionary<PdfName, IPdfObject> objects1 = new Dictionary<PdfName, IPdfObject> { { new PdfName("Name1"), new PdfNull() } };
        PdfDictionary pdfArray1 = objects1.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance
        PdfDictionary pdfArray2 = pdfArray1; // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance

        // Act
        bool actualResult = pdfArray1.Equals((object)pdfArray2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has different references as values")]
    public void PdfDictionary_Equals_DifferentReferences_ShouldReturnFalse()
    {
        // Arrange
        Dictionary<PdfName, IPdfObject> objects1 = new Dictionary<PdfName, IPdfObject> { { new PdfName("Name1"), new PdfNull() } };
        PdfDictionary pdfArray1 = objects1.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance
        PdfDictionary pdfArray2 = objects1.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance

        // Act
        bool actualResult = pdfArray1.Equals((object)pdfArray2);

        // Assert
        Assert.False(actualResult);
    }

    [Theory(DisplayName = "Check if Bytes property returns valid data")]
    [MemberData(nameof(TestDataGenerator.PdfDictionary_Bytes_TestCases), MemberType = typeof(TestDataGenerator))]
    public void PdfDictionary_Bytes_CheckValidity(Dictionary<PdfName, IPdfObject> value1, byte[] expectedBytes)
    {
        // Arrange
        PdfDictionary pdfArray1 = value1.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance

        // Act
        byte[] actualBytes = pdfArray1.Bytes;

        // Assert
        Assert.Equal(expectedBytes, actualBytes);
    }

    [Theory(DisplayName = "Check if GetHashCode method returns valid value")]
    [MemberData(nameof(TestDataGenerator.PdfDictionary_GetHashCode_TestCases), MemberType = typeof(TestDataGenerator))]
    public void PdfDictionary_GetHashCode_CheckValidity(IReadOnlyDictionary<PdfName, IPdfObject> value1)
    {
        // Arrange
        PdfDictionary pdfArray1 = new PdfDictionary(value1);
        int expectedHashCode = HashCode.Combine(nameof(PdfDictionary).GetHashCode(), value1.GetHashCode());

        // Act
        int actualHashCode = pdfArray1.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "Compare the hash codes of two array objects.")]
    public void PdfDictionary_GetHashCode_CompareHashes_ShouldBeEqual()
    {
        // Arrange
        Dictionary<PdfName, IPdfObject> value1 = new Dictionary<PdfName, IPdfObject> { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } };
        PdfDictionary pdfArray1 = new PdfDictionary(value1);
        PdfDictionary pdfArray2 = new PdfDictionary(value1);
        int expectedHashCode = HashCode.Combine(nameof(PdfDictionary).GetHashCode(), value1.GetHashCode());

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
    public void PdfDictionary_GetHashCode_CompareHashes_ShouldNotBeEqual()
    {
        // Arrange
        Dictionary<PdfName, IPdfObject> value1 = new Dictionary<PdfName, IPdfObject> { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } };
        Dictionary<PdfName, IPdfObject> value2 = new Dictionary<PdfName, IPdfObject> { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } };
        PdfDictionary pdfArray1 = new PdfDictionary(value1);
        PdfDictionary pdfArray2 = new PdfDictionary(value2);

        // Act
        int actualHashCode1 = pdfArray1.GetHashCode();
        int actualHashCode2 = pdfArray2.GetHashCode();
        bool areHashCodeEquals = actualHashCode1 == actualHashCode2;

        // Assert
        Assert.False(areHashCodeEquals);
    }

    [Fact(DisplayName = "Check the Value property.")]
    public void PdfDictionary_Value_Count_ShouldReturn1()
    {
        // Arrange
        PdfDictionary pdfArray1 = PdfDictionary.Create(new KeyValuePair<PdfName, IPdfObject>(new PdfName("Type"), new PdfName("Example")));

        // Act
        int actualValueCount = pdfArray1.Value.Count;

        // Assert
        Assert.Equal(1, actualValueCount);
    }

    [Theory(DisplayName = "Check the Content property.")]
    [MemberData(nameof(TestDataGenerator.PdfDictionary_Content_TestCases), MemberType = typeof(TestDataGenerator))]
    public void PdfDictionary_Content_Check(Dictionary<PdfName, IPdfObject> inputValues, string expectedContentValue)
    {
        // Arrange
        PdfDictionary pdfArray1 = inputValues.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance

        // Act
        string actualContentValue = pdfArray1.Content;
        string actualContentValue2 = pdfArray1.Content;

        // Assert
        Assert.Equal(expectedContentValue, actualContentValue);
        Assert.Equal(actualContentValue.GetHashCode(), actualContentValue2.GetHashCode());
    }
}

public class TestDataGenerator
{
    public static IEnumerable<object[]> PdfDictionary_ParameterizedConstructor_TestCases()
    {
        yield return new object[] { new Dictionary<PdfName, IPdfObject> { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } } };
    }

    public static IEnumerable<object[]> PdfDictionary_Length_TestCases()
    {
        yield return new object[]
        {
            new Dictionary<PdfName, IPdfObject>
            {
                { new PdfName("Type"), new PdfName("Example") },
                { new PdfName("SubType"), new PdfName("DictionaryExample") },
                { new PdfName("Version"), new PdfReal(0.01f) },
                { new PdfName("IntegerItem"), new PdfInteger(12) },
                { new PdfName("StringItem"), new PdfString("a string") },
                {
                    new PdfName("Subdictionary"), PdfDictionary.CreateRange(new Dictionary<PdfName, IPdfObject>
                    {
                        { new PdfName("Item1"), new PdfReal(0.4f) },
                        { new PdfName("Item2"), new PdfBoolean(true) },
                        { new PdfName("LastItem"), new PdfString("not!") },
                        { new PdfName("VeryLastItem"), new PdfString("OK") },
                    })
                }
            },
            177
        };
        yield return new object[] { new Dictionary<PdfName, IPdfObject> { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } }, 46 };
    }

    public static IEnumerable<object[]> PdfDictionary_Bytes_TestCases()
    {
        yield return new object[]
        {
            new Dictionary<PdfName, IPdfObject>
            {
                { new PdfName("Type"), new PdfName("Example") },
                { new PdfName("SubType"), new PdfName("DictionaryExample") },
                { new PdfName("Version"), new PdfReal(0.01f) },
                { new PdfName("IntegerItem"), new PdfInteger(12) },
                { new PdfName("StringItem"), new PdfString("a string") },
                {
                    new PdfName("Subdictionary"), PdfDictionary.CreateRange(new Dictionary<PdfName, IPdfObject>
                    {
                        { new PdfName("Item1"), new PdfReal(0.4f) },
                        { new PdfName("Item2"), new PdfBoolean(true) },
                        { new PdfName("LastItem"), new PdfString("not!") },
                        { new PdfName("VeryLastItem"), new PdfString("OK") },
                    })
                }
            },
            new byte[]
            {
                60, 60, 47, 84, 121, 112, 101, 32, 47, 69, 120, 97, 109, 112, 108, 101, 32, 47, 83, 117, 98, 84, 121, 112, 101, 32, 47, 68, 105, 99, 116, 105, 111, 110, 97, 114, 121, 69, 120, 97,
                109, 112, 108, 101, 32, 47, 86, 101, 114, 115, 105, 111, 110, 32, 48, 46, 48, 49, 32, 47, 73, 110, 116, 101, 103, 101, 114, 73, 116, 101, 109, 32, 49, 50, 32, 47, 83, 116, 114,
                105, 110, 103, 73, 116, 101, 109, 32, 40, 97, 32, 115, 116, 114, 105, 110, 103, 41, 32, 47, 83, 117, 98, 100, 105, 99, 116, 105, 111, 110, 97, 114, 121, 32, 60, 60, 47, 73, 116,
                101, 109, 49, 32, 48, 46, 52, 32, 47, 73, 116, 101, 109, 50, 32, 116, 114, 117, 101, 32, 47, 76, 97, 115, 116, 73, 116, 101, 109, 32, 40, 110, 111, 116, 33, 41, 32, 47, 86, 101,
                114, 121, 76, 97, 115, 116, 73, 116, 101, 109, 32, 40, 79, 75, 41, 62, 62, 62, 62
            }
        };
        yield return new object[]
        {
            new Dictionary<PdfName, IPdfObject> { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } },
            new byte[]
            {
                60, 60, 47, 84, 121, 112, 101, 32, 47, 69, 120, 97, 109, 112, 108, 101, 32, 47, 83, 117, 98, 84, 121, 112, 101, 32, 47, 68, 105, 99, 116, 105, 111, 110, 97, 114, 121, 69, 120,
                97, 109, 112, 108, 101, 62, 62
            }
        };
    }

    public static IEnumerable<object[]> PdfDictionary_GetHashCode_TestCases()
    {
        yield return new object[] { new Dictionary<PdfName, IPdfObject> { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } } };
        yield return new object[]
        {
            new Dictionary<PdfName, IPdfObject>
            {
                { new PdfName("Type"), new PdfName("Example") },
                { new PdfName("SubType"), new PdfName("DictionaryExample") },
                { new PdfName("Version"), new PdfReal(0.01f) },
                { new PdfName("IntegerItem"), new PdfInteger(12) },
                {
                    new PdfName("Subdictionary"), PdfDictionary.CreateRange(new Dictionary<PdfName, IPdfObject>
                    {
                        { new PdfName("Item1"), new PdfReal(0.4f) },
                        { new PdfName("Item2"), new PdfBoolean(true) },
                        { new PdfName("LastItem"), new PdfString("not!") },
                        { new PdfName("VeryLastItem"), new PdfString("OK") },
                    })
                }
            }
        };
    }

    public static IEnumerable<object[]> PdfDictionary_Content_TestCases()
    {
        yield return new object[]
        {
            new Dictionary<PdfName, IPdfObject>(0),
            "<<>>"
        };
        yield return new object[]
        {
            new Dictionary<PdfName, IPdfObject> { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } },
            "<</Type /Example /SubType /DictionaryExample>>"
        };
        yield return new object[]
        {
            new Dictionary<PdfName, IPdfObject>
            {
                { new PdfName("Type"), new PdfName("Example") },
                { new PdfName("SubType"), new PdfName("DictionaryExample") },
                { new PdfName("Version"), new PdfReal(0.01f) },
                { new PdfName("IntegerItem"), new PdfInteger(12) },
                { new PdfName("StringItem"), new PdfString("a string") },
                {
                    new PdfName("Subdictionary"), PdfDictionary.CreateRange(new Dictionary<PdfName, IPdfObject>
                    {
                        { new PdfName("Item1"), new PdfReal(0.4f) },
                        { new PdfName("Item2"), new PdfBoolean(true) },
                        { new PdfName("LastItem"), new PdfString("not!") },
                        { new PdfName("VeryLastItem"), new PdfString("OK") },
                    })
                }
            },
            "<</Type /Example /SubType /DictionaryExample /Version 0.01 /IntegerItem 12 /StringItem (a string) /Subdictionary <</Item1 0.4 /Item2 true /LastItem (not!) /VeryLastItem (OK)>>>>"
        };
    }
}
