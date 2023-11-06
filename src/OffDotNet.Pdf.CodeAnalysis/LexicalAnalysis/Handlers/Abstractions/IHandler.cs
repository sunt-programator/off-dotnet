// <copyright file="IHandler.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis.Handlers;

internal interface IHandler
{
    IHandler SetNext(IHandler handler);

    void Handle(HandlerContext request);
}
