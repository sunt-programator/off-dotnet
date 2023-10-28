// <copyright file="SyntaxToken.SyntaxTokenWithTrivia.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal partial class SyntaxToken
{
    internal class SyntaxTokenWithTrivia : SyntaxToken
    {
        private readonly GreenNode? leadingField;
        private readonly GreenNode? trailingField;

        internal SyntaxTokenWithTrivia(SyntaxKind kind, GreenNode leading, GreenNode trailing)
            : base(kind, 0)
        {
            this.leadingField = leading;
            this.trailingField = trailing;
        }

        public override GreenNode? LeadingTrivia => this.leadingField;
    }
}
