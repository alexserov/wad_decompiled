// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.UISiblings`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  public class UISiblings<T> : UICollection<T> where T : UIObject
  {
    public UISiblings(UIObject root, IFactory<T> factory)
      : base((UINavigator) new SiblingsNavigator(root, Context.Current.TreeCondition), factory)
    {
    }

    internal UISiblings(AutomationElement root, IFactory<T> factory)
      : base((UINavigator) new SiblingsNavigator(root, Context.Current.TreeCondition), factory)
    {
    }

    public UISiblings(UIObject root, UICondition treeCondition, IFactory<T> factory)
      : base((UINavigator) new SiblingsNavigator(root, treeCondition), factory)
    {
    }

    internal UISiblings(AutomationElement root, UICondition treeCondition, IFactory<T> factory)
      : base((UINavigator) new SiblingsNavigator(root, treeCondition), factory)
    {
    }
  }
}
