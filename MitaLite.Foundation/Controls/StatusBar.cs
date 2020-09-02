// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.StatusBar
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class StatusBar : StatusBar<UIObject>
  {
    private static IFactory<StatusBar> _factory;

    public StatusBar(UIObject uiObject)
      : base(uiObject, UIObject.Factory)
    {
    }

    internal StatusBar(AutomationElement element)
      : base(element, UIObject.Factory)
    {
    }

    public static IFactory<StatusBar> Factory
    {
      get
      {
        if (StatusBar._factory == null)
          StatusBar._factory = (IFactory<StatusBar>) new StatusBar.StatusBarFactory();
        return StatusBar._factory;
      }
    }

    private class StatusBarFactory : IFactory<StatusBar>
    {
      public StatusBar Create(UIObject element) => new StatusBar(element);
    }
  }
}
