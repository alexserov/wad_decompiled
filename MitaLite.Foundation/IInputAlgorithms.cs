// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.IInputAlgorithms
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    internal interface IInputAlgorithms {
        IList<IInputAction> DynamicPress(PointI point, uint contactId);

        IInputAction DynamicMove(PointI point, uint contactId);

        IInputAction DynamicMoves(IList<PointI> point, IList<uint> contactIds);

        IList<IInputAction> DynamicRelease(PointI point, uint contactId);

        IList<IInputAction> DynamicPointerActions(PointerData[] pointerDataArray);

        IList<IInputAction> DynamicDrag(
            PointI start,
            PointI end,
            uint duration,
            uint packetDelta,
            uint contactId);

        IList<IInputAction> PressAndHold(
            PointI point,
            uint holdDuration,
            uint tapCount,
            uint tapDelta,
            uint packetDelta);

        IList<IInputAction> PressAndHoldAndDrag(
            PointI start,
            PointI end,
            uint dragDuration,
            uint holdDuration,
            uint packetDelta);

        IList<IInputAction> PressAndHoldAndDragWithAcceleration(
            PointI start,
            PointI end,
            uint holdDuration,
            float acceleration,
            uint packetDelta);

        IList<IInputAction> TwoFingerPressAndHold(
            PointI pointOne,
            PointI pointTwo,
            uint holdDuration,
            uint tapCount,
            uint tapDelta,
            uint packetDelta);

        IList<IInputAction> TwoFingerZoom(
            PointI pointOne,
            PointI pointTwo,
            float direction,
            uint duration,
            uint distance,
            bool pivotZoom,
            uint packetDelta);

        IList<IInputAction> TwoFingerPressAndHoldAndDragWithAcceleration(
            PointI pointOne,
            PointI pointTwo,
            float direction,
            uint distance,
            uint holdDuration,
            float acceleration,
            uint packetDelta);

        IList<IInputAction> TwoFingerRotate(
            PointI pointOne,
            PointI pointTwo,
            float rotationAngle,
            uint duration,
            bool pivotRotate,
            uint packetDelta);

        IList<IInputAction> RawMultiTouchGesture(
            MultiTouchInjectionData[] pointdata,
            uint packetDelta);
    }
}