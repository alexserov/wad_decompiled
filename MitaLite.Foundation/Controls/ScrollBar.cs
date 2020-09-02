// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ScrollBar
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;

namespace MS.Internal.Mita.Foundation.Controls {
    public class ScrollBar : UIObject, IRangeValue {
        static IFactory<ScrollBar> _factory;

        public ScrollBar(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal ScrollBar(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<ScrollBar> Factory {
            get {
                if (_factory == null)
                    _factory = new ScrollBarFactory();
                return _factory;
            }
        }

        protected IRangeValue RangeValueProvider { get; set; }

        public virtual void SetValue(double value) {
            RangeValueProvider.SetValue(value: value);
        }

        public virtual double Value {
            get { return RangeValueProvider.Value; }
        }

        public virtual bool IsReadOnly {
            get { return RangeValueProvider.IsReadOnly; }
        }

        public virtual double Minimum {
            get { return RangeValueProvider.Minimum; }
        }

        public virtual double Maximum {
            get { return RangeValueProvider.Maximum; }
        }

        public virtual double LargeChange {
            get { return RangeValueProvider.LargeChange; }
        }

        public virtual double SmallChange {
            get { return RangeValueProvider.SmallChange; }
        }

        void Initialize() {
            RangeValueProvider = new RangeValueImplementation(uiObject: this);
        }

        class ScrollBarFactory : IFactory<ScrollBar> {
            public ScrollBar Create(UIObject element) {
                return new ScrollBar(uiObject: element);
            }
        }
    }
}