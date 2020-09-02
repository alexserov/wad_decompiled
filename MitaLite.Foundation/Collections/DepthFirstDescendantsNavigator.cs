// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.DepthFirstDescendantsNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class DepthFirstDescendantsNavigator : UINavigator, IDisposable
  {
    private DepthFirstTreeEnumerator<AutomationElement> _treeEnumerator;

    public DepthFirstDescendantsNavigator(UIObject root, UICondition treeCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      this.Initialize(root.AutomationElement, treeCondition);
    }

    public DepthFirstDescendantsNavigator(AutomationElement root, UICondition treeCondition) => this.Initialize(root, treeCondition);

    public DepthFirstDescendantsNavigator(DepthFirstDescendantsNavigator previous)
      : base((UINavigator) previous)
      => this._treeEnumerator = new DepthFirstTreeEnumerator<AutomationElement>(previous._treeEnumerator);

    private void Initialize(AutomationElement root, UICondition treeCondition) => this._treeEnumerator = new DepthFirstTreeEnumerator<AutomationElement>(root, (ITreeNavigator<AutomationElement>) new AutomationElementTreeNavigator(new TreeWalker(treeCondition.Condition)), true);

    ~DepthFirstDescendantsNavigator() => this.Dispose(false);

    public override UINavigator Duplicate() => (UINavigator) new DepthFirstDescendantsNavigator(this);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing && this._treeEnumerator != null)
        this._treeEnumerator.Dispose();
      this._treeEnumerator = (DepthFirstTreeEnumerator<AutomationElement>) null;
    }

    public override IEnumerator<AutomationElement> GetEnumerator()
    {
      DepthFirstDescendantsNavigator descendantsNavigator = this;
      UIObjectFilter filter = descendantsNavigator.Filter;
      descendantsNavigator._treeEnumerator.Reset();
      while (descendantsNavigator._treeEnumerator.MoveNext())
      {
        if (filter.Matches(descendantsNavigator._treeEnumerator.Current))
          yield return descendantsNavigator._treeEnumerator.Current;
      }
    }

    public override string ToString() => StringResource.Get("DepthFirstDescendantsNavigator_ToString_2", (object) new UIObject(this._treeEnumerator.Root).ToString(), (object) this.Filter.ToString());
  }
}
