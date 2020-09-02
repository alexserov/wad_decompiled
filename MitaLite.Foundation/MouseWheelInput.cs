// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.MouseWheelInput
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  public static class MouseWheelInput
  {
    private static Stack<IMouseWheelInput> _mouseWheelInputStack = new Stack<IMouseWheelInput>();

    public static void RotateWheel(int delta) => MouseWheelInput.Current.RotateWheel(delta);

    public static void RotateWheel(UIObject uiObject, int delta)
    {
      PointerInput.Move(uiObject);
      MouseWheelInput.RotateWheel(delta);
    }

    public static IDisposable Activate(IMouseWheelInput mouseWheel) => (IDisposable) new InputControllerMartyr<IMouseWheelInput>(MouseWheelInput._mouseWheelInputStack, mouseWheel);

    public static IMouseWheelInput Current
    {
      get
      {
        if (MouseWheelInput._mouseWheelInputStack.Count == 0)
        {
          lock (MouseWheelInput._mouseWheelInputStack)
          {
            if (MouseWheelInput._mouseWheelInputStack.Count == 0)
              MouseWheelInput._mouseWheelInputStack.Push((IMouseWheelInput) Mouse.Instance);
          }
        }
        return MouseWheelInput._mouseWheelInputStack.Peek();
      }
    }
  }
}
