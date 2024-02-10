// <copyright file="SourceText.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Text;

public abstract class SourceText
{
    public abstract int Length { get; }

    public abstract void CopyTo(int sourceIndex, byte[] destination, int destinationIndex, int count);
}
