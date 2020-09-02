// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.InvokeImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Waiters;
using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class InvokeImplementation : PatternImplementation<InvokePattern>, IInvoke
  {
    public InvokeImplementation(UIObject uiObject)
      : base(uiObject, InvokePattern.Pattern)
    {
    }

    public void Invoke()
    {
      int num1 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      int num2 = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("MakeVisible"));
      if (ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (Invoke), Array.Empty<object>())) != ActionResult.Unhandled)
        return;
      this.Pattern.Invoke();
    }

    public UIEventWaiter GetInvokedWaiter() => (UIEventWaiter) new AutomationEventWaiter(InvokePattern.InvokedEvent, this.UIObject, Scope.Element);
  }
}
