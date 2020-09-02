// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.MultiPointGesture
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public static class MultiPointGesture {
        static readonly Stack<IMultiPointGestureInput> _gestureInputStack = new Stack<IMultiPointGestureInput>();
        public static readonly uint DefaultHoldDuration = 500;
        public static readonly uint DefaultTapDelta = 500;
        public static readonly uint DefaultTapDistance = 10;
        public static readonly float DefaultTwoPointPanAcceleration = 0.1f;
        public static readonly uint DefaultTwoPointPanDistance = 10;
        public static readonly uint DefaultPinchStretchDistance = 150;
        public static readonly float DefaultPinchStretchDirection = 90f;
        public static readonly uint DefaultPinchStretchDuration = 1000;
        public static readonly uint DefaultRotateDistance = 100;
        public static readonly uint DefaultRotateDuration = 1000;

        public static IMultiPointGestureInput Current {
            get {
                if (_gestureInputStack.Count == 0)
                    lock (_gestureInputStack) {
                        if (_gestureInputStack.Count == 0)
                            _gestureInputStack.Push(item: MultiTouch.Instance);
                    }

                return _gestureInputStack.Peek();
            }
        }

        public static void PressAndTap(uint tapCount, uint tapDuration, uint tapDelta, uint distance) {
            Current.PressAndTap(tapCount: tapCount, tapDuration: tapDuration, tapDelta: tapDelta, distance: distance);
        }

        public static void TwoPointPressAndHold(
            uint tapCount,
            uint holdDuration,
            uint tapDelta,
            uint distance) {
            Current.TwoPointPressAndHold(tapCount: tapCount, holdDuration: holdDuration, tapDelta: tapDelta, distance: distance);
        }

        public static void TwoPointPan(
            PointI endPoint,
            uint holdDuration,
            float acceleration,
            uint distance) {
            Current.TwoPointPan(endPoint: endPoint, holdDuration: holdDuration, acceleration: acceleration, distance: distance);
        }

        public static void Pinch(float direction, uint duration, uint startDistance, uint endDistance) {
            Current.Pinch(direction: direction, duration: duration, startDistance: startDistance, endDistance: endDistance);
        }

        public static void Pinch(
            float direction,
            uint duration,
            uint startDistance,
            uint endDistance,
            bool pivot) {
            Current.Pinch(direction: direction, duration: duration, startDistance: startDistance, endDistance: endDistance, pivot: pivot);
        }

        public static void Stretch(
            float direction,
            uint duration,
            uint startDistance,
            uint endDistance) {
            Current.Stretch(direction: direction, duration: duration, startDistance: startDistance, endDistance: endDistance);
        }

        public static void Stretch(
            float direction,
            uint duration,
            uint startDistance,
            uint endDistance,
            bool pivot) {
            Current.Stretch(direction: direction, duration: duration, startDistance: startDistance, endDistance: endDistance, pivot: pivot);
        }

        public static void Rotate(float angle, uint duration, bool centerOnPoint, uint distance) {
            Current.Rotate(angle: angle, duration: duration, centerOnPoint: centerOnPoint, distance: distance);
        }

        public static void InjectMultiPointGesture(MultiTouchInjectionData[] injectionData) {
            Current.InjectMultiPointGesture(injectionData: injectionData);
        }

        public static void PressAndTap(UIObject uiObject) {
            Current.Move(point: uiObject.GetClickablePoint());
            PressAndTap(tapCount: 1U, tapDuration: DefaultHoldDuration, tapDelta: DefaultTapDelta, distance: DefaultTapDistance);
        }

        public static void TwoPointPressAndHold(UIObject uiObject) {
            TwoPointPressAndHold(uiObject: uiObject, count: 1U);
        }

        public static void TwoPointPressAndHold(UIObject uiObject, uint count) {
            Current.Move(point: uiObject.GetClickablePoint());
            TwoPointPressAndHold(tapCount: count, holdDuration: InputManager.DefaultPressDuration, tapDelta: InputManager.DefaultTapDelta, distance: DefaultTapDistance);
        }

        public static void TwoPointPan(UIObject targetObject, UIObject destinationObject) {
            TwoPointPan(targetObject: targetObject, destinationObject: destinationObject, acceleration: DefaultTwoPointPanAcceleration);
        }

        public static void TwoPointPan(
            UIObject targetObject,
            UIObject destinationObject,
            float acceleration) {
            var clickablePoint1 = targetObject.GetClickablePoint();
            var clickablePoint2 = destinationObject.GetClickablePoint();
            Current.Move(point: clickablePoint1);
            var defaultPressDuration = (int) InputManager.DefaultPressDuration;
            double num = acceleration;
            var pointPanDistance = (int) DefaultTwoPointPanDistance;
            TwoPointPan(endPoint: clickablePoint2, holdDuration: (uint) defaultPressDuration, acceleration: (float) num, distance: (uint) pointPanDistance);
        }

        public static void TwoPointPan(
            UIObject uiObject,
            float acceleration,
            uint distance,
            float direction) {
            var clickablePoint = uiObject.GetClickablePoint();
            var num1 = direction * Math.PI / 180.0;
            var endPoint = new PointI(x: clickablePoint.X + (int) Math.Round(a: distance * Math.Cos(d: num1)), y: clickablePoint.Y - (int) Math.Round(a: distance * Math.Sin(a: num1)));
            Current.Move(point: clickablePoint);
            var defaultPressDuration = (int) InputManager.DefaultPressDuration;
            double num2 = acceleration;
            var num3 = (int) distance;
            TwoPointPan(endPoint: endPoint, holdDuration: (uint) defaultPressDuration, acceleration: (float) num2, distance: (uint) num3);
        }

        public static void Pinch(UIObject uiObject) {
            Pinch(uiObject: uiObject, distance: DefaultPinchStretchDistance);
        }

        public static void Pinch(UIObject uiObject, uint distance) {
            Pinch(uiObject: uiObject, distance: distance, direction: DefaultPinchStretchDirection);
        }

        public static void Pinch(UIObject uiObject, uint distance, float direction) {
            Current.Move(point: OffsetPinchPoints(uiObject: uiObject, distance: distance, direction: direction));
            Pinch(direction: direction, duration: DefaultPinchStretchDuration, startDistance: distance, endDistance: 0U);
        }

        public static void Pinch(UIObject uiObject, uint distance, float direction, bool pivot) {
            var point = uiObject.GetClickablePoint();
            if (!pivot)
                point = OffsetPinchPoints(uiObject: uiObject, distance: distance, direction: direction);
            Current.Move(point: point);
            Pinch(direction: direction, duration: DefaultPinchStretchDuration, startDistance: distance, endDistance: 0U, pivot: pivot);
        }

        public static void Stretch(UIObject uiObject) {
            Stretch(uiObject: uiObject, distance: DefaultPinchStretchDistance);
        }

        public static void Stretch(UIObject uiObject, uint distance) {
            Stretch(uiObject: uiObject, distance: distance, direction: DefaultPinchStretchDirection);
        }

        public static void Stretch(UIObject uiObject, uint distance, float direction) {
            Current.Move(point: uiObject.GetClickablePoint());
            Stretch(direction: direction, duration: DefaultPinchStretchDuration, startDistance: 0U, endDistance: distance);
        }

        public static void Stretch(UIObject uiObject, uint distance, float direction, bool pivot) {
            Current.Move(point: uiObject.GetClickablePoint());
            Stretch(direction: direction, duration: DefaultPinchStretchDuration, startDistance: 0U, endDistance: distance, pivot: pivot);
        }

        public static void Rotate(UIObject uiObject, float angle) {
            Rotate(uiObject: uiObject, angle: angle, centerOnPoint: false);
        }

        public static void Rotate(UIObject uiObject, uint distance, float angle) {
            Rotate(uiObject: uiObject, distance: distance, angle: angle, centerOnPoint: false);
        }

        public static void Rotate(UIObject uiObject, float angle, bool centerOnPoint) {
            Rotate(uiObject: uiObject, distance: DefaultRotateDistance, angle: angle, centerOnPoint: centerOnPoint);
        }

        public static void Rotate(UIObject uiObject, uint distance, float angle, bool centerOnPoint) {
            var point = uiObject.GetClickablePoint();
            if (!centerOnPoint)
                point = OffsetPinchPoints(uiObject: uiObject, distance: distance, direction: 0.0f);
            Current.Move(point: point);
            Rotate(angle: angle, duration: DefaultRotateDuration, centerOnPoint: centerOnPoint, distance: distance);
        }

        public static IDisposable Activate(IMultiPointGestureInput pointer) {
            return new InputControllerMartyr<IMultiPointGestureInput>(inputStack: _gestureInputStack, inputController: pointer);
        }

        internal static PointI OffsetPinchPoints(
            UIObject uiObject,
            uint distance,
            float direction) {
            return OffsetPinchPoints(targetPoint: uiObject.GetClickablePoint(), boundingRectangle: uiObject.BoundingRectangle, distance: distance, direction: direction);
        }

        internal static PointI OffsetPinchPoints(
            PointI targetPoint,
            RectangleI boundingRectangle,
            uint distance,
            float direction) {
            var num1 = distance / 2.0;
            var num2 = direction * Math.PI / 180.0;
            var offsetX = (int) Math.Round(a: num1 * Math.Cos(d: num2));
            var offsetY = (int) Math.Round(a: num1 * Math.Sin(a: num2));
            if (targetPoint.X + offsetX > boundingRectangle.Right || targetPoint.X + offsetX < boundingRectangle.Left || targetPoint.Y + offsetY > boundingRectangle.Bottom || targetPoint.Y + offsetY < boundingRectangle.Top)
                Log.Out(msg: "Target points are outside of target object");
            targetPoint.Offset(offsetX: offsetX, offsetY: offsetY);
            return targetPoint;
        }
    }
}