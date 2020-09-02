// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.StatusBar`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;

namespace MS.Internal.Mita.Foundation.Controls {
    public abstract class StatusBar<I> : UIObject, IGrid<I> where I : UIObject {
        protected StatusBar(UIObject uiObject, IFactory<I> itemFactory)
            : base(uiObject: uiObject) {
            Initialize(itemFactory: itemFactory);
        }

        internal StatusBar(AutomationElement element, IFactory<I> itemFactory)
            : base(element: element) {
            Initialize(itemFactory: itemFactory);
        }

        protected IGrid<I> GridProvider { get; set; }

        public virtual I GetCell(int row, int column) {
            return GridProvider.GetCell(row: row, column: column);
        }

        public virtual int RowCount {
            get { return GridProvider.RowCount; }
        }

        public virtual int ColumnCount {
            get { return GridProvider.ColumnCount; }
        }

        void Initialize(IFactory<I> itemFactory) {
            GridProvider = new GridImplementation<I>(uiObject: this, itemFactory: itemFactory);
        }
    }
}