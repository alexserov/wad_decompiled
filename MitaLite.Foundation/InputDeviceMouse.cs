// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDeviceMouse
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using Windows.UI.Input.Preview.Injection;

namespace MS.Internal.Mita.Foundation {
    internal class InputDeviceMouse : InputDevice {
        public InputDeviceMouse() {
            this.injector = InputInjector.TryCreate();
            if (this.injector == null)
                throw new ActionException(message: "Failed to initialize mouse input injection");
        }

        public override void InjectMouseInput(RIMNativeMethods.MouseInput mouseInput) {
            var injectedInputMouseInfo = new InjectedInputMouseInfo();
            injectedInputMouseInfo.DeltaX = mouseInput.dx;
            injectedInputMouseInfo.DeltaY = mouseInput.dy;
            injectedInputMouseInfo.MouseData = mouseInput.mouseData;
            injectedInputMouseInfo.MouseOptions = (InjectedInputMouseOptions) (int) mouseInput.flags;
            injectedInputMouseInfo.TimeOffsetInMilliseconds = mouseInput.time;
            var injectedInputMouseInfoList = new List<InjectedInputMouseInfo>();
            injectedInputMouseInfoList.Add(item: injectedInputMouseInfo);
            this.injector.InjectMouseInput(input: injectedInputMouseInfoList);
        }
    }
}