// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.IPointer
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation
{
  public interface IPointer
  {
    void Click(UIObject uiObject);

    void Click(UIObject uiObject, PointerButtons button);

    void Click(UIObject uiObject, PointerButtons button, double offsetX, double offsetY);

    void DoubleClick(UIObject uiObject);

    void DoubleClick(UIObject uiObject, PointerButtons button);

    void DoubleClick(UIObject uiObject, PointerButtons button, double offsetX, double offsetY);

    void ClickDrag(UIObject targetObject, UIObject destinationObject);

    void ClickDrag(UIObject targetObject, UIObject destinationObject, PointerButtons button);

    void Move(UIObject uiObject);

    void Move(UIObject uiObject, double offsetX, double offsetY);
  }
}
