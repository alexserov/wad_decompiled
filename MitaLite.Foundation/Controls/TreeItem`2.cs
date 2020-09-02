// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.TreeItem`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public abstract class TreeItem<C, I> : UIObject, IContainer<I>, IExpandCollapse, ISelectionItem<C>
        where C : UIObject
        where I : UIObject {
        protected TreeItem(UIObject uiObject, IFactory<C> containerFactory, IFactory<I> itemFactory)
            : base(uiObject: uiObject) {
            Initialize(containerFactory: containerFactory, itemFactory: itemFactory);
        }

        internal TreeItem(
            AutomationElement element,
            IFactory<C> containerFactory,
            IFactory<I> itemFactory)
            : base(element: element) {
            Initialize(containerFactory: containerFactory, itemFactory: itemFactory);
        }

        protected IFactory<I> ItemFactory { get; set; }

        protected IExpandCollapse ExpandCollapseProvider { get; set; }

        protected ISelectionItem<C> SelectionItemProvider { get; set; }

        protected static UICondition TreeCondition { get; } = UICondition.Create(query: "@ControlType=TreeItem");

        public virtual UICollection<I> Items {
            get { return new UIChildren<I>(root: this, treeCondition: TreeCondition, factory: ItemFactory); }
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

        public virtual void Select() {
            SelectionItemProvider.Select();
        }

        public virtual void AddToSelection() {
            SelectionItemProvider.AddToSelection();
        }

        public UIEventWaiter GetAddedToSelectionWaiter() {
            return SelectionItemProvider.GetAddedToSelectionWaiter();
        }

        public UIEventWaiter GetRemovedFromSelectionWaiter() {
            return SelectionItemProvider.GetRemovedFromSelectionWaiter();
        }

        public UIEventWaiter GetSelectedWaiter() {
            return SelectionItemProvider.GetSelectedWaiter();
        }

        public virtual void RemoveFromSelection() {
            SelectionItemProvider.RemoveFromSelection();
        }

        public virtual bool IsSelected {
            get { return SelectionItemProvider.IsSelected; }
        }

        public virtual C SelectionContainer {
            get { return SelectionItemProvider.SelectionContainer; }
        }

        void Initialize(IFactory<C> containerFactory, IFactory<I> itemFactory) {
            Validate.ArgumentNotNull(parameter: itemFactory, parameterName: nameof(itemFactory));
            ItemFactory = itemFactory;
            ExpandCollapseProvider = new ExpandCollapseImplementation(uiObject: this);
            SelectionItemProvider = new SelectionItemImplementation<C>(uiObject: this, containerFactory: containerFactory);
        }
    }
}