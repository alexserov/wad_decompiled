// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDeviceFactory
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation
{
  internal class InputDeviceFactory : IInputDeviceFactory
  {
    public IInputDevice Get(INPUT_DEVICE_TYPE type)
    {
      if (AppModelStatus.IsInAppContainer)
      {
        switch (type)
        {
          case INPUT_DEVICE_TYPE.TOUCH:
            return (IInputDevice) new InputDeviceTouch();
          case INPUT_DEVICE_TYPE.PEN:
            return (IInputDevice) new InputDevicePen();
          case INPUT_DEVICE_TYPE.MOUSE:
            return (IInputDevice) new InputDeviceMouse();
          case INPUT_DEVICE_TYPE.KEYBOARD:
            return (IInputDevice) new InputDeviceKeyboard();
          default:
            throw new ArgumentException(string.Format("Invalid input device type {0}", (object) type));
        }
      }
      else
      {
        switch (type)
        {
          case INPUT_DEVICE_TYPE.TOUCH:
            return (IInputDevice) new InputDeviceTouchRIMLegacy();
          case INPUT_DEVICE_TYPE.PEN:
            return (IInputDevice) new InputDevicePenRIM();
          case INPUT_DEVICE_TYPE.MOUSE:
            return (IInputDevice) new InputDeviceMouseRIM();
          case INPUT_DEVICE_TYPE.KEYBOARD:
            return (IInputDevice) new InputDeviceKeyboardRIM();
          default:
            throw new ArgumentException(string.Format("Invalid input device type {0}", (object) type));
        }
      }
    }
  }
}
