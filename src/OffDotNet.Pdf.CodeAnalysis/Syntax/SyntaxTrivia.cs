using OffDotNet.Pdf.CodeAnalysis.LexicalAnalysis;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

public readonly struct SyntaxTrivia
{
    internal SyntaxTrivia(SyntaxKind kind, string text, int width)
    {
        this.Kind = kind;
        this.Text = text;
        this.Width = width;
    }

    public SyntaxKind Kind { get; }

    public string Text { get; }

    public int Width { get; }
}
