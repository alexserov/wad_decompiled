// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ScrollImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class ScrollImplementation : PatternImplementation<ScrollPattern>, IScroll
  {
    public ScrollImplementation(UIObject uiObject)
      : base(uiObject, ScrollPattern.Pattern)
    {
    }

    public void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (Scroll), new object[2]
      {
        (object) horizontalAmount,
        (object) verticalAmount
      })) == ActionResult.Unhandled)
        this.Pattern.Scroll(horizontalAmount, verticalAmount);
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("UIScrollComplete"));
    }

    public void ScrollHorizontal(ScrollAmount amount)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (ScrollHorizontal), new object[1]
      {
        (object) amount
      })) == ActionResult.Unhandled)
        this.Pattern.ScrollHorizontal(amount);
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("UIScrollComplete"));
    }

    public void ScrollVertical(ScrollAmount amount)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (ScrollVertical), new object[1]
      {
        (object) amount
      })) == ActionResult.Unhandled)
        this.Pattern.ScrollVertical(amount);
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("UIScrollComplete"));
    }

    public void SetScrollPercent(double horizontalPercent, double verticalPercent)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (SetScrollPercent), new object[2]
      {
        (object) horizontalPercent,
        (object) verticalPercent
      })) == ActionResult.Unhandled)
        this.Pattern.SetScrollPercent(horizontalPercent, verticalPercent);
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("UIScrollComplete"));
    }

    public bool HorizontallyScrollable
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (HorizontallyScrollable)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.HorizontallyScrollable;
      }
    }

    public bool VerticallyScrollable
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (VerticallyScrollable)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.VerticallyScrollable;
      }
    }

    public double HorizontalScrollPercent
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (HorizontalScrollPercent)), out overridden) == ActionResult.Handled ? (double) overridden : this.Pattern.Current.HorizontalScrollPercent;
      }
    }

    public double VerticalScrollPercent
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (VerticalScrollPercent)), out overridden) == ActionResult.Handled ? (double) overridden : this.Pattern.Current.VerticalScrollPercent;
      }
    }

    public double HorizontalViewSize
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (HorizontalViewSize)), out overridden) == ActionResult.Handled ? (double) overridden : this.Pattern.Current.HorizontalViewSize;
      }
    }

    public double VerticalViewSize
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (VerticalViewSize)), out overridden) == ActionResult.Handled ? (double) overridden : this.Pattern.Current.VerticalViewSize;
      }
    }
  }
}
