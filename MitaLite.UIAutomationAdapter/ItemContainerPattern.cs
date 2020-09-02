// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ItemContainerPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class ItemContainerPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<ItemContainerPattern, IUIAutomationItemContainerPattern>(10019, "ItemContainerPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationItemContainerPattern, ItemContainerPattern>(ItemContainerPattern.Wrap));
    private readonly IUIAutomationItemContainerPattern _itemContainerPattern;

    private ItemContainerPattern(
      AutomationElement element,
      IUIAutomationItemContainerPattern itemContainerPattern)
      : base(element)
      => this._itemContainerPattern = itemContainerPattern;

    internal static ItemContainerPattern Wrap(
      AutomationElement element,
      IUIAutomationItemContainerPattern itemContainerPattern) => new ItemContainerPattern(element, itemContainerPattern);

    public AutomationElement FindItemByProperty(
      AutomationElement element,
      AutomationProperty property,
      object value)
    {
      Validate.ArgumentNotNull((object) property, nameof (property));
      Variant variant = value.ToVariant();
      IUIAutomationElement autoElement = element == (AutomationElement) null ? this._itemContainerPattern.FindItemByProperty((IUIAutomationElement) null, property.Id, variant) : this._itemContainerPattern.FindItemByProperty(element.IUIAutomationElement, property.Id, variant);
      variant.Free();
      return autoElement != null ? new AutomationElement(autoElement) : (AutomationElement) null;
    }
  }
}
