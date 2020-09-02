// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UIObject
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Types;

namespace MS.Internal.Mita.Foundation
{
  public class UIObject
  {
    private AutomationElement _automationElement;
    private static IFactory<UIObject> _factory;

    static UIObject() => Provider.LoadProviders();

    public UIObject(UIObject uiObject)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) uiObject, nameof (uiObject));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) uiObject.AutomationElement, "uiObject.AutomationElement");
      this._automationElement = uiObject.AutomationElement;
    }

    internal UIObject(AutomationElement element)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) element, nameof (element));
      this._automationElement = element;
    }

    public static UIObject Root
    {
      get
      {
        object overridden;
        return ActionHandler.Invoke((UIObject) null, ActionEventArgs.GetDefault(nameof (Root)), out overridden) == ActionResult.Handled ? (UIObject) overridden : new UIObject(AutomationElement.RootElement);
      }
    }

    public static UIObject Focused
    {
      get
      {
        object overridden;
        return ActionHandler.Invoke((UIObject) null, ActionEventArgs.GetDefault(nameof (Focused)), out overridden) == ActionResult.Handled ? (UIObject) overridden : new UIObject(AutomationElement.FocusedElement);
      }
    }

    public static UIObject FromHandle(IntPtr hwnd)
    {
      object overridden;
      return ActionHandler.Invoke((UIObject) null, new ActionEventArgs(nameof (FromHandle), new object[1]
      {
        (object) hwnd
      }), out overridden) == ActionResult.Handled ? (UIObject) overridden : new UIObject(AutomationElement.FromHandle(hwnd));
    }

    public static UIObject FromPoint(PointI pixelLocation)
    {
      object overridden;
      return ActionHandler.Invoke((UIObject) null, new ActionEventArgs(nameof (FromPoint), new object[1]
      {
        (object) pixelLocation
      }), out overridden) == ActionResult.Handled ? (UIObject) overridden : new UIObject(AutomationElement.FromPoint(new Point((double) pixelLocation.X, (double) pixelLocation.Y)));
    }

    public static IFactory<UIObject> Factory
    {
      get
      {
        if (UIObject._factory == null)
          UIObject._factory = (IFactory<UIObject>) new UIObject.UIObjectFactory();
        return UIObject._factory;
      }
    }

    public static bool TrySetConnectionTimeout(TimeSpan timeout) => System.Windows.Automation.Automation.TrySetConnectionTimeout(timeout);

    public static bool TryGetConnectionTimeout(out TimeSpan timeout) => System.Windows.Automation.Automation.TryGetConnectionTimeout(out timeout);

    public static bool TrySetTransactionTimeout(TimeSpan timeout) => System.Windows.Automation.Automation.TrySetTransactionTimeout(timeout);

    public static bool TryGetTransactionTimeout(out TimeSpan timeout) => System.Windows.Automation.Automation.TryGetTransactionTimeout(out timeout);

    public virtual string AcceleratorKey
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (AcceleratorKey)), out overridden) == ActionResult.Handled ? (string) overridden : this._automationElement.GetCurrentPropertyValue(AutomationElement.AcceleratorKeyProperty, false).ToString();
      }
    }

    public virtual string AccessKey
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (AccessKey)), out overridden) == ActionResult.Handled ? (string) overridden : this._automationElement.GetCurrentPropertyValue(AutomationElement.AccessKeyProperty, false).ToString();
      }
    }

    internal AutomationElement AutomationElement => this._automationElement;

    public string AutomationId
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (AutomationId)), out overridden) == ActionResult.Handled ? (string) overridden : this._automationElement.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty, false).ToString();
      }
    }

    public RectangleI BoundingRectangle
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        if (ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (BoundingRectangle)), out overridden) == ActionResult.Handled)
          return (RectangleI) overridden;
        System.Windows.Types.Rect boundingRectangle = this.AutomationElement.Current.BoundingRectangle;
        return new RectangleI((int) boundingRectangle.X, (int) boundingRectangle.Y, (int) boundingRectangle.Width, (int) boundingRectangle.Height);
      }
    }

    public string ClassName
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        if (ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (ClassName)), out overridden) == ActionResult.Handled)
          return (string) overridden;
        return this._automationElement.GetCurrentPropertyValue(AutomationElement.ClassNameProperty, false)?.ToString();
      }
    }

    public ControlType ControlType
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (ControlType)), out overridden) == ActionResult.Handled ? (ControlType) overridden : (ControlType) this._automationElement.GetCurrentPropertyValue(AutomationElement.ControlTypeProperty);
      }
    }

    public CultureInfo Culture
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (Culture)), out overridden) == ActionResult.Handled ? (CultureInfo) overridden : (CultureInfo) this._automationElement.GetCurrentPropertyValue(AutomationElement.CultureProperty);
      }
    }

    public string FrameworkId
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (FrameworkId)), out overridden) == ActionResult.Handled ? (string) overridden : this._automationElement.GetCurrentPropertyValue(AutomationElement.FrameworkIdProperty, false).ToString();
      }
    }

    public bool HasKeyboardFocus
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (HasKeyboardFocus)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.HasKeyboardFocusProperty, false);
      }
    }

    public string HelpText
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (HelpText)), out overridden) == ActionResult.Handled ? (string) overridden : this._automationElement.GetCurrentPropertyValue(AutomationElement.HelpTextProperty, false).ToString();
      }
    }

    public bool IsContentElement
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (IsContentElement)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.IsContentElementProperty, false);
      }
    }

    public bool IsItemContainerPatternAvailable
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (IsItemContainerPatternAvailable)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.IsItemContainerPatternAvailableProperty, false);
      }
    }

    public bool IsVirtualizedItemPatternAvailable
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (IsVirtualizedItemPatternAvailable)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.IsVirtualizedItemPatternAvailableProperty, false);
      }
    }

    public bool IsControlElement
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (IsControlElement)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.IsControlElementProperty, false);
      }
    }

    public bool IsEnabled
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (IsEnabled)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.IsEnabledProperty, false);
      }
    }

    public bool IsKeyboardFocusable
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (IsKeyboardFocusable)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.IsKeyboardFocusableProperty, false);
      }
    }

    public bool IsOffscreen
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (IsOffscreen)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.IsOffscreenProperty, false);
      }
    }

    public bool IsPassword
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (IsPassword)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.IsPasswordProperty, false);
      }
    }

    public bool IsRequiredForForm
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (IsRequiredForForm)), out overridden) == ActionResult.Handled ? (bool) overridden : (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.IsRequiredForFormProperty, false);
      }
    }

    public string ItemStatus
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (ItemStatus)), out overridden) == ActionResult.Handled ? (string) overridden : this._automationElement.GetCurrentPropertyValue(AutomationElement.ItemStatusProperty, false).ToString();
      }
    }

    public string ItemType
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (ItemType)), out overridden) == ActionResult.Handled ? (string) overridden : this._automationElement.GetCurrentPropertyValue(AutomationElement.ItemTypeProperty, false).ToString();
      }
    }

    public UIObject LabeledBy
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        if (ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (LabeledBy)), out overridden) == ActionResult.Handled)
          return (UIObject) overridden;
        AutomationElement currentPropertyValue = (AutomationElement) this._automationElement.GetCurrentPropertyValue(AutomationElement.LabeledByProperty, false);
        return currentPropertyValue != (AutomationElement) null ? new UIObject(currentPropertyValue) : (UIObject) null;
      }
    }

    public string LocalizedControlType
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (LocalizedControlType)), out overridden) == ActionResult.Handled ? (string) overridden : this._automationElement.GetCurrentPropertyValue(AutomationElement.LocalizedControlTypeProperty, false).ToString();
      }
    }

    public virtual string Name
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        if (ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (Name)), out overridden) == ActionResult.Handled)
          return (string) overridden;
        return this._automationElement.GetCurrentPropertyValue(AutomationElement.NameProperty, false)?.ToString();
      }
    }

    public IntPtr NativeWindowHandle
    {
      get
      {
        int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        if (ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (NativeWindowHandle)), out overridden) == ActionResult.Handled)
          return (IntPtr) overridden;
        object currentPropertyValue = this._automationElement.GetCurrentPropertyValue(AutomationElement.NativeWindowHandleProperty, false);
        if (currentPropertyValue == null)
          return IntPtr.Zero;
        return currentPropertyValue is int num2 ? new IntPtr(num2) : (IntPtr) currentPropertyValue;
      }
    }

    public OrientationType Orientation
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (Orientation)), out overridden) == ActionResult.Handled ? (OrientationType) overridden : (OrientationType) this._automationElement.GetCurrentPropertyValue(AutomationElement.OrientationProperty, false);
      }
    }

    public int ProcessId
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (ProcessId)), out overridden) == ActionResult.Handled ? (int) overridden : (int) this._automationElement.GetCurrentPropertyValue(AutomationElement.ProcessIdProperty, false);
      }
    }

    public string RuntimeId
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (RuntimeId)), out overridden) == ActionResult.Handled ? (string) overridden : MS.Internal.Mita.Foundation.Utilities.RuntimeId.StringFromParts(this._automationElement.GetRuntimeId());
      }
    }

    public UICollection<UIObject> Children => (UICollection<UIObject>) new UIChildren<UIObject>(this, UIObject.Factory);

    public UICollection<UIObject> Siblings => (UICollection<UIObject>) new UISiblings<UIObject>(this, UIObject.Factory);

    public UICollection<UIObject> Ancestors => (UICollection<UIObject>) new UIAncestors<UIObject>(this, UIObject.Factory);

    public UICollection<UIObject> Descendants => (UICollection<UIObject>) new UIDepthFirstDescendants<UIObject>(this, UIObject.Factory);

    public UIObject FirstChild
    {
      get
      {
        AutomationElement firstChild = new TreeWalker(Context.Current.TreeCondition.Condition).GetFirstChild(this._automationElement);
        return firstChild == (AutomationElement) null ? (UIObject) null : new UIObject(firstChild);
      }
    }

    public UIObject LastChild
    {
      get
      {
        AutomationElement lastChild = new TreeWalker(Context.Current.TreeCondition.Condition).GetLastChild(this._automationElement);
        return lastChild == (AutomationElement) null ? (UIObject) null : new UIObject(lastChild);
      }
    }

    public UIObject NextSibling
    {
      get
      {
        AutomationElement nextSibling = new TreeWalker(Context.Current.TreeCondition.Condition).GetNextSibling(this._automationElement);
        return nextSibling == (AutomationElement) null ? (UIObject) null : new UIObject(nextSibling);
      }
    }

    public UIObject PreviousSibling
    {
      get
      {
        AutomationElement previousSibling = new TreeWalker(Context.Current.TreeCondition.Condition).GetPreviousSibling(this._automationElement);
        return previousSibling == (AutomationElement) null ? (UIObject) null : new UIObject(previousSibling);
      }
    }

    public UIObject Parent
    {
      get
      {
        AutomationElement parent = new TreeWalker(Context.Current.TreeCondition.Condition).GetParent(this._automationElement);
        return parent == (AutomationElement) null ? (UIObject) null : new UIObject(parent);
      }
    }

    public void Click()
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      PointI fromUpperLeftCorner = this.ComputeOffsetFromUpperLeftCorner();
      this.ClickImplementation(PointerButtons.Primary, (double) fromUpperLeftCorner.X, (double) fromUpperLeftCorner.Y);
    }

    public void Click(PointerButtons button)
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      PointI fromUpperLeftCorner = this.ComputeOffsetFromUpperLeftCorner();
      this.ClickImplementation(button, (double) fromUpperLeftCorner.X, (double) fromUpperLeftCorner.Y);
    }

    public void Click(PointerButtons button, double offsetX, double offsetY)
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      this.ClickImplementation(button, offsetX, offsetY);
    }

    protected void ClickImplementation(PointerButtons button, double offsetX, double offsetY)
    {
      if (ActionHandler.Invoke(this, new ActionEventArgs("Click", new object[3]
      {
        (object) button,
        (object) offsetX,
        (object) offsetY
      })) != ActionResult.Unhandled)
        return;
      this.MovePointer(offsetX, offsetY);
      PointerInput.Click(button, 1);
    }

    public void DoubleClick()
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      PointI fromUpperLeftCorner = this.ComputeOffsetFromUpperLeftCorner();
      this.DoubleClickImplementation(PointerButtons.Primary, (double) fromUpperLeftCorner.X, (double) fromUpperLeftCorner.Y);
    }

    public void DoubleClick(PointerButtons button)
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      PointI fromUpperLeftCorner = this.ComputeOffsetFromUpperLeftCorner();
      this.DoubleClickImplementation(button, (double) fromUpperLeftCorner.X, (double) fromUpperLeftCorner.Y);
    }

    public void DoubleClick(PointerButtons button, double offsetX, double offsetY)
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      this.DoubleClickImplementation(button, offsetX, offsetY);
    }

    protected void DoubleClickImplementation(PointerButtons button, double offsetX, double offsetY)
    {
      if (ActionHandler.Invoke(this, new ActionEventArgs("DoubleClick", new object[3]
      {
        (object) button,
        (object) offsetX,
        (object) offsetY
      })) != ActionResult.Unhandled)
        return;
      this.MovePointer(offsetX, offsetY);
      PointerInput.Click(button, 2);
    }

    public void ClickDrag(UIObject uiObject) => this.ClickDrag(PointerButtons.Primary, uiObject);

    public void ClickDrag(PointerButtons button, UIObject uiObject)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) uiObject, nameof (uiObject));
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this, new ActionEventArgs(nameof (ClickDrag), new object[2]
      {
        (object) button,
        (object) uiObject
      })) != ActionResult.Unhandled)
        return;
      PointerInput.ClickDrag(this, uiObject, button);
    }

    public void MovePointer()
    {
      PointI fromUpperLeftCorner = this.ComputeOffsetFromUpperLeftCorner();
      this.MovePointer((double) fromUpperLeftCorner.X, (double) fromUpperLeftCorner.Y);
    }

    public void MovePointer(double offsetX, double offsetY)
    {
      PointI location = this.BoundingRectangle.Location;
      PointerInput.Move(new PointI((int) ((double) location.X + offsetX), (int) ((double) location.Y + offsetY)));
    }

    public void Tap()
    {
      PointI fromUpperLeftCorner = this.ComputeOffsetFromUpperLeftCorner();
      this.Tap((double) fromUpperLeftCorner.X, (double) fromUpperLeftCorner.Y);
    }

    public virtual void Tap(double offsetX, double offsetY)
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this, new ActionEventArgs(nameof (Tap), new object[2]
      {
        (object) offsetX,
        (object) offsetY
      })) != ActionResult.Unhandled)
        return;
      using (InputController.Activate(PointerInputType.MultiTouch))
        PointerInput.Click(this, PointerButtons.Primary, offsetX, offsetY);
    }

    public void DoubleTap()
    {
      PointI fromUpperLeftCorner = this.ComputeOffsetFromUpperLeftCorner();
      this.DoubleTap((double) fromUpperLeftCorner.X, (double) fromUpperLeftCorner.Y);
    }

    public virtual void DoubleTap(double offsetX, double offsetY)
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this, new ActionEventArgs(nameof (DoubleTap), new object[2]
      {
        (object) offsetX,
        (object) offsetY
      })) != ActionResult.Unhandled)
        return;
      using (InputController.Activate(PointerInputType.MultiTouch))
        PointerInput.DoubleClick(this, PointerButtons.Primary, offsetX, offsetY);
    }

    public void TapAndHold() => this.TapAndHold(SinglePointGesture.DefaultHoldDuration);

    public void TapAndHold(uint duration)
    {
      PointI fromUpperLeftCorner = this.ComputeOffsetFromUpperLeftCorner();
      this.TapAndHold((double) fromUpperLeftCorner.X, (double) fromUpperLeftCorner.Y, duration);
    }

    public void TapAndHold(double offsetX, double offsetY) => this.TapAndHold(offsetX, offsetY, SinglePointGesture.DefaultHoldDuration);

    public virtual void TapAndHold(double offsetX, double offsetY, uint duration)
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this, new ActionEventArgs(nameof (TapAndHold), new object[3]
      {
        (object) offsetX,
        (object) offsetY,
        (object) duration
      })) != ActionResult.Unhandled)
        return;
      SinglePointGesture.Current.Move(this.ObjectPointToScreenPoint(new PointI((int) Math.Round(offsetX), (int) Math.Round(offsetY))));
      SinglePointGesture.PressAndHold(duration);
    }

    public void Pan(float direction) => this.Pan(SinglePointGesture.MinimumPanFlickDistance, direction);

    public void Pan(int distance, float direction) => this.Pan(this.ComputeOffsetFromUpperLeftCorner(), distance, direction);

    public void Pan(PointI startPoint, int distance, float direction)
    {
      double num = (double) direction * Math.PI / 180.0;
      int x = startPoint.X + (int) Math.Round((double) distance * Math.Cos(num));
      int y = startPoint.Y - (int) Math.Round((double) distance * Math.Sin(num));
      this.Pan(startPoint, new PointI(x, y));
    }

    public virtual void Pan(PointI startPoint, PointI endPoint)
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this, new ActionEventArgs(nameof (Pan), new object[2]
      {
        (object) startPoint,
        (object) endPoint
      })) != ActionResult.Unhandled)
        return;
      PointI screenPoint1 = this.ObjectPointToScreenPoint(startPoint);
      PointI screenPoint2 = this.ObjectPointToScreenPoint(endPoint);
      SinglePointGesture.Current.Move(screenPoint1);
      int defaultPressDuration = (int) InputManager.DefaultPressDuration;
      double defaultPanAcceleration = (double) SinglePointGesture.DefaultPanAcceleration;
      SinglePointGesture.Pan(screenPoint2, (uint) defaultPressDuration, (float) defaultPanAcceleration);
    }

    public void Flick(float direction) => this.Flick(SinglePointGesture.MinimumPanFlickDistance, direction);

    public void Flick(PointI startPoint, float direction) => this.Flick(startPoint, SinglePointGesture.MinimumPanFlickDistance, direction);

    public void Flick(int distance, float direction) => this.Flick(this.ComputeOffsetFromUpperLeftCorner(), distance, direction);

    public void Flick(PointI startPoint, int distance, float direction)
    {
      double num = (double) direction * Math.PI / 180.0;
      int x = startPoint.X + (int) Math.Round((double) distance * Math.Cos(num));
      int y = startPoint.Y - (int) Math.Round((double) distance * Math.Sin(num));
      this.Flick(startPoint, new PointI(x, y));
    }

    public virtual void Flick(PointI startPoint, PointI endPoint)
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this, new ActionEventArgs(nameof (Flick), new object[2]
      {
        (object) startPoint,
        (object) endPoint
      })) == ActionResult.Unhandled)
      {
        PointI screenPoint1 = this.ObjectPointToScreenPoint(startPoint);
        PointI screenPoint2 = this.ObjectPointToScreenPoint(endPoint);
        SinglePointGesture.Current.Move(screenPoint1);
        int defaultPressDuration = (int) InputManager.DefaultPressDuration;
        double flickAcceleration = (double) SinglePointGesture.DefaultFlickAcceleration;
        SinglePointGesture.Flick(screenPoint2, (uint) defaultPressDuration, (float) flickAcceleration);
      }
      int num3 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("UIScrollComplete"));
    }

    public void PinchStretch(uint initialDistance, uint finalDistance) => this.PinchStretch(this.ComputeOffsetFromUpperLeftCorner(), initialDistance, finalDistance, MultiPointGesture.DefaultPinchStretchDuration);

    public void PinchStretch(PointI startPoint, uint initialDistance, uint finalDistance) => this.PinchStretch(startPoint, initialDistance, finalDistance, MultiPointGesture.DefaultPinchStretchDuration);

    public virtual void PinchStretch(
      PointI startPoint,
      uint initialDistance,
      uint finalDistance,
      uint duration)
    {
      int num1 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this, new ActionEventArgs(nameof (PinchStretch), new object[4]
      {
        (object) startPoint,
        (object) initialDistance,
        (object) finalDistance,
        (object) duration
      })) != ActionResult.Unhandled)
        return;
      PointI screenPoint = this.ObjectPointToScreenPoint(startPoint);
      if (initialDistance > finalDistance)
      {
        MultiPointGesture.Current.Move(MultiPointGesture.OffsetPinchPoints(screenPoint, this.BoundingRectangle, initialDistance, MultiPointGesture.DefaultPinchStretchDirection));
        MultiPointGesture.Pinch(MultiPointGesture.DefaultPinchStretchDirection, duration, initialDistance, finalDistance);
      }
      else
      {
        MultiPointGesture.Current.Move(screenPoint);
        MultiPointGesture.Stretch(MultiPointGesture.DefaultPinchStretchDirection, duration, initialDistance, finalDistance);
      }
    }

    private PointI ObjectPointToScreenPoint(PointI objectPoint)
    {
      RectangleI boundingRectangle = this.BoundingRectangle;
      boundingRectangle.Offset(objectPoint.X, objectPoint.Y);
      return boundingRectangle.Location;
    }

    public override bool Equals(object obj)
    {
      UIObject uiObject = obj as UIObject;
      if (!(uiObject != (UIObject) null))
        return base.Equals(obj);
      try
      {
        return System.Windows.Automation.Automation.Compare(this._automationElement, uiObject.AutomationElement);
      }
      catch (COMException ex)
      {
        return false;
      }
    }

    public PointI GetClickablePoint()
    {
      int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      object overridden;
      if (ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (GetClickablePoint)), out overridden) == ActionResult.Handled)
        return (PointI) overridden;
      Point pt;
      if (this._automationElement.TryGetClickablePoint(out pt))
        return new PointI((int) pt.X, (int) pt.Y);
      RectangleI boundingRectangle = this.BoundingRectangle;
      return new PointI(boundingRectangle.X + boundingRectangle.Width / 2, boundingRectangle.Y + boundingRectangle.Height / 2);
    }

    public override int GetHashCode()
    {
      int[] runtimeId = this._automationElement.GetRuntimeId();
      int num1 = 0;
      foreach (int num2 in runtimeId)
        num1 = (num1 + num2) * 101;
      return num1;
    }

    public IEnumerable<UIProperty> SupportedProperties => (IEnumerable<UIProperty>) new MS.Internal.Mita.Foundation.SupportedProperties(this);

    public AutomationPattern[] GetSupportedPatterns() => this._automationElement.GetSupportedPatterns();

    public object GetProperty(UIProperty property)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) property, nameof (property));
      return UIObject.WrapValue(this._automationElement.GetCurrentPropertyValue(property.Property, false));
    }

    public object GetCachedProperty(UIProperty property)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) property, nameof (property));
      return UIObject.WrapValue(this._automationElement.GetCachedPropertyValue(property.Property, false));
    }

    public bool Matches(UICondition condition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition, nameof (condition));
      return condition.Matches(this._automationElement);
    }

    public static bool Matches(UIObject uiObject, UICondition condition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) uiObject, nameof (uiObject));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition, nameof (condition));
      return condition.Matches(uiObject.AutomationElement);
    }

    internal static bool Matches(AutomationElement element, UICondition condition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition, nameof (condition));
      return condition.Matches(element);
    }

    public void SaveImage(string fileName)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.StringNeitherNullNorEmpty(fileName, nameof (fileName));
      int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke(this, new ActionEventArgs(nameof (SaveImage), new object[1]
      {
        (object) fileName
      })) != ActionResult.Unhandled)
        return;
      ScreenCapture.SaveImage(this.BoundingRectangle, fileName);
    }

    public void SendKeys(string text)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.StringNeitherNullNorEmpty(text, nameof (text));
      this.SetFocus();
      if (ActionHandler.Invoke(this, new ActionEventArgs(nameof (SendKeys), new object[1]
      {
        (object) text
      })) != ActionResult.Unhandled)
        return;
      TextInput.SendText(text);
    }

    public void SetFocus()
    {
      int num = (int) ActionHandler.Invoke(this, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (SetFocus))) != ActionResult.Unhandled || (bool) this._automationElement.GetCurrentPropertyValue(AutomationElement.HasKeyboardFocusProperty))
        return;
      this._automationElement.SetFocus();
    }

    public override string ToString()
    {
      object overridden;
      if (ActionHandler.Invoke(this, ActionEventArgs.GetDefault(nameof (ToString)), out overridden) == ActionResult.Handled)
        return (string) overridden;
      return "{" + this.Name + ", " + this.ClassName + ", " + this.RuntimeId + "}";
    }

    public static bool operator ==(UIObject firstElement, UIObject secondElement)
    {
      if ((object) firstElement == (object) secondElement)
        return true;
      return (object) firstElement != null && firstElement.Equals((object) secondElement);
    }

    public static bool operator !=(UIObject firstElement, UIObject secondElement)
    {
      if ((object) firstElement == (object) secondElement)
        return false;
      return (object) firstElement == null || !firstElement.Equals((object) secondElement);
    }

    internal static string SafeGetName(UIObject uiObject)
    {
      if (uiObject == (UIObject) null)
        throw new ArgumentNullException(nameof (uiObject));
      string name = StringResource.Get("UIObject_SafeGetNameDefault");
      Thread thread = new Thread((ParameterizedThreadStart) (o =>
      {
        try
        {
          name = uiObject.Name;
        }
        catch
        {
        }
      }));
      thread.Start();
      thread.Join(500);
      return name;
    }

    private PointI ComputeOffsetFromUpperLeftCorner()
    {
      PointI clickablePoint = this.GetClickablePoint();
      return new PointI(clickablePoint.X - this.BoundingRectangle.X, clickablePoint.Y - this.BoundingRectangle.Y);
    }

    private static object WrapValue(object value)
    {
      AutomationElement element;
      if ((element = value as AutomationElement) != (AutomationElement) null)
        return (object) new UIObject(element);
      switch (value)
      {
        case AutomationElement[] automationElementArray:
          return (object) new UICollection<UIObject>((UINavigator) new EnumerableNavigator((IEnumerable) automationElementArray), UIObject.Factory);
        case AutomationElementCollection elementCollection:
          return (object) new UICollection<UIObject>((UINavigator) new EnumerableNavigator((IEnumerable) elementCollection), UIObject.Factory);
        case System.Windows.Types.Rect rect:
          return (object) new RectangleI((int) rect.X, (int) rect.Y, (int) rect.Width, (int) rect.Height);
        default:
          return value;
      }
    }

    private class UIObjectFactory : IFactory<UIObject>
    {
      public UIObject Create(UIObject element) => new UIObject(element);
    }
  }
}
