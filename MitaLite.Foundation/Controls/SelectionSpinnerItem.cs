// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SelectionSpinnerItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class SelectionSpinnerItem : ListItem<SelectionSpinner>
  {
    private static IFactory<SelectionSpinnerItem> _factory;

    public SelectionSpinnerItem(UIObject uiObject)
      : base(uiObject, SelectionSpinner.Factory)
    {
    }

    internal SelectionSpinnerItem(AutomationElement element)
      : base(element, SelectionSpinner.Factory)
    {
    }

    public static IFactory<SelectionSpinnerItem> Factory
    {
      get
      {
        if (SelectionSpinnerItem._factory == null)
          SelectionSpinnerItem._factory = (IFactory<SelectionSpinnerItem>) new SelectionSpinnerItem.SelectionSpinnerItemFactory();
        return SelectionSpinnerItem._factory;
      }
    }

    private class SelectionSpinnerItemFactory : IFactory<SelectionSpinnerItem>
    {
      public SelectionSpinnerItem Create(UIObject element) => new SelectionSpinnerItem(element);
    }
  }
}
