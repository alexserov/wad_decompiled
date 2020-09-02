// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ItemContainerImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class ItemContainerImplementation : PatternImplementation<ItemContainerPattern>, IItemContainer
  {
    public ItemContainerImplementation(UIObject uiObject)
      : base(uiObject, ItemContainerPattern.Pattern)
    {
    }

    public UIObject FindItemByProperty(
      UIObject uiObject,
      UIProperty uiProperty,
      object value)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) uiProperty, nameof (uiProperty));
      if (value != null && uiProperty == UIProperty.Get(AutomationElement.SearchVirtualItemsProperty))
        throw new ArgumentException(StringResource.Get("FindItemByProperty_ArgumentException"));
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      AutomationElement element = uiObject == (UIObject) null ? this.Pattern.FindItemByProperty((AutomationElement) null, uiProperty.Property, value) : this.Pattern.FindItemByProperty(uiObject.AutomationElement, uiProperty.Property, value);
      return !(element == (AutomationElement) null) ? new UIObject(element) : (UIObject) null;
    }
  }
}
