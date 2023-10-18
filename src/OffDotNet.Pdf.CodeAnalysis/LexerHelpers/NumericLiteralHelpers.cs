using System.Buffers;
using System.Globalization;
using System.Text;
using OffDotNet.Pdf.CodeAnalysis.Parser;
using OffDotNet.Pdf.CodeAnalysis.Syntax;

namespace OffDotNet.Pdf.CodeAnalysis.LexerHelpers;

internal static class NumericLiteralHelpers
{
    public static bool TryScanNumericLiteral(InputReader reader, StringBuilder stringBuilder, ref TokenInfo info)
    {
        info.Text = string.Empty;
        info.ValueKind = TokenInfoSpecialKind.None;
        stringBuilder.Clear();

        ScanNumericLiteralInteger(reader, stringBuilder);
        ScanNumericLiteralIntegerWithDecimalPoint(reader, stringBuilder, ref info);

        info.Kind = SyntaxKind.NumericLiteralToken;
        info.Text = stringBuilder.ToString();

        switch (info.ValueKind)
        {
            case TokenInfoSpecialKind.Single:
                info.FloatValue = float.Parse(info.Text, CultureInfo.InvariantCulture);
                break;
            default:
                info.ValueKind = TokenInfoSpecialKind.Int32;
                info.IntValue = int.Parse(info.Text, CultureInfo.InvariantCulture);
                break;
        }

        return true;
    }

    private static void ScanNumericLiteralIntegerWithDecimalPoint(InputReader reader, StringBuilder stringBuilder, ref TokenInfo info)
    {
        byte? peekedByte = reader.Peek();
        if (peekedByte != 0x2e)
        {
            return;
        }

        info.ValueKind = TokenInfoSpecialKind.Single;
        stringBuilder.Append((char)peekedByte.Value);
        reader.AdvanceByte();

        byte? peekedByte2 = reader.Peek();
        if (peekedByte2.HasValue && peekedByte2.Value.IsDecDigit())
        {
            ScanNumericLiteralInteger(reader, stringBuilder);
        }
    }

    private static void ScanNumericLiteralInteger(InputReader reader, StringBuilder stringBuilder)
    {
        while (true)
        {
            byte? peekedByte = reader.Peek();

            if (!peekedByte.HasValue || !peekedByte.IsDecDigit())
            {
                break;
            }

            stringBuilder.Append((char)peekedByte.Value);
            reader.AdvanceByte();
        }
    }
}
