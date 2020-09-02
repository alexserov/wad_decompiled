// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.IPointerInput
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation {
    public interface IPointerInput {
        PointI Location { get; }
        void Click(PointerButtons button, int count);

        void ClickDrag(PointI endPoint, PointerButtons button, uint dragDuration);

        void Move(PointI point);

        void Press(PointerButtons button);

        void Release(PointerButtons button);

        void InjectPointers(PointerData[] pointerDataArray);
    }
}