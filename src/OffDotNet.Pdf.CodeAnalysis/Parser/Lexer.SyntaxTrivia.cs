// <copyright file="Lexer.SyntaxTrivia.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Parser;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

internal partial class Lexer
{
    private const int TriviaListInitialCapacity = 8;

    private void LexSyntaxTrivia(ref SyntaxListBuilder triviaList)
    {
        triviaList.Clear();
        this.stringBuilder.Clear();

        while (true)
        {
            byte? peekedByte = this.reader.PeekByte();

            switch (peekedByte)
            {
                case 0x25: // '%'
                    this.ScanToEndOfLine();
                    this.AddTrivia(SyntaxFactory.Comment(this.stringBuilder.ToString()), ref triviaList);
                    break;
                default:
                    return;
            }
        }
    }

    private void AddTrivia(GreenNode trivia, [NotNull] ref SyntaxListBuilder? list)
    {
        if (this.errors != null)
        {
            trivia = trivia.WithDiagnosticsGreen(this.GetErrors(leadingTriviaWidth: 0));
        }

        list ??= new SyntaxListBuilder(TriviaListInitialCapacity);
        list.Add(trivia);
    }

    private void ScanToEndOfLine()
    {
        while (true)
        {
            byte? peekedByte = this.reader.PeekByte();

            if (!peekedByte.HasValue || peekedByte.IsNewLine())
            {
                break;
            }

            this.stringBuilder.Append((char)peekedByte.Value);
            this.reader.AdvanceByte();
        }
    }
}
