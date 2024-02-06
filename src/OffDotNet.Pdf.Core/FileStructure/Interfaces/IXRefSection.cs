// <copyright file="IXRefSection.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

using OffDotNet.Pdf.Core.Common;

public interface IXRefSection : IPdfObject
{
    ICollection<IXRefSubSection> Value { get; }
}
