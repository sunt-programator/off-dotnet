// <copyright file="DiagnosticInfoTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Diagnostic;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;

public class DiagnosticInfoTests
{
    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.Code)} property must return the code from the constructor")]
    public void CodeProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        IMessageProvider messageProvider = MessageProvider.Instance;

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code);
        DiagnosticCode actualCode = diagnosticInfo.Code;

        // Assert
        Assert.Equal(code, actualCode);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.DefaultSeverity)} property must be computed from the {nameof(DiagnosticInfo.Code)} property")]
    public void DefaultSeverityProperty_MustBeComputedFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticSeverity severity = DiagnosticSeverity.Info;

        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(code).Returns(severity);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code);
        DiagnosticSeverity actualSeverity = diagnosticInfo.DefaultSeverity;

        // Assert
        Assert.Equal(severity, actualSeverity);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.EffectiveSeverity)} property must be computed from the {nameof(DiagnosticInfo.Code)} property")]
    public void EffectiveSeverityProperty_MustBeComputedFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticSeverity severity = DiagnosticSeverity.Info;

        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(code).Returns(severity);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code);
        DiagnosticSeverity actualSeverity = diagnosticInfo.EffectiveSeverity;

        // Assert
        Assert.Equal(severity, actualSeverity);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.Descriptor)} property must be computed from the {nameof(DiagnosticInfo.Code)} property")]
    public void DescriptorProperty_MustBeComputedFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        DiagnosticDescriptor descriptor = new("PDF0001", "Title", "Format", "Category", DiagnosticSeverity.Info, true, "Empty", "HelpLink", "Compiler", "Telemetry");

        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(code).Returns(descriptor.DefaultSeverity);
        messageProvider.GetTitle(code).Returns(descriptor.Title);
        messageProvider.GetDescription(code).Returns(descriptor.Description);
        messageProvider.GetMessage(code).Returns(descriptor.MessageFormat);
        messageProvider.GetHelpLink(code).Returns(descriptor.HelpLinkUri);
        messageProvider.GetCategory(code).Returns(descriptor.Category);
        messageProvider.GetIsEnabledByDefault(code).Returns(descriptor.IsEnabledByDefault);
        messageProvider.GetIdForErrorCode(code).Returns(descriptor.Id);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code);
        DiagnosticDescriptor actualDescriptor = diagnosticInfo.Descriptor;

        // Assert
        Assert.Equal(descriptor, actualDescriptor);
    }

    [Fact(DisplayName = $"The class must implement the {nameof(IFormattable)} interface")]
    public void Class_MustImplementIFormattableInterface()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code);

        // Assert
        Assert.IsAssignableFrom<IFormattable>(diagnosticInfo);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.MessageIdentifier)} property must be computed from the {nameof(DiagnosticInfo.Code)} property")]
    public void MessageIdentifierProperty_MustBeComputedFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        const string identifier = "PDF0001";

        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetIdForErrorCode(code).Returns(identifier);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code);
        string actualMessageIdentifier = diagnosticInfo.MessageIdentifier;

        // Assert
        Assert.Equal(identifier, actualMessageIdentifier);
    }

    [Fact(DisplayName =
        $"The {nameof(DiagnosticInfo.MessageIdentifier)} property must return true when {nameof(DiagnosticInfo.DefaultSeverity)}={nameof(DiagnosticSeverity.Warning)} and {nameof(DiagnosticInfo.EffectiveSeverity)}={nameof(DiagnosticSeverity.Error)}")]
    public void IsWarningAsErrorProperty_MustBeTrueWhenDefaultSeverityIsWarningAndEffectiveSeverityIsError()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticSeverity severity = DiagnosticSeverity.Warning;
        const bool isWarningAsError = true;

        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(code).Returns(severity);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code, isWarningAsError);
        bool actualIsWarningAsError = diagnosticInfo.IsWarningAsError;

        // Assert
        Assert.True(actualIsWarningAsError);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.MessageIdentifier)} property must return false")]
    public void IsWarningAsErrorProperty_MustBeFalseInOtherCases()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticSeverity severity = DiagnosticSeverity.Warning;
        const bool isWarningAsError = false;

        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(code).Returns(severity);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code, isWarningAsError);
        bool actualIsWarningAsError = diagnosticInfo.IsWarningAsError;

        // Assert
        Assert.False(actualIsWarningAsError);
    }

    [Fact(DisplayName = "The ToString method must be computed from message provider")]
    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Reviewed.")]
    public void ToStringMethod_MustBeComputedFromMessageProvider()
    {
        // Arrange
        const string message = "error PDF0001: Invalid PDF file";
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        DiagnosticDescriptor descriptor = new("PDF0001", "Title", "Format", "Category", DiagnosticSeverity.Info, true, "Empty", "HelpLink", "Compiler", "Telemetry");

        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(code).Returns(descriptor.DefaultSeverity);
        messageProvider.GetMessage(code).Returns(descriptor.MessageFormat);
        messageProvider.GetIdForErrorCode(code).Returns(descriptor.Id);
        messageProvider.GetMessagePrefix(descriptor.Id, descriptor.DefaultSeverity, false, CultureInfo.InvariantCulture).Returns("error PDF0001");
        messageProvider.LoadMessage(code, CultureInfo.InvariantCulture).Returns("Invalid PDF file");

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code);
        string actualString1 = diagnosticInfo.ToString(null);
        string actualString2 = diagnosticInfo.ToString();

        // Assert
        Assert.Equal(message, actualString1);
        Assert.Equal(message, actualString2);
    }

    [Fact(DisplayName = "The GetMessage method must return empty string when the message is null")]
    public void GetMessageMethod_NullMessage_MustReturnStringEmpty()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.LoadMessage(code, CultureInfo.InvariantCulture).Returns(string.Empty);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code);
        string actualMessage = diagnosticInfo.GetMessage(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(string.Empty, actualMessage);
    }

    [Fact(DisplayName = "The GetMessage method must get the message from the message provider")]
    public void GetMessageMethod_MustReturnFromMessageProvider()
    {
        // Arrange
        const string message = "Invalid PDF file";
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.LoadMessage(code, CultureInfo.InvariantCulture).Returns(message);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code);
        string actualMessage = diagnosticInfo.GetMessage(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(message, actualMessage);
    }

    [Fact(DisplayName = "The GetMessage method with arguments must format the string from the message provider")]
    public void GetMessageMethod_WithArguments_MustFormatStringFromMessageProvider()
    {
        // Arrange
        const string arg1 = "arg1";
        const int arg2 = 2;
        const string messageTemplate = "Invalid PDF file: {0}, {1}";
        string message = $"Invalid PDF file: {arg1}, {arg2}";
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;

        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.LoadMessage(code, CultureInfo.InvariantCulture).Returns(messageTemplate);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code, arg1, arg2);
        string actualMessage = diagnosticInfo.GetMessage(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(message, actualMessage);
    }

    [Fact(DisplayName = "The GetMessage method with nested argument must format the string from the message provider")]
    public void GetMessageMethod_WithNestedArgument_MustFormatStringFromMessageProvider()
    {
        // Arrange
        const string messageTemplate = "Invalid PDF file: {0}";
        const string message = "Invalid PDF file: Invalid PDF file: {0}";
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;

        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.LoadMessage(code, CultureInfo.InvariantCulture).Returns(messageTemplate);

        DiagnosticInfo nestedDiagnosticInfo = new(messageProvider, code);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code, nestedDiagnosticInfo);
        string actualMessage = diagnosticInfo.GetMessage(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(message, actualMessage);
    }

    [Fact(DisplayName = "The GetHashCode method must return the diagnostic code")]
    public void GetHashCodeMethod_MustComputeDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;

        int expectedHashCode = HashCode.Combine(code);
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code);
        int actualHashCode = diagnosticInfo.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "The GetHashCode method with arguments must compute the diagnostic code and arguments")]
    public void GetHashCodeMethod_WithArguments_MustComputeDiagnosticCodeAndArguments()
    {
        // Arrange
        const string arg1 = "arg1";
        const int arg2 = 2;
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;

        int expectedHashCode = HashCode.Combine(code, arg1, arg2);
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, code, arg1, arg2);
        int actualHashCode = diagnosticInfo.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "The Equals method with null object must return false")]
    public void EqualsMethod_Null_MustReturnFalse()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, code);
        bool actualEquals = diagnosticInfo1.Equals(null);

        // Assert
        Assert.False(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with different codes must return false")]
    public void EqualsMethod_DifferentCodes_MustReturnFalse()
    {
        // Arrange
        const DiagnosticCode code1 = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticCode code2 = DiagnosticCode.ERR_InvalidPDFVersion;
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, code1);
        DiagnosticInfo diagnosticInfo2 = new(messageProvider, code2);
        bool actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.False(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with different types must return false")]
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global", Justification = "Reviewed.")]
    public void EqualsMethod_DifferentTypes_MustReturnFalse()
    {
        // Arrange
        const DiagnosticCode code1 = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticCode code2 = DiagnosticCode.ERR_InvalidPDFVersion;
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, code1);
        object diagnosticInfo2 = new DiagnosticInfo(messageProvider, code2);
        bool actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.False(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with different number of arguments must return false")]
    public void EqualsMethod_DifferentNumberOfArguments_MustReturnFalse()
    {
        // Arrange
        const string arg1 = "arg1";
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo diagnosticInfo2 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, arg1);
        bool actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.False(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with different arguments must return false")]
    public void EqualsMethod_DifferentArguments_MustReturnFalse()
    {
        // Arrange
        const string arg1 = "arg1";
        const string arg2 = "arg2";
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, arg1);
        DiagnosticInfo diagnosticInfo2 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, arg2);
        bool actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.False(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with same codes must return true")]
    public void EqualsMethod_SameCodes_MustReturnTrue()
    {
        // Arrange
        const DiagnosticCode code = DiagnosticCode.ERR_InvalidPDF;
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, code);
        DiagnosticInfo diagnosticInfo2 = new(messageProvider, code);
        bool actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.True(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with same arguments must return false")]
    public void EqualsMethod_SameArguments_MustReturnFalse()
    {
        // Arrange
        const string arg1 = "arg1";
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, arg1);
        DiagnosticInfo diagnosticInfo2 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, arg1);
        bool actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.True(actualEquals);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.Arguments)} property must be set from the constructor")]
    public void ArgumentsProperty_MustBeComputedFromConstructor()
    {
        // Arrange
        const string arg1 = "arg1";
        const string arg2 = "arg2";
        object[] expectedArguments = { arg1, arg2 };
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, arg1, arg2);
        object[] actualArguments = diagnosticInfo.Arguments;

        // Assert
        Assert.Equal(expectedArguments, actualArguments);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.MessageProvider)} property must be set from the constructor")]
    public void MessageProviderProperty_MustBeComputedFromConstructor()
    {
        // Arrange
        IMessageProvider messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, DiagnosticCode.ERR_InvalidPDF);
        IMessageProvider actualMessageProvider = diagnosticInfo.MessageProvider;

        // Assert
        Assert.Equal(messageProvider, actualMessageProvider);
    }
}
