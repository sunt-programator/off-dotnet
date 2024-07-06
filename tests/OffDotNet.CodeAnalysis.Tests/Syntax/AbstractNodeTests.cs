// <copyright file="AbstractNodeTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.CodeAnalysis.Tests.Syntax;

using System.Diagnostics.CodeAnalysis;
using OffDotNet.CodeAnalysis.Syntax;
using OffDotNet.CodeAnalysis.Utils;

[WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
public class AbstractNodeTests
{
    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(AbstractNode.RawKind)} property should the value passed to the constructor")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(ushort.MaxValue)]
    public void RawKindProperty_ShouldReturnTheValuePassedToTheConstructor(ushort rawKind)
    {
        // Arrange
        var rawNode = new MockAbstractNode(rawKind);

        // Act
        var kind = rawNode.RawKind;

        // Assert
        Assert.Equal(rawKind, kind);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(AbstractNode.KindText)} property should return the name of the syntax kind")]
    [InlineData(0, "RawKind0")]
    [InlineData(1, "RawKind1")]
    public void KindTextProperty_ShouldReturnTheNameOfTheSyntaxKind(ushort rawKind, string expected)
    {
        // Arrange
        var rawNode = new MockAbstractNode(rawKind);

        // Act
        var kindText = rawNode.KindText;

        // Assert
        Assert.Equal(expected, kindText);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.IsToken)} property should return false by default")]
    public void IsTokenProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        const ushort RawKind = 0;
        var rawNode = new MockAbstractNode(RawKind);

        // Act
        var isToken = rawNode.IsToken;

        // Assert
        Assert.False(isToken);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.IsTrivia)} property should return false by default")]
    public void IsTriviaProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        const ushort RawKind = 0;
        var rawNode = new MockAbstractNode(RawKind);

        // Act
        var isTrivia = rawNode.IsTrivia;

        // Assert
        Assert.False(isTrivia);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName =
        $"{nameof(AbstractNode.IsList)} property should return true only if the {nameof(AbstractNode.RawKind)} is {nameof(AbstractNode.ListKind)}")]
    [InlineData(0, false)]
    [InlineData(1, true)]
    public void IsListProperty_ShouldReturnTrueOnlyIfTheRawKindIsListKind(ushort rawKind, bool expected)
    {
        // Arrange
        var rawNode = new MockAbstractNode(rawKind);

        // Act
        var isList = rawNode.IsList;

        // Assert
        Assert.Equal(expected, isList);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Theory(DisplayName = $"{nameof(AbstractNode.FullWidth)} property should return the value passed to the constructor")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void FullWidthProperty_ShouldReturnTheValuePassedToTheConstructor(int fullWidth)
    {
        // Arrange
        var rawNode = new MockAbstractNode(0, fullWidth);

        // Act
        var width = rawNode.FullWidth;

        // Assert
        Assert.Equal(fullWidth, width);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName =
        $"{nameof(AbstractNode.Width)} property should exclude the {nameof(AbstractNode.LeadingTriviaWidth)} and {nameof(AbstractNode.TrailingTriviaWidth)}")]
    public void WidthProperty_ShouldExcludeTheLeadingTriviaWidthAndTrailingTriviaWidth()
    {
        // Arrange
        var rawNode = new MockAbstractNodeWithTwoSlots(0);

        // Act
        var width = rawNode.Width;

        // Assert
        Assert.Equal(12, width);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.SlotCount)} property should return 0 by default")]
    public void SlotCountProperty_ShouldReturnZeroByDefault()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var slotCount = rawNode.SlotCount;

        // Assert
        Assert.Equal(0, slotCount);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.SlotCount)} property should return the value passed to the constructor")]
    public void SlotCountProperty_ShouldReturnTheValuePassedToTheConstructor()
    {
        // Arrange
        var rawNode = new MockAbstractNodeWithTwoSlots(0);

        // Act
        var slotCount = rawNode.SlotCount;

        // Assert
        Assert.Equal(2, slotCount);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.SlotCount)} property should return the large slot count if the slot count is too large")]
    public void SlotCountProperty_ShouldReturnTheLargeSlotCountIfTheSlotCountIsTooLarge()
    {
        // Arrange
        var rawNode = new MockAbstractNodeWithManySlots(0);

        // Act
        var slotCount = rawNode.SlotCount;

        // Assert
        Assert.Equal(255, slotCount);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.GetSlotOffset)} method with index=0 should return 0")]
    public void GetSlotOffsetMethod_WithIndex0_ShouldReturn0()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var actual = rawNode.GetSlotOffset(0);

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.GetSlotOffset)} method with index=1 and the slot does not exist should return 0")]
    public void GetSlotOffsetMethod_WithIndex1AndTheSlotDoesNotExist_ShouldReturn0()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var actual = rawNode.GetSlotOffset(1);

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName =
        $"{nameof(AbstractNode.GetSlotOffset)} method with index=1 and the slot exists should return the {nameof(AbstractNode.FullWidth)} of the slot")]
    public void GetSlotOffsetMethod_WithIndex1AndTheSlotExists_ShouldReturnTheFullWidthOfTheSlot()
    {
        // Arrange
        var rawNode = new MockAbstractNodeWithTwoSlots(0);

        // Act
        var actual = rawNode.GetSlotOffset(1);

        // Assert
        Assert.Equal(14, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.LeadingTriviaWidth)} property should return 0 by default")]
    public void LeadingTriviaWidthProperty_ShouldReturn0ByDefault()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var actual = rawNode.LeadingTriviaWidth;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.LeadingTriviaWidth)} property should return 0 if the node itself is a terminal node")]
    public void LeadingTriviaWidthProperty_WithTerminalNode_ShouldReturn0()
    {
        // Arrange
        const int FullWidth = 10;
        var rawNode = Substitute.ForPartsOf<AbstractNode>((ushort)0, FullWidth);

        // Act
        var actual = rawNode.LeadingTriviaWidth;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName =
        $"{nameof(AbstractNode.LeadingTriviaWidth)} property should return the {nameof(AbstractNode.LeadingTriviaWidth)} of the first terminal node")]
    public void LeadingTriviaWidthProperty_WithNonTerminalNode_ShouldReturnTheLeadingTriviaWidthOfTheFirstTerminalNode()
    {
        // Arrange
        var rawNode = new MockAbstractNodeWithTwoSlots(0);

        // Act
        var actual = rawNode.LeadingTriviaWidth;

        // Assert
        Assert.Equal(14, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.TrailingTriviaWidth)} property should return 0 by default")]
    public void TrailingTriviaWidthProperty_ShouldReturn0ByDefault()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var actual = rawNode.TrailingTriviaWidth;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.TrailingTriviaWidth)} property should return 0 if the node itself is a terminal node")]
    public void TrailingTriviaWidthProperty_WithTerminalNode_ShouldReturn0()
    {
        // Arrange
        const int FullWidth = 10;
        var rawNode = Substitute.ForPartsOf<AbstractNode>((ushort)0, FullWidth);

        // Act
        var actual = rawNode.TrailingTriviaWidth;

        // Assert
        Assert.Equal(0, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName =
        $"{nameof(AbstractNode.TrailingTriviaWidth)} property should return the {nameof(AbstractNode.TrailingTriviaWidth)} of the last terminal node")]
    public void TrailingTriviaWidthProperty_WithNonTerminalNode_ShouldReturnTheTrailingTriviaWidthOfTheLastTerminalNode()
    {
        // Arrange
        var rawNode = new MockAbstractNodeWithTwoSlots(0);

        // Act
        var actual = rawNode.TrailingTriviaWidth;

        // Assert
        Assert.Equal(10, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.HasLeadingTrivia)} property should return false by default")]
    public void HasLeadingTriviaProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var actual = rawNode.HasLeadingTrivia;

        // Assert
        Assert.False(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.HasLeadingTrivia)} property should return true if the {nameof(AbstractNode.LeadingTriviaWidth)} is not 0")]
    public void HasLeadingTriviaProperty_WithLeadingTrivia_ShouldReturnTrue()
    {
        // Arrange
        var rawNode = new MockAbstractNodeWithTwoSlots(0);

        // Act
        var actual = rawNode.HasLeadingTrivia;

        // Assert
        Assert.True(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.HasTrailingTrivia)} property should return false by default")]
    public void HasTrailingTriviaProperty_ShouldReturnFalseByDefault()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var actual = rawNode.HasTrailingTrivia;

        // Assert
        Assert.False(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.HasTrailingTrivia)} property should return true if the {nameof(AbstractNode.TrailingTriviaWidth)} is not 0")]
    public void HasTrailingTriviaProperty_WithTrailingTrivia_ShouldReturnTrue()
    {
        // Arrange
        var rawNode = new MockAbstractNodeWithTwoSlots(0);

        // Act
        var actual = rawNode.HasTrailingTrivia;

        // Assert
        Assert.True(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.LeadingTrivia)} property should return {nameof(Option<AbstractNode>.None)} by default")]
    public void LeadingTriviaProperty_ShouldReturnNoneByDefault()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var actual = rawNode.LeadingTrivia.IsSome(out _);

        // Assert
        Assert.False(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.TrailingTrivia)} property should return {nameof(Option<AbstractNode>.None)} by default")]
    public void TrailingTriviaProperty_ShouldReturnNoneByDefault()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var actual = rawNode.TrailingTrivia.IsSome(out _);

        // Assert
        Assert.False(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.Value)} property should return {nameof(Option<AbstractNode>.None)} by default")]
    public void ValueProperty_ShouldReturnNoneByDefault()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var actual = rawNode.Value.IsSome(out _);

        // Assert
        Assert.False(actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.Value)} property should return the populated value")]
    public void ValueProperty_ShouldReturnThePopulatedValue()
    {
        // Arrange
        var rawNode = new MockSyntaxNode(0);
        object expected = "Node0";

        // Act
        var actual = rawNode.Value;

        // Assert
        Assert.True(actual.IsSome(out var actualValue));
        Assert.Equal(expected, actualValue);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.ValueText)} should return an empty string by default")]
    public void ValueTextProperty_ShouldReturnAnEmptyStringByDefault()
    {
        // Arrange
        var rawNode = new MockAbstractNode(0);

        // Act
        var actual = rawNode.ValueText;

        // Assert
        Assert.Equal(string.Empty, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.ValueText)} property should return the populated value")]
    public void ValueTextProperty_ShouldReturnThePopulatedValue()
    {
        // Arrange
        var rawNode = new MockSyntaxNode(0);
        const string Expected = "Node0";

        // Act
        var actual = rawNode.ValueText;

        // Assert
        Assert.Equal(Expected, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(ToString)} method should return the text when the node is a trivia node")]
    public void ToStringMethod_WithTriviaNode_ShouldReturnTheText()
    {
        // Arrange
        const string Text = "RawKind0";
        var rawNode = new MockTriviaNode(0, 2);

        // Act
        var actual = rawNode.ToString();

        // Assert
        Assert.Equal(Text, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(ToString)} method should return the text when the node is a token node")]
    public void ToStringMethod_WithTokenNode_ShouldReturnTheText()
    {
        // Arrange
        const string Text = "RawKind0";
        var rawNode = new MockSyntaxNode(0, 2);

        // Act
        var actual = rawNode.ToString();

        // Assert
        Assert.Equal(Text, actual);
    }

    [WorkItem("https://github.com/sunt-programator/off-dotnet/issues/339")]
    [Fact(DisplayName = $"{nameof(AbstractNode.ToFullString)} method should return the text when the node is a token node and has leading & trailing trivia")]
    public void ToFullStringMethod_WithTokenNodeAndLeadingTrivia_ShouldReturnTheText()
    {
        // Arrange
        const string Text = "<leading>RawKind0<trailing>";
        var rawNode = new MockSyntaxNode(0, 2);

        // Act
        var actual = rawNode.ToFullString();

        // Assert
        Assert.Equal(Text, actual);
    }
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Test classes are small.")]
internal class MockAbstractNode : AbstractNode
{
    public MockAbstractNode(ushort rawKind)
        : base(rawKind)
    {
        KindText = $"RawKind{rawKind}";
    }

    public MockAbstractNode(ushort rawKind, int fullWidth)
        : base(rawKind, fullWidth)
    {
        KindText = $"RawKind{rawKind}";
    }

    public override string KindText { get; }

    internal override Option<AbstractNode> GetSlot(int index)
    {
        return Option<AbstractNode>.None;
    }
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Test classes are small.")]
internal class MockAbstractNodeWithTwoSlots : AbstractNode
{
    public MockAbstractNodeWithTwoSlots(ushort rawKind)
        : this(rawKind, 36)
    {
    }

    private MockAbstractNodeWithTwoSlots(ushort rawKind, int fullWidth)
        : base(rawKind, fullWidth)
    {
        KindText = $"{RawKind}{rawKind}";
        SlotCount = 2;
    }

    public override string KindText { get; }

    internal override Option<AbstractNode> GetSlot(int index)
    {
        return index switch
        {
            0 => Option<AbstractNode>.Some(new MockSyntaxNode(1, 14)),
            1 => Option<AbstractNode>.Some(new MockSyntaxNode(2, 10)),
            _ => Option<AbstractNode>.None,
        };
    }
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Test classes are small.")]
internal class MockAbstractNodeWithManySlots : AbstractNode
{
    public MockAbstractNodeWithManySlots(ushort rawKind)
        : this(rawKind, 14)
    {
    }

    private MockAbstractNodeWithManySlots(ushort rawKind, int fullWidth)
        : base(rawKind, fullWidth)
    {
        KindText = $"{RawKind}{rawKind}";
        SlotCount = 16;
    }

    public override string KindText { get; }

    internal override Option<AbstractNode> GetSlot(int index)
    {
        return index switch
        {
            _ => Option<AbstractNode>.None,
        };
    }

    protected override int GetSlotCount() => 255;
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Test classes are small.")]
internal class MockSyntaxNode : AbstractNode
{
    public MockSyntaxNode(ushort rawKind)
        : base(rawKind)
    {
        KindText = $"RawKind{rawKind}";
    }

    public MockSyntaxNode(ushort rawKind, int fullWidth)
        : base(rawKind, fullWidth)
    {
        KindText = $"RawKind{rawKind}";
    }

    public override string KindText { get; }

    public override Option<object> Value => ValueText;

    public override string ValueText => $"Node{RawKind}";

    public override bool IsToken => true;

    public override int LeadingTriviaWidth => FullWidth;

    public override int TrailingTriviaWidth => FullWidth;

    internal override Option<AbstractNode> GetSlot(int index) => Option<AbstractNode>.None;

    protected override void WriteTokenTo(TextWriter writer, bool leading, bool trailing)
    {
        if (leading)
        {
            writer.Write("<leading>");
        }

        writer.Write(KindText);

        if (trailing)
        {
            writer.Write("<trailing>");
        }
    }
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Test classes are small.")]
internal class MockTriviaNode : AbstractNode
{
    public MockTriviaNode(ushort rawKind)
        : base(rawKind)
    {
        KindText = $"RawKind{rawKind}";
    }

    public MockTriviaNode(ushort rawKind, int fullWidth)
        : base(rawKind, fullWidth)
    {
        KindText = $"RawKind{rawKind}";
    }

    public override string KindText { get; }

    public override bool IsTrivia => true;

    internal override Option<AbstractNode> GetSlot(int index) => Option<AbstractNode>.None;

    protected override void WriteTriviaTo(TextWriter writer)
    {
        writer.Write(KindText);
    }
}
