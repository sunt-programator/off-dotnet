// <copyright file="AbstractMessageProviderTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Tests.Diagnostics;

using OffDotNet.CodeAnalysis.Diagnostics;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
public class AbstractMessageProviderTests
{
    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should implement the {nameof(IMessageProvider)} interface")]
    public void Class_ShouldImplementTheIMessageProviderInterface()
    {
        // Arrange

        // Act
        var actual = Substitute.For<AbstractMessageProvider>();

        // Assert
        Assert.IsAssignableFrom<IMessageProvider>(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractMessageProvider.GetIdForDiagnosticCode)} method should return the language prefix and the diagnostic code")]
    public void GetIdForDiagnosticCodeMethod_ShouldReturnTheLanguagePrefixAndTheDiagnosticCode()
    {
        // Arrange
        const ushort Code = 1234;
        const string LanguagePrefix = "PDF";
        const string ExpectedDiagnosticId = "PDF1234";
        var messageProvider = Substitute.For<AbstractMessageProvider>();
        messageProvider.LanguagePrefix.Returns(LanguagePrefix);

        // Act
        var id = messageProvider.GetIdForDiagnosticCode(Code);

        // Assert
        Assert.Equal(ExpectedDiagnosticId, id);
    }
}
