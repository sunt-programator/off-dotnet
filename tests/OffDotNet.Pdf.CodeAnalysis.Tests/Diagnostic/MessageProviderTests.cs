// <copyright file="MessageProviderTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Globalization;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

public class MessageProviderTests
{
    [Fact(DisplayName = $"The {nameof(MessageProvider.CodePrefix)} property must return PDF")]
    public void CodePrefixProperty_MustReturnPDF()
    {
        // Arrange
        const string expectedCodePrefix = "PDF";

        // Act
        string actualCodePrefix = MessageProvider.Instance.CodePrefix;

        // Assert
        Assert.Equal(expectedCodePrefix, actualCodePrefix);
    }

    [Fact(DisplayName = "The GetTitle() method must return the title from the resource string.")]
    public void GetTitleMethod_MustReturnFromResourceString()
    {
        // Arrange
        const string title = "The PDF file is invalid";

        // Act
        string actualTitle = MessageProvider.Instance.GetTitle(DiagnosticCode.ERR_InvalidPDF).ToString(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(title, actualTitle);
    }

    [Fact(DisplayName = "The GetDescription() method must return the title from the resource string.")]
    public void GetDescriptionMethod_MustReturnFromResourceString()
    {
        // Arrange
        const string description = "Some detailed description about this error";

        // Act
        string actualDescription = MessageProvider.Instance.GetDescription(DiagnosticCode.ERR_InvalidPDF).ToString(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(description, actualDescription);
    }

    [Fact(DisplayName = "The GetSeverity() method must return the severity based on the diagnostic code.")]
    public void GetSeverityMethod_MustComputeFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticSeverity severity = DiagnosticSeverity.Error;

        // Act
        DiagnosticSeverity actualSeverity = MessageProvider.Instance.GetSeverity(DiagnosticCode.ERR_InvalidPDF);

        // Assert
        Assert.Equal(severity, actualSeverity);
    }

    [Fact(DisplayName = "The GetMessage() method must return the message from the resource string.")]
    public void GetMessageMethod_MustComputeFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        const string message = "The PDF file is invalid";

        // Act
        string actualMessage = MessageProvider.Instance.GetMessage(code).ToString(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(message, actualMessage);
    }

    [Fact(DisplayName = "The GetMessage() method must return the message from the resource string.")]
    public void GetHelpLinkMethod_MustComputeFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        const int codeNumber = (int)code;
        string helpLink = $"https://github.com/search?q=repo%3Asunt-programator%2Foff-dotnet%20PDF{codeNumber:0000}&type=code";

        // Act
        string actualHelpLink = MessageProvider.Instance.GetHelpLink(code);

        // Assert
        Assert.Equal(helpLink, actualHelpLink);
    }

    [Theory(DisplayName = "The GetCategory() method must return the message from the resource string.")]
    [InlineData(DiagnosticCode.ERR_InvalidPDF, "Syntax")]
    public void GetCategoryMethod_MustComputeFromDiagnosticCode(DiagnosticCode code, string category)
    {
        // Arrange

        // Act
        string actualCategory = MessageProvider.Instance.GetCategory(code);

        // Assert
        Assert.Equal(category, actualCategory);
    }

    [Fact(DisplayName = "The GetIsEnabledByDefault() method must return true.")]
    public void GetIsEnabledByDefaultMethod_MustReturnTrue()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;

        // Act
        bool actualIsEnabledByDefault = MessageProvider.Instance.GetIsEnabledByDefault(code);

        // Assert
        Assert.True(actualIsEnabledByDefault);
    }

    [Fact(DisplayName = "The LoadMessage() method must return correct message.")]
    public void LoadMessageMethod_MustReturnCorrectMessage()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        const string message = "The PDF file is invalid";

        // Act
        string actualMessage = MessageProvider.Instance.LoadMessage(code, CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(message, actualMessage);
    }

    [Theory(DisplayName = "The GetMessagePrefix() method must return correct prefix.")]
    [InlineData("PDF0001", DiagnosticSeverity.Error, false, "error PDF0001")]
    [InlineData("PDF0001", DiagnosticSeverity.Warning, false, "warning PDF0001")]
    [InlineData("PDF0001", DiagnosticSeverity.Warning, true, "error PDF0001")]
    [InlineData("PDF0001", DiagnosticSeverity.Info, false, "warning PDF0001")]
    [InlineData("PDF0001", DiagnosticSeverity.Hidden, false, "warning PDF0001")]
    public void GetMessagePrefixMethod_ErrorSeverity_MustReturnErrorAndCode(string code, DiagnosticSeverity severity, bool isWarningAsError, string prefix)
    {
        // Arrange

        // Act
        string actualPrefix = MessageProvider.Instance.GetMessagePrefix(code, severity, isWarningAsError, CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(prefix, actualPrefix);
    }
}
