// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Mouse
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    internal class Mouse : IPointerInput, IMouseWheelInput {
        const int afterMoveActionWaitDuration = 0;
        const int afterClickActionWaitDuration = 100;
        const int afterWheelActionWaitDuration = 200;
        static readonly object _classLock = new object();
        static Mouse _singletonInstance;
        PointI _location;
        int[] _previousRuntimeId;
        readonly IInputDevice inputDevice;
        internal IInputQueue inputQueue;
        internal INativeMethods nativeMethods;

        protected Mouse() {
            this.nativeMethods = new NativeMethods();
            this.inputQueue = new InputQueue();
            this.inputDevice = new InputDeviceFactory().Get(type: INPUT_DEVICE_TYPE.MOUSE);
        }

        public static Mouse Instance {
            get {
                if (_singletonInstance == null)
                    lock (_classLock) {
                        if (_singletonInstance == null)
                            _singletonInstance = new Mouse();
                    }

                return _singletonInstance;
            }
        }

        public virtual void RotateWheel(int delta) {
            var inputActionList = new List<IInputAction>();
            inputActionList.AddRange(collection: RotateWheel(delta: delta, modifierKeys: ModifierKeys.None));
            inputActionList.Add(item: Input.CreateWait(duration: 200));
            this.inputQueue.Process(inputDevice: null, inputList: inputActionList);
        }

        public virtual void Click(PointerButtons button, int count) {
            if (0 >= count)
                throw new ArgumentOutOfRangeException(paramName: nameof(count), actualValue: count, message: "count should be a positive value");
            var location = Location;
            var inputActionList = new List<IInputAction>();
            inputActionList.AddRange(collection: Input.PreventAccidentalDoubleClick(x: location.X, y: location.Y, previousRuntimeId: ref this._previousRuntimeId));
            for (var index = 0; index < count; ++index) {
                if (index > 0)
                    inputActionList.Add(item: Input.CreateWait(duration: (int) InputManager.DefaultTapDelta));
                inputActionList.AddRange(collection: Down(button: button, modifierKeys: ModifierKeys.None));
                inputActionList.Add(item: Input.CreateWait(duration: (int) InputManager.DefaultPressDuration));
                inputActionList.AddRange(collection: Up(button: button, modifierKeys: ModifierKeys.None));
            }

            inputActionList.Add(item: Input.CreateWait(duration: 100));
            this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputActionList);
        }

        public void ClickDrag(PointI endPoint, PointerButtons button, uint dragDuration) {
            var inputActionList = new List<IInputAction>();
            inputActionList.AddRange(collection: Down(button: button, modifierKeys: ModifierKeys.None));
            inputActionList.Add(item: Input.CreateWait(duration: (int) dragDuration));
            inputActionList.Add(item: Input.CreateMouseMoveInput(absoluteX: endPoint.X, absoluteY: endPoint.Y));
            inputActionList.AddRange(collection: Up(button: button, modifierKeys: ModifierKeys.None));
            inputActionList.Add(item: Input.CreateWait(duration: 0));
            this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputActionList);
            this._location = endPoint;
        }

        public virtual void Move(PointI point) {
            this.inputQueue.Process(inputDevice: this.inputDevice, inputList: new List<IInputAction> {
                Input.CreateMouseMoveInput(absoluteX: point.X, absoluteY: point.Y),
                Input.CreateWait(duration: 0)
            });
            this._location = point;
        }

        public virtual void Press(PointerButtons button) {
            var inputList = Down(button: button, modifierKeys: ModifierKeys.None);
            inputList.Add(item: Input.CreateWait(duration: 100));
            this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputList);
        }

        public void InjectPointers(PointerData[] pointerDataArray) {
            throw new NotImplementedException(message: "SingleTouch move with PointerData argument is not implemented");
        }

        public virtual void Release(PointerButtons button) {
            var inputList = Up(button: button, modifierKeys: ModifierKeys.None);
            inputList.Add(item: Input.CreateWait(duration: 100));
            this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputList);
        }

        public virtual PointI Location {
            get { return this._location; }
        }

        IList<IInputAction> Down(
            PointerButtons button,
            ModifierKeys modifierKeys) {
            var inputActionList = new List<IInputAction>();
            inputActionList.AddRange(collection: Input.CreateKeyModifierInputs(modifierKeys: modifierKeys, press: true, duration: Keyboard.SendKeysDelay));
            inputActionList.Add(item: Input.CreateMouseDownInput(button: button, swapped: GetMouseButtonsSwapped()));
            return inputActionList;
        }

        IList<IInputAction> Up(
            PointerButtons button,
            ModifierKeys modifierKeys) {
            var inputActionList = new List<IInputAction>();
            inputActionList.Add(item: Input.CreateMouseUpInput(button: button, swapped: GetMouseButtonsSwapped()));
            inputActionList.AddRange(collection: Input.CreateKeyModifierInputs(modifierKeys: modifierKeys, press: false, duration: Keyboard.SendKeysDelay));
            return inputActionList;
        }

        bool GetMouseButtonsSwapped() {
            return this.nativeMethods.GetMouseButtonsSwapped();
        }

        IList<IInputAction> RotateWheel(int delta, ModifierKeys modifierKeys) {
            var inputActionList = new List<IInputAction>();
            inputActionList.AddRange(collection: Input.CreateKeyModifierInputs(modifierKeys: modifierKeys, press: true, duration: Keyboard.SendKeysDelay));
            inputActionList.Add(item: Input.CreateMouseRotateWheelInput(delta: delta));
            inputActionList.AddRange(collection: Input.CreateKeyModifierInputs(modifierKeys: modifierKeys, press: false, duration: Keyboard.SendKeysDelay));
            return inputActionList;
        }
    }
}