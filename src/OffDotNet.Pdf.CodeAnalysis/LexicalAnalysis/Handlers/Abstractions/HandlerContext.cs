// <copyright file="HandlerContext.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using OffDotNet.Pdf.CodeAnalysis.InputReaders;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis.Handlers;

internal class HandlerContext
{
    public HandlerContext(InputReader reader)
    {
        this.Reader = reader;
    }

    public SyntaxToken SyntaxToken { get; set; }

    public InputReader Reader { get; set; }

    public StringBuilder StringBuilder { get; set; } = new();
}
