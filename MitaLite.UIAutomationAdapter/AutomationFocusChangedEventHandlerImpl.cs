// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationFocusChangedEventHandlerImpl
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  internal class AutomationFocusChangedEventHandlerImpl : UIAutomationEventHandler<AutomationFocusChangedEventHandlerImpl>, IUIAutomationFocusChangedEventHandler
  {
    private AutomationFocusChangedEventHandler _handlingDelegate;

    private AutomationFocusChangedEventHandlerImpl(
      AutomationFocusChangedEventHandler handlingDelegate) => this._handlingDelegate = handlingDelegate;

    protected override void Remove() => Boundary.NoExceptions((Action) (() => System.Windows.Automation.Automation.AutomationClass.RemoveFocusChangedEventHandler((IUIAutomationFocusChangedEventHandler) this)));

    void IUIAutomationFocusChangedEventHandler.HandleFocusChangedEvent(
      IUIAutomationElement sender)
    {
      AutomationElement automationElement = (AutomationElement) null;
      if (sender != null)
        automationElement = new AutomationElement(sender);
      this._handlingDelegate((object) automationElement, (AutomationFocusChangedEventArgs) null);
    }

    internal static void Add(
      AutomationFocusChangedEventHandler handlingDelegate)
    {
      AutomationFocusChangedEventHandlerImpl e = new AutomationFocusChangedEventHandlerImpl(handlingDelegate);
      IUIAutomationCacheRequest cacheRequest = AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest;
      Boundary.UIAutomation((Action) (() => System.Windows.Automation.Automation.AutomationClass.AddFocusChangedEventHandler(cacheRequest, (IUIAutomationFocusChangedEventHandler) e)));
      UIAutomationEventHandler<AutomationFocusChangedEventHandlerImpl>.Add(e);
    }

    internal static void Remove(
      AutomationFocusChangedEventHandler handlingDelegate) => UIAutomationEventHandler<AutomationFocusChangedEventHandlerImpl>.Remove((Predicate<AutomationFocusChangedEventHandlerImpl>) (item => item._handlingDelegate == handlingDelegate));
  }
}
