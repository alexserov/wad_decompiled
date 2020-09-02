// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.CheckMenuItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class CheckMenuItem : UIObject, IToggle
  {
    private IToggle _togglePattern;
    private static IFactory<CheckMenuItem> _factory;

    public CheckMenuItem(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal CheckMenuItem(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._togglePattern = (IToggle) new ToggleImplementation((UIObject) this);

    public UIEventWaiter GetToggledWaiter() => this._togglePattern.GetToggledWaiter();

    public virtual void Toggle() => this._togglePattern.Toggle();

    public virtual ToggleState ToggleState => this._togglePattern.ToggleState;

    public static IFactory<CheckMenuItem> Factory
    {
      get
      {
        if (CheckMenuItem._factory == null)
          CheckMenuItem._factory = (IFactory<CheckMenuItem>) new CheckMenuItem.CheckMenuItemFactory();
        return CheckMenuItem._factory;
      }
    }

    private class CheckMenuItemFactory : IFactory<CheckMenuItem>
    {
      public CheckMenuItem Create(UIObject element) => new CheckMenuItem(element);
    }
  }
}
