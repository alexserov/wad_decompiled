// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.SplitButton
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class SplitButton : UIObject, IExpandCollapse, IInvoke
  {
    private IExpandCollapse _expandCollapsePattern;
    private IInvoke _invokePattern;
    private static IFactory<SplitButton> _factory;

    public SplitButton(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal SplitButton(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize()
    {
      this._expandCollapsePattern = (IExpandCollapse) new ExpandCollapseImplementation((UIObject) this);
      this._invokePattern = (IInvoke) new InvokeImplementation((UIObject) this);
    }

    public virtual void Collapse() => this._expandCollapsePattern.Collapse();

    public virtual void Expand() => this._expandCollapsePattern.Expand();

    public virtual ExpandCollapseState ExpandCollapseState => this._expandCollapsePattern.ExpandCollapseState;

    public UIEventWaiter GetCollapsedWaiter() => this._expandCollapsePattern.GetCollapsedWaiter();

    public UIEventWaiter GetExpandedWaiter() => this._expandCollapsePattern.GetExpandedWaiter();

    public virtual void Invoke() => this._invokePattern.Invoke();

    public UIEventWaiter GetInvokedWaiter() => this._invokePattern.GetInvokedWaiter();

    public static IFactory<SplitButton> Factory
    {
      get
      {
        if (SplitButton._factory == null)
          SplitButton._factory = (IFactory<SplitButton>) new SplitButton.SplitButtonFactory();
        return SplitButton._factory;
      }
    }

    private class SplitButtonFactory : IFactory<SplitButton>
    {
      public SplitButton Create(UIObject element) => new SplitButton(element);
    }
  }
}
