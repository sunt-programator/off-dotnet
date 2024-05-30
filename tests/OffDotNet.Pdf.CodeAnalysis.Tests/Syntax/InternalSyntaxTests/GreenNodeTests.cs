// <copyright file="GreenNodeTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxFactory = CodeAnalysis.Syntax.InternalSyntax.SyntaxFactory;

[SuppressMessage("ReSharper", "GenericEnumeratorNotDisposed", Justification = "Test file")]
public class GreenNodeTests
{
    [SuppressMessage("Substitute creation", "NS2002:Constructor parameters count mismatch.", Justification = "False positive.")]
    private readonly GreenNode _node = Substitute.For<GreenNode>(SyntaxKind.None, null);

    [Fact(DisplayName = $"The {nameof(GreenNode.Flags)} property must return {nameof(NodeFlags.None)} by default")]
    public void FlagsProperty_MustReturnNoneByDefault()
    {
        // Arrange

        // Act
        var actualFlags = _node.Flags;

        // Assert
        Assert.Equal(NodeFlags.None, actualFlags);
    }

    [Fact(DisplayName = $"The SetFlags() method must bitwise set the {nameof(GreenNode.Flags)} property")]
    public void SetFlagsMethod_MustBitwiseSetFlagsProperty()
    {
        // Arrange
        var expectedFlags = NodeFlags.ContainsDiagnostics;

        // Act
        _node.SetFlags(expectedFlags);
        var actualFlags = _node.Flags;

        // Assert
        Assert.Equal(expectedFlags, actualFlags);
    }

    [Fact(DisplayName = $"The ClearFlags() method must bitwise clear the {nameof(GreenNode.Flags)} property")]
    public void ClearFlagsMethod_MustBitwiseClearFlagsProperty()
    {
        // Arrange
        var expectedFlags = NodeFlags.ContainsDiagnostics;
        _node.SetFlags(expectedFlags);

        // Act
        _node.ClearFlags(expectedFlags);
        var actualFlags = _node.Flags;

        // Assert
        Assert.Equal(NodeFlags.None, actualFlags);
    }

    [Fact(DisplayName = $"The ContainsFlags() method must return true if the {nameof(GreenNode.Flags)} property contains the specified flags")]
    public void ContainsFlagsMethod_MustReturnTrueIfFlagsPropertyContainsSpecifiedFlags()
    {
        // Arrange
        var expectedFlags = NodeFlags.ContainsDiagnostics;
        _node.SetFlags(expectedFlags);

        // Act
        var actualResult = _node.ContainsFlags(expectedFlags);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName =
        $"The GetDiagnostics() method must return an empty array if the {nameof(NodeFlags)} property does not contain {nameof(NodeFlags.ContainsDiagnostics)} flag")]
    public void GetDiagnosticsMethod_MustReturnEmptyArrayIfFlagsPropertyDoesNotContainContainsDiagnosticsFlag()
    {
        // Arrange

        // Act
        var actualResult = _node.GetDiagnostics();

        // Assert
        Assert.Empty(actualResult);
    }

    [Fact(DisplayName = $"The class must implement the {nameof(IReadOnlyList<GreenNode>)} interface")]
    public void Class_MustImplementIReadOnlyListInterface()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsAssignableFrom<IReadOnlyList<GreenNode>>(_node);
    }

    [Fact(DisplayName = "The GetEnumerator() method shoud return correct enumerator")]
    public void GetEnumeratorMethod_ShouldReturnCorrectEnumerator()
    {
        // Arrange

        // Act
        var enumerator1 = _node.GetEnumerator();
        var enumerator2 = ((IEnumerable<GreenNode>)_node).GetEnumerator();
        var enumerator3 = ((IEnumerable)_node).GetEnumerator();

        // Assert
        Assert.IsType<GreenNode.Enumerator>(enumerator1);
        Assert.IsType<GreenNode.Enumerator>(enumerator2);
        Assert.IsType<GreenNode.Enumerator>(enumerator3);
    }

    [Fact(DisplayName = $"The Indexer must throw a {nameof(ArgumentOutOfRangeException)} if the node is not a list")]
    public void Indexer_MustThrowArgumentOutOfRangeException_IfNodeIsNotList()
    {
        // Arrange

        // Act
        void Action() => _ = _node[0];

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Action);
    }

    [Theory(DisplayName = $"The Indexer must throw a {nameof(ArgumentOutOfRangeException)} if the index is out of range")]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(3)]
    [InlineData(10)]
    public void Indexer_MustThrowArgumentOutOfRangeException_IfIndexIsOutOfRange(int index)
    {
        // Arrange
        var node = SyntaxFactory.List(
            SyntaxFactory.Token(SyntaxKind.TrueKeyword),
            SyntaxFactory.Token(SyntaxKind.FalseKeyword),
            SyntaxFactory.Token(SyntaxKind.NullKeyword));

        // Act
        void Action() => _ = node[index];

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Action);
    }

    [Fact(DisplayName = "The Indexer must return the node at the specified index")]
    public void Indexer_MustReturnNodeAtSpecifiedIndex()
    {
        // Arrange
        var node = SyntaxFactory.List(
            SyntaxFactory.Token(SyntaxKind.TrueKeyword),
            SyntaxFactory.Token(SyntaxKind.FalseKeyword),
            SyntaxFactory.Token(SyntaxKind.NullKeyword));

        // Act
        var actual = node[1];

        // Assert
        Assert.Same(node.GetSlot(1), actual);
    }
}
