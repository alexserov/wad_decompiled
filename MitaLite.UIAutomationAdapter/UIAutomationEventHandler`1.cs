// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.UIAutomationEventHandler`1
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;

namespace System.Windows.Automation
{
  internal abstract class UIAutomationEventHandler<T> where T : UIAutomationEventHandler<T>
  {
    internal static List<T> _events = new List<T>();

    protected abstract void Remove();

    protected static void Add(T instance)
    {
      lock (UIAutomationEventHandler<T>._events)
        UIAutomationEventHandler<T>._events.Add(instance);
    }

    protected static bool Remove(Predicate<T> predicate)
    {
      lock (UIAutomationEventHandler<T>._events)
      {
        int index = UIAutomationEventHandler<T>._events.FindIndex(predicate);
        if (index == -1)
          return false;
        T obj = UIAutomationEventHandler<T>._events[index];
        UIAutomationEventHandler<T>._events.RemoveAt(index);
        obj.Remove();
        return true;
      }
    }

    public static void RemoveAll()
    {
      do
        ;
      while (UIAutomationEventHandler<T>.Remove((Predicate<T>) (item => true)));
    }
  }
}
