// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ComboBox`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public abstract class ComboBox<C, I> : UIObject, IExpandCollapse, IValue, ISelection<I>, IContainer<I>, IVirtualizedContainer<I>
        where C : UIObject
        where I : ListItem<C> {
        IValue _valuePattern;

        protected ComboBox(UIObject uiObject, IFactory<I> itemFactory)
            : base(uiObject: uiObject) {
            Initialize(itemFactory: itemFactory);
        }

        internal ComboBox(AutomationElement element, IFactory<I> itemFactory)
            : base(element: element) {
            Initialize(itemFactory: itemFactory);
        }

        protected static UICondition TreeCondition { get; } = UICondition.Create(query: "@ControlType=ComboBox or @ControlType=ListItem");

        protected IFactory<I> ItemFactory { get; set; }

        protected IExpandCollapse ExpandCollapseProvider { get; set; }

        protected ISelection<I> SelectionProvider { get; set; }

        IValue ValueProvider {
            get {
                if (this._valuePattern == null) {
                    if (!(bool) AutomationElement.GetCurrentPropertyValue(property: AutomationElement.IsValuePatternAvailableProperty))
                        throw new PatternNotFoundException(message: "Value Pattern Not found on Combobox");
                    this._valuePattern = new ValueImplementation(uiObject: this);
                }

                return this._valuePattern;
            }
            set { this._valuePattern = value; }
        }

        public virtual UICollection<I> Items {
            get { return new UIBreadthFirstDescendants<I>(root: this, treeCondition: TreeCondition, factory: ItemFactory); }
        }

        public virtual void Collapse() {
            ExpandCollapseProvider.Collapse();
        }

        public virtual void Expand() {
            ExpandCollapseProvider.Expand();
        }

        public virtual ExpandCollapseState ExpandCollapseState {
            get { return ExpandCollapseProvider.ExpandCollapseState; }
        }

        public UIEventWaiter GetCollapsedWaiter() {
            return ExpandCollapseProvider.GetCollapsedWaiter();
        }

        public UIEventWaiter GetExpandedWaiter() {
            return ExpandCollapseProvider.GetExpandedWaiter();
        }

        public virtual UICollection<I> Selection {
            get { return SelectionProvider.Selection; }
        }

        public virtual bool CanSelectMultiple {
            get { return SelectionProvider.CanSelectMultiple; }
        }

        public virtual bool IsSelectionRequired {
            get { return SelectionProvider.IsSelectionRequired; }
        }

        public virtual void SetValue(string value) {
            ValueProvider.SetValue(value: value);
        }

        public virtual string Value {
            get { return ValueProvider.Value; }
        }

        public virtual bool IsReadOnly {
            get { return ValueProvider.IsReadOnly; }
        }

        public UICollection<I> AllItems {
            get { return new UIItemContainerChildren<I>(root: this, treeCondition: TreeCondition, factory: ItemFactory); }
        }

        void Initialize(IFactory<I> itemFactory) {
            Validate.ArgumentNotNull(parameter: itemFactory, parameterName: nameof(itemFactory));
            ItemFactory = itemFactory;
            ExpandCollapseProvider = new ExpandCollapseImplementation(uiObject: this);
            SelectionProvider = new SelectionImplementation<I>(uiObject: this, itemFactory: itemFactory);
            ValueProvider = null;
        }
    }
}