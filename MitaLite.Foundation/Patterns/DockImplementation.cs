// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.DockImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class DockImplementation : PatternImplementation<DockPattern>, IDock
  {
    public DockImplementation(UIObject uiObject)
      : base(uiObject, DockPattern.Pattern)
    {
    }

    public void SetDockPosition(DockPosition position)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (SetDockPosition), new object[1]
      {
        (object) position
      })) == ActionResult.Unhandled)
        this.Pattern.SetDockPosition(position);
      int num3 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("UIScrollComplete"));
    }

    public DockPosition DockPosition
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (DockPosition)), out overridden) == ActionResult.Handled ? (DockPosition) overridden : DockPosition.None;
      }
    }
  }
}
