// Decompiled with JetBrains decompiler
// Type: MitaBroker.MultipleWindows
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MS.Internal.Mita.Foundation;
using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MitaBroker
{
  internal class MultipleWindows
  {
    private const string ApplicationFrameWindowClassName = "ApplicationFrameWindow";

    public static IEnumerable<UIObject> GetTopLevelWindowsClassicApp(
      int processId)
    {
      UICollection.Timeout = TimeSpan.Zero;
      foreach (UIObject uiObject in UIObject.Root.Children.FindMultiple(UICondition.Create(UIProperty.Get("ProcessId"), (object) processId).AndWith(UICondition.Create(UIProperty.Get("ControlType"), (object) ControlType.Window))))
        yield return uiObject;
    }

    public static IEnumerable<UIObject> GetTopLevelWindowsModernApp(int processId)
    {
      UICollection.Timeout = TimeSpan.Zero;
      foreach (UIObject uiObject in UIObject.Root.Children.FindMultiple(UICondition.CreateFromClassName("ApplicationFrameWindow")))
      {
        if (uiObject.Children.Contains(UIProperty.Get("ProcessId"), (object) processId))
          yield return uiObject;
      }
    }
  }
}
