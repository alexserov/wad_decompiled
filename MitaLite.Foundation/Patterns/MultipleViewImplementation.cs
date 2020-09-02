// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.MultipleViewImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class MultipleViewImplementation : PatternImplementation<MultipleViewPattern>, IMultipleView
  {
    public MultipleViewImplementation(UIObject uiObject)
      : base(uiObject, MultipleViewPattern.Pattern)
    {
    }

    public string GetViewName(int viewId)
    {
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      object overridden;
      return ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (GetViewName), new object[1]
      {
        (object) viewId
      }), out overridden) == ActionResult.Handled ? (string) overridden : this.Pattern.GetViewName(viewId);
    }

    public void SetCurrentView(int viewId)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (SetCurrentView), new object[1]
      {
        (object) viewId
      })) == ActionResult.Unhandled)
        this.Pattern.SetCurrentView(viewId);
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("UIScrollComplete"));
    }

    public int[] GetSupportedViews()
    {
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      object overridden;
      return ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (GetSupportedViews), Array.Empty<object>()), out overridden) == ActionResult.Handled ? (int[]) overridden : this.Pattern.Current.GetSupportedViews();
    }

    public int CurrentView
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (CurrentView), Array.Empty<object>()), out overridden) == ActionResult.Handled ? (int) overridden : this.Pattern.Current.CurrentView;
      }
    }
  }
}
