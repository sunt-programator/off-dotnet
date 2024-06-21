// <copyright file="OptionExtensionsTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Tests.Utils;

using OffDotNet.CodeAnalysis.Utils;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
public class OptionExtensionsTests
{
    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Match() method should return the result of the first function if the option has a value")]
    public void Match_ShouldReturnFirstFunctionResultIfOptionHasValue()
    {
        // Arrange
        const int Expected = 42;
        var option = Option<int>.Some(Expected);

        // Act
        var actual = option.Match(
            some: static value => value * 2,
            none: static () => 0);

        // Assert
        Assert.Equal(Expected * 2, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Match() method should return the result of the second function if the option has no value")]
    public void Match_ShouldReturnSecondFunctionResultIfOptionHasNoValue()
    {
        // Arrange
        var option = Option<int>.None;

        // Act
        var actual = option.Match(
            some: static value => value * 2,
            none: static () => 0);

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Bind() method should return None if the option has no value")]
    public void Bind_ShouldReturnNoneIfOptionHasNoValue()
    {
        // Arrange
        var option = Option<int>.None;

        // Act
        var actual = option.Bind(static value => Option<int>.Some(value * 2));

        // Assert
        Assert.False(actual.IsSome(out _));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Bind() method should transform the value of the option if it has a value")]
    public void Bind_ShouldTransformValueIfOptionHasValue()
    {
        // Arrange
        const int Expected = 42;
        var option = Option<int>.Some(Expected);

        // Act
        var actual = option.Bind(static value => Option<int>.Some(value * 2));

        // Assert
        Assert.True(actual.IsSome(out var actualValue));
        Assert.Equal(Expected * 2, actualValue);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Select() method should return None if the option has no value")]
    public void Select_ShouldReturnNoneIfOptionHasNoValue()
    {
        // Arrange
        var option = Option<int>.None;

        // Act
        var actual = option.Select(static value => value * 2);

        // Assert
        Assert.False(actual.IsSome(out _));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Select() method should transform the value of the option if it has a value")]
    public void Select_ShouldTransformValueIfOptionHasValue()
    {
        // Arrange
        const int Expected = 42;
        var option = Option<int>.Some(Expected);

        // Act
        var actual = option.Select(static value => value * 2);

        // Assert
        Assert.True(actual.IsSome(out var actualValue));
        Assert.Equal(Expected * 2, actualValue);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Where() method should return None if the option has no value")]
    public void Where_ShouldReturnNoneIfOptionHasNoValue()
    {
        // Arrange
        var option = Option<int>.None;

        // Act
        var actual = option.Where(static value => value > 0);

        // Assert
        Assert.False(actual.IsSome(out _));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Where() method should return None if the predicate returns false")]
    public void Where_ShouldReturnNoneIfPredicateReturnsFalse()
    {
        // Arrange
        const int Expected = 42;
        var option = Option<int>.Some(Expected);

        // Act
        var actual = option.Where(static value => value < 0);

        // Assert
        Assert.False(actual.IsSome(out _));
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "Where() method should return the option if the predicate returns true")]
    public void Where_ShouldReturnOptionIfPredicateReturnsTrue()
    {
        // Arrange
        const int Expected = 42;
        var option = Option<int>.Some(Expected);

        // Act
        var actual = option.Where(static value => value > 0);

        // Assert
        Assert.True(actual.IsSome(out var actualValue));
        Assert.Equal(Expected, actualValue);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "GetValueOrDefault() method should return the default value if the option has no value")]
    public void DefaultValue_ShouldReturnDefaultValueIfOptionHasNoValue()
    {
        // Arrange
        const int Expected = 123;
        var option = Option<int>.None;

        // Act
        var actual = option.GetValueOrDefault(Expected);

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "GetValueOrDefault() method should return the value of the option if it has a value")]
    public void DefaultValue_ShouldReturnValueIfOptionHasValue()
    {
        // Arrange
        const int Expected = 42;
        var option = Option<int>.Some(Expected);

        // Act
        var actual = option.GetValueOrDefault(0);

        // Assert
        Assert.Equal(Expected, actual);
    }
}
