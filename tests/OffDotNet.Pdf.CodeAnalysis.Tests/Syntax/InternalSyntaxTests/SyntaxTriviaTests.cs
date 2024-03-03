// <copyright file="SyntaxTriviaTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using InternalSyntax = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

public class SyntaxTriviaTests
{
    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxTrivia.Kind)} property.")]
    public void Create_MustSetKindProperty()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";
        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        SyntaxKind actualSyntaxKind = trivia.Kind;

        // Assert
        Assert.Equal(kind, actualSyntaxKind);
    }

    [Fact(DisplayName = $"The Create() method must set the {nameof(InternalSyntax.SyntaxTrivia.Text)} property.")]
    public void Create_MustSetTextProperty()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";
        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        string actualText = trivia.Text;

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
        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        int actualFullWidth = trivia.FullWidth;

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
        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        int actualWidth = trivia.Width;

        // Assert
        Assert.Equal(expectedFullWidth, actualWidth);
    }

    [Fact(DisplayName = $"The GetSlot() method must throw an {nameof(InvalidOperationException)}")]
    public void GetSlot_MustThrowInvalidOperationException()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";
        GreenNode trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        GreenNode? ActualSlotFunc()
        {
            return trivia.GetSlot(0);
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

        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        string actualToStringValue = trivia.ToString();

        // Assert
        Assert.Equal(text, actualToStringValue);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.LeadingTriviaWidth)} must be overriden to 0.")]
    public void LeadingTriviaWidth_MustBeOverridenToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";

        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        int actualLeadingTriviaWidth = trivia.LeadingTriviaWidth;

        // Assert
        Assert.Equal(0, actualLeadingTriviaWidth);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.TrailingTriviaWidth)} must be overriden to 0.")]
    public void TrailingTriviaWidth_MustBeOverridenToZero()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";

        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        int actualTrailingTriviaWidth = trivia.TrailingTriviaWidth;

        // Assert
        Assert.Equal(0, actualTrailingTriviaWidth);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.IsTrivia)} property must return true.")]
    public void IsTriviaProperty_MustReturnTrue()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";

        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        bool actualIsTrivia = trivia.IsTrivia;

        // Assert
        Assert.True(actualIsTrivia);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.SyntaxTrivia.IsToken)} property must return false.")]
    public void IsTokenProperty_MustReturnFalse()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "\r\n";

        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        bool actualIsToken = trivia.IsToken;

        // Assert
        Assert.False(actualIsToken);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "    ";

        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        string actualString = trivia.ToString();

        // Assert
        Assert.Equal(text, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "    ";

        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        string actualString = trivia.ToFullString();

        // Assert
        Assert.Equal(text, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.EndOfLineTrivia;
        const string text = "    ";
        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        InternalSyntax.SyntaxTrivia trivia = InternalSyntax.SyntaxTrivia.Create(kind, text);

        // Act
        GreenNode actualNode = trivia.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(trivia, actualNode);
        Assert.Equal(diagnostics, actualNode.GetDiagnostics());
        Assert.True(actualNode.ContainsFlags(GreenNode.NodeFlags.ContainsDiagnostics));
    }
}
