// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.PropertyCondition
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class PropertyCondition : Condition {
        public PropertyCondition(AutomationProperty property, object value) {
            Init(property: property, value: value);
        }

        public AutomationProperty Property {
            get { return IUIAutomationCondition is IUIAutomationPropertyCondition automationCondition ? AutomationProperty.LookupById(id: automationCondition.propertyId) : null; }
        }

        public object Value {
            get { return IUIAutomationCondition is IUIAutomationPropertyCondition automationCondition ? automationCondition.PropertyValue.ToObject() : null; }
        }

        public PropertyConditionFlags Flags {
            get { return PropertyConditionFlags.None; }
        }

        void Init(AutomationProperty property, object value) {
            if (property == AutomationElement.ControlTypeProperty) {
                switch (value) {
                    case ControlType controlType2:
                        this._condition = Automation.AutomationClass.CreatePropertyCondition(propertyId: property.Id, value: controlType2.Id.ToVariant());
                        break;
                    case int _:
                        var controlType1 = ControlType.LookupById(id: int.Parse(s: value.ToString()));
                        this._condition = controlType1 == null ? null : Automation.AutomationClass.CreatePropertyCondition(propertyId: property.Id, value: controlType1.Id.ToVariant());
                        break;
                }
            } else {
                var variant = value.ToVariant();
                this._condition = Automation.AutomationClass.CreatePropertyCondition(propertyId: property.Id, value: variant);
                variant.Free();
            }
        }
    }
}