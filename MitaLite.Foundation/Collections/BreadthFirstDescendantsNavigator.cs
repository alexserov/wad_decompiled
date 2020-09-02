// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.BreadthFirstDescendantsNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class BreadthFirstDescendantsNavigator : UINavigator, IDisposable
  {
    private BreadthFirstTreeEnumerator<AutomationElement> _treeEnumerator;

    public BreadthFirstDescendantsNavigator(UIObject root, UICondition treeCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      this.Initialize(root.AutomationElement, treeCondition);
    }

    public BreadthFirstDescendantsNavigator(AutomationElement root, UICondition treeCondition) => this.Initialize(root, treeCondition);

    public BreadthFirstDescendantsNavigator(BreadthFirstDescendantsNavigator previous)
      : base((UINavigator) previous)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous, nameof (previous));
      this._treeEnumerator = new BreadthFirstTreeEnumerator<AutomationElement>(previous._treeEnumerator);
    }

    private void Initialize(AutomationElement root, UICondition treeCondition) => this._treeEnumerator = new BreadthFirstTreeEnumerator<AutomationElement>(root, (ITreeNavigator<AutomationElement>) new AutomationElementTreeNavigator(new TreeWalker(treeCondition.Condition)), true);

    ~BreadthFirstDescendantsNavigator() => this.Dispose(false);

    public override UINavigator Duplicate() => (UINavigator) new BreadthFirstDescendantsNavigator(this);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing && this._treeEnumerator != null)
        this._treeEnumerator.Dispose();
      this._treeEnumerator = (BreadthFirstTreeEnumerator<AutomationElement>) null;
    }

    public override IEnumerator<AutomationElement> GetEnumerator()
    {
      BreadthFirstDescendantsNavigator descendantsNavigator = this;
      UIObjectFilter filter = descendantsNavigator.Filter;
      descendantsNavigator._treeEnumerator.Reset();
      while (descendantsNavigator._treeEnumerator.MoveNext())
      {
        if (filter.Matches(descendantsNavigator._treeEnumerator.Current))
          yield return descendantsNavigator._treeEnumerator.Current;
      }
    }

    public override string ToString() => StringResource.Get("BreadthFirstDescendantsNavigator_ToString_2", (object) new UIObject(this._treeEnumerator.Root).ToString(), (object) this.Filter.ToString());
  }
}
