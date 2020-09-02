// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.RadioButton
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public class RadioButton : UIObject, ISelectionItem<UIObject> {
        static IFactory<RadioButton> _factory;
        ISelectionItem<UIObject> _selectionItemPattern;

        public RadioButton(UIObject uiObject)
            : base(uiObject: uiObject) {
            Initialize();
        }

        internal RadioButton(AutomationElement element)
            : base(element: element) {
            Initialize();
        }

        public static IFactory<RadioButton> Factory {
            get {
                if (_factory == null)
                    _factory = new RadioButtonFactory();
                return _factory;
            }
        }

        public virtual void Select() {
            this._selectionItemPattern.Select();
        }

        public virtual void AddToSelection() {
            this._selectionItemPattern.AddToSelection();
        }

        public UIEventWaiter GetAddedToSelectionWaiter() {
            return this._selectionItemPattern.GetAddedToSelectionWaiter();
        }

        public UIEventWaiter GetRemovedFromSelectionWaiter() {
            return this._selectionItemPattern.GetRemovedFromSelectionWaiter();
        }

        public UIEventWaiter GetSelectedWaiter() {
            return this._selectionItemPattern.GetSelectedWaiter();
        }

        public virtual void RemoveFromSelection() {
            this._selectionItemPattern.RemoveFromSelection();
        }

        public virtual bool IsSelected {
            get { return this._selectionItemPattern.IsSelected; }
        }

        public virtual UIObject SelectionContainer {
            get { return this._selectionItemPattern.SelectionContainer; }
        }

        void Initialize() {
            this._selectionItemPattern = new SelectionItemImplementation<UIObject>(uiObject: this, containerFactory: UIObject.Factory);
        }

        class RadioButtonFactory : IFactory<RadioButton> {
            public RadioButton Create(UIObject element) {
                return new RadioButton(uiObject: element);
            }
        }
    }
}