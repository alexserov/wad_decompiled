// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Window
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public class Window : UIObject, ITransform, IWindow {
        static IFactory<Window> _factory;
        ITransform _transformPattern;
        IWindow _windowPattern;

        public Window(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal Window(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<Window> Factory {
            get {
                if (_factory == null)
                    _factory = new WindowFactory();
                return _factory;
            }
        }

        public virtual void Rotate(double degrees) {
            this._transformPattern.Rotate(degrees: degrees);
        }

        public virtual void Resize(double width, double height) {
            this._transformPattern.Resize(width: width, height: height);
        }

        public virtual void Move(double x, double y) {
            this._transformPattern.Move(x: x, y: y);
        }

        public virtual bool CanRotate {
            get { return this._transformPattern.CanRotate; }
        }

        public virtual bool CanResize {
            get { return this._transformPattern.CanResize; }
        }

        public virtual bool CanMove {
            get { return this._transformPattern.CanMove; }
        }

        public virtual void SetWindowVisualState(WindowVisualState state) {
            this._windowPattern.SetWindowVisualState(state: state);
        }

        public virtual void Close() {
            this._windowPattern.Close();
        }

        public UIEventWaiter GetWindowClosedWaiter() {
            return this._windowPattern.GetWindowClosedWaiter();
        }

        public virtual void WaitForInputIdle(int milliseconds) {
            this._windowPattern.WaitForInputIdle(milliseconds: milliseconds);
        }

        public virtual bool CanMaximize {
            get { return this._windowPattern.CanMaximize; }
        }

        public virtual bool CanMinimize {
            get { return this._windowPattern.CanMinimize; }
        }

        public virtual bool IsModal {
            get { return this._windowPattern.IsModal; }
        }

        public virtual WindowVisualState WindowVisualState {
            get { return this._windowPattern.WindowVisualState; }
        }

        public virtual WindowInteractionState WindowInteractionState {
            get { return this._windowPattern.WindowInteractionState; }
        }

        public virtual bool IsTopmost {
            get { return this._windowPattern.IsTopmost; }
        }

        void Initialize() {
            this._transformPattern = new TransformImplementation(uiObject: this);
            this._windowPattern = new WindowImplementation(uiObject: this);
        }

        class WindowFactory : IFactory<Window> {
            public Window Create(UIObject element) {
                return new Window(uiObject: element);
            }
        }
    }
}