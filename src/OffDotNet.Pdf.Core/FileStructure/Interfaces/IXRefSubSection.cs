﻿// <copyright file="IXRefSubSection.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Core.Common;

namespace OffDotNet.Pdf.Core.FileStructure;

public interface IXRefSubSection : IPdfObject
{
    int ObjectNumber { get; }

    ICollection<IXRefEntry> Value { get; }
}