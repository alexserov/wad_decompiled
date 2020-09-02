// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Tab
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class Tab : Tab<Tab, TabItem>
  {
    private static IFactory<Tab> _factory;

    public Tab(UIObject uiObject)
      : base(uiObject, TabItem.Factory)
    {
    }

    internal Tab(AutomationElement element)
      : base(element, TabItem.Factory)
    {
    }

    public static IFactory<Tab> Factory
    {
      get
      {
        if (Tab._factory == null)
          Tab._factory = (IFactory<Tab>) new Tab.TabFactory();
        return Tab._factory;
      }
    }

    private class TabFactory : IFactory<Tab>
    {
      public Tab Create(UIObject element) => new Tab(element);
    }
  }
}
