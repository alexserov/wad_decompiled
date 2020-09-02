// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ItemsView
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class ItemsView : ItemsView<ItemsView, ItemsViewItem>
  {
    private static IFactory<ItemsView> _factory;

    public ItemsView(UIObject uiObject)
      : base(uiObject, ItemsViewItem.Factory)
    {
    }

    internal ItemsView(AutomationElement element)
      : base(element, ItemsViewItem.Factory)
    {
    }

    public static IFactory<ItemsView> Factory
    {
      get
      {
        if (ItemsView._factory == null)
          ItemsView._factory = (IFactory<ItemsView>) new ItemsView.ItemsViewFactory();
        return ItemsView._factory;
      }
    }

    private class ItemsViewFactory : IFactory<ItemsView>
    {
      public ItemsView Create(UIObject element) => new ItemsView(element);
    }
  }
}
