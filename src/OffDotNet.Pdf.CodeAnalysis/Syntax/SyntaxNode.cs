// <copyright file="SyntaxNode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public abstract class SyntaxNode
{
    internal SyntaxNode(SyntaxKind kind, DiagnosticInfo[]? diagnostics, int textLength)
    {
        throw new NotImplementedException();
    }
}
