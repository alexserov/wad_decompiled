// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UICollection
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation {
    public abstract class UICollection {
        static bool _autoRealize;
        static TimeSpan _timeout = TimeSpan.Zero;
        static int _retryCount = 5;

        public static TimeSpan Timeout {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public static int RetryCount {
            get { return _retryCount; }
            set { _retryCount = value; }
        }

        public static bool AutoRealize {
            get { return _autoRealize; }
            set { _autoRealize = value; }
        }
    }
}