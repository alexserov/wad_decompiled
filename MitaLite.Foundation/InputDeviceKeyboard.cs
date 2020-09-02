// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDeviceKeyboard
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using Windows.UI.Input.Preview.Injection;

namespace MS.Internal.Mita.Foundation
{
  internal class InputDeviceKeyboard : InputDevice
  {
    public InputDeviceKeyboard()
    {
      this.injector = InputInjector.TryCreate();
      if (this.injector == null)
        throw new ActionException("Failed to initialize keyboard input injection");
    }

    public override void InjectKeyboardInput(RIMNativeMethods.KeyboardInput keyboardInput)
    {
      InjectedInputKeyboardInfo inputKeyboardInfo = new InjectedInputKeyboardInfo();
      inputKeyboardInfo.KeyOptions = (InjectedInputKeyOptions) (int) keyboardInput.flags;
      inputKeyboardInfo.ScanCode = keyboardInput.scanCode;
      inputKeyboardInfo.VirtualKey = keyboardInput.virtualKeyCode;
      List<InjectedInputKeyboardInfo> inputKeyboardInfoList = new List<InjectedInputKeyboardInfo>();
      inputKeyboardInfoList.Add(inputKeyboardInfo);
      this.injector.InjectKeyboardInput((IEnumerable<InjectedInputKeyboardInfo>) inputKeyboardInfoList);
    }
  }
}
