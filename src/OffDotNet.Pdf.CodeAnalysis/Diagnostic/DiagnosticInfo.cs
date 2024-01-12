// <copyright file="DiagnosticInfo.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace OffDotNet.Pdf.CodeAnalysis.Diagnostic;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal class DiagnosticInfo : IFormattable
{
    private static readonly ImmutableArray<string> CompilerErrorCustomTags =
        ImmutableArray.Create(WellKnownDiagnosticTags.Compiler, WellKnownDiagnosticTags.Telemetry, WellKnownDiagnosticTags.NotConfigurable);

    private static readonly ImmutableArray<string> CompilerNonErrorCustomTags = ImmutableArray.Create(WellKnownDiagnosticTags.Compiler, WellKnownDiagnosticTags.Telemetry);

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Reviewed.")]
    private static ImmutableDictionary<DiagnosticCode, DiagnosticDescriptor> errorCodeToDescriptorMap = ImmutableDictionary<DiagnosticCode, DiagnosticDescriptor>.Empty;

    internal DiagnosticInfo(IMessageProvider messageProvider, DiagnosticCode code)
        : this(messageProvider, code, Array.Empty<object>())
    {
    }

    internal DiagnosticInfo(IMessageProvider messageProvider, DiagnosticCode code, params object[] arguments)
    {
        this.MessageProvider = messageProvider;
        this.Code = code;
        this.DefaultSeverity = this.MessageProvider.GetSeverity(code);
        this.EffectiveSeverity = this.DefaultSeverity;
        this.Arguments = arguments;
    }

    internal DiagnosticInfo(IMessageProvider messageProvider, DiagnosticCode code, bool isWarningAsError, params object[] arguments)
        : this(messageProvider, code, arguments)
    {
        Debug.Assert(!isWarningAsError || this.DefaultSeverity == DiagnosticSeverity.Warning, "Only warning diagnostics can be treated as errors.");

        if (isWarningAsError)
        {
            this.EffectiveSeverity = DiagnosticSeverity.Error;
        }
    }

    public DiagnosticCode Code { get; }

    public DiagnosticSeverity DefaultSeverity { get; }

    public DiagnosticSeverity EffectiveSeverity { get; }

    /// <summary>
    /// Gets the message id (for example "PDF0001") for the message. This includes both the error number
    /// and a prefix identifying the source.
    /// </summary>
    public virtual string MessageIdentifier => this.MessageProvider.GetIdForErrorCode(this.Code);

    public virtual DiagnosticDescriptor Descriptor => GetOrCreateDescriptor(this.Code, this.DefaultSeverity, this.MessageProvider);

    /// <summary>Gets a value indicating whether the warning diagnostic is treated as an error.</summary>
    /// <remarks>True implies <see cref="EffectiveSeverity"/> = <see cref="DiagnosticSeverity.Error"/> and <see cref="DefaultSeverity"/> = <see cref="DiagnosticSeverity.Warning"/>.</remarks>
    public bool IsWarningAsError => this.DefaultSeverity == DiagnosticSeverity.Warning && this.EffectiveSeverity == DiagnosticSeverity.Error;

    internal object[] Arguments { get; }

    internal IMessageProvider MessageProvider { get; }

    /// <summary>Get the text of the message in the given language.</summary>
    /// <param name="formatProvider">The culture used to get the message.</param>
    /// <returns>The text of the message.</returns>
    public virtual string GetMessage(IFormatProvider? formatProvider = null)
    {
        CultureInfo culture = formatProvider as CultureInfo ?? CultureInfo.InvariantCulture;
        string message = this.MessageProvider.LoadMessage(this.Code, culture);

        if (string.IsNullOrEmpty(message))
        {
            return string.Empty;
        }

        return this.Arguments.Length == 0 ? message : string.Format(formatProvider, message, this.GetArgumentsToUse(formatProvider));
    }

    public override string ToString()
    {
        return this.ToString(null);
    }

    public string ToString(IFormatProvider? formatProvider)
    {
        return ((IFormattable)this).ToString(null, formatProvider);
    }

    string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
    {
        CultureInfo culture = formatProvider as CultureInfo ?? CultureInfo.InvariantCulture;
        string messagePrefix = this.MessageProvider.GetMessagePrefix(this.MessageIdentifier, this.EffectiveSeverity, this.IsWarningAsError, culture);
        string message = this.GetMessage(formatProvider);
        return string.Format(formatProvider, "{0}: {1}", messagePrefix, message);
    }

    public sealed override int GetHashCode()
    {
        var hashCode = default(HashCode);
        hashCode.Add(this.Code);

        for (int i = 0; i < this.Arguments.Length; i++)
        {
            hashCode.Add(this.Arguments[i]);
        }

        return hashCode.ToHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not DiagnosticInfo other)
        {
            return false;
        }

        if (this.Code != other.Code || this.GetType() != other.GetType() || this.Arguments.Length != other.Arguments.Length)
        {
            return false;
        }

        return this.Arguments.SequenceEqual(other.Arguments);
    }

    private static DiagnosticDescriptor GetOrCreateDescriptor(DiagnosticCode code, DiagnosticSeverity defaultSeverity, IMessageProvider messageProvider)
    {
        return ImmutableInterlocked.GetOrAdd(
            ref errorCodeToDescriptorMap,
            code,
            static (c, arg) => CreateDescriptor(c, arg.defaultSeverity, arg.messageProvider),
            (defaultSeverity, messageProvider));
    }

    private static DiagnosticDescriptor CreateDescriptor(DiagnosticCode errorCode, DiagnosticSeverity defaultSeverity, IMessageProvider messageProvider)
    {
        string id = messageProvider.GetIdForErrorCode(errorCode);
        LocalizableString title = messageProvider.GetTitle(errorCode);
        LocalizableString description = messageProvider.GetDescription(errorCode);
        LocalizableString messageFormat = messageProvider.GetMessage(errorCode);
        string helpLink = messageProvider.GetHelpLink(errorCode);
        string category = messageProvider.GetCategory(errorCode);
        ImmutableArray<string> customTags = defaultSeverity == DiagnosticSeverity.Error ? CompilerErrorCustomTags : CompilerNonErrorCustomTags;
        bool isEnabledByDefault = messageProvider.GetIsEnabledByDefault(errorCode);
        return new DiagnosticDescriptor(id, title, messageFormat, category, defaultSeverity, isEnabledByDefault, description, helpLink, customTags);
    }

    private object[] GetArgumentsToUse(IFormatProvider? formatProvider)
    {
        object[]? argumentsToUse = null;
        for (int i = 0; i < this.Arguments.Length; i++)
        {
            if (this.Arguments[i] is DiagnosticInfo embedded)
            {
                argumentsToUse = this.InitializeArgumentListIfNeeded(argumentsToUse);
                argumentsToUse[i] = embedded.GetMessage(formatProvider);
            }
        }

        return argumentsToUse ?? this.Arguments;
    }

    private object[] InitializeArgumentListIfNeeded(object[]? argumentsToUse)
    {
        if (argumentsToUse != null)
        {
            return argumentsToUse;
        }

        object[] newArguments = new object[this.Arguments.Length];
        Array.Copy(this.Arguments, newArguments, newArguments.Length);
        return newArguments;
    }

    private string GetDebuggerDisplay()
    {
        return this.ToString(null);
    }
}
