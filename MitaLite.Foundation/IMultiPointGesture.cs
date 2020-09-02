// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.IMultiPointGesture
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation {
    public interface IMultiPointGesture {
        void PressAndTap(UIObject uiObject);

        void TwoPointPressAndHold(UIObject uiObject);

        void TwoPointPressAndHold(UIObject uiObject, uint count);

        void TwoPointPan(UIObject targetObject, UIObject destinationObject);

        void TwoPointPan(UIObject targetObject, UIObject destinationObject, float acceleration);

        void TwoPointPan(UIObject uiObject, float acceleration, uint distance, float direction);

        void Pinch(UIObject uiObject);

        void Pinch(UIObject uiObject, uint distance);

        void Pinch(UIObject uiObject, uint distance, float direction);

        void Pinch(UIObject uiObject, uint distance, float direction, bool pivot);

        void Stretch(UIObject uiObject);

        void Stretch(UIObject uiObject, uint distance);

        void Stretch(UIObject uiObject, uint distance, float direction);

        void Stretch(UIObject uiObject, uint distance, float direction, bool pivot);

        void Rotate(UIObject uiObject, float angle);

        void Rotate(UIObject uiObject, uint distance, float angle);

        void Rotate(UIObject uiObject, float angle, bool centerOnPoint);

        void Rotate(UIObject uiObject, uint distance, float angle, bool centerOnPoint);
    }
}