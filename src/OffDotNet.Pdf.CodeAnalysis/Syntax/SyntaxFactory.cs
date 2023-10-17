using OffDotNet.Pdf.CodeAnalysis.Parser;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public static class SyntaxFactory
{
    public static IEnumerable<SyntaxToken> ParseTokens(byte[] source)
    {
        Lexer lexer = new(source);
        yield return lexer.Lex();
    }
}
