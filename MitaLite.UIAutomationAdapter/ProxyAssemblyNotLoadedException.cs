// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ProxyAssemblyNotLoadedException
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation {
    public class ProxyAssemblyNotLoadedException : Exception {
        static readonly int ElementException = -2147220991;

        public ProxyAssemblyNotLoadedException()
            : base(message: "ElementNotAvailable") {
            HResult = ElementException;
        }

        public ProxyAssemblyNotLoadedException(Exception innerException)
            : base(message: "ElementNotAvailable", innerException: innerException) {
            HResult = ElementException;
        }

        public ProxyAssemblyNotLoadedException(string message)
            : base(message: message) {
            HResult = ElementException;
        }

        public ProxyAssemblyNotLoadedException(string message, Exception innerException)
            : base(message: message, innerException: innerException) {
            HResult = ElementException;
        }
    }
}