// <copyright file="GreenNodeTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class GreenNodeTests
{
    [Fact(DisplayName = $"The {nameof(GreenNode.Flags)} must return {nameof(GreenNode.NodeFlags.None)} by default")]
    public void FlagsProperty_MustReturnNoneByDefault()
    {
        // Arrange
        GreenNode node = Substitute.For<GreenNode>(SyntaxKind.None);

        // Act
        GreenNode.NodeFlags actualFlags = node.Flags;

        // Assert
        Assert.Equal(GreenNode.NodeFlags.None, actualFlags);
    }

    [Fact(DisplayName = $"The SetFlags() method must bitwise set the {nameof(GreenNode.Flags)} property")]
    public void SetFlagsMethod_MustBitwiseSetFlagsProperty()
    {
        // Arrange
        GreenNode node = Substitute.For<GreenNode>(SyntaxKind.None);
        GreenNode.NodeFlags expectedFlags = GreenNode.NodeFlags.ContainsDiagnostics;

        // Act
        node.SetFlags(expectedFlags);
        GreenNode.NodeFlags actualFlags = node.Flags;

        // Assert
        Assert.Equal(expectedFlags, actualFlags);
    }

    [Fact(DisplayName = $"The ClearFlags() method must bitwise clear the {nameof(GreenNode.Flags)} property")]
    public void ClearFlagsMethod_MustBitwiseClearFlagsProperty()
    {
        // Arrange
        GreenNode node = Substitute.For<GreenNode>(SyntaxKind.None);
        GreenNode.NodeFlags expectedFlags = GreenNode.NodeFlags.ContainsDiagnostics;
        node.SetFlags(expectedFlags);

        // Act
        node.ClearFlags(expectedFlags);
        GreenNode.NodeFlags actualFlags = node.Flags;

        // Assert
        Assert.Equal(GreenNode.NodeFlags.None, actualFlags);
    }

    [Fact(DisplayName = $"The ContainsFlags() method must return true if the {nameof(GreenNode.Flags)} property contains the specified flags")]
    public void ContainsFlagsMethod_MustReturnTrueIfFlagsPropertyContainsSpecifiedFlags()
    {
        // Arrange
        GreenNode node = Substitute.For<GreenNode>(SyntaxKind.None);
        GreenNode.NodeFlags expectedFlags = GreenNode.NodeFlags.ContainsDiagnostics;
        node.SetFlags(expectedFlags);

        // Act
        bool actualResult = node.ContainsFlags(expectedFlags);

        // Assert
        Assert.True(actualResult);
    }
}
