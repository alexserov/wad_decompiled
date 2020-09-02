// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.MouseWheelInput
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    public static class MouseWheelInput {
        static readonly Stack<IMouseWheelInput> _mouseWheelInputStack = new Stack<IMouseWheelInput>();

        public static IMouseWheelInput Current {
            get {
                if (_mouseWheelInputStack.Count == 0)
                    lock (_mouseWheelInputStack) {
                        if (_mouseWheelInputStack.Count == 0)
                            _mouseWheelInputStack.Push(item: Mouse.Instance);
                    }

                return _mouseWheelInputStack.Peek();
            }
        }

        public static void RotateWheel(int delta) {
            Current.RotateWheel(delta: delta);
        }

        public static void RotateWheel(UIObject uiObject, int delta) {
            PointerInput.Move(uiObject: uiObject);
            RotateWheel(delta: delta);
        }

        public static IDisposable Activate(IMouseWheelInput mouseWheel) {
            return new InputControllerMartyr<IMouseWheelInput>(inputStack: _mouseWheelInputStack, inputController: mouseWheel);
        }
    }
}