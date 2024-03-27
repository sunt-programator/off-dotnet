// <copyright file="DiagnosticDescriptorTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

public class DiagnosticDescriptorTests
{
    private const string Id = "PDF0001";
    private const string Title = "The PDF file is invalid.";
    private const string MessageFormat = "PDF Error '{0}'";
    private const string Category = "Naming";
    private const bool IsEnabledByDefault = true;
    private const string Description = "Some longer description of the diagnostic.";
    private const string HelpLinkUri = "https://github.com/sunt-programator/off-dotnet";
    private const string CustomTag = "CustomTag";
    private static readonly DiagnosticSeverity s_diagnosticSeverity = DiagnosticSeverity.Error;

    private readonly DiagnosticDescriptor _diagnosticDescriptor = new(Id, Title, MessageFormat, Category, s_diagnosticSeverity, IsEnabledByDefault, Description, HelpLinkUri, CustomTag);

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.Id)} property should return the value passed to the constructor")]
    public void IdProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualId = _diagnosticDescriptor.Id;

        // Assert
        Assert.Equal(Id, actualId);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.Title)} property should return the value passed to the constructor")]
    public void TitleProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualTitle = _diagnosticDescriptor.Title;

        // Assert
        Assert.Equal(Title, actualTitle);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.MessageFormat)} property should return the value passed to the constructor")]
    public void MessageFormatProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualMessageFormat = _diagnosticDescriptor.MessageFormat;

        // Assert
        Assert.Equal(MessageFormat, actualMessageFormat);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.Category)} property should return the value passed to the constructor")]
    public void CategoryProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualCategory = _diagnosticDescriptor.Category;

        // Assert
        Assert.Equal(Category, actualCategory);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.DefaultSeverity)} property should return the value passed to the constructor")]
    public void DiagnosticSeverityProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualDiagnosticSeverity = _diagnosticDescriptor.DefaultSeverity;

        // Assert
        Assert.Equal(s_diagnosticSeverity, actualDiagnosticSeverity);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.IsEnabledByDefault)} property should return the value passed to the constructor")]
    public void IsEnabledByDefaultProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualIsEnabledByDefault = _diagnosticDescriptor.IsEnabledByDefault;

        // Assert
        Assert.Equal(IsEnabledByDefault, actualIsEnabledByDefault);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.Description)} property should return the value passed to the constructor")]
    public void DescriptionProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualDescription = _diagnosticDescriptor.Description;

        // Assert
        Assert.Equal(Description, actualDescription);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.HelpLinkUri)} property should return the value passed to the constructor")]
    public void HelpLinkUriProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualHelpLinkUri = _diagnosticDescriptor.HelpLinkUri;

        // Assert
        Assert.Equal(HelpLinkUri, actualHelpLinkUri);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.CustomTags)} property should return the value passed to the constructor")]
    public void CustomTagsProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        var actualCustomTags = _diagnosticDescriptor.CustomTags;

        // Assert
        Assert.Single(actualCustomTags);
        Assert.Equal(CustomTag, actualCustomTags[0]);
    }

    [Fact(DisplayName = $"The class must implement the {nameof(IEquatable<DiagnosticDescriptor>)} interface")]
    public void Class_MustImplementIEquatableInterface()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsAssignableFrom<IEquatable<DiagnosticDescriptor>>(_diagnosticDescriptor);
    }

    [Fact(DisplayName = "The Equals() method must return true")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        var actualEquals0 = diagnosticDescriptor1.Equals(diagnosticDescriptor1); // skipcq: CS-W1086
        var actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        var actualEquals2 = diagnosticDescriptor1.Equals((object?)diagnosticDescriptor2);
        var actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        var actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.True(actualEquals0);
        Assert.True(actualEquals1);
        Assert.True(actualEquals2);
        Assert.True(actualEquals3);
        Assert.False(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.Id)} is different")]
    public void EqualsMethod_DifferentId_MustReturnFalse()
    {
        // Arrange
        const string Id1 = "PDF0001";
        const string Id2 = "PDF0002";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new(Id1, "2", "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new(Id2, "2", "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        var actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        var actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        var actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        var actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.Title)} is different")]
    public void EqualsMethod_DifferentTitle_MustReturnFalse()
    {
        // Arrange
        const string OldValue = "x";
        const string NewValue = "y";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", OldValue, "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", NewValue, "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        var actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        var actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        var actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        var actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.MessageFormat)} is different")]
    public void EqualsMethod_DifferentMessageFormat_MustReturnFalse()
    {
        // Arrange
        const string OldValue = "x";
        const string NewValue = "y";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", OldValue, "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", NewValue, "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        var actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        var actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        var actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        var actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.Category)} is different")]
    public void EqualsMethod_DifferentCategory_MustReturnFalse()
    {
        // Arrange
        const string OldValue = "x";
        const string NewValue = "y";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", OldValue, DiagnosticSeverity.Error, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", NewValue, DiagnosticSeverity.Error, true, "5", "6", "7");
        var actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        var actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        var actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        var actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.DefaultSeverity)} is different")]
    public void EqualsMethod_DifferentDiagnosticSeverity_MustReturnFalse()
    {
        // Arrange
        const DiagnosticSeverity OldValue = DiagnosticSeverity.Error;
        const DiagnosticSeverity NewValue = DiagnosticSeverity.Warning;

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", "4", OldValue, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", "4", NewValue, true, "5", "6", "7");

        var actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        var actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        var actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        var actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.IsEnabledByDefault)} is different")]
    public void EqualsMethod_DifferentIsEnabledByDefault_MustReturnFalse()
    {
        // Arrange
        const bool OldValue = true;
        const bool NewValue = false;

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", "4", DiagnosticSeverity.Error, OldValue, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", "4", DiagnosticSeverity.Error, NewValue, "5", "6", "7");

        var actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        var actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        var actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        var actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.Description)} is different")]
    public void EqualsMethod_DifferentDescription_MustReturnFalse()
    {
        // Arrange
        const string OldValue = "x";
        const string NewValue = "y";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, OldValue, "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, NewValue, "6", "7");
        var actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        var actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        var actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        var actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.HelpLinkUri)} is different")]
    public void EqualsMethod_DifferentHelpLinkUri_MustReturnFalse()
    {
        // Arrange
        const string OldValue = "x";
        const string NewValue = "y";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, "5", OldValue, "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, "5", NewValue, "7");
        var actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        var actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        var actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        var actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = "The GetHashCode() method must not be zero")]
    public void GetHashCodeMethod_MustNotBeZero()
    {
        // Arrange

        // Act
        var actualHashCode = _diagnosticDescriptor.GetHashCode();

        // Assert
        Assert.NotEqual(0, actualHashCode);
    }
}
