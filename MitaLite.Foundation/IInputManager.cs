// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.IInputManager
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation
{
  internal interface IInputManager : IDisposable
  {
    void InjectDynamicPress(PointI touchPoint, uint contactId);

    void InjectDynamicMove(
      PointI start,
      PointI end,
      uint maxDragDuration,
      uint contactId,
      uint packetDelta);

    void InjectDynamicRelease(PointI touchPoint, uint contactId);

    void InjectDynamicPointers(PointerData[] pointerDataArray);

    void InjectPress(
      PointI point,
      uint holdDuration,
      uint tapCount,
      uint tapDelta,
      uint packetDelta);

    void InjectPressAndDrag(
      PointI start,
      PointI end,
      uint dragDuration,
      uint holdDuration,
      uint packetDelta);

    void InjectPressAndDragWithAcceleration(
      PointI start,
      PointI end,
      uint holdDuration,
      float acceleration,
      uint packetDelta);

    void InjectMTPanWithAcceleration(
      PointI startFingerOne,
      PointI startFingerTwo,
      float direction,
      uint distance,
      uint holdDuration,
      float acceleration,
      uint packetDelta);

    void InjectMTZoom(
      PointI startFingerOne,
      PointI startFingerTwo,
      float direction,
      uint duration,
      uint distance,
      bool pivotZoom,
      uint packetDelta);

    void InjectMTRotate(
      PointI startFingerOne,
      PointI startFingerTwo,
      float rotationAngle,
      uint duration,
      bool pivotRotate,
      uint packetDelta);

    void InjectMTTwoFingerPress(
      PointI pointOne,
      PointI pointTwo,
      uint holdDuration,
      uint tapCount,
      uint tapDelta,
      uint packetDelta);

    void InjectMTPressAndTap(
      PointI startFingerOne,
      PointI startFingerTwo,
      PointI endFingerOne,
      uint deltaFingerTwoDown,
      uint deltaFingerTwoUp,
      uint deltaFingerOneUp,
      uint packetDelta);

    void InjectMultiTouch(MultiTouchInjectionData[] injectionData, uint packetDelta);

    bool DynamicPressActive(int ContactId);
  }
}
