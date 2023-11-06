// <copyright file="StartHandler.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis.Handlers;

internal sealed class StartHandler : AbstractHandler
{
    public override void Handle(HandlerContext request)
    {
        request.SyntaxToken = default;
        request.StringBuilder.Clear();

        base.Handle(request);

        if (request.Reader.IsEndOfStream && request.SyntaxToken.Kind == SyntaxKind.None)
        {
            request.SyntaxToken = new SyntaxToken(SyntaxKind.EndOfFileToken, string.Empty);
            return;
        }

        if (request.SyntaxToken.Kind == SyntaxKind.None)
        {
            request.SyntaxToken = new SyntaxToken(SyntaxKind.BadToken, request.SyntaxToken.Text);
        }
    }
}
