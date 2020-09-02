// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.VirtualizedItemImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class VirtualizedItemImplementation : PatternImplementation<VirtualizedItemPattern>, IVirtualizedItem
  {
    public VirtualizedItemImplementation(UIObject uiObject)
      : base(uiObject, VirtualizedItemPattern.Pattern)
    {
    }

    public void Realize()
    {
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      this.Pattern.Realize();
    }
  }
}
