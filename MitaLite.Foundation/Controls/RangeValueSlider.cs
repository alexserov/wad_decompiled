// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.RangeValueSlider
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;

namespace MS.Internal.Mita.Foundation.Controls {
    public class RangeValueSlider : UIObject, IRangeValue {
        static IFactory<RangeValueSlider> _factory;
        IRangeValue _rangeValuePattern;

        public RangeValueSlider(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal RangeValueSlider(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<RangeValueSlider> Factory {
            get {
                if (_factory == null)
                    _factory = new RangeValueSliderFactory();
                return _factory;
            }
        }

        public virtual void SetValue(double value) {
            this._rangeValuePattern.SetValue(value: value);
        }

        public virtual double Value {
            get { return this._rangeValuePattern.Value; }
        }

        public virtual bool IsReadOnly {
            get { return this._rangeValuePattern.IsReadOnly; }
        }

        public virtual double Minimum {
            get { return this._rangeValuePattern.Minimum; }
        }

        public virtual double Maximum {
            get { return this._rangeValuePattern.Maximum; }
        }

        public virtual double LargeChange {
            get { return this._rangeValuePattern.LargeChange; }
        }

        public virtual double SmallChange {
            get { return this._rangeValuePattern.SmallChange; }
        }

        void Initialize() {
            this._rangeValuePattern = new RangeValueImplementation(uiObject: this);
        }

        class RangeValueSliderFactory : IFactory<RangeValueSlider> {
            public RangeValueSlider Create(UIObject element) {
                return new RangeValueSlider(uiObject: element);
            }
        }
    }
}