// <copyright file="Location.None.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

public abstract partial class Location
{
    internal class NoLocation : Location
    {
        internal static readonly Location Instance = new NoLocation();

        private NoLocation()
        {
        }

        /// <inheritdoc/>
        public override LocationKind Kind => LocationKind.None;

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return 0x16487756;
        }
    }
}
