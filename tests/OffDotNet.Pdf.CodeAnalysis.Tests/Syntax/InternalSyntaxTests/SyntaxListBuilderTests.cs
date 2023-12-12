// <copyright file="SyntaxListBuilderTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using OffDotNet.Pdf.CodeAnalysis.Syntax;
using OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax;
using SyntaxToken = OffDotNet.Pdf.CodeAnalysis.Syntax.InternalSyntax.SyntaxToken;

namespace OffDotNet.Pdf.CodeAnalysis.Tests.Syntax.InternalSyntaxTests;

public class SyntaxListBuilderTests
{
    [Fact(DisplayName = "The constructor must initialize the nodes with the given capacity.")]
    public void NodesField_MustHaveTheCapacityAssignedFromConstructor()
    {
        // Arrange
        const int capacity = 3;
        SyntaxListBuilder syntaxListBuilder = new(capacity);

        // Act
        GreenNode? node = syntaxListBuilder[capacity - 1];

        // Assert
        Assert.Null(node); // Null instead of IndexOutOfRangeException
    }

    [Fact(DisplayName = $"The {nameof(SyntaxListBuilder.Count)} property must be zero by default.")]
    public void CountProperty_MustBeZeroByDefault()
    {
        // Arrange
        const int capacity = 3;
        SyntaxListBuilder syntaxListBuilder = new(capacity);

        // Act
        int actualCount = syntaxListBuilder.Count;

        // Assert
        Assert.Equal(0, actualCount);
    }

    [Fact(DisplayName = "The Create() method must set the capacity to 8.")]
    public void CreateMethod_MustSetTheCapacityTo8()
    {
        // Arrange
        const int capacity = 8;
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        GreenNode? node = syntaxListBuilder[capacity - 1];

        // Assert
        Assert.Null(node); // Null instead of IndexOutOfRangeException
    }

    [Fact(DisplayName = "The indexer must get and set the node at the given index.")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Evident", Justification = "Test code")]
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer", Justification = "Test code")]
    public void Index_MustReturnCorrectNode()
    {
        // Arrange
        const int capacity = 3;
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);

        SyntaxListBuilder syntaxListBuilder = new(capacity);
        syntaxListBuilder.Add(trueKeyword);
        syntaxListBuilder.Add(trueKeyword);
        syntaxListBuilder.Add(trueKeyword);

        // Act
        GreenNode? node = syntaxListBuilder[2];

        // Assert
        Assert.Equal(trueKeyword, node);
    }

    [Fact(DisplayName = "The Add() method with null argument must not add a node.")]
    public void AddMethod_NullArgument_MustNotAddANode()
    {
        // Arrange
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        syntaxListBuilder.Add(null);

        // Assert
        Assert.Null(syntaxListBuilder[0]);
    }

    [Fact(DisplayName = "The Add() method must add a node.")]
    public void AddMethod_MustAddANode()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        syntaxListBuilder.Add(trueKeyword);

        // Assert
        Assert.Equal(trueKeyword, syntaxListBuilder[0]);
    }

    [Fact(DisplayName = $"The Add() method must increment the {nameof(SyntaxListBuilder.Count)} property.")]
    public void AddMethod_MustIncrementCountProperty()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();
        syntaxListBuilder.Add(trueKeyword);

        // Act
        int actualCount = syntaxListBuilder.Count;

        // Assert
        Assert.Equal(1, actualCount);
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

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        syntaxListBuilder.AddRange(items, offset, length);

        // Assert
        Assert.Equal(trueKeyword, syntaxListBuilder[0]);
        Assert.Equal(falseKeyword, syntaxListBuilder[1]);
    }

    [Fact(DisplayName = "The AddRange() method with offset and length must add a list of nodes.")]
    public void AddRangeMethod_WithOffsetAndLength2_MustAddAListOfNodes()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        GreenNode[] items = { trueKeyword, falseKeyword };
        const int offset = 0;
        const int length = 1;

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        syntaxListBuilder.AddRange(items, offset, length);

        // Assert
        Assert.Equal(trueKeyword, syntaxListBuilder[0]);
        Assert.Null(syntaxListBuilder[1]);
    }

    [Fact(DisplayName = "The AddRange() method with offset and length must add a list of nodes.")]
    public void AddRangeMethod_WithOffsetAndLength3_MustAddAListOfNodes()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        GreenNode[] items = { trueKeyword, falseKeyword };
        const int offset = 1;
        const int length = 1;

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        syntaxListBuilder.AddRange(items, offset, length);

        // Assert
        Assert.Equal(falseKeyword, syntaxListBuilder[0]);
        Assert.Null(syntaxListBuilder[1]);
    }

    [Fact(DisplayName = "The AddRange() method with offset and length must add a list of nodes.")]
    public void AddRangeMethod_WithOffsetAndLength4_MustAddAListOfNodes()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        GreenNode[] items = { trueKeyword, falseKeyword };

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        syntaxListBuilder.AddRange(items);

        // Assert
        Assert.Equal(trueKeyword, syntaxListBuilder[0]);
        Assert.Equal(falseKeyword, syntaxListBuilder[1]);
    }

    [Fact(DisplayName = "The Add() method must ensure additional capacity.")]
    public void AddMethod_MustEnsureAdditionalCapacity()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxListBuilder syntaxListBuilder = new(1);
        syntaxListBuilder.Add(trueKeyword);

        // Act
        syntaxListBuilder.Add(trueKeyword);

        // Assert
        Assert.Equal(trueKeyword, syntaxListBuilder[1]);
    }

    [Fact(DisplayName = $"The AddRange() method with negative offset must throw {nameof(ArgumentOutOfRangeException)}.")]
    public void AddRangeMethod_WithNegativeOffset_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        GreenNode[] items = { trueKeyword, falseKeyword };
        const int offset = -1;
        const int length = 2;

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        void AddRangeFunc()
        {
            syntaxListBuilder.AddRange(items, offset, length);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(AddRangeFunc);
        Assert.Equal("offset", exception.ParamName);
    }

    [Fact(DisplayName = $"The AddRange() method with offset greater than array length must throw {nameof(ArgumentOutOfRangeException)}.")]
    public void AddRangeMethod_OffsetExceedsArrayLength_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        GreenNode[] items = { trueKeyword, falseKeyword };
        const int offset = 3;
        const int length = 2;

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        void AddRangeFunc()
        {
            syntaxListBuilder.AddRange(items, offset, length);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(AddRangeFunc);
        Assert.Equal("offset", exception.ParamName);
    }

    [Fact(DisplayName = $"The AddRange() method with negative length must throw {nameof(ArgumentOutOfRangeException)}.")]
    public void AddRangeMethod_WithNegativeLength_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        GreenNode[] items = { trueKeyword, falseKeyword };
        const int offset = 1;
        const int length = -1;

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        void AddRangeFunc()
        {
            syntaxListBuilder.AddRange(items, offset, length);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(AddRangeFunc);
        Assert.Equal("length", exception.ParamName);
    }

    [Fact(DisplayName = $"The AddRange() method with length greater than array length must throw {nameof(ArgumentOutOfRangeException)}.")]
    public void AddRangeMethod_LengthExceedsArrayLength_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        GreenNode[] items = { trueKeyword, falseKeyword };
        const int offset = 0;
        const int length = 3;

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        void AddRangeFunc()
        {
            syntaxListBuilder.AddRange(items, offset, length);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(AddRangeFunc);
        Assert.Equal("length", exception.ParamName);
    }

    [Fact(DisplayName = $"The AddRange() method with offset and length greater than array length must throw {nameof(ArgumentOutOfRangeException)}.")]
    public void AddRangeMethod_OffsetAndLengthExceedsArrayLength_MustThrowArgumentOutOfRangeException()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        GreenNode[] items = { trueKeyword, falseKeyword };
        const int offset = 1;
        const int length = 2;

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        // Act
        void AddRangeFunc()
        {
            syntaxListBuilder.AddRange(items, offset, length);
        }

        // Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(AddRangeFunc);
        Assert.Equal("length", exception.ParamName);
    }

    [Fact(DisplayName = "The AddRange() method must ensure additional capacity.")]
    public void AddRangeMethod_MustEnsureAdditionalCapacity()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);

        GreenNode[] items = new GreenNode[8];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = trueKeyword;
        }

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();
        syntaxListBuilder.AddRange(items);

        // Act
        syntaxListBuilder.AddRange(items);

        // Assert
        Assert.Equal(trueKeyword, syntaxListBuilder[15]);
    }

    [Fact(DisplayName = $"The Clear() method must reset the {nameof(SyntaxListBuilder.Count)} property.")]
    public void ClearMethod_MustResetCountProperty()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();
        syntaxListBuilder.Add(trueKeyword);
        syntaxListBuilder.Clear();

        // Act
        int actualCount = syntaxListBuilder.Count;

        // Assert
        Assert.Equal(0, actualCount);
    }

    [Fact(DisplayName = "The RemoveLast() method set the node to null.")]
    public void RemoveLastMethod_MustSetNodeToNull()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        syntaxListBuilder.Add(trueKeyword);
        Assert.NotNull(syntaxListBuilder[0]);

        // Act
        syntaxListBuilder.RemoveLast();
        GreenNode? actualNode = syntaxListBuilder[0];

        // Assert
        Assert.Null(actualNode);
    }

    [Fact(DisplayName = $"The RemoveLast() method must decrement the {nameof(SyntaxListBuilder.Count)} property.")]
    public void RemoveLastMethod_MustDecrementCountProperty()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

        syntaxListBuilder.Add(trueKeyword);
        Assert.Equal(1, syntaxListBuilder.Count);

        // Act
        syntaxListBuilder.RemoveLast();

        // Assert
        Assert.Equal(0, syntaxListBuilder.Count);
    }

    [Fact(DisplayName = "The Any() method must return false if the list does not contain the given kind.")]
    public void AnyMethod_NonExistingKind_MustReturnFalse()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);

        GreenNode[] items = { trueKeyword, falseKeyword };
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();
        syntaxListBuilder.AddRange(items);

        // Act
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

        GreenNode[] items = { trueKeyword, falseKeyword };
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();
        syntaxListBuilder.AddRange(items);

        // Act
        bool actualResult = syntaxListBuilder.Any(SyntaxKind.TrueKeyword);

        // Assert
        Assert.True(actualResult);
    }

    [Fact(DisplayName = $"The ToArray() method must return a collection of {nameof(GreenNode)}s.")]
    public void ToArrayMethod_MustReturnACollectionOfNodes()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        GreenNode[] items = { trueKeyword, falseKeyword };

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();
        syntaxListBuilder.AddRange(items);

        // Act
        GreenNode[] actualItems = syntaxListBuilder.ToArray();

        // Assert
        Assert.Equivalent(actualItems, items);
    }

    [Fact(DisplayName = $"The ToListNode() method with {nameof(SyntaxListBuilder.Count)}=0 must return null.")]
    public void ToListNodeMethod_Count0_MustReturnNull()
    {
        // Arrange
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();

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
        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();
        syntaxListBuilder.Add(trueKeyword);

        // Act
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

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();
        syntaxListBuilder.Add(trueKeyword);
        syntaxListBuilder.Add(falseKeyword);

        // Act
        GreenNode? actualListNode = syntaxListBuilder.ToListNode();

        // Assert
        Assert.NotNull(actualListNode);
        Assert.Equal(trueKeyword, actualListNode.GetSlot(0));
        Assert.Equal(falseKeyword, actualListNode.GetSlot(1));
    }

    [Fact(DisplayName = $"The ToListNode() method with {nameof(SyntaxListBuilder.Count)}=3 must return a syntax list.")]
    public void ToListNodeMethod_Count3_MustReturnSyntaxList()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        SyntaxToken nullKeyword = SyntaxFactory.Token(SyntaxKind.NullKeyword);

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();
        syntaxListBuilder.Add(trueKeyword);
        syntaxListBuilder.Add(falseKeyword);
        syntaxListBuilder.Add(nullKeyword);

        // Act
        GreenNode? actualListNode = syntaxListBuilder.ToListNode();

        // Assert
        Assert.NotNull(actualListNode);
        Assert.Equal(trueKeyword, actualListNode.GetSlot(0));
        Assert.Equal(falseKeyword, actualListNode.GetSlot(1));
        Assert.Equal(nullKeyword, actualListNode.GetSlot(2));
    }

    [Fact(DisplayName = $"The ToListNode() method with {nameof(SyntaxListBuilder.Count)}>3 must return a syntax list.")]
    public void ToListNodeMethod_Count4_MustReturnSyntaxList()
    {
        // Arrange
        SyntaxToken trueKeyword = SyntaxFactory.Token(SyntaxKind.TrueKeyword);
        SyntaxToken falseKeyword = SyntaxFactory.Token(SyntaxKind.FalseKeyword);
        SyntaxToken nullKeyword = SyntaxFactory.Token(SyntaxKind.NullKeyword);
        SyntaxToken xRefKeyword = SyntaxFactory.Token(SyntaxKind.XRefKeyword);

        SyntaxListBuilder syntaxListBuilder = SyntaxListBuilder.Create();
        syntaxListBuilder.Add(trueKeyword);
        syntaxListBuilder.Add(falseKeyword);
        syntaxListBuilder.Add(nullKeyword);
        syntaxListBuilder.Add(xRefKeyword);

        // Act
        GreenNode? actualListNode = syntaxListBuilder.ToListNode();

        // Assert
        Assert.NotNull(actualListNode);
        Assert.Equal(trueKeyword, actualListNode.GetSlot(0));
        Assert.Equal(falseKeyword, actualListNode.GetSlot(1));
        Assert.Equal(nullKeyword, actualListNode.GetSlot(2));
        Assert.Equal(xRefKeyword, actualListNode.GetSlot(3));
    }
}
