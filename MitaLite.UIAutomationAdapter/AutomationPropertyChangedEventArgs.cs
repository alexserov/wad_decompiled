// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationPropertyChangedEventArgs
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation {
    public sealed class AutomationPropertyChangedEventArgs : AutomationEventArgs {
        public AutomationPropertyChangedEventArgs(
            AutomationProperty property,
            object oldValue,
            object newValue)
            : base(eventId: AutomationElement.AutomationPropertyChangedEvent) {
            Validate.ArgumentNotNull(parameter: property, parameterName: nameof(property));
            Validate.ArgumentNotNull(parameter: newValue, parameterName: nameof(newValue));
            OldValue = oldValue;
            NewValue = newValue;
            Property = property;
        }

        public object NewValue { get; }

        public object OldValue { get; }

        public AutomationProperty Property { get; }
    }
}