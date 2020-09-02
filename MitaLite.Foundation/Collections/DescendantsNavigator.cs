// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.DescendantsNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class DescendantsNavigator : UINavigator
  {
    private AutomationElement _root;
    private UICondition _treeCondition;

    public DescendantsNavigator(UIObject root, UICondition treeCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      this.Initialize(root.AutomationElement, treeCondition);
    }

    public DescendantsNavigator(AutomationElement root, UICondition treeCondition) => this.Initialize(root, treeCondition);

    public DescendantsNavigator(DescendantsNavigator previous)
      : base((UINavigator) previous)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous, nameof (previous));
      this.Initialize(previous._root, previous._treeCondition);
    }

    private void Initialize(AutomationElement root, UICondition treeCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) treeCondition, nameof (treeCondition));
      this._root = root;
      this._treeCondition = treeCondition;
    }

    public override UINavigator Duplicate() => (UINavigator) new DescendantsNavigator(this);

    public override IEnumerator<AutomationElement> GetEnumerator()
    {
      DescendantsNavigator descendantsNavigator = this;
      UIObjectFilter filter = descendantsNavigator.Filter;
      foreach (AutomationElement element in filter.UICondition.FindAll(descendantsNavigator._root, Scope.Descendants, new CacheRequest()
      {
        TreeFilter = descendantsNavigator._treeCondition.Condition
      }))
      {
        if (filter.MatchesFilter(element))
          yield return element;
      }
    }

    public override string ToString() => StringResource.Get("DescendantsNavigator_ToString_3", (object) new UIObject(this._root).ToString(), (object) this._treeCondition.ToString(), (object) this.Filter.ToString());
  }
}
