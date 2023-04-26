// <copyright file="FileTrailerExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Off.Net.Pdf.Core.FileStructure;

public static class FileTrailerExtensions
{
    public static FileTrailer ToFileTrailer(this FileTrailerOptions fileTrailerOptions, long byteOffset)
    {
        return new FileTrailer(byteOffset, fileTrailerOptions);
    }
}
