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
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        IMessageProvider messageProvider = MessageProvider.Instance;

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code);
        var actualCode = diagnosticInfo.Code;

        // Assert
        Assert.Equal(Code, actualCode);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.DefaultSeverity)} property must be computed from the {nameof(DiagnosticInfo.Code)} property")]
    public void DefaultSeverityProperty_MustBeComputedFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticSeverity Severity = DiagnosticSeverity.Info;

        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(Code).Returns(Severity);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code);
        var actualSeverity = diagnosticInfo.DefaultSeverity;

        // Assert
        Assert.Equal(Severity, actualSeverity);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.EffectiveSeverity)} property must be computed from the {nameof(DiagnosticInfo.Code)} property")]
    public void EffectiveSeverityProperty_MustBeComputedFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticSeverity Severity = DiagnosticSeverity.Info;

        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(Code).Returns(Severity);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code);
        var actualSeverity = diagnosticInfo.EffectiveSeverity;

        // Assert
        Assert.Equal(Severity, actualSeverity);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.Descriptor)} property must be computed from the {nameof(DiagnosticInfo.Code)} property")]
    public void DescriptorProperty_MustBeComputedFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        DiagnosticDescriptor descriptor = new("PDF0001", "Title", "Format", "Category", DiagnosticSeverity.Info, true, "Empty", "HelpLink", "Compiler", "Telemetry");

        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(Code).Returns(descriptor.DefaultSeverity);
        messageProvider.GetTitle(Code).Returns(descriptor.Title);
        messageProvider.GetDescription(Code).Returns(descriptor.Description);
        messageProvider.GetMessage(Code).Returns(descriptor.MessageFormat);
        messageProvider.GetHelpLink(Code).Returns(descriptor.HelpLinkUri);
        messageProvider.GetCategory(Code).Returns(descriptor.Category);
        messageProvider.GetIsEnabledByDefault(Code).Returns(descriptor.IsEnabledByDefault);
        messageProvider.GetIdForErrorCode(Code).Returns(descriptor.Id);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code);
        var actualDescriptor = diagnosticInfo.Descriptor;

        // Assert
        Assert.Equal(descriptor, actualDescriptor);
    }

    [Fact(DisplayName = $"The class must implement the {nameof(IFormattable)} interface")]
    public void Class_MustImplementIFormattableInterface()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code);

        // Assert
        Assert.IsAssignableFrom<IFormattable>(diagnosticInfo);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.MessageIdentifier)} property must be computed from the {nameof(DiagnosticInfo.Code)} property")]
    public void MessageIdentifierProperty_MustBeComputedFromDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        const string Identifier = "PDF0001";

        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetIdForErrorCode(Code).Returns(Identifier);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code);
        var actualMessageIdentifier = diagnosticInfo.MessageIdentifier;

        // Assert
        Assert.Equal(Identifier, actualMessageIdentifier);
    }

    [Fact(DisplayName =
        $"The {nameof(DiagnosticInfo.MessageIdentifier)} property must return true when {nameof(DiagnosticInfo.DefaultSeverity)}={nameof(DiagnosticSeverity.Warning)} and {nameof(DiagnosticInfo.EffectiveSeverity)}={nameof(DiagnosticSeverity.Error)}")]
    public void IsWarningAsErrorProperty_MustBeTrueWhenDefaultSeverityIsWarningAndEffectiveSeverityIsError()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticSeverity Severity = DiagnosticSeverity.Warning;
        const bool IsWarningAsError = true;

        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(Code).Returns(Severity);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code, IsWarningAsError);
        var actualIsWarningAsError = diagnosticInfo.IsWarningAsError;

        // Assert
        Assert.True(actualIsWarningAsError);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.MessageIdentifier)} property must return false")]
    public void IsWarningAsErrorProperty_MustBeFalseInOtherCases()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticSeverity Severity = DiagnosticSeverity.Warning;
        const bool IsWarningAsError = false;

        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(Code).Returns(Severity);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code, IsWarningAsError);
        var actualIsWarningAsError = diagnosticInfo.IsWarningAsError;

        // Assert
        Assert.False(actualIsWarningAsError);
    }

    [Fact(DisplayName = "The ToString method must be computed from message provider")]
    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Reviewed.")]
    public void ToStringMethod_MustBeComputedFromMessageProvider()
    {
        // Arrange
        const string Message = "error PDF0001: Invalid PDF file";
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        DiagnosticDescriptor descriptor = new("PDF0001", "Title", "Format", "Category", DiagnosticSeverity.Info, true, "Empty", "HelpLink", "Compiler", "Telemetry");

        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.GetSeverity(Code).Returns(descriptor.DefaultSeverity);
        messageProvider.GetMessage(Code).Returns(descriptor.MessageFormat);
        messageProvider.GetIdForErrorCode(Code).Returns(descriptor.Id);
        messageProvider.GetMessagePrefix(descriptor.Id, descriptor.DefaultSeverity, false, CultureInfo.InvariantCulture).Returns("error PDF0001");
        messageProvider.LoadMessage(Code, CultureInfo.InvariantCulture).Returns("Invalid PDF file");

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code);
        var actualString1 = diagnosticInfo.ToString(null);
        var actualString2 = diagnosticInfo.ToString();

        // Assert
        Assert.Equal(Message, actualString1);
        Assert.Equal(Message, actualString2);
    }

    [Fact(DisplayName = "The GetMessage method must return empty string when the message is null")]
    public void GetMessageMethod_NullMessage_MustReturnStringEmpty()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.LoadMessage(Code, CultureInfo.InvariantCulture).Returns(string.Empty);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code);
        var actualMessage = diagnosticInfo.GetMessage(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(string.Empty, actualMessage);
    }

    [Fact(DisplayName = "The GetMessage method must get the message from the message provider")]
    public void GetMessageMethod_MustReturnFromMessageProvider()
    {
        // Arrange
        const string Message = "Invalid PDF file";
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.LoadMessage(Code, CultureInfo.InvariantCulture).Returns(Message);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code);
        var actualMessage = diagnosticInfo.GetMessage(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(Message, actualMessage);
    }

    [Fact(DisplayName = "The GetMessage method with arguments must format the string from the message provider")]
    public void GetMessageMethod_WithArguments_MustFormatStringFromMessageProvider()
    {
        // Arrange
        const string Arg1 = "arg1";
        const int Arg2 = 2;
        const string MessageTemplate = "Invalid PDF file: {0}, {1}";
        var message = $"Invalid PDF file: {Arg1}, {Arg2}";
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;

        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.LoadMessage(Code, CultureInfo.InvariantCulture).Returns(MessageTemplate);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code, Arg1, Arg2);
        var actualMessage = diagnosticInfo.GetMessage(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(message, actualMessage);
    }

    [Fact(DisplayName = "The GetMessage method with nested argument must format the string from the message provider")]
    public void GetMessageMethod_WithNestedArgument_MustFormatStringFromMessageProvider()
    {
        // Arrange
        const string MessageTemplate = "Invalid PDF file: {0}";
        const string Message = "Invalid PDF file: Invalid PDF file: {0}";
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;

        var messageProvider = Substitute.For<IMessageProvider>();
        messageProvider.LoadMessage(Code, CultureInfo.InvariantCulture).Returns(MessageTemplate);

        DiagnosticInfo nestedDiagnosticInfo = new(messageProvider, Code);

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code, nestedDiagnosticInfo);
        var actualMessage = diagnosticInfo.GetMessage(CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(Message, actualMessage);
    }

    [Fact(DisplayName = "The GetHashCode method must return the diagnostic code")]
    public void GetHashCodeMethod_MustComputeDiagnosticCode()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;

        var expectedHashCode = HashCode.Combine(Code);
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code);
        var actualHashCode = diagnosticInfo.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "The GetHashCode method with arguments must compute the diagnostic code and arguments")]
    public void GetHashCodeMethod_WithArguments_MustComputeDiagnosticCodeAndArguments()
    {
        // Arrange
        const string Arg1 = "arg1";
        const int Arg2 = 2;
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;

        var expectedHashCode = HashCode.Combine(Code, Arg1, Arg2);
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, Code, Arg1, Arg2);
        var actualHashCode = diagnosticInfo.GetHashCode();

        // Assert
        Assert.Equal(expectedHashCode, actualHashCode);
    }

    [Fact(DisplayName = "The Equals method with null object must return false")]
    public void EqualsMethod_Null_MustReturnFalse()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, Code);
        var actualEquals = diagnosticInfo1.Equals(null);

        // Assert
        Assert.False(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with different codes must return false")]
    public void EqualsMethod_DifferentCodes_MustReturnFalse()
    {
        // Arrange
        const DiagnosticCode Code1 = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticCode Code2 = DiagnosticCode.ERR_InvalidPDFVersion;
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, Code1);
        DiagnosticInfo diagnosticInfo2 = new(messageProvider, Code2);
        var actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.False(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with different types must return false")]
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global", Justification = "Reviewed.")]
    public void EqualsMethod_DifferentTypes_MustReturnFalse()
    {
        // Arrange
        const DiagnosticCode Code1 = DiagnosticCode.ERR_InvalidPDF;
        const DiagnosticCode Code2 = DiagnosticCode.ERR_InvalidPDFVersion;
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, Code1);
        object diagnosticInfo2 = new DiagnosticInfo(messageProvider, Code2);
        var actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.False(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with different number of arguments must return false")]
    public void EqualsMethod_DifferentNumberOfArguments_MustReturnFalse()
    {
        // Arrange
        const string Arg1 = "arg1";
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo diagnosticInfo2 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, Arg1);
        var actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.False(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with different arguments must return false")]
    public void EqualsMethod_DifferentArguments_MustReturnFalse()
    {
        // Arrange
        const string Arg1 = "arg1";
        const string Arg2 = "arg2";
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, Arg1);
        DiagnosticInfo diagnosticInfo2 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, Arg2);
        var actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.False(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with same codes must return true")]
    public void EqualsMethod_SameCodes_MustReturnTrue()
    {
        // Arrange
        const DiagnosticCode Code = DiagnosticCode.ERR_InvalidPDF;
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, Code);
        DiagnosticInfo diagnosticInfo2 = new(messageProvider, Code);
        var actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.True(actualEquals);
    }

    [Fact(DisplayName = "The Equals method with same arguments must return false")]
    public void EqualsMethod_SameArguments_MustReturnFalse()
    {
        // Arrange
        const string Arg1 = "arg1";
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo1 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, Arg1);
        DiagnosticInfo diagnosticInfo2 = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, Arg1);
        var actualEquals = diagnosticInfo1.Equals(diagnosticInfo2);

        // Assert
        Assert.True(actualEquals);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.Arguments)} property must be set from the constructor")]
    public void ArgumentsProperty_MustBeComputedFromConstructor()
    {
        // Arrange
        const string Arg1 = "arg1";
        const string Arg2 = "arg2";
        object[] expectedArguments = [Arg1, Arg2];
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, DiagnosticCode.ERR_InvalidPDF, Arg1, Arg2);
        object[] actualArguments = diagnosticInfo.Arguments;

        // Assert
        Assert.Equal(expectedArguments, actualArguments);
    }

    [Fact(DisplayName = $"The {nameof(DiagnosticInfo.MessageProvider)} property must be set from the constructor")]
    public void MessageProviderProperty_MustBeComputedFromConstructor()
    {
        // Arrange
        var messageProvider = Substitute.For<IMessageProvider>();

        // Act
        DiagnosticInfo diagnosticInfo = new(messageProvider, DiagnosticCode.ERR_InvalidPDF);
        var actualMessageProvider = diagnosticInfo.MessageProvider;

        // Assert
        Assert.Equal(messageProvider, actualMessageProvider);
    }
}
