// <copyright file="AbstractSyntaxTree.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

using Diagnostic;
using Text;

public abstract class AbstractSyntaxTree
{
    public abstract string FilePath { get; }

    public abstract IEnumerable<AbstractDiagnostic> GetDiagnostics(SyntaxTrivia syntaxTrivia);

    public abstract Location GetLocation(TextSpan span);
}
