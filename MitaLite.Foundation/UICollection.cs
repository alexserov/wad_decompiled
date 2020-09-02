// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UICollection
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation
{
  public abstract class UICollection
  {
    private static bool _autoRealize;
    private static TimeSpan _timeout = TimeSpan.Zero;
    private static int _retryCount = 5;

    public static TimeSpan Timeout
    {
      get => UICollection._timeout;
      set => UICollection._timeout = value;
    }

    public static int RetryCount
    {
      get => UICollection._retryCount;
      set => UICollection._retryCount = value;
    }

    public static bool AutoRealize
    {
      get => UICollection._autoRealize;
      set => UICollection._autoRealize = value;
    }
  }
}
