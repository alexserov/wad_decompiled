// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UAPApp
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using Windows.System.Profile;
using MS.Internal.Mita.AppModel;
using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation {
    public static class UAPApp {
        public enum ActivateOptions {
            None = 0,
            DesignMode = 1,
            NoErrorUI = 2,
            NoSplashScreen = 4
        }

        const string SHELL_NAMESPACE_API_DLL = "api-ms-win-shell-namespace-l1-1-0.dll";
        const string STORAGE_API_DLL = "api-ms-win-storage-exports-external-l1-1-0.dll";
        const string ISHELLITEM_IID = "43826d1e-e718-42ee-bc55-a1e261c37bfe";
        const string ISHELLITEMARRAY_IID = "b63ea76d-1f85-456f-a19c-48159efa858b";
        const string SCALING_DLL = "api-ms-win-shcore-scaling-l1-1-1.dll";
        const int S_OK = 0;
        const int E_INVALIDARG = -2147024809;
        const int E_ACCESSDENIED = -2147024891;
        const int InternalCall = 4096;

        public static UIObject Launch(string appName, UICondition topLevelWindowCondition) {
            Log.Out(msg: nameof(Launch));
            UIObject source;
            using (var appLaunchWaiter = new AppLaunchWaiter(topLevelWindowCondition: topLevelWindowCondition)) {
                new ApplicationActivationManager().ActivateApplication(appUserModelId: appName, arguments: null, options: ActivateOptions.None, processId: out var _);
                appLaunchWaiter.Wait();
                source = appLaunchWaiter.Source;
            }

            return GetTopLevelUIObject(topWindow: source);
        }

        public static UIObject LaunchEx(string appName, UICondition topLevelWindowCondition) {
            Log.Out(msg: "Launch");
            UIObject source;
            using (var appLaunchWaiter = new AppLaunchWaiter(topLevelWindowCondition: topLevelWindowCondition)) {
                AppStateManagementUtils.LaunchApplication(AumId: appName);
                appLaunchWaiter.Wait();
                source = appLaunchWaiter.Source;
            }

            return GetTopLevelUIObject(topWindow: source);
        }

        public static UIObject LaunchForProtocol(
            string appName,
            string uri,
            UICondition topLevelWindowCondition) {
            Log.Out(msg: nameof(LaunchForProtocol));
            var riid1 = new Guid(g: "43826d1e-e718-42ee-bc55-a1e261c37bfe");
            IShellItem shellItem;
            SHCreateItemFromParsingName(path: uri, pbc: IntPtr.Zero, riid: ref riid1, shellItem: out shellItem);
            var riid2 = new Guid(g: "b63ea76d-1f85-456f-a19c-48159efa858b");
            IShellItemArray ppenum;
            STORAGE_SHCreateShellItemArrayFromShellItem(psi: shellItem, riid: ref riid2, ppenum: out ppenum);
            UIObject source;
            using (var appLaunchWaiter = new AppLaunchWaiter(topLevelWindowCondition: topLevelWindowCondition)) {
                new ApplicationActivationManager().ActivateForProtocol(appUserModelId: appName, itemArray: ppenum, processId: out var _);
                appLaunchWaiter.Wait();
                source = appLaunchWaiter.Source;
            }

            return GetTopLevelUIObject(topWindow: source);
        }

        public static UIObject GetTopLevelUIObject(UICondition topLevelWindowCondition) {
            if (topLevelWindowCondition == null)
                throw new ArgumentNullException(paramName: nameof(topLevelWindowCondition));
            Log.Out(msg: "GetTopLevelUIObject: condition");
            UIObject element;
            if (!UIObject.Root.Descendants.TryFind(condition: topLevelWindowCondition, element: out element))
                throw new InvalidOperationException(message: "topLevelWindowCondition didn't find an element after app activation completed.");
            return GetTopLevelUIObject(topWindow: element);
        }

        public static UIObject GetTopLevelUIObject(UIObject topWindow) {
            if (topWindow == null)
                throw new ArgumentNullException(paramName: nameof(topWindow));
            Log.Out(msg: "GetTopLevelUIObject: object");
            UIObject element = null;
            if ((ControlType) topWindow.GetProperty(property: UIProperty.Get(name: "ControlType")) != ControlType.Window)
                throw new InvalidOperationException(message: "topWindow didn't find a window (ControlType==Window) after app activation completed.");
            if (topWindow.ClassName.Equals(value: "Windows.UI.Core.CoreWindow", comparisonType: StringComparison.OrdinalIgnoreCase)) {
                element = topWindow;
            } else {
                uint num;
                for (num = 0U; num < 10U && !topWindow.Children.TryFind(condition: UICondition.CreateFromClassName(className: "Windows.UI.Core.CoreWindow").AndWith(newCondition: UICondition.CreateFromName(name: topWindow.Name)), element: out element); ++num)
                    Thread.Sleep(millisecondsTimeout: 100);
                if (element == null)
                    throw new UIObjectNotFoundException(message: string.Format(format: "Unable to find {0} in {1}", arg0: "Windows.UI.Core.CoreWindow", arg1: topWindow));
                Log.Out(msg: "Found {0} in {1} tries", (object) "Windows.UI.Core.CoreWindow", (object) num);
            }

            return element;
        }

        public static UICondition CreateTopLevelWindowCondition() {
            var deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
            return deviceFamily.Equals(value: "Windows.Desktop", comparisonType: StringComparison.OrdinalIgnoreCase) || deviceFamily.Equals(value: "Windows.Server", comparisonType: StringComparison.OrdinalIgnoreCase) || deviceFamily.Equals(value: "Windows.Team", comparisonType: StringComparison.OrdinalIgnoreCase) ? UICondition.CreateFromClassName(className: "ApplicationFrameWindow") : UICondition.CreateFromClassName(className: "Windows.UI.Core.CoreWindow");
        }

        public static void CloseWindow(UIObject window) {
            if (window == null)
                throw new ArgumentNullException(paramName: nameof(window));
            using (var processClosedWaiter = new ProcessClosedWaiter(processId: window.ProcessId)) {
                Process.GetProcessById(processId: window.ProcessId).Kill();
                processClosedWaiter.Wait();
            }

            Thread.Sleep(millisecondsTimeout: 750);
        }

        public static void CloseWindow() {
            AppStateManagementUtils.CloseApplication();
        }

        public static void SetTestDPIAwareness() {
            switch (SetProcessDpiAwareness(value: ProcessDPIAwareness.PerMonitorDPIAware)) {
                case -2147024891:
                    Log.Out(msg: "Per monitor DPI awareness is already set.");
                    break;
                case -2147024809:
                    throw new ArgumentException(message: "The value passed in is not valid.");
                case 0:
                    Log.Out(msg: "Per monitor DPI awareness for the app was set successfully.");
                    break;
                default:
                    throw new Exception(message: "SetProcessDpiAwareness() returned unexpected value");
            }
        }

        [DllImport(dllName: "api-ms-win-shell-namespace-l1-1-0.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        static extern void SHCreateItemFromParsingName(
            [MarshalAs(unmanagedType: UnmanagedType.LPWStr), In]
            string path,
            [In] IntPtr pbc,
            [In] ref Guid riid,
            [MarshalAs(unmanagedType: UnmanagedType.Interface)]
            out IShellItem shellItem);

        [DllImport(dllName: "api-ms-win-storage-exports-external-l1-1-0.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        static extern void STORAGE_SHCreateShellItemArrayFromShellItem(
            [In] IShellItem psi,
            [In] ref Guid riid,
            [MarshalAs(unmanagedType: UnmanagedType.Interface)]
            out IShellItemArray ppenum);

        [DllImport(dllName: "api-ms-win-shcore-scaling-l1-1-1.dll")]
        static extern int SetProcessDpiAwareness([In] ProcessDPIAwareness value);

        enum ProcessDPIAwareness {
            DPIUnaware,
            SystemDPIAware,
            PerMonitorDPIAware
        }

        [Guid(guid: "2e941141-7f97-4756-ba1d-9decde894a3d"), InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), ComImport]
        interface IApplicationActivationManager {
            IntPtr ActivateApplication(
                [In] string appUserModelId,
                [In] string arguments,
                [In] ActivateOptions options,
                out uint processId);

            IntPtr ActivateForFile(
                [In] string appUserModelId,
                [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                IShellItemArray itemArray,
                [In] string verb,
                out uint processId);

            IntPtr ActivateForProtocol(
                [In] string appUserModelId,
                [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                IShellItemArray itemArray,
                out uint processId);
        }

        [Guid(guid: "45BA127D-10A8-46EA-8AB7-56EA9078943C"), ComImport]
        public class ApplicationActivationManager : IApplicationActivationManager {
            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall)]
            public extern IntPtr ActivateApplication(
                [In] string appUserModelId,
                [In] string arguments,
                [In] ActivateOptions options,
                out uint processId);

            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall)]
            public extern IntPtr ActivateForFile(
                [In] string appUserModelId,
                [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                IShellItemArray itemArray,
                [In] string verb,
                out uint processId);

            [MethodImpl(methodImplOptions: MethodImplOptions.InternalCall)]
            public extern IntPtr ActivateForProtocol(
                [In] string appUserModelId,
                [MarshalAs(unmanagedType: UnmanagedType.Interface), In]
                IShellItemArray itemArray,
                out uint processId);
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "b63ea76d-1f85-456f-a19c-48159efa858b"), ComImport]
        public interface IShellItemArray {
        }

        [InterfaceType(interfaceType: ComInterfaceType.InterfaceIsIUnknown), Guid(guid: "43826d1e-e718-42ee-bc55-a1e261c37bfe"), ComImport]
        public interface IShellItem {
        }
    }
}