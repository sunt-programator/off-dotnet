// <copyright file="GreenNode.Spans.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

/// <summary>
/// Additional class containing span-related methods and properties for <see cref="GreenNode"/>.
/// </summary>
internal abstract partial class GreenNode
{
    /// <summary>Gets the width of the token, not including its <see cref="LeadingTrivia"/> and <see cref="TrailingTrivia"/>.</summary>
    public virtual int Width => this.FullWidth;

    /// <summary>Gets the full width of the token, including its <see cref="LeadingTrivia"/> and <see cref="TrailingTrivia"/>.</summary>
    public int FullWidth { get; }

    /// <summary>Gets the <see cref="LeadingTrivia"/> width of the first terminal node.</summary>
    public virtual int LeadingTriviaWidth { get; }

    /// <summary>Gets the <see cref="TrailingTrivia"/> width of the last terminal node.</summary>
    public virtual int TrailingTriviaWidth { get; }
}
