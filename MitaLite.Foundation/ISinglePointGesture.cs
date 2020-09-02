// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.ISinglePointGesture
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation {
    public interface ISinglePointGesture {
        void Flick(UIObject targetObject, UIObject destinationObject);

        void Flick(UIObject targetObject, UIObject destinationObject, float acceleration);

        void Flick(UIObject uiObject, float acceleration, uint distance, float direction);

        void Pan(UIObject targetObject, UIObject destinationObject);

        void Pan(UIObject targetObject, UIObject destinationObject, float acceleration);

        void Pan(UIObject uiObject, float acceleration, uint distance, float direction);

        void PressAndDrag(UIObject targetObject, UIObject destinationObject);

        void PressAndDrag(UIObject targetObject, UIObject destinationObject, uint dragDuration);

        void PressAndDrag(
            UIObject targetObject,
            UIObject destinationObject,
            uint dragDuration,
            uint pressDuration);

        void PressAndDrag(UIObject uiObject, uint distance, float direction);

        void PressAndHold(UIObject uiObject);

        void PressAndHold(UIObject uiObject, uint holdDuration);
    }
}