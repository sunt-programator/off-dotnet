// <copyright file="OptionExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Utils;

/// <summary>
/// Provides extension methods for the <see cref="Option{T}"/> type.
/// </summary>
public static class OptionExtensions
{
    /// <summary>
    /// Matches the value of an <see cref="Option{T}"/> and executes the corresponding function.
    /// </summary>
    /// <typeparam name="TIn">The type of the value contained in the <see cref="Option{T}"/>.</typeparam>
    /// <typeparam name="TOut">The return type of the provided functions.</typeparam>
    /// <param name="option">The <see cref="Option{T}"/> instance to match against.</param>
    /// <param name="some">The function to execute if the option is <see cref="Option{T}.Some"/>.</param>
    /// <param name="none">The function to execute if the option is <see cref="Option{T}.None"/>.</param>
    /// <returns>The result of either <paramref name="some"/> or <paramref name="none"/>.</returns>
    /// <remarks>
    /// <para>
    /// This method is preferred over using <see cref="Option{T}.IsSome"/> directly because it enforces handling
    /// of both the <see cref="Option{T}.Some"/> and <see cref="Option{T}.None"/> cases, ensuring that no case is missed.
    /// </para>
    /// <para>
    /// Use <see cref="Option{T}.IsSome"/> when you do not need to return a value but want to perform side effects instead.
    /// </para>
    /// </remarks>
    public static TOut Match<TIn, TOut>(this Option<TIn> option, Func<TIn, TOut> some, Func<TOut> none)
        where TIn : notnull
    {
        return option.IsSome(out var value) ? some(value) : none();
    }

    /// <summary>
    /// Binds the value of an <see cref="Option{TIn}"/> to a function that returns an <see cref="Option{TOut}"/>.
    /// </summary>
    /// <typeparam name="TIn">The type of the value contained in the input <see cref="Option{TIn}"/>.</typeparam>
    /// <typeparam name="TOut">The type of the value contained in the output <see cref="Option{TOut}"/>.</typeparam>
    /// <param name="option">The input <see cref="Option{TIn}"/> instance.</param>
    /// <param name="binder">
    /// A function that takes a value of type <typeparamref name="TIn"/> and returns an <see cref="Option{TOut}"/>.
    /// This function is called if the option is <see cref="Option{TIn}.Some"/>.
    /// </param>
    /// <returns>
    /// An <see cref="Option{TOut}"/>. If the input option is <see cref="Option{TIn}.Some"/>, the result of
    /// the <paramref name="binder"/> function is returned. If the input option is <see cref="Option{TIn}.None"/>,
    /// <see cref="Option{TOut}.None"/> is returned.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The <see cref="Bind{TIn,TOut}"/> method is used to chain operations that return options. It is useful for
    /// composing multiple computations that may fail, without having to check for the presence of a value
    /// at each step.
    /// </para>
    /// <para>
    /// If the input option is <see cref="Option{TIn}.Some"/>, the <paramref name="binder"/> function is called with
    /// the value of the option as its parameter, and the result of the function (which is an <see cref="Option{TOut}"/>)
    /// is returned. If the input option is <see cref="Option{TIn}.None"/>, the <paramref name="binder"/> function
    /// is not called, and <see cref="Option{TOut}.None"/> is returned.
    /// </para>
    /// </remarks>
    public static Option<TOut> Bind<TIn, TOut>(this Option<TIn> option, Func<TIn, Option<TOut>> binder)
        where TIn : notnull
        where TOut : notnull
    {
        return option.Match(
            some: binder,
            none: () => Option<TOut>.None);
    }

    /// <summary>
    /// Projects the value of an <see cref="Option{TIn}"/> into a new form using the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The type of the value contained in the input <see cref="Option{TIn}"/>.</typeparam>
    /// <typeparam name="TOut">The type of the value contained in the output <see cref="Option{TOut}"/>.</typeparam>
    /// <param name="option">The input <see cref="Option{TIn}"/> instance.</param>
    /// <param name="mapper">
    /// A function that takes a value of type <typeparamref name="TIn"/> and returns a value of type <typeparamref name="TOut"/>.
    /// This function is called if the option is <see cref="Option{TIn}.Some"/>.
    /// </param>
    /// <returns>
    /// An <see cref="Option{TOut}"/>. If the input option is <see cref="Option{TIn}.Some"/>, the result of
    /// the <paramref name="mapper"/> function wrapped in an <see cref="Option{TOut}.Some"/> is returned. If the input option
    /// is <see cref="Option{TIn}.None"/>, <see cref="Option{TOut}.None"/> is returned.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The <see cref="Select{TIn,TOut}"/> method is similar to <see cref="Bind{TIn,TOut}"/>, but it wraps the result of the <paramref name="mapper"/>
    /// function in an <see cref="Option{TOut}.Some"/> before returning it. This method is useful for transforming the value
    /// inside an option while maintaining the option type.
    /// </para>
    /// <para>
    /// If the input option is <see cref="Option{TIn}.Some"/>, the <paramref name="mapper"/> function is called with the value
    /// of the option as its parameter, and the result of the function is wrapped in an <see cref="Option{TOut}.Some"/> and returned.
    /// If the input option is <see cref="Option{TIn}.None"/>, the <paramref name="mapper"/> function is not called, and
    /// <see cref="Option{TOut}.None"/> is returned.
    /// </para>
    /// </remarks>
    public static Option<TOut> Select<TIn, TOut>(this Option<TIn> option, Func<TIn, TOut> mapper)
        where TIn : notnull
        where TOut : notnull
    {
        return option.Bind(value => Option<TOut>.Some(mapper(value)));
    }

    /// <summary>
    /// Filters the value of an <see cref="Option{T}"/> based on a predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Option{T}"/>.</typeparam>
    /// <param name="option">The input <see cref="Option{T}"/> instance.</param>
    /// <param name="predicate">
    /// A predicate function that takes a value of type <typeparamref name="T"/> and returns a boolean.
    /// The predicate is called if the option is <see cref="Option{T}.Some"/>.
    /// </param>
    /// <returns>
    /// An <see cref="Option{T}"/>. If the input option is <see cref="Option{T}.Some"/> and the <paramref name="predicate"/> returns
    /// <c>true</c>, the same option is returned. If the predicate returns <c>false</c>, <see cref="Option{T}.None"/> is returned.
    /// If the input option is <see cref="Option{T}.None"/>, <see cref="Option{T}.None"/> is returned.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The <see cref="Where{T}"/> method is used to filter the value inside an option based on a predicate. It allows you
    /// to conditionally include or exclude the value in the resulting option.
    /// </para>
    /// <para>
    /// If the input option is <see cref="Option{T}.Some"/> and the <paramref name="predicate"/> returns <c>true</c>, the same option
    /// is returned. If the predicate returns <c>false</c>, <see cref="Option{T}.None"/> is returned. If the input option is
    /// <see cref="Option{T}.None"/>, the <paramref name="predicate"/> function is not called, and <see cref="Option{T}.None"/> is returned.
    /// </para>
    /// </remarks>
    public static Option<T> Where<T>(this Option<T> option, Predicate<T> predicate)
        where T : notnull
    {
        return option.Bind(value => predicate(value) ? option : Option<T>.None);
    }

    /// <summary>
    /// Returns the value of an <see cref="Option{T}"/> if it is <see cref="Option{T}.Some"/>, otherwise returns a specified default value.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Option{T}"/>.</typeparam>
    /// <param name="option">The input <see cref="Option{T}"/> instance.</param>
    /// <param name="defaultValue">The default value to return if the option is <see cref="Option{T}.None"/>.</param>
    /// <returns>
    /// The value contained in the option if it is <see cref="Option{T}.Some"/>, otherwise the specified <paramref name="defaultValue"/>.
    /// </returns>
    /// <remarks>
    /// The <see cref="GetValueOrDefault{T}"/> method is used to provide a fallback value in case the option is <see cref="Option{T}.None"/>.
    /// This is useful for ensuring that a non-null value is returned without having to manually check the state of the option.
    /// </remarks>
    public static T GetValueOrDefault<T>(this Option<T> option, T defaultValue)
        where T : notnull
    {
        return option.Match(
            some: value => value,
            none: () => defaultValue);
    }
}
