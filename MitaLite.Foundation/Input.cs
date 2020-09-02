// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Input
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Types;

namespace MS.Internal.Mita.Foundation
{
  internal static class Input
  {
    internal static INativeMethods nativeMethods = (INativeMethods) new NativeMethods();

    internal static IInputAction CreateWait(int duration) => (IInputAction) new AbsoluteWaitInputAction()
    {
      duration = duration
    };

    internal static IList<IInputAction> CreateKeyInput(
      VirtualKey key,
      KeyAction action,
      int duration)
    {
      List<IInputAction> inputActionList = new List<IInputAction>();
      KeyboardInputAction keyboardInputAction = new KeyboardInputAction();
      if (action == KeyAction.PressAndRelease)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(key, KeyAction.Press, duration));
      keyboardInputAction.keyboardInput.virtualKeyCode = (ushort) 0;
      keyboardInputAction.keyboardInput.scanCode = KeyTranslator.Instance.GetMakeScanCode(key);
      keyboardInputAction.keyboardInput.flags = RIMNativeMethods.KEY_EVENT_FLAGS.SCANCODE;
      if (key == VirtualKey.VK_NUMLOCK || key == VirtualKey.VK_RSHIFT || ((int) keyboardInputAction.keyboardInput.scanCode & 57344) == 57344)
        keyboardInputAction.keyboardInput.flags |= RIMNativeMethods.KEY_EVENT_FLAGS.EXTENDEDKEY;
      if (action != KeyAction.Press)
        keyboardInputAction.keyboardInput.flags |= RIMNativeMethods.KEY_EVENT_FLAGS.KEYUP;
      keyboardInputAction.keyboardInput.time = 0U;
      keyboardInputAction.keyboardInput.extraInfo = IntPtr.Zero;
      inputActionList.Add((IInputAction) keyboardInputAction);
      inputActionList.Add(Input.CreateWait(duration));
      return (IList<IInputAction>) inputActionList;
    }

    internal static IList<IInputAction> CreateKeyInput(
      char character,
      int duration) => (IList<IInputAction>) new List<IInputAction>()
    {
      (IInputAction) new KeyboardInputAction()
      {
        keyboardInput = {
          virtualKeyCode = (ushort) 0,
          scanCode = (ushort) character,
          flags = RIMNativeMethods.KEY_EVENT_FLAGS.UNICODE,
          time = 0U,
          extraInfo = IntPtr.Zero
        }
      },
      Input.CreateWait(duration)
    };

    internal static IList<IInputAction> CreateKeyModifierInputs(
      ModifierKeys modifierKeys,
      bool press,
      int duration)
    {
      List<IInputAction> inputActionList = new List<IInputAction>();
      KeyAction action = KeyAction.Release;
      if (press)
        action = KeyAction.Press;
      if ((modifierKeys & ModifierKeys.ShiftFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_SHIFT, action, duration));
      if ((modifierKeys & ModifierKeys.ControlFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_CONTROL, action, duration));
      if ((modifierKeys & ModifierKeys.AltFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_MENU, action, duration));
      if ((modifierKeys & ModifierKeys.LeftShiftFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_LSHIFT, action, duration));
      if ((modifierKeys & ModifierKeys.RightShiftFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_RSHIFT, action, duration));
      if ((modifierKeys & ModifierKeys.LeftAltFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_LMENU, action, duration));
      if ((modifierKeys & ModifierKeys.RightAltFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_RMENU, action, duration));
      if ((modifierKeys & ModifierKeys.LeftWindowsFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_LWIN, action, duration));
      if ((modifierKeys & ModifierKeys.RightWindowsFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_RWIN, action, duration));
      if ((modifierKeys & ModifierKeys.LeftControlFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_LCONTROL, action, duration));
      if ((modifierKeys & ModifierKeys.RightControlFlag) != ModifierKeys.None)
        inputActionList.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_RCONTROL, action, duration));
      return (IList<IInputAction>) inputActionList;
    }

    internal static IInputAction CreateMouseMoveInput(
      double absoluteX,
      double absoluteY)
    {
      Log.Out("Raw mouse move pixels ({0}, {1})", (object) absoluteX, (object) absoluteY);
      MouseInputAction mouseInputAction = new MouseInputAction();
      RectangleI clientArea = Input.nativeMethods.GetClientArea();
      absoluteX = (absoluteX - (double) clientArea.X) * (double) ushort.MaxValue / (double) clientArea.Width;
      absoluteY = (absoluteY - (double) clientArea.Y) * (double) ushort.MaxValue / (double) clientArea.Height;
      mouseInputAction.mouseInput.dx = (int) Math.Round(absoluteX);
      mouseInputAction.mouseInput.dy = (int) Math.Round(absoluteY);
      mouseInputAction.mouseInput.flags = RIMNativeMethods.MOUSE_EVENT_FLAGS.VIRTUALDESK | RIMNativeMethods.MOUSE_EVENT_FLAGS.ABSOLUTE;
      return (IInputAction) mouseInputAction;
    }

    internal static PointI AdjustPointerMoveInput(PointI originalPoint)
    {
      Log.Out("Raw pointer move pixels ({0})", (object) originalPoint);
      RectangleI clientArea = Input.nativeMethods.GetClientArea();
      return new PointI(originalPoint.X - clientArea.X, originalPoint.Y - clientArea.Y);
    }

    internal static IInputAction CreateMouseDownInput(
      PointerButtons button,
      bool swapped)
    {
      PointerButtons physicalButton = Input.DeterminePhysicalButton(button, swapped);
      MouseInputAction mouseInputAction = new MouseInputAction();
      mouseInputAction.mouseInput.flags = Input.MouseButtonsToMouseInputs(physicalButton, true);
      if ((button & PointerButtons.XButton1) != PointerButtons.None)
        mouseInputAction.mouseInput.mouseData |= 1U;
      if ((button & PointerButtons.XButton2) != PointerButtons.None)
        mouseInputAction.mouseInput.mouseData |= 2U;
      return (IInputAction) mouseInputAction;
    }

    internal static IInputAction CreateMouseUpInput(PointerButtons button, bool swapped)
    {
      PointerButtons physicalButton = Input.DeterminePhysicalButton(button, swapped);
      MouseInputAction mouseInputAction = new MouseInputAction();
      mouseInputAction.mouseInput.flags = Input.MouseButtonsToMouseInputs(physicalButton, false);
      if ((button & PointerButtons.XButton1) != PointerButtons.None)
        mouseInputAction.mouseInput.mouseData |= 1U;
      if ((button & PointerButtons.XButton2) != PointerButtons.None)
        mouseInputAction.mouseInput.mouseData |= 2U;
      return (IInputAction) mouseInputAction;
    }

    internal static IInputAction CreateMouseRotateWheelInput(int delta) => (IInputAction) new MouseInputAction()
    {
      mouseInput = {
        mouseData = (uint) delta,
        flags = RIMNativeMethods.MOUSE_EVENT_FLAGS.WHEEL
      }
    };

    private static PointerButtons DeterminePhysicalButton(
      PointerButtons button,
      bool swapped)
    {
      if ((button & PointerButtons.Primary) != PointerButtons.None)
      {
        button &= ~PointerButtons.Primary;
        button = swapped ? button | PointerButtons.PhysicalRight : button | PointerButtons.PhysicalLeft;
      }
      if ((button & PointerButtons.Secondary) != PointerButtons.None)
      {
        button &= ~PointerButtons.Secondary;
        button = swapped ? button | PointerButtons.PhysicalLeft : button | PointerButtons.PhysicalRight;
      }
      return button;
    }

    private static RIMNativeMethods.MOUSE_EVENT_FLAGS MouseButtonsToMouseInputs(
      PointerButtons physicalButton,
      bool isMouseFlagsDown)
    {
      RIMNativeMethods.MOUSE_EVENT_FLAGS mouseEventFlags = RIMNativeMethods.MOUSE_EVENT_FLAGS.NONE;
      if (isMouseFlagsDown)
      {
        if ((physicalButton & PointerButtons.PhysicalLeft) != PointerButtons.None)
          mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.LEFTDOWN;
        if ((physicalButton & PointerButtons.PhysicalRight) != PointerButtons.None)
          mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.RIGHTDOWN;
        if ((physicalButton & PointerButtons.Middle) != PointerButtons.None)
          mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.MIDDLEDOWN;
        if ((physicalButton & PointerButtons.XButton1) != PointerButtons.None || (physicalButton & PointerButtons.XButton2) != PointerButtons.None)
          mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.XDOWN;
      }
      else
      {
        if ((physicalButton & PointerButtons.PhysicalLeft) != PointerButtons.None)
          mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.LEFTUP;
        if ((physicalButton & PointerButtons.PhysicalRight) != PointerButtons.None)
          mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.RIGHTUP;
        if ((physicalButton & PointerButtons.Middle) != PointerButtons.None)
          mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.MIDDLEUP;
        if ((physicalButton & PointerButtons.XButton1) != PointerButtons.None || (physicalButton & PointerButtons.XButton2) != PointerButtons.None)
          mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.XUP;
      }
      return mouseEventFlags != RIMNativeMethods.MOUSE_EVENT_FLAGS.NONE ? mouseEventFlags : throw new ArgumentException(StringResource.Get("InvalidMouseButtonEnum"), "mouseInputs");
    }

    internal static IList<IInputAction> PreventAccidentalDoubleClick(
      double x,
      double y,
      ref int[] previousRuntimeId)
    {
      List<IInputAction> inputActionList = new List<IInputAction>();
      int[] runtimeId = AutomationElement.FromPoint(new Point(x, y)).GetRuntimeId();
      int duration = 550;
      if (previousRuntimeId != null && System.Windows.Automation.Automation.Compare(previousRuntimeId, runtimeId))
        inputActionList.Add(Input.CreateWait(duration));
      else
        previousRuntimeId = runtimeId;
      return (IList<IInputAction>) inputActionList;
    }
  }
}
