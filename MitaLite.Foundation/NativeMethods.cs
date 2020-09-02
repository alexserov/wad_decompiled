// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.NativeMethods
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MS.Internal.Mita.Foundation
{
  internal class NativeMethods : INativeMethods
  {
    private const int SM_SWAPBUTTON = 23;
    private const int SM_XVIRTUALSCREEN = 76;
    private const int SM_YVIRTUALSCREEN = 77;
    private const int SM_CXVIRTUALSCREEN = 78;
    private const int SM_CYVIRTUALSCREEN = 79;

    public void GetKeyboardState([Out] byte[] keyState)
    {
      if (!NativeMethods.InternalNativeMethods.GetKeyboardState(keyState))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public bool GetMouseButtonsSwapped() => NativeMethods.InternalNativeMethods.GetSystemMetrics(23) != 0;

    public RectangleI GetClientArea()
    {
      int systemMetrics1 = NativeMethods.InternalNativeMethods.GetSystemMetrics(76);
      int systemMetrics2 = NativeMethods.InternalNativeMethods.GetSystemMetrics(77);
      int systemMetrics3 = NativeMethods.InternalNativeMethods.GetSystemMetrics(78);
      int systemMetrics4 = NativeMethods.InternalNativeMethods.GetSystemMetrics(79);
      if (systemMetrics3 == 0 || systemMetrics4 == 0)
        throw new Exception("GetSystemMetrics failed");
      Log.Out("Client Area ({0}, {1}, {2}, {3})", (object) systemMetrics1, (object) systemMetrics2, (object) systemMetrics3, (object) systemMetrics4);
      return new RectangleI(systemMetrics1, systemMetrics2, systemMetrics3, systemMetrics4);
    }

    private static class InternalNativeMethods
    {
      private const string SYSPARAMS_API_DLL = "api-ms-win-ntuser-sysparams-l1-1-0.dll";
      private const string RAWINPUT_DLL = "ext-ms-win-rtcore-ntuser-rawinput-l1-1-0.dll";

      [DllImport("ext-ms-win-rtcore-ntuser-rawinput-l1-1-0.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool GetKeyboardState([Out] byte[] keyState);

      [DllImport("api-ms-win-ntuser-sysparams-l1-1-0.dll")]
      public static extern int GetSystemMetrics([In] int nIndex);
    }
  }
}
