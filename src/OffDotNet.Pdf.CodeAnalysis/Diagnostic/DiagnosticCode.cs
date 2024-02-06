// <copyright file="DiagnosticCode.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "The names of the enum members must be the same as the resource strings.")]
public enum DiagnosticCode : ushort
{
    ERR_InvalidPDF = 1,
    ERR_InvalidPDFVersion = 2,
}
