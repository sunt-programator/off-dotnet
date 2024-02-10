// <copyright file="IShowTextOperation.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text.Operations.TextShowing;

using Common;
using Primitives;

public interface IShowTextOperation : IPdfOperation
{
    PdfString Text { get; }
}
