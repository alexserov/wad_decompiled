// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.SingleTouch
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation {
    internal class SingleTouch : ISinglePointGestureInput, IPointerInput, IDisposable {
        static readonly object _classLock = new object();
        static SingleTouch _singletonInstance;
        readonly uint DefaultContactId;
        bool _disposed;
        IInputManager _inputManager = new InputManager(inputType: INPUT_DEVICE_TYPE.TOUCH, inputFactory: new InputDeviceFactory(), timeFactory: new TimeManagerFactory(), inputAlgorithms: new InputAlgorithms());

        protected SingleTouch() {
        }

        public static SingleTouch Instance {
            get {
                if (_singletonInstance == null)
                    lock (_classLock) {
                        if (_singletonInstance == null)
                            _singletonInstance = new SingleTouch();
                    }

                return _singletonInstance;
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        public void Flick(PointI endPoint, uint holdDuration, float acceleration) {
            this._inputManager.InjectPressAndDragWithAcceleration(start: Location, end: Input.AdjustPointerMoveInput(originalPoint: endPoint), holdDuration: holdDuration, acceleration: acceleration, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void Pan(PointI endPoint, uint holdDuration, float acceleration) {
            this._inputManager.InjectPressAndDragWithAcceleration(start: Location, end: Input.AdjustPointerMoveInput(originalPoint: endPoint), holdDuration: holdDuration, acceleration: acceleration, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void PressAndDrag(PointI endPoint, uint dragDuration) {
            this._inputManager.InjectPressAndDrag(start: Location, end: Input.AdjustPointerMoveInput(originalPoint: endPoint), dragDuration: dragDuration, holdDuration: InputManager.DefaultPressDuration, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void PressAndDrag(PointI endPoint, uint dragDuration, uint pressDuration) {
            this._inputManager.InjectPressAndDrag(start: Location, end: Input.AdjustPointerMoveInput(originalPoint: endPoint), dragDuration: dragDuration, holdDuration: pressDuration, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void PressAndHold(uint holdDuration) {
            this._inputManager.InjectPress(point: Location, holdDuration: holdDuration, tapCount: 1U, tapDelta: InputManager.DefaultTapDelta, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void Click(PointerButtons button, int count) {
            this._inputManager.InjectPress(point: Location, holdDuration: InputManager.DefaultPressDuration, tapCount: (uint) count, tapDelta: InputManager.DefaultTapDelta, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void ClickDrag(PointI endPoint, PointerButtons button, uint dragDuration) {
            PressAndDrag(endPoint: endPoint, dragDuration: dragDuration);
        }

        public void Move(PointI point) {
            var location = Location;
            Location = Input.AdjustPointerMoveInput(originalPoint: point);
            this._inputManager.InjectDynamicMove(start: location, end: Location, maxDragDuration: SinglePointGesture.DefaultDragDuration, contactId: this.DefaultContactId, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void Press(PointerButtons button) {
            this._inputManager.InjectDynamicPress(touchPoint: Location, contactId: this.DefaultContactId);
        }

        public void Release(PointerButtons button) {
            this._inputManager.InjectDynamicRelease(touchPoint: Location, contactId: this.DefaultContactId);
        }

        public void InjectPointers(PointerData[] pointerDataArray) {
            throw new NotImplementedException(message: "SingleTouch move with PointerData argument is not implemented");
        }

        public PointI Location { get; set; }

        ~SingleTouch() {
            Dispose(disposing: false);
        }

        void Dispose(bool disposing) {
            try {
                if (this._disposed || !disposing)
                    return;
                this._inputManager.Dispose();
                this._inputManager = null;
            } finally {
                this._disposed = true;
            }
        }
    }
}