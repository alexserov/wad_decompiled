// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SelectionSpinner
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class SelectionSpinner : UIObject, IContainer<SelectionSpinnerItem>, ISelection<SelectionSpinnerItem>
  {
    private ISelection<SelectionSpinnerItem> _selectionPattern;
    private static IFactory<SelectionSpinner> _factory;

    public SelectionSpinner(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal SelectionSpinner(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._selectionPattern = (ISelection<SelectionSpinnerItem>) new SelectionImplementation<SelectionSpinnerItem>((UIObject) this, SelectionSpinnerItem.Factory);

    public UICollection<SelectionSpinnerItem> Items => (UICollection<SelectionSpinnerItem>) new UIChildren<SelectionSpinnerItem>((UIObject) this, UICondition.ControlTree, SelectionSpinnerItem.Factory);

    public UICollection<SelectionSpinnerItem> Selection => this._selectionPattern.Selection;

    public virtual bool CanSelectMultiple => this._selectionPattern.CanSelectMultiple;

    public virtual bool IsSelectionRequired => this._selectionPattern.IsSelectionRequired;

    public static IFactory<SelectionSpinner> Factory
    {
      get
      {
        if (SelectionSpinner._factory == null)
          SelectionSpinner._factory = (IFactory<SelectionSpinner>) new SelectionSpinner.SelectionSpinnerFactory();
        return SelectionSpinner._factory;
      }
    }

    private class SelectionSpinnerFactory : IFactory<SelectionSpinner>
    {
      public SelectionSpinner Create(UIObject element) => new SelectionSpinner(element);
    }
  }
}
