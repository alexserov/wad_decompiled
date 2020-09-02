// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationPropertyChangedEventArgs
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public sealed class AutomationPropertyChangedEventArgs : AutomationEventArgs
  {
    private object _oldValue;
    private object _newValue;
    private AutomationProperty _property;

    public AutomationPropertyChangedEventArgs(
      AutomationProperty property,
      object oldValue,
      object newValue)
      : base(AutomationElement.AutomationPropertyChangedEvent)
    {
      Validate.ArgumentNotNull((object) property, nameof (property));
      Validate.ArgumentNotNull(newValue, nameof (newValue));
      this._oldValue = oldValue;
      this._newValue = newValue;
      this._property = property;
    }

    public object NewValue => this._newValue;

    public object OldValue => this._oldValue;

    public AutomationProperty Property => this._property;
  }
}
