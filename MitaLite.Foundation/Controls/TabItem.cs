// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.TabItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class TabItem : TabItem<Tab>
  {
    private static IFactory<TabItem> _factory;

    public TabItem(UIObject uiObject)
      : base(uiObject, Tab.Factory)
    {
    }

    internal TabItem(AutomationElement element)
      : base(element, Tab.Factory)
    {
    }

    public static IFactory<TabItem> Factory
    {
      get
      {
        if (TabItem._factory == null)
          TabItem._factory = (IFactory<TabItem>) new TabItem.TabItemFactory();
        return TabItem._factory;
      }
    }

    private class TabItemFactory : IFactory<TabItem>
    {
      public TabItem Create(UIObject element) => new TabItem(element);
    }
  }
}
