// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ToolTip
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class ToolTip : UIObject
  {
    private static IFactory<ToolTip> _factory;

    public ToolTip(UIObject uiObject)
      : base(uiObject)
    {
    }

    internal ToolTip(AutomationElement element)
      : base(element)
    {
    }

    public static IFactory<ToolTip> Factory
    {
      get
      {
        if (ToolTip._factory == null)
          ToolTip._factory = (IFactory<ToolTip>) new ToolTip.ToolTipFactory();
        return ToolTip._factory;
      }
    }

    private class ToolTipFactory : IFactory<ToolTip>
    {
      public ToolTip Create(UIObject element) => new ToolTip(element);
    }
  }
}
