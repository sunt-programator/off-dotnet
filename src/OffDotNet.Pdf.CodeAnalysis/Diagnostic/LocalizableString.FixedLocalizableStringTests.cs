// <copyright file="LocalizableString.FixedLocalizableStringTests.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

public abstract partial class LocalizableString
{
    private sealed class FixedLocalizableString : LocalizableString
    {
        private static readonly FixedLocalizableString StringEmpty = new(string.Empty);
        private readonly string fixedString;

        private FixedLocalizableString(string fixedResource)
        {
            this.fixedString = fixedResource;
        }

        internal override bool CanThrowExceptions => false;

        public static FixedLocalizableString Create(string? fixedResource)
        {
            return string.IsNullOrEmpty(fixedResource) ? StringEmpty : new FixedLocalizableString(fixedResource);
        }

        protected override string GetText(IFormatProvider? formatProvider)
        {
            return this.fixedString;
        }

        protected override int GetHash()
        {
            return this.fixedString.GetHashCode();
        }

        protected override bool AreEqual(object? other)
        {
            return other is FixedLocalizableString fixedLocalizableString && string.Equals(this.fixedString, fixedLocalizableString.fixedString);
        }
    }
}
