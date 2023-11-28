// <copyright file="SyntaxTriviaTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using InternalSyntax = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class SyntaxTriviaTests
{
    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxTrivia.Kind)} property.")]
    public void Create_MustSetKindProperty()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";
        InternalSyntax.SyntaxTrivia token = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        SyntaxKind actualSyntaxKind = token.Kind;

        // Assert
        Assert.Equal(kind, actualSyntaxKind);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxTrivia.Text)} property.")]
    public void Create_MustSetTextProperty()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";
        InternalSyntax.SyntaxTrivia token = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        string actualText = token.Text;

        // Assert
        Assert.Equal(text, actualText);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxTrivia.FullWidth)} property with the computed {nameof(InternalSyntax.SyntaxTrivia.Text)} property length.")]
    public void Create_MustSetFullWidthPropertyFromTextLength()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";
        const int expectedFullWidth = 2;
        InternalSyntax.SyntaxTrivia token = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        int actualFullWidth = token.FullWidth;

        // Assert
        Assert.Equal(expectedFullWidth, actualFullWidth);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxTrivia.Width)} property with the computed {nameof(InternalSyntax.SyntaxTrivia.Text)} property length.")]
    public void Create_MustSetWidthPropertyFromTextLength()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";
        const int expectedFullWidth = 2;
        InternalSyntax.SyntaxTrivia token = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        int actualWidth = token.Width;

        // Assert
        Assert.Equal(expectedFullWidth, actualWidth);
    }

    [Fact(DisplayName = $"The GetSlot() method must throw an {nameof(InvalidOperationException)}")]
    public void GetSlot_MustThrowInvalidOperationException()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";
        GreenNode token = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        GreenNode? ActualSlotFunc()
        {
            return token.GetSlot(0);
        }

        // Assert
        Assert.Throws<InvalidOperationException>(ActualSlotFunc);
    }

    [Fact(DisplayName = $"The ToString() method must return the {nameof(InternalSyntax.SyntaxTrivia.Text)} property value.")]
    public void ToStringMethod_MustReturnTheTextPropertyValue()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";

        InternalSyntax.SyntaxTrivia token = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        string actualToStringValue = token.ToString();

        // Assert
        Assert.Equal(text, actualToStringValue);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.LeadingTriviaWidth)} must be overriden to 0.")]
    public void LeadingTriviaWidth_MustBeOverridenToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";

        InternalSyntax.SyntaxTrivia token = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        int actualLeadingTriviaWidth = token.LeadingTriviaWidth;

        // Assert
        Assert.Equal(0, actualLeadingTriviaWidth);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.TrailingTriviaWidth)} must be overriden to 0.")]
    public void TrailingTriviaWidth_MustBeOverridenToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";

        InternalSyntax.SyntaxTrivia token = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        int actualTrailingTriviaWidth = token.TrailingTriviaWidth;

        // Assert
        Assert.Equal(0, actualTrailingTriviaWidth);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.IsTrivia)} property must return true.")]
    public void IsTriviaProperty_MustReturnTrue()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";

        InternalSyntax.SyntaxTrivia token = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        bool actualIsTrivia = token.IsTrivia;

        // Assert
        Assert.True(actualIsTrivia);
    }
}
