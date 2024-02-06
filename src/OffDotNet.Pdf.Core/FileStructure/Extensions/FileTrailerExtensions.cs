// <copyright file="FileTrailerExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

public static class FileTrailerExtensions
{
    public static IFileTrailer ToFileTrailer(this FileTrailerOptions fileTrailerOptions, long byteOffset)
    {
        return new FileTrailer(byteOffset, fileTrailerOptions);
    }
}
