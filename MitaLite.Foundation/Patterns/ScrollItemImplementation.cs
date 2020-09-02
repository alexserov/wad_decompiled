// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ScrollItemImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class ScrollItemImplementation : PatternImplementation<ScrollItemPattern>, IScrollItem
  {
    public ScrollItemImplementation(UIObject uiObject)
      : base(uiObject, ScrollItemPattern.Pattern)
    {
    }

    public void ScrollIntoView()
    {
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (ScrollIntoView), Array.Empty<object>())) != ActionResult.Unhandled)
        return;
      this.Pattern.ScrollIntoView();
    }
  }
}
