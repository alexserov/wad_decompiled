﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.SiblingsNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class SiblingsNavigator : UINavigator
  {
    private AutomationElement _root;
    private TreeWalker _treeWalker;

    public SiblingsNavigator(UIObject root, UICondition treeCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      this.Initialize(root.AutomationElement, treeCondition);
    }

    public SiblingsNavigator(AutomationElement root, UICondition treeCondition) => this.Initialize(root, treeCondition);

    public SiblingsNavigator(SiblingsNavigator previous)
      : base((UINavigator) previous)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous, nameof (previous));
      this._root = previous._root;
      this._treeWalker = previous._treeWalker;
    }

    private void Initialize(AutomationElement root, UICondition treeCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) treeCondition, nameof (treeCondition));
      this._root = root;
      this._treeWalker = new TreeWalker(treeCondition.Condition);
    }

    public override UINavigator Duplicate() => (UINavigator) new SiblingsNavigator(this);

    public override IEnumerator<AutomationElement> GetEnumerator()
    {
      SiblingsNavigator siblingsNavigator = this;
      UIObjectFilter filter = siblingsNavigator.Filter;
      AutomationElement current;
      for (current = siblingsNavigator._treeWalker.GetFirstChild(siblingsNavigator._treeWalker.GetParent(siblingsNavigator._root)); current != (AutomationElement) null; current = siblingsNavigator._treeWalker.GetNextSibling(current))
      {
        if (!System.Windows.Automation.Automation.Compare(current, siblingsNavigator._root) && filter.Matches(current))
          yield return current;
      }
      current = (AutomationElement) null;
    }

    public override string ToString() => StringResource.Get("SiblingsNavigator_ToString_3", (object) new UIObject(this._root).ToString(), (object) UICondition.ToString(this._treeWalker.Condition), (object) this.Filter.ToString());
  }
}