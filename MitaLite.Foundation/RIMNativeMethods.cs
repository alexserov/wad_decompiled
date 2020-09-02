// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.RIMNativeMethods
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MS.Internal.Mita.Foundation
{
  internal static class RIMNativeMethods
  {
    public const ushort XBUTTON1 = 1;
    public const ushort XBUTTON2 = 2;

    public static void InjectKeyboardInput([In] RIMNativeMethods.KeyboardInput keyboardInput)
    {
      Log.Out("Inject Keyboard: {0}", (object) keyboardInput.ToString());
      if (!RIMNativeMethods.InternalNativeMethods.InjectKeyboardInput(new RIMNativeMethods.KeyboardInput[1]
      {
        keyboardInput
      }, 1U))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public static void InjectMouseInput([In] RIMNativeMethods.MouseInput mouseInput)
    {
      Log.Out("Inject Mouse: {0}", (object) mouseInput.ToString());
      if (!RIMNativeMethods.InternalNativeMethods.InjectMouseInput(new RIMNativeMethods.MouseInput[1]
      {
        mouseInput
      }, 1U))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    [Flags]
    public enum KEY_EVENT_FLAGS : uint
    {
      NONE = 0,
      EXTENDEDKEY = 1,
      KEYUP = 2,
      UNICODE = 4,
      SCANCODE = 8,
    }

    public struct KeyboardInput
    {
      public ushort virtualKeyCode;
      public ushort scanCode;
      public RIMNativeMethods.KEY_EVENT_FLAGS flags;
      public uint time;
      public IntPtr extraInfo;

      public override string ToString() => "vk: 0x" + this.virtualKeyCode.ToString("X") + ", sc: 0x" + this.scanCode.ToString("X") + ", flags: " + (object) this.flags;
    }

    [Flags]
    public enum MOUSE_EVENT_FLAGS : uint
    {
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
      ABSOLUTE = 32768, // 0x00008000
    }

    public struct MouseInput
    {
      public int dx;
      public int dy;
      public uint mouseData;
      public RIMNativeMethods.MOUSE_EVENT_FLAGS flags;
      public uint time;
      public IntPtr extraInfo;

      public override string ToString() => "(" + (object) this.dx + ", " + (object) this.dy + ") data: 0x" + this.mouseData.ToString("X") + ", flags: " + (object) this.flags;
    }

    private static class InternalNativeMethods
    {
      private const string NTUSER_RIM = "ext-ms-win-ntuser-rim-l1-1-0.dll";

      [DllImport("ext-ms-win-ntuser-rim-l1-1-0.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool InjectKeyboardInput(
        [In] RIMNativeMethods.KeyboardInput[] keyboardInput,
        [In] uint count);

      [DllImport("ext-ms-win-ntuser-rim-l1-1-0.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool InjectMouseInput(
        [In] RIMNativeMethods.MouseInput[] mouseInput,
        [In] uint count);
    }
  }
}
