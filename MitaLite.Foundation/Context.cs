// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Context
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;

namespace MS.Internal.Mita.Foundation
{
  public class Context
  {
    private bool _activated;
    private UICondition _treeCondition;
    [ThreadStatic]
    private static System.Collections.Generic.Stack<Context> _stack;
    private static readonly Context _rawContext = new Context(UICondition.RawTree);
    private static readonly Context _contentContext = new Context(UICondition.ContentTree);
    private static readonly Context _controlContext = new Context(UICondition.ControlTree);
    private static readonly Context _defaultContext = Context.ControlContext;

    public Context(UICondition treeCondition)
    {
      Validate.ArgumentNotNull((object) treeCondition, nameof (treeCondition));
      this._activated = false;
      this._treeCondition = treeCondition;
    }

    public IDisposable Activate() => (IDisposable) new ContextMartyr(this);

    public bool IsActivated
    {
      get => this._activated;
      set => this._activated = value;
    }

    public UICondition TreeCondition => this._treeCondition;

    public static Context Current => Context.Stack.Peek();

    public static Context RawContext => Context._rawContext;

    public static Context ContentContext => Context._contentContext;

    public static Context ControlContext => Context._controlContext;

    internal static System.Collections.Generic.Stack<Context> Stack
    {
      get
      {
        if (Context._stack == null)
        {
          Context._stack = new System.Collections.Generic.Stack<Context>();
          Context._stack.Push(Context._defaultContext);
        }
        return Context._stack;
      }
    }
  }
}
