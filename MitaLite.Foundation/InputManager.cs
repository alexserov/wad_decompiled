// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputManager
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MS.Internal.Mita.Foundation
{
  internal class InputManager : IInputManager, IDisposable
  {
    private bool _disposed;
    private INPUT_DEVICE_TYPE inputType;
    private IInputDevice inputDevice;
    private ITimeManagerFactory timeFactory;
    private IInputAlgorithms inputAlgorithms;
    private static uint _defaultPressDuration = 100;
    private static uint _defaultTapDelta = 100;
    private static uint _defaultPacketDelta = 10;
    private IInputQueue inputQueue;
    private object downPressLock;
    private PointI[] downPressLocation;
    private Thread inputDownPressThread;
    private int pressActive;
    private List<IInputAction> backgroundInputActions;
    private Dictionary<uint, PointerData> _activePointerStates;
    private int[] _previousRuntimeId;

    protected InputManager() => throw new NotImplementedException();

    public InputManager(
      INPUT_DEVICE_TYPE inputType,
      IInputDeviceFactory inputFactory,
      ITimeManagerFactory timeFactory,
      IInputAlgorithms inputAlgorithms)
    {
      this.inputDevice = inputFactory.Get(inputType);
      this.inputType = inputType;
      this.timeFactory = timeFactory;
      this.inputAlgorithms = inputAlgorithms;
      this.downPressLock = new object();
      this.inputQueue = (IInputQueue) new InputQueue();
      this.backgroundInputActions = new List<IInputAction>();
      this.downPressLocation = new PointI[10];
      this._activePointerStates = new Dictionary<uint, PointerData>();
    }

    ~InputManager() => this.Dispose(false);

    public void InjectPress(
      PointI point,
      uint holdDuration,
      uint tapCount,
      uint tapDelta,
      uint packetDelta)
    {
      this._UpdatePressActive(false);
      List<IInputAction> inputActionList = new List<IInputAction>();
      inputActionList.AddRange((IEnumerable<IInputAction>) Input.PreventAccidentalDoubleClick((double) point.X, (double) point.Y, ref this._previousRuntimeId));
      inputActionList.AddRange((IEnumerable<IInputAction>) this.inputAlgorithms.PressAndHold(point, holdDuration, tapCount, tapDelta, packetDelta));
      using (this.timeFactory.Get())
        this.inputQueue.Process(this.inputDevice, (IList<IInputAction>) inputActionList);
    }

    public void InjectPressAndDrag(
      PointI start,
      PointI end,
      uint dragDuration,
      uint holdDuration,
      uint packetDelta)
    {
      this._UpdatePressActive(false);
      IList<IInputAction> inputList = this.inputAlgorithms.PressAndHoldAndDrag(start, end, dragDuration, holdDuration, packetDelta);
      using (this.timeFactory.Get())
        this.inputQueue.Process(this.inputDevice, inputList);
    }

    public void InjectPressAndDragWithAcceleration(
      PointI start,
      PointI end,
      uint holdDuration,
      float acceleration,
      uint packetDelta)
    {
      this._UpdatePressActive(false);
      IList<IInputAction> inputList = this.inputAlgorithms.PressAndHoldAndDragWithAcceleration(start, end, holdDuration, acceleration, packetDelta);
      using (this.timeFactory.Get())
        this.inputQueue.Process(this.inputDevice, inputList);
    }

    public void InjectMTPanWithAcceleration(
      PointI startFingerOne,
      PointI startFingerTwo,
      float direction,
      uint distance,
      uint holdDuration,
      float acceleration,
      uint packetDelta)
    {
      this._UpdatePressActive(false);
      this._UpdatePressActive(false, 1);
      IList<IInputAction> inputList = this.inputAlgorithms.TwoFingerPressAndHoldAndDragWithAcceleration(startFingerOne, startFingerTwo, direction, distance, holdDuration, acceleration, packetDelta);
      using (this.timeFactory.Get())
        this.inputQueue.Process(this.inputDevice, inputList);
    }

    public void InjectMTZoom(
      PointI startFingerOne,
      PointI startFingerTwo,
      float direction,
      uint duration,
      uint distance,
      bool pivotZoom,
      uint packetDelta)
    {
      this._UpdatePressActive(false);
      this._UpdatePressActive(false, 1);
      IList<IInputAction> inputList = this.inputAlgorithms.TwoFingerZoom(startFingerOne, startFingerTwo, direction, duration, distance, pivotZoom, packetDelta);
      using (this.timeFactory.Get())
        this.inputQueue.Process(this.inputDevice, inputList);
    }

    public void InjectMTRotate(
      PointI startFingerOne,
      PointI startFingerTwo,
      float rotationAngle,
      uint duration,
      bool pivotRotate,
      uint packetDelta)
    {
      this._UpdatePressActive(false);
      this._UpdatePressActive(false, 1);
      IList<IInputAction> inputList = this.inputAlgorithms.TwoFingerRotate(startFingerOne, startFingerTwo, rotationAngle, duration, pivotRotate, packetDelta);
      using (this.timeFactory.Get())
        this.inputQueue.Process(this.inputDevice, inputList);
    }

    public void InjectMTTwoFingerPress(
      PointI startFingerOne,
      PointI startFingerTwo,
      uint holdDuration,
      uint tapCount,
      uint tapDelta,
      uint packetDelta)
    {
      this._UpdatePressActive(false);
      this._UpdatePressActive(false, 1);
      IList<IInputAction> inputList = this.inputAlgorithms.TwoFingerPressAndHold(startFingerOne, startFingerTwo, holdDuration, tapCount, tapDelta, packetDelta);
      using (this.timeFactory.Get())
        this.inputQueue.Process(this.inputDevice, inputList);
    }

    public void InjectMTPressAndTap(
      PointI startFingerOne,
      PointI startFingerTwo,
      PointI endFingerOne,
      uint deltaFingerTwoDown,
      uint deltaFingerTwoUp,
      uint deltaFingerOneUp,
      uint packetDelta)
    {
      this._UpdatePressActive(false);
      this._UpdatePressActive(false, 1);
      throw new NotImplementedException();
    }

    public void InjectMultiTouch(MultiTouchInjectionData[] injectionData, uint packetDelta)
    {
      lock (this.downPressLock)
        this.backgroundInputActions.AddRange((IEnumerable<IInputAction>) this.inputAlgorithms.RawMultiTouchGesture(injectionData, packetDelta));
      this._StartDownpressThread();
      while (this.backgroundInputActions.Count > 0)
        Thread.Sleep((int) InputManager.DefaultPressDuration);
    }

    public void InjectDynamicPress(PointI touchPoint, uint contactId)
    {
      this._UpdatePressActive(true, (int) contactId);
      this.downPressLocation[(int) contactId] = touchPoint;
      lock (this.downPressLock)
        this.backgroundInputActions.AddRange((IEnumerable<IInputAction>) this.inputAlgorithms.DynamicPress(touchPoint, contactId));
      this._StartDownpressThread();
      while (this.backgroundInputActions.Count > 0)
        Thread.Sleep((int) InputManager.DefaultPressDuration);
    }

    public void InjectDynamicMove(
      PointI start,
      PointI end,
      uint maxDragDuration,
      uint contactId,
      uint packetDelta)
    {
      IList<IInputAction> inputActionList = (IList<IInputAction>) new List<IInputAction>();
      if (!this.DynamicPressActive())
        return;
      this.backgroundInputActions.AddRange((IEnumerable<IInputAction>) this.inputAlgorithms.DynamicDrag(start, end, maxDragDuration, packetDelta, contactId));
      while (this.backgroundInputActions.Count > 0)
        Thread.Sleep((int) InputManager.DefaultPressDuration);
    }

    public void InjectDynamicRelease(PointI touchPoint, uint contactId)
    {
      if (!this.DynamicPressActive((int) contactId))
        return;
      this._UpdatePressActive(false, (int) contactId);
      IList<IInputAction> inputList = this.inputAlgorithms.DynamicRelease(touchPoint, contactId);
      using (this.timeFactory.Get())
        this.inputQueue.Process(this.inputDevice, inputList);
    }

    public void InjectDynamicPointers(PointerData[] pointerDataArray)
    {
      lock (this.downPressLock)
      {
        Dictionary<uint, PointerData> dictionary = new Dictionary<uint, PointerData>((IDictionary<uint, PointerData>) this._activePointerStates);
        foreach (PointerData pointerData1 in pointerDataArray)
        {
          uint pointerId = pointerData1.pointerId;
          if ((pointerData1.flags & POINTER_FLAGS.ContactDown) == POINTER_FLAGS.ContactDown)
          {
            dictionary[pointerId] = pointerData1;
            PointerData pointerData2 = pointerData1;
            pointerData2.flags = POINTER_FLAGS.ContactMoves;
            this._activePointerStates[pointerId] = pointerData2;
          }
          else if ((pointerData1.flags & POINTER_FLAGS.ContactMoves) == POINTER_FLAGS.ContactMoves)
          {
            if (!this._activePointerStates.ContainsKey(pointerId))
              throw new ArgumentException("$Contact moves cannot take place before contact down: {contactId}");
            dictionary[pointerId] = pointerData1;
            this._activePointerStates[pointerId] = pointerData1;
          }
          else
          {
            dictionary[pointerId] = (pointerData1.flags & POINTER_FLAGS.UP) == POINTER_FLAGS.UP ? pointerData1 : throw new Exception("Unexpected condition!");
            this._activePointerStates.Remove(pointerId);
          }
        }
        PointerData[] pointerDataArray1 = new PointerData[dictionary.Count];
        dictionary.Values.CopyTo(pointerDataArray1, 0);
        this.backgroundInputActions.AddRange((IEnumerable<IInputAction>) this.inputAlgorithms.DynamicPointerActions(pointerDataArray1));
      }
      if (this._activePointerStates.Count <= 0)
        return;
      while (this.backgroundInputActions.Count > 0)
      {
        this._StartDownpressThread();
        Thread.Sleep((int) InputManager.DefaultPacketDelta);
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (this._disposed)
        return;
      if (disposing)
      {
        this.inputDevice.Dispose();
        this.inputDevice = (IInputDevice) null;
      }
      this._disposed = true;
    }

    public static uint DefaultPressDuration => InputManager._defaultPressDuration;

    public static uint DefaultTapDelta => InputManager._defaultTapDelta;

    public static uint DefaultPacketDelta => InputManager._defaultPacketDelta;

    private void _StartDownpressThread()
    {
      if (this.inputDownPressThread != null && this.inputDownPressThread.IsAlive)
        return;
      this.inputDownPressThread = new Thread(this.DownPressThreadDelegate());
      this.inputDownPressThread.Start();
    }

    private IInputAction buildFingerDownUpdateAction()
    {
      IInputAction inputAction;
      if (this._activePointerStates.Count > 0)
      {
        PointerData[] array = new PointerData[this._activePointerStates.Count];
        this._activePointerStates.Values.CopyTo(array, 0);
        inputAction = (IInputAction) new MultiPointerInputAction()
        {
          pointerData = array
        };
      }
      else
      {
        List<PointI> pointIList = new List<PointI>();
        List<uint> uintList = new List<uint>();
        for (int iContactId = 0; iContactId < 10; ++iContactId)
        {
          if (this.DynamicPressActive(iContactId))
          {
            pointIList.Add(this.downPressLocation[iContactId]);
            uintList.Add((uint) iContactId);
          }
        }
        inputAction = this.inputAlgorithms.DynamicMoves((IList<PointI>) pointIList, (IList<uint>) uintList);
      }
      return inputAction;
    }

    private ThreadStart DownPressThreadDelegate() => (ThreadStart) (() =>
    {
      Stopwatch stopwatch = new Stopwatch();
      try
      {
        using (this.timeFactory.Get())
        {
          while (true)
          {
            lock (this.downPressLock)
            {
              stopwatch.Restart();
              if (this.backgroundInputActions.Count == 0 && this._activePointerStates.Count == 0 && !this.DynamicPressActive())
                break;
              IInputAction inputAction = this.backgroundInputActions.Count <= 0 ? this.buildFingerDownUpdateAction() : this.backgroundInputActions[0];
              int num = (int) InputManager.DefaultPacketDelta;
              if (inputAction.GetActionType() == InputActionType.RelativeWaitInputAction)
              {
                num = ((RelativeWaitInputAction) inputAction).duration;
                if (this.DynamicPressActive())
                  this.buildFingerDownUpdateAction().Execute(this.inputDevice, 0);
              }
              else if (inputAction.GetActionType() == InputActionType.AbsoluteWaitInputAction)
              {
                num = ((AbsoluteWaitInputAction) inputAction).duration;
                if (this.DynamicPressActive())
                  this.buildFingerDownUpdateAction().Execute(this.inputDevice, 0);
              }
              else if (inputAction.GetActionType() == InputActionType.MultiPointerInputAction)
              {
                if (this._activePointerStates.Count == 0)
                {
                  PointerData[] pointerData1 = ((MultiPointerInputAction) inputAction).pointerData;
                  for (int index = 0; index < pointerData1.Length && pointerData1[index].pointerId < 10U; ++index)
                  {
                    PointerData pointerData2 = pointerData1[index];
                    this.downPressLocation[(int) pointerData2.pointerId] = pointerData2.location;
                    if ((pointerData2.flags & POINTER_FLAGS.UP) != POINTER_FLAGS.NONE)
                      this._UpdatePressActive(false, (int) pointerData2.pointerId);
                    else if ((pointerData2.flags & POINTER_FLAGS.ContactDown) != POINTER_FLAGS.NONE)
                      this._UpdatePressActive(true, (int) pointerData2.pointerId);
                  }
                }
                inputAction.Execute(this.inputDevice, 0);
              }
              else
              {
                if (inputAction.GetActionType() == InputActionType.PointerInputAction)
                {
                  PointerData pointerData = ((PointerInputAction) inputAction).pointerData;
                  this.downPressLocation[(int) pointerData.pointerId] = pointerData.location;
                  if ((pointerData.flags & POINTER_FLAGS.UP) != POINTER_FLAGS.NONE)
                    this._UpdatePressActive(false, (int) pointerData.pointerId);
                  else if ((pointerData.flags & POINTER_FLAGS.ContactDown) != POINTER_FLAGS.NONE)
                    this._UpdatePressActive(true, (int) pointerData.pointerId);
                }
                inputAction.Execute(this.inputDevice, 0);
                if (this.DynamicPressActive())
                  this.buildFingerDownUpdateAction().Execute(this.inputDevice, 0);
              }
              if (this.backgroundInputActions.Count > 0)
                this.backgroundInputActions.RemoveAt(0);
              if (stopwatch.ElapsedMilliseconds < (long) num)
                Thread.Sleep(num - (int) stopwatch.ElapsedMilliseconds);
            }
          }
        }
      }
      catch (Exception ex)
      {
        this._ClearAllPressActive();
        this._activePointerStates.Clear();
        this.backgroundInputActions.Clear();
      }
    });

    private void DebugLogInputAction(IInputAction act, string context)
    {
      if (act.GetActionType() == InputActionType.MultiPointerInputAction)
      {
        PointerData[] pointerData = ((MultiPointerInputAction) act).pointerData;
        int num = 0;
        while (num < pointerData.Length)
          ++num;
      }
      else
      {
        int actionType1 = (int) act.GetActionType();
        int actionType2 = (int) act.GetActionType();
        int actionType3 = (int) act.GetActionType();
        int actionType4 = (int) act.GetActionType();
        int actionType5 = (int) act.GetActionType();
      }
    }

    private void _UpdatePressActive(bool bActive, int contactId = 0)
    {
      lock (this.downPressLock)
      {
        if (bActive)
          this.pressActive |= 1 << contactId;
        else
          this.pressActive &= 0 << contactId;
      }
    }

    private void _ClearAllPressActive()
    {
      lock (this.downPressLock)
        this.pressActive = 0;
    }

    public bool DynamicPressActive(int iContactId = -1) => iContactId == -1 ? this.pressActive > 0 : (this.pressActive & 1 << iContactId) > 0;
  }
}
