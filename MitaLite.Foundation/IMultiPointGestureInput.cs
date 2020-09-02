// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.IMultiPointGestureInput
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation {
    public interface IMultiPointGestureInput : ISinglePointGestureInput, IPointerInput {
        void PressAndTap(uint tapCount, uint tapDuration, uint tapDelta, uint distance);

        void TwoPointPressAndHold(uint tapCount, uint holdDuration, uint tapDelta, uint distance);

        void TwoPointPan(PointI endPoint, uint holdDuration, float acceleration, uint distance);

        void Pinch(float direction, uint duration, uint startDistance, uint endDistance);

        void Pinch(float direction, uint duration, uint startDistance, uint endDistance, bool pivot);

        void Stretch(float direction, uint duration, uint startDistance, uint endDistance);

        void Stretch(
            float direction,
            uint duration,
            uint startDistance,
            uint endDistance,
            bool pivot);

        void Rotate(float angle, uint duration, bool centerOnPoint, uint distance);

        void InjectMultiPointGesture(MultiTouchInjectionData[] injectionData);
    }
}