// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.UIAutomationEventHandler`1
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;

namespace System.Windows.Automation {
    internal abstract class UIAutomationEventHandler<T> where T : UIAutomationEventHandler<T> {
        internal static List<T> _events = new List<T>();

        protected abstract void Remove();

        protected static void Add(T instance) {
            lock (_events) {
                _events.Add(item: instance);
            }
        }

        protected static bool Remove(Predicate<T> predicate) {
            lock (_events) {
                var index = _events.FindIndex(match: predicate);
                if (index == -1)
                    return false;
                var obj = _events[index: index];
                _events.RemoveAt(index: index);
                obj.Remove();
                return true;
            }
        }

        public static void RemoveAll() {
            do {
                ;
            } while (Remove(predicate: item => true));
        }
    }
}