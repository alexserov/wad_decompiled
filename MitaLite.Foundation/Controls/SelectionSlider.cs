// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SelectionSlider
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class SelectionSlider : UIObject, IContainer<SelectionSliderItem>, ISelection<SelectionSliderItem>
  {
    private ISelection<SelectionSliderItem> _selectionPattern;
    private static IFactory<SelectionSlider> _factory;

    public SelectionSlider(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal SelectionSlider(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._selectionPattern = (ISelection<SelectionSliderItem>) new SelectionImplementation<SelectionSliderItem>((UIObject) this, SelectionSliderItem.Factory);

    public UICollection<SelectionSliderItem> Items => (UICollection<SelectionSliderItem>) new UIChildren<SelectionSliderItem>((UIObject) this, UICondition.ControlTree, SelectionSliderItem.Factory);

    public UICollection<SelectionSliderItem> Selection => this._selectionPattern.Selection;

    public virtual bool CanSelectMultiple => this._selectionPattern.CanSelectMultiple;

    public virtual bool IsSelectionRequired => this._selectionPattern.IsSelectionRequired;

    public static IFactory<SelectionSlider> Factory
    {
      get
      {
        if (SelectionSlider._factory == null)
          SelectionSlider._factory = (IFactory<SelectionSlider>) new SelectionSlider.SelectionSliderFactory();
        return SelectionSlider._factory;
      }
    }

    private class SelectionSliderFactory : IFactory<SelectionSlider>
    {
      public SelectionSlider Create(UIObject element) => new SelectionSlider(element);
    }
  }
}
