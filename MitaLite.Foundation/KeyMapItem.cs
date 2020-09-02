// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.KeyMapItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation
{
  internal class KeyMapItem
  {
    public readonly VirtualKey VirtualKey;
    public KeyScanCodes ScanCodes;

    public KeyMapItem(VirtualKey virtualKey, KeyScanCodes scanCodes)
    {
      this.VirtualKey = virtualKey;
      this.ScanCodes = scanCodes;
    }
  }
}
