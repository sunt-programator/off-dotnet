// <copyright file="IFontOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text.Operations.TextState;

using OffDotNet.Pdf.Core.Common;
using OffDotNet.Pdf.Core.Primitives;

public interface IFontOperation : IPdfOperation
{
    PdfName FontName { get; }

    PdfInteger FontSize { get; }
}
