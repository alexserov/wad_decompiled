// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.KeyScanCodes
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation
{
  internal struct KeyScanCodes
  {
    public ushort MakeCode;
    public ushort BreakCode;

    public KeyScanCodes(ushort makeCode, ushort breakCode)
    {
      this.MakeCode = makeCode;
      this.BreakCode = breakCode;
    }
  }
}
