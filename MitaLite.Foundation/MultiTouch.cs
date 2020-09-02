// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.MultiTouch
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation
{
  internal class MultiTouch : IMultiPointGestureInput, ISinglePointGestureInput, IPointerInput, IDisposable
  {
    private static object _classLock = new object();
    private static MultiTouch _singletonInstance;
    private IInputManager _inputManager = (IInputManager) new InputManager(INPUT_DEVICE_TYPE.TOUCH, (IInputDeviceFactory) new InputDeviceFactory(), (ITimeManagerFactory) new TimeManagerFactory(), (IInputAlgorithms) new InputAlgorithms());
    private PointI[] _location = new PointI[(int) MultiTouch.MaxNumberOfContacts];
    private readonly uint DefaultFirstFingerUpTapDelta = 100;
    private readonly uint DefaultContactId;
    private bool _disposed;
    private static readonly uint MaxNumberOfContacts = 10;

    protected MultiTouch()
    {
    }

    ~MultiTouch() => this.Dispose(false);

    public static MultiTouch Instance
    {
      get
      {
        if (MultiTouch._singletonInstance == null)
        {
          lock (MultiTouch._classLock)
          {
            if (MultiTouch._singletonInstance == null)
              MultiTouch._singletonInstance = new MultiTouch();
          }
        }
        return MultiTouch._singletonInstance;
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

    public void Flick(PointI endPoint, uint holdDuration, float acceleration) => this._inputManager.InjectPressAndDragWithAcceleration(this.Location, Input.AdjustPointerMoveInput(endPoint), holdDuration, acceleration, InputManager.DefaultPacketDelta);

    public void Pan(PointI endPoint, uint holdDuration, float acceleration) => this._inputManager.InjectPressAndDragWithAcceleration(this.Location, Input.AdjustPointerMoveInput(endPoint), holdDuration, acceleration, InputManager.DefaultPacketDelta);

    public void PressAndDrag(PointI endPoint, uint dragDuration) => this._inputManager.InjectPressAndDrag(this.Location, Input.AdjustPointerMoveInput(endPoint), dragDuration, InputManager.DefaultPressDuration, InputManager.DefaultPacketDelta);

    public void PressAndDrag(PointI endPoint, uint dragDuration, uint pressDuration) => this._inputManager.InjectPressAndDrag(this.Location, Input.AdjustPointerMoveInput(endPoint), dragDuration, pressDuration, InputManager.DefaultPacketDelta);

    public void PressAndHold(uint holdDuration) => this._inputManager.InjectPress(this.Location, holdDuration, 1U, InputManager.DefaultTapDelta, InputManager.DefaultPacketDelta);

    public void Click(PointerButtons button, int count) => this._inputManager.InjectPress(this.Location, InputManager.DefaultPressDuration, (uint) count, InputManager.DefaultTapDelta, InputManager.DefaultPacketDelta);

    public void ClickDrag(PointI endPoint, PointerButtons button, uint dragDuration) => this.PressAndDrag(endPoint, dragDuration);

    public void Move(PointI point) => this.Move(point, this.DefaultContactId);

    public void Press(PointerButtons button) => this._inputManager.InjectDynamicPress(this.Location, this.DefaultContactId);

    public void Release(PointerButtons button) => this._inputManager.InjectDynamicRelease(this.Location, this.DefaultContactId);

    public void InjectPointers(PointerData[] pointerDataArray) => this._inputManager.InjectDynamicPointers(pointerDataArray);

    public PointI Location => this.GetLocationOfContact(this.DefaultContactId);

    public void PressAndTap(uint tapCount, uint tapDuration, uint tapDelta, uint distance) => this._inputManager.InjectMTPressAndTap(this.Location, this.CreateSecondFingerPointFromDistance(distance, 180f), this.Location, tapDelta, tapDuration, this.DefaultFirstFingerUpTapDelta, InputManager.DefaultPacketDelta);

    public void TwoPointPressAndHold(
      uint tapCount,
      uint holdDuration,
      uint tapDelta,
      uint distance) => this._inputManager.InjectMTTwoFingerPress(this.Location, this.CreateSecondFingerPointFromDistance(distance, 180f), holdDuration, tapCount, tapDelta, InputManager.DefaultPacketDelta);

    public void TwoPointPan(PointI endPoint, uint holdDuration, float acceleration, uint distance)
    {
      PointI pointI = Input.AdjustPointerMoveInput(endPoint);
      uint distance1 = (uint) Math.Round(Math.Sqrt((double) ((pointI.Y - this.Location.Y) * (pointI.Y - this.Location.Y) + (pointI.X - this.Location.X) * (pointI.X - this.Location.X))));
      float direction = (float) (Math.Atan2((double) (pointI.Y - this.Location.Y), (double) (pointI.X - this.Location.X)) * (180.0 / Math.PI));
      this._inputManager.InjectMTPanWithAcceleration(this.Location, this.CreateSecondFingerPointFromDistance(distance, direction + 90f), direction, distance1, holdDuration, acceleration, InputManager.DefaultPacketDelta);
    }

    public void Pinch(float direction, uint duration, uint startDistance, uint endDistance) => this.Pinch(direction, duration, startDistance, endDistance, false);

    public void Pinch(
      float direction,
      uint duration,
      uint startDistance,
      uint endDistance,
      bool pivot)
    {
      PointI pointFromDistance = this.CreateSecondFingerPointFromDistance(startDistance, direction);
      uint distance = pivot ? startDistance - endDistance : (startDistance - endDistance) / 2U;
      direction = (float) (((double) direction + 180.0) % 360.0);
      this._inputManager.InjectMTZoom(this.Location, pointFromDistance, direction, duration, distance, pivot, InputManager.DefaultPacketDelta);
    }

    public void Stretch(float direction, uint duration, uint startDistance, uint endDistance) => this.Stretch(direction, duration, startDistance, endDistance, false);

    public void Stretch(
      float direction,
      uint duration,
      uint startDistance,
      uint endDistance,
      bool pivot)
    {
      PointI pointFromDistance = this.CreateSecondFingerPointFromDistance(startDistance, direction);
      uint distance = pivot ? endDistance - startDistance : (endDistance - startDistance) / 2U;
      this._inputManager.InjectMTZoom(this.Location, pointFromDistance, direction, duration, distance, pivot, InputManager.DefaultPacketDelta);
    }

    public void Rotate(float angle, uint duration, bool centerOnPoint, uint distance) => this._inputManager.InjectMTRotate(this.Location, this.CreateSecondFingerPointFromDistance(distance, 180f), angle, duration, centerOnPoint, InputManager.DefaultPacketDelta);

    public void InjectMultiPointGesture(MultiTouchInjectionData[] injectionData) => this._inputManager.InjectMultiTouch(injectionData, InputManager.DefaultPacketDelta);

    private void Move(PointI point, uint contactId)
    {
      PointI start = contactId < MultiTouch.MaxNumberOfContacts && contactId >= 0U ? this._location[(int) contactId] : throw new ArgumentOutOfRangeException(nameof (contactId), "The given contact Id does not exist.");
      this._location[(int) contactId] = Input.AdjustPointerMoveInput(point);
      this._inputManager.InjectDynamicMove(start, this._location[(int) contactId], SinglePointGesture.DefaultDragDuration, this.DefaultContactId, InputManager.DefaultPacketDelta);
    }

    private PointI GetLocationOfContact(uint contactId) => contactId < MultiTouch.MaxNumberOfContacts && contactId >= 0U ? this._location[(int) contactId] : throw new ArgumentOutOfRangeException(nameof (contactId), "The given contact Id does not exist.");

        private PointI CreateSecondFingerPointFromDistance(uint distance, float direction) {
            PointI pointI;
            PointI location = this.Location;
            int x = location.X;
            location = this.Location;
            int y = location.Y;
            pointI = new PointI(x, y);
            double num = (double)direction * Math.PI / 180.0;
            pointI.X += (int)((double)distance * Math.Cos(num));
            pointI.Y -= (int)((double)distance * Math.Sin(num));
            return pointI;
        }
  }
}
