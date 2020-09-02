// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ItemsViewItem`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Controls {
    public abstract class ItemsViewItem<C> : UIObject, ISelectionItem<C>, IVirtualizedItem
        where C : UIObject {
        protected ItemsViewItem(UIObject uiObject, IFactory<C> containerFactory)
            : base(uiObject: uiObject) {
            Initialize(containerFactory: containerFactory);
        }

        internal ItemsViewItem(AutomationElement element, IFactory<C> containerFactory)
            : base(element: element) {
            Initialize(containerFactory: containerFactory);
        }

        protected ISelectionItem<C> SelectionItemProvider { get; set; }

        protected IVirtualizedItem VirtualizedItemProvider { get; set; }

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

        public void Realize() {
            VirtualizedItemProvider.Realize();
        }

        void Initialize(IFactory<C> containerFactory) {
            SelectionItemProvider = new SelectionItemImplementation<C>(uiObject: this, containerFactory: containerFactory);
            VirtualizedItemProvider = new VirtualizedItemImplementation(uiObject: this);
        }
    }
}