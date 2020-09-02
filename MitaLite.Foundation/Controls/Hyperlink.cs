// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Hyperlink
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public class Hyperlink : UIObject, IInvoke {
        static IFactory<Hyperlink> _factory;
        IInvoke _invokePattern;

        public Hyperlink(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal Hyperlink(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<Hyperlink> Factory {
            get {
                if (_factory == null)
                    _factory = new HyperlinkFactory();
                return _factory;
            }
        }

        public virtual void Invoke() {
            this._invokePattern.Invoke();
        }

        public UIEventWaiter GetInvokedWaiter() {
            return this._invokePattern.GetInvokedWaiter();
        }

        void Initialize() {
            this._invokePattern = new InvokeImplementation(uiObject: this);
        }

        class HyperlinkFactory : IFactory<Hyperlink> {
            public Hyperlink Create(UIObject element) {
                return new Hyperlink(uiObject: element);
            }
        }
    }
}