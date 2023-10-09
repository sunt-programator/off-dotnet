﻿// <copyright file="IMoveTextOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;

namespace OffDotNet.Pdf.Core.Text.Operations.TextPosition;

public interface IMoveTextOperation : IPdfOperation
{
    PdfReal X { get; }

    PdfReal Y { get; }
}