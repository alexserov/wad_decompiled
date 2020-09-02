// Decompiled with JetBrains decompiler
// Type: MitaBroker.MouseHandler
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MS.Internal.Mita.Foundation;
using System;

namespace MitaBroker
{
  public sealed class MouseHandler
  {
    internal static ResponseStatus SendMouseAction(
      string actionType,
      int buttonNumber)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      PointerButtons button;
      switch (buttonNumber)
      {
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
          throw new ArgumentException(string.Format("Bad mouse button value: {0}. Valid values are LEFT = 0, MIDDLE = 1, RIGHT = 2", (object) buttonNumber));
      }
      if (!(actionType == "buttondown"))
      {
        if (!(actionType == "buttonup"))
        {
          if (actionType == "click")
          {
            using (InputController.Activate(PointerInputType.Mouse))
              PointerInput.Click(button, 1);
            responseStatus = ResponseStatus.Success;
          }
        }
        else
        {
          using (InputController.Activate(PointerInputType.Mouse))
            PointerInput.Release(button);
          responseStatus = ResponseStatus.Success;
        }
      }
      else
      {
        using (InputController.Activate(PointerInputType.Mouse))
          PointerInput.Press(button);
        responseStatus = ResponseStatus.Success;
      }
      return responseStatus;
    }

    internal static ResponseStatus SendMouseDoubleClick()
    {
      using (InputController.Activate(PointerInputType.Mouse))
        PointerInput.Click(PointerButtons.Primary, 2);
      return ResponseStatus.Success;
    }

    internal static ResponseStatus SendMouseMoveToElementCenter(
      string mouseMoveType,
      UIObject element)
    {
      PointI rectangleCenterPosition = element.GetAdjustedBoundingRectangleCenterPosition();
      using (InputController.Activate(PointerInputType.Mouse))
      {
        PointerInput.Move(rectangleCenterPosition);
        return ResponseStatus.Success;
      }
    }

    internal static ResponseStatus SendMouseMoveToElementRelative(
      string mouseMoveType,
      UIObject element,
      int xOffset,
      int yOffset)
    {
      PointI topLeft = element.GetAdjustedBoundingRectangle().TopLeft;
      topLeft.X += xOffset;
      topLeft.Y += yOffset;
      using (InputController.Activate(PointerInputType.Mouse))
      {
        PointerInput.Move(topLeft);
        return ResponseStatus.Success;
      }
    }

    internal static ResponseStatus SendMouseMoveToRelative(
      string mouseMoveType,
      int xOffset,
      int yOffset)
    {
      using (InputController.Activate(PointerInputType.Mouse))
      {
        PointI location = PointerInput.Location;
        location.X += xOffset;
        location.Y += yOffset;
        PointerInput.Move(location);
        return ResponseStatus.Success;
      }
    }
  }
}
