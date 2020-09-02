// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.MenuItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class MenuItem : UIObject, IInvoke
  {
    private static IFactory<MenuItem> _factory;
    private IInvoke _invokePattern;

    public MenuItem(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal MenuItem(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._invokePattern = (IInvoke) new InvokeImplementation((UIObject) this);

    public virtual void Invoke() => this._invokePattern.Invoke();

    public UIEventWaiter GetInvokedWaiter() => this._invokePattern.GetInvokedWaiter();

    public static IFactory<MenuItem> Factory
    {
      get
      {
        if (MenuItem._factory == null)
          MenuItem._factory = (IFactory<MenuItem>) new MenuItem.MenuItemFactory();
        return MenuItem._factory;
      }
    }

    private class MenuItemFactory : IFactory<MenuItem>
    {
      public MenuItem Create(UIObject element) => new MenuItem(element);
    }
  }
}
