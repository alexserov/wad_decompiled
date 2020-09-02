// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ToggleImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class ToggleImplementation : PatternImplementation<TogglePattern>, IToggle
  {
    public ToggleImplementation(UIObject uiObject)
      : base(uiObject, TogglePattern.Pattern)
    {
    }

    public UIEventWaiter GetToggledWaiter() => (UIEventWaiter) new PropertyChangedEventWaiter(this.UIObject, Scope.Element, new UIProperty[1]
    {
      UIProperty.Get("Toggle.ToggleState")
    });

    public void Toggle()
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Toggle))) != ActionResult.Unhandled)
        return;
      this.Pattern.Toggle();
    }

    public ToggleState ToggleState
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (ToggleState)), out overridden) == ActionResult.Handled ? (ToggleState) overridden : this.Pattern.Current.ToggleState;
      }
    }
  }
}
