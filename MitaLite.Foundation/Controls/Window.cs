// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Window
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class Window : UIObject, ITransform, IWindow
  {
    private ITransform _transformPattern;
    private IWindow _windowPattern;
    private static IFactory<Window> _factory;

    public Window(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal Window(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize()
    {
      this._transformPattern = (ITransform) new TransformImplementation((UIObject) this);
      this._windowPattern = (IWindow) new WindowImplementation((UIObject) this);
    }

    public virtual void Rotate(double degrees) => this._transformPattern.Rotate(degrees);

    public virtual void Resize(double width, double height) => this._transformPattern.Resize(width, height);

    public virtual void Move(double x, double y) => this._transformPattern.Move(x, y);

    public virtual bool CanRotate => this._transformPattern.CanRotate;

    public virtual bool CanResize => this._transformPattern.CanResize;

    public virtual bool CanMove => this._transformPattern.CanMove;

    public virtual void SetWindowVisualState(WindowVisualState state) => this._windowPattern.SetWindowVisualState(state);

    public virtual void Close() => this._windowPattern.Close();

    public UIEventWaiter GetWindowClosedWaiter() => this._windowPattern.GetWindowClosedWaiter();

    public virtual void WaitForInputIdle(int milliseconds) => this._windowPattern.WaitForInputIdle(milliseconds);

    public virtual bool CanMaximize => this._windowPattern.CanMaximize;

    public virtual bool CanMinimize => this._windowPattern.CanMinimize;

    public virtual bool IsModal => this._windowPattern.IsModal;

    public virtual WindowVisualState WindowVisualState => this._windowPattern.WindowVisualState;

    public virtual WindowInteractionState WindowInteractionState => this._windowPattern.WindowInteractionState;

    public virtual bool IsTopmost => this._windowPattern.IsTopmost;

    public static IFactory<Window> Factory
    {
      get
      {
        if (Window._factory == null)
          Window._factory = (IFactory<Window>) new Window.WindowFactory();
        return Window._factory;
      }
    }

    private class WindowFactory : IFactory<Window>
    {
      public Window Create(UIObject element) => new Window(element);
    }
  }
}
