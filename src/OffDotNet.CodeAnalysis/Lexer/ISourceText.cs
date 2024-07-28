// <copyright file="ISourceText.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Lexer;

public interface ISourceText
{
    ReadOnlyMemory<byte> Source { get; }

    int Length { get; }

    byte this[int position] { get; }

    void CopyTo(int sourceIndex, byte[] destination, int destinationIndex, int count);
}
