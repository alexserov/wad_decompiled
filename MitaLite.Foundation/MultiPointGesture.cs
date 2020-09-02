// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.MultiPointGesture
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  public static class MultiPointGesture
  {
    private static Stack<IMultiPointGestureInput> _gestureInputStack = new Stack<IMultiPointGestureInput>();
    public static readonly uint DefaultHoldDuration = 500;
    public static readonly uint DefaultTapDelta = 500;
    public static readonly uint DefaultTapDistance = 10;
    public static readonly float DefaultTwoPointPanAcceleration = 0.1f;
    public static readonly uint DefaultTwoPointPanDistance = 10;
    public static readonly uint DefaultPinchStretchDistance = 150;
    public static readonly float DefaultPinchStretchDirection = 90f;
    public static readonly uint DefaultPinchStretchDuration = 1000;
    public static readonly uint DefaultRotateDistance = 100;
    public static readonly uint DefaultRotateDuration = 1000;

    public static void PressAndTap(uint tapCount, uint tapDuration, uint tapDelta, uint distance) => MultiPointGesture.Current.PressAndTap(tapCount, tapDuration, tapDelta, distance);

    public static void TwoPointPressAndHold(
      uint tapCount,
      uint holdDuration,
      uint tapDelta,
      uint distance) => MultiPointGesture.Current.TwoPointPressAndHold(tapCount, holdDuration, tapDelta, distance);

    public static void TwoPointPan(
      PointI endPoint,
      uint holdDuration,
      float acceleration,
      uint distance) => MultiPointGesture.Current.TwoPointPan(endPoint, holdDuration, acceleration, distance);

    public static void Pinch(float direction, uint duration, uint startDistance, uint endDistance) => MultiPointGesture.Current.Pinch(direction, duration, startDistance, endDistance);

    public static void Pinch(
      float direction,
      uint duration,
      uint startDistance,
      uint endDistance,
      bool pivot) => MultiPointGesture.Current.Pinch(direction, duration, startDistance, endDistance, pivot);

    public static void Stretch(
      float direction,
      uint duration,
      uint startDistance,
      uint endDistance) => MultiPointGesture.Current.Stretch(direction, duration, startDistance, endDistance);

    public static void Stretch(
      float direction,
      uint duration,
      uint startDistance,
      uint endDistance,
      bool pivot) => MultiPointGesture.Current.Stretch(direction, duration, startDistance, endDistance, pivot);

    public static void Rotate(float angle, uint duration, bool centerOnPoint, uint distance) => MultiPointGesture.Current.Rotate(angle, duration, centerOnPoint, distance);

    public static void InjectMultiPointGesture(MultiTouchInjectionData[] injectionData) => MultiPointGesture.Current.InjectMultiPointGesture(injectionData);

    public static void PressAndTap(UIObject uiObject)
    {
      MultiPointGesture.Current.Move(uiObject.GetClickablePoint());
      MultiPointGesture.PressAndTap(1U, MultiPointGesture.DefaultHoldDuration, MultiPointGesture.DefaultTapDelta, MultiPointGesture.DefaultTapDistance);
    }

    public static void TwoPointPressAndHold(UIObject uiObject) => MultiPointGesture.TwoPointPressAndHold(uiObject, 1U);

    public static void TwoPointPressAndHold(UIObject uiObject, uint count)
    {
      MultiPointGesture.Current.Move(uiObject.GetClickablePoint());
      MultiPointGesture.TwoPointPressAndHold(count, InputManager.DefaultPressDuration, InputManager.DefaultTapDelta, MultiPointGesture.DefaultTapDistance);
    }

    public static void TwoPointPan(UIObject targetObject, UIObject destinationObject) => MultiPointGesture.TwoPointPan(targetObject, destinationObject, MultiPointGesture.DefaultTwoPointPanAcceleration);

    public static void TwoPointPan(
      UIObject targetObject,
      UIObject destinationObject,
      float acceleration)
    {
      PointI clickablePoint1 = targetObject.GetClickablePoint();
      PointI clickablePoint2 = destinationObject.GetClickablePoint();
      MultiPointGesture.Current.Move(clickablePoint1);
      int defaultPressDuration = (int) InputManager.DefaultPressDuration;
      double num = (double) acceleration;
      int pointPanDistance = (int) MultiPointGesture.DefaultTwoPointPanDistance;
      MultiPointGesture.TwoPointPan(clickablePoint2, (uint) defaultPressDuration, (float) num, (uint) pointPanDistance);
    }

    public static void TwoPointPan(
      UIObject uiObject,
      float acceleration,
      uint distance,
      float direction)
    {
      PointI clickablePoint = uiObject.GetClickablePoint();
      double num1 = (double) direction * Math.PI / 180.0;
      PointI endPoint = new PointI(clickablePoint.X + (int) Math.Round((double) distance * Math.Cos(num1)), clickablePoint.Y - (int) Math.Round((double) distance * Math.Sin(num1)));
      MultiPointGesture.Current.Move(clickablePoint);
      int defaultPressDuration = (int) InputManager.DefaultPressDuration;
      double num2 = (double) acceleration;
      int num3 = (int) distance;
      MultiPointGesture.TwoPointPan(endPoint, (uint) defaultPressDuration, (float) num2, (uint) num3);
    }

    public static void Pinch(UIObject uiObject) => MultiPointGesture.Pinch(uiObject, MultiPointGesture.DefaultPinchStretchDistance);

    public static void Pinch(UIObject uiObject, uint distance) => MultiPointGesture.Pinch(uiObject, distance, MultiPointGesture.DefaultPinchStretchDirection);

    public static void Pinch(UIObject uiObject, uint distance, float direction)
    {
      MultiPointGesture.Current.Move(MultiPointGesture.OffsetPinchPoints(uiObject, distance, direction));
      MultiPointGesture.Pinch(direction, MultiPointGesture.DefaultPinchStretchDuration, distance, 0U);
    }

    public static void Pinch(UIObject uiObject, uint distance, float direction, bool pivot)
    {
      PointI point = uiObject.GetClickablePoint();
      if (!pivot)
        point = MultiPointGesture.OffsetPinchPoints(uiObject, distance, direction);
      MultiPointGesture.Current.Move(point);
      MultiPointGesture.Pinch(direction, MultiPointGesture.DefaultPinchStretchDuration, distance, 0U, pivot);
    }

    public static void Stretch(UIObject uiObject) => MultiPointGesture.Stretch(uiObject, MultiPointGesture.DefaultPinchStretchDistance);

    public static void Stretch(UIObject uiObject, uint distance) => MultiPointGesture.Stretch(uiObject, distance, MultiPointGesture.DefaultPinchStretchDirection);

    public static void Stretch(UIObject uiObject, uint distance, float direction)
    {
      MultiPointGesture.Current.Move(uiObject.GetClickablePoint());
      MultiPointGesture.Stretch(direction, MultiPointGesture.DefaultPinchStretchDuration, 0U, distance);
    }

    public static void Stretch(UIObject uiObject, uint distance, float direction, bool pivot)
    {
      MultiPointGesture.Current.Move(uiObject.GetClickablePoint());
      MultiPointGesture.Stretch(direction, MultiPointGesture.DefaultPinchStretchDuration, 0U, distance, pivot);
    }

    public static void Rotate(UIObject uiObject, float angle) => MultiPointGesture.Rotate(uiObject, angle, false);

    public static void Rotate(UIObject uiObject, uint distance, float angle) => MultiPointGesture.Rotate(uiObject, distance, angle, false);

    public static void Rotate(UIObject uiObject, float angle, bool centerOnPoint) => MultiPointGesture.Rotate(uiObject, MultiPointGesture.DefaultRotateDistance, angle, centerOnPoint);

    public static void Rotate(UIObject uiObject, uint distance, float angle, bool centerOnPoint)
    {
      PointI point = uiObject.GetClickablePoint();
      if (!centerOnPoint)
        point = MultiPointGesture.OffsetPinchPoints(uiObject, distance, 0.0f);
      MultiPointGesture.Current.Move(point);
      MultiPointGesture.Rotate(angle, MultiPointGesture.DefaultRotateDuration, centerOnPoint, distance);
    }

    public static IDisposable Activate(IMultiPointGestureInput pointer) => (IDisposable) new InputControllerMartyr<IMultiPointGestureInput>(MultiPointGesture._gestureInputStack, pointer);

    public static IMultiPointGestureInput Current
    {
      get
      {
        if (MultiPointGesture._gestureInputStack.Count == 0)
        {
          lock (MultiPointGesture._gestureInputStack)
          {
            if (MultiPointGesture._gestureInputStack.Count == 0)
              MultiPointGesture._gestureInputStack.Push((IMultiPointGestureInput) MultiTouch.Instance);
          }
        }
        return MultiPointGesture._gestureInputStack.Peek();
      }
    }

    internal static PointI OffsetPinchPoints(
      UIObject uiObject,
      uint distance,
      float direction) => MultiPointGesture.OffsetPinchPoints(uiObject.GetClickablePoint(), uiObject.BoundingRectangle, distance, direction);

    internal static PointI OffsetPinchPoints(
      PointI targetPoint,
      RectangleI boundingRectangle,
      uint distance,
      float direction)
    {
      double num1 = (double) distance / 2.0;
      double num2 = (double) direction * Math.PI / 180.0;
      int offsetX = (int) Math.Round(num1 * Math.Cos(num2));
      int offsetY = (int) Math.Round(num1 * Math.Sin(num2));
      if (targetPoint.X + offsetX > boundingRectangle.Right || targetPoint.X + offsetX < boundingRectangle.Left || (targetPoint.Y + offsetY > boundingRectangle.Bottom || targetPoint.Y + offsetY < boundingRectangle.Top))
        Log.Out("Target points are outside of target object");
      targetPoint.Offset(offsetX, offsetY);
      return targetPoint;
    }
  }
}
