// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.IInputDevice
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation
{
  internal interface IInputDevice : IDisposable
  {
    void InjectPointer(PointerData pointerData);

    void InjectPointer(PointerData[] pointerData);

    void InjectMouseInput(RIMNativeMethods.MouseInput mouseInput);

    void InjectKeyboardInput(RIMNativeMethods.KeyboardInput keyboardInput);
  }
}
