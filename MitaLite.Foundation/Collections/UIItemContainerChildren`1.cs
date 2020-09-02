// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.UIItemContainerChildren`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  public class UIItemContainerChildren<I> : UICollection<I> where I : UIObject
  {
    public UIItemContainerChildren(UIObject root, IFactory<I> factory)
      : base((UINavigator) new ItemContainerChildrenNavigator(root, Context.Current.TreeCondition), factory)
    {
    }

    internal UIItemContainerChildren(AutomationElement root, IFactory<I> factory)
      : base((UINavigator) new ItemContainerChildrenNavigator(root, Context.Current.TreeCondition), factory)
    {
    }

    public UIItemContainerChildren(UIObject root, UICondition treeCondition, IFactory<I> factory)
      : base((UINavigator) new ItemContainerChildrenNavigator(root, treeCondition), factory)
    {
    }

    internal UIItemContainerChildren(
      AutomationElement root,
      UICondition treeCondition,
      IFactory<I> factory)
      : base((UINavigator) new ItemContainerChildrenNavigator(root, treeCondition), factory)
    {
    }
  }
}
