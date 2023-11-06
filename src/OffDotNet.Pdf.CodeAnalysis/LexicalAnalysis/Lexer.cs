// <copyright file="Lexer.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.InputReaders;
using OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis.Handlers;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis;

internal sealed class Lexer : IDisposable
{
    private static readonly StartHandler StartHandler = new();
    private static readonly NumberCharHandler NumberCharHandler = new();
    private static readonly DotCharHandler DotCharHandler = new();
    private readonly HandlerContext handlerContext;

    public Lexer(ReadOnlyMemory<byte> source)
    {
        StartHandler
            .SetNext(DotCharHandler)
            .SetNext(NumberCharHandler);

        this.handlerContext = new HandlerContext(new InputReader(source));
    }

    public SyntaxToken NextToken()
    {
        StartHandler.Handle(this.handlerContext);
        return this.handlerContext.SyntaxToken;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}
