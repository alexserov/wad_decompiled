// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ListView
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class ListView : ListView<ListView, ListViewItem>
  {
    private static IFactory<ListView> _factory;

    public ListView(UIObject uiObject)
      : base(uiObject, ListViewItem.Factory)
    {
    }

    internal ListView(AutomationElement element)
      : base(element, ListViewItem.Factory)
    {
    }

    public static IFactory<ListView> Factory
    {
      get
      {
        if (ListView._factory == null)
          ListView._factory = (IFactory<ListView>) new ListView.ListViewFactory();
        return ListView._factory;
      }
    }

    private class ListViewFactory : IFactory<ListView>
    {
      public ListView Create(UIObject element) => new ListView(element);
    }
  }
}
