// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UAPApp
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.AppModel;
using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Foundation.Waiters;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using Windows.System.Profile;

namespace MS.Internal.Mita.Foundation
{
  public static class UAPApp
  {
    private const string SHELL_NAMESPACE_API_DLL = "api-ms-win-shell-namespace-l1-1-0.dll";
    private const string STORAGE_API_DLL = "api-ms-win-storage-exports-external-l1-1-0.dll";
    private const string ISHELLITEM_IID = "43826d1e-e718-42ee-bc55-a1e261c37bfe";
    private const string ISHELLITEMARRAY_IID = "b63ea76d-1f85-456f-a19c-48159efa858b";
    private const string SCALING_DLL = "api-ms-win-shcore-scaling-l1-1-1.dll";
    private const int S_OK = 0;
    private const int E_INVALIDARG = -2147024809;
    private const int E_ACCESSDENIED = -2147024891;
    private const int InternalCall = 4096;

    public static UIObject Launch(string appName, UICondition topLevelWindowCondition)
    {
      Log.Out(nameof (Launch));
      UIObject source;
      using (AppLaunchWaiter appLaunchWaiter = new AppLaunchWaiter(topLevelWindowCondition))
      {
        new UAPApp.ApplicationActivationManager().ActivateApplication(appName, (string) null, UAPApp.ActivateOptions.None, out uint _);
        appLaunchWaiter.Wait();
        source = appLaunchWaiter.Source;
      }
      return UAPApp.GetTopLevelUIObject(source);
    }

    public static UIObject LaunchEx(string appName, UICondition topLevelWindowCondition)
    {
      Log.Out("Launch");
      UIObject source;
      using (AppLaunchWaiter appLaunchWaiter = new AppLaunchWaiter(topLevelWindowCondition))
      {
        AppStateManagementUtils.LaunchApplication(appName);
        appLaunchWaiter.Wait();
        source = appLaunchWaiter.Source;
      }
      return UAPApp.GetTopLevelUIObject(source);
    }

    public static UIObject LaunchForProtocol(
      string appName,
      string uri,
      UICondition topLevelWindowCondition)
    {
      Log.Out(nameof (LaunchForProtocol));
      Guid riid1 = new Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe");
      UAPApp.IShellItem shellItem;
      UAPApp.SHCreateItemFromParsingName(uri, IntPtr.Zero, ref riid1, out shellItem);
      Guid riid2 = new Guid("b63ea76d-1f85-456f-a19c-48159efa858b");
      UAPApp.IShellItemArray ppenum;
      UAPApp.STORAGE_SHCreateShellItemArrayFromShellItem(shellItem, ref riid2, out ppenum);
      UIObject source;
      using (AppLaunchWaiter appLaunchWaiter = new AppLaunchWaiter(topLevelWindowCondition))
      {
        new UAPApp.ApplicationActivationManager().ActivateForProtocol(appName, ppenum, out uint _);
        appLaunchWaiter.Wait();
        source = appLaunchWaiter.Source;
      }
      return UAPApp.GetTopLevelUIObject(source);
    }

    public static UIObject GetTopLevelUIObject(UICondition topLevelWindowCondition)
    {
      if (topLevelWindowCondition == null)
        throw new ArgumentNullException(nameof (topLevelWindowCondition));
      Log.Out("GetTopLevelUIObject: condition");
      UIObject element;
      if (!UIObject.Root.Descendants.TryFind(topLevelWindowCondition, out element))
        throw new InvalidOperationException("topLevelWindowCondition didn't find an element after app activation completed.");
      return UAPApp.GetTopLevelUIObject(element);
    }

    public static UIObject GetTopLevelUIObject(UIObject topWindow)
    {
      if (topWindow == (UIObject) null)
        throw new ArgumentNullException(nameof (topWindow));
      Log.Out("GetTopLevelUIObject: object");
      UIObject element = (UIObject) null;
      if ((ControlType) topWindow.GetProperty(UIProperty.Get("ControlType")) != ControlType.Window)
        throw new InvalidOperationException("topWindow didn't find a window (ControlType==Window) after app activation completed.");
      if (topWindow.ClassName.Equals("Windows.UI.Core.CoreWindow", StringComparison.OrdinalIgnoreCase))
      {
        element = topWindow;
      }
      else
      {
        uint num;
        for (num = 0U; num < 10U && !topWindow.Children.TryFind(UICondition.CreateFromClassName("Windows.UI.Core.CoreWindow").AndWith(UICondition.CreateFromName(topWindow.Name)), out element); ++num)
          Thread.Sleep(100);
        if (element == (UIObject) null)
          throw new UIObjectNotFoundException(string.Format("Unable to find {0} in {1}", (object) "Windows.UI.Core.CoreWindow", (object) topWindow));
        Log.Out("Found {0} in {1} tries", (object) "Windows.UI.Core.CoreWindow", (object) num);
      }
      return element;
    }

    public static UICondition CreateTopLevelWindowCondition()
    {
      string deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
      return deviceFamily.Equals("Windows.Desktop", StringComparison.OrdinalIgnoreCase) || deviceFamily.Equals("Windows.Server", StringComparison.OrdinalIgnoreCase) || deviceFamily.Equals("Windows.Team", StringComparison.OrdinalIgnoreCase) ? UICondition.CreateFromClassName("ApplicationFrameWindow") : UICondition.CreateFromClassName("Windows.UI.Core.CoreWindow");
    }

    public static void CloseWindow(UIObject window)
    {
      if (window == (UIObject) null)
        throw new ArgumentNullException(nameof (window));
      using (ProcessClosedWaiter processClosedWaiter = new ProcessClosedWaiter(window.ProcessId))
      {
        Process.GetProcessById(window.ProcessId).Kill();
        processClosedWaiter.Wait();
      }
      Thread.Sleep(750);
    }

    public static void CloseWindow() => AppStateManagementUtils.CloseApplication();

    public static void SetTestDPIAwareness()
    {
      switch (UAPApp.SetProcessDpiAwareness(UAPApp.ProcessDPIAwareness.PerMonitorDPIAware))
      {
        case -2147024891:
          Log.Out("Per monitor DPI awareness is already set.");
          break;
        case -2147024809:
          throw new ArgumentException("The value passed in is not valid.");
        case 0:
          Log.Out("Per monitor DPI awareness for the app was set successfully.");
          break;
        default:
          throw new Exception("SetProcessDpiAwareness() returned unexpected value");
      }
    }

    [DllImport("api-ms-win-shell-namespace-l1-1-0.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
    private static extern void SHCreateItemFromParsingName(
      [MarshalAs(UnmanagedType.LPWStr), In] string path,
      [In] IntPtr pbc,
      [In] ref Guid riid,
      [MarshalAs(UnmanagedType.Interface)] out UAPApp.IShellItem shellItem);

    [DllImport("api-ms-win-storage-exports-external-l1-1-0.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
    private static extern void STORAGE_SHCreateShellItemArrayFromShellItem(
      [In] UAPApp.IShellItem psi,
      [In] ref Guid riid,
      [MarshalAs(UnmanagedType.Interface)] out UAPApp.IShellItemArray ppenum);

    [DllImport("api-ms-win-shcore-scaling-l1-1-1.dll")]
    private static extern int SetProcessDpiAwareness([In] UAPApp.ProcessDPIAwareness value);

    private enum ProcessDPIAwareness
    {
      DPIUnaware,
      SystemDPIAware,
      PerMonitorDPIAware,
    }

    public enum ActivateOptions
    {
      None = 0,
      DesignMode = 1,
      NoErrorUI = 2,
      NoSplashScreen = 4,
    }

    [Guid("2e941141-7f97-4756-ba1d-9decde894a3d")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    private interface IApplicationActivationManager
    {
      IntPtr ActivateApplication(
        [In] string appUserModelId,
        [In] string arguments,
        [In] UAPApp.ActivateOptions options,
        out uint processId);

      IntPtr ActivateForFile(
        [In] string appUserModelId,
        [MarshalAs(UnmanagedType.Interface), In] UAPApp.IShellItemArray itemArray,
        [In] string verb,
        out uint processId);

      IntPtr ActivateForProtocol(
        [In] string appUserModelId,
        [MarshalAs(UnmanagedType.Interface), In] UAPApp.IShellItemArray itemArray,
        out uint processId);
    }

    [Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]
    [ComImport]
    public class ApplicationActivationManager : UAPApp.IApplicationActivationManager
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      public extern IntPtr ActivateApplication(
        [In] string appUserModelId,
        [In] string arguments,
        [In] UAPApp.ActivateOptions options,
        out uint processId);

      [MethodImpl(MethodImplOptions.InternalCall)]
      public extern IntPtr ActivateForFile(
        [In] string appUserModelId,
        [MarshalAs(UnmanagedType.Interface), In] UAPApp.IShellItemArray itemArray,
        [In] string verb,
        out uint processId);

      [MethodImpl(MethodImplOptions.InternalCall)]
      public extern IntPtr ActivateForProtocol(
        [In] string appUserModelId,
        [MarshalAs(UnmanagedType.Interface), In] UAPApp.IShellItemArray itemArray,
        out uint processId);
      
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("b63ea76d-1f85-456f-a19c-48159efa858b")]
    [ComImport]
    public interface IShellItemArray
    {
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
    [ComImport]
    public interface IShellItem
    {
    }
  }
}
