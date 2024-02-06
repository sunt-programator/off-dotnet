// <copyright file="GreenNodeTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Diagnostic;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;

public class GreenNodeTests
{
    [SuppressMessage("Substitute creation", "NS2002:Constructor parameters count mismatch.", Justification = "False positive.")]
    private readonly GreenNode node = Substitute.For<GreenNode>(SyntaxKind.None, null);

    [Fact(DisplayName = $"The {nameof(GreenNode.Flags)} property must return {nameof(GreenNode.NodeFlags.None)} by default")]
    public void FlagsProperty_MustReturnNoneByDefault()
    {
        // Arrange

        // Act
        GreenNode.NodeFlags actualFlags = this.node.Flags;

        // Assert
        Assert.Equal(GreenNode.NodeFlags.None, actualFlags);
    }

    [Fact(DisplayName = $"The SetFlags() method must bitwise set the {nameof(GreenNode.Flags)} property")]
    public void SetFlagsMethod_MustBitwiseSetFlagsProperty()
    {
        // Arrange
        GreenNode.NodeFlags expectedFlags = GreenNode.NodeFlags.ContainsDiagnostics;

        // Act
        this.node.SetFlags(expectedFlags);
        GreenNode.NodeFlags actualFlags = this.node.Flags;

        // Assert
        Assert.Equal(expectedFlags, actualFlags);
    }

    [Fact(DisplayName = $"The ClearFlags() method must bitwise clear the {nameof(GreenNode.Flags)} property")]
    public void ClearFlagsMethod_MustBitwiseClearFlagsProperty()
    {
        // Arrange
        GreenNode.NodeFlags expectedFlags = GreenNode.NodeFlags.ContainsDiagnostics;
        this.node.SetFlags(expectedFlags);

        // Act
        this.node.ClearFlags(expectedFlags);
        GreenNode.NodeFlags actualFlags = this.node.Flags;

        // Assert
        Assert.Equal(GreenNode.NodeFlags.None, actualFlags);
    }

    [Fact(DisplayName = $"The ContainsFlags() method must return true if the {nameof(GreenNode.Flags)} property contains the specified flags")]
    public void ContainsFlagsMethod_MustReturnTrueIfFlagsPropertyContainsSpecifiedFlags()
    {
        // Arrange
        GreenNode.NodeFlags expectedFlags = GreenNode.NodeFlags.ContainsDiagnostics;
        this.node.SetFlags(expectedFlags);

        // Act
        bool actualResult = this.node.ContainsFlags(expectedFlags);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName =
        $"The GetDiagnostics() method must return an empty array if the {nameof(GreenNode.NodeFlags)} property does not contain {nameof(GreenNode.NodeFlags.ContainsDiagnostics)} flag")]
    public void GetDiagnosticsMethod_MustReturnEmptyArrayIfFlagsPropertyDoesNotContainContainsDiagnosticsFlag()
    {
        // Arrange

        // Act
        DiagnosticInfo[] actualResult = this.node.GetDiagnostics();

        // Assert
        Assert.Empty(actualResult);
    }
}
