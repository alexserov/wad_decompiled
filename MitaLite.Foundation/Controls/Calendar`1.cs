// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Calendar`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public abstract class Calendar<I> : UIObject, IGrid<I>, ISelection<I>, IValue
    where I : UIObject
  {
    private IGrid<I> _gridPattern;
    private ISelection<I> _selectionPattern;
    private IValue _valuePattern;

    protected Calendar(UIObject uiObject, IFactory<I> itemFactory)
      : base(uiObject)
      => this.Initialize(itemFactory);

    internal Calendar(AutomationElement element, IFactory<I> itemFactory)
      : base(element)
      => this.Initialize(itemFactory);

    private void Initialize(IFactory<I> itemFactory)
    {
      this.GridProvider = (IGrid<I>) new GridImplementation<I>((UIObject) this, itemFactory);
      this.SelectionProvider = (ISelection<I>) new SelectionImplementation<I>((UIObject) this, itemFactory);
      this.ValueProvider = (IValue) new ValueImplementation((UIObject) this);
    }

    public virtual UICollection<I> Selection => this.SelectionProvider.Selection;

    public virtual bool CanSelectMultiple => this.SelectionProvider.CanSelectMultiple;

    public virtual bool IsSelectionRequired => this.SelectionProvider.IsSelectionRequired;

    public virtual I GetCell(int row, int column) => this.GridProvider.GetCell(row, column);

    public virtual int RowCount => this.GridProvider.RowCount;

    public virtual int ColumnCount => this.GridProvider.ColumnCount;

    public virtual void SetValue(string value) => this.ValueProvider.SetValue(value);

    public virtual string Value => this.ValueProvider.Value;

    public virtual bool IsReadOnly => this.ValueProvider.IsReadOnly;

    protected IValue ValueProvider
    {
      get => this._valuePattern;
      set => this._valuePattern = value;
    }

    protected IGrid<I> GridProvider
    {
      get => this._gridPattern;
      set => this._gridPattern = value;
    }

    protected ISelection<I> SelectionProvider
    {
      get => this._selectionPattern;
      set => this._selectionPattern = value;
    }
  }
}
