// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.SinglePointGesture
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    public static class SinglePointGesture {
        static readonly Stack<ISinglePointGestureInput> _gestureInputStack = new Stack<ISinglePointGestureInput>();
        public static readonly float DefaultFlickAcceleration = 2f;
        public static readonly float DefaultPanAcceleration = 0.1f;
        public static readonly uint DefaultDragDuration = 900;
        public static readonly uint DefaultHoldDuration = 1500;

        public static ISinglePointGestureInput Current {
            get {
                if (_gestureInputStack.Count == 0)
                    lock (_gestureInputStack) {
                        if (_gestureInputStack.Count == 0)
                            _gestureInputStack.Push(item: SingleTouch.Instance);
                    }

                return _gestureInputStack.Peek();
            }
        }

        public static int MinimumPanFlickDistance { get; } = 160;

        public static void Flick(PointI endPoint, uint holdDuration, float acceleration) {
            Current.Flick(endPoint: endPoint, holdDuration: holdDuration, acceleration: acceleration);
        }

        public static void Pan(PointI endPoint, uint holdDuration, float acceleration) {
            Current.Pan(endPoint: endPoint, holdDuration: holdDuration, acceleration: acceleration);
        }

        public static void PressAndDrag(PointI endPoint, uint dragDuration) {
            Current.PressAndDrag(endPoint: endPoint, dragDuration: dragDuration);
        }

        public static void PressAndDrag(PointI endPoint, uint dragDuration, uint pressDuration) {
            Current.PressAndDrag(endPoint: endPoint, dragDuration: dragDuration, pressDuration: pressDuration);
        }

        public static void PressAndHold(uint holdDuration) {
            Current.PressAndHold(holdDuration: holdDuration);
        }

        public static void Flick(UIObject targetObject, UIObject destinationObject) {
            Flick(targetObject: targetObject, destinationObject: destinationObject, acceleration: DefaultFlickAcceleration);
        }

        public static void Flick(UIObject targetObject, UIObject destinationObject, float acceleration) {
            var clickablePoint1 = targetObject.GetClickablePoint();
            var clickablePoint2 = destinationObject.GetClickablePoint();
            Current.Move(point: clickablePoint1);
            var defaultPressDuration = (int) InputManager.DefaultPressDuration;
            double num = acceleration;
            Flick(endPoint: clickablePoint2, holdDuration: (uint) defaultPressDuration, acceleration: (float) num);
        }

        public static void Flick(
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
            Flick(endPoint: endPoint, holdDuration: (uint) defaultPressDuration, acceleration: (float) num2);
        }

        public static void Pan(UIObject targetObject, UIObject destinationObject) {
            Pan(targetObject: targetObject, destinationObject: destinationObject, acceleration: DefaultPanAcceleration);
        }

        public static void Pan(UIObject targetObject, UIObject destinationObject, float acceleration) {
            var clickablePoint1 = targetObject.GetClickablePoint();
            var clickablePoint2 = destinationObject.GetClickablePoint();
            Current.Move(point: clickablePoint1);
            var defaultPressDuration = (int) InputManager.DefaultPressDuration;
            double num = acceleration;
            Pan(endPoint: clickablePoint2, holdDuration: (uint) defaultPressDuration, acceleration: (float) num);
        }

        public static void Pan(UIObject uiObject, float acceleration, uint distance, float direction) {
            var clickablePoint = uiObject.GetClickablePoint();
            var num1 = direction * Math.PI / 180.0;
            var endPoint = new PointI(x: clickablePoint.X + (int) Math.Round(a: distance * Math.Cos(d: num1)), y: clickablePoint.Y - (int) Math.Round(a: distance * Math.Sin(a: num1)));
            Current.Move(point: clickablePoint);
            var defaultPressDuration = (int) InputManager.DefaultPressDuration;
            double num2 = acceleration;
            Pan(endPoint: endPoint, holdDuration: (uint) defaultPressDuration, acceleration: (float) num2);
        }

        public static void PressAndDrag(UIObject targetObject, UIObject destinationObject) {
            PressAndDrag(targetObject: targetObject, destinationObject: destinationObject, dragDuration: DefaultDragDuration);
        }

        public static void PressAndDrag(
            UIObject targetObject,
            UIObject destinationObject,
            uint dragDuration,
            uint pressDuration) {
            var clickablePoint1 = targetObject.GetClickablePoint();
            var clickablePoint2 = destinationObject.GetClickablePoint();
            Current.Move(point: clickablePoint1);
            var num1 = (int) dragDuration;
            var num2 = (int) pressDuration;
            PressAndDrag(endPoint: clickablePoint2, dragDuration: (uint) num1, pressDuration: (uint) num2);
        }

        public static void PressAndDrag(
            UIObject targetObject,
            UIObject destinationObject,
            uint dragDuration) {
            var clickablePoint1 = targetObject.GetClickablePoint();
            var clickablePoint2 = destinationObject.GetClickablePoint();
            Current.Move(point: clickablePoint1);
            var num = (int) dragDuration;
            PressAndDrag(endPoint: clickablePoint2, dragDuration: (uint) num);
        }

        public static void PressAndDrag(UIObject uiObject, uint distance, float direction) {
            var clickablePoint = uiObject.GetClickablePoint();
            var num = direction * Math.PI / 180.0;
            var endPoint = new PointI(x: clickablePoint.X + (int) Math.Round(a: distance * Math.Cos(d: num)), y: clickablePoint.Y - (int) Math.Round(a: distance * Math.Sin(a: num)));
            Current.Move(point: clickablePoint);
            var defaultDragDuration = (int) DefaultDragDuration;
            PressAndDrag(endPoint: endPoint, dragDuration: (uint) defaultDragDuration);
        }

        public static void PressAndHold(UIObject uiObject) {
            PressAndHold(uiObject: uiObject, holdDuration: DefaultHoldDuration);
        }

        public static void PressAndHold(UIObject uiObject, uint holdDuration) {
            Current.Move(point: uiObject.GetClickablePoint());
            PressAndHold(holdDuration: holdDuration);
        }

        public static IDisposable Activate(ISinglePointGestureInput pointer) {
            return new InputControllerMartyr<ISinglePointGestureInput>(inputStack: _gestureInputStack, inputController: pointer);
        }
    }
}