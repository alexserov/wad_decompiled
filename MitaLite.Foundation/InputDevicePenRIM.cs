// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDevicePenRIM
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MS.Internal.Mita.Foundation
{
  internal class InputDevicePenRIM : InputDeviceTouchRIM
  {
    private const uint DEFAULT_PEN_COUNT = 1;
    private IntPtr device = IntPtr.Zero;

    public InputDevicePenRIM()
    {
      if (!InputDeviceTouchRIM.InternalNativeMethods.InitializePointerDeviceInjection(POINTER_INPUT_TYPE.PEN, 1U, IntPtr.Zero, InputDevice.TOUCH_FEEDBACK.INDIRECT, out this.device))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public override void InjectPointer(PointerData pointerData)
    {
      Log.Out("Inject Pointer: PenRIM {0}", (object) pointerData.ToString());
      if (!InputDeviceTouchRIM.InternalNativeMethods.InjectPointerInput(this.device, new InputDevice.PointerTypeInfo[1]
      {
        this.TransformPointer(pointerData, POINTER_INPUT_TYPE.PEN)
      }, 1U))
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
      InputDevice.PointerTypeInfo[] pointerTypeInfo = new InputDevice.PointerTypeInfo[pointerData.Length];
      for (int index = 0; index < pointerData.Length; ++index)
        pointerTypeInfo[index] = this.TransformPointer(pointerData[index], POINTER_INPUT_TYPE.PEN);
      if (!InputDeviceTouchRIM.InternalNativeMethods.InjectPointerInput(this.device, pointerTypeInfo, (uint) pointerTypeInfo.Length))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }
  }
}
