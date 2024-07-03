// <copyright file="RawSyntaxNodeTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Tests.Syntax;

using OffDotNet.CodeAnalysis.Pdf.Syntax;
using OffDotNet.CodeAnalysis.Syntax;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
public class RawSyntaxNodeTests
{
    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"Class should inherit from {nameof(RawSyntaxNode)}")]
    public void Class_ShouldInheritFromAbstractNode()
    {
        // Arrange

        // Act
        var actual = Substitute.For<RawSyntaxNode>(SyntaxKind.None);

        // Assert
        Assert.IsAssignableFrom<AbstractNode>(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxNode.RawKind)} property should the value passed to the constructor")]
    [InlineData(SyntaxKind.None)]
    [InlineData(SyntaxKind.List)]
    public void RawKindProperty_ShouldReturnTheValuePassedToTheConstructor(SyntaxKind rawKind)
    {
        // Arrange
        var rawNode = Substitute.For<RawSyntaxNode>(rawKind);

        // Act
        var kind = rawNode.RawKind;

        // Assert
        Assert.Equal((int)rawKind, kind);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.IsToken)} property should return false by default")]
    public void IsTokenProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        var rawNode = Substitute.For<RawSyntaxNode>(SyntaxKind.None);

        // Act
        var isToken = rawNode.IsToken;

        // Assert
        Assert.False(isToken);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.IsTrivia)} property should return false by default")]
    public void IsTriviaProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        var rawNode = Substitute.For<RawSyntaxNode>(SyntaxKind.None);

        // Act
        var isTrivia = rawNode.IsTrivia;

        // Assert
        Assert.False(isTrivia);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxNode.FullWidth)} property should return the value passed to the constructor")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void FullWidthProperty_ShouldReturnTheValuePassedToTheConstructor(int fullWidth)
    {
        // Arrange
        var rawNode = Substitute.For<RawSyntaxNode>(SyntaxKind.None, fullWidth);

        // Act
        var width = rawNode.FullWidth;

        // Assert
        Assert.Equal(fullWidth, width);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxNode.Kind)} property should return the value passed to the constructor")]
    [InlineData(SyntaxKind.None)]
    [InlineData(SyntaxKind.List)]
    public void KindProperty_ShouldReturnTheValuePassedToTheConstructor(SyntaxKind kind)
    {
        // Arrange
        var rawNode = Substitute.For<RawSyntaxNode>(kind);

        // Act
        var actual = rawNode.Kind;

        // Assert
        Assert.Equal(kind, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(RawSyntaxNode.KindText)} property should return the name of the syntax kind", Skip = "Not implemented")]
    [InlineData(SyntaxKind.None, "None")]
    [InlineData(SyntaxKind.List, "List")]
    public void KindTextProperty_ShouldReturnTheNameOfTheSyntaxKind(SyntaxKind kind, string expected)
    {
        // Arrange
        var rawNode = Substitute.For<RawSyntaxNode>(kind);

        // Act
        var kindText = rawNode.KindText;

        // Assert
        Assert.Equal(expected, kindText);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(RawSyntaxNode.Language)} property should return PDF")]
    public void LanguageProperty_ShouldReturnPDF()
    {
        // Arrange
        var rawNode = Substitute.For<RawSyntaxNode>(SyntaxKind.None);

        // Act
        var language = rawNode.Language;

        // Assert
        Assert.Equal("PDF", language);
    }
}
