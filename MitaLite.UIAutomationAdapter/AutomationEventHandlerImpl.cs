// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationEventHandlerImpl
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  internal class AutomationEventHandlerImpl : UIAutomationEventHandler<AutomationEventHandlerImpl>, IUIAutomationEventHandler
  {
    private int _eventId;
    private IUIAutomationElement _uiAutomationElement;
    private AutomationEventHandler _handlingDelegate;

    private AutomationEventHandlerImpl(
      int eventId,
      IUIAutomationElement uiAutomationElement,
      AutomationEventHandler handlingDelegate)
    {
      this._eventId = eventId;
      this._uiAutomationElement = uiAutomationElement;
      this._handlingDelegate = handlingDelegate;
    }

    protected override void Remove() => Boundary.NoExceptions((Action) (() => System.Windows.Automation.Automation.AutomationClass.RemoveAutomationEventHandler(this._eventId, this._uiAutomationElement, (IUIAutomationEventHandler) this)));

    void IUIAutomationEventHandler.HandleAutomationEvent(
      IUIAutomationElement sender,
      int eventId)
    {
      AutomationElement automationElement = (AutomationElement) null;
      if (sender != null)
        automationElement = new AutomationElement(sender);
      if (eventId == 20017)
        this._handlingDelegate((object) automationElement, (AutomationEventArgs) new WindowClosedEventArgs(automationElement.GetRuntimeId()));
      else
        this._handlingDelegate((object) automationElement, new AutomationEventArgs(AutomationEvent.LookupById(eventId)));
    }

    internal static void Add(
      AutomationEvent eventId,
      AutomationElement element,
      TreeScope scope,
      AutomationEventHandler handlingDelegate)
    {
      AutomationEventHandlerImpl e = new AutomationEventHandlerImpl(eventId.Id, element.IUIAutomationElement, handlingDelegate);
      IUIAutomationCacheRequest cacheRequest = AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest;
      Boundary.UIAutomation((Action) (() => System.Windows.Automation.Automation.AutomationClass.AddAutomationEventHandler(e._eventId, e._uiAutomationElement, UiaConvert.Convert(scope), cacheRequest, (IUIAutomationEventHandler) e)));
      UIAutomationEventHandler<AutomationEventHandlerImpl>.Add(e);
    }

    internal static void Remove(
      AutomationEvent eventId,
      AutomationElement element,
      AutomationEventHandler handlingDelegate) => UIAutomationEventHandler<AutomationEventHandlerImpl>.Remove((Predicate<AutomationEventHandlerImpl>) (item => item._eventId == eventId.Id && 1 == System.Windows.Automation.Automation.AutomationClass.CompareElements(item._uiAutomationElement, element.IUIAutomationElement) && item._handlingDelegate == handlingDelegate));
  }
}
