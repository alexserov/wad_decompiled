// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Hyperlink
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class Hyperlink : UIObject, IInvoke
  {
    private IInvoke _invokePattern;
    private static IFactory<Hyperlink> _factory;

    public Hyperlink(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal Hyperlink(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._invokePattern = (IInvoke) new InvokeImplementation((UIObject) this);

    public virtual void Invoke() => this._invokePattern.Invoke();

    public UIEventWaiter GetInvokedWaiter() => this._invokePattern.GetInvokedWaiter();

    public static IFactory<Hyperlink> Factory
    {
      get
      {
        if (Hyperlink._factory == null)
          Hyperlink._factory = (IFactory<Hyperlink>) new Hyperlink.HyperlinkFactory();
        return Hyperlink._factory;
      }
    }

    private class HyperlinkFactory : IFactory<Hyperlink>
    {
      public Hyperlink Create(UIObject element) => new Hyperlink(element);
    }
  }
}
