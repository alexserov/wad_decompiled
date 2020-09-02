// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.SingleTouch
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation
{
  internal class SingleTouch : ISinglePointGestureInput, IPointerInput, IDisposable
  {
    private static object _classLock = new object();
    private static SingleTouch _singletonInstance;
    private IInputManager _inputManager = (IInputManager) new InputManager(INPUT_DEVICE_TYPE.TOUCH, (IInputDeviceFactory) new InputDeviceFactory(), (ITimeManagerFactory) new TimeManagerFactory(), (IInputAlgorithms) new InputAlgorithms());
    private PointI _location;
    private readonly uint DefaultContactId;
    private bool _disposed;

    protected SingleTouch()
    {
    }

    ~SingleTouch() => this.Dispose(false);

    public static SingleTouch Instance
    {
      get
      {
        if (SingleTouch._singletonInstance == null)
        {
          lock (SingleTouch._classLock)
          {
            if (SingleTouch._singletonInstance == null)
              SingleTouch._singletonInstance = new SingleTouch();
          }
        }
        return SingleTouch._singletonInstance;
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      try
      {
        if (this._disposed || !disposing)
          return;
        this._inputManager.Dispose();
        this._inputManager = (IInputManager) null;
      }
      finally
      {
        this._disposed = true;
      }
    }

    public void Flick(PointI endPoint, uint holdDuration, float acceleration) => this._inputManager.InjectPressAndDragWithAcceleration(this._location, Input.AdjustPointerMoveInput(endPoint), holdDuration, acceleration, InputManager.DefaultPacketDelta);

    public void Pan(PointI endPoint, uint holdDuration, float acceleration) => this._inputManager.InjectPressAndDragWithAcceleration(this._location, Input.AdjustPointerMoveInput(endPoint), holdDuration, acceleration, InputManager.DefaultPacketDelta);

    public void PressAndDrag(PointI endPoint, uint dragDuration) => this._inputManager.InjectPressAndDrag(this._location, Input.AdjustPointerMoveInput(endPoint), dragDuration, InputManager.DefaultPressDuration, InputManager.DefaultPacketDelta);

    public void PressAndDrag(PointI endPoint, uint dragDuration, uint pressDuration) => this._inputManager.InjectPressAndDrag(this._location, Input.AdjustPointerMoveInput(endPoint), dragDuration, pressDuration, InputManager.DefaultPacketDelta);

    public void PressAndHold(uint holdDuration) => this._inputManager.InjectPress(this._location, holdDuration, 1U, InputManager.DefaultTapDelta, InputManager.DefaultPacketDelta);

    public void Click(PointerButtons button, int count) => this._inputManager.InjectPress(this._location, InputManager.DefaultPressDuration, (uint) count, InputManager.DefaultTapDelta, InputManager.DefaultPacketDelta);

    public void ClickDrag(PointI endPoint, PointerButtons button, uint dragDuration) => this.PressAndDrag(endPoint, dragDuration);

    public void Move(PointI point)
    {
      PointI location = this._location;
      this._location = Input.AdjustPointerMoveInput(point);
      this._inputManager.InjectDynamicMove(location, this._location, SinglePointGesture.DefaultDragDuration, this.DefaultContactId, InputManager.DefaultPacketDelta);
    }

    public void Press(PointerButtons button) => this._inputManager.InjectDynamicPress(this._location, this.DefaultContactId);

    public void Release(PointerButtons button) => this._inputManager.InjectDynamicRelease(this._location, this.DefaultContactId);

    public void InjectPointers(PointerData[] pointerDataArray) => throw new NotImplementedException("SingleTouch move with PointerData argument is not implemented");

    public PointI Location => this._location;
  }
}
