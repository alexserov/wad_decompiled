// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ItemsViewItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class ItemsViewItem : ItemsViewItem<ItemsView>
  {
    private static IFactory<ItemsViewItem> _factory;

    public ItemsViewItem(UIObject uiObject)
      : base(uiObject, ItemsView.Factory)
    {
    }

    internal ItemsViewItem(AutomationElement element)
      : base(element, ItemsView.Factory)
    {
    }

    public static IFactory<ItemsViewItem> Factory
    {
      get
      {
        if (ItemsViewItem._factory == null)
          ItemsViewItem._factory = (IFactory<ItemsViewItem>) new ItemsViewItem.ItemsViewItemFactory();
        return ItemsViewItem._factory;
      }
    }

    private class ItemsViewItemFactory : IFactory<ItemsViewItem>
    {
      public ItemsViewItem Create(UIObject element) => new ItemsViewItem(element);
    }
  }
}
