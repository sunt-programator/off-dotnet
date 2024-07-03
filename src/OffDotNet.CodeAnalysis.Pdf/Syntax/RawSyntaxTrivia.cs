// <copyright file="RawSyntaxTrivia.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Pdf.Syntax;

using OffDotNet.CodeAnalysis.Syntax;
using Utils;

internal sealed class RawSyntaxTrivia : RawSyntaxNode
{
    internal RawSyntaxTrivia(SyntaxKind kind, ReadOnlySpan<byte> text)
        : base(kind, text.Length)
    {
        Debug.Assert(kind.IsTrivia(), "Invalid trivia kind");
        Text = Encoding.ASCII.GetString(text);
    }

    public string Text { get; }

    public override bool IsTrivia => true;

    internal override Option<AbstractNode> GetSlot(int index)
    {
        throw new NotImplementedException();
    }
}
