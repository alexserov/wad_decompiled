// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.RIMNativeMethods
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    internal static class RIMNativeMethods {
        [Flags]
        public enum KEY_EVENT_FLAGS : uint {
            NONE = 0,
            EXTENDEDKEY = 1,
            KEYUP = 2,
            UNICODE = 4,
            SCANCODE = 8
        }

        [Flags]
        public enum MOUSE_EVENT_FLAGS : uint {
            NONE = 0,
            MOVE = 1,
            LEFTDOWN = 2,
            LEFTUP = 4,
            RIGHTDOWN = 8,
            RIGHTUP = 16, // 0x00000010
            MIDDLEDOWN = 32, // 0x00000020
            MIDDLEUP = 64, // 0x00000040
            XDOWN = 128, // 0x00000080
            XUP = 256, // 0x00000100
            WHEEL = 2048, // 0x00000800
            HWHEEL = 4096, // 0x00001000
            MOVE_NOCOALESCE = 8192, // 0x00002000
            VIRTUALDESK = 16384, // 0x00004000
            ABSOLUTE = 32768 // 0x00008000
        }

        public const ushort XBUTTON1 = 1;
        public const ushort XBUTTON2 = 2;

        public static void InjectKeyboardInput([In] KeyboardInput keyboardInput) {
            Log.Out(msg: "Inject Keyboard: {0}", (object) keyboardInput.ToString());
            if (!InternalNativeMethods.InjectKeyboardInput(keyboardInput: new KeyboardInput[1] {
                keyboardInput
            }, count: 1U))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        public static void InjectMouseInput([In] MouseInput mouseInput) {
            Log.Out(msg: "Inject Mouse: {0}", (object) mouseInput.ToString());
            if (!InternalNativeMethods.InjectMouseInput(mouseInput: new MouseInput[1] {
                mouseInput
            }, count: 1U))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        public struct KeyboardInput {
            public ushort virtualKeyCode;
            public ushort scanCode;
            public KEY_EVENT_FLAGS flags;
            public uint time;
            public IntPtr extraInfo;

            public override string ToString() {
                return "vk: 0x" + this.virtualKeyCode.ToString(format: "X") + ", sc: 0x" + this.scanCode.ToString(format: "X") + ", flags: " + this.flags;
            }
        }

        public struct MouseInput {
            public int dx;
            public int dy;
            public uint mouseData;
            public MOUSE_EVENT_FLAGS flags;
            public uint time;
            public IntPtr extraInfo;

            public override string ToString() {
                return "(" + this.dx + ", " + this.dy + ") data: 0x" + this.mouseData.ToString(format: "X") + ", flags: " + this.flags;
            }
        }

        static class InternalNativeMethods {
            const string NTUSER_RIM = "ext-ms-win-ntuser-rim-l1-1-0.dll";

            [DllImport(dllName: "ext-ms-win-ntuser-rim-l1-1-0.dll", SetLastError = true)]
            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            public static extern bool InjectKeyboardInput(
                [In] KeyboardInput[] keyboardInput,
                [In] uint count);

            [DllImport(dllName: "ext-ms-win-ntuser-rim-l1-1-0.dll", SetLastError = true)]
            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            public static extern bool InjectMouseInput(
                [In] MouseInput[] mouseInput,
                [In] uint count);
        }
    }
}