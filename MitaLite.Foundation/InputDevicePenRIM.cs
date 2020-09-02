// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDevicePenRIM
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    internal class InputDevicePenRIM : InputDeviceTouchRIM {
        const uint DEFAULT_PEN_COUNT = 1;
        readonly IntPtr device = IntPtr.Zero;

        public InputDevicePenRIM() {
            if (!InternalNativeMethods.InitializePointerDeviceInjection(type: POINTER_INPUT_TYPE.PEN, contactCount: 1U, monitor: IntPtr.Zero, visualMode: TOUCH_FEEDBACK.INDIRECT, device: out this.device))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        public override void InjectPointer(PointerData pointerData) {
            Log.Out(msg: "Inject Pointer: PenRIM {0}", (object) pointerData.ToString());
            if (!InternalNativeMethods.InjectPointerInput(device: this.device, pointerTypeInfo: new PointerTypeInfo[1] {
                TransformPointer(pointerData: pointerData, inputType: POINTER_INPUT_TYPE.PEN)
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
                pointerTypeInfo[index] = TransformPointer(pointerData: pointerData[index], inputType: POINTER_INPUT_TYPE.PEN);
            if (!InternalNativeMethods.InjectPointerInput(device: this.device, pointerTypeInfo: pointerTypeInfo, count: (uint) pointerTypeInfo.Length))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }
    }
}