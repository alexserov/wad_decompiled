// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputAlgorithms
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  internal class InputAlgorithms : IInputAlgorithms
  {
    private const int afterMoveActionWaitDuration = 800;
    private const int afterTapActionWaitDuration = 200;

    public IList<IInputAction> DynamicPress(PointI point, uint contactId)
    {
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      this.Press(new PointI?(point), injectList, contactId);
      return injectList;
    }

    public IInputAction DynamicMove(PointI point, uint contactId)
    {
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      this.Update(point, injectList, contactId);
      return injectList[0];
    }

    public IInputAction DynamicMoves(IList<PointI> points, IList<uint> contactIds)
    {
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      this.MultiUpdate(points, injectList, contactIds);
      return injectList[0];
    }

    public IList<IInputAction> DynamicRelease(PointI point, uint contactId)
    {
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      this.Release(new PointI?(point), injectList, contactId);
      return injectList;
    }

    public IList<IInputAction> DynamicPointerActions(PointerData[] pointerDataArray)
    {
      List<IInputAction> inputActionList = new List<IInputAction>();
      inputActionList.Add((IInputAction) new MultiPointerInputAction()
      {
        pointerData = pointerDataArray
      });
      return (IList<IInputAction>) inputActionList;
    }

    public IList<IInputAction> DynamicDrag(
      PointI start,
      PointI end,
      uint maxDuration,
      uint packetDelta,
      uint contactId)
    {
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      uint num1 = maxDuration / packetDelta;
      int num2 = end.X - start.X;
      int num3 = end.Y - start.Y;
      uint num4 = 1U + (uint) Math.Sqrt((double) (num2 * num2 + num3 * num3)) / 50U;
      if (num4 > num1)
        num4 = num1;
      PointI location = start;
      int elapsedTime = 0;
      double num5 = (double) (end.X - start.X) / (double) num4;
      double num6 = (double) (end.Y - start.Y) / (double) num4;
      for (int index = 0; (long) index < (long) (num4 - 1U); ++index)
      {
        location.X = start.X + index * (int) num5;
        location.Y = start.Y + index * (int) num6;
        this.Update(location, injectList, contactId);
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      }
      this.Update(end, injectList, contactId);
      return injectList;
    }

    public IList<IInputAction> PressAndHold(
      PointI point,
      uint holdDuration,
      uint tapCount,
      uint tapDelta,
      uint packetDelta)
    {
      if (packetDelta < 10U)
        throw new ArgumentOutOfRangeException("packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
      if (holdDuration < packetDelta)
        throw new ArgumentOutOfRangeException("holdDuration must be greater than or equal to packetDelta");
      if (tapCount > 1U && tapDelta < packetDelta)
        throw new ArgumentOutOfRangeException("tapDelta must be greater than or equal to packetDelta");
      int elapsedTime = 0;
      int num = (int) Math.Ceiling((double) holdDuration / (double) packetDelta);
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      for (int index1 = 0; (long) index1 < (long) tapCount; ++index1)
      {
        if (index1 > 0)
          this.Wait(ref elapsedTime, (int) tapDelta, injectList);
        this.Press(new PointI?(point), injectList);
        for (int index2 = 0; index2 < num; ++index2)
        {
          this.Wait(ref elapsedTime, (int) packetDelta, injectList);
          this.Update(point, injectList);
        }
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
        this.Release(new PointI?(point), injectList);
      }
      injectList.Add(Input.CreateWait(200));
      return injectList;
    }

    public IList<IInputAction> PressAndHoldAndDrag(
      PointI start,
      PointI end,
      uint dragDuration,
      uint holdDuration,
      uint packetDelta)
    {
      if (packetDelta < 10U)
        throw new ArgumentOutOfRangeException("packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
      if (dragDuration < packetDelta)
        throw new ArgumentOutOfRangeException("dragDuration must be greater than or equal to packetDelta");
      if (holdDuration < packetDelta)
        throw new ArgumentOutOfRangeException("holdDuration must be greater than or equal to packetDelta");
      int elapsedTime = 0;
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      this.Press(new PointI?(start), injectList);
      int num1 = (int) Math.Ceiling((double) holdDuration / (double) packetDelta);
      for (int index = 0; index < num1; ++index)
      {
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
        this.Update(start, injectList);
      }
      int num2 = (int) Math.Ceiling((double) dragDuration / (double) packetDelta);
      PointI location = start;
      double num3 = (double) (end.X - start.X) / (double) dragDuration;
      double num4 = (double) (end.Y - start.Y) / (double) dragDuration;
      int num5 = (int) packetDelta;
      for (int index = 0; index < num2 - 1; ++index)
      {
        location.X = start.X + (int) Math.Round(num3 * (double) num5);
        location.Y = start.Y + (int) Math.Round(num4 * (double) num5);
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
        this.Update(location, injectList);
        num5 += (int) packetDelta;
      }
      this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      this.Update(end, injectList);
      this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      this.Release(new PointI?(end), injectList);
      injectList.Add(Input.CreateWait(800));
      return injectList;
    }

    public IList<IInputAction> PressAndHoldAndDragWithAcceleration(
      PointI start,
      PointI end,
      uint holdDuration,
      float acceleration,
      uint packetDelta)
    {
      if (packetDelta < 10U)
        throw new ArgumentOutOfRangeException("packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
      if (holdDuration < packetDelta)
        throw new ArgumentOutOfRangeException("holdDuration must be greater than or equal to packetDelta");
      if ((double) acceleration <= 0.0)
        throw new ArgumentOutOfRangeException("acceleration must be greater than 0.0");
      int elapsedTime = 0;
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      this.Press(new PointI?(start), injectList);
      int num1 = (int) Math.Ceiling((double) holdDuration / (double) packetDelta);
      for (int index = 0; index < num1; ++index)
      {
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
        this.Update(start, injectList);
      }
      int num2 = end.X - start.X;
      int num3 = end.Y - start.Y;
      int num4 = (int) Math.Ceiling(Math.Sqrt(2.0 * Math.Sqrt((double) (num2 * num2 + num3 * num3)) / (double) acceleration) / (double) packetDelta);
      PointI location = start;
      double num5 = Math.Atan2((double) num3, (double) num2);
      double num6 = (double) acceleration * Math.Cos(num5);
      double num7 = (double) acceleration * Math.Sin(num5);
      int num8 = (int) packetDelta;
      for (int index = 0; index < num4 - 1; ++index)
      {
        location.X = start.X + (int) Math.Round(0.5 * num6 * (double) num8 * (double) num8);
        location.Y = start.Y + (int) Math.Round(0.5 * num7 * (double) num8 * (double) num8);
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
        this.Update(location, injectList);
        num8 += (int) packetDelta;
      }
      this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      this.Update(end, injectList);
      this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      this.Release(new PointI?(end), injectList);
      injectList.Add(Input.CreateWait(800));
      return injectList;
    }

    public IList<IInputAction> TwoFingerPressAndHold(
      PointI pointOne,
      PointI pointTwo,
      uint holdDuration,
      uint tapCount,
      uint tapDelta,
      uint packetDelta)
    {
      if (packetDelta < 10U)
        throw new ArgumentOutOfRangeException("packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
      if (holdDuration < packetDelta)
        throw new ArgumentOutOfRangeException("holdDuration must be greater than or equal to packetDelta");
      if (tapCount > 1U && tapDelta < packetDelta)
        throw new ArgumentOutOfRangeException("tapDelta must be greater than or equal to packetDelta");
      int elapsedTime = 0;
      int num = (int) Math.Ceiling((double) holdDuration / (double) packetDelta);
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      for (int index1 = 0; (long) index1 < (long) tapCount; ++index1)
      {
        if (index1 > 0)
          this.Wait(ref elapsedTime, (int) tapDelta, injectList);
        this.MultiPress((IList<PointI>) new List<PointI>()
        {
          pointOne,
          pointTwo
        }, injectList);
        for (int index2 = 0; index2 < num; ++index2)
        {
          this.Wait(ref elapsedTime, (int) packetDelta, injectList);
          this.MultiUpdate((IList<PointI>) new List<PointI>()
          {
            pointOne,
            pointTwo
          }, injectList);
        }
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
        this.MultiRelease((IList<PointI>) new List<PointI>()
        {
          pointOne,
          pointTwo
        }, injectList);
      }
      injectList.Add(Input.CreateWait(200));
      return injectList;
    }

    public IList<IInputAction> TwoFingerZoom(
      PointI pointOne,
      PointI pointTwo,
      float direction,
      uint duration,
      uint distance,
      bool pivotZoom,
      uint packetDelta)
    {
      if (packetDelta < 10U)
        throw new ArgumentOutOfRangeException("packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
      if (duration < packetDelta)
        throw new ArgumentOutOfRangeException("duration must be greater than or equal to packetDelta");
      if ((double) direction > 360.0 || (double) direction < 0.0)
        throw new ArgumentOutOfRangeException("direction must be greater than or equal to 0.0 and less than or equal to 360.0");
      if (distance < 25U)
        throw new ArgumentOutOfRangeException("distance must be greater than or equal to 25");
      int elapsedTime = 0;
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      this.MultiPress((IList<PointI>) new List<PointI>()
      {
        pointOne,
        pointTwo
      }, injectList);
      int num1 = (int) Math.Ceiling((double) duration / (double) packetDelta);
      double num2 = (double) direction * Math.PI / 180.0;
      PointI pointI1 = pointOne;
      PointI pointI2 = pointTwo;
      PointI pointI3 = pointOne;
      PointI pointI4 = pointTwo;
      double a1 = (double) distance * Math.Cos(num2);
      double a2 = (double) distance * Math.Sin(num2);
      double a3 = -a1;
      double a4 = -a2;
      double num3 = a1 / (double) duration;
      double num4 = a2 / (double) duration;
      double num5 = -num3;
      double num6 = -num4;
      int num7 = (int) packetDelta;
      for (int index = 0; index < num1 - 1; ++index)
      {
        if (!pivotZoom)
        {
          pointI1.X = pointOne.X + (int) Math.Round(num3 * (double) num7);
          pointI1.Y = pointOne.Y + (int) Math.Round(num4 * (double) num7);
        }
        pointI2.X = pointTwo.X + (int) Math.Round(num5 * (double) num7);
        pointI2.Y = pointTwo.Y + (int) Math.Round(num6 * (double) num7);
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
        this.MultiUpdate((IList<PointI>) new List<PointI>()
        {
          pointI1,
          pointI2
        }, injectList);
        num7 += (int) packetDelta;
      }
      if (!pivotZoom)
        pointI3.Offset((int) Math.Round(a1), (int) Math.Round(a2));
      pointI4.Offset((int) Math.Round(a3), (int) Math.Round(a4));
      this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      this.MultiUpdate((IList<PointI>) new List<PointI>()
      {
        pointI3,
        pointI4
      }, injectList);
      this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      this.MultiRelease((IList<PointI>) new List<PointI>()
      {
        pointI3,
        pointI4
      }, injectList);
      injectList.Add(Input.CreateWait(800));
      return injectList;
    }

    public IList<IInputAction> TwoFingerPressAndHoldAndDragWithAcceleration(
      PointI pointOne,
      PointI pointTwo,
      float direction,
      uint distance,
      uint holdDuration,
      float acceleration,
      uint packetDelta)
    {
      if (packetDelta < 10U)
        throw new ArgumentOutOfRangeException("packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
      if (holdDuration < packetDelta)
        throw new ArgumentOutOfRangeException("duration must be greater than or equal to packetDelta");
      if ((double) acceleration <= 0.0)
        throw new ArgumentOutOfRangeException("acceleration must be greater than 0.0");
      if ((double) direction > 360.0 || (double) direction < 0.0)
        throw new ArgumentOutOfRangeException("direction must be greater than or equal to 0.0 and less than or equal to 360.0");
      if (distance < 110U)
        throw new ArgumentOutOfRangeException("distance must be greater than or equal to 110");
      int elapsedTime = 0;
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      this.MultiPress((IList<PointI>) new List<PointI>()
      {
        pointOne,
        pointTwo
      }, injectList);
      int num1 = (int) Math.Ceiling((double) holdDuration / (double) packetDelta);
      for (int index = 0; index < num1; ++index)
      {
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
        this.MultiUpdate((IList<PointI>) new List<PointI>()
        {
          pointOne,
          pointTwo
        }, injectList);
      }
      int num2 = (int) Math.Ceiling(Math.Sqrt(2.0 * (double) distance / (double) acceleration) / (double) packetDelta);
      double num3 = (double) direction * Math.PI / 180.0;
      PointI pointI1 = pointOne;
      PointI pointI2 = pointTwo;
      PointI pointI3 = pointOne;
      PointI pointI4 = pointTwo;
      double a1 = (double) distance * Math.Cos(num3);
      double a2 = (double) distance * Math.Sin(num3);
      double num4 = (double) acceleration * Math.Cos(num3);
      double num5 = (double) acceleration * Math.Sin(num3);
      int num6 = (int) packetDelta;
      for (int index = 0; index < num2 - 1; ++index)
      {
        pointI1.X = pointOne.X + (int) Math.Round(0.5 * num4 * (double) num6 * (double) num6);
        pointI1.Y = pointOne.Y + (int) Math.Round(0.5 * num5 * (double) num6 * (double) num6);
        pointI2.X = pointTwo.X + (int) Math.Round(0.5 * num4 * (double) num6 * (double) num6);
        pointI2.Y = pointTwo.Y + (int) Math.Round(0.5 * num5 * (double) num6 * (double) num6);
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
        this.MultiUpdate((IList<PointI>) new List<PointI>()
        {
          pointI1,
          pointI2
        }, injectList);
        num6 += (int) packetDelta;
      }
      pointI3.Offset((int) Math.Round(a1), (int) Math.Round(a2));
      pointI4.Offset((int) Math.Round(a1), (int) Math.Round(a2));
      this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      this.MultiUpdate((IList<PointI>) new List<PointI>()
      {
        pointI3,
        pointI4
      }, injectList);
      this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      this.MultiRelease((IList<PointI>) new List<PointI>()
      {
        pointI3,
        pointI4
      }, injectList);
      injectList.Add(Input.CreateWait(800));
      return injectList;
    }

    public IList<IInputAction> TwoFingerRotate(
      PointI pointOne,
      PointI pointTwo,
      float rotationAngle,
      uint duration,
      bool pivotRotate,
      uint packetDelta)
    {
      if (packetDelta < 10U)
        throw new ArgumentOutOfRangeException("packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
      if (duration < packetDelta)
        throw new ArgumentOutOfRangeException("duration must be greater than or equal to packetDelta");
      if (pointOne == pointTwo)
        throw new ArgumentOutOfRangeException("the two fingers must be at different points");
      int elapsedTime = 0;
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      this.MultiPress((IList<PointI>) new List<PointI>()
      {
        pointOne,
        pointTwo
      }, injectList);
      int num1 = (int) Math.Ceiling((double) duration / (double) packetDelta);
      PointI pointI1 = pointOne;
      PointI pointI2 = pointTwo;
      double num2 = (double) rotationAngle * Math.PI / 180.0 / (double) duration;
      double num3 = (double) (pointTwo.X - pointOne.X);
      double num4 = (double) (pointTwo.Y - pointOne.Y);
      double num5 = Math.Sqrt(num3 * num3 + num4 * num4);
      PointI pointI3 = pointOne;
      if (!pivotRotate)
      {
        num5 /= 2.0;
        pointI3.Offset((pointTwo.X - pointOne.X) / 2, (pointTwo.Y - pointOne.Y) / 2);
      }
      double num6 = Math.Atan2((double) (pointOne.Y - pointI3.Y), (double) (pointOne.X - pointI3.X));
      double num7 = Math.Atan2((double) (pointTwo.Y - pointI3.Y), (double) (pointTwo.X - pointI3.X));
      int num8 = (int) packetDelta;
      for (int index = 0; index < num1 - 1; ++index)
      {
        if (!pivotRotate)
        {
          pointI1.X = pointI3.X + (int) Math.Round(num5 * Math.Cos(num6 + num2 * (double) num8));
          pointI1.Y = pointI3.Y + (int) Math.Round(num5 * Math.Sin(num6 + num2 * (double) num8));
        }
        pointI2.X = pointI3.X + (int) Math.Round(num5 * Math.Cos(num7 + num2 * (double) num8));
        pointI2.Y = pointI3.Y + (int) Math.Round(num5 * Math.Sin(num7 + num2 * (double) num8));
        this.Wait(ref elapsedTime, (int) packetDelta, injectList);
        this.MultiUpdate((IList<PointI>) new List<PointI>()
        {
          pointI1,
          pointI2
        }, injectList);
        num8 += (int) packetDelta;
      }
      if (!pivotRotate)
      {
        pointI1.X = pointI3.X + (int) Math.Round(num5 * Math.Cos(num6 + num2 * (double) duration));
        pointI1.Y = pointI3.Y + (int) Math.Round(num5 * Math.Sin(num6 + num2 * (double) duration));
      }
      pointI2.X = pointI3.X + (int) Math.Round(num5 * Math.Cos(num7 + num2 * (double) duration));
      pointI2.Y = pointI3.Y + (int) Math.Round(num5 * Math.Sin(num7 + num2 * (double) duration));
      this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      this.MultiUpdate((IList<PointI>) new List<PointI>()
      {
        pointI1,
        pointI2
      }, injectList);
      this.Wait(ref elapsedTime, (int) packetDelta, injectList);
      this.MultiRelease((IList<PointI>) new List<PointI>()
      {
        pointI1,
        pointI2
      }, injectList);
      injectList.Add(Input.CreateWait(800));
      return injectList;
    }

    private bool EmitPoint(
      MultiAction[] actions,
      int rowIndex,
      uint pointerId,
      List<PointerData> injectListPointerData,
      ref int? waitData)
    {
      if (rowIndex > actions.Length - 1)
        return false;
      MultiAction action1 = actions[rowIndex];
      if (action1.Action == ActionType.Wait)
      {
        if (!waitData.HasValue || (long) action1.Duration > (long) waitData.Value)
          waitData = new int?((int) action1.Duration);
        PointerData[] pointerInputActions;
        if (rowIndex > 0)
        {
          MultiAction action2 = actions[rowIndex - 1];
          pointerInputActions = this.CreateMultiPointerInputActions((IList<PointI>) new PointI[1]
          {
            action2.Point
          }, POINTER_FLAGS.ContactMoves, (IList<uint>) new uint[1]
          {
            pointerId
          });
          action1.Point = action2.Point;
        }
        else
          pointerInputActions = this.CreateMultiPointerInputActions((IList<PointI>) new PointI[1]
          {
            new PointI(0, 0)
          }, POINTER_FLAGS.ContactMoves, (IList<uint>) new uint[1]
          {
            pointerId
          });
        injectListPointerData.Add(pointerInputActions[0]);
      }
      else
      {
        PointerData[] pointerInputActions = this.CreateMultiPointerInputActions((IList<PointI>) new PointI[1]
        {
          action1.Point
        }, this.ActionTypeToPointerFlags(action1.Action), (IList<uint>) new uint[1]
        {
          pointerId
        });
        injectListPointerData.Add(pointerInputActions[0]);
      }
      return rowIndex != actions.Length - 1;
    }

    private POINTER_FLAGS ActionTypeToPointerFlags(ActionType actionType)
    {
      switch (actionType)
      {
        case ActionType.Press:
          return POINTER_FLAGS.ContactDown;
        case ActionType.Move:
          return POINTER_FLAGS.ContactMoves;
        case ActionType.Release:
          return POINTER_FLAGS.UP;
        default:
          return POINTER_FLAGS.NONE;
      }
    }

    public IList<IInputAction> RawMultiTouchGesture(
      MultiTouchInjectionData[] injectionData,
      uint packetDelta)
    {
      if (packetDelta < 10U)
        throw new ArgumentOutOfRangeException("packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
      IList<IInputAction> injectList = (IList<IInputAction>) new List<IInputAction>();
      int elapsedTime = 0;
      int rowIndex = 0;
      bool flag = true;
      while (flag)
      {
        flag = false;
        List<PointerData> injectListPointerData = new List<PointerData>();
        int? waitData = new int?();
        for (uint pointerId = 0; (long) pointerId < (long) injectionData.Length; ++pointerId)
          flag = this.EmitPoint(injectionData[(int) pointerId].Actions, rowIndex, pointerId, injectListPointerData, ref waitData) | flag;
        if (injectListPointerData.Count > 0)
          injectList.Add((IInputAction) new MultiPointerInputAction()
          {
            pointerData = injectListPointerData.ToArray()
          });
        if (waitData.HasValue)
          injectList.Add(Input.CreateWait(waitData.Value));
        else if (flag)
          injectList.Add(Input.CreateWait((int) packetDelta));
        ++rowIndex;
      }
      this.Interpolate(ref elapsedTime, injectList, packetDelta);
      injectList.Add(Input.CreateWait(800));
      this.ConvertBigWaitTime(injectList);
      return injectList;
    }

    private void ConvertBigWaitTime(IList<IInputAction> injectList, int maxTime = 100)
    {
      for (int index1 = injectList.Count - 1; index1 >= 0; --index1)
      {
        if (injectList[index1] is AbsoluteWaitInputAction inject)
        {
          int duration = inject.duration;
          if (duration > maxTime)
          {
            injectList.RemoveAt(index1);
            int num = duration / maxTime + 1;
            for (int index2 = 0; index2 < num; ++index2)
            {
              injectList.Insert(index1, Input.CreateWait(Math.Min(duration, maxTime)));
              duration -= maxTime;
            }
          }
        }
      }
    }

    private void Interpolate(ref int elapsedTime, IList<IInputAction> injectList, uint packetDelta)
    {
      for (int index1 = injectList.Count - 1; index1 >= 0; --index1)
      {
        if (injectList[index1] is MultiPointerInputAction inject)
        {
          for (int index2 = 0; index2 < inject.pointerData.Length; ++index2)
          {
            if ((inject.pointerData[index2].flags & POINTER_FLAGS.ContactMoves) == POINTER_FLAGS.ContactMoves || (inject.pointerData[index2].flags & POINTER_FLAGS.ContactDown) == POINTER_FLAGS.ContactDown)
            {
              int indexOfNext;
              MultiPointerInputAction multiPointerFrame = this.FindNextMultiPointerFrame(index1 + 1, injectList, out indexOfNext);
              if (multiPointerFrame != null)
              {
                this.InterpolateFrame(injectList, packetDelta, indexOfNext, inject, multiPointerFrame);
                break;
              }
              break;
            }
          }
        }
      }
      this.ConvertBigWaitTime(injectList);
      for (int index = 0; index < injectList.Count; ++index)
      {
        if (injectList[index] is AbsoluteWaitInputAction inject)
        {
          injectList.RemoveAt(index);
          List<IInputAction> inputActionList = new List<IInputAction>();
          this.Wait(ref elapsedTime, inject.duration, (IList<IInputAction>) inputActionList);
          injectList.Insert(index, inputActionList[0]);
        }
      }
    }

    private MultiPointerInputAction FindNextMultiPointerFrame(
      int index,
      IList<IInputAction> injectList,
      out int indexOfNext)
    {
      for (int index1 = index; index1 < injectList.Count; ++index1)
      {
        if (injectList[index1] is MultiPointerInputAction inject)
        {
          indexOfNext = index1;
          return inject;
        }
      }
      indexOfNext = -1;
      return (MultiPointerInputAction) null;
    }

    private void InterpolateFrame(
      IList<IInputAction> injectList,
      uint packetDelta,
      int index,
      MultiPointerInputAction multiPointerInputAction,
      MultiPointerInputAction nextPointerInputAction)
    {
      double num1 = Math.Ceiling(50.0 / (double) packetDelta);
      for (int index1 = 0; (double) index1 <= num1; ++index1)
      {
        List<PointerData> pointerDataList = new List<PointerData>();
        for (int index2 = 0; index2 < multiPointerInputAction.pointerData.Length; ++index2)
        {
          if ((multiPointerInputAction.pointerData[index2].flags & POINTER_FLAGS.ContactMoves) == POINTER_FLAGS.ContactMoves || (multiPointerInputAction.pointerData[index2].flags & POINTER_FLAGS.ContactDown) == POINTER_FLAGS.ContactDown)
          {
            PointerData pointerData1 = multiPointerInputAction.pointerData[index2];
            PointerData pointerData2 = nextPointerInputAction.pointerData[index2];
            int num2 = pointerData2.location.X - pointerData1.location.X;
            int num3 = pointerData2.location.Y - pointerData1.location.Y;
            pointerData1.location.X += (int) Math.Round((double) (num2 * index1) / num1);
            pointerData1.location.Y += (int) Math.Round((double) (num3 * index1) / num1);
            pointerData1.flags = POINTER_FLAGS.ContactMoves | POINTER_FLAGS.PRIMARY | POINTER_FLAGS.CONFIDENCE;
            pointerDataList.Add(pointerData1);
          }
        }
        injectList.Insert(index + index1 * 2, (IInputAction) new MultiPointerInputAction()
        {
          pointerData = pointerDataList.ToArray()
        });
        injectList.Insert(index + index1 * 2 + 1, Input.CreateWait((int) packetDelta));
      }
    }

    private void Press(PointI? location, IList<IInputAction> injectList, uint pointerId = 0)
    {
      PointerInputAction pointerInputAction = new PointerInputAction();
      pointerInputAction.pointerData.flags = POINTER_FLAGS.ContactDown | POINTER_FLAGS.PRIMARY | POINTER_FLAGS.CONFIDENCE;
      if (location.HasValue && location.HasValue)
        pointerInputAction.pointerData.location = location.Value;
      pointerInputAction.pointerData.pointerId = pointerId;
      injectList.Add((IInputAction) pointerInputAction);
    }

    private void Wait(ref int elapsedTime, int duration, IList<IInputAction> injectList)
    {
      injectList.Add((IInputAction) new RelativeWaitInputAction()
      {
        start = elapsedTime,
        duration = duration
      });
      elapsedTime += duration;
    }

    private void Update(PointI location, IList<IInputAction> injectList, uint pointerId = 0) => injectList.Add((IInputAction) new PointerInputAction()
    {
      pointerData = {
        flags = (POINTER_FLAGS.ContactMoves | POINTER_FLAGS.PRIMARY | POINTER_FLAGS.CONFIDENCE),
        location = location,
        pointerId = pointerId
      }
    });

    private PointerData[] CreateMultiPointerInputActions(
      IList<PointI> locations,
      POINTER_FLAGS flag,
      IList<uint> pointerIds = null)
    {
      PointerData[] pointerDataArray = new PointerData[locations.Count];
      for (int index = 0; index < locations.Count; ++index)
      {
        pointerDataArray[index].flags = flag | POINTER_FLAGS.CONFIDENCE | POINTER_FLAGS.PRIMARY;
        pointerDataArray[index].location = locations[index];
        pointerDataArray[index].pointerId = pointerIds != null ? pointerIds[index] : (uint) index;
      }
      return pointerDataArray;
    }

    private void MultiPress(IList<PointI> locations, IList<IInputAction> injectList)
    {
      MultiPointerInputAction pointerInputAction = new MultiPointerInputAction()
      {
        pointerData = this.CreateMultiPointerInputActions(locations, POINTER_FLAGS.ContactDown)
      };
      injectList.Add((IInputAction) pointerInputAction);
    }

    private void MultiUpdate(
      IList<PointI> locations,
      IList<IInputAction> injectList,
      IList<uint> contactIds = null)
    {
      MultiPointerInputAction pointerInputAction = new MultiPointerInputAction()
      {
        pointerData = this.CreateMultiPointerInputActions(locations, POINTER_FLAGS.ContactMoves, contactIds)
      };
      injectList.Add((IInputAction) pointerInputAction);
    }

    private void Release(PointI? location, IList<IInputAction> injectList, uint contactId = 0)
    {
      PointerInputAction pointerInputAction = new PointerInputAction();
      pointerInputAction.pointerData.flags = POINTER_FLAGS.PRIMARY | POINTER_FLAGS.CONFIDENCE | POINTER_FLAGS.UP;
      if (location.HasValue && location.HasValue)
        pointerInputAction.pointerData.location = location.Value;
      pointerInputAction.pointerData.pointerId = contactId;
      injectList.Add((IInputAction) pointerInputAction);
    }

    private void MultiRelease(IList<PointI> locations, IList<IInputAction> injectList)
    {
      MultiPointerInputAction pointerInputAction = new MultiPointerInputAction()
      {
        pointerData = this.CreateMultiPointerInputActions(locations, POINTER_FLAGS.UP)
      };
      injectList.Add((IInputAction) pointerInputAction);
    }
  }
}
