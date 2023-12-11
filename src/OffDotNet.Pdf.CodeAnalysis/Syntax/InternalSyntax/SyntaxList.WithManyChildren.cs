// <copyright file="SyntaxList.WithManyChildren.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Collections;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal abstract partial class SyntaxList
{
    internal class WithManyChildren : WithManyChildrenBase
    {
        public WithManyChildren(ArrayElement<GreenNode>[] children)
            : base(children)
        {
        }
    }
}
