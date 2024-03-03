// <copyright file="PdfDictionaryTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.Primitives;

using Common;
using OffDotNet.Pdf.Core.Primitives;

public class PdfDictionaryTests
{
    [Fact(DisplayName = "Create an instance using parametrized constructor and check the Value property")]
    public void PdfDictionary_ParameterizedConstructor_CheckValue()
    {
        // Arrange
        Dictionary<PdfName, IPdfObject> inputValue = new() { { new PdfName("Name1"), default(PdfNull) } };
        IPdfDictionary<IPdfObject> pdfDictionary = inputValue.ToPdfDictionary();

        // Act

        // Assert
        Assert.Equal(inputValue, pdfDictionary.Value); // Checks if reference is equal
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfDictionary_Equals_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        IPdfDictionary<IPdfObject> pdfDictionary1 = new KeyValuePair<PdfName, IPdfObject>(new PdfName("Name1"), new PdfBoolean()).ToPdfDictionary();

        // Act
        bool actualResult = pdfDictionary1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method if the argument is null")]
    public void PdfDictionary_Equals2_NullArgument_ShouldReturnFalse()
    {
        // Arrange
        IPdfDictionary<IPdfObject> pdfDictionary1 = new KeyValuePair<PdfName, IPdfObject>(new PdfName("Name1"), new PdfString("Value1")).ToPdfDictionary();

        // Act
        bool actualResult = pdfDictionary1.Equals(null);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has the same reference as a value")]
    public void PdfDictionary_Equals_SameReference_ShouldReturnTrue()
    {
        // Arrange
        Dictionary<PdfName, IPdfObject> objects1 = new() { { new PdfName("Name1"), default(PdfNull) } };
        IPdfDictionary<IPdfObject> pdfDictionary1 = objects1.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance
        IPdfDictionary<IPdfObject> pdfDictionary2 = pdfDictionary1; // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance

        // Act
        bool actualResult = pdfDictionary1.Equals(pdfDictionary2);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = "Check Equals method that has different references as values")]
    public void PdfDictionary_Equals_DifferentReferences_ShouldReturnFalse()
    {
        // Arrange
        Dictionary<PdfName, IPdfObject> objects1 = new() { { new PdfName("Name1"), default(PdfNull) } };
        IPdfDictionary<IPdfObject> pdfDictionary1 = objects1.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance
        IPdfDictionary<IPdfObject> pdfDictionary2 = objects1.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance

        // Act
        bool actualResult = pdfDictionary1.Equals(pdfDictionary2);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "Check if Bytes property returns valid data")]
    public void PdfDictionary_Bytes_CheckValidity()
    {
        // Arrange
        byte[] expectedBytes =
        [
            0x3C,
            0x3C,
            0x2F,
            0x54,
            0x79,
            0x70,
            0x65,
            0x20,
            0x2F,
            0x45,
            0x78,
            0x61,
            0x6D,
            0x70,
            0x6C,
            0x65,
            0x20,
            0x2F,
            0x53,
            0x75,
            0x62,
            0x54,
            0x79,
            0x70,
            0x65,
            0x20,
            0x2F,
            0x44,
            0x69,
            0x63,
            0x74,
            0x69,
            0x6F,
            0x6E,
            0x61,
            0x72,
            0x79,
            0x45,
            0x78,
            0x61,
            0x6D,
            0x70,
            0x6C,
            0x65,
            0x20,
            0x2F,
            0x56,
            0x65,
            0x72,
            0x73,
            0x69,
            0x6F,
            0x6E,
            0x20,
            0x30,
            0x2E,
            0x30,
            0x31,
            0x20,
            0x2F,
            0x49,
            0x6E,
            0x74,
            0x65,
            0x67,
            0x65,
            0x72,
            0x49,
            0x74,
            0x65,
            0x6D,
            0x20,
            0x31,
            0x32,
            0x20,
            0x2F,
            0x53,
            0x74,
            0x72,
            0x69,
            0x6E,
            0x67,
            0x49,
            0x74,
            0x65,
            0x6D,
            0x20,
            0x28,
            0x61,
            0x20,
            0x73,
            0x74,
            0x72,
            0x69,
            0x6E,
            0x67,
            0x29,
            0x20,
            0x2F,
            0x53,
            0x75,
            0x62,
            0x44,
            0x69,
            0x63,
            0x74,
            0x69,
            0x6F,
            0x6E,
            0x61,
            0x72,
            0x79,
            0x20,
            0x3C,
            0x3C,
            0x2F,
            0x49,
            0x74,
            0x65,
            0x6D,
            0x31,
            0x20,
            0x30,
            0x2E,
            0x34,
            0x20,
            0x2F,
            0x49,
            0x74,
            0x65,
            0x6D,
            0x32,
            0x20,
            0x74,
            0x72,
            0x75,
            0x65,
            0x20,
            0x2F,
            0x4C,
            0x61,
            0x73,
            0x74,
            0x49,
            0x74,
            0x65,
            0x6D,
            0x20,
            0x28,
            0x6E,
            0x6F,
            0x74,
            0x21,
            0x29,
            0x20,
            0x2F,
            0x56,
            0x65,
            0x72,
            0x79,
            0x4C,
            0x61,
            0x73,
            0x74,
            0x49,
            0x74,
            0x65,
            0x6D,
            0x20,
            0x28,
            0x4F,
            0x4B,
            0x29,
            0x3E,
            0x3E,
            0x3E,
            0x3E
        ];

        var inputValues = new Dictionary<PdfName, IPdfObject>
        {
            { "Type", new PdfName("Example") },
            { "SubType", new PdfName("DictionaryExample") },
            { "Version", new PdfReal(0.01f) },
            { "IntegerItem", new PdfInteger(12) },
            { "StringItem", new PdfString("a string") },
            {
                "SubDictionary",
                new Dictionary<PdfName, IPdfObject>
                {
                    { "Item1", new PdfReal(0.4f) },
                    { "Item2", new PdfBoolean(true) },
                    { "LastItem", new PdfString("not!") },
                    { "VeryLastItem", new PdfString("OK") },
                }.ToPdfDictionary()
            },
        };

        IPdfDictionary<IPdfObject> pdfDictionary1 = inputValues.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance

        // Act
        byte[] actualBytes = pdfDictionary1.Bytes.ToArray();

        // Assert
        Assert.True(actualBytes.SequenceEqual(expectedBytes));
    }

    [Fact(DisplayName = "Compare the hash codes of two dictionary objects.")]
    public void PdfDictionary_GetHashCode_CompareHashes_ShouldBeEqual()
    {
        // Arrange
        Dictionary<PdfName, IPdfObject> value1 = new() { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } };
        IPdfDictionary<IPdfObject> pdfDictionary1 = new PdfDictionary<IPdfObject>(value1);
        IPdfDictionary<IPdfObject> pdfDictionary2 = new PdfDictionary<IPdfObject>(value1);
        int expectedHashCode = HashCode.Combine(nameof(PdfDictionary<IPdfObject>), value1);

        // Act
        int actualHashCode1 = pdfDictionary1.GetHashCode();
        int actualHashCode2 = pdfDictionary2.GetHashCode();
        bool areHashCodeEquals = actualHashCode1 == actualHashCode2;

        // Assert
        Assert.True(areHashCodeEquals);
        Assert.Equal(expectedHashCode, actualHashCode1);
        Assert.Equal(expectedHashCode, actualHashCode2);
    }

    [Fact(DisplayName = "Compare the hash codes of two dictionary objects.")]
    public void PdfDictionary_GetHashCode_CompareHashes_ShouldNotBeEqual()
    {
        // Arrange
        Dictionary<PdfName, IPdfObject> value1 = new() { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } };
        Dictionary<PdfName, IPdfObject> value2 = new() { { new PdfName("Type"), new PdfName("Example") }, { new PdfName("SubType"), new PdfName("DictionaryExample") } };
        IPdfDictionary<IPdfObject> pdfDictionary1 = new PdfDictionary<IPdfObject>(value1);
        IPdfDictionary<IPdfObject> pdfDictionary2 = new PdfDictionary<IPdfObject>(value2);

        // Act
        int actualHashCode1 = pdfDictionary1.GetHashCode();
        int actualHashCode2 = pdfDictionary2.GetHashCode();
        bool areHashCodeEquals = actualHashCode1 == actualHashCode2;

        // Assert
        Assert.False(areHashCodeEquals);
    }

    [Fact(DisplayName = "Check the Value property.")]
    public void PdfDictionary_Value_Count_ShouldReturn1()
    {
        // Arrange
        IPdfDictionary<IPdfObject> pdfDictionary1 = new KeyValuePair<PdfName, IPdfObject>(new PdfName("Type"), new PdfName("Example")).ToPdfDictionary();

        // Act
        int actualValueCount = pdfDictionary1.Value.Count;

        // Assert
        Assert.Equal(1, actualValueCount);
    }

    [Fact(DisplayName = "Check the Content property.")]
    public void PdfDictionary_Content_Check()
    {
        // Arrange
        const string expectedContentValue =
            "<</Type /Example /SubType /DictionaryExample /Version 0.01 /IntegerItem 12 /StringItem (a string) /SubDictionary <</Item1 0.4 /Item2 true /LastItem (not!) /VeryLastItem (OK)>>>>";

        var inputValues = new Dictionary<PdfName, IPdfObject>
        {
            { "Type", new PdfName("Example") },
            { "SubType", new PdfName("DictionaryExample") },
            { "Version", new PdfReal(0.01f) },
            { "IntegerItem", new PdfInteger(12) },
            { "StringItem", new PdfString("a string") },
            {
                "SubDictionary",
                new Dictionary<PdfName, IPdfObject>
                {
                    { "Item1", new PdfReal(0.4f) },
                    { "Item2", new PdfBoolean(true) },
                    { "LastItem", new PdfString("not!") },
                    { "VeryLastItem", new PdfString("OK") },
                }.ToPdfDictionary()
            },
        };

        IPdfDictionary<IPdfObject> pdfDictionary1 = inputValues.ToPdfDictionary(); // Use the ToPdfDictionary extension method to initialize an PdfDictionary instance

        // Act
        string actualContentValue = pdfDictionary1.Content;
        string actualContentValue2 = pdfDictionary1.Content;

        // Assert
        Assert.Equal(expectedContentValue, actualContentValue);
        Assert.Equal(actualContentValue, actualContentValue2);
    }
}
