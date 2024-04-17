﻿// <copyright file="SyntaxListTests.WithManyChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntaxNodes;
using SyntaxFactory = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;

public class WithManyChildrenTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxList.SlotCount)} property must be equal to 3.")]
    public void SlotCountProperty_MustBeEqualTo256()
    {
        // Arrange
        const int SlotCount = 3;

        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        for (var i = 0; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualSlotCount = syntaxList.SlotCount;

        // Assert
        Assert.Equal(SlotCount, actualSlotCount);
    }

    [Fact(DisplayName = "The GetSlot() method with index 0 must return the first child.")]
    public void GetSlotMethod_Index0_MustReturnFirstObject()
    {
        // Arrange
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[3];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualSlot = syntaxList.GetSlot(0);

        // Assert
        Assert.Equal(children[0]._value, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 1 must return the second child.")]
    public void GetSlotMethod_Index1_MustReturnSecondObject()
    {
        // Arrange
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[3];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualSlot = syntaxList.GetSlot(1);

        // Assert
        Assert.Equal(children[1]._value, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 2 must return the third child.")]
    public void GetSlotMethod_Index2_MustReturnThirdObject()
    {
        // Arrange
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[3];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualSlot = syntaxList.GetSlot(2);

        // Assert
        Assert.Equal(children[2]._value, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 3 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[3];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualSlot = syntaxList.GetSlot(3);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[3];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualFullWidth = syntaxList.FullWidth;

        // Assert
        Assert.Equal(19, actualFullWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[3];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualWidth = syntaxList.Width;

        // Assert
        Assert.Equal(17, actualWidth);
    }

    [Theory(DisplayName = "The GetSlotOffset() method with specified index must return correct offset.")]
    [InlineData(0, 0)]
    [InlineData(1, 6)]
    [InlineData(2, 13)]
    [InlineData(3, 19)]
    [InlineData(4, 19)] // No more slots, so the offset is the same as the last slot.
    public void GetSlotOffsetMethod_MustReturnTheCorrectOffset(int index, int offset)
    {
        // Arrange
        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[3];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualOffset = syntaxList.GetSlotOffset(index);

        // Assert
        Assert.Equal(offset, actualOffset);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxFactory)}.{nameof(SyntaxFactory.List)} method must return a {nameof(SyntaxList.WithManyChildren)} type")]
    public void SyntaxFactory_List_MustBeAssignedToWithManyChildren()
    {
        // Arrange
        const int SlotCount = 9;

        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        for (var i = 0; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        // Act
        var syntaxList = SyntaxFactory.List(children);

        // Assert
        Assert.IsType<SyntaxList.WithManyChildren>(syntaxList);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const int SlotCount = 9;
        const string ExpectedString = "true  true  true  true  true  true  true  true  true";
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        for (var i = 0; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        // Act
        var syntaxList = SyntaxFactory.List(children);
        var actualString = syntaxList.ToString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The ToFullString() method must include the trivia.")]
    public void ToFullStringMethod_MustIncludeTrivia()
    {
        // Arrange
        const int SlotCount = 9;
        const string ExpectedString = " true  true  true  true  true  true  true  true  true ";
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        for (var i = 0; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        // Act
        var syntaxList = SyntaxFactory.List(children);
        var actualString = syntaxList.ToFullString();

        // Assert
        Assert.Equal(ExpectedString, actualString);
    }

    [Fact(DisplayName = "The SetDiagnostics() method must set the diagnostics and return a new instance.")]
    public void SetDiagnosticsMethod_MustSetDiagnosticsAndReturnNewInstance()
    {
        // Arrange
        const int SlotCount = 9;
        GreenNode trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        for (var i = 0; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        var syntaxList = SyntaxFactory.List(children);

        DiagnosticInfo expectedDiagnostic = new(Substitute.For<IMessageProvider>(), DiagnosticCode.ERR_InvalidPDF);
        DiagnosticInfo[] diagnostics = [expectedDiagnostic];

        // Act
        var actualSyntaxList = syntaxList.SetDiagnostics(diagnostics);

        // Assert
        Assert.NotSame(syntaxList, actualSyntaxList);
        Assert.Equal(diagnostics, actualSyntaxList.GetDiagnostics());
        Assert.True(actualSyntaxList.ContainsFlags(NodeFlags.ContainsDiagnostics));
    }
}
