// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.ModifierKeys
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation
{
  [Flags]
  public enum ModifierKeys
  {
    None = 0,
    ShiftFlag = 64, // 0x00000040
    ControlFlag = 128, // 0x00000080
    AltFlag = 256, // 0x00000100
    LeftShiftFlag = 1,
    RightShiftFlag = 2,
    LeftControlFlag = 4,
    RightControlFlag = 8,
    LeftAltFlag = 16, // 0x00000010
    RightAltFlag = 32, // 0x00000020
    LeftWindowsFlag = 1024, // 0x00000400
    RightWindowsFlag = 2048, // 0x00000800
  }
}
