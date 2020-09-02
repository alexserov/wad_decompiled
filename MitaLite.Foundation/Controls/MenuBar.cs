// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.MenuBar
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class MenuBar : UIObject, IContainer<SubmenuItem>
  {
    private static IFactory<MenuBar> _factory = (IFactory<MenuBar>) null;
    private static readonly UICondition _treeCondition = UICondition.Create("@ControlType=MenuBar Or @ControlType=MenuItem");

    public MenuBar(UIObject uiObject)
      : base(uiObject)
    {
    }

    internal MenuBar(AutomationElement element)
      : base(element)
    {
    }

    public UICollection<SubmenuItem> Items => (UICollection<SubmenuItem>) new UIChildren<SubmenuItem>((UIObject) this, MenuBar._treeCondition, SubmenuItem.Factory);

    public static IFactory<MenuBar> Factory
    {
      get
      {
        if (MenuBar._factory == null)
          MenuBar._factory = (IFactory<MenuBar>) new MenuBar.MenuBarFactory();
        return MenuBar._factory;
      }
    }

    private class MenuBarFactory : IFactory<MenuBar>
    {
      public MenuBar Create(UIObject element) => new MenuBar(element);
    }
  }
}
