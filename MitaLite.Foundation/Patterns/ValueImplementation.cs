// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ValueImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class ValueImplementation : PatternImplementation<ValuePattern>, IValue
  {
    public ValueImplementation(UIObject uiObject)
      : base(uiObject, ValuePattern.Pattern)
    {
    }

    public void SetValue(string value)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) value, nameof (value));
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (SetValue), new object[1]
      {
        (object) value
      })) == ActionResult.Unhandled)
        this.Pattern.SetValue(value);
      int num3 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("UIScrollComplete"));
    }

    public string Value
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (Value)), out overridden) == ActionResult.Handled ? (string) overridden : this.Pattern.Current.Value;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (IsReadOnly)), out overridden) == ActionResult.Handled ? (bool) overridden : this.Pattern.Current.IsReadOnly;
      }
    }
  }
}
