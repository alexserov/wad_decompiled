// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.WindowImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class WindowImplementation : PatternImplementation<WindowPattern>, IWindow
  {
    public WindowImplementation(UIObject uiObject)
      : base(uiObject, WindowPattern.Pattern)
    {
    }

    public void SetWindowVisualState(WindowVisualState state)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (SetWindowVisualState), new object[1]
      {
        (object) state
      })) != ActionResult.Unhandled)
        return;
      this.Pattern.SetWindowVisualState(state);
    }

    public void Close()
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Close))) != ActionResult.Unhandled)
        return;
      this.Pattern.Close();
    }

    public void WaitForInputIdle(int milliseconds)
    {
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (WaitForInputIdle), new object[1]
      {
        (object) milliseconds
      })) != ActionResult.Unhandled)
        return;
      this.Pattern.WaitForInputIdle(milliseconds);
    }

    public UIEventWaiter GetWindowClosedWaiter() => (UIEventWaiter) new WindowClosedWaiter(this.UIObject);

    public bool CanMaximize
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (CanMaximize)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.CanMaximize;
      }
    }

    public bool CanMinimize
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (CanMinimize)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.CanMinimize;
      }
    }

    public bool IsModal
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (IsModal)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.IsModal;
      }
    }

    public WindowVisualState WindowVisualState
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (WindowVisualState)), out overridden) == ActionResult.Handled ? (WindowVisualState) overridden : this.Pattern.Current.WindowVisualState;
      }
    }

    public WindowInteractionState WindowInteractionState
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (WindowInteractionState)), out overridden) == ActionResult.Handled ? (WindowInteractionState) overridden : this.Pattern.Current.WindowInteractionState;
      }
    }

    public bool IsTopmost
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (IsTopmost)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.IsTopmost;
      }
    }
  }
}
