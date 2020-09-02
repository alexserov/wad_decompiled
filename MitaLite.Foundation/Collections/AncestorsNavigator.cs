// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.AncestorsNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class AncestorsNavigator : UINavigator
  {
    private AutomationElement _root;
    private TreeWalker _treeWalker;

    public AncestorsNavigator(UIObject root, UICondition treeCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) treeCondition, nameof (treeCondition));
      this.Initialize(root.AutomationElement, new TreeWalker(treeCondition.Condition));
    }

    public AncestorsNavigator(AutomationElement root, UICondition treeCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) treeCondition, nameof (treeCondition));
      this.Initialize(root, new TreeWalker(treeCondition.Condition));
    }

    public AncestorsNavigator(AncestorsNavigator previous)
      : base((UINavigator) previous)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous, nameof (previous));
      this.Initialize(previous._root, previous._treeWalker);
    }

    private void Initialize(AutomationElement root, TreeWalker treeWalker)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) treeWalker, nameof (treeWalker));
      this._root = root;
      this._treeWalker = treeWalker;
    }

    public override UINavigator Duplicate() => (UINavigator) new AncestorsNavigator(this);

    public override IEnumerator<AutomationElement> GetEnumerator()
    {
      AncestorsNavigator ancestorsNavigator = this;
      UIObjectFilter filter = ancestorsNavigator.Filter;
      AutomationElement current;
      for (current = ancestorsNavigator._treeWalker.GetParent(ancestorsNavigator._root); current != (AutomationElement) null; current = ancestorsNavigator._treeWalker.GetParent(current))
      {
        if (filter.Matches(current))
          yield return current;
      }
      current = (AutomationElement) null;
    }

    public override string ToString() => StringResource.Get("AncestorsNavigator_ToString_3", (object) new UIObject(this._root).ToString(), (object) UICondition.ToString(this._treeWalker.Condition), (object) this.Filter.ToString());
  }
}
