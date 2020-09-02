// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.POINTER_FLAGS
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation {
    [Flags]
    public enum POINTER_FLAGS : uint {
        NONE = 0,
        NEW = 1,
        INRANGE = 2,
        INCONTACT = 4,
        FIRSTBUTTON = 16, // 0x00000010
        SECONDBUTTON = 32, // 0x00000020
        THIRDBUTTON = 64, // 0x00000040
        FOURTHBUTTON = 128, // 0x00000080
        FIFTHBUTTON = 256, // 0x00000100
        PRIMARY = 8192, // 0x00002000
        CONFIDENCE = 16384, // 0x00004000
        CANCELED = 32768, // 0x00008000
        DOWN = 65536, // 0x00010000
        UPDATE = 131072, // 0x00020000
        UP = 262144, // 0x00040000
        WHEEL = 524288, // 0x00080000
        HWHEEL = 1048576, // 0x00100000
        CAPTURECHANGED = 2097152, // 0x00200000
        HASTRANSFORM = 4194304, // 0x00400000
        HoverStartsOrMoves = UPDATE | INRANGE, // 0x00020002
        ContactDown = DOWN | INCONTACT | INRANGE, // 0x00010006
        ContactMoves = HoverStartsOrMoves | INCONTACT, // 0x00020006
        ContactUpToHover = UP | INRANGE, // 0x00040002
        HoverEnds = UPDATE, // 0x00020000
        End = UP // 0x00040000
    }
}