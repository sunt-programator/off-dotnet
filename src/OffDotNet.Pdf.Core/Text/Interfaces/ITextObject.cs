﻿// <copyright file="ITextObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Text;

using Common;

public interface ITextObject : IPdfObject
{
    IReadOnlyCollection<IPdfOperation> Value { get; }
}
