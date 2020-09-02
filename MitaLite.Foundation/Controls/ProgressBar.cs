// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ProgressBar
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;

namespace MS.Internal.Mita.Foundation.Controls {
    public class ProgressBar : UIObject, IValue {
        static IFactory<ProgressBar> _factory;
        IValue _valuePattern;

        public ProgressBar(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal ProgressBar(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<ProgressBar> Factory {
            get {
                if (_factory == null)
                    _factory = new ProgressBarFactory();
                return _factory;
            }
        }

        public virtual void SetValue(string value) {
            this._valuePattern.SetValue(value: value);
        }

        public virtual string Value {
            get { return this._valuePattern.Value; }
        }

        public virtual bool IsReadOnly {
            get { return this._valuePattern.IsReadOnly; }
        }

        void Initialize() {
            this._valuePattern = new ValueImplementation(uiObject: this);
        }

        class ProgressBarFactory : IFactory<ProgressBar> {
            public ProgressBar Create(UIObject element) {
                return new ProgressBar(uiObject: element);
            }
        }
    }
}