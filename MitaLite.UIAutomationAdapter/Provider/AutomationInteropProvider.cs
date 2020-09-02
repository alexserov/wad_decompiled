// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Provider.AutomationInteropProvider
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;
using UIAutomationClient;

namespace System.Windows.Automation.Provider
{
  public static class AutomationInteropProvider
  {
    public const int AppendRuntimeId = 3;
    public const int InvalidateLimit = 20;
    public const int ItemsInvalidateLimit = 5;
    public const int RootObjectId = -25;
    private static readonly object lockObject = new object();
    private static object notSupportedValue;

    public static bool ClientsAreListening => NativeMethods.UiaClientsAreListening();

    public static IRawElementProviderSimple HostProviderFromHandle(
      IntPtr windowHandle)
    {
      IRawElementProviderSimple provider = (IRawElementProviderSimple) null;
      Marshal.ThrowExceptionForHR(NativeMethods.UiaHostProviderFromHwnd(windowHandle, out provider));
      return provider;
    }

    public static void RaiseAutomationEvent(
      AutomationEvent eventId,
      IRawElementProviderSimple provider,
      AutomationEventArgs e)
    {
      Validate.ArgumentNotNull((object) provider, nameof (provider));
      Validate.ArgumentNotNull((object) e, nameof (e));
      if (e.EventId == AutomationElementIdentifiers.AsyncContentLoadedEvent)
      {
        if (!(e is AsyncContentLoadedEventArgs contentLoadedEventArgs))
          throw new ArgumentException("args");
        Marshal.ThrowExceptionForHR(NativeMethods.UiaRaiseAsyncContentLoadedEvent(provider, contentLoadedEventArgs.AsyncContentLoadedState, contentLoadedEventArgs.PercentComplete));
      }
      else
      {
        if (e.EventId == WindowPatternIdentifiers.WindowClosedEvent && !(e is WindowClosedEventArgs))
          throw new ArgumentException("e.EventId");
        Marshal.ThrowExceptionForHR(NativeMethods.UiaRaiseAutomationEvent(provider, eventId.Id));
      }
    }

    public static void RaiseAutomationPropertyChangedEvent(
      IRawElementProviderSimple element,
      AutomationPropertyChangedEventArgs e)
    {
      Validate.ArgumentNotNull((object) element, nameof (element));
      Validate.ArgumentNotNull((object) e, nameof (e));
      Marshal.ThrowExceptionForHR(NativeMethods.UiaRaiseAutomationPropertyChangedEvent(element, e.Property.Id, e.OldValue, e.NewValue));
    }

    public static void RaiseStructureChangedEvent(
      IRawElementProviderSimple provider,
      StructureChangedEventArgs e)
    {
      Validate.ArgumentNotNull((object) provider, nameof (provider));
      Validate.ArgumentNotNull((object) e, nameof (e));
      int[] runtimeId = e.GetRuntimeId();
      Marshal.ThrowExceptionForHR(NativeMethods.UiaRaiseStructureChangedEvent(provider, e.StructureChangeType, runtimeId, runtimeId.Length));
    }

    public static IntPtr ReturnRawElementProvider(
      IntPtr windowHandle,
      IntPtr wParam,
      IntPtr lParam,
      IRawElementProviderSimple el) => NativeMethods.UiaReturnRawElementProvider(windowHandle, wParam, lParam, el);

    public static object NotSupportedValue
    {
      get
      {
        lock (AutomationInteropProvider.lockObject)
        {
          if (AutomationInteropProvider.notSupportedValue == null)
            Marshal.ThrowExceptionForHR(NativeMethods.UiaGetReservedNotSupportedValue(out AutomationInteropProvider.notSupportedValue));
          return AutomationInteropProvider.notSupportedValue;
        }
      }
    }
  }
}
