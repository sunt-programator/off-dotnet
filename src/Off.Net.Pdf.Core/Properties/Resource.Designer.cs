﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Off.Net.Pdf.Core {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Off.Net.Pdf.Core.Properties.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The generation number must be positive.
        /// </summary>
        internal static string PdfIndirect_GenerationNumberMustBePositive {
            get {
                return ResourceManager.GetString("PdfIndirect_GenerationNumberMustBePositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The generation number must not exceed 65535.
        /// </summary>
        internal static string PdfIndirect_GenerationNumberMustNotExceedMaxAllowedValue {
            get {
                return ResourceManager.GetString("PdfIndirect_GenerationNumberMustNotExceedMaxAllowedValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The object number of the PDF indirect type must be positive.
        /// </summary>
        internal static string PdfIndirect_ObjectNumberMustBePositive {
            get {
                return ResourceManager.GetString("PdfIndirect_ObjectNumberMustBePositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The object must be a PDF Integer type.
        /// </summary>
        internal static string PdfInteger_MustBePdfInteger {
            get {
                return ResourceManager.GetString("PdfInteger_MustBePdfInteger", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The PDF name type cannot be null or contain whitespaces.
        /// </summary>
        internal static string PdfName_CannotBeNullOrWhitespace {
            get {
                return ResourceManager.GetString("PdfName_CannotBeNullOrWhitespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The object must be a PDF Real type.
        /// </summary>
        internal static string PdfReal_MustBePdfReal {
            get {
                return ResourceManager.GetString("PdfReal_MustBePdfReal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The PDF string type must have balanced parentheses.
        /// </summary>
        internal static string PdfString_MustHaveBalancedParentheses {
            get {
                return ResourceManager.GetString("PdfString_MustHaveBalancedParentheses", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The object must be a PDF String type with a valid Hex value.
        /// </summary>
        internal static string PdfString_MustHaveValidHexValue {
            get {
                return ResourceManager.GetString("PdfString_MustHaveValidHexValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The PDF string type must have valid solidus characters.
        /// </summary>
        internal static string PdfString_MustHaveValidSolidusChars {
            get {
                return ResourceManager.GetString("PdfString_MustHaveValidSolidusChars", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The PDF string type with Hex representation should have a value provided.
        /// </summary>
        internal static string PdfString_MustNotBeEmpty {
            get {
                return ResourceManager.GetString("PdfString_MustNotBeEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The byte offset of the XRef entry must be positive.
        /// </summary>
        internal static string XRefEntry_ByteOffsetMustBePositive {
            get {
                return ResourceManager.GetString("XRefEntry_ByteOffsetMustBePositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The byte offset must not exceed 9999999999.
        /// </summary>
        internal static string XRefEntry_ByteOffsetMustNotExceedMaxAllowedValue {
            get {
                return ResourceManager.GetString("XRefEntry_ByteOffsetMustNotExceedMaxAllowedValue", resourceCulture);
            }
        }
    }
}
