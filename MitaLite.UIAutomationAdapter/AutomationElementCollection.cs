// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AutomationElementCollection
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections;
using UIAutomationClient;

namespace System.Windows.Automation {
    public class AutomationElementCollection : ICollection, IEnumerable {
        readonly AutomationElement[] _elements;

        internal AutomationElementCollection(AutomationElement[] elements) {
            this._elements = elements;
        }

        internal AutomationElementCollection(IUIAutomationElementArray elementArray) {
            this._elements = new AutomationElement[elementArray.Length];
            for (var index = 0; index < elementArray.Length; ++index)
                this._elements[index] = new AutomationElement(autoElement: elementArray.GetElement(index: index));
        }

        public AutomationElement this[int index] {
            get { return this._elements[index]; }
        }

        public virtual void CopyTo(Array array, int index) {
            this._elements.CopyTo(array: array, index: index);
        }

        public IEnumerator GetEnumerator() {
            return this._elements.GetEnumerator();
        }

        public int Count {
            get { return this._elements.Length; }
        }

        public virtual object SyncRoot {
            get { return this; }
        }

        public virtual bool IsSynchronized {
            get { return false; }
        }

        public void CopyTo(AutomationElement[] array, int index) {
            Validate.ArgumentNotNull(parameter: array, parameterName: nameof(array));
            for (var index1 = 0; index1 < this._elements.Length; ++index1)
                array[index1] = this._elements[index1];
        }

        public static explicit operator AutomationElement[](
            AutomationElementCollection collection) {
            var array = new AutomationElement[collection.Count];
            collection.CopyTo(array: array, index: 0);
            return array;
        }
    }
}