// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.CacheRequest
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;
using System.Threading;
using UIAutomationClient;

namespace System.Windows.Automation
{
  public sealed class CacheRequest
  {
    private readonly IUIAutomationCacheRequest _cacheRequest;
    private int _stackEntryCount;
    [ThreadStatic]
    private static Stack<CacheRequest> _stack;
    private static readonly CacheRequest _defaultCacheRequest = new CacheRequest();

    public CacheRequest() => this._cacheRequest = System.Windows.Automation.Automation.AutomationClass.CreateCacheRequest();

    internal CacheRequest(CacheRequest other) => this._cacheRequest = other._cacheRequest;

    public IDisposable Activate()
    {
      this.Push();
      return (IDisposable) new CacheRequest.CacheRequestMartyr(this);
    }

    public void Add(AutomationProperty property)
    {
      lock (this._cacheRequest)
      {
        this.CheckAccess();
        this._cacheRequest.AddProperty(property.Id);
      }
    }

    public void Add(AutomationPattern pattern)
    {
      lock (this._cacheRequest)
      {
        this.CheckAccess();
        this._cacheRequest.AddPattern(pattern.Id);
      }
    }

    public CacheRequest Clone() => new CacheRequest(this);

    public void Pop()
    {
      if (CacheRequest._stack == null || CacheRequest._stack.Count == 0 || CacheRequest._stack.Peek() != this)
        throw new InvalidOperationException();
      CacheRequest._stack.Pop();
      Interlocked.Decrement(ref this._stackEntryCount);
    }

    public void Push()
    {
      if (CacheRequest._stack == null)
        CacheRequest._stack = new Stack<CacheRequest>();
      CacheRequest._stack.Push(this);
      Interlocked.Increment(ref this._stackEntryCount);
    }

    private void CheckAccess()
    {
      if (this._stackEntryCount != 0)
        throw new InvalidOperationException("Can't modify an activated cache request");
      if (this == CacheRequest._defaultCacheRequest)
        throw new InvalidOperationException("Can't modify a default cache request");
    }

    public AutomationElementMode AutomationElementMode
    {
      get => UiaConvert.Convert(this._cacheRequest.AutomationElementMode);
      set => this._cacheRequest.AutomationElementMode = UiaConvert.Convert(value);
    }

    public CacheRequest Current => CacheRequest._stack == null || CacheRequest._stack.Count == 0 ? CacheRequest._defaultCacheRequest : CacheRequest._stack.Peek();

    public TreeScope TreeScope
    {
      get => UiaConvert.Convert(this._cacheRequest.TreeScope);
      set => this._cacheRequest.TreeScope = UiaConvert.Convert(value);
    }

    public Condition TreeFilter
    {
      get => new Condition(this._cacheRequest.TreeFilter);
      set => this._cacheRequest.TreeFilter = value.IUIAutomationCondition;
    }

    internal IUIAutomationCacheRequest IUIAutomationCacheRequest => this._cacheRequest;

    private class CacheRequestMartyr : IDisposable
    {
      private CacheRequest _request;

      internal CacheRequestMartyr(CacheRequest request) => this._request = request;

      void IDisposable.Dispose()
      {
        if (this._request == null)
          return;
        this._request.Pop();
        this._request = (CacheRequest) null;
      }
    }
  }
}
