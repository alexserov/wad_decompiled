// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.StatusBar`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public abstract class StatusBar<I> : UIObject, IGrid<I> where I : UIObject
  {
    private IGrid<I> _gridPattern;

    protected StatusBar(UIObject uiObject, IFactory<I> itemFactory)
      : base(uiObject)
      => this.Initialize(itemFactory);

    internal StatusBar(AutomationElement element, IFactory<I> itemFactory)
      : base(element)
      => this.Initialize(itemFactory);

    private void Initialize(IFactory<I> itemFactory) => this.GridProvider = (IGrid<I>) new GridImplementation<I>((UIObject) this, itemFactory);

    public virtual I GetCell(int row, int column) => this.GridProvider.GetCell(row, column);

    public virtual int RowCount => this.GridProvider.RowCount;

    public virtual int ColumnCount => this.GridProvider.ColumnCount;

    protected IGrid<I> GridProvider
    {
      get => this._gridPattern;
      set => this._gridPattern = value;
    }
  }
}
