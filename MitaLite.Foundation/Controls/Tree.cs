// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Tree
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class Tree : Tree<Tree, TreeItem>
  {
    private static IFactory<Tree> _factory;

    public Tree(UIObject uiObject)
      : base(uiObject, TreeItem.Factory)
    {
    }

    internal Tree(AutomationElement element)
      : base(element, TreeItem.Factory)
    {
    }

    public static IFactory<Tree> Factory
    {
      get
      {
        if (Tree._factory == null)
          Tree._factory = (IFactory<Tree>) new Tree.TreeFactory();
        return Tree._factory;
      }
    }

    private class TreeFactory : IFactory<Tree>
    {
      public Tree Create(UIObject element) => new Tree(element);
    }
  }
}
