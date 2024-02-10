// <copyright file="AsciiCharacter.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OffDotNet.Pdf.CodeAnalysis.Parser;

/// <summary>Represents the ASCII characters set.</summary>
public enum AsciiCharacters
{
    /// <summary>Represents the <c>NUL</c> character.</summary>
    Null = 0x00,

    /// <summary>Represents the <c>SOH</c> character.</summary>
    StartOfHeading = 0x01,

    /// <summary>Represents the <c>STX</c> character.</summary>
    StartOfText = 0x02,

    /// <summary>Represents the <c>ETX</c> character.</summary>
    EndOfText = 0x03,

    /// <summary>Represents the <c>EOT</c> character.</summary>
    EndOfTransmission = 0x04,

    /// <summary>Represents the <c>ENQ</c> character.</summary>
    Enquiry = 0x05,

    /// <summary>Represents the <c>ACK</c> character.</summary>
    Acknowledgement = 0x06,

    /// <summary>Represents the <c>BEL</c> character.</summary>
    Bell = 0x07,

    /// <summary>Represents the <c>BS</c> character.</summary>
    Backspace = 0x08,

    /// <summary>Represents the <c>HT</c> character.</summary>
    Tab = 0x09,

    /// <summary>Represents the <c>LF</c> character.</summary>
    LineFeed = 0x0A,

    /// <summary>Represents the <c>VT</c> character.</summary>
    VerticalTab = 0x0B,

    /// <summary>Represents the <c>FF</c> character.</summary>
    FormFeed = 0x0C,

    /// <summary>Represents the <c>CR</c> character.</summary>
    CarriageReturn = 0x0D,

    /// <summary>Represents the <c>SO</c> character.</summary>
    ShiftOut = 0x0E,

    /// <summary>Represents the <c>SI</c> character.</summary>
    ShiftIn = 0x0F,

    /// <summary>Represents the <c>DLE</c> character.</summary>
    DataLinkEscape = 0x10,

    /// <summary>Represents the <c>DC1</c> character.</summary>
    DeviceControl1 = 0x11,

    /// <summary>Represents the <c>DC2</c> character.</summary>
    DeviceControl2 = 0x12,

    /// <summary>Represents the <c>DC3</c> character.</summary>
    DeviceControl3 = 0x13,

    /// <summary>Represents the <c>DC4</c> character.</summary>
    DeviceControl4 = 0x14,

    /// <summary>Represents the <c>NAK</c> character.</summary>
    NegativeAcknowledge = 0x15,

    /// <summary>Represents the <c>SYN</c> character.</summary>
    SynchronousIdle = 0x16,

    /// <summary>Represents the <c>ETB</c> character.</summary>
    EndOfTransmissionBlock = 0x17,

    /// <summary>Represents the <c>CAN</c> character.</summary>
    Cancel = 0x18,

    /// <summary>Represents the <c>EM</c> character.</summary>
    EndOfMedium = 0x19,

    /// <summary>Represents the <c>SUB</c> character.</summary>
    Substitute = 0x1A,

    /// <summary>Represents the <c>ESC</c> character.</summary>
    Escape = 0x1B,

    /// <summary>Represents the <c>FS</c> character.</summary>
    FileSeparator = 0x1C,

    /// <summary>Represents the <c>GS</c> character.</summary>
    GroupSeparator = 0x1D,

    /// <summary>Represents the <c>RS</c> character.</summary>
    RecordSeparator = 0x1E,

    /// <summary>Represents the <c>US</c> character.</summary>
    UnitSeparator = 0x1F,

    /// <summary>Represents the <c>Space</c> character.</summary>
    Space = 0x20,

    /// <summary>Represents the <c>!</c> character.</summary>
    ExclamationMark = 0x21,

    /// <summary>Represents the <c>"</c> character.</summary>
    DoubleQuote = 0x22,

    /// <summary>Represents the <c>#</c> character.</summary>
    NumberSign = 0x23,

    /// <summary>Represents the <c>$</c> character.</summary>
    DollarSign = 0x24,

    /// <summary>Represents the <c>%</c> character.</summary>
    PercentSign = 0x25,

    /// <summary>Represents the <c>&amp;</c> character.</summary>
    Ampersand = 0x26,

    /// <summary>Represents the <c>'</c> character.</summary>
    SingleQuote = 0x27,

    /// <summary>Represents the <c>(</c> character.</summary>
    LeftParenthesis = 0x28,

    /// <summary>Represents the <c>)</c> character.</summary>
    RightParenthesis = 0x29,

    /// <summary>Represents the <c>*</c> character.</summary>
    Asterisk = 0x2A,

    /// <summary>Represents the <c>+</c> character.</summary>
    PlusSign = 0x2B,

    /// <summary>Represents the <c>,</c> character.</summary>
    Comma = 0x2C,

    /// <summary>Represents the <c>-</c> character.</summary>
    Hyphen = 0x2D,

    /// <summary>Represents the <c>.</c> character.</summary>
    Period = 0x2E,

    /// <summary>Represents the <c>/</c> character.</summary>
    ForwardSlash = 0x2F,

    /// <summary>Represents the <c>0</c> character.</summary>
    Digit0 = 0x30,

    /// <summary>Represents the <c>1</c> character.</summary>
    Digit1 = 0x31,

    /// <summary>Represents the <c>2</c> character.</summary>
    Digit2 = 0x32,

    /// <summary>Represents the <c>3</c> character.</summary>
    Digit3 = 0x33,

    /// <summary>Represents the <c>4</c> character.</summary>
    Digit4 = 0x34,

    /// <summary>Represents the <c>5</c> character.</summary>
    Digit5 = 0x35,

    /// <summary>Represents the <c>6</c> character.</summary>
    Digit6 = 0x36,

    /// <summary>Represents the <c>7</c> character.</summary>
    Digit7 = 0x37,

    /// <summary>Represents the <c>8</c> character.</summary>
    Digit8 = 0x38,

    /// <summary>Represents the <c>9</c> character.</summary>
    Digit9 = 0x39,

    /// <summary>Represents the <c>:</c> character.</summary>
    Colon = 0x3A,

    /// <summary>Represents the <c>;</c> character.</summary>
    Semicolon = 0x3B,

    /// <summary>Represents the <c>&lt;</c> character.</summary>
    LessThan = 0x3C,

    /// <summary>Represents the <c>=</c> character.</summary>
    EqualsSign = 0x3D,

    /// <summary>Represents the <c>&gt;</c> character.</summary>
    GreaterThan = 0x3E,

    /// <summary>Represents the <c>?</c> character.</summary>
    QuestionMark = 0x3F,

    /// <summary>Represents the <c>@</c> character.</summary>
    AtSign = 0x40,

    /// <summary>Represents the <c>A</c> character.</summary>
    UppercaseA = 0x41,

    /// <summary>Represents the <c>B</c> character.</summary>
    UppercaseB = 0x42,

    /// <summary>Represents the <c>C</c> character.</summary>
    UppercaseC = 0x43,

    /// <summary>Represents the <c>D</c> character.</summary>
    UppercaseD = 0x44,

    /// <summary>Represents the <c>E</c> character.</summary>
    UppercaseE = 0x45,

    /// <summary>Represents the <c>F</c> character.</summary>
    UppercaseF = 0x46,

    /// <summary>Represents the <c>G</c> character.</summary>
    UppercaseG = 0x47,

    /// <summary>Represents the <c>H</c> character.</summary>
    UppercaseH = 0x48,

    /// <summary>Represents the <c>I</c> character.</summary>
    UppercaseI = 0x49,

    /// <summary>Represents the <c>J</c> character.</summary>
    UppercaseJ = 0x4A,

    /// <summary>Represents the <c>K</c> character.</summary>
    UppercaseK = 0x4B,

    /// <summary>Represents the <c>L</c> character.</summary>
    UppercaseL = 0x4C,

    /// <summary>Represents the <c>M</c> character.</summary>
    UppercaseM = 0x4D,

    /// <summary>Represents the <c>N</c> character.</summary>
    UppercaseN = 0x4E,

    /// <summary>Represents the <c>O</c> character.</summary>
    UppercaseO = 0x4F,

    /// <summary>Represents the <c>P</c> character.</summary>
    UppercaseP = 0x50,

    /// <summary>Represents the <c>Q</c> character.</summary>
    UppercaseQ = 0x51,

    /// <summary>Represents the <c>R</c> character.</summary>
    UppercaseR = 0x52,

    /// <summary>Represents the <c>S</c> character.</summary>
    UppercaseS = 0x53,

    /// <summary>Represents the <c>T</c> character.</summary>
    UppercaseT = 0x54,

    /// <summary>Represents the <c>U</c> character.</summary>
    UppercaseU = 0x55,

    /// <summary>Represents the <c>B</c> character.</summary>
    UppercaseV = 0x56,

    /// <summary>Represents the <c>W</c> character.</summary>
    UppercaseW = 0x57,

    /// <summary>Represents the <c>X</c> character.</summary>
    UppercaseX = 0x58,

    /// <summary>Represents the <c>Y</c> character.</summary>
    UppercaseY = 0x59,

    /// <summary>Represents the <c>Z</c> character.</summary>
    UppercaseZ = 0x5A,

    /// <summary>Represents the <c>[</c> character.</summary>
    LeftSquareBracket = 0x5B,

    /// <summary>Represents the <c>\</c> character.</summary>
    Backslash = 0x5C,

    /// <summary>Represents the <c>]</c> character.</summary>
    RightSquareBracket = 0x5D,

    /// <summary>Represents the <c>^</c> character.</summary>
    Caret = 0x5E,

    /// <summary>Represents the <c>_</c> character.</summary>
    Underscore = 0x5F,

    /// <summary>Represents the <c>`</c> character.</summary>
    GraveAccent = 0x60,

    /// <summary>Represents the <c>a</c> character.</summary>
    LowercaseA = 0x61,

    /// <summary>Represents the <c>b</c> character.</summary>
    LowercaseB = 0x62,

    /// <summary>Represents the <c>c</c> character.</summary>
    LowercaseC = 0x63,

    /// <summary>Represents the <c>d</c> character.</summary>
    LowercaseD = 0x64,

    /// <summary>Represents the <c>e</c> character.</summary>
    LowercaseE = 0x65,

    /// <summary>Represents the <c>f</c> character.</summary>
    LowercaseF = 0x66,

    /// <summary>Represents the <c>g</c> character.</summary>
    LowercaseG = 0x67,

    /// <summary>Represents the <c>h</c> character.</summary>
    LowercaseH = 0x68,

    /// <summary>Represents the <c>i</c> character.</summary>
    LowercaseI = 0x69,

    /// <summary>Represents the <c>j</c> character.</summary>
    LowercaseJ = 0x6A,

    /// <summary>Represents the <c>k</c> character.</summary>
    LowercaseK = 0x6B,

    /// <summary>Represents the <c>l</c> character.</summary>
    LowercaseL = 0x6C,

    /// <summary>Represents the <c>m</c> character.</summary>
    LowercaseM = 0x6D,

    /// <summary>Represents the <c>n</c> character.</summary>
    LowercaseN = 0x6E,

    /// <summary>Represents the <c>o</c> character.</summary>
    LowercaseO = 0x6F,

    /// <summary>Represents the <c>p</c> character.</summary>
    LowercaseP = 0x70,

    /// <summary>Represents the <c>q</c> character.</summary>
    LowercaseQ = 0x71,

    /// <summary>Represents the <c>r</c> character.</summary>
    LowercaseR = 0x72,

    /// <summary>Represents the <c>s</c> character.</summary>
    LowercaseS = 0x73,

    /// <summary>Represents the <c>t</c> character.</summary>
    LowercaseT = 0x74,

    /// <summary>Represents the <c>u</c> character.</summary>
    LowercaseU = 0x75,

    /// <summary>Represents the <c>v</c> character.</summary>
    LowercaseV = 0x76,

    /// <summary>Represents the <c>w</c> character.</summary>
    LowercaseW = 0x77,

    /// <summary>Represents the <c>x</c> character.</summary>
    LowercaseX = 0x78,

    /// <summary>Represents the <c>y</c> character.</summary>
    LowercaseY = 0x79,

    /// <summary>Represents the <c>z</c> character.</summary>
    LowercaseZ = 0x7A,

    /// <summary>Represents the <c>{</c> character.</summary>
    LeftCurlyBrace = 0x7B,

    /// <summary>Represents the <c>|</c> character.</summary>
    VerticalBar = 0x7C,

    /// <summary>Represents the <c>}</c> character.</summary>
    RightCurlyBrace = 0x7D,

    /// <summary>Represents the <c>~</c> character.</summary>
    Tilde = 0x7E,

    /// <summary>Represents the <c>DEL</c> character.</summary>
    Delete = 0x7F,
}
