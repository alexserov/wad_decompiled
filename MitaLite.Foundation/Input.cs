// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Input
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Types;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    internal static class Input {
        internal static INativeMethods nativeMethods = new NativeMethods();

        internal static IInputAction CreateWait(int duration) {
            return new AbsoluteWaitInputAction {
                duration = duration
            };
        }

        internal static IList<IInputAction> CreateKeyInput(
            VirtualKey key,
            KeyAction action,
            int duration) {
            var inputActionList = new List<IInputAction>();
            var keyboardInputAction = new KeyboardInputAction();
            if (action == KeyAction.PressAndRelease)
                inputActionList.AddRange(collection: CreateKeyInput(key: key, action: KeyAction.Press, duration: duration));
            keyboardInputAction.keyboardInput.virtualKeyCode = 0;
            keyboardInputAction.keyboardInput.scanCode = KeyTranslator.Instance.GetMakeScanCode(virtualKey: key);
            keyboardInputAction.keyboardInput.flags = RIMNativeMethods.KEY_EVENT_FLAGS.SCANCODE;
            if (key == VirtualKey.VK_NUMLOCK || key == VirtualKey.VK_RSHIFT || (keyboardInputAction.keyboardInput.scanCode & 57344) == 57344)
                keyboardInputAction.keyboardInput.flags |= RIMNativeMethods.KEY_EVENT_FLAGS.EXTENDEDKEY;
            if (action != KeyAction.Press)
                keyboardInputAction.keyboardInput.flags |= RIMNativeMethods.KEY_EVENT_FLAGS.KEYUP;
            keyboardInputAction.keyboardInput.time = 0U;
            keyboardInputAction.keyboardInput.extraInfo = IntPtr.Zero;
            inputActionList.Add(item: keyboardInputAction);
            inputActionList.Add(item: CreateWait(duration: duration));
            return inputActionList;
        }

        internal static IList<IInputAction> CreateKeyInput(
            char character,
            int duration) {
            return new List<IInputAction> {
                new KeyboardInputAction {
                    keyboardInput = {
                        virtualKeyCode = 0,
                        scanCode = character,
                        flags = RIMNativeMethods.KEY_EVENT_FLAGS.UNICODE,
                        time = 0U,
                        extraInfo = IntPtr.Zero
                    }
                },
                CreateWait(duration: duration)
            };
        }

        internal static IList<IInputAction> CreateKeyModifierInputs(
            ModifierKeys modifierKeys,
            bool press,
            int duration) {
            var inputActionList = new List<IInputAction>();
            var action = KeyAction.Release;
            if (press)
                action = KeyAction.Press;
            if ((modifierKeys & ModifierKeys.ShiftFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_SHIFT, action: action, duration: duration));
            if ((modifierKeys & ModifierKeys.ControlFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_CONTROL, action: action, duration: duration));
            if ((modifierKeys & ModifierKeys.AltFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_MENU, action: action, duration: duration));
            if ((modifierKeys & ModifierKeys.LeftShiftFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_LSHIFT, action: action, duration: duration));
            if ((modifierKeys & ModifierKeys.RightShiftFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_RSHIFT, action: action, duration: duration));
            if ((modifierKeys & ModifierKeys.LeftAltFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_LMENU, action: action, duration: duration));
            if ((modifierKeys & ModifierKeys.RightAltFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_RMENU, action: action, duration: duration));
            if ((modifierKeys & ModifierKeys.LeftWindowsFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_LWIN, action: action, duration: duration));
            if ((modifierKeys & ModifierKeys.RightWindowsFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_RWIN, action: action, duration: duration));
            if ((modifierKeys & ModifierKeys.LeftControlFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_LCONTROL, action: action, duration: duration));
            if ((modifierKeys & ModifierKeys.RightControlFlag) != ModifierKeys.None)
                inputActionList.AddRange(collection: CreateKeyInput(key: VirtualKey.VK_RCONTROL, action: action, duration: duration));
            return inputActionList;
        }

        internal static IInputAction CreateMouseMoveInput(
            double absoluteX,
            double absoluteY) {
            Log.Out(msg: "Raw mouse move pixels ({0}, {1})", (object) absoluteX, (object) absoluteY);
            var mouseInputAction = new MouseInputAction();
            var clientArea = nativeMethods.GetClientArea();
            absoluteX = (absoluteX - clientArea.X) * ushort.MaxValue / clientArea.Width;
            absoluteY = (absoluteY - clientArea.Y) * ushort.MaxValue / clientArea.Height;
            mouseInputAction.mouseInput.dx = (int) Math.Round(a: absoluteX);
            mouseInputAction.mouseInput.dy = (int) Math.Round(a: absoluteY);
            mouseInputAction.mouseInput.flags = RIMNativeMethods.MOUSE_EVENT_FLAGS.VIRTUALDESK | RIMNativeMethods.MOUSE_EVENT_FLAGS.ABSOLUTE;
            return mouseInputAction;
        }

        internal static PointI AdjustPointerMoveInput(PointI originalPoint) {
            Log.Out(msg: "Raw pointer move pixels ({0})", (object) originalPoint);
            var clientArea = nativeMethods.GetClientArea();
            return new PointI(x: originalPoint.X - clientArea.X, y: originalPoint.Y - clientArea.Y);
        }

        internal static IInputAction CreateMouseDownInput(
            PointerButtons button,
            bool swapped) {
            var physicalButton = DeterminePhysicalButton(button: button, swapped: swapped);
            var mouseInputAction = new MouseInputAction();
            mouseInputAction.mouseInput.flags = MouseButtonsToMouseInputs(physicalButton: physicalButton, isMouseFlagsDown: true);
            if ((button & PointerButtons.XButton1) != PointerButtons.None)
                mouseInputAction.mouseInput.mouseData |= 1U;
            if ((button & PointerButtons.XButton2) != PointerButtons.None)
                mouseInputAction.mouseInput.mouseData |= 2U;
            return mouseInputAction;
        }

        internal static IInputAction CreateMouseUpInput(PointerButtons button, bool swapped) {
            var physicalButton = DeterminePhysicalButton(button: button, swapped: swapped);
            var mouseInputAction = new MouseInputAction();
            mouseInputAction.mouseInput.flags = MouseButtonsToMouseInputs(physicalButton: physicalButton, isMouseFlagsDown: false);
            if ((button & PointerButtons.XButton1) != PointerButtons.None)
                mouseInputAction.mouseInput.mouseData |= 1U;
            if ((button & PointerButtons.XButton2) != PointerButtons.None)
                mouseInputAction.mouseInput.mouseData |= 2U;
            return mouseInputAction;
        }

        internal static IInputAction CreateMouseRotateWheelInput(int delta) {
            return new MouseInputAction {
                mouseInput = {
                    mouseData = (uint) delta,
                    flags = RIMNativeMethods.MOUSE_EVENT_FLAGS.WHEEL
                }
            };
        }

        static PointerButtons DeterminePhysicalButton(
            PointerButtons button,
            bool swapped) {
            if ((button & PointerButtons.Primary) != PointerButtons.None) {
                button &= ~PointerButtons.Primary;
                button = swapped ? button | PointerButtons.PhysicalRight : button | PointerButtons.PhysicalLeft;
            }

            if ((button & PointerButtons.Secondary) != PointerButtons.None) {
                button &= ~PointerButtons.Secondary;
                button = swapped ? button | PointerButtons.PhysicalLeft : button | PointerButtons.PhysicalRight;
            }

            return button;
        }

        static RIMNativeMethods.MOUSE_EVENT_FLAGS MouseButtonsToMouseInputs(
            PointerButtons physicalButton,
            bool isMouseFlagsDown) {
            var mouseEventFlags = RIMNativeMethods.MOUSE_EVENT_FLAGS.NONE;
            if (isMouseFlagsDown) {
                if ((physicalButton & PointerButtons.PhysicalLeft) != PointerButtons.None)
                    mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.LEFTDOWN;
                if ((physicalButton & PointerButtons.PhysicalRight) != PointerButtons.None)
                    mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.RIGHTDOWN;
                if ((physicalButton & PointerButtons.Middle) != PointerButtons.None)
                    mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.MIDDLEDOWN;
                if ((physicalButton & PointerButtons.XButton1) != PointerButtons.None || (physicalButton & PointerButtons.XButton2) != PointerButtons.None)
                    mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.XDOWN;
            } else {
                if ((physicalButton & PointerButtons.PhysicalLeft) != PointerButtons.None)
                    mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.LEFTUP;
                if ((physicalButton & PointerButtons.PhysicalRight) != PointerButtons.None)
                    mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.RIGHTUP;
                if ((physicalButton & PointerButtons.Middle) != PointerButtons.None)
                    mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.MIDDLEUP;
                if ((physicalButton & PointerButtons.XButton1) != PointerButtons.None || (physicalButton & PointerButtons.XButton2) != PointerButtons.None)
                    mouseEventFlags |= RIMNativeMethods.MOUSE_EVENT_FLAGS.XUP;
            }

            return mouseEventFlags != RIMNativeMethods.MOUSE_EVENT_FLAGS.NONE ? mouseEventFlags : throw new ArgumentException(message: StringResource.Get(id: "InvalidMouseButtonEnum"), paramName: "mouseInputs");
        }

        internal static IList<IInputAction> PreventAccidentalDoubleClick(
            double x,
            double y,
            ref int[] previousRuntimeId) {
            var inputActionList = new List<IInputAction>();
            var runtimeId = AutomationElement.FromPoint(pt: new Point(x: x, y: y)).GetRuntimeId();
            var duration = 550;
            if (previousRuntimeId != null && Automation.Compare(runtimeId1: previousRuntimeId, runtimeId2: runtimeId))
                inputActionList.Add(item: CreateWait(duration: duration));
            else
                previousRuntimeId = runtimeId;
            return inputActionList;
        }
    }
}