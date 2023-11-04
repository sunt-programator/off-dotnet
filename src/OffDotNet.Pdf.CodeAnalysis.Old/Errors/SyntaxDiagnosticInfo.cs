// <copyright file="SyntaxDiagnosticInfo.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Old.Errors;

internal class SyntaxDiagnosticInfo : DiagnosticInfo
{
    internal SyntaxDiagnosticInfo(int offset, int width, ErrorCode code, params object[] args)
        : base(code, args)
    {
        this.Offset = offset;
        this.Width = width;
    }

    internal SyntaxDiagnosticInfo(ErrorCode code)
        : this(0, 0, code)
    {
    }

    internal int Offset { get; }

    internal int Width { get; }

    public SyntaxDiagnosticInfo WithOffset(int offset)
    {
        return new SyntaxDiagnosticInfo(offset, this.Width, this.Code, this.Arguments);
    }
}
