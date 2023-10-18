using OffDotNet.Pdf.CodeAnalysis.Parser;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

internal static class SyntaxFactory
{
    public static IEnumerable<SyntaxToken> ParseTokens(byte[] source)
    {
        Lexer lexer = new(source);
        yield return lexer.Lex();
    }

    public static SyntaxToken Literal(string text, int value)
    {
        return new SyntaxToken(SyntaxKind.NumericLiteralToken, text, value);
    }
}
