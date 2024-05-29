// <copyright file="AbstractDiagnostic.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

public abstract class AbstractDiagnostic : IEquatable<AbstractDiagnostic?>, IFormattable
{
    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        FormattableString formattable = $"";
        return formattable.ToString(formatProvider);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return string.Empty;
    }

    /// <inheritdoc/>
    public bool Equals(AbstractDiagnostic? other)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((AbstractDiagnostic)obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(AbstractDiagnostic? left, AbstractDiagnostic? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(AbstractDiagnostic? left, AbstractDiagnostic? right)
    {
        return !Equals(left, right);
    }
}
