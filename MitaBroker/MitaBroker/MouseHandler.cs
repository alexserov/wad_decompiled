// Decompiled with JetBrains decompiler
// Type: MitaBroker.MouseHandler
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using MS.Internal.Mita.Foundation;

namespace MitaBroker {
    public sealed class MouseHandler {
        internal static ResponseStatus SendMouseAction(
            string actionType,
            int buttonNumber) {
            var responseStatus = ResponseStatus.UnknownError;
            PointerButtons button;
            switch (buttonNumber) {
                case 0:
                    button = PointerButtons.Primary;
                    break;
                case 1:
                    button = PointerButtons.Middle;
                    break;
                case 2:
                    button = PointerButtons.Secondary;
                    break;
                default:
                    throw new ArgumentException(message: string.Format(format: "Bad mouse button value: {0}. Valid values are LEFT = 0, MIDDLE = 1, RIGHT = 2", arg0: buttonNumber));
            }

            if (!(actionType == "buttondown")) {
                if (!(actionType == "buttonup")) {
                    if (actionType == "click") {
                        using (InputController.Activate(inputType: PointerInputType.Mouse)) {
                            PointerInput.Click(button: button, count: 1);
                        }

                        responseStatus = ResponseStatus.Success;
                    }
                } else {
                    using (InputController.Activate(inputType: PointerInputType.Mouse)) {
                        PointerInput.Release(button: button);
                    }

                    responseStatus = ResponseStatus.Success;
                }
            } else {
                using (InputController.Activate(inputType: PointerInputType.Mouse)) {
                    PointerInput.Press(button: button);
                }

                responseStatus = ResponseStatus.Success;
            }

            return responseStatus;
        }

        internal static ResponseStatus SendMouseDoubleClick() {
            using (InputController.Activate(inputType: PointerInputType.Mouse)) {
                PointerInput.Click(button: PointerButtons.Primary, count: 2);
            }

            return ResponseStatus.Success;
        }

        internal static ResponseStatus SendMouseMoveToElementCenter(
            string mouseMoveType,
            UIObject element) {
            var rectangleCenterPosition = element.GetAdjustedBoundingRectangleCenterPosition();
            using (InputController.Activate(inputType: PointerInputType.Mouse)) {
                PointerInput.Move(point: rectangleCenterPosition);
                return ResponseStatus.Success;
            }
        }

        internal static ResponseStatus SendMouseMoveToElementRelative(
            string mouseMoveType,
            UIObject element,
            int xOffset,
            int yOffset) {
            var topLeft = element.GetAdjustedBoundingRectangle().TopLeft;
            topLeft.X += xOffset;
            topLeft.Y += yOffset;
            using (InputController.Activate(inputType: PointerInputType.Mouse)) {
                PointerInput.Move(point: topLeft);
                return ResponseStatus.Success;
            }
        }

        internal static ResponseStatus SendMouseMoveToRelative(
            string mouseMoveType,
            int xOffset,
            int yOffset) {
            using (InputController.Activate(inputType: PointerInputType.Mouse)) {
                var location = PointerInput.Location;
                location.X += xOffset;
                location.Y += yOffset;
                PointerInput.Move(point: location);
                return ResponseStatus.Success;
            }
        }
    }
}