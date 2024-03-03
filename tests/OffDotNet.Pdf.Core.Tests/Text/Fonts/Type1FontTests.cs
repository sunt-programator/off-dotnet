// <copyright file="Type1FontTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.Core.Tests.Text.Fonts;

using OffDotNet.Pdf.Core.Text.Fonts;

public class Type1FontTests
{
    [Fact(DisplayName = $"Constructor with a null {nameof(Type1FontOptions.BaseFont)} value should throw an {nameof(ArgumentNullException)}")]
    public void Type1Font_ConstructorWithNullBaseFont_ShouldThrowException()
    {
        // Arrange
        Type1FontOptions documentCatalogOptions = new() { BaseFont = null! };

        // Act
        IType1Font Type1FontFunction()
        {
            return new Type1Font(documentCatalogOptions);
        }

        // Assert
        Assert.Throws<ArgumentNullException>(Type1FontFunction);
    }

    [Theory(DisplayName = $"The {nameof(Type1Font.Content)} should return a valid value")]
    [MemberData(nameof(Type1FontTestsDataGenerator.Type1Font_Content_TestCases), MemberType = typeof(Type1FontTestsDataGenerator))]
    public void Type1Font_Content_ShouldReturnValidValue(IType1Font type1Font, string expectedContent)
    {
        // Arrange

        // Act
        string actualContent = type1Font.Content;

        // Assert
        Assert.Equal(expectedContent, actualContent);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "TestData generator class can be in the same file")]
internal static class Type1FontTestsDataGenerator
{
    private static readonly Type1FontOptions Options = new() { BaseFont = "Helvetica", FontName = "Custom-Font" };
    private static readonly IType1Font CustomType1Font = new Type1Font(Options);

    private static readonly Type1FontOptions OptionsWithoutName = new() { BaseFont = "Helvetica" };
    private static readonly IType1Font CustomType1FontWithoutName = new Type1Font(OptionsWithoutName);

    public static IEnumerable<object[]> Type1Font_Content_TestCases()
    {
        yield return [StandardFonts.TimesRoman,
            "<</Type /Font /Subtype /Type1 /Name /Times-Roman /BaseFont /Times-Roman>>"
        ];
        yield return [StandardFonts.TimesBold,
            "<</Type /Font /Subtype /Type1 /Name /Times-Bold /BaseFont /Times-Bold>>"
        ];
        yield return [StandardFonts.TimesItalic,
            "<</Type /Font /Subtype /Type1 /Name /Times-Italic /BaseFont /Times-Italic>>"
        ];
        yield return [StandardFonts.TimesBoldItalic,
            "<</Type /Font /Subtype /Type1 /Name /Times-BoldItalic /BaseFont /Times-BoldItalic>>"
        ];
        yield return [StandardFonts.Helvetica, "<</Type /Font /Subtype /Type1 /Name /Helvetica /BaseFont /Helvetica>>"];
        yield return [StandardFonts.HelveticaBold,
            "<</Type /Font /Subtype /Type1 /Name /Helvetica-Bold /BaseFont /Helvetica-Bold>>"
        ];
        yield return [StandardFonts.HelveticaOblique,
            "<</Type /Font /Subtype /Type1 /Name /Helvetica-Oblique /BaseFont /Helvetica-Oblique>>"
        ];
        yield return [StandardFonts.HelveticaBoldOblique,
            "<</Type /Font /Subtype /Type1 /Name /Helvetica-BoldOblique /BaseFont /Helvetica-BoldOblique>>"
        ];
        yield return [StandardFonts.Courier, "<</Type /Font /Subtype /Type1 /Name /Courier /BaseFont /Courier>>"];
        yield return [StandardFonts.CourierBold,
            "<</Type /Font /Subtype /Type1 /Name /Courier-Bold /BaseFont /Courier-Bold>>"
        ];
        yield return [StandardFonts.CourierOblique,
            "<</Type /Font /Subtype /Type1 /Name /Courier-Oblique /BaseFont /Courier-Oblique>>"
        ];
        yield return [StandardFonts.CourierBoldOblique,
            "<</Type /Font /Subtype /Type1 /Name /Courier-BoldOblique /BaseFont /Courier-BoldOblique>>"
        ];
        yield return [StandardFonts.Symbol, "<</Type /Font /Subtype /Type1 /Name /Symbol /BaseFont /Symbol>>"];
        yield return [StandardFonts.ZapfDingbats,
            "<</Type /Font /Subtype /Type1 /Name /ZapfDingbats /BaseFont /ZapfDingbats>>"
        ];
        yield return [CustomType1Font, "<</Type /Font /Subtype /Type1 /Name /Custom-Font /BaseFont /Helvetica>>"];
        yield return [CustomType1FontWithoutName, "<</Type /Font /Subtype /Type1 /BaseFont /Helvetica>>"];
    }
}
