using OffDotNet.Pdf.CodeAnalysis.Errors;

namespace OffDotNet.Pdf.CodeAnalysis.Syntax;

internal static class AbstractSyntaxTokenExtensions
{
    public static TNode WithDiagnostics<TNode>(this TNode node, DiagnosticInfo[]? diagnostics)
        where TNode : AbstractSyntaxToken
    {
        return (TNode)node.SetDiagnostics(diagnostics);
    }
}
