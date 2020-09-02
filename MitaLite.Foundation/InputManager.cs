// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputManager
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MS.Internal.Mita.Foundation {
    internal class InputManager : IInputManager, IDisposable {
        readonly Dictionary<uint, PointerData> _activePointerStates;
        bool _disposed;
        int[] _previousRuntimeId;
        readonly List<IInputAction> backgroundInputActions;
        readonly PointI[] downPressLocation;
        readonly object downPressLock;
        readonly IInputAlgorithms inputAlgorithms;
        IInputDevice inputDevice;
        Thread inputDownPressThread;
        readonly IInputQueue inputQueue;
        INPUT_DEVICE_TYPE inputType;
        int pressActive;
        readonly ITimeManagerFactory timeFactory;

        protected InputManager() {
            throw new NotImplementedException();
        }

        public InputManager(
            INPUT_DEVICE_TYPE inputType,
            IInputDeviceFactory inputFactory,
            ITimeManagerFactory timeFactory,
            IInputAlgorithms inputAlgorithms) {
            this.inputDevice = inputFactory.Get(inputType: inputType);
            this.inputType = inputType;
            this.timeFactory = timeFactory;
            this.inputAlgorithms = inputAlgorithms;
            this.downPressLock = new object();
            this.inputQueue = new InputQueue();
            this.backgroundInputActions = new List<IInputAction>();
            this.downPressLocation = new PointI[10];
            this._activePointerStates = new Dictionary<uint, PointerData>();
        }

        public static uint DefaultPressDuration { get; } = 100;

        public static uint DefaultTapDelta { get; } = 100;

        public static uint DefaultPacketDelta { get; } = 10;

        public void InjectPress(
            PointI point,
            uint holdDuration,
            uint tapCount,
            uint tapDelta,
            uint packetDelta) {
            _UpdatePressActive(bActive: false);
            var inputActionList = new List<IInputAction>();
            inputActionList.AddRange(collection: Input.PreventAccidentalDoubleClick(x: point.X, y: point.Y, previousRuntimeId: ref this._previousRuntimeId));
            inputActionList.AddRange(collection: this.inputAlgorithms.PressAndHold(point: point, holdDuration: holdDuration, tapCount: tapCount, tapDelta: tapDelta, packetDelta: packetDelta));
            using (this.timeFactory.Get()) {
                this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputActionList);
            }
        }

        public void InjectPressAndDrag(
            PointI start,
            PointI end,
            uint dragDuration,
            uint holdDuration,
            uint packetDelta) {
            _UpdatePressActive(bActive: false);
            var inputList = this.inputAlgorithms.PressAndHoldAndDrag(start: start, end: end, dragDuration: dragDuration, holdDuration: holdDuration, packetDelta: packetDelta);
            using (this.timeFactory.Get()) {
                this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputList);
            }
        }

        public void InjectPressAndDragWithAcceleration(
            PointI start,
            PointI end,
            uint holdDuration,
            float acceleration,
            uint packetDelta) {
            _UpdatePressActive(bActive: false);
            var inputList = this.inputAlgorithms.PressAndHoldAndDragWithAcceleration(start: start, end: end, holdDuration: holdDuration, acceleration: acceleration, packetDelta: packetDelta);
            using (this.timeFactory.Get()) {
                this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputList);
            }
        }

        public void InjectMTPanWithAcceleration(
            PointI startFingerOne,
            PointI startFingerTwo,
            float direction,
            uint distance,
            uint holdDuration,
            float acceleration,
            uint packetDelta) {
            _UpdatePressActive(bActive: false);
            _UpdatePressActive(bActive: false, contactId: 1);
            var inputList = this.inputAlgorithms.TwoFingerPressAndHoldAndDragWithAcceleration(pointOne: startFingerOne, pointTwo: startFingerTwo, direction: direction, distance: distance, holdDuration: holdDuration, acceleration: acceleration, packetDelta: packetDelta);
            using (this.timeFactory.Get()) {
                this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputList);
            }
        }

        public void InjectMTZoom(
            PointI startFingerOne,
            PointI startFingerTwo,
            float direction,
            uint duration,
            uint distance,
            bool pivotZoom,
            uint packetDelta) {
            _UpdatePressActive(bActive: false);
            _UpdatePressActive(bActive: false, contactId: 1);
            var inputList = this.inputAlgorithms.TwoFingerZoom(pointOne: startFingerOne, pointTwo: startFingerTwo, direction: direction, duration: duration, distance: distance, pivotZoom: pivotZoom, packetDelta: packetDelta);
            using (this.timeFactory.Get()) {
                this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputList);
            }
        }

        public void InjectMTRotate(
            PointI startFingerOne,
            PointI startFingerTwo,
            float rotationAngle,
            uint duration,
            bool pivotRotate,
            uint packetDelta) {
            _UpdatePressActive(bActive: false);
            _UpdatePressActive(bActive: false, contactId: 1);
            var inputList = this.inputAlgorithms.TwoFingerRotate(pointOne: startFingerOne, pointTwo: startFingerTwo, rotationAngle: rotationAngle, duration: duration, pivotRotate: pivotRotate, packetDelta: packetDelta);
            using (this.timeFactory.Get()) {
                this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputList);
            }
        }

        public void InjectMTTwoFingerPress(
            PointI startFingerOne,
            PointI startFingerTwo,
            uint holdDuration,
            uint tapCount,
            uint tapDelta,
            uint packetDelta) {
            _UpdatePressActive(bActive: false);
            _UpdatePressActive(bActive: false, contactId: 1);
            var inputList = this.inputAlgorithms.TwoFingerPressAndHold(pointOne: startFingerOne, pointTwo: startFingerTwo, holdDuration: holdDuration, tapCount: tapCount, tapDelta: tapDelta, packetDelta: packetDelta);
            using (this.timeFactory.Get()) {
                this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputList);
            }
        }

        public void InjectMTPressAndTap(
            PointI startFingerOne,
            PointI startFingerTwo,
            PointI endFingerOne,
            uint deltaFingerTwoDown,
            uint deltaFingerTwoUp,
            uint deltaFingerOneUp,
            uint packetDelta) {
            _UpdatePressActive(bActive: false);
            _UpdatePressActive(bActive: false, contactId: 1);
            throw new NotImplementedException();
        }

        public void InjectMultiTouch(MultiTouchInjectionData[] injectionData, uint packetDelta) {
            lock (this.downPressLock) {
                this.backgroundInputActions.AddRange(collection: this.inputAlgorithms.RawMultiTouchGesture(pointdata: injectionData, packetDelta: packetDelta));
            }

            _StartDownpressThread();
            while (this.backgroundInputActions.Count > 0)
                Thread.Sleep(millisecondsTimeout: (int) DefaultPressDuration);
        }

        public void InjectDynamicPress(PointI touchPoint, uint contactId) {
            _UpdatePressActive(bActive: true, contactId: (int) contactId);
            this.downPressLocation[(int) contactId] = touchPoint;
            lock (this.downPressLock) {
                this.backgroundInputActions.AddRange(collection: this.inputAlgorithms.DynamicPress(point: touchPoint, contactId: contactId));
            }

            _StartDownpressThread();
            while (this.backgroundInputActions.Count > 0)
                Thread.Sleep(millisecondsTimeout: (int) DefaultPressDuration);
        }

        public void InjectDynamicMove(
            PointI start,
            PointI end,
            uint maxDragDuration,
            uint contactId,
            uint packetDelta) {
            IList<IInputAction> inputActionList = new List<IInputAction>();
            if (!DynamicPressActive())
                return;
            this.backgroundInputActions.AddRange(collection: this.inputAlgorithms.DynamicDrag(start: start, end: end, duration: maxDragDuration, packetDelta: packetDelta, contactId: contactId));
            while (this.backgroundInputActions.Count > 0)
                Thread.Sleep(millisecondsTimeout: (int) DefaultPressDuration);
        }

        public void InjectDynamicRelease(PointI touchPoint, uint contactId) {
            if (!DynamicPressActive(iContactId: (int) contactId))
                return;
            _UpdatePressActive(bActive: false, contactId: (int) contactId);
            var inputList = this.inputAlgorithms.DynamicRelease(point: touchPoint, contactId: contactId);
            using (this.timeFactory.Get()) {
                this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputList);
            }
        }

        public void InjectDynamicPointers(PointerData[] pointerDataArray) {
            lock (this.downPressLock) {
                var dictionary = new Dictionary<uint, PointerData>(dictionary: this._activePointerStates);
                foreach (var pointerData1 in pointerDataArray) {
                    var pointerId = pointerData1.pointerId;
                    if ((pointerData1.flags & POINTER_FLAGS.ContactDown) == POINTER_FLAGS.ContactDown) {
                        dictionary[key: pointerId] = pointerData1;
                        var pointerData2 = pointerData1;
                        pointerData2.flags = POINTER_FLAGS.ContactMoves;
                        this._activePointerStates[key: pointerId] = pointerData2;
                    } else if ((pointerData1.flags & POINTER_FLAGS.ContactMoves) == POINTER_FLAGS.ContactMoves) {
                        if (!this._activePointerStates.ContainsKey(key: pointerId))
                            throw new ArgumentException(message: "$Contact moves cannot take place before contact down: {contactId}");
                        dictionary[key: pointerId] = pointerData1;
                        this._activePointerStates[key: pointerId] = pointerData1;
                    } else {
                        dictionary[key: pointerId] = (pointerData1.flags & POINTER_FLAGS.UP) == POINTER_FLAGS.UP ? pointerData1 : throw new Exception(message: "Unexpected condition!");
                        this._activePointerStates.Remove(key: pointerId);
                    }
                }

                var pointerDataArray1 = new PointerData[dictionary.Count];
                dictionary.Values.CopyTo(array: pointerDataArray1, index: 0);
                this.backgroundInputActions.AddRange(collection: this.inputAlgorithms.DynamicPointerActions(pointerDataArray: pointerDataArray1));
            }

            if (this._activePointerStates.Count <= 0)
                return;
            while (this.backgroundInputActions.Count > 0) {
                _StartDownpressThread();
                Thread.Sleep(millisecondsTimeout: (int) DefaultPacketDelta);
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        public bool DynamicPressActive(int iContactId = -1) {
            return iContactId == -1 ? this.pressActive > 0 : (this.pressActive & (1 << iContactId)) > 0;
        }

        ~InputManager() {
            Dispose(disposing: false);
        }

        void Dispose(bool disposing) {
            if (this._disposed)
                return;
            if (disposing) {
                this.inputDevice.Dispose();
                this.inputDevice = null;
            }

            this._disposed = true;
        }

        void _StartDownpressThread() {
            if (this.inputDownPressThread != null && this.inputDownPressThread.IsAlive)
                return;
            this.inputDownPressThread = new Thread(start: DownPressThreadDelegate());
            this.inputDownPressThread.Start();
        }

        IInputAction buildFingerDownUpdateAction() {
            IInputAction inputAction;
            if (this._activePointerStates.Count > 0) {
                var array = new PointerData[this._activePointerStates.Count];
                this._activePointerStates.Values.CopyTo(array: array, index: 0);
                inputAction = new MultiPointerInputAction {
                    pointerData = array
                };
            } else {
                var pointIList = new List<PointI>();
                var uintList = new List<uint>();
                for (var iContactId = 0; iContactId < 10; ++iContactId)
                    if (DynamicPressActive(iContactId: iContactId)) {
                        pointIList.Add(item: this.downPressLocation[iContactId]);
                        uintList.Add(item: (uint) iContactId);
                    }

                inputAction = this.inputAlgorithms.DynamicMoves(point: pointIList, contactIds: uintList);
            }

            return inputAction;
        }

        ThreadStart DownPressThreadDelegate() {
            return () => {
                var stopwatch = new Stopwatch();
                try {
                    using (this.timeFactory.Get()) {
                        while (true)
                            lock (this.downPressLock) {
                                stopwatch.Restart();
                                if (this.backgroundInputActions.Count == 0 && this._activePointerStates.Count == 0 && !DynamicPressActive())
                                    break;
                                var inputAction = this.backgroundInputActions.Count <= 0 ? buildFingerDownUpdateAction() : this.backgroundInputActions[index: 0];
                                var num = (int) DefaultPacketDelta;
                                if (inputAction.GetActionType() == InputActionType.RelativeWaitInputAction) {
                                    num = ((RelativeWaitInputAction) inputAction).duration;
                                    if (DynamicPressActive())
                                        buildFingerDownUpdateAction().Execute(inputDevice: this.inputDevice, elapsedMs: 0);
                                } else if (inputAction.GetActionType() == InputActionType.AbsoluteWaitInputAction) {
                                    num = ((AbsoluteWaitInputAction) inputAction).duration;
                                    if (DynamicPressActive())
                                        buildFingerDownUpdateAction().Execute(inputDevice: this.inputDevice, elapsedMs: 0);
                                } else if (inputAction.GetActionType() == InputActionType.MultiPointerInputAction) {
                                    if (this._activePointerStates.Count == 0) {
                                        var pointerData1 = ((MultiPointerInputAction) inputAction).pointerData;
                                        for (var index = 0; index < pointerData1.Length && pointerData1[index].pointerId < 10U; ++index) {
                                            var pointerData2 = pointerData1[index];
                                            this.downPressLocation[(int) pointerData2.pointerId] = pointerData2.location;
                                            if ((pointerData2.flags & POINTER_FLAGS.UP) != POINTER_FLAGS.NONE)
                                                _UpdatePressActive(bActive: false, contactId: (int) pointerData2.pointerId);
                                            else if ((pointerData2.flags & POINTER_FLAGS.ContactDown) != POINTER_FLAGS.NONE)
                                                _UpdatePressActive(bActive: true, contactId: (int) pointerData2.pointerId);
                                        }
                                    }

                                    inputAction.Execute(inputDevice: this.inputDevice, elapsedMs: 0);
                                } else {
                                    if (inputAction.GetActionType() == InputActionType.PointerInputAction) {
                                        var pointerData = ((PointerInputAction) inputAction).pointerData;
                                        this.downPressLocation[(int) pointerData.pointerId] = pointerData.location;
                                        if ((pointerData.flags & POINTER_FLAGS.UP) != POINTER_FLAGS.NONE)
                                            _UpdatePressActive(bActive: false, contactId: (int) pointerData.pointerId);
                                        else if ((pointerData.flags & POINTER_FLAGS.ContactDown) != POINTER_FLAGS.NONE)
                                            _UpdatePressActive(bActive: true, contactId: (int) pointerData.pointerId);
                                    }

                                    inputAction.Execute(inputDevice: this.inputDevice, elapsedMs: 0);
                                    if (DynamicPressActive())
                                        buildFingerDownUpdateAction().Execute(inputDevice: this.inputDevice, elapsedMs: 0);
                                }

                                if (this.backgroundInputActions.Count > 0)
                                    this.backgroundInputActions.RemoveAt(index: 0);
                                if (stopwatch.ElapsedMilliseconds < num)
                                    Thread.Sleep(millisecondsTimeout: num - (int) stopwatch.ElapsedMilliseconds);
                            }
                    }
                } catch (Exception ex) {
                    _ClearAllPressActive();
                    this._activePointerStates.Clear();
                    this.backgroundInputActions.Clear();
                }
            };
        }

        void DebugLogInputAction(IInputAction act, string context) {
            if (act.GetActionType() == InputActionType.MultiPointerInputAction) {
                var pointerData = ((MultiPointerInputAction) act).pointerData;
                var num = 0;
                while (num < pointerData.Length)
                    ++num;
            } else {
                var actionType1 = (int) act.GetActionType();
                var actionType2 = (int) act.GetActionType();
                var actionType3 = (int) act.GetActionType();
                var actionType4 = (int) act.GetActionType();
                var actionType5 = (int) act.GetActionType();
            }
        }

        void _UpdatePressActive(bool bActive, int contactId = 0) {
            lock (this.downPressLock) {
                if (bActive)
                    this.pressActive |= 1 << contactId;
                else
                    this.pressActive &= 0 << contactId;
            }
        }

        void _ClearAllPressActive() {
            lock (this.downPressLock) {
                this.pressActive = 0;
            }
        }
    }
}