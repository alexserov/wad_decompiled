// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.AutomationElementTreeNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class AutomationElementTreeNavigator : ITreeNavigator<AutomationElement>
  {
    private TreeWalker _treeWalker;

    public AutomationElementTreeNavigator(TreeWalker treeWalker)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) treeWalker, nameof (treeWalker));
      this._treeWalker = treeWalker;
    }

    public AutomationElement GetNextSibling(AutomationElement current) => this._treeWalker.GetNextSibling(current);

    public AutomationElement GetFirstChild(AutomationElement current) => this._treeWalker.GetFirstChild(current);
  }
}
