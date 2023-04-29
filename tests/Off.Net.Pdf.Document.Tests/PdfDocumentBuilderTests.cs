// <copyright file="PdfDocumentBuilderTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using Xunit;

namespace Off.Net.Pdf.Document.Tests;

public class PdfDocumentBuilderTests
{
    [Fact(DisplayName = $"The {nameof(PdfDocumentBuilder)} constructor without arguments should generate a valid PDF output stream")]
    public async Task PdfDocument_ConstructorWithoutArguments_ShouldGenerateValidOutputStream()
    {
        // Arrange
        const string expectedContent =
            "%PDF-1.7\n" +
            "1 0 obj\n<</Type /Catalog /Pages 2 0 R>>\nendobj\n" +
            "2 0 obj\n<</Type /Pages /Kids [3 0 R] /Count 1>>\nendobj\n" +
            "3 0 obj\n<</Type /Page /Parent 2 0 R /Resources <</Font <</F1 5 0 R>> /ProcSet [/PDF /Text /ImageB /ImageC /ImageI]>> /MediaBox [0 0 612 792] /Contents 4 0 R>>\nendobj\n" +
            "4 0 obj\n<</Length 44>>\nstream\nBT\n/F1 24 Tf\n100 100 Td\n(Hello World) Tj\nET\nendstream\nendobj\n" +
            "5 0 obj\n<</Type /Font /Subtype /Type1 /Name /Helvetica /BaseFont /Helvetica>>\nendobj\n" +
            "xref\n0 6\n0000000000 65535 f \n0000000009 00000 n \n0000000056 00000 n \n0000000111 00000 n \n0000000277 00000 n \n0000000368 00000 n \n" +
            "trailer\n<</Size 5 /Root 1 0 R>>\n" +
            "startxref\n453\n%%EOF";

        byte[] expectedBytes = Encoding.ASCII.GetBytes(expectedContent);
        MemoryStream stream = new();
        IPdfDocumentBuilder pdfDocumentBuilder = new PdfDocumentBuilder();
        PdfDocument pdfDocument = pdfDocumentBuilder.BuildPdfDocument(stream);

        // Act
        await using (stream.ConfigureAwait(false))
        await using (pdfDocument.ConfigureAwait(false))
        {
            await pdfDocument.GenerateOutputStream().ConfigureAwait(false);

            // Assert
            Assert.Equal(expectedBytes, stream.ToArray());
        }
    }
}
