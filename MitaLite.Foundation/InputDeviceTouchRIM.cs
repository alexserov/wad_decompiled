// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDeviceTouchRIM
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MS.Internal.Mita.Foundation
{
  internal class InputDeviceTouchRIM : InputDevice
  {
    private IntPtr device = IntPtr.Zero;

    public InputDeviceTouchRIM()
    {
      if (!InputDeviceTouchRIM.InternalNativeMethods.InitializePointerDeviceInjection(POINTER_INPUT_TYPE.TOUCH, 256U, IntPtr.Zero, InputDevice.TOUCH_FEEDBACK.INDIRECT, out this.device))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public override void InjectPointer(PointerData pointerData)
    {
      Log.Out("Inject Pointer: {0}", (object) pointerData.ToString());
      if (!InputDeviceTouchRIM.InternalNativeMethods.InjectPointerInput(this.device, new InputDevice.PointerTypeInfo[1]
      {
        this.TransformPointer(pointerData, POINTER_INPUT_TYPE.TOUCH)
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
        pointerTypeInfo[index] = this.TransformPointer(pointerData[index], POINTER_INPUT_TYPE.TOUCH);
      if (!InputDeviceTouchRIM.InternalNativeMethods.InjectPointerInput(this.device, pointerTypeInfo, (uint) pointerTypeInfo.Length))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    protected override void RemoveInjectionDevice()
    {
      if (!(this.device != IntPtr.Zero))
        return;
      this.device = InputDeviceTouchRIM.InternalNativeMethods.RemoveInjectionDevice(this.device) ? IntPtr.Zero : throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    protected InputDevice.PointerTypeInfo TransformPointer(
      PointerData pointerData,
      POINTER_INPUT_TYPE inputType)
    {
      InputDevice.PointerTypeInfo pointerTypeInfo = new InputDevice.PointerTypeInfo();
      pointerTypeInfo.type = inputType;
      InputDevice.PointerInfo pointerInfo = new InputDevice.PointerInfo();
      pointerInfo.pointerType = inputType;
      pointerInfo.pointerId = pointerData.pointerId;
      pointerInfo.pointerFlags = pointerData.flags;
      pointerInfo.pixelLocation = pointerData.location;
      switch (inputType)
      {
        case POINTER_INPUT_TYPE.TOUCH:
          pointerTypeInfo.data.touchInfo.pointerInfo = pointerInfo;
          pointerTypeInfo.data.touchInfo.touchMask = InputDevice.TOUCH_MASK.NONE;
          pointerTypeInfo.data.touchInfo.touchFlags = (InputDevice.TOUCH_FLAGS) pointerData.pressedButton;
          if (pointerData.width.HasValue && pointerData.height.HasValue)
          {
            pointerTypeInfo.data.touchInfo.contact = new InputDevice.Rect()
            {
              left = pointerTypeInfo.data.touchInfo.pointerInfo.pixelLocation.X - pointerData.width.Value / 2,
              right = pointerTypeInfo.data.touchInfo.pointerInfo.pixelLocation.X + pointerData.width.Value / 2,
              top = pointerTypeInfo.data.touchInfo.pointerInfo.pixelLocation.Y - pointerData.height.Value / 2,
              bottom = pointerTypeInfo.data.touchInfo.pointerInfo.pixelLocation.Y + pointerData.height.Value / 2
            };
            pointerTypeInfo.data.touchInfo.touchMask |= InputDevice.TOUCH_MASK.CONTACTAREA;
          }
          if (pointerData.pressure.HasValue)
          {
            pointerTypeInfo.data.touchInfo.pressure = pointerData.pressure.Value;
            pointerTypeInfo.data.touchInfo.touchMask |= InputDevice.TOUCH_MASK.PRESSURE;
          }
          if (pointerData.twist.HasValue)
          {
            pointerTypeInfo.data.touchInfo.orientation = pointerData.twist.Value;
            pointerTypeInfo.data.touchInfo.touchMask |= InputDevice.TOUCH_MASK.ORIENTATION;
            break;
          }
          break;
        case POINTER_INPUT_TYPE.PEN:
          pointerTypeInfo.data.penInfo.pointerInfo = pointerInfo;
          pointerTypeInfo.data.penInfo.penMask = InputDevice.PEN_MASK.NONE;
          pointerTypeInfo.data.penInfo.penFlags = (InputDevice.PEN_FLAGS) pointerData.pressedButton;
          if (pointerData.tiltX.HasValue)
          {
            pointerTypeInfo.data.penInfo.tiltX = pointerData.tiltX.Value;
            pointerTypeInfo.data.penInfo.penMask |= InputDevice.PEN_MASK.TILT_X;
          }
          if (pointerData.tiltY.HasValue)
          {
            pointerTypeInfo.data.penInfo.tiltY = pointerData.tiltY.Value;
            pointerTypeInfo.data.penInfo.penMask |= InputDevice.PEN_MASK.TILT_Y;
          }
          if (pointerData.pressure.HasValue)
          {
            pointerTypeInfo.data.penInfo.pressure = pointerData.pressure.Value;
            pointerTypeInfo.data.penInfo.penMask |= InputDevice.PEN_MASK.PRESSURE;
          }
          if (pointerData.twist.HasValue)
          {
            pointerTypeInfo.data.penInfo.rotation = pointerData.twist.Value;
            pointerTypeInfo.data.penInfo.penMask |= InputDevice.PEN_MASK.ROTATION;
            break;
          }
          break;
        default:
          pointerTypeInfo.data.touchInfo.pointerInfo = pointerInfo;
          pointerTypeInfo.data.touchInfo.touchMask = InputDevice.TOUCH_MASK.NONE;
          break;
      }
      return pointerTypeInfo;
    }

    protected static class InternalNativeMethods
    {
      private const string NTUSER_RIM = "ext-ms-win-ntuser-rim-l1-1-0.dll";

      [DllImport("ext-ms-win-ntuser-rim-l1-1-0.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool InitializePointerDeviceInjection(
        [In] POINTER_INPUT_TYPE type,
        [In] uint contactCount,
        [In] IntPtr monitor,
        [In] InputDevice.TOUCH_FEEDBACK visualMode,
        out IntPtr device);

      [DllImport("ext-ms-win-ntuser-rim-l1-1-0.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool RemoveInjectionDevice([In] IntPtr device);

      [DllImport("ext-ms-win-ntuser-rim-l1-1-0.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool InjectPointerInput(
        [In] IntPtr device,
        [In] InputDevice.PointerTypeInfo[] pointerTypeInfo,
        [In] uint count);
    }
  }
}
