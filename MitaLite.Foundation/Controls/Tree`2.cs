// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Tree`2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Controls {
    public abstract class Tree<C, I> : UIObject, IContainer<I>, ISelection<I>
        where C : UIObject
        where I : TreeItem<C, I> {
        protected Tree(UIObject uiObject, IFactory<I> itemFactory)
            : base(uiObject: uiObject) {
            Initialize(itemFactory: itemFactory);
        }

        internal Tree(AutomationElement element, IFactory<I> itemFactory)
            : base(element: element) {
            Initialize(itemFactory: itemFactory);
        }

        protected IFactory<I> ItemFactory { get; set; }

        protected ISelection<I> SelectionProvider { get; set; }

        protected static UICondition TreeCondition { get; } = UICondition.Create(query: "@ControlType=Tree Or @ControlType=TreeItem");

        public virtual UICollection<I> Items {
            get { return new UIChildren<I>(root: this, treeCondition: TreeCondition, factory: ItemFactory); }
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

        void Initialize(IFactory<I> itemFactory) {
            Validate.ArgumentNotNull(parameter: itemFactory, parameterName: nameof(itemFactory));
            ItemFactory = itemFactory;
            SelectionProvider = new SelectionImplementation<I>(uiObject: this, itemFactory: itemFactory);
        }
    }
}