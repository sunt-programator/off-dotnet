// <copyright file="AbstractHandler.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis.Handlers;

internal abstract class AbstractHandler : IHandler
{
    private IHandler? nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        this.nextHandler = handler;
        return handler;
    }

    public virtual void Handle(HandlerContext request)
    {
        this.nextHandler?.Handle(request);
    }
}
