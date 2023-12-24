// <copyright file="LocalizableString.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

[SuppressMessage("Sonar", "S4035:Classes implementing IComparable<T> should be sealed", Justification = "Reviewed.")]
public abstract partial class LocalizableString : IFormattable, IEquatable<LocalizableString>
{
    /// <summary>
    /// Fired when an exception is raised by any of the public methods of <see cref="LocalizableString"/>.
    /// If the exception handler itself throws an exception, that exception is ignored.
    /// </summary>
    public event EventHandler<Exception>? OnException;

    /// <summary>Gets a value indicating whether the <see cref="LocalizableString"/> can throw exceptions.</summary>
    internal virtual bool CanThrowExceptions => true;

    public static explicit operator string(LocalizableString localizableResource)
    {
        return localizableResource.ToString(null);
    }

    public static implicit operator LocalizableString(string? fixedResource)
    {
        return FixedLocalizableString.Create(fixedResource);
    }

    public static bool operator ==(LocalizableString? left, LocalizableString? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(LocalizableString? left, LocalizableString? right)
    {
        return !Equals(left, right);
    }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>The string value of the current instance.</returns>
    public sealed override string ToString()
    {
        return this.ToString(null);
    }

    /// <summary>Formats the value of the current instance using the optionally specified format.</summary>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(IFormatProvider? formatProvider)
    {
        try
        {
            return this.GetText(formatProvider);
        }
        catch (Exception ex)
        {
            this.RaiseOnException(ex);
            return string.Empty;
        }
    }

    string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
    {
        return this.GetText(formatProvider);
    }

    public bool Equals(LocalizableString? other)
    {
        return this.Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        try
        {
            return this.AreEqual(obj);
        }
        catch (Exception ex)
        {
            this.RaiseOnException(ex);
            return false;
        }
    }

    /// <summary>Gets the hash code for this instance.</summary>
    /// <returns>A hash code for the current object.</returns>
    public sealed override int GetHashCode()
    {
        try
        {
            return this.GetHash();
        }
        catch (Exception ex)
        {
            this.RaiseOnException(ex);
            return 0;
        }
    }

    /// <summary>
    /// Formats the value of the current instance using the optionally specified format.
    /// Provides the implementation of ToString.
    /// ToString will provide a default value if this method throws an exception.
    /// </summary>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    protected abstract string GetText(IFormatProvider? formatProvider);

    /// <summary>Gets the hash code for this instance.</summary>
    /// <returns>A hash code for the current object.</returns>
    protected abstract int GetHash();

    /// <summary>Provides the implementation of Equals. Equals will provide a default value if this method throws an exception.</summary>
    /// <param name="other">The object to compare with the current instance.</param>
    /// <returns>True if the objects are equal, false otherwise.</returns>
    protected abstract bool AreEqual(object? other);

    private void RaiseOnException(Exception ex)
    {
        if (ex is OperationCanceledException)
        {
            return;
        }

        try
        {
            this.OnException?.Invoke(this, ex);
        }
        catch
        {
            // Ignore exceptions from the exception handlers themselves.
        }
    }
}
