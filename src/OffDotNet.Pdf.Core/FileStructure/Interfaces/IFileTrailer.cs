// <copyright file="IFileTrailer.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

using Common;
using Primitives;

public interface IFileTrailer : IPdfObject
{
    IPdfDictionary<IPdfObject> FileTrailerDictionary { get; }

    long ByteOffset { get; }
}
