// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.AutomationElementTreeNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class AutomationElementTreeNavigator : ITreeNavigator<AutomationElement> {
        readonly TreeWalker _treeWalker;

        public AutomationElementTreeNavigator(TreeWalker treeWalker) {
            Validate.ArgumentNotNull(parameter: treeWalker, parameterName: nameof(treeWalker));
            this._treeWalker = treeWalker;
        }

        public AutomationElement GetNextSibling(AutomationElement current) {
            return this._treeWalker.GetNextSibling(element: current);
        }

        public AutomationElement GetFirstChild(AutomationElement current) {
            return this._treeWalker.GetFirstChild(element: current);
        }
    }
}