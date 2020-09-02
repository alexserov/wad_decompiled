// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.AppModel.AppStateManagementUtils
// Assembly: MitaLite.AppModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A122DB45-8FFE-4D55-BBC1-6161ECF617EB
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.AppModel.dll

//using Microsoft.OneCoreUap.Test.AppModel;

namespace MS.Internal.Mita.AppModel {
    public static class AppStateManagementUtils {
        //private static IViewDescriptor appViewDescriptor;

        public static void LaunchApplication(string AumId) {
        } /*AppStateManagementUtils.appViewDescriptor = NavigationHelper.LaunchApplication(AumId);*/

        public static void CloseApplication() {
            //if (AppStateManagementUtils.appViewDescriptor == null)
            //  throw new Exception("View Descriptor was null. Ensure you started the application with the LaunchEx method or use the CloseWindow method that takes a UIObject as a parameter");
            //NavigationHelper.CloseView(AppStateManagementUtils.appViewDescriptor.get_ViewId());
        }
    }
}