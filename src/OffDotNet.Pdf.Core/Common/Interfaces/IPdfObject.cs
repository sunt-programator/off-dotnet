// <copyright file="IPdfObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Common;

public interface IPdfObject
{
    /// <summary>
    ///     Gets the bytes array representation of the Pdf object.
    /// </summary>
    ReadOnlyMemory<byte> Bytes { get; }

    /// <summary>
    ///     Gets the string representation of the Pdf object.
    /// </summary>
    string Content { get; }
}
