// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.ContextMartyr
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation
{
  internal class ContextMartyr : IDisposable
  {
    private Context currentContext;

    public ContextMartyr(Context currentContext)
    {
      this.currentContext = currentContext;
      this.currentContext.IsActivated = true;
      Context.Stack.Push(this.currentContext);
    }

    ~ContextMartyr() => this.Dispose(false);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.currentContext == null || !disposing || Context.Stack.Peek() != this.currentContext)
        return;
      Context.Stack.Pop();
      this.currentContext.IsActivated = false;
      this.currentContext = (Context) null;
    }
  }
}
