// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYaccSet
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSYaccSet : Queue<object>
  {
    public bool add(object q_object)
    {
      if (this.Contains(q_object))
        return false;
      this.Enqueue(q_object);
      return true;
    }

    public bool locate(int q_locate) => this.Contains((object) q_locate);
  }
}
