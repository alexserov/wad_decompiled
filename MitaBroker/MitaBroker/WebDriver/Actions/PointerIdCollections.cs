// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.PointerIdCollections
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace MitaBroker.WebDriver.Actions
{
  internal sealed class PointerIdCollections
  {
    public const int Uninitialized = -1;
    private const int RangeStart = 10;
    private const int RangeCount = 246;
    private Queue<int> pointerIdCollections;

    public PointerIdCollections() => this.pointerIdCollections = new Queue<int>((IEnumerable<int>) Enumerable.Range(10, 246).ToList<int>());

    public int Get()
    {
      lock (this.pointerIdCollections)
        return this.pointerIdCollections.Count != 0 ? this.pointerIdCollections.Dequeue() : throw new Exception("Cannot issue more valid pointer id. There are too many active pointers.");
    }

    public void Recycle(int pointerId)
    {
      lock (this.pointerIdCollections)
      {
        if (pointerId < 10 || pointerId >= 256)
          throw new InternalErrorException(string.Format("Invalid pointer id {0} cannot be recycled", (object) pointerId));
        this.pointerIdCollections.Enqueue(pointerId);
      }
    }
  }
}
