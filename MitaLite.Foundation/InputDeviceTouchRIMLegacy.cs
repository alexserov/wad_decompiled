// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDeviceTouchRIMLegacy
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    internal class InputDeviceTouchRIMLegacy : InputDeviceTouchRIM {
        public InputDeviceTouchRIMLegacy() {
            if (!InternalNativeMethodsLegacy.InitializeTouchInjection(contactCount: 256U, visualMode: TOUCH_FEEDBACK.INDIRECT))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        public override void InjectPointer(PointerData pointerData) {
            Log.Out(msg: "Inject Pointer: {0}", (object) pointerData.ToString());
            var pointerTouchInfo = new PointerTouchInfo[1] {
                new PointerTypeInfo[1] {
                    TransformPointer(pointerData: pointerData, inputType: POINTER_INPUT_TYPE.TOUCH)
                }[0].data.touchInfo
            };
            PrunePointerFlags(pointerFlags: ref pointerTouchInfo[0].pointerInfo.pointerFlags);
            if (!InternalNativeMethodsLegacy.InjectTouchInput(count: (uint) pointerTouchInfo.Length, pointerTouchInfo: pointerTouchInfo))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        public override void InjectPointer(PointerData[] pointerData) {
            if (pointerData.Length > 256)
                throw new ArgumentOutOfRangeException(paramName: string.Format(format: "The maximum number of simultaneous touch points is {0}.", arg0: 256U));
            if (Log.OutImplementation != null)
                foreach (var pointerData1 in pointerData)
                    Log.Out(msg: "Inject Pointer: {0}", (object) pointerData1.ToString());
            var pointerTouchInfo = new PointerTouchInfo[pointerData.Length];
            for (var index = 0; index < pointerData.Length; ++index) {
                pointerTouchInfo[index] = TransformPointer(pointerData: pointerData[index], inputType: POINTER_INPUT_TYPE.TOUCH).data.touchInfo;
                PrunePointerFlags(pointerFlags: ref pointerTouchInfo[index].pointerInfo.pointerFlags);
            }

            if (!InternalNativeMethodsLegacy.InjectTouchInput(count: (uint) pointerTouchInfo.Length, pointerTouchInfo: pointerTouchInfo))
                throw new Win32Exception(error: Marshal.GetLastWin32Error());
        }

        protected override void RemoveInjectionDevice() {
        }

        void PrunePointerFlags(ref POINTER_FLAGS pointerFlags) {
            pointerFlags &= ~(POINTER_FLAGS.FIRSTBUTTON | POINTER_FLAGS.SECONDBUTTON | POINTER_FLAGS.THIRDBUTTON | POINTER_FLAGS.FOURTHBUTTON | POINTER_FLAGS.FIFTHBUTTON | POINTER_FLAGS.PRIMARY | POINTER_FLAGS.CONFIDENCE | POINTER_FLAGS.WHEEL);
        }

        static class InternalNativeMethodsLegacy {
            const string NTUSER_POINTER = "api-ms-win-rtcore-ntuser-wmpointer-l1-1-0.dll";

            [DllImport(dllName: "api-ms-win-rtcore-ntuser-wmpointer-l1-1-0.dll", SetLastError = true)]
            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            public static extern bool InitializeTouchInjection(
                [In] uint contactCount,
                [In] TOUCH_FEEDBACK visualMode);

            [DllImport(dllName: "api-ms-win-rtcore-ntuser-wmpointer-l1-1-0.dll", SetLastError = true)]
            [return: MarshalAs(unmanagedType: UnmanagedType.Bool)]
            public static extern bool InjectTouchInput(
                [In] uint count,
                [In] PointerTouchInfo[] pointerTouchInfo);
        }
    }
}