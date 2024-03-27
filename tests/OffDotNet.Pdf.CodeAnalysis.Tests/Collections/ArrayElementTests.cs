// <copyright file="ArrayElementTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Collections;

using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

public class ArrayElementTests
{
    [Fact(DisplayName = $"The {nameof(ArrayElement<GreenNode>)} struct must have a {nameof(ArrayElement<GreenNode>._value)} property")]
    public void ValueProperty()
    {
        // Arrange
        var syntaxToken = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        ArrayElement<GreenNode> arrayElement = new() { _value = syntaxToken };

        // Act
        var actualValue = arrayElement._value;

        // Assert
        Assert.Equal(syntaxToken, actualValue);
    }

    [Fact(DisplayName = $"The {nameof(ArrayElement<GreenNode>)} struct must have an implicit operator")]
    public void ImplicitOperator()
    {
        // Arrange
        var syntaxToken = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        ArrayElement<GreenNode> arrayElement = new() { _value = syntaxToken };

        // Act
        GreenNode actualValue = arrayElement;

        // Assert
        Assert.Equal(syntaxToken, actualValue);
    }
}
