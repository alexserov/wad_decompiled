// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.CompositeInputControllerMartyr
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  public class CompositeInputControllerMartyr : IDisposable
  {
    private Stack<IDisposable> _inputControllerMartyrStack;
    private PointerInputType _previousInputType;
    private bool _disposed;

    public CompositeInputControllerMartyr()
      : this(PointerInputType.Mouse)
    {
    }

    public CompositeInputControllerMartyr(PointerInputType previousInputType)
    {
      this._inputControllerMartyrStack = new Stack<IDisposable>();
      this._previousInputType = previousInputType;
    }

    public void Add(IDisposable martyr) => this._inputControllerMartyrStack.Push(martyr);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      try
      {
        if (this._disposed || !disposing)
          return;
        lock (this._inputControllerMartyrStack)
        {
          while (this._inputControllerMartyrStack.Count > 0)
            this._inputControllerMartyrStack.Pop().Dispose();
        }
        InputController.ActiveInputType = this._previousInputType;
      }
      finally
      {
        this._disposed = true;
      }
    }
  }
}
