// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.RelativeWaitInputAction
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Threading;

namespace MS.Internal.Mita.Foundation
{
  internal class RelativeWaitInputAction : IInputAction
  {
    public int start;
    public int duration;

    public void Execute(IInputDevice inputDevice, int elapsedMs)
    {
      int millisecondsTimeout = this.start + this.duration - elapsedMs;
      if (millisecondsTimeout <= 0)
        return;
      Thread.Sleep(millisecondsTimeout);
    }

    public InputActionType GetActionType() => InputActionType.RelativeWaitInputAction;
  }
}
