// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.MultiTouch
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation {
    internal class MultiTouch : IMultiPointGestureInput, ISinglePointGestureInput, IPointerInput, IDisposable {
        static readonly object _classLock = new object();
        static MultiTouch _singletonInstance;
        static readonly uint MaxNumberOfContacts = 10;
        readonly uint DefaultContactId;
        readonly uint DefaultFirstFingerUpTapDelta = 100;
        bool _disposed;
        IInputManager _inputManager = new InputManager(inputType: INPUT_DEVICE_TYPE.TOUCH, inputFactory: new InputDeviceFactory(), timeFactory: new TimeManagerFactory(), inputAlgorithms: new InputAlgorithms());
        readonly PointI[] _location = new PointI[(int) MaxNumberOfContacts];

        protected MultiTouch() {
        }

        public static MultiTouch Instance {
            get {
                if (_singletonInstance == null)
                    lock (_classLock) {
                        if (_singletonInstance == null)
                            _singletonInstance = new MultiTouch();
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
            Move(point: point, contactId: this.DefaultContactId);
        }

        public void Press(PointerButtons button) {
            this._inputManager.InjectDynamicPress(touchPoint: Location, contactId: this.DefaultContactId);
        }

        public void Release(PointerButtons button) {
            this._inputManager.InjectDynamicRelease(touchPoint: Location, contactId: this.DefaultContactId);
        }

        public void InjectPointers(PointerData[] pointerDataArray) {
            this._inputManager.InjectDynamicPointers(pointerDataArray: pointerDataArray);
        }

        public PointI Location {
            get { return GetLocationOfContact(contactId: this.DefaultContactId); }
        }

        public void PressAndTap(uint tapCount, uint tapDuration, uint tapDelta, uint distance) {
            this._inputManager.InjectMTPressAndTap(startFingerOne: Location, startFingerTwo: CreateSecondFingerPointFromDistance(distance: distance, direction: 180f), endFingerOne: Location, deltaFingerTwoDown: tapDelta, deltaFingerTwoUp: tapDuration, deltaFingerOneUp: this.DefaultFirstFingerUpTapDelta, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void TwoPointPressAndHold(
            uint tapCount,
            uint holdDuration,
            uint tapDelta,
            uint distance) {
            this._inputManager.InjectMTTwoFingerPress(pointOne: Location, pointTwo: CreateSecondFingerPointFromDistance(distance: distance, direction: 180f), holdDuration: holdDuration, tapCount: tapCount, tapDelta: tapDelta, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void TwoPointPan(PointI endPoint, uint holdDuration, float acceleration, uint distance) {
            var pointI = Input.AdjustPointerMoveInput(originalPoint: endPoint);
            var distance1 = (uint) Math.Round(a: Math.Sqrt(d: (pointI.Y - Location.Y) * (pointI.Y - Location.Y) + (pointI.X - Location.X) * (pointI.X - Location.X)));
            var direction = (float) (Math.Atan2(y: pointI.Y - Location.Y, x: pointI.X - Location.X) * (180.0 / Math.PI));
            this._inputManager.InjectMTPanWithAcceleration(startFingerOne: Location, startFingerTwo: CreateSecondFingerPointFromDistance(distance: distance, direction: direction + 90f), direction: direction, distance: distance1, holdDuration: holdDuration, acceleration: acceleration, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void Pinch(float direction, uint duration, uint startDistance, uint endDistance) {
            Pinch(direction: direction, duration: duration, startDistance: startDistance, endDistance: endDistance, pivot: false);
        }

        public void Pinch(
            float direction,
            uint duration,
            uint startDistance,
            uint endDistance,
            bool pivot) {
            var pointFromDistance = CreateSecondFingerPointFromDistance(distance: startDistance, direction: direction);
            var distance = pivot ? startDistance - endDistance : (startDistance - endDistance) / 2U;
            direction = (float) ((direction + 180.0) % 360.0);
            this._inputManager.InjectMTZoom(startFingerOne: Location, startFingerTwo: pointFromDistance, direction: direction, duration: duration, distance: distance, pivotZoom: pivot, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void Stretch(float direction, uint duration, uint startDistance, uint endDistance) {
            Stretch(direction: direction, duration: duration, startDistance: startDistance, endDistance: endDistance, pivot: false);
        }

        public void Stretch(
            float direction,
            uint duration,
            uint startDistance,
            uint endDistance,
            bool pivot) {
            var pointFromDistance = CreateSecondFingerPointFromDistance(distance: startDistance, direction: direction);
            var distance = pivot ? endDistance - startDistance : (endDistance - startDistance) / 2U;
            this._inputManager.InjectMTZoom(startFingerOne: Location, startFingerTwo: pointFromDistance, direction: direction, duration: duration, distance: distance, pivotZoom: pivot, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void Rotate(float angle, uint duration, bool centerOnPoint, uint distance) {
            this._inputManager.InjectMTRotate(startFingerOne: Location, startFingerTwo: CreateSecondFingerPointFromDistance(distance: distance, direction: 180f), rotationAngle: angle, duration: duration, pivotRotate: centerOnPoint, packetDelta: InputManager.DefaultPacketDelta);
        }

        public void InjectMultiPointGesture(MultiTouchInjectionData[] injectionData) {
            this._inputManager.InjectMultiTouch(injectionData: injectionData, packetDelta: InputManager.DefaultPacketDelta);
        }

        ~MultiTouch() {
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

        void Move(PointI point, uint contactId) {
            var start = contactId < MaxNumberOfContacts && contactId >= 0U ? this._location[(int) contactId] : throw new ArgumentOutOfRangeException(paramName: nameof(contactId), message: "The given contact Id does not exist.");
            this._location[(int) contactId] = Input.AdjustPointerMoveInput(originalPoint: point);
            this._inputManager.InjectDynamicMove(start: start, end: this._location[(int) contactId], maxDragDuration: SinglePointGesture.DefaultDragDuration, contactId: this.DefaultContactId, packetDelta: InputManager.DefaultPacketDelta);
        }

        PointI GetLocationOfContact(uint contactId) {
            return contactId < MaxNumberOfContacts && contactId >= 0U ? this._location[(int) contactId] : throw new ArgumentOutOfRangeException(paramName: nameof(contactId), message: "The given contact Id does not exist.");
        }

        PointI CreateSecondFingerPointFromDistance(uint distance, float direction) {
            PointI pointI;
            var location = Location;
            var x = location.X;
            location = Location;
            var y = location.Y;
            pointI = new PointI(x: x, y: y);
            var num = direction * Math.PI / 180.0;
            pointI.X += (int) (distance * Math.Cos(d: num));
            pointI.Y -= (int) (distance * Math.Sin(a: num));
            return pointI;
        }
    }
}