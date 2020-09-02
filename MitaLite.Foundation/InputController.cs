// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputController
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public static class InputController {
        static PointerInputType activeInputType = PointerInputType.Mouse;

        public static PointerInputType ActiveInputType {
            get { return activeInputType; }
            internal set {
                activeInputType = value;
                Log.Out(msg: "Active Input Type: {0}", (object) value);
            }
        }

        public static CompositeInputControllerMartyr Activate(
            PointerInputType inputType) {
            var controllerMartyr = new CompositeInputControllerMartyr(previousInputType: ActiveInputType);
            switch (inputType) {
                case PointerInputType.Mouse:
                    controllerMartyr.Add(martyr: PointerInput.Activate(pointer: Mouse.Instance));
                    controllerMartyr.Add(martyr: MouseWheelInput.Activate(mouseWheel: Mouse.Instance));
                    break;
                case PointerInputType.Pen:
                    controllerMartyr.Add(martyr: PointerInput.Activate(pointer: Pen.Instance));
                    controllerMartyr.Add(martyr: SinglePointGesture.Activate(pointer: Pen.Instance));
                    break;
                case PointerInputType.SingleTouch:
                    controllerMartyr.Add(martyr: PointerInput.Activate(pointer: SingleTouch.Instance));
                    controllerMartyr.Add(martyr: SinglePointGesture.Activate(pointer: SingleTouch.Instance));
                    break;
                case PointerInputType.MultiTouch:
                    controllerMartyr.Add(martyr: PointerInput.Activate(pointer: MultiTouch.Instance));
                    controllerMartyr.Add(martyr: SinglePointGesture.Activate(pointer: MultiTouch.Instance));
                    controllerMartyr.Add(martyr: MultiPointGesture.Activate(pointer: MultiTouch.Instance));
                    break;
            }

            ActiveInputType = inputType;
            return controllerMartyr;
        }
    }
}