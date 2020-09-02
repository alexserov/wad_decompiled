// Decompiled with JetBrains decompiler
// Type: MitaBroker.TouchHandler
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using MS.Internal.Mita.Foundation;

namespace MitaBroker {
    internal sealed class TouchHandler {
        internal static ResponseStatus SendTouchTypeClick(
            string touchType,
            UIObject clickElement) {
            var responseStatus = ResponseStatus.UnknownError;
            if (!(touchType == "click")) {
                if (!(touchType == "doubleclick")) {
                    if (touchType == "longclick") {
                        clickElement.TapAndHold();
                        responseStatus = ResponseStatus.Success;
                    }
                } else {
                    clickElement.DoubleTap();
                    responseStatus = ResponseStatus.Success;
                }
            } else {
                clickElement.Tap();
                responseStatus = ResponseStatus.Success;
            }

            return responseStatus;
        }

        internal static ResponseStatus SendTouchTypePress(string touchType, int x, int y) {
            var responseStatus = ResponseStatus.UnknownError;
            var point = new PointI(x: x, y: y);
            if (!(touchType == "down")) {
                if (!(touchType == "move")) {
                    if (touchType == "up") {
                        using (InputController.Activate(inputType: PointerInputType.MultiTouch)) {
                            PointerInput.Move(point: point);
                            PointerInput.Release(button: PointerButtons.Primary);
                        }

                        responseStatus = ResponseStatus.Success;
                    }
                } else {
                    using (InputController.Activate(inputType: PointerInputType.MultiTouch)) {
                        PointerInput.Move(point: point);
                    }

                    responseStatus = ResponseStatus.Success;
                }
            } else {
                using (InputController.Activate(inputType: PointerInputType.MultiTouch)) {
                    PointerInput.Move(point: point);
                    PointerInput.Press(button: PointerButtons.Primary);
                }

                responseStatus = ResponseStatus.Success;
            }

            return responseStatus;
        }

        internal static ResponseStatus SendTouchTypeScroll(
            int xOffset,
            int yOffset,
            UIObject scrollElement) {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                var relativeClickablePoint = GetRelativeClickablePoint(element: scrollElement);
                var endPoint = new PointI(x: relativeClickablePoint.X + xOffset, y: relativeClickablePoint.Y + yOffset);
                scrollElement.Pan(startPoint: relativeClickablePoint, endPoint: endPoint);
                responseStatus = ResponseStatus.Success;
            } catch {
            }

            return responseStatus;
        }

        internal static ResponseStatus SendTouchFlick(
            int flickXOffset,
            int flickYOffset,
            UIObject flickElement) {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                var relativeClickablePoint = GetRelativeClickablePoint(element: flickElement);
                var endPoint = new PointI(x: relativeClickablePoint.X + flickXOffset, y: relativeClickablePoint.Y + flickYOffset);
                flickElement.Flick(startPoint: relativeClickablePoint, endPoint: endPoint);
                responseStatus = ResponseStatus.Success;
            } catch {
            }

            return responseStatus;
        }

        internal static ResponseStatus SendTouchFlick(
            UIObject mainElement,
            int xSpeed,
            int ySpeed) {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                mainElement.SetFocus();
                mainElement.Flick(startPoint: GetRelativeClickablePoint(element: mainElement), distance: (int) Math.Max(val1: Math.Sqrt(d: xSpeed * xSpeed + ySpeed * ySpeed), val2: SinglePointGesture.MinimumPanFlickDistance), direction: (float) (Math.Atan2(y: ySpeed, x: xSpeed) * (180.0 / Math.PI)));
                responseStatus = ResponseStatus.Success;
            } catch {
            }

            return responseStatus;
        }

        static PointI GetRelativeClickablePoint(UIObject element) {
            var boundingRectangle = element.BoundingRectangle;
            var left = boundingRectangle.Left;
            boundingRectangle = element.BoundingRectangle;
            var right = boundingRectangle.Right;
            var num1 = (left + right) / 2;
            boundingRectangle = element.BoundingRectangle;
            var x1 = boundingRectangle.TopLeft.X;
            var x2 = num1 - x1;
            boundingRectangle = element.BoundingRectangle;
            var top = boundingRectangle.Top;
            boundingRectangle = element.BoundingRectangle;
            var bottom = boundingRectangle.Bottom;
            var num2 = (top + bottom) / 2;
            boundingRectangle = element.BoundingRectangle;
            var y1 = boundingRectangle.TopLeft.Y;
            var y2 = num2 - y1;
            return new PointI(x: x2, y: y2);
        }
    }
}