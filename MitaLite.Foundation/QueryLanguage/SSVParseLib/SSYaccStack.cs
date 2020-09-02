// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYaccStack
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSYaccStack : List<object>
  {
    public int m_size;

    public SSYaccStack(int q_size, int q_inc) => this.m_size = 0;

    public void push(object q_ele)
    {
      this.Add(q_ele);
      ++this.m_size;
    }

    public void pop()
    {
      --this.m_size;
      this.RemoveAt(this.m_size);
    }

    public object elementAt(int index) => this[index];

    public int getSize() => this.Count;

    public object peek() => this.elementAt(this.m_size - 1);
  }
}
