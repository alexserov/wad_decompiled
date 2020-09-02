// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDevicePen
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using Windows.UI.Input.Preview.Injection;

namespace MS.Internal.Mita.Foundation
{
  internal class InputDevicePen : InputDevice
  {
    public InputDevicePen()
    {
      this.injector = InputInjector.TryCreate();
      if (this.injector == null)
        throw new ActionException("Failed to initialize pen input injection");
      this.injector.InitializePenInjection((InjectedInputVisualizationMode) 1);
    }

    private InjectedInputPenInfo GetInjectedInputPenInfo(PointerData pointerData)
    {
      InjectedInputPenInfo injectedInputPenInfo = new InjectedInputPenInfo();
      injectedInputPenInfo.PenParameters = (InjectedInputPenParameters) 0;
      InjectedInputPoint injectedInputPoint;
      injectedInputPoint.PositionX =  pointerData.location.X;
      injectedInputPoint.PositionY =  pointerData.location.Y;
      InjectedInputPointerInfo inputPointerInfo;
      inputPointerInfo.PointerOptions = (InjectedInputPointerOptions)(int) pointerData.flags;
      inputPointerInfo.PointerId = (uint)(int) pointerData.pointerId;
      inputPointerInfo.PixelLocation =  injectedInputPoint;
      inputPointerInfo.PerformanceCount =  0L;
      inputPointerInfo.TimeOffsetInMilliseconds =  0;
      injectedInputPenInfo.PointerInfo = inputPointerInfo;
      return injectedInputPenInfo;
    }

    public override void InjectPointer(PointerData pointerData)
    {
      Log.Out("Inject Pointer: {0}", (object) pointerData.ToString());
      this.injector.InjectPenInput(this.GetInjectedInputPenInfo(pointerData));
    }

    protected override void RemoveInjectionDevice()
    {
      if (this.injector == null)
        return;
      this.injector.UninitializePenInjection();
    }
  }
}
