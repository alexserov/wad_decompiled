// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Provider.AutomationInteropProvider
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;
using UIAutomationClient;

namespace System.Windows.Automation.Provider {
    public static class AutomationInteropProvider {
        public const int AppendRuntimeId = 3;
        public const int InvalidateLimit = 20;
        public const int ItemsInvalidateLimit = 5;
        public const int RootObjectId = -25;
        static readonly object lockObject = new object();
        static object notSupportedValue;

        public static bool ClientsAreListening {
            get { return NativeMethods.UiaClientsAreListening(); }
        }

        public static object NotSupportedValue {
            get {
                lock (lockObject) {
                    if (notSupportedValue == null)
                        Marshal.ThrowExceptionForHR(errorCode: NativeMethods.UiaGetReservedNotSupportedValue(punkNotSupportedValue: out notSupportedValue));
                    return notSupportedValue;
                }
            }
        }

        public static IRawElementProviderSimple HostProviderFromHandle(
            IntPtr windowHandle) {
            IRawElementProviderSimple provider = null;
            Marshal.ThrowExceptionForHR(errorCode: NativeMethods.UiaHostProviderFromHwnd(hwnd: windowHandle, provider: out provider));
            return provider;
        }

        public static void RaiseAutomationEvent(
            AutomationEvent eventId,
            IRawElementProviderSimple provider,
            AutomationEventArgs e) {
            Validate.ArgumentNotNull(parameter: provider, parameterName: nameof(provider));
            Validate.ArgumentNotNull(parameter: e, parameterName: nameof(e));
            if (e.EventId == AutomationElementIdentifiers.AsyncContentLoadedEvent) {
                if (!(e is AsyncContentLoadedEventArgs contentLoadedEventArgs))
                    throw new ArgumentException(message: "args");
                Marshal.ThrowExceptionForHR(errorCode: NativeMethods.UiaRaiseAsyncContentLoadedEvent(provider: provider, asyncContentLoadedState: contentLoadedEventArgs.AsyncContentLoadedState, PercentComplete: contentLoadedEventArgs.PercentComplete));
            } else {
                if (e.EventId == WindowPatternIdentifiers.WindowClosedEvent && !(e is WindowClosedEventArgs))
                    throw new ArgumentException(message: "e.EventId");
                Marshal.ThrowExceptionForHR(errorCode: NativeMethods.UiaRaiseAutomationEvent(provider: provider, id: eventId.Id));
            }
        }

        public static void RaiseAutomationPropertyChangedEvent(
            IRawElementProviderSimple element,
            AutomationPropertyChangedEventArgs e) {
            Validate.ArgumentNotNull(parameter: element, parameterName: nameof(element));
            Validate.ArgumentNotNull(parameter: e, parameterName: nameof(e));
            Marshal.ThrowExceptionForHR(errorCode: NativeMethods.UiaRaiseAutomationPropertyChangedEvent(provider: element, id: e.Property.Id, oldValue: e.OldValue, newValue: e.NewValue));
        }

        public static void RaiseStructureChangedEvent(
            IRawElementProviderSimple provider,
            StructureChangedEventArgs e) {
            Validate.ArgumentNotNull(parameter: provider, parameterName: nameof(provider));
            Validate.ArgumentNotNull(parameter: e, parameterName: nameof(e));
            var runtimeId = e.GetRuntimeId();
            Marshal.ThrowExceptionForHR(errorCode: NativeMethods.UiaRaiseStructureChangedEvent(provider: provider, structureChangeType: e.StructureChangeType, runtimeId: runtimeId, runtimeIdLen: runtimeId.Length));
        }

        public static IntPtr ReturnRawElementProvider(
            IntPtr windowHandle,
            IntPtr wParam,
            IntPtr lParam,
            IRawElementProviderSimple el) {
            return NativeMethods.UiaReturnRawElementProvider(hwnd: windowHandle, wParam: wParam, lParam: lParam, el: el);
        }
    }
}