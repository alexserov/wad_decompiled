// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ElementNotEnabledException
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation {
    public class ElementNotEnabledException : Exception {
        public ElementNotEnabledException()
            : base(message: "ElementNotAvailable") {
            HResult = -2147220992;
        }

        public ElementNotEnabledException(Exception innerException)
            : base(message: "ElementNotAvailable", innerException: innerException) {
            HResult = -2147220992;
        }

        public ElementNotEnabledException(string message)
            : base(message: message) {
            HResult = -2147220992;
        }

        public ElementNotEnabledException(string message, Exception innerException)
            : base(message: message, innerException: innerException) {
            HResult = -2147220992;
        }
    }
}