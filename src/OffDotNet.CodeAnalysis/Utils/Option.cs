// <copyright file="Option.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Utils;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/// <summary>
/// The option type in is used when an actual value might not exist for a named value or variable.
/// An option has an underlying type and can hold a value of that type, or it might not have a value.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
[StructLayout(LayoutKind.Auto)]
public readonly struct Option<T>
    where T : notnull
{
    private readonly bool _isSome;
    private readonly T _value;

    /// <summary>Initializes a new instance of the <see cref="Option{T}"/> struct with no value.</summary>
    public Option()
    {
        _value = default!;
    }

    private Option(T value)
    {
        _value = value;
        _isSome = true;
    }

    /// <summary>Gets an option with no value.</summary>
    public static Option<T> None => new();

    public static implicit operator Option<T>(T value) => new(value);

    /// <summary>Creates an option with the specified value.</summary>
    /// <param name="value">The value to be stored in the option.</param>
    /// <returns>An option with the specified value.</returns>
    public static Option<T> Some(T value) => new(value);

    /// <summary>Determines whether the option has a value and returns that value if it exists.</summary>
    /// <param name="value">The returned value if the option has a value.</param>
    /// <returns><see langword="true"/> if the option has a value; otherwise, <see langword="false"/>.</returns>
    public bool IsSome([MaybeNullWhen(false)] out T value)
    {
        if (_isSome)
        {
            value = _value;
            return true;
        }

        value = default!;
        return false;
    }

    /// <summary>Returns a string that represents the current option.</summary>
    /// <returns>A string that represents the current option.</returns>
    public override string ToString() => _isSome ? $"Some({_value})" : "None";

    private string DebuggerDisplay() => ToString();
}
