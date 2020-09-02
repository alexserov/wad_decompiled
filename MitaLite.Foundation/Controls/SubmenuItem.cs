// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SubmenuItem
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class SubmenuItem : UIObject, IExpandCollapse
  {
    private IExpandCollapse _expandCollapsePattern;
    private static IFactory<SubmenuItem> _factory;

    public SubmenuItem(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal SubmenuItem(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._expandCollapsePattern = (IExpandCollapse) new ExpandCollapseImplementation((UIObject) this);

    public virtual void Collapse() => this._expandCollapsePattern.Collapse();

    public virtual void Expand() => this._expandCollapsePattern.Expand();

    public virtual ExpandCollapseState ExpandCollapseState => this._expandCollapsePattern.ExpandCollapseState;

    public UIEventWaiter GetCollapsedWaiter() => this._expandCollapsePattern.GetCollapsedWaiter();

    public UIEventWaiter GetExpandedWaiter() => this._expandCollapsePattern.GetExpandedWaiter();

    public static IFactory<SubmenuItem> Factory
    {
      get
      {
        if (SubmenuItem._factory == null)
          SubmenuItem._factory = (IFactory<SubmenuItem>) new SubmenuItem.SubmenuItemFactory();
        return SubmenuItem._factory;
      }
    }

    private class SubmenuItemFactory : IFactory<SubmenuItem>
    {
      public SubmenuItem Create(UIObject element) => new SubmenuItem(element);
    }
  }
}
