// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDeviceFactory
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation {
    internal class InputDeviceFactory : IInputDeviceFactory {
        public IInputDevice Get(INPUT_DEVICE_TYPE type) {
            if (AppModelStatus.IsInAppContainer) {
                switch (type) {
                    case INPUT_DEVICE_TYPE.TOUCH:
                        return new InputDeviceTouch();
                    case INPUT_DEVICE_TYPE.PEN:
                        return new InputDevicePen();
                    case INPUT_DEVICE_TYPE.MOUSE:
                        return new InputDeviceMouse();
                    case INPUT_DEVICE_TYPE.KEYBOARD:
                        return new InputDeviceKeyboard();
                    default:
                        throw new ArgumentException(message: string.Format(format: "Invalid input device type {0}", arg0: type));
                }
            }

            switch (type) {
                case INPUT_DEVICE_TYPE.TOUCH:
                    return new InputDeviceTouchRIMLegacy();
                case INPUT_DEVICE_TYPE.PEN:
                    return new InputDevicePenRIM();
                case INPUT_DEVICE_TYPE.MOUSE:
                    return new InputDeviceMouseRIM();
                case INPUT_DEVICE_TYPE.KEYBOARD:
                    return new InputDeviceKeyboardRIM();
                default:
                    throw new ArgumentException(message: string.Format(format: "Invalid input device type {0}", arg0: type));
            }
        }
    }
}