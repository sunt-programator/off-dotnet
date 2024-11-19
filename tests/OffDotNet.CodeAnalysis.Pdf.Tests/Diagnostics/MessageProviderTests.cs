// <copyright file="MessageProviderTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Tests.Diagnostics;

using Configs;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OffDotNet.CodeAnalysis.Diagnostics;
using OffDotNet.CodeAnalysis.Pdf.Diagnostics;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
public class MessageProviderTests
{
    private const string HelpLink = "https://help.local/{0}";
    private readonly IStringLocalizer<MessageProvider> _localizer = Substitute.For<IStringLocalizer<MessageProvider>>();
    private readonly IOptions<DiagnosticOptions> _options = Substitute.For<IOptions<DiagnosticOptions>>();

    public MessageProviderTests()
    {
        _options.Value.Returns(new DiagnosticOptions { HelpLink = HelpLink });
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should implement the {nameof(IMessageProvider)} interface")]
    public void Class_ShouldImplementTheIMessageProviderInterface()
    {
        // Arrange

        // Act
        var actual = new MessageProvider(_localizer, _options);

        // Assert
        Assert.IsAssignableFrom<IMessageProvider>(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(MessageProvider.LanguagePrefix)} should return the 'PDF' value")]
    public void LanguagePrefix_ShouldReturnThePDFValue()
    {
        // Arrange
        const string Expected = "PDF";
        var messageProvider = new MessageProvider(_localizer, _options);

        // Act
        var actual = messageProvider.LanguagePrefix;

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(MessageProvider.GetTitle)} should return the localized title")]
    public void GetTitle_ShouldReturnTheLocalizedTitle()
    {
        // Arrange
        const string Expected = "Localized Title";
        const DiagnosticCode Code = DiagnosticCode.Unknown;
        var stringCode = Code + "_Title";

        var messageProvider = new MessageProvider(_localizer, _options);
        _localizer[stringCode].Returns(new LocalizedString(stringCode, Expected));

        // Act
        var actual = messageProvider.GetTitle((ushort)Code);

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(MessageProvider.GetDescription)} should return the localized description")]
    public void GetDescription_ShouldReturnTheLocalizedDescription()
    {
        // Arrange
        const string Expected = "Localized Description";
        const DiagnosticCode Code = DiagnosticCode.Unknown;
        var stringCode = Code + "_Description";

        var messageProvider = new MessageProvider(_localizer, _options);
        _localizer[stringCode].Returns(new LocalizedString(stringCode, Expected));

        // Act
        var actual = messageProvider.GetDescription((ushort)Code);

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(MessageProvider.GetHelpLink)} should return the help URL")]
    public void GetHelpLink_ShouldReturnTheHelpURL()
    {
        // Arrange
        const int Code = 1234;
        const string Expected = "https://help.local/PDF1234";

        _options.Value.Returns(new DiagnosticOptions { HelpLink = HelpLink });
        var messageProvider = new MessageProvider(_localizer, _options);

        // Act
        var actual = messageProvider.GetHelpLink(Code);

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(MessageProvider.GetSeverity)} should return the severity")]
    [InlineData(DiagnosticCode.Unknown, DiagnosticSeverity.Hidden)]
    public void GetSeverity_ShouldReturnTheSeverity(DiagnosticCode code, DiagnosticSeverity expected)
    {
        // Arrange
        var messageProvider = new MessageProvider(_localizer, _options);

        // Act
        var actual = messageProvider.GetSeverity((ushort)code);

        // Assert
        Assert.Equal((byte)expected, actual);
    }
}
