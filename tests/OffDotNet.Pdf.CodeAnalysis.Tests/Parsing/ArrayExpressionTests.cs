// <copyright file="ArrayExpressionTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Parsing;

[SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:Closing brace should be followed by blank line", Justification = "Test code")]
[SuppressMessage("Minor Code Smell", "S1199:Nested code blocks should not be used", Justification = "Test code")]
[SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Test code")]
public class ArrayExpressionTests
{
    [Fact(DisplayName = "The basic array expression should not return errors")]
    [Trait("Feature", "ArrayExpression")]
    public void ArrayExpression_Valid_ShouldNotProduceErrors()
    {
        // UsingExpression("[549 3.14 false (Ralph) /SomeName]");
        //
        // N(SyntaxKind.ArrayExpression);
        // {
        //     N(SyntaxKind.LeftSquareBracketToken);
        //     N(SyntaxKind.NumericLiteralExpression);
        //     {
        //         N(SyntaxKind.NumericLiteralToken, "549");
        //     }
        //     N(SyntaxKind.NumericLiteralExpression);
        //     {
        //         N(SyntaxKind.NumericLiteralToken, "3.14");
        //     }
        //     N(SyntaxKind.FalseLiteralExpression);
        //     {
        //         N(SyntaxKind.FalseKeyword, "false");
        //     }
        //     N(SyntaxKind.StringLiteralExpression);
        //     {
        //         N(SyntaxKind.StringLiteralToken, "(Ralph)");
        //     }
        //     N(SyntaxKind.NameLiteralExpression);
        //     {
        //         N(SyntaxKind.NameLiteralToken, "/SomeName");
        //     }
        //     N(SyntaxKind.RightSquareBracketToken);
        // }
        // Eof();
    }
}
