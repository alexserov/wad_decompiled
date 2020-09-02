﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputControllerMartyr`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  internal class InputControllerMartyr<T> : IDisposable
  {
    private Stack<T> _inputControllerStack;
    private T _currentController;

    private InputControllerMartyr()
    {
    }

    public InputControllerMartyr(Stack<T> inputStack, T inputController)
    {
      this._inputControllerStack = inputStack;
      this._currentController = inputController;
      lock (this._inputControllerStack)
        this._inputControllerStack.Push(inputController);
    }

    ~InputControllerMartyr() => this.Dispose(false);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      lock (this._inputControllerStack)
      {
        if (!this._inputControllerStack.Peek().Equals((object) this._currentController))
          return;
        this._inputControllerStack.Pop();
        this._currentController = default (T);
      }
    }
  }
}
