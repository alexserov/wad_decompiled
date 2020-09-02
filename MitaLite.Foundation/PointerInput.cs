// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.PointerInput
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    public static class PointerInput {
        public static readonly uint DefaultDragDuration = 900;
        static readonly Stack<IPointerInput> _pointerInputStack = new Stack<IPointerInput>();

        public static PointI Location {
            get { return Current.Location; }
        }

        public static IPointerInput Current {
            get {
                if (_pointerInputStack.Count == 0)
                    lock (_pointerInputStack) {
                        if (_pointerInputStack.Count == 0)
                            _pointerInputStack.Push(item: Mouse.Instance);
                    }

                return _pointerInputStack.Peek();
            }
        }

        public static void Click(PointerButtons button, int count) {
            Current.Click(button: button, count: count);
        }

        public static void ClickDrag(PointI endPoint, PointerButtons button, uint dragDuration) {
            Current.ClickDrag(endPoint: endPoint, button: button, dragDuration: dragDuration);
        }

        public static void Move(PointI point) {
            Current.Move(point: point);
        }

        public static void Press(PointerButtons button) {
            Current.Press(button: button);
        }

        public static void Release(PointerButtons button) {
            Current.Release(button: button);
        }

        public static void InjectPointers(PointerData[] pointerDataArray) {
            Current.InjectPointers(pointerDataArray: pointerDataArray);
        }

        public static void Click(UIObject uiObject) {
            Click(uiObject: uiObject, button: PointerButtons.Primary);
        }

        public static void Click(UIObject uiObject, PointerButtons button) {
            ClickImplementation(uiObject: uiObject, button: button, numClicks: 1U);
        }

        public static void Click(
            UIObject uiObject,
            PointerButtons button,
            double offsetX,
            double offsetY) {
            ClickImplementation(uiObject: uiObject, button: button, numClicks: 1U, offsetX: offsetX, offsetY: offsetY);
        }

        public static void DoubleClick(UIObject uiObject) {
            DoubleClick(uiObject: uiObject, button: PointerButtons.Primary);
        }

        public static void DoubleClick(UIObject uiObject, PointerButtons button) {
            ClickImplementation(uiObject: uiObject, button: button, numClicks: 2U);
        }

        public static void DoubleClick(
            UIObject uiObject,
            PointerButtons button,
            double offsetX,
            double offsetY) {
            ClickImplementation(uiObject: uiObject, button: button, numClicks: 2U, offsetX: offsetX, offsetY: offsetY);
        }

        public static void ClickDrag(UIObject targetObject, UIObject destinationObject) {
            ClickDrag(targetObject: targetObject, destinationObject: destinationObject, button: PointerButtons.Primary);
        }

        public static void ClickDrag(
            UIObject targetObject,
            UIObject destinationObject,
            PointerButtons button) {
            Move(uiObject: targetObject);
            ClickDrag(endPoint: destinationObject.GetClickablePoint(), button: button, dragDuration: DefaultDragDuration);
        }

        public static void Move(UIObject uiObject) {
            Move(point: uiObject.GetClickablePoint());
        }

        public static void Move(UIObject uiObject, double offsetX, double offsetY) {
            var location = uiObject.BoundingRectangle.Location;
            Move(point: new PointI(x: (int) (location.X + offsetX), y: (int) (location.Y + offsetY)));
        }

        public static IDisposable Activate(IPointerInput pointer) {
            return new InputControllerMartyr<IPointerInput>(inputStack: _pointerInputStack, inputController: pointer);
        }

        static void ClickImplementation(
            UIObject uiObject,
            PointerButtons button,
            uint numClicks,
            double offsetX,
            double offsetY) {
            Move(uiObject: uiObject, offsetX: offsetX, offsetY: offsetY);
            Click(button: button, count: (int) numClicks);
        }

        static void ClickImplementation(
            UIObject uiObject,
            PointerButtons button,
            uint numClicks) {
            Move(uiObject: uiObject);
            Click(button: button, count: (int) numClicks);
        }
    }
}