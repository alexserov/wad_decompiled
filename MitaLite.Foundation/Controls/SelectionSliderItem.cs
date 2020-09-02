// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SelectionSliderItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class SelectionSliderItem : ListItem<SelectionSlider>
  {
    private static IFactory<SelectionSliderItem> _factory;

    public SelectionSliderItem(UIObject uiObject)
      : base(uiObject, SelectionSlider.Factory)
    {
    }

    internal SelectionSliderItem(AutomationElement element)
      : base(element, SelectionSlider.Factory)
    {
    }

    public static IFactory<SelectionSliderItem> Factory
    {
      get
      {
        if (SelectionSliderItem._factory == null)
          SelectionSliderItem._factory = (IFactory<SelectionSliderItem>) new SelectionSliderItem.SelectionSliderItemFactory();
        return SelectionSliderItem._factory;
      }
    }

    private class SelectionSliderItemFactory : IFactory<SelectionSliderItem>
    {
      public SelectionSliderItem Create(UIObject element) => new SelectionSliderItem(element);
    }
  }
}
