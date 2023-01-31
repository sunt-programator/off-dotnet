using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Off.Net.Pdf.Core.Extensions;

internal static class GuardClauses
{
    public static void NotNull<T>([NotNull] T? value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    public static T CheckConstraints<T>(this T value, Func<T, bool> checkFunc, string exceptionMessage, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (!checkFunc(value))
        {
            throw new ArgumentOutOfRangeException(paramName, exceptionMessage);
        }
        return value;
    }
}
