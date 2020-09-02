// Decompiled with JetBrains decompiler
// Type: MitaBroker.MultipleWindows
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation;

namespace MitaBroker {
    internal class MultipleWindows {
        const string ApplicationFrameWindowClassName = "ApplicationFrameWindow";

        public static IEnumerable<UIObject> GetTopLevelWindowsClassicApp(
            int processId) {
            UICollection.Timeout = TimeSpan.Zero;
            foreach (var uiObject in UIObject.Root.Children.FindMultiple(condition: UICondition.Create(property: UIProperty.Get(name: "ProcessId"), value: processId).AndWith(newCondition: UICondition.Create(property: UIProperty.Get(name: "ControlType"), value: ControlType.Window))))
                yield return uiObject;
        }

        public static IEnumerable<UIObject> GetTopLevelWindowsModernApp(int processId) {
            UICollection.Timeout = TimeSpan.Zero;
            foreach (var uiObject in UIObject.Root.Children.FindMultiple(condition: UICondition.CreateFromClassName(className: "ApplicationFrameWindow")))
                if (uiObject.Children.Contains(uiProperty: UIProperty.Get(name: "ProcessId"), value: processId))
                    yield return uiObject;
        }
    }
}