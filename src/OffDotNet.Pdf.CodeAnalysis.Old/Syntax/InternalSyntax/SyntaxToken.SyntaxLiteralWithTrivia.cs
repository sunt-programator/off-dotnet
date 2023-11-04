// <copyright file="SyntaxToken.SyntaxLiteralWithTrivia.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Old.Syntax.InternalSyntax;

internal partial class SyntaxToken
{
    internal class SyntaxTokenWithValueAndTrivia<T> : SyntaxTokenWithValue<T>
        where T : notnull
    {
        private readonly GreenNode? leading;
        private readonly GreenNode? trailing;

        internal SyntaxTokenWithValueAndTrivia(SyntaxKind kind, string text, T value, GreenNode? leading, GreenNode? trailing)
            : base(kind, text, value)
        {
            this.leading = leading;
            this.trailing = trailing;
        }
    }
}
