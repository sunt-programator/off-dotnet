// <copyright file="DiagnosticDescriptorTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Immutable;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

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
    private static readonly DiagnosticSeverity DiagnosticSeverity = DiagnosticSeverity.Error;

    private readonly DiagnosticDescriptor diagnosticDescriptor = new(Id, Title, MessageFormat, Category, DiagnosticSeverity, IsEnabledByDefault, Description, HelpLinkUri, CustomTag);

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.Id)} property should return the value passed to the constructor")]
    public void IdProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        string actualId = this.diagnosticDescriptor.Id;

        // Assert
        Assert.Equal(Id, actualId);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.Title)} property should return the value passed to the constructor")]
    public void TitleProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        LocalizableString actualTitle = this.diagnosticDescriptor.Title;

        // Assert
        Assert.Equal(Title, actualTitle);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.MessageFormat)} property should return the value passed to the constructor")]
    public void MessageFormatProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        LocalizableString actualMessageFormat = this.diagnosticDescriptor.MessageFormat;

        // Assert
        Assert.Equal(MessageFormat, actualMessageFormat);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.Category)} property should return the value passed to the constructor")]
    public void CategoryProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        string actualCategory = this.diagnosticDescriptor.Category;

        // Assert
        Assert.Equal(Category, actualCategory);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.DiagnosticSeverity)} property should return the value passed to the constructor")]
    public void DiagnosticSeverityProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        DiagnosticSeverity actualDiagnosticSeverity = this.diagnosticDescriptor.DiagnosticSeverity;

        // Assert
        Assert.Equal(DiagnosticSeverity, actualDiagnosticSeverity);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.IsEnabledByDefault)} property should return the value passed to the constructor")]
    public void IsEnabledByDefaultProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        bool actualIsEnabledByDefault = this.diagnosticDescriptor.IsEnabledByDefault;

        // Assert
        Assert.Equal(IsEnabledByDefault, actualIsEnabledByDefault);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.Description)} property should return the value passed to the constructor")]
    public void DescriptionProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        LocalizableString actualDescription = this.diagnosticDescriptor.Description;

        // Assert
        Assert.Equal(Description, actualDescription);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.HelpLinkUri)} property should return the value passed to the constructor")]
    public void HelpLinkUriProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        string actualHelpLinkUri = this.diagnosticDescriptor.HelpLinkUri;

        // Assert
        Assert.Equal(HelpLinkUri, actualHelpLinkUri);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticDescriptor.CustomTags)} property should return the value passed to the constructor")]
    public void CustomTagsProperty_MustBeSetFromConstructor()
    {
        // Arrange

        // Act
        ImmutableArray<string> actualCustomTags = this.diagnosticDescriptor.CustomTags;

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
        Assert.IsAssignableFrom<IEquatable<DiagnosticDescriptor>>(this.diagnosticDescriptor);
    }

    [Fact(DisplayName = "The Equals() method must return true")]
    public void EqualsMethod_MustReturnTrue()
    {
        // Arrange

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        bool actualEquals0 = diagnosticDescriptor1.Equals(diagnosticDescriptor1);
        bool actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        bool actualEquals2 = diagnosticDescriptor1.Equals((object?)diagnosticDescriptor2);
        bool actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        bool actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.True(actualEquals0);
        Assert.True(actualEquals1);
        Assert.True(actualEquals2);
        Assert.True(actualEquals3);
        Assert.False(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.Id)} is different")]
    public void EqualsMethod_DifferentId_MustReturnTrue()
    {
        // Arrange
        const string id1 = "PDF0001";
        const string id2 = "PDF0002";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new(id1, "2", "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new(id2, "2", "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        bool actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        bool actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        bool actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        bool actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.Title)} is different")]
    public void EqualsMethod_DifferentTitle_MustReturnTrue()
    {
        // Arrange
        const string oldValue = "x";
        const string newValue = "y";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", oldValue, "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", newValue, "3", "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        bool actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        bool actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        bool actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        bool actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.MessageFormat)} is different")]
    public void EqualsMethod_DifferentMessageFormat_MustReturnTrue()
    {
        // Arrange
        const string oldValue = "x";
        const string newValue = "y";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", oldValue, "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", newValue, "4", DiagnosticSeverity.Error, true, "5", "6", "7");
        bool actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        bool actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        bool actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        bool actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.Category)} is different")]
    public void EqualsMethod_DifferentCategory_MustReturnTrue()
    {
        // Arrange
        const string oldValue = "x";
        const string newValue = "y";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", oldValue, DiagnosticSeverity.Error, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", newValue, DiagnosticSeverity.Error, true, "5", "6", "7");
        bool actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        bool actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        bool actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        bool actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.DiagnosticSeverity)} is different")]
    public void EqualsMethod_DifferentDiagnosticSeverity_MustReturnTrue()
    {
        // Arrange
        const DiagnosticSeverity oldValue = DiagnosticSeverity.Error;
        const DiagnosticSeverity newValue = DiagnosticSeverity.Warning;

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", "4", oldValue, true, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", "4", newValue, true, "5", "6", "7");

        bool actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        bool actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        bool actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        bool actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.IsEnabledByDefault)} is different")]
    public void EqualsMethod_DifferentIsEnabledByDefault_MustReturnTrue()
    {
        // Arrange
        const bool oldValue = true;
        const bool newValue = false;

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", "4", DiagnosticSeverity.Error, oldValue, "5", "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", "4", DiagnosticSeverity.Error, newValue, "5", "6", "7");

        bool actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        bool actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        bool actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        bool actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.Description)} is different")]
    public void EqualsMethod_DifferentDescription_MustReturnTrue()
    {
        // Arrange
        const string oldValue = "x";
        const string newValue = "y";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, oldValue, "6", "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, newValue, "6", "7");
        bool actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        bool actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        bool actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        bool actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

        // Assert
        Assert.False(actualEquals1);
        Assert.False(actualEquals2);
        Assert.False(actualEquals3);
        Assert.True(actualEquals4);
    }

    [Fact(DisplayName = $"The Equals() method must return false if the {nameof(DiagnosticDescriptor.HelpLinkUri)} is different")]
    public void EqualsMethod_DifferentHelpLinkUri_MustReturnTrue()
    {
        // Arrange
        const string oldValue = "x";
        const string newValue = "y";

        // Act
        DiagnosticDescriptor diagnosticDescriptor1 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, "5", oldValue, "7");
        DiagnosticDescriptor diagnosticDescriptor2 = new("1", "2", "3", "4", DiagnosticSeverity.Error, true, "5", newValue, "7");
        bool actualEquals1 = diagnosticDescriptor1.Equals(diagnosticDescriptor2);
        bool actualEquals2 = diagnosticDescriptor1.Equals((object?)null);
        bool actualEquals3 = diagnosticDescriptor1 == diagnosticDescriptor2;
        bool actualEquals4 = diagnosticDescriptor1 != diagnosticDescriptor2;

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
        int actualHashCode = this.diagnosticDescriptor.GetHashCode();

        // Assert
        Assert.True(actualHashCode != 0);
    }
}
