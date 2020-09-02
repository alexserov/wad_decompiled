// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.SinglePointGesture
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  public static class SinglePointGesture
  {
    private static Stack<ISinglePointGestureInput> _gestureInputStack = new Stack<ISinglePointGestureInput>();
    public static readonly float DefaultFlickAcceleration = 2f;
    public static readonly float DefaultPanAcceleration = 0.1f;
    public static readonly uint DefaultDragDuration = 900;
    public static readonly uint DefaultHoldDuration = 1500;
    private static readonly int minimumPanFlickDistance = 160;

    public static void Flick(PointI endPoint, uint holdDuration, float acceleration) => SinglePointGesture.Current.Flick(endPoint, holdDuration, acceleration);

    public static void Pan(PointI endPoint, uint holdDuration, float acceleration) => SinglePointGesture.Current.Pan(endPoint, holdDuration, acceleration);

    public static void PressAndDrag(PointI endPoint, uint dragDuration) => SinglePointGesture.Current.PressAndDrag(endPoint, dragDuration);

    public static void PressAndDrag(PointI endPoint, uint dragDuration, uint pressDuration) => SinglePointGesture.Current.PressAndDrag(endPoint, dragDuration, pressDuration);

    public static void PressAndHold(uint holdDuration) => SinglePointGesture.Current.PressAndHold(holdDuration);

    public static void Flick(UIObject targetObject, UIObject destinationObject) => SinglePointGesture.Flick(targetObject, destinationObject, SinglePointGesture.DefaultFlickAcceleration);

    public static void Flick(UIObject targetObject, UIObject destinationObject, float acceleration)
    {
      PointI clickablePoint1 = targetObject.GetClickablePoint();
      PointI clickablePoint2 = destinationObject.GetClickablePoint();
      SinglePointGesture.Current.Move(clickablePoint1);
      int defaultPressDuration = (int) InputManager.DefaultPressDuration;
      double num = (double) acceleration;
      SinglePointGesture.Flick(clickablePoint2, (uint) defaultPressDuration, (float) num);
    }

    public static void Flick(
      UIObject uiObject,
      float acceleration,
      uint distance,
      float direction)
    {
      PointI clickablePoint = uiObject.GetClickablePoint();
      double num1 = (double) direction * Math.PI / 180.0;
      PointI endPoint = new PointI(clickablePoint.X + (int) Math.Round((double) distance * Math.Cos(num1)), clickablePoint.Y - (int) Math.Round((double) distance * Math.Sin(num1)));
      SinglePointGesture.Current.Move(clickablePoint);
      int defaultPressDuration = (int) InputManager.DefaultPressDuration;
      double num2 = (double) acceleration;
      SinglePointGesture.Flick(endPoint, (uint) defaultPressDuration, (float) num2);
    }

    public static void Pan(UIObject targetObject, UIObject destinationObject) => SinglePointGesture.Pan(targetObject, destinationObject, SinglePointGesture.DefaultPanAcceleration);

    public static void Pan(UIObject targetObject, UIObject destinationObject, float acceleration)
    {
      PointI clickablePoint1 = targetObject.GetClickablePoint();
      PointI clickablePoint2 = destinationObject.GetClickablePoint();
      SinglePointGesture.Current.Move(clickablePoint1);
      int defaultPressDuration = (int) InputManager.DefaultPressDuration;
      double num = (double) acceleration;
      SinglePointGesture.Pan(clickablePoint2, (uint) defaultPressDuration, (float) num);
    }

    public static void Pan(UIObject uiObject, float acceleration, uint distance, float direction)
    {
      PointI clickablePoint = uiObject.GetClickablePoint();
      double num1 = (double) direction * Math.PI / 180.0;
      PointI endPoint = new PointI(clickablePoint.X + (int) Math.Round((double) distance * Math.Cos(num1)), clickablePoint.Y - (int) Math.Round((double) distance * Math.Sin(num1)));
      SinglePointGesture.Current.Move(clickablePoint);
      int defaultPressDuration = (int) InputManager.DefaultPressDuration;
      double num2 = (double) acceleration;
      SinglePointGesture.Pan(endPoint, (uint) defaultPressDuration, (float) num2);
    }

    public static void PressAndDrag(UIObject targetObject, UIObject destinationObject) => SinglePointGesture.PressAndDrag(targetObject, destinationObject, SinglePointGesture.DefaultDragDuration);

    public static void PressAndDrag(
      UIObject targetObject,
      UIObject destinationObject,
      uint dragDuration,
      uint pressDuration)
    {
      PointI clickablePoint1 = targetObject.GetClickablePoint();
      PointI clickablePoint2 = destinationObject.GetClickablePoint();
      SinglePointGesture.Current.Move(clickablePoint1);
      int num1 = (int) dragDuration;
      int num2 = (int) pressDuration;
      SinglePointGesture.PressAndDrag(clickablePoint2, (uint) num1, (uint) num2);
    }

    public static void PressAndDrag(
      UIObject targetObject,
      UIObject destinationObject,
      uint dragDuration)
    {
      PointI clickablePoint1 = targetObject.GetClickablePoint();
      PointI clickablePoint2 = destinationObject.GetClickablePoint();
      SinglePointGesture.Current.Move(clickablePoint1);
      int num = (int) dragDuration;
      SinglePointGesture.PressAndDrag(clickablePoint2, (uint) num);
    }

    public static void PressAndDrag(UIObject uiObject, uint distance, float direction)
    {
      PointI clickablePoint = uiObject.GetClickablePoint();
      double num = (double) direction * Math.PI / 180.0;
      PointI endPoint = new PointI(clickablePoint.X + (int) Math.Round((double) distance * Math.Cos(num)), clickablePoint.Y - (int) Math.Round((double) distance * Math.Sin(num)));
      SinglePointGesture.Current.Move(clickablePoint);
      int defaultDragDuration = (int) SinglePointGesture.DefaultDragDuration;
      SinglePointGesture.PressAndDrag(endPoint, (uint) defaultDragDuration);
    }

    public static void PressAndHold(UIObject uiObject) => SinglePointGesture.PressAndHold(uiObject, SinglePointGesture.DefaultHoldDuration);

    public static void PressAndHold(UIObject uiObject, uint holdDuration)
    {
      SinglePointGesture.Current.Move(uiObject.GetClickablePoint());
      SinglePointGesture.PressAndHold(holdDuration);
    }

    public static IDisposable Activate(ISinglePointGestureInput pointer) => (IDisposable) new InputControllerMartyr<ISinglePointGestureInput>(SinglePointGesture._gestureInputStack, pointer);

    public static ISinglePointGestureInput Current
    {
      get
      {
        if (SinglePointGesture._gestureInputStack.Count == 0)
        {
          lock (SinglePointGesture._gestureInputStack)
          {
            if (SinglePointGesture._gestureInputStack.Count == 0)
              SinglePointGesture._gestureInputStack.Push((ISinglePointGestureInput) SingleTouch.Instance);
          }
        }
        return SinglePointGesture._gestureInputStack.Peek();
      }
    }

    public static int MinimumPanFlickDistance => SinglePointGesture.minimumPanFlickDistance;
  }
}
