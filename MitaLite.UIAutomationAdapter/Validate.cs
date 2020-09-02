// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Validate
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation {
    internal static class Validate {
        public static void ArgumentNotNull(object parameter, string parameterName) {
            if (parameter == null)
                throw new ArgumentNullException(paramName: parameterName, message: "Parameter cannot be NULL");
        }

        public static void StringNeitherNullNorEmpty(string parameter, string parameterName) {
            if (string.IsNullOrEmpty(value: parameter))
                throw new ArgumentException(message: "String parameter cannot be NULL or Empty ", paramName: parameterName);
        }
    }
}