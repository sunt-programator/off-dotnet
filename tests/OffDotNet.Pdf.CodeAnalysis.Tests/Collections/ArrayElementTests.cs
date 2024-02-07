// <copyright file="ArrayElementTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Collections;

using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;

public class ArrayElementTests
{
    [Fact(DisplayName = $"The {nameof(ArrayElement<GreenNode>)} struct must have a {nameof(ArrayElement<GreenNode>.Value)} property")]
    public void ValueProperty()
    {
        // Arrange
        SyntaxToken syntaxToken = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        ArrayElement<GreenNode> arrayElement = new() { Value = syntaxToken };

        // Act
        GreenNode actualValue = arrayElement.Value;

        // Assert
        Assert.Equal(syntaxToken, actualValue);
    }

    [Fact(DisplayName = $"The {nameof(ArrayElement<GreenNode>)} struct must have an implicit operator")]
    public void ImplicitOperator()
    {
        // Arrange
        SyntaxToken syntaxToken = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        ArrayElement<GreenNode> arrayElement = new() { Value = syntaxToken };

        // Act
        GreenNode actualValue = arrayElement;

        // Assert
        Assert.Equal(syntaxToken, actualValue);
    }
}
