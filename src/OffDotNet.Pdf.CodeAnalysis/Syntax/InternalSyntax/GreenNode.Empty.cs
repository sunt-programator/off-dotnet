// <copyright file="GreenNode.Empty.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

using Diagnostic;
using InternalUtilities;

/// <summary>Represents the wrapper class for <see cref="GreenNode.NoneGreenNode"/> class.</summary>
internal abstract partial class GreenNode
{
    /// <summary>Represents a none node used instead of nulls.</summary>
    internal static readonly NoneGreenNode None = new();

    /// <summary>Represents a none node used instead of nulls.</summary>
    internal class NoneGreenNode : GreenNode
    {
        /// <summary>Initializes a new instance of the <see cref="GreenNode.NoneGreenNode"/> class.</summary>
        internal NoneGreenNode()
            : base(SyntaxKind.None)
        {
        }

        /// <inheritdoc/>
        internal override GreenNode GetSlot(int index) => throw ExceptionUtilities.Unreachable();

        /// <inheritdoc/>
        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) => throw ExceptionUtilities.Unreachable();
    }
}
