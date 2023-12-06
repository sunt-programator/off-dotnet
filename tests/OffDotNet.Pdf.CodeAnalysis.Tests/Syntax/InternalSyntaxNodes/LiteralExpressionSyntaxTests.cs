// <copyright file="LiteralExpressionSyntaxTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using InternalSyntax = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class LiteralExpressionSyntaxTests
{
    [Fact(DisplayName = $"The {nameof(InternalSyntax.LiteralExpressionSyntax.Token)} property must be assigned from constructor.")]
    public void KeywordProperty_MustBeAssignedFromConstructor()
    {
        // Arrange
        InternalSyntax.SyntaxToken keyword = InternalSyntax.SyntaxToken.Create(SyntaxKind.TrueKeyword);
        InternalSyntax.LiteralExpressionSyntax literalExpression = InternalSyntax.SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        InternalSyntax.SyntaxToken actualToken = literalExpression.Token;

        // Assert
        Assert.Equal(keyword, actualToken);
    }

    [Fact(DisplayName = $"The {nameof(InternalSyntax.LiteralExpressionSyntax.SlotCount)} property must be equal to 1.")]
    public void SlotCountProperty_MustBeEqualTo1()
    {
        // Arrange
        InternalSyntax.SyntaxToken keyword = InternalSyntax.SyntaxToken.Create(SyntaxKind.TrueKeyword);
        InternalSyntax.LiteralExpressionSyntax literalExpression = InternalSyntax.SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        int actualSlotCount = literalExpression.SlotCount;

        // Assert
        Assert.Equal(1, actualSlotCount);
    }

    [Fact(DisplayName = $"The GetSlot() method with index 0 must return {nameof(InternalSyntax.LiteralExpressionSyntax.Token)} property.")]
    public void GetSlotMethod_Index0_MustReturnKeywordProperty()
    {
        // Arrange
        InternalSyntax.SyntaxToken keyword = InternalSyntax.SyntaxToken.Create(SyntaxKind.TrueKeyword);
        InternalSyntax.LiteralExpressionSyntax literalExpression = InternalSyntax.SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        InternalSyntax.GreenNode? actualSlot = literalExpression.GetSlot(0);

        // Assert
        Assert.Equal(keyword, actualSlot);
    }

    [Fact(DisplayName = "The GetSlot() method with index 1 must return null.")]
    public void GetSlotMethod_Index1_MustReturnNull()
    {
        // Arrange
        InternalSyntax.SyntaxToken keyword = InternalSyntax.SyntaxToken.Create(SyntaxKind.TrueKeyword);
        InternalSyntax.LiteralExpressionSyntax literalExpression = InternalSyntax.SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, keyword);

        // Act
        InternalSyntax.GreenNode? actualSlot = literalExpression.GetSlot(1);

        // Assert
        Assert.Null(actualSlot);
    }
}
