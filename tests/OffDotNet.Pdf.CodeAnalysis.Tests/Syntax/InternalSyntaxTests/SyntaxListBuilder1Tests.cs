// <copyright file="SyntaxListBuilder1Tests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

using OffDotNet.Pdf.CodeAnalysis.Collections;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;

public class SyntaxListBuilder1Tests
{
    [Fact(DisplayName = $"The {nameof(SyntaxListBuilder<GreenNode>.IsNull)} property must be true for the default struct.")]
    public void IsNullProperty_DefaultStruct_MustBeTrue()
    {
        // Arrange

        // Act
        SyntaxListBuilder<GreenNode> syntaxListBuilder = default;
        bool isNull = syntaxListBuilder.IsNull;

        // Assert
        Assert.True(isNull);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxListBuilder<GreenNode>.IsNull)} property must be false if the struct was initialized.")]
    public void IsNullProperty_MustBeFalse()
    {
        // Arrange

        // Act
        SyntaxListBuilder<GreenNode> syntaxListBuilder = new(0);
        bool isNull = syntaxListBuilder.IsNull;

        // Assert
        Assert.False(isNull);
    }

    [Fact(DisplayName = $"The {nameof(SyntaxListBuilder.Count)} property must be zero by default.")]
    public void CountProperty_MustBeZeroByDefault()
    {
        // Arrange
        const int capacity = 3;

        // Act
        SyntaxListBuilder<GreenNode> syntaxListBuilder = new(capacity);
        int actualCount = syntaxListBuilder.Count;

        // Assert
        Assert.Equal(0, actualCount);
    }

    [Fact(DisplayName = "The indexer must get and set the node at the given index.")]
    public void Index_MustReturnCorrectNode()
    {
        // Arrange
        const int capacity = 3;
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        SyntaxToken nullKeyword = SyntaxFactory.Token(SyntaxKind.NullKeyword);

        SyntaxListBuilder syntaxListBuilder = new(capacity);
        syntaxListBuilder.AddRange(new GreenNode[] { trueKeyword, falseKeyword, nullKeyword });

        SyntaxListBuilder<GreenNode> builder = new(syntaxListBuilder);

        // Act
        GreenNode node1 = builder[0];
        GreenNode node2 = builder[1];
        GreenNode node3 = builder[2];

        // Assert
        Assert.Equal(trueKeyword, node1);
        Assert.Equal(falseKeyword, node2);
        Assert.Equal(nullKeyword, node3);
    }

    [Fact(DisplayName = $"The Add() method with null argument must not increment the {nameof(SyntaxListBuilder.Count)} property.")]
    public void AddMethod_NullArgument_MustNotIncrementCount()
    {
        // Arrange

        // Act
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();
        syntaxListBuilder.Add(null);
        int actualCount = syntaxListBuilder.Count;

        // Assert
        Assert.Equal(0, actualCount);
    }

    [Fact(DisplayName = "The Add() method must add a node.")]
    public void AddMethod_MustAddANode()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);

        // Act
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();
        syntaxListBuilder.Add(trueKeyword);

        // Assert
        Assert.Equal(trueKeyword, syntaxListBuilder[0]);
    }

    [Fact(DisplayName = $"The Add() method must increment the {nameof(SyntaxListBuilder.Count)} property.")]
    public void AddMethod_MustIncrementCount()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);

        // Act
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();
        syntaxListBuilder.Add(trueKeyword);
        int actualCount = syntaxListBuilder.Count;

        // Assert
        Assert.Equal(1, actualCount);
    }

    [Fact(DisplayName = $"The Add() method must flat the list in a green node with kind={nameof(SyntaxKind.List)}.")]
    public void AddMethod_ListNode_MustFlatList()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        SyntaxToken nullKeyword = SyntaxFactory.Token(SyntaxKind.NullKeyword);
        GreenNode listNode = SyntaxFactory.List(trueKeyword, falseKeyword, nullKeyword);

        // Act
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();
        syntaxListBuilder.Add(listNode);
        int actualCount = syntaxListBuilder.Count;

        // Assert
        Assert.Equal(trueKeyword, syntaxListBuilder[0]);
        Assert.Equal(falseKeyword, syntaxListBuilder[1]);
        Assert.Equal(nullKeyword, syntaxListBuilder[2]);
        Assert.Equal(3, actualCount);
    }

    [Fact(DisplayName = "The AddRange() method with offset and length must add a list of nodes.")]
    public void AddRangeMethod_WithOffsetAndLength1_MustAddAListOfNodes()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        GreenNode[] items = { trueKeyword, falseKeyword };
        const int offset = 0;
        const int length = 2;

        // Act
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();
        syntaxListBuilder.AddRange(items, offset, length);

        // Assert
        Assert.Equal(trueKeyword, syntaxListBuilder[0]);
        Assert.Equal(falseKeyword, syntaxListBuilder[1]);
    }

    [Fact(DisplayName = "The AddRange() method with offset and length must add a list of nodes.")]
    public void AddRangeMethod_WithOffsetAndLength4_MustAddAListOfNodes()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        ArrayElement<GreenNode>[] items = new ArrayElement<GreenNode>[2];
        items[0].Value = trueKeyword;
        items[1].Value = falseKeyword;

        SyntaxList<GreenNode> syntaxList = new(SyntaxFactory.List(items));

        // Act
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();
        syntaxListBuilder.AddRange(syntaxList);

        // Assert
        Assert.Equal(trueKeyword, syntaxListBuilder[0]);
        Assert.Equal(falseKeyword, syntaxListBuilder[1]);
    }

    [Fact(DisplayName = $"The Clear() method must reset the {nameof(SyntaxListBuilder.Count)} property.")]
    public void ClearMethod_MustResetCountProperty()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();

        // Act
        syntaxListBuilder.Add(trueKeyword);
        Assert.Equal(1, syntaxListBuilder.Count);

        syntaxListBuilder.Clear();
        int actualCount = syntaxListBuilder.Count;

        // Assert
        Assert.Equal(0, actualCount);
    }

    [Fact(DisplayName = "The Any() method must return false if the list does not contain the given kind.")]
    public void AnyMethod_NonExistingKind_MustReturnFalse()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        ArrayElement<GreenNode>[] items = new ArrayElement<GreenNode>[2];
        items[0].Value = trueKeyword;
        items[1].Value = falseKeyword;

        SyntaxList<GreenNode> syntaxList = new(SyntaxFactory.List(items));
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();

        // Act
        syntaxListBuilder.AddRange(syntaxList);
        bool actualResult = syntaxListBuilder.Any(SyntaxKind.IndirectReference);

        // Assert
        Assert.False(actualResult);
    }

    [Fact(DisplayName = "The Any() method must return true if the list contains the given kind.")]
    public void AnyMethod_ExistingKind_MustReturnTrue()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        ArrayElement<GreenNode>[] items = new ArrayElement<GreenNode>[2];
        items[0].Value = trueKeyword;
        items[1].Value = falseKeyword;

        SyntaxList<GreenNode> syntaxList = new(SyntaxFactory.List(items));
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();

        // Act
        syntaxListBuilder.AddRange(syntaxList);
        bool actualResult = syntaxListBuilder.Any(SyntaxKind.TrueKeyword);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = $"The ToListNode() method with {nameof(SyntaxListBuilder.Count)}=0 must return null.")]
    public void ToListNodeMethod_Count0_MustReturnNull()
    {
        // Arrange
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();

        // Act
        GreenNode? actualListNode = syntaxListBuilder.ToListNode();

        // Assert
        Assert.Null(actualListNode);
    }

    [Fact(DisplayName = $"The ToListNode() method with {nameof(SyntaxListBuilder.Count)}=1 must return a single node.")]
    public void ToListNodeMethod_Count1_MustReturnSingleNode()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();

        // Act
        syntaxListBuilder.Add(trueKeyword);
        GreenNode? actualListNode = syntaxListBuilder.ToListNode();

        // Assert
        Assert.Equal(trueKeyword, actualListNode);
    }

    [Fact(DisplayName = $"The ToListNode() method with {nameof(SyntaxListBuilder.Count)}=2 must return a syntax list.")]
    public void ToListNodeMethod_Count2_MustReturnSyntaxList()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();

        // Act
        syntaxListBuilder.Add(trueKeyword);
        syntaxListBuilder.Add(falseKeyword);
        GreenNode? actualListNode = syntaxListBuilder.ToListNode();

        // Assert
        Assert.NotNull(actualListNode);
        Assert.Equal(trueKeyword, actualListNode.GetSlot(0));
        Assert.Equal(falseKeyword, actualListNode.GetSlot(1));
    }

    [Fact(DisplayName = "The ToListNode() method must return a syntax list.")]
    public void ToListMethod_MustReturnSyntaxList()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();

        // Act
        syntaxListBuilder.Add(trueKeyword);
        syntaxListBuilder.Add(falseKeyword);
        SyntaxList<GreenNode> actualListNode = syntaxListBuilder.ToList();

        // Assert
        Assert.Equal(2, actualListNode.Count);
        Assert.Equal(trueKeyword, actualListNode[0]);
        Assert.Equal(falseKeyword, actualListNode[1]);
    }

    [Fact(DisplayName = $"The implicit operator must convert the {nameof(SyntaxListBuilder<GreenNode>)} struct to {nameof(SyntaxListBuilder)} class.")]
    public void ImplicitOperator_MustConvertToClass()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();

        // Act
        syntaxListBuilder.Add(trueKeyword);
        syntaxListBuilder.Add(falseKeyword);
        SyntaxListBuilder actualSyntaxListBuilder = syntaxListBuilder;

        // Assert
        Assert.Equal(2, actualSyntaxListBuilder.Count);
        Assert.Equal(trueKeyword, actualSyntaxListBuilder[0]);
        Assert.Equal(falseKeyword, actualSyntaxListBuilder[1]);
    }

    [Fact(DisplayName = $"The implicit operator must convert the {nameof(SyntaxListBuilder<GreenNode>)} struct to {nameof(SyntaxList<GreenNode>)}.")]
    public void ImplicitOperator_MustConvertToSyntaxList()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        SyntaxListBuilder<GreenNode> syntaxListBuilder = SyntaxListBuilder<GreenNode>.Create();

        // Act
        syntaxListBuilder.Add(trueKeyword);
        syntaxListBuilder.Add(falseKeyword);
        SyntaxList<GreenNode> actualSyntaxListBuilder = syntaxListBuilder;

        // Assert
        Assert.Equal(2, actualSyntaxListBuilder.Count);
        Assert.Equal(trueKeyword, actualSyntaxListBuilder[0]);
        Assert.Equal(falseKeyword, actualSyntaxListBuilder[1]);
    }
}
