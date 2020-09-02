// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.PointerInput
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  public static class PointerInput
  {
    public static readonly uint DefaultDragDuration = 900;
    private static Stack<IPointerInput> _pointerInputStack = new Stack<IPointerInput>();

    public static void Click(PointerButtons button, int count) => PointerInput.Current.Click(button, count);

    public static void ClickDrag(PointI endPoint, PointerButtons button, uint dragDuration) => PointerInput.Current.ClickDrag(endPoint, button, dragDuration);

    public static void Move(PointI point) => PointerInput.Current.Move(point);

    public static void Press(PointerButtons button) => PointerInput.Current.Press(button);

    public static void Release(PointerButtons button) => PointerInput.Current.Release(button);

    public static void InjectPointers(PointerData[] pointerDataArray) => PointerInput.Current.InjectPointers(pointerDataArray);

    public static PointI Location => PointerInput.Current.Location;

    public static void Click(UIObject uiObject) => PointerInput.Click(uiObject, PointerButtons.Primary);

    public static void Click(UIObject uiObject, PointerButtons button) => PointerInput.ClickImplementation(uiObject, button, 1U);

    public static void Click(
      UIObject uiObject,
      PointerButtons button,
      double offsetX,
      double offsetY) => PointerInput.ClickImplementation(uiObject, button, 1U, offsetX, offsetY);

    public static void DoubleClick(UIObject uiObject) => PointerInput.DoubleClick(uiObject, PointerButtons.Primary);

    public static void DoubleClick(UIObject uiObject, PointerButtons button) => PointerInput.ClickImplementation(uiObject, button, 2U);

    public static void DoubleClick(
      UIObject uiObject,
      PointerButtons button,
      double offsetX,
      double offsetY) => PointerInput.ClickImplementation(uiObject, button, 2U, offsetX, offsetY);

    public static void ClickDrag(UIObject targetObject, UIObject destinationObject) => PointerInput.ClickDrag(targetObject, destinationObject, PointerButtons.Primary);

    public static void ClickDrag(
      UIObject targetObject,
      UIObject destinationObject,
      PointerButtons button)
    {
      PointerInput.Move(targetObject);
      PointerInput.ClickDrag(destinationObject.GetClickablePoint(), button, PointerInput.DefaultDragDuration);
    }

    public static void Move(UIObject uiObject) => PointerInput.Move(uiObject.GetClickablePoint());

    public static void Move(UIObject uiObject, double offsetX, double offsetY)
    {
      PointI location = uiObject.BoundingRectangle.Location;
      PointerInput.Move(new PointI((int) ((double) location.X + offsetX), (int) ((double) location.Y + offsetY)));
    }

    public static IDisposable Activate(IPointerInput pointer) => (IDisposable) new InputControllerMartyr<IPointerInput>(PointerInput._pointerInputStack, pointer);

    public static IPointerInput Current
    {
      get
      {
        if (PointerInput._pointerInputStack.Count == 0)
        {
          lock (PointerInput._pointerInputStack)
          {
            if (PointerInput._pointerInputStack.Count == 0)
              PointerInput._pointerInputStack.Push((IPointerInput) Mouse.Instance);
          }
        }
        return PointerInput._pointerInputStack.Peek();
      }
    }

    private static void ClickImplementation(
      UIObject uiObject,
      PointerButtons button,
      uint numClicks,
      double offsetX,
      double offsetY)
    {
      PointerInput.Move(uiObject, offsetX, offsetY);
      PointerInput.Click(button, (int) numClicks);
    }

    private static void ClickImplementation(
      UIObject uiObject,
      PointerButtons button,
      uint numClicks)
    {
      PointerInput.Move(uiObject);
      PointerInput.Click(button, (int) numClicks);
    }
  }
}
