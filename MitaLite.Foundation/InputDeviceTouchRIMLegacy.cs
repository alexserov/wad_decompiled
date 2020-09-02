// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDeviceTouchRIMLegacy
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MS.Internal.Mita.Foundation
{
  internal class InputDeviceTouchRIMLegacy : InputDeviceTouchRIM
  {
    public InputDeviceTouchRIMLegacy()
    {
      if (!InputDeviceTouchRIMLegacy.InternalNativeMethodsLegacy.InitializeTouchInjection(256U, InputDevice.TOUCH_FEEDBACK.INDIRECT))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public override void InjectPointer(PointerData pointerData)
    {
      Log.Out("Inject Pointer: {0}", (object) pointerData.ToString());
      InputDevice.PointerTouchInfo[] pointerTouchInfo = new InputDevice.PointerTouchInfo[1]
      {
        new InputDevice.PointerTypeInfo[1]
        {
          this.TransformPointer(pointerData, POINTER_INPUT_TYPE.TOUCH)
        }[0].data.touchInfo
      };
      this.PrunePointerFlags(ref pointerTouchInfo[0].pointerInfo.pointerFlags);
      if (!InputDeviceTouchRIMLegacy.InternalNativeMethodsLegacy.InjectTouchInput((uint) pointerTouchInfo.Length, pointerTouchInfo))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public override void InjectPointer(PointerData[] pointerData)
    {
      if (pointerData.Length > 256)
        throw new ArgumentOutOfRangeException(string.Format("The maximum number of simultaneous touch points is {0}.", (object) 256U));
      if (Log.OutImplementation != null)
      {
        foreach (PointerData pointerData1 in pointerData)
          Log.Out("Inject Pointer: {0}", (object) pointerData1.ToString());
      }
      InputDevice.PointerTouchInfo[] pointerTouchInfo = new InputDevice.PointerTouchInfo[pointerData.Length];
      for (int index = 0; index < pointerData.Length; ++index)
      {
        pointerTouchInfo[index] = this.TransformPointer(pointerData[index], POINTER_INPUT_TYPE.TOUCH).data.touchInfo;
        this.PrunePointerFlags(ref pointerTouchInfo[index].pointerInfo.pointerFlags);
      }
      if (!InputDeviceTouchRIMLegacy.InternalNativeMethodsLegacy.InjectTouchInput((uint) pointerTouchInfo.Length, pointerTouchInfo))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    protected override void RemoveInjectionDevice()
    {
    }

    private void PrunePointerFlags(ref POINTER_FLAGS pointerFlags) => pointerFlags &= ~(POINTER_FLAGS.FIRSTBUTTON | POINTER_FLAGS.SECONDBUTTON | POINTER_FLAGS.THIRDBUTTON | POINTER_FLAGS.FOURTHBUTTON | POINTER_FLAGS.FIFTHBUTTON | POINTER_FLAGS.PRIMARY | POINTER_FLAGS.CONFIDENCE | POINTER_FLAGS.WHEEL);

    private static class InternalNativeMethodsLegacy
    {
      private const string NTUSER_POINTER = "api-ms-win-rtcore-ntuser-wmpointer-l1-1-0.dll";

      [DllImport("api-ms-win-rtcore-ntuser-wmpointer-l1-1-0.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool InitializeTouchInjection(
        [In] uint contactCount,
        [In] InputDevice.TOUCH_FEEDBACK visualMode);

      [DllImport("api-ms-win-rtcore-ntuser-wmpointer-l1-1-0.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool InjectTouchInput(
        [In] uint count,
        [In] InputDevice.PointerTouchInfo[] pointerTouchInfo);
    }
  }
}
