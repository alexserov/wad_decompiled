// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.Boundary
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Runtime.InteropServices;

namespace System.Windows.Automation {
    internal static class Boundary {
        internal static void NoExceptions(Action a) {
            try {
                a();
            } catch (Exception ex) {
            }
        }

        internal static void NoExceptions(Action a, Action<Exception> c) {
            try {
                a();
            } catch (Exception ex1) {
                try {
                    c(obj: ex1);
                } catch (Exception ex2) {
                }
            }
        }

        internal static void UIAutomation(Action a) {
            try {
                a();
            } catch (COMException ex) {
                Exception exception;
                if (UiaConvert.ConvertException(e: ex, newException: out exception))
                    throw exception;
                throw;
            }
        }

        internal static R UIAutomation<R>(Func<R> f) {
            try {
                return f();
            } catch (COMException ex) {
                Exception exception;
                if (UiaConvert.ConvertException(e: ex, newException: out exception))
                    throw exception;
                throw;
            }
        }
    }
}