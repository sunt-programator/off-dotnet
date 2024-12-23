﻿// <copyright file="OptionTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Tests.Utils;

using OffDotNet.CodeAnalysis.Utils;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
public class OptionTests
{
    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"ToString() method should return 'None' for the default instance of {nameof(Option<int>)}")]
    public void ToString_ShouldReturnNoneForDefaultInstance()
    {
        // Arrange
        var option = default(Option<int>);

        // Act
        var actual = option.ToString();

        // Assert
        Assert.Equal("None", actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = "ToString() method should return 'Some(42)' if the value is set to 42")]
    public void ToString_ShouldReturnSomeValueIfSet()
    {
        // Arrange
        const int Expected = 42;
        Option<int> option = Expected;

        // Act
        var actual = option.ToString();

        // Assert
        Assert.Equal($"Some({Expected})", actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(Option<int>.None)} property should return a default instance of {nameof(Option<int>)}")]
    public void None_ShouldReturnDefaultInstance()
    {
        // Arrange
        var option = Option<int>.None;

        // Act
        var actual = option.TryGetValue(out var value);

        // Assert
        Assert.False(actual);
        Assert.Equal(default, value);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(Option<int>.Some)} method should return an instance of {nameof(Option<int>)} with the specified value")]
    public void Some_ShouldReturnInstanceWithValue()
    {
        // Arrange
        const int Expected = 42;
        var option = Option<int>.Some(Expected);

        // Act
        var actual = option.TryGetValue(out var value);

        // Assert
        Assert.True(actual);
        Assert.Equal(Expected, value);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(Option<int>.TryGetValue)} method should return false for the default instance of {nameof(Option<int>)}")]
    public void TryGetValue_ShouldReturnFalseForDefaultInstance()
    {
        // Arrange
        var option = default(Option<int>);

        // Act
        var actual = option.TryGetValue(out var value);

        // Assert
        Assert.False(actual);
        Assert.Equal(default, value);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(Option<int>.TryGetValue)} method should return true for the instance of {nameof(Option<int>)} with value set")]
    public void TryGetValue_ShouldReturnTrueForInstanceWithValueSet()
    {
        // Arrange
        const int Expected = 42;
        Option<int> option = Expected;

        // Act
        var actual = option.TryGetValue(out var value);

        // Assert
        Assert.True(actual);
        Assert.Equal(Expected, value);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(Option<int>.IsSome)} property should return false for the default instance of {nameof(Option<int>)}")]
    public void IsSome_ShouldReturnFalseForDefaultInstance()
    {
        // Arrange
        var option = default(Option<int>);

        // Act
        var actual = option.IsSome;

        // Assert
        Assert.False(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/335")]
    [Fact(DisplayName = $"{nameof(Option<int>.IsSome)} property should return true for the instance of {nameof(Option<int>)} with value set")]
    public void IsSome_ShouldReturnTrueForInstanceWithValueSet()
    {
        // Arrange
        const int Expected = 42;
        Option<int> option = Expected;

        // Act
        var actual = option.IsSome;

        // Assert
        Assert.True(actual);
    }
}
