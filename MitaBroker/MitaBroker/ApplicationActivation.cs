// Decompiled with JetBrains decompiler
// Type: MitaBroker.ApplicationActivation
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MS.Internal.Mita.Foundation;

namespace MitaBroker {
    internal static class ApplicationActivation {
        public static void ActivateApplication(
            string applicationId,
            string arguments,
            out uint processId) {
            new UAPApp.ApplicationActivationManager().ActivateApplication(appUserModelId: applicationId, arguments: arguments, options: UAPApp.ActivateOptions.None, processId: out processId);
        }
    }
}