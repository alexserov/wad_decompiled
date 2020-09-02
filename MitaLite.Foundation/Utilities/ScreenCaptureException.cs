﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.ScreenCaptureException
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;

namespace MS.Internal.Mita.Foundation.Utilities {
    public class ScreenCaptureException : MitaException {
        public ScreenCaptureException() {
        }

        public ScreenCaptureException(string message)
            : base(message: message) {
        }

        public ScreenCaptureException(string message, Exception innerException)
            : base(message: message, innerException: innerException) {
        }

        public ScreenCaptureException(string message, int windowsError)
            : base(message: message) {
            Win32Error = windowsError;
        }

        public override string Message {
            get { return string.IsNullOrEmpty(value: base.Message) ? "Error: " + Win32Error.ToString(provider: CultureInfo.InvariantCulture) : base.Message + " : " + Win32Error.ToString(provider: CultureInfo.InvariantCulture); }
        }

        public int Win32Error { get; }
    }
}