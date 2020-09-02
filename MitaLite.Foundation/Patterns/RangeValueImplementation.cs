// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.RangeValueImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class RangeValueImplementation : PatternImplementation<RangeValuePattern>, IRangeValue
  {
    public RangeValueImplementation(UIObject uiObject)
      : base(uiObject, RangeValuePattern.Pattern)
    {
    }

    public double Minimum
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Minimum)), out overridden) == ActionResult.Handled ? (double) overridden : this.Pattern.Current.Minimum;
      }
    }

    public double Maximum
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Maximum)), out overridden) == ActionResult.Handled ? (double) overridden : this.Pattern.Current.Maximum;
      }
    }

    public double LargeChange
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (LargeChange)), out overridden) == ActionResult.Handled ? (double) overridden : this.Pattern.Current.LargeChange;
      }
    }

    public double SmallChange
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (SmallChange)), out overridden) == ActionResult.Handled ? (double) overridden : this.Pattern.Current.SmallChange;
      }
    }

    public void SetValue(double value)
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs("RangeSetValue", new object[1]
      {
        (object) value
      })) == ActionResult.Unhandled)
        this.Pattern.SetValue(value);
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("UIScrollComplete"));
    }

    public double Value
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("RangeValue"), out overridden) == ActionResult.Handled ? (double) overridden : this.Pattern.Current.Value;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("RangeIsReadOnly"), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.IsReadOnly;
      }
    }
  }
}
