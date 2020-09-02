// Decompiled with JetBrains decompiler
// Type: MitaBroker.CompareMethodValue
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

namespace MitaBroker
{
  internal class CompareMethodValue
  {
    public CompareMethodValue(CompareMethod method, string value)
    {
      this.CompareMethod = method;
      this.CompareValue = value;
    }

    public CompareMethod CompareMethod { set; get; }

    public string CompareValue { set; get; }
  }
}
