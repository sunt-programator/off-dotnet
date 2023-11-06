// <copyright file="NumberCharHandler.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Globalization;
using OffDotNet.Pdf.CodeAnalysis.InputReaders;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis.Handlers;

internal sealed class NumberCharHandler : AbstractHandler
{
    public override void Handle(HandlerContext request)
    {
        if (!request.Reader.PeekByte().IsDecDigit())
        {
            base.Handle(request);
            return;
        }

        bool containsDotChar = false;

        while (true)
        {
            byte? peekedByte = request.Reader.PeekByte();

            if (!peekedByte.HasValue || (!peekedByte.IsDecDigit() && peekedByte != (char)CharacterKind.Dot))
            {
                break;
            }

            if (peekedByte == (char)CharacterKind.Dot)
            {
                containsDotChar = true;
            }

            request.StringBuilder.Append((char)peekedByte.Value);
            request.Reader.AdvanceByte();
        }

        string lexeme = request.StringBuilder.ToString();

        if (containsDotChar)
        {
            request.SyntaxToken = new SyntaxToken(SyntaxKind.NumericLiteralToken, lexeme, GetSingleValue(lexeme));
            return;
        }

        request.SyntaxToken = new SyntaxToken(SyntaxKind.NumericLiteralToken, lexeme, GetInt32Value(lexeme));
    }

    private static int GetInt32Value(string text)
    {
        if (!int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out int result))
        {
            // this.AddError(MakeError(ErrorCode.ErrorIntOverflow));
        }

        return result;
    }

    private static float GetSingleValue(string text)
    {
        if (!float.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out float result))
        {
            // this.AddError(MakeError(ErrorCode.ErrorIntOverflow));
        }

        return result;
    }
}
