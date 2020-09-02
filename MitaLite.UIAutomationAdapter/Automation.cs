// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Automation
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public static class Automation
  {
    public static readonly Condition RawViewCondition = new Condition(System.Windows.Automation.Automation.AutomationClass.RawViewCondition);
    public static readonly Condition ControlViewCondition = new Condition(System.Windows.Automation.Automation.AutomationClass.ControlViewCondition);
    public static readonly Condition ContentViewCondition = new Condition(System.Windows.Automation.Automation.AutomationClass.ContentViewCondition);
    internal static IUIAutomation _automation;

    public static bool Compare(AutomationElement el1, AutomationElement el2) => Convert.ToBoolean(Boundary.UIAutomation<int>((Func<int>) (() => System.Windows.Automation.Automation.AutomationClass.CompareElements(el1.IUIAutomationElement, el2.IUIAutomationElement))));

    public static bool Compare(int[] runtimeId1, int[] runtimeId2) => Convert.ToBoolean(Boundary.UIAutomation<int>((Func<int>) (() => System.Windows.Automation.Automation.AutomationClass.CompareRuntimeIds(runtimeId1, runtimeId2))));

    public static string PropertyName(AutomationProperty property) => Boundary.UIAutomation<string>((Func<string>) (() => System.Windows.Automation.Automation.AutomationClass.GetPropertyProgrammaticName(property.Id)));

    public static string PatternName(AutomationPattern pattern) => Boundary.UIAutomation<string>((Func<string>) (() => System.Windows.Automation.Automation.AutomationClass.GetPatternProgrammaticName(pattern.Id)));

    public static void AddAutomationEventHandler(
      AutomationEvent eventId,
      AutomationElement element,
      TreeScope scope,
      AutomationEventHandler eventHandler) => AutomationEventHandlerImpl.Add(eventId, element, scope, eventHandler);

    public static void RemoveAutomationEventHandler(
      AutomationEvent eventId,
      AutomationElement element,
      AutomationEventHandler eventHandler) => AutomationEventHandlerImpl.Remove(eventId, element, eventHandler);

    public static void AddAutomationPropertyChangedEventHandler(
      AutomationElement element,
      TreeScope scope,
      AutomationPropertyChangedEventHandler eventHandler,
      params AutomationProperty[] properties) => AutomationPropertyChangedEventHandlerImpl.Add(element, scope, eventHandler, properties);

    public static void RemoveAutomationPropertyChangedEventHandler(
      AutomationElement element,
      AutomationPropertyChangedEventHandler eventHandler) => AutomationPropertyChangedEventHandlerImpl.Remove(element, eventHandler);

    public static void AddStructureChangedEventHandler(
      AutomationElement element,
      TreeScope scope,
      StructureChangedEventHandler eventHandler) => StructureChangedEventHandlerImpl.Add(element, scope, eventHandler);

    public static void RemoveStructureChangedEventHandler(
      AutomationElement element,
      StructureChangedEventHandler eventHandler) => StructureChangedEventHandlerImpl.Remove(element, eventHandler);

    public static void AddAutomationFocusChangedEventHandler(
      AutomationFocusChangedEventHandler eventHandler) => AutomationFocusChangedEventHandlerImpl.Add(eventHandler);

    public static void RemoveAutomationFocusChangedEventHandler(
      AutomationFocusChangedEventHandler eventHandler) => AutomationFocusChangedEventHandlerImpl.Remove(eventHandler);

    public static void RemoveAllEventHandlers()
    {
      UIAutomationEventHandler<AutomationEventHandlerImpl>.RemoveAll();
      UIAutomationEventHandler<StructureChangedEventHandlerImpl>.RemoveAll();
      UIAutomationEventHandler<AutomationPropertyChangedEventHandlerImpl>.RemoveAll();
      UIAutomationEventHandler<AutomationFocusChangedEventHandlerImpl>.RemoveAll();
    }

    public static bool TrySetConnectionTimeout(TimeSpan timeout)
    {
      if (!(System.Windows.Automation.Automation.AutomationClass is IUIAutomation2 automationClass))
        return false;
      try
      {
        automationClass.ConnectionTimeout = Convert.ToUInt32(timeout.TotalMilliseconds);
        return true;
      }
      catch (OverflowException ex)
      {
        return false;
      }
    }

    public static bool TryGetConnectionTimeout(out TimeSpan timeout)
    {
      if (!(System.Windows.Automation.Automation.AutomationClass is IUIAutomation2 automationClass))
      {
        timeout = TimeSpan.Zero;
        return false;
      }
      timeout = TimeSpan.FromMilliseconds((double) automationClass.ConnectionTimeout);
      return true;
    }

    public static bool TrySetTransactionTimeout(TimeSpan timeout)
    {
      if (!(System.Windows.Automation.Automation.AutomationClass is IUIAutomation2 automationClass))
        return false;
      try
      {
        automationClass.TransactionTimeout = Convert.ToUInt32(timeout.TotalMilliseconds);
        return true;
      }
      catch (OverflowException ex)
      {
        return false;
      }
    }

    public static bool TryGetTransactionTimeout(out TimeSpan timeout)
    {
      if (!(System.Windows.Automation.Automation.AutomationClass is IUIAutomation2 automationClass))
      {
        timeout = TimeSpan.Zero;
        return false;
      }
      timeout = TimeSpan.FromMilliseconds((double) automationClass.TransactionTimeout);
      return true;
    }

    internal static IUIAutomation AutomationClass
    {
      get
      {
        if (System.Windows.Automation.Automation._automation == null)
          System.Windows.Automation.Automation._automation = (IUIAutomation) new CUIAutomation8Class();
        return System.Windows.Automation.Automation._automation;
      }
    }
  }
}
