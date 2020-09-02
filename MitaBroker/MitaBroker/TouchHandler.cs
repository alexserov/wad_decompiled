// Decompiled with JetBrains decompiler
// Type: MitaBroker.TouchHandler
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MS.Internal.Mita.Foundation;
using System;

namespace MitaBroker
{
  internal sealed class TouchHandler
  {
    internal static ResponseStatus SendTouchTypeClick(
      string touchType,
      UIObject clickElement)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      if (!(touchType == "click"))
      {
        if (!(touchType == "doubleclick"))
        {
          if (touchType == "longclick")
          {
            clickElement.TapAndHold();
            responseStatus = ResponseStatus.Success;
          }
        }
        else
        {
          clickElement.DoubleTap();
          responseStatus = ResponseStatus.Success;
        }
      }
      else
      {
        clickElement.Tap();
        responseStatus = ResponseStatus.Success;
      }
      return responseStatus;
    }

    internal static ResponseStatus SendTouchTypePress(string touchType, int x, int y)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      PointI point = new PointI(x, y);
      if (!(touchType == "down"))
      {
        if (!(touchType == "move"))
        {
          if (touchType == "up")
          {
            using (InputController.Activate(PointerInputType.MultiTouch))
            {
              PointerInput.Move(point);
              PointerInput.Release(PointerButtons.Primary);
            }
            responseStatus = ResponseStatus.Success;
          }
        }
        else
        {
          using (InputController.Activate(PointerInputType.MultiTouch))
            PointerInput.Move(point);
          responseStatus = ResponseStatus.Success;
        }
      }
      else
      {
        using (InputController.Activate(PointerInputType.MultiTouch))
        {
          PointerInput.Move(point);
          PointerInput.Press(PointerButtons.Primary);
        }
        responseStatus = ResponseStatus.Success;
      }
      return responseStatus;
    }

    internal static ResponseStatus SendTouchTypeScroll(
      int xOffset,
      int yOffset,
      UIObject scrollElement)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      try
      {
        PointI relativeClickablePoint = TouchHandler.GetRelativeClickablePoint(scrollElement);
        PointI endPoint = new PointI(relativeClickablePoint.X + xOffset, relativeClickablePoint.Y + yOffset);
        scrollElement.Pan(relativeClickablePoint, endPoint);
        responseStatus = ResponseStatus.Success;
      }
      catch
      {
      }
      return responseStatus;
    }

    internal static ResponseStatus SendTouchFlick(
      int flickXOffset,
      int flickYOffset,
      UIObject flickElement)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      try
      {
        PointI relativeClickablePoint = TouchHandler.GetRelativeClickablePoint(flickElement);
        PointI endPoint = new PointI(relativeClickablePoint.X + flickXOffset, relativeClickablePoint.Y + flickYOffset);
        flickElement.Flick(relativeClickablePoint, endPoint);
        responseStatus = ResponseStatus.Success;
      }
      catch
      {
      }
      return responseStatus;
    }

    internal static ResponseStatus SendTouchFlick(
      UIObject mainElement,
      int xSpeed,
      int ySpeed)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      try
      {
        mainElement.SetFocus();
        mainElement.Flick(TouchHandler.GetRelativeClickablePoint(mainElement), (int) Math.Max(Math.Sqrt((double) (xSpeed * xSpeed + ySpeed * ySpeed)), (double) SinglePointGesture.MinimumPanFlickDistance), (float) (Math.Atan2((double) ySpeed, (double) xSpeed) * (180.0 / Math.PI)));
        responseStatus = ResponseStatus.Success;
      }
      catch
      {
      }
      return responseStatus;
    }

    private static PointI GetRelativeClickablePoint(UIObject element)
    {
      RectangleI boundingRectangle = element.BoundingRectangle;
      int left = boundingRectangle.Left;
      boundingRectangle = element.BoundingRectangle;
      int right = boundingRectangle.Right;
      int num1 = (left + right) / 2;
      boundingRectangle = element.BoundingRectangle;
      int x1 = boundingRectangle.TopLeft.X;
      int x2 = num1 - x1;
      boundingRectangle = element.BoundingRectangle;
      int top = boundingRectangle.Top;
      boundingRectangle = element.BoundingRectangle;
      int bottom = boundingRectangle.Bottom;
      int num2 = (top + bottom) / 2;
      boundingRectangle = element.BoundingRectangle;
      int y1 = boundingRectangle.TopLeft.Y;
      int y2 = num2 - y1;
      return new PointI(x2, y2);
    }
  }
}
