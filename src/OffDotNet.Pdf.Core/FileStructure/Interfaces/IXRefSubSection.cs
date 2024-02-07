// <copyright file="IXRefSubSection.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

using OffDotNet.Pdf.Core.Common;

public interface IXRefSubSection : IPdfObject
{
    int ObjectNumber { get; }

    ICollection<IXRefEntry> Value { get; }
}
