// <copyright file="Lexer.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Buffers;
using System.Globalization;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.Parser;

internal class Lexer
{
    private readonly InputReader reader;

    public Lexer(byte[] source)
    {
        this.reader = new InputReader(source);
    }

    public SyntaxToken Lex()
    {
        bool canPeek = this.reader.TryPeek(out byte? peekByte);

        if (!canPeek)
        {
            return default;
        }

        switch (peekByte)
        {
            case >= 0x30 and <= 0x39: // 0-9
                return this.ScanNumericLiteralSingleInteger();
        }

        return default;
    }

    private SyntaxToken ScanNumericLiteralSingleInteger()
    {
        ArrayPool<byte> pool = ArrayPool<byte>.Shared;
        byte[] numericByteArray = pool.Rent(12);
        int i = 0;

        while (true)
        {
            this.reader.TryPeek(out byte? peekByte);

            if (!peekByte.HasValue || !peekByte.IsDecDigit())
            {
                pool.Return(numericByteArray);
                break;
            }

            numericByteArray[i] = peekByte.Value;
            i++;
            this.reader.AdvanceByte();
        }

        pool.Return(numericByteArray);

        string text = string.Create(i, numericByteArray, (span, bytes) =>
        {
            for (int j = 0; j < span.Length; j++)
            {
                span[j] = (char)bytes[j];
            }
        });

        return new SyntaxToken(SyntaxKind.NumericLiteralToken, text, int.Parse(text, CultureInfo.InvariantCulture));
    }
}
