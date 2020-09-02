// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ListBoxItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class ListBoxItem : ListItem<ListBox>
  {
    private static IFactory<ListBoxItem> _factory;

    public ListBoxItem(UIObject uiObject)
      : base(uiObject, ListBox.Factory)
    {
    }

    internal ListBoxItem(AutomationElement element)
      : base(element, ListBox.Factory)
    {
    }

    public static IFactory<ListBoxItem> Factory
    {
      get
      {
        if (ListBoxItem._factory == null)
          ListBoxItem._factory = (IFactory<ListBoxItem>) new ListBoxItem.ListBoxItemFactory();
        return ListBoxItem._factory;
      }
    }

    private class ListBoxItemFactory : IFactory<ListBoxItem>
    {
      public ListBoxItem Create(UIObject element) => new ListBoxItem(element);
    }
  }
}
