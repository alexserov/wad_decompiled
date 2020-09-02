// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.PropertyCondition
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class PropertyCondition : Condition
  {
    public PropertyCondition(AutomationProperty property, object value) => this.Init(property, value);

    public AutomationProperty Property => this.IUIAutomationCondition is IUIAutomationPropertyCondition automationCondition ? AutomationProperty.LookupById(automationCondition.propertyId) : (AutomationProperty) null;

    public object Value => this.IUIAutomationCondition is IUIAutomationPropertyCondition automationCondition ? automationCondition.PropertyValue.ToObject() : (object) null;

    public PropertyConditionFlags Flags => PropertyConditionFlags.None;

    private void Init(AutomationProperty property, object value)
    {
      if (property == AutomationElement.ControlTypeProperty)
      {
        switch (value)
        {
          case ControlType controlType2:
            this._condition = System.Windows.Automation.Automation.AutomationClass.CreatePropertyCondition(property.Id, controlType2.Id.ToVariant());
            break;
          case int _:
            ControlType controlType1 = ControlType.LookupById(int.Parse(value.ToString()));
            this._condition = controlType1 == null ? (IUIAutomationCondition) null : System.Windows.Automation.Automation.AutomationClass.CreatePropertyCondition(property.Id, controlType1.Id.ToVariant());
            break;
        }
      }
      else
      {
        Variant variant = value.ToVariant();
        this._condition = System.Windows.Automation.Automation.AutomationClass.CreatePropertyCondition(property.Id, variant);
        variant.Free();
      }
    }
  }
}
