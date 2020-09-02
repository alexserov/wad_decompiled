// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.StructureChangedEventHandlerImpl
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  internal class StructureChangedEventHandlerImpl : UIAutomationEventHandler<StructureChangedEventHandlerImpl>, IUIAutomationStructureChangedEventHandler
  {
    private StructureChangedEventHandler _handlingDelegate;
    private IUIAutomationElement _uiAutomationElement;

    private StructureChangedEventHandlerImpl(
      IUIAutomationElement uiAutomationElement,
      StructureChangedEventHandler handlingDelegate)
    {
      this._uiAutomationElement = uiAutomationElement;
      this._handlingDelegate = handlingDelegate;
    }

    protected override void Remove() => Boundary.NoExceptions((Action) (() => System.Windows.Automation.Automation.AutomationClass.RemoveStructureChangedEventHandler(this._uiAutomationElement, (IUIAutomationStructureChangedEventHandler) this)));

    void IUIAutomationStructureChangedEventHandler.HandleStructureChangedEvent(
      IUIAutomationElement sender,
      UIAutomationClient.StructureChangeType changeType,
      int[] runtimeId)
    {
      AutomationElement automationElement = (AutomationElement) null;
      if (sender != null)
        automationElement = new AutomationElement(sender);
      this._handlingDelegate((object) automationElement, new StructureChangedEventArgs(UiaConvert.Convert(changeType), runtimeId));
    }

    internal static void Add(
      AutomationElement element,
      TreeScope scope,
      StructureChangedEventHandler handlingDelegate)
    {
      StructureChangedEventHandlerImpl e = new StructureChangedEventHandlerImpl(element.IUIAutomationElement, handlingDelegate);
      IUIAutomationCacheRequest cacheRequest = AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest;
      Boundary.UIAutomation((Action) (() => System.Windows.Automation.Automation.AutomationClass.AddStructureChangedEventHandler(e._uiAutomationElement, UiaConvert.Convert(scope), cacheRequest, (IUIAutomationStructureChangedEventHandler) e)));
      UIAutomationEventHandler<StructureChangedEventHandlerImpl>.Add(e);
    }

    internal static void Remove(
      AutomationElement element,
      StructureChangedEventHandler handlingDelegate) => UIAutomationEventHandler<StructureChangedEventHandlerImpl>.Remove((Predicate<StructureChangedEventHandlerImpl>) (item => item._handlingDelegate == handlingDelegate && 1 == System.Windows.Automation.Automation.AutomationClass.CompareElements(item._uiAutomationElement, element.IUIAutomationElement)));
  }
}
