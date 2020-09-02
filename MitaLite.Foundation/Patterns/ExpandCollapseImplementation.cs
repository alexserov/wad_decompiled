// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ExpandCollapseImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Waiters;
using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class ExpandCollapseImplementation : PatternImplementation<ExpandCollapsePattern>, IExpandCollapse
  {
    public ExpandCollapseImplementation(UIObject uiObject)
      : base(uiObject, ExpandCollapsePattern.Pattern)
    {
    }

    public void Collapse()
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Collapse))) != ActionResult.Unhandled)
        return;
      this.Pattern.Collapse();
    }

    public void Expand()
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Expand))) != ActionResult.Unhandled)
        return;
      this.Pattern.Expand();
    }

    public ExpandCollapseState ExpandCollapseState
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (ExpandCollapseState)), out overridden) == ActionResult.Handled ? (ExpandCollapseState) overridden : this.Pattern.Current.ExpandCollapseState;
      }
    }

    public UIEventWaiter GetCollapsedWaiter()
    {
      PropertyChangedEventWaiter changedEventWaiter = new PropertyChangedEventWaiter(this.UIObject, Scope.Subtree, new UIProperty[1]
      {
        UIProperty.Get("ExpandCollapse.ExpandCollapseState")
      });
      changedEventWaiter.AddFilter(new Predicate<WaiterEventArgs>(ExpandCollapseImplementation.CollapseFilter));
      return (UIEventWaiter) changedEventWaiter;
    }

    public UIEventWaiter GetExpandedWaiter()
    {
      PropertyChangedEventWaiter changedEventWaiter = new PropertyChangedEventWaiter(this.UIObject, Scope.Subtree, new UIProperty[1]
      {
        UIProperty.Get("ExpandCollapse.ExpandCollapseState")
      });
      changedEventWaiter.AddFilter(new Predicate<WaiterEventArgs>(ExpandCollapseImplementation.ExpandFilter));
      return (UIEventWaiter) changedEventWaiter;
    }

    private static bool CollapseFilter(WaiterEventArgs args) => ExpandCollapseImplementation.ExpandCollapseStateFilter((AutomationPropertyChangedEventArgs) args.EventArgs, ExpandCollapseState.Collapsed);

    private static bool ExpandFilter(WaiterEventArgs args) => ExpandCollapseImplementation.ExpandCollapseStateFilter((AutomationPropertyChangedEventArgs) args.EventArgs, ExpandCollapseState.Expanded);

    private static bool ExpandCollapseStateFilter(
      AutomationPropertyChangedEventArgs args,
      ExpandCollapseState state)
    {
      bool flag = false;
      if (ExpandCollapsePattern.ExpandCollapseStateProperty == args.Property && state == (ExpandCollapseState) args.NewValue)
        flag = true;
      return flag;
    }
  }
}
