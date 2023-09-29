// <copyright file="IPdfObject.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Interfaces;

public interface IPdfObject
{
    /// <summary>
    ///     Gets the bytes array representation of the Pdf object.
    /// </summary>
    ReadOnlyMemory<byte> Bytes { get; }

    string Content { get; }
}

public interface IPdfObject<out T> : IPdfObject
{
    /// <summary>
    ///     Gets the value of the object.
    /// </summary>
    T Value { get; }
}

public interface IMutablePdfObject<out T> : IPdfObject
{
    /// <summary>
    ///     Gets the value of the object.
    /// </summary>
    T? Value { get; }
}
