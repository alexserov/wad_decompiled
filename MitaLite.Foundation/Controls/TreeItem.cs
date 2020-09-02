// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.TreeItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class TreeItem : TreeItem<Tree, TreeItem>
  {
    private static IFactory<TreeItem> _factory;

    public TreeItem(UIObject uiObject)
      : base(uiObject, Tree.Factory, TreeItem.Factory)
    {
    }

    internal TreeItem(AutomationElement element)
      : base(element, Tree.Factory, TreeItem.Factory)
    {
    }

    public static IFactory<TreeItem> Factory
    {
      get
      {
        if (TreeItem._factory == null)
          TreeItem._factory = (IFactory<TreeItem>) new TreeItem.TreeItemFactory();
        return TreeItem._factory;
      }
    }

    private class TreeItemFactory : IFactory<TreeItem>
    {
      public TreeItem Create(UIObject element) => new TreeItem(element);
    }
  }
}
