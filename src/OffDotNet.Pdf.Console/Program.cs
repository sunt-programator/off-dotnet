// <copyright file="Program.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.Document;

namespace OffDotNet.Pdf.Console;

internal static class Program
{
    public static async Task Main()
    {
        await GeneratePdf().ConfigureAwait(false);
    }

    private static async Task GeneratePdf()
    {
        string fileName = Path.Combine(Environment.CurrentDirectory, "off.net.pdf");
        FileStream fileStream = new(fileName, FileMode.Create, FileAccess.Write);
        PdfDocument pdfDocument = new(fileStream, new PdfDocumentOptions());

        await using (fileStream.ConfigureAwait(false))
        await using (pdfDocument.ConfigureAwait(false))
        {
            await pdfDocument.GenerateOutputStream().ConfigureAwait(false);
        }
    }
}
