// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Mouse
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  internal class Mouse : IPointerInput, IMouseWheelInput
  {
    private static object _classLock = new object();
    private static Mouse _singletonInstance;
    private PointI _location;
    private int[] _previousRuntimeId;
    internal INativeMethods nativeMethods;
    internal IInputQueue inputQueue;
    private IInputDevice inputDevice;
    private const int afterMoveActionWaitDuration = 0;
    private const int afterClickActionWaitDuration = 100;
    private const int afterWheelActionWaitDuration = 200;

    protected Mouse()
    {
      this.nativeMethods = (INativeMethods) new NativeMethods();
      this.inputQueue = (IInputQueue) new InputQueue();
      this.inputDevice = new InputDeviceFactory().Get(INPUT_DEVICE_TYPE.MOUSE);
    }

    public static Mouse Instance
    {
      get
      {
        if (Mouse._singletonInstance == null)
        {
          lock (Mouse._classLock)
          {
            if (Mouse._singletonInstance == null)
              Mouse._singletonInstance = new Mouse();
          }
        }
        return Mouse._singletonInstance;
      }
    }

    public virtual void Click(PointerButtons button, int count)
    {
      if (0 >= count)
        throw new ArgumentOutOfRangeException(nameof (count), (object) count, "count should be a positive value");
      PointI location = this.Location;
      List<IInputAction> inputActionList = new List<IInputAction>();
      inputActionList.AddRange((IEnumerable<IInputAction>) Input.PreventAccidentalDoubleClick((double) location.X, (double) location.Y, ref this._previousRuntimeId));
      for (int index = 0; index < count; ++index)
      {
        if (index > 0)
          inputActionList.Add(Input.CreateWait((int) InputManager.DefaultTapDelta));
        inputActionList.AddRange((IEnumerable<IInputAction>) this.Down(button, ModifierKeys.None));
        inputActionList.Add(Input.CreateWait((int) InputManager.DefaultPressDuration));
        inputActionList.AddRange((IEnumerable<IInputAction>) this.Up(button, ModifierKeys.None));
      }
      inputActionList.Add(Input.CreateWait(100));
      this.inputQueue.Process(this.inputDevice, (IList<IInputAction>) inputActionList);
    }

    public void ClickDrag(PointI endPoint, PointerButtons button, uint dragDuration)
    {
      List<IInputAction> inputActionList = new List<IInputAction>();
      inputActionList.AddRange((IEnumerable<IInputAction>) this.Down(button, ModifierKeys.None));
      inputActionList.Add(Input.CreateWait((int) dragDuration));
      inputActionList.Add(Input.CreateMouseMoveInput((double) endPoint.X, (double) endPoint.Y));
      inputActionList.AddRange((IEnumerable<IInputAction>) this.Up(button, ModifierKeys.None));
      inputActionList.Add(Input.CreateWait(0));
      this.inputQueue.Process(this.inputDevice, (IList<IInputAction>) inputActionList);
      this._location = endPoint;
    }

    public virtual void Move(PointI point)
    {
      this.inputQueue.Process(this.inputDevice, (IList<IInputAction>) new List<IInputAction>()
      {
        Input.CreateMouseMoveInput((double) point.X, (double) point.Y),
        Input.CreateWait(0)
      });
      this._location = point;
    }

    public virtual void Press(PointerButtons button)
    {
      IList<IInputAction> inputList = this.Down(button, ModifierKeys.None);
      inputList.Add(Input.CreateWait(100));
      this.inputQueue.Process(this.inputDevice, inputList);
    }

    public void InjectPointers(PointerData[] pointerDataArray) => throw new NotImplementedException("SingleTouch move with PointerData argument is not implemented");

    public virtual void Release(PointerButtons button)
    {
      IList<IInputAction> inputList = this.Up(button, ModifierKeys.None);
      inputList.Add(Input.CreateWait(100));
      this.inputQueue.Process(this.inputDevice, inputList);
    }

    public virtual PointI Location => this._location;

    public virtual void RotateWheel(int delta)
    {
      List<IInputAction> inputActionList = new List<IInputAction>();
      inputActionList.AddRange((IEnumerable<IInputAction>) this.RotateWheel(delta, ModifierKeys.None));
      inputActionList.Add(Input.CreateWait(200));
      this.inputQueue.Process((IInputDevice) null, (IList<IInputAction>) inputActionList);
    }

    private IList<IInputAction> Down(
      PointerButtons button,
      ModifierKeys modifierKeys)
    {
      List<IInputAction> inputActionList = new List<IInputAction>();
      inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyModifierInputs(modifierKeys, true, Keyboard.SendKeysDelay));
      inputActionList.Add(Input.CreateMouseDownInput(button, this.GetMouseButtonsSwapped()));
      return (IList<IInputAction>) inputActionList;
    }

    private IList<IInputAction> Up(
      PointerButtons button,
      ModifierKeys modifierKeys)
    {
      List<IInputAction> inputActionList = new List<IInputAction>();
      inputActionList.Add(Input.CreateMouseUpInput(button, this.GetMouseButtonsSwapped()));
      inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyModifierInputs(modifierKeys, false, Keyboard.SendKeysDelay));
      return (IList<IInputAction>) inputActionList;
    }

    private bool GetMouseButtonsSwapped() => this.nativeMethods.GetMouseButtonsSwapped();

    private IList<IInputAction> RotateWheel(int delta, ModifierKeys modifierKeys)
    {
      List<IInputAction> inputActionList = new List<IInputAction>();
      inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyModifierInputs(modifierKeys, true, Keyboard.SendKeysDelay));
      inputActionList.Add(Input.CreateMouseRotateWheelInput(delta));
      inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyModifierInputs(modifierKeys, false, Keyboard.SendKeysDelay));
      return (IList<IInputAction>) inputActionList;
    }
  }
}
