﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.WaiterException
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation.Waiters {
    public class WaiterException : MitaException {
        public WaiterException() {
        }

        public WaiterException(string message)
            : base(message: message) {
        }

        public WaiterException(string message, Exception innerException)
            : base(message: message, innerException: innerException) {
        }
    }
}