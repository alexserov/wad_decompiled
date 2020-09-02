// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDeviceTouchRIM
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    internal class InputDeviceTouchRIM : InputDevice {
        IntPtr device = IntPtr.Zero;

        public InputDeviceTouchRIM() {
            if (!InternalNativeMethods.InitializePointerDeviceInjection(type: POINTER_INPUT_TYPE.TOUCH, contactCount: 256U, monitor: IntPtr.Zero, visualMode: TOUCH_FEEDBACK.INDIRECT, device: out this.device))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        public override void InjectPointer(PointerData pointerData) {
            Log.Out(msg: "Inject Pointer: {0}", (object) pointerData.ToString());
            if (!InternalNativeMethods.InjectPointerInput(device: this.device, pointerTypeInfo: new PointerTypeInfo[1] {
                TransformPointer(pointerData: pointerData, inputType: POINTER_INPUT_TYPE.TOUCH)
            }, count: 1U))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        public override void InjectPointer(PointerData[] pointerData) {
            if (pointerData.Length > 256)
                throw new ArgumentOutOfRangeException(paramName: string.Format(format: "The maximum number of simultaneous touch points is {0}.", arg0: 256U));
            if (Log.OutImplementation != null)
                foreach (var pointerData1 in pointerData)
                    Log.Out(msg: "Inject Pointer: {0}", (object) pointerData1.ToString());
            var pointerTypeInfo = new PointerTypeInfo[pointerData.Length];
            for (var index = 0; index < pointerData.Length; ++index)
                pointerTypeInfo[index] = TransformPointer(pointerData: pointerData[index], inputType: POINTER_INPUT_TYPE.TOUCH);
            if (!InternalNativeMethods.InjectPointerInput(device: this.device, pointerTypeInfo: pointerTypeInfo, count: (uint) pointerTypeInfo.Length))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        protected override void RemoveInjectionDevice() {
            if (!(this.device != IntPtr.Zero))
                return;
            this.device = InternalNativeMethods.RemoveInjectionDevice(device: this.device) ? IntPtr.Zero : throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        protected PointerTypeInfo TransformPointer(
            PointerData pointerData,
            POINTER_INPUT_TYPE inputType) {
            var pointerTypeInfo = new PointerTypeInfo();
            pointerTypeInfo.type = inputType;
            var pointerInfo = new PointerInfo();
            pointerInfo.pointerType = inputType;
            pointerInfo.pointerId = pointerData.pointerId;
            pointerInfo.pointerFlags = pointerData.flags;
            pointerInfo.pixelLocation = pointerData.location;
            switch (inputType) {
                case POINTER_INPUT_TYPE.TOUCH:
                    pointerTypeInfo.data.touchInfo.pointerInfo = pointerInfo;
                    pointerTypeInfo.data.touchInfo.touchMask = TOUCH_MASK.NONE;
                    pointerTypeInfo.data.touchInfo.touchFlags = (TOUCH_FLAGS) pointerData.pressedButton;
                    if (pointerData.width.HasValue && pointerData.height.HasValue) {
                        pointerTypeInfo.data.touchInfo.contact = new Rect {
                            left = pointerTypeInfo.data.touchInfo.pointerInfo.pixelLocation.X - pointerData.width.Value / 2,
                            right = pointerTypeInfo.data.touchInfo.pointerInfo.pixelLocation.X + pointerData.width.Value / 2,
                            top = pointerTypeInfo.data.touchInfo.pointerInfo.pixelLocation.Y - pointerData.height.Value / 2,
                            bottom = pointerTypeInfo.data.touchInfo.pointerInfo.pixelLocation.Y + pointerData.height.Value / 2
                        };
                        pointerTypeInfo.data.touchInfo.touchMask |= TOUCH_MASK.CONTACTAREA;
                    }

                    if (pointerData.pressure.HasValue) {
                        pointerTypeInfo.data.touchInfo.pressure = pointerData.pressure.Value;
                        pointerTypeInfo.data.touchInfo.touchMask |= TOUCH_MASK.PRESSURE;
                    }

                    if (pointerData.twist.HasValue) {
                        pointerTypeInfo.data.touchInfo.orientation = pointerData.twist.Value;
                        pointerTypeInfo.data.touchInfo.touchMask |= TOUCH_MASK.ORIENTATION;
                    }

                    break;
                case POINTER_INPUT_TYPE.PEN:
                    pointerTypeInfo.data.penInfo.pointerInfo = pointerInfo;
                    pointerTypeInfo.data.penInfo.penMask = PEN_MASK.NONE;
                    pointerTypeInfo.data.penInfo.penFlags = (PEN_FLAGS) pointerData.pressedButton;
                    if (pointerData.tiltX.HasValue) {
                        pointerTypeInfo.data.penInfo.tiltX = pointerData.tiltX.Value;
                        pointerTypeInfo.data.penInfo.penMask |= PEN_MASK.TILT_X;
                    }

                    if (pointerData.tiltY.HasValue) {
                        pointerTypeInfo.data.penInfo.tiltY = pointerData.tiltY.Value;
                        pointerTypeInfo.data.penInfo.penMask |= PEN_MASK.TILT_Y;
                    }

                    if (pointerData.pressure.HasValue) {
                        pointerTypeInfo.data.penInfo.pressure = pointerData.pressure.Value;
                        pointerTypeInfo.data.penInfo.penMask |= PEN_MASK.PRESSURE;
                    }

                    if (pointerData.twist.HasValue) {
                        pointerTypeInfo.data.penInfo.rotation = pointerData.twist.Value;
                        pointerTypeInfo.data.penInfo.penMask |= PEN_MASK.ROTATION;
                    }

                    break;
                default:
                    pointerTypeInfo.data.touchInfo.pointerInfo = pointerInfo;
                    pointerTypeInfo.data.touchInfo.touchMask = TOUCH_MASK.NONE;
                    break;
            }

            return pointerTypeInfo;
        }

        protected static class InternalNativeMethods {
            const string NTUSER_RIM = "ext-ms-win-ntuser-rim-l1-1-0.dll";

            [DllImport(dllName: "ext-ms-win-ntuser-rim-l1-1-0.dll", SetLastError = true)]
            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            public static extern bool InitializePointerDeviceInjection(
                [In] POINTER_INPUT_TYPE type,
                [In] uint contactCount,
                [In] IntPtr monitor,
                [In] TOUCH_FEEDBACK visualMode,
                out IntPtr device);

            [DllImport(dllName: "ext-ms-win-ntuser-rim-l1-1-0.dll", SetLastError = true)]
            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            public static extern bool RemoveInjectionDevice([In] IntPtr device);

            [DllImport(dllName: "ext-ms-win-ntuser-rim-l1-1-0.dll", SetLastError = true)]
            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            public static extern bool InjectPointerInput(
                [In] IntPtr device,
                [In] PointerTypeInfo[] pointerTypeInfo,
                [In] uint count);
        }
    }
}