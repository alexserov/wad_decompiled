// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.RadioButton
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class RadioButton : UIObject, ISelectionItem<UIObject>
  {
    private ISelectionItem<UIObject> _selectionItemPattern;
    private static IFactory<RadioButton> _factory;

    public RadioButton(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal RadioButton(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._selectionItemPattern = (ISelectionItem<UIObject>) new SelectionItemImplementation<UIObject>((UIObject) this, UIObject.Factory);

    public virtual void Select() => this._selectionItemPattern.Select();

    public virtual void AddToSelection() => this._selectionItemPattern.AddToSelection();

    public UIEventWaiter GetAddedToSelectionWaiter() => this._selectionItemPattern.GetAddedToSelectionWaiter();

    public UIEventWaiter GetRemovedFromSelectionWaiter() => this._selectionItemPattern.GetRemovedFromSelectionWaiter();

    public UIEventWaiter GetSelectedWaiter() => this._selectionItemPattern.GetSelectedWaiter();

    public virtual void RemoveFromSelection() => this._selectionItemPattern.RemoveFromSelection();

    public virtual bool IsSelected => this._selectionItemPattern.IsSelected;

    public virtual UIObject SelectionContainer => this._selectionItemPattern.SelectionContainer;

    public static IFactory<RadioButton> Factory
    {
      get
      {
        if (RadioButton._factory == null)
          RadioButton._factory = (IFactory<RadioButton>) new RadioButton.RadioButtonFactory();
        return RadioButton._factory;
      }
    }

    private class RadioButtonFactory : IFactory<RadioButton>
    {
      public RadioButton Create(UIObject element) => new RadioButton(element);
    }
  }
}
