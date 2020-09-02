// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationPropertyChangedEventHandlerImpl
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;

namespace System.Windows.Automation
{
  internal class AutomationPropertyChangedEventHandlerImpl : UIAutomationEventHandler<AutomationPropertyChangedEventHandlerImpl>, IUIAutomationPropertyChangedEventHandler
  {
    private IUIAutomationElement _uiAutomationElement;
    private AutomationPropertyChangedEventHandler _handlingDelegate;

    private AutomationPropertyChangedEventHandlerImpl(
      IUIAutomationElement uiAutomationElement,
      AutomationPropertyChangedEventHandler handlingDelegate)
    {
      this._uiAutomationElement = uiAutomationElement;
      this._handlingDelegate = handlingDelegate;
    }

    protected override void Remove() => Boundary.NoExceptions((Action) (() => System.Windows.Automation.Automation.AutomationClass.RemovePropertyChangedEventHandler(this._uiAutomationElement, (IUIAutomationPropertyChangedEventHandler) this)));

    void IUIAutomationPropertyChangedEventHandler.HandlePropertyChangedEvent(
      IUIAutomationElement sender,
      int propertyId,
      Variant newValue)
    {
      AutomationElement automationElement = (AutomationElement) null;
      if (sender != null)
        automationElement = new AutomationElement(sender);
      this._handlingDelegate((object) automationElement, new AutomationPropertyChangedEventArgs(AutomationProperty.LookupById(propertyId), (object) null, (object) newValue));
    }

    internal static void Add(
      AutomationElement element,
      TreeScope scope,
      AutomationPropertyChangedEventHandler handlingDelegate,
      AutomationProperty[] properties)
    {
      AutomationPropertyChangedEventHandlerImpl e = new AutomationPropertyChangedEventHandlerImpl(element.IUIAutomationElement, handlingDelegate);
      IUIAutomationCacheRequest cacheRequest = AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest;
      int[] propertiesArray = ((IEnumerable<AutomationProperty>) properties).Select<AutomationProperty, int>((Func<AutomationProperty, int>) (p => p.Id)).ToArray<int>();
      Boundary.UIAutomation((Action) (() => System.Windows.Automation.Automation.AutomationClass.AddPropertyChangedEventHandler(e._uiAutomationElement, UiaConvert.Convert(scope), cacheRequest, (IUIAutomationPropertyChangedEventHandler) e, propertiesArray)));
      UIAutomationEventHandler<AutomationPropertyChangedEventHandlerImpl>.Add(e);
    }

    internal static void Remove(
      AutomationElement element,
      AutomationPropertyChangedEventHandler handlingDelegate) => UIAutomationEventHandler<AutomationPropertyChangedEventHandlerImpl>.Remove((Predicate<AutomationPropertyChangedEventHandlerImpl>) (item => 1 == System.Windows.Automation.Automation.AutomationClass.CompareElements(item._uiAutomationElement, element.IUIAutomationElement) && item._handlingDelegate == handlingDelegate));
  }
}
