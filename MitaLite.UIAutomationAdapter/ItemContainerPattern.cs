// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ItemContainerPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class ItemContainerPattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<ItemContainerPattern, IUIAutomationItemContainerPattern>(id: 10019, programmaticName: "ItemContainerPatternIdentifiers.Pattern", wrap: Wrap);
        readonly IUIAutomationItemContainerPattern _itemContainerPattern;

        ItemContainerPattern(
            AutomationElement element,
            IUIAutomationItemContainerPattern itemContainerPattern)
            : base(el: element) {
            this._itemContainerPattern = itemContainerPattern;
        }

        internal static ItemContainerPattern Wrap(
            AutomationElement element,
            IUIAutomationItemContainerPattern itemContainerPattern) {
            return new ItemContainerPattern(element: element, itemContainerPattern: itemContainerPattern);
        }

        public AutomationElement FindItemByProperty(
            AutomationElement element,
            AutomationProperty property,
            object value) {
            Validate.ArgumentNotNull(parameter: property, parameterName: nameof(property));
            var variant = value.ToVariant();
            var autoElement = element == (AutomationElement) null ? this._itemContainerPattern.FindItemByProperty(pStartAfter: null, propertyId: property.Id, value: variant) : this._itemContainerPattern.FindItemByProperty(pStartAfter: element.IUIAutomationElement, propertyId: property.Id, value: variant);
            variant.Free();
            return autoElement != null ? new AutomationElement(autoElement: autoElement) : null;
        }
    }
}