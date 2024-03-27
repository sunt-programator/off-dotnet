// <copyright file="SyntaxListTests.WithLotsOfChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

public class WithLotsOfChildrenTests
{
    [Fact(DisplayName = $"The {nameof(SyntaxList.SlotCount)} property must be equal to 256.")]
    public void SlotCountProperty_MustBeEqualTo256()
    {
        // Arrange
        const int SlotCount = 256;

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
        const int SlotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        for (var i = 3; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

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
        const int SlotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        for (var i = 3; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

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
        const int SlotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        for (var i = 3; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualSlot = syntaxList.GetSlot(2);

        // Assert
        Assert.Equal(children[2]._value, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 10 must return null.")]
    public void GetSlotMethod_Index3_MustReturnNull()
    {
        // Arrange
        const int SlotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword));

        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));

        for (var i = 3; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualSlot = syntaxList.GetSlot(SlotCount);

        // Assert
        Assert.Null(actualSlot);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.FullWidth)} property must consider all slots.")]
    public void FullWidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        const int SlotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));

        for (var i = 3; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualFullWidth = syntaxList.FullWidth;

        // Assert
        Assert.Equal(61, actualFullWidth);
    }

    [Fact(DisplayName = $"The {nameof(LiteralExpressionSyntax.Width)} property must consider all slots.")]
    public void WidthProperty_MustIncludeAllSlots()
    {
        // Arrange
        const int SlotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));

        for (var i = 3; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualWidth = syntaxList.Width;

        // Assert
        Assert.Equal(59, actualWidth);
    }

    [Theory(DisplayName = "The GetSlotOffset() method with specified index must return correct offset.")]
    [InlineData(0, 0)]
    [InlineData(1, 6)]
    [InlineData(2, 13)]
    [InlineData(3, 19)]
    [InlineData(4, 25)]
    [InlineData(5, 31)]
    [InlineData(6, 37)]
    [InlineData(7, 43)]
    [InlineData(8, 49)]
    [InlineData(9, 55)]
    [InlineData(10, 55)] // No more slots, so the offset is the same as the last slot.
    public void GetSlotOffsetMethod_MustReturnTheCorrectOffset(int index, int offset)
    {
        // Arrange
        const int SlotCount = 10;
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        var trivia = SyntaxFactory.Trivia(SyntaxKind.WhitespaceTrivia, " ");
        children[0]._value = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));
        children[1]._value = SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword, trivia, trivia));
        children[2]._value = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression, SyntaxFactory.Token(SyntaxKind.NullKeyword, trivia, trivia));

        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword, trivia, trivia));

        for (var i = 3; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        var syntaxList = SyntaxFactory.List(children);

        // Act
        var actualOffset = syntaxList.GetSlotOffset(index);

        // Assert
        Assert.Equal(offset, actualOffset);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxFactory)}.{nameof(SyntaxFactory.List)} method must return a {nameof(SyntaxList.WithLotsOfChildren)} type")]
    public void SyntaxFactory_List_MustBeAssignedToWithLotsOfChildren()
    {
        // Arrange
        const int SlotCount = 10;

        var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword));
        ArrayElement<GreenNode>[] children = new ArrayElement<GreenNode>[SlotCount];

        for (var i = 0; i < SlotCount; i++)
        {
            children[i]._value = literalExpressionSyntax;
        }

        // Act
        var syntaxList = SyntaxFactory.List(children);

        // Assert
        Assert.IsType<SyntaxList.WithLotsOfChildren>(syntaxList);
    }

    [Fact(DisplayName = "The ToString() method must not include the trivia.")]
    public void ToStringMethod_MustNotIncludeTrivia()
    {
        // Arrange
        const int SlotCount = 10;
        const string ExpectedString = "true  true  true  true  true  true  true  true  true  true";
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
        const int SlotCount = 10;
        const string ExpectedString = " true  true  true  true  true  true  true  true  true  true ";
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
        const int SlotCount = 10;
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
        Assert.True(actualSyntaxList.ContainsFlags(GreenNode.NodeFlags.ContainsDiagnostics));
    }
}
