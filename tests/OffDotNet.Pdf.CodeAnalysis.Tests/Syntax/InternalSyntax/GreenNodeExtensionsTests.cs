// <copyright file="GreenNodeExtensionsTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntax;

public class GreenNodeExtensionsTests
{
    [Fact(DisplayName = "The GetFirstTerminal() extensions method must return null if providing a null input node.")]
    public void GetFirstTerminal_NullInputNode_ShouldReturnNull()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        SyntaxToken token = SyntaxToken.Create(kind);

        // Act
        GreenNode? actualFirstTerminal = token.GetFirstTerminal();

        // Assert
        Assert.Null(actualFirstTerminal);
    }

    [Fact(DisplayName = "The GetLastTerminal() extensions method must return null if providing a null input node.")]
    public void GetLastTerminal_NullInputNode_ShouldReturnNull()
    {
        // Arrange
        const SyntaxKind kind = SyntaxKind.TrueKeyword;
        SyntaxToken token = SyntaxToken.Create(kind);

        // Act
        GreenNode? actualFirstTerminal = token.GetLastTerminal();

        // Assert
        Assert.Null(actualFirstTerminal);
    }
}
