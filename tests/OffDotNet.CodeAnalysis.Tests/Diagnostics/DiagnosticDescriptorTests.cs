// <copyright file="DiagnosticDescriptorTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Tests.Diagnostics;

using Microsoft.Extensions.Localization;
using OffDotNet.CodeAnalysis.Diagnostics;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
public class DiagnosticDescriptorTests
{
    private const string DiagnosticId = "PDF1234";
    private const string LocalizedString = "Diagnostic Description";
    private const string ExpectedHelpLink = "https://help.local/{0}";
    private const DiagnosticSeverity ExpectedSeverity = DiagnosticSeverity.Error;
    private readonly LocalizedString _localizedString = new(LocalizedString, LocalizedString);
    private readonly DiagnosticDescriptor _descriptor;

    public DiagnosticDescriptorTests()
    {
        _descriptor = new DiagnosticDescriptor
        {
            Id = DiagnosticId,
            Title = _localizedString,
            Description = _localizedString,
            HelpLink = ExpectedHelpLink,
            DefaultSeverity = ExpectedSeverity,
        };
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should implement the {nameof(IEquatable<DiagnosticDescriptor>)} interface")]
    public void Class_ShouldImplementTheIEquatableInterface()
    {
        // Arrange

        // Act
        var actual = _descriptor;

        // Assert
        Assert.IsAssignableFrom<IEquatable<DiagnosticDescriptor>>(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(DiagnosticDescriptor.Id)} property should return the diagnostic id")]
    public void IdProperty_ShouldReturnTheDiagnosticId()
    {
        // Arrange

        // Act
        var id = _descriptor.Id;

        // Assert
        Assert.Equal(DiagnosticId, id);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(DiagnosticDescriptor.Title)} property should return the diagnostic title")]
    public void TitleProperty_ShouldReturnTheDiagnosticTitle()
    {
        // Arrange

        // Act
        var title = _descriptor.Title;

        // Assert
        Assert.Equal(LocalizedString, title);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(DiagnosticDescriptor.Description)} property should return the diagnostic description")]
    public void DescriptionProperty_ShouldReturnTheDiagnosticDescription()
    {
        // Arrange

        // Act
        var description = _descriptor.Description;

        // Assert
        Assert.Equal(LocalizedString, description);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(DiagnosticDescriptor.HelpLink)} property should return the diagnostic help link")]
    public void HelpLinkProperty_ShouldReturnTheDiagnosticHelpLink()
    {
        // Arrange

        // Act
        var helpLink = _descriptor.HelpLink;

        // Assert
        Assert.Equal(ExpectedHelpLink, helpLink);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(DiagnosticDescriptor.DefaultSeverity)} property should return the diagnostic default severity")]
    public void DefaultSeverityProperty_ShouldReturnTheDiagnosticDefaultSeverity()
    {
        // Arrange

        // Act
        var defaultSeverity = _descriptor.DefaultSeverity;

        // Assert
        Assert.Equal(ExpectedSeverity, defaultSeverity);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(DiagnosticDescriptor.CreateDescriptor)} method should return a new instance of the {nameof(DiagnosticDescriptor)} class")]
    public void CreateDescriptorMethod_ShouldReturnANewInstance()
    {
        // Arrange
        const ushort Id = 1234;
        var abstractMessageProvider = Substitute.For<AbstractMessageProvider>();

        // Act
        var actual = DiagnosticDescriptor.CreateDescriptor(Id, abstractMessageProvider);

        // Assert
        Assert.IsType<DiagnosticDescriptor>(actual);
    }
}
