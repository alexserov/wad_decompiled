// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.PointerButtons
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation {
    [Flags]
    public enum PointerButtons {
        None = 0,
        Primary = 1,
        Secondary = 2,
        PhysicalLeft = 4,
        PhysicalRight = 8,
        Middle = 16, // 0x00000010
        XButton1 = 32, // 0x00000020
        XButton2 = 64 // 0x00000040
    }
}