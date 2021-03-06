﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDevice
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Runtime.InteropServices;
using Windows.UI.Input.Preview.Injection;

namespace MS.Internal.Mita.Foundation {
    internal class InputDevice : IInputDevice, IDisposable {
        [Flags]
        public enum PEN_FLAGS : uint {
            NONE = 0,
            BARREL = 1,
            INVERTED = 2,
            ERASER = 4
        }

        [Flags]
        public enum PEN_MASK : uint {
            NONE = 0,
            PRESSURE = 1,
            ROTATION = 2,
            TILT_X = 4,
            TILT_Y = 8
        }

        public enum POINTER_BUTTON_CHANGE_TYPE {
            NONE,
            FIRSTBUTTON_DOWN,
            FIRSTBUTTON_UP,
            SECONDBUTTON_DOWN,
            SECONDBUTTON_UP,
            THIRDBUTTON_DOWN,
            THIRDBUTTON_UP,
            FOURTHBUTTON_DOWN,
            FOURTHBUTTON_UP,
            FIFTHBUTTON_DOWN,
            FIFTHBUTTON_UP
        }

        [Flags]
        public enum TOUCH_FLAGS : uint {
            NONE = 0
        }

        [Flags]
        public enum TOUCH_MASK : uint {
            NONE = 0,
            CONTACTAREA = 1,
            ORIENTATION = 2,
            PRESSURE = 4
        }

        public const uint MIN_PACKET_FREQUENCY = 10;
        public const uint DEFAULT_TOUCH_COUNT = 10;
        protected const uint MAX_POINTER_COUNT = 256;
        bool disposedValue;
        protected InputInjector injector;

        public virtual void InjectKeyboardInput(RIMNativeMethods.KeyboardInput input) {
            throw new NotImplementedException(message: "InjectKeyboardInput is not implemented in this InputDevice object");
        }

        public virtual void InjectMouseInput(RIMNativeMethods.MouseInput mouseInput) {
            throw new NotImplementedException(message: "InjectMouseInput is not implemented in this InputDevice object");
        }

        public virtual void InjectPointer(PointerData pointerData) {
            throw new NotImplementedException(message: "InjectPointer is not implemented in this InputDevice object");
        }

        public virtual void InjectPointer(PointerData[] pointerData) {
            throw new NotImplementedException(message: "InjectPointer(PointerData[] pointerData) is not implemented in this InputDevice object");
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        protected virtual void RemoveInjectionDevice() {
        }

        protected virtual void Dispose(bool disposing) {
            if (this.disposedValue)
                return;
            RemoveInjectionDevice();
            this.disposedValue = true;
        }

        ~InputDevice() {
            Dispose(disposing: false);
        }

        protected enum TOUCH_FEEDBACK : uint {
            DEFAULT = 1,
            INDIRECT = 2,
            NONE = 3
        }

        public struct Rect {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public struct PointerInfo {
            public POINTER_INPUT_TYPE pointerType;
            public uint pointerId;
            public uint frameId;
            public POINTER_FLAGS pointerFlags;
            public IntPtr sourceDevice;
            public IntPtr target;
            public PointI pixelLocation;
            public PointI himetricLocation;
            public PointI pixelLocationRaw;
            public PointI himetricLocationRaw;
            public uint time;
            public uint historyCount;
            public int inputData;
            public uint keyStates;
            public ulong performanceCount;
            public POINTER_BUTTON_CHANGE_TYPE buttonChangeType;
        }

        public struct PointerTouchInfo {
            public PointerInfo pointerInfo;
            public TOUCH_FLAGS touchFlags;
            public TOUCH_MASK touchMask;
            public Rect contact;
            public Rect contactRaw;
            public uint orientation;
            public uint pressure;
        }

        public struct PointerPenInfo {
            public PointerInfo pointerInfo;
            public PEN_FLAGS penFlags;
            public PEN_MASK penMask;
            public uint pressure;
            public uint rotation;
            public int tiltX;
            public int tiltY;
        }

        [StructLayout(layoutKind: LayoutKind.Explicit)]
        public struct PointerCategoryUnion {
            [FieldOffset(offset: 0)]
            public PointerTouchInfo touchInfo;

            [FieldOffset(offset: 0)]
            public PointerPenInfo penInfo;
        }

        public struct PointerTypeInfo {
            public POINTER_INPUT_TYPE type;
            public PointerCategoryUnion data;
        }
    }
}