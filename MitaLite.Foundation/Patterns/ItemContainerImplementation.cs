// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ItemContainerImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class ItemContainerImplementation : PatternImplementation<ItemContainerPattern>, IItemContainer {
        public ItemContainerImplementation(UIObject uiObject)
            : base(uiObject: uiObject, patternIdentifier: ItemContainerPattern.Pattern) {
        }

        public UIObject FindItemByProperty(
            UIObject uiObject,
            UIProperty uiProperty,
            object value) {
            Validate.ArgumentNotNull(parameter: uiProperty, parameterName: nameof(uiProperty));
            if (value != null && uiProperty == UIProperty.Get(property: AutomationElement.SearchVirtualItemsProperty))
                throw new ArgumentException(message: StringResource.Get(id: "FindItemByProperty_ArgumentException"));
            var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
            var element = uiObject == (UIObject) null ? Pattern.FindItemByProperty(element: null, property: uiProperty.Property, value: value) : Pattern.FindItemByProperty(element: uiObject.AutomationElement, property: uiProperty.Property, value: value);
            return !(element == null) ? new UIObject(element: element) : null;
        }
    }
}