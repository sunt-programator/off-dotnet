// <copyright file="XRefExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

public static class XRefExtensions
{
    public static IxRefSubSection ToXRefSubSection(this IxRefEntry entry, int objectNumber)
    {
        return new XRefSubSection(objectNumber, new List<IxRefEntry>(1) { entry });
    }

    public static IxRefSubSection ToXRefSubSection(this ICollection<IxRefEntry> entries, int objectNumber)
    {
        return new XRefSubSection(objectNumber, entries);
    }

    public static IxRefSection ToXRefSection(this IxRefSubSection subSection)
    {
        return new XRefSection(new List<IxRefSubSection>(1) { subSection });
    }

    public static IxRefSection ToXRefSection(this ICollection<IxRefSubSection> subSections)
    {
        return new XRefSection(subSections);
    }

    public static IxRefSection ToXRefSection(this IxRefEntry entry, int objectNumber)
    {
        return entry.ToXRefSubSection(objectNumber).ToXRefSection();
    }

    public static IxRefSection ToXRefSection(this ICollection<IxRefEntry> entries, int objectNumber)
    {
        return entries.ToXRefSubSection(objectNumber).ToXRefSection();
    }

    public static IxRefTable ToXRefTable(this IxRefSection section)
    {
        return new XRefTable(new List<IxRefSection>(1) { section });
    }

    public static IxRefTable ToXRefTable(this ICollection<IxRefSection> sections)
    {
        return new XRefTable(sections);
    }

    public static IxRefTable ToXRefTable(this IxRefSubSection subSection)
    {
        return subSection.ToXRefSection().ToXRefTable();
    }

    public static IxRefTable ToXRefTable(this ICollection<IxRefSubSection> subSections)
    {
        return subSections.ToXRefSection().ToXRefTable();
    }

    public static IxRefTable ToXRefTable(this IxRefEntry entry, int objectNumber)
    {
        return entry.ToXRefSubSection(objectNumber).ToXRefSection().ToXRefTable();
    }

    public static IxRefTable ToXRefTable(this ICollection<IxRefEntry> entries, int objectNumber)
    {
        return entries.ToXRefSubSection(objectNumber).ToXRefSection().ToXRefTable();
    }
}
