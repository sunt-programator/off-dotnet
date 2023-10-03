// <copyright file="XRefExtensions.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.FileStructure;

public static class XRefExtensions
{
    public static IXRefSubSection ToXRefSubSection(this IXRefEntry entry, int objectNumber)
    {
        return new XRefSubSection(objectNumber, new List<IXRefEntry>(1) { entry });
    }

    public static IXRefSubSection ToXRefSubSection(this ICollection<IXRefEntry> entries, int objectNumber)
    {
        return new XRefSubSection(objectNumber, entries);
    }

    public static XRefSection ToXRefSection(this IXRefSubSection subSection)
    {
        return new XRefSection(new List<IXRefSubSection>(1) { subSection });
    }

    public static XRefSection ToXRefSection(this ICollection<IXRefSubSection> subSections)
    {
        return new XRefSection(subSections);
    }

    public static XRefSection ToXRefSection(this IXRefEntry entry, int objectNumber)
    {
        return entry.ToXRefSubSection(objectNumber).ToXRefSection();
    }

    public static XRefSection ToXRefSection(this ICollection<IXRefEntry> entries, int objectNumber)
    {
        return entries.ToXRefSubSection(objectNumber).ToXRefSection();
    }

    public static XRefTable ToXRefTable(this XRefSection section)
    {
        return new XRefTable(new List<XRefSection>(1) { section });
    }

    public static XRefTable ToXRefTable(this ICollection<XRefSection> sections)
    {
        return new XRefTable(sections);
    }

    public static XRefTable ToXRefTable(this IXRefSubSection subSection)
    {
        return subSection.ToXRefSection().ToXRefTable();
    }

    public static XRefTable ToXRefTable(this ICollection<IXRefSubSection> subSections)
    {
        return subSections.ToXRefSection().ToXRefTable();
    }

    public static XRefTable ToXRefTable(this IXRefEntry entry, int objectNumber)
    {
        return entry.ToXRefSubSection(objectNumber).ToXRefSection().ToXRefTable();
    }

    public static XRefTable ToXRefTable(this ICollection<IXRefEntry> entries, int objectNumber)
    {
        return entries.ToXRefSubSection(objectNumber).ToXRefSection().ToXRefTable();
    }
}
