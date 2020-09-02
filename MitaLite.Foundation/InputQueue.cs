// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputQueue
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  internal class InputQueue : IInputQueue
  {
    public void Process(IInputDevice inputDevice, IList<IInputAction> inputList)
    {
      long ticks = DateTime.Now.Ticks;
      int elapsedMs = 0;
      foreach (IInputAction input in (IEnumerable<IInputAction>) inputList)
      {
        input.Execute(inputDevice, elapsedMs);
        elapsedMs = (int) ((DateTime.Now.Ticks - ticks) / 10000L);
      }
    }
  }
}
