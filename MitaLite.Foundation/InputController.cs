// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputController
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation
{
  public static class InputController
  {
    private static PointerInputType activeInputType = PointerInputType.Mouse;

    public static CompositeInputControllerMartyr Activate(
      PointerInputType inputType)
    {
      CompositeInputControllerMartyr controllerMartyr = new CompositeInputControllerMartyr(InputController.ActiveInputType);
      switch (inputType)
      {
        case PointerInputType.Mouse:
          controllerMartyr.Add(PointerInput.Activate((IPointerInput) Mouse.Instance));
          controllerMartyr.Add(MouseWheelInput.Activate((IMouseWheelInput) Mouse.Instance));
          break;
        case PointerInputType.Pen:
          controllerMartyr.Add(PointerInput.Activate((IPointerInput) Pen.Instance));
          controllerMartyr.Add(SinglePointGesture.Activate((ISinglePointGestureInput) Pen.Instance));
          break;
        case PointerInputType.SingleTouch:
          controllerMartyr.Add(PointerInput.Activate((IPointerInput) SingleTouch.Instance));
          controllerMartyr.Add(SinglePointGesture.Activate((ISinglePointGestureInput) SingleTouch.Instance));
          break;
        case PointerInputType.MultiTouch:
          controllerMartyr.Add(PointerInput.Activate((IPointerInput) MultiTouch.Instance));
          controllerMartyr.Add(SinglePointGesture.Activate((ISinglePointGestureInput) MultiTouch.Instance));
          controllerMartyr.Add(MultiPointGesture.Activate((IMultiPointGestureInput) MultiTouch.Instance));
          break;
      }
      InputController.ActiveInputType = inputType;
      return controllerMartyr;
    }

    public static PointerInputType ActiveInputType
    {
      get => InputController.activeInputType;
      internal set
      {
        InputController.activeInputType = value;
        Log.Out("Active Input Type: {0}", (object) value);
      }
    }
  }
}
