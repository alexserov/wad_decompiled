// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.NativeMethods
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    internal class NativeMethods : INativeMethods {
        const int SM_SWAPBUTTON = 23;
        const int SM_XVIRTUALSCREEN = 76;
        const int SM_YVIRTUALSCREEN = 77;
        const int SM_CXVIRTUALSCREEN = 78;
        const int SM_CYVIRTUALSCREEN = 79;

        public void GetKeyboardState([Out] byte[] keyState) {
            if (!InternalNativeMethods.GetKeyboardState(keyState: keyState))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        public bool GetMouseButtonsSwapped() {
            return InternalNativeMethods.GetSystemMetrics(nIndex: 23) != 0;
        }

        public RectangleI GetClientArea() {
            var systemMetrics1 = InternalNativeMethods.GetSystemMetrics(nIndex: 76);
            var systemMetrics2 = InternalNativeMethods.GetSystemMetrics(nIndex: 77);
            var systemMetrics3 = InternalNativeMethods.GetSystemMetrics(nIndex: 78);
            var systemMetrics4 = InternalNativeMethods.GetSystemMetrics(nIndex: 79);
            if (systemMetrics3 == 0 || systemMetrics4 == 0)
                throw new Exception(message: "GetSystemMetrics failed");
            Log.Out(msg: "Client Area ({0}, {1}, {2}, {3})", (object) systemMetrics1, (object) systemMetrics2, (object) systemMetrics3, (object) systemMetrics4);
            return new RectangleI(x: systemMetrics1, y: systemMetrics2, width: systemMetrics3, height: systemMetrics4);
        }

        static class InternalNativeMethods {
            const string SYSPARAMS_API_DLL = "api-ms-win-ntuser-sysparams-l1-1-0.dll";
            const string RAWINPUT_DLL = "ext-ms-win-rtcore-ntuser-rawinput-l1-1-0.dll";

            [DllImport(dllName: "ext-ms-win-rtcore-ntuser-rawinput-l1-1-0.dll", SetLastError = true)]
            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            public static extern bool GetKeyboardState([Out] byte[] keyState);

            [DllImport(dllName: "api-ms-win-ntuser-sysparams-l1-1-0.dll")]
            public static extern int GetSystemMetrics([In] int nIndex);
        }
    }
}