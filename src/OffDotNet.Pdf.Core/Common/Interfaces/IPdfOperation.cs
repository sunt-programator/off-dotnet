// <copyright file="IPdfOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Common;

public interface IPdfOperation : IPdfObject
{
    string PdfOperator { get; }
}
