// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputDeviceTouch
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;
using Windows.UI.Input.Preview.Injection;

namespace MS.Internal.Mita.Foundation
{
  internal class InputDeviceTouch : InputDevice
  {
    public InputDeviceTouch()
    {
      this.injector = InputInjector.TryCreate();
      if (this.injector == null)
        throw new ActionException("Failed to initialize touch input injection");
      this.injector.InitializeTouchInjection((InjectedInputVisualizationMode) 1);
    }

    private InjectedInputTouchInfo GetInjectedInputTouchInfo(
      PointerData pointerData)
    {
      InjectedInputTouchInfo injectedInputTouchInfo = new InjectedInputTouchInfo();
      injectedInputTouchInfo.TouchParameters = (InjectedInputTouchParameters) 1;
      InjectedInputRectangle injectedInputRectangle;
      injectedInputRectangle.Top =  pointerData.location.Y;
      injectedInputRectangle.Bottom =  pointerData.location.Y;
      injectedInputRectangle.Left =  pointerData.location.X;
      injectedInputRectangle.Right =  pointerData.location.X;
      injectedInputTouchInfo.Contact = injectedInputRectangle;
      InjectedInputPoint injectedInputPoint;
      injectedInputPoint.PositionX =  pointerData.location.X;
      injectedInputPoint.PositionY =  pointerData.location.Y;
      InjectedInputPointerInfo inputPointerInfo;
      inputPointerInfo.PointerOptions = (InjectedInputPointerOptions)(int) pointerData.flags;
      inputPointerInfo.PointerId = (uint)(int) pointerData.pointerId;
      inputPointerInfo.PixelLocation =  injectedInputPoint;
      inputPointerInfo.PerformanceCount =  0L;
      inputPointerInfo.TimeOffsetInMilliseconds =  0;
      injectedInputTouchInfo.PointerInfo = inputPointerInfo;
      return injectedInputTouchInfo;
    }

    public override void InjectPointer(PointerData pointerData)
    {
      Log.Out("Inject Pointer: {0}", (object) pointerData.ToString());
      InjectedInputTouchInfo injectedInputTouchInfo = this.GetInjectedInputTouchInfo(pointerData);
      List<InjectedInputTouchInfo> injectedInputTouchInfoList = new List<InjectedInputTouchInfo>();
      injectedInputTouchInfoList.Add(injectedInputTouchInfo);
      this.injector.InjectTouchInput((IEnumerable<InjectedInputTouchInfo>) injectedInputTouchInfoList);
    }

    public override void InjectPointer(PointerData[] pointerData)
    {
      if (pointerData.Length > 256)
        throw new ArgumentOutOfRangeException(string.Format("The maximum number of simultaneous touch points is {0}.", (object) 256U));
      if (Log.OutImplementation != null)
      {
        foreach (PointerData pointerData1 in pointerData)
          Log.Out("Inject Pointer: {0}", (object) pointerData1.ToString());
      }
      List<InjectedInputTouchInfo> injectedInputTouchInfoList = new List<InjectedInputTouchInfo>();
      foreach (PointerData pointerData1 in pointerData)
        injectedInputTouchInfoList.Add(this.GetInjectedInputTouchInfo(pointerData1));
      this.injector.InjectTouchInput((IEnumerable<InjectedInputTouchInfo>) injectedInputTouchInfoList);
    }

    protected override void RemoveInjectionDevice()
    {
      if (this.injector == null)
        return;
      this.injector.UninitializeTouchInjection();
    }
  }
}
