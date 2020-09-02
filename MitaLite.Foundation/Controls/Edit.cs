// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Edit
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using System.Windows.Automation.Text;
using MS.Internal.Mita.Foundation.Patterns;

namespace MS.Internal.Mita.Foundation.Controls {
    public class Edit : UIObject, IText, IValue {
        static IFactory<Edit> _factory;
        IText _textPattern;
        IValue _valuePattern;

        public Edit(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal Edit(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<Edit> Factory {
            get {
                if (_factory == null)
                    _factory = new EditFactory();
                return _factory;
            }
        }

        public virtual bool SupportsTextSelection {
            get { return this._textPattern.SupportsTextSelection; }
        }

        public virtual TextPatternRange DocumentRange {
            get { return this._textPattern.DocumentRange; }
        }

        public virtual TextPatternRange GetSelection() {
            return this._textPattern.GetSelection();
        }

        public virtual TextPatternRange RangeFromPoint(PointI screenLocation) {
            return this._textPattern.RangeFromPoint(screenLocation: screenLocation);
        }

        public virtual TextPatternRange RangeFromChild(UIObject childElement) {
            return this._textPattern.RangeFromChild(childElement: childElement);
        }

        public virtual TextPatternRange GetVisibleRange() {
            return this._textPattern.GetVisibleRange();
        }

        public virtual void SetValue(string value) {
            this._valuePattern.SetValue(value: value);
        }

        public virtual string Value {
            get { return this._valuePattern.Value; }
        }

        public bool IsReadOnly {
            get { return this._valuePattern.IsReadOnly; }
        }

        void Initialize() {
            this._textPattern = new TextImplementation(uiObject: this);
            this._valuePattern = new ValueImplementation(uiObject: this);
        }

        class EditFactory : IFactory<Edit> {
            public Edit Create(UIObject element) {
                return new Edit(uiObject: element);
            }
        }
    }
}