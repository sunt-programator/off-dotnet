using System.Runtime.CompilerServices;
using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

internal abstract class AbstractSyntaxToken
{
    private static readonly ConditionalWeakTable<AbstractSyntaxToken, DiagnosticInfo[]> DiagnosticsTable = new();

    protected AbstractSyntaxToken(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics)
    {
        this.Kind = kind;
        this.FullWidth = fullWidth;

        if (diagnostics?.Length > 0)
        {
            DiagnosticsTable.Add(this, diagnostics);
        }
    }

    public SyntaxKind Kind { get; }

    public virtual object? Value => string.Empty;

    public virtual string ValueText => string.Empty;

    public int FullWidth { get; }

    public override string ToString()
    {
        return string.Empty;
    }

    internal DiagnosticInfo[] GetDiagnostics()
    {
        return DiagnosticsTable.TryGetValue(this, out var diagnostics) ? diagnostics : Array.Empty<DiagnosticInfo>();
    }

    internal abstract AbstractSyntaxToken SetDiagnostics(DiagnosticInfo[]? diagnostics);
}
