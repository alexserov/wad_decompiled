// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputQueue
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    internal class InputQueue : IInputQueue {
        public void Process(IInputDevice inputDevice, IList<IInputAction> inputList) {
            var ticks = DateTime.Now.Ticks;
            var elapsedMs = 0;
            foreach (var input in inputList) {
                input.Execute(inputDevice: inputDevice, elapsedMs: elapsedMs);
                elapsedMs = (int) ((DateTime.Now.Ticks - ticks) / 10000L);
            }
        }
    }
}