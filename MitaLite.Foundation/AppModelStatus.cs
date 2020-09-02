// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.AppModelStatus
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using Windows.ApplicationModel;

namespace MS.Internal.Mita.Foundation {
    public static class AppModelStatus {
        public static bool IsInAppContainer = IsCurrentProcessInAppContainer();

        static bool IsCurrentProcessInAppContainer() {
            try {
                var curr = Package.Current;
                return true;
            } catch {
                return false;
            }
        }
    }
}