// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.CacheRequest
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;
using System.Threading;
using UIAutomationClient;

namespace System.Windows.Automation {
    public sealed class CacheRequest {
        [ThreadStatic]
        static Stack<CacheRequest> _stack;

        static readonly CacheRequest _defaultCacheRequest = new CacheRequest();
        int _stackEntryCount;

        public CacheRequest() {
            this.IUIAutomationCacheRequest = Automation.AutomationClass.CreateCacheRequest();
        }

        internal CacheRequest(CacheRequest other) {
            this.IUIAutomationCacheRequest = other.IUIAutomationCacheRequest;
        }

        public AutomationElementMode AutomationElementMode {
            get { return UiaConvert.Convert(automationElementMode: this.IUIAutomationCacheRequest.AutomationElementMode); }
            set { this.IUIAutomationCacheRequest.AutomationElementMode = UiaConvert.Convert(automationElementMode: value); }
        }

        public CacheRequest Current {
            get { return _stack == null || _stack.Count == 0 ? _defaultCacheRequest : _stack.Peek(); }
        }

        public TreeScope TreeScope {
            get { return UiaConvert.Convert(treeScope: this.IUIAutomationCacheRequest.TreeScope); }
            set { this.IUIAutomationCacheRequest.TreeScope = UiaConvert.Convert(treeScope: value); }
        }

        public Condition TreeFilter {
            get { return new Condition(condition: this.IUIAutomationCacheRequest.TreeFilter); }
            set { this.IUIAutomationCacheRequest.TreeFilter = value.IUIAutomationCondition; }
        }

        internal IUIAutomationCacheRequest IUIAutomationCacheRequest { get; }

        public IDisposable Activate() {
            Push();
            return new CacheRequestMartyr(request: this);
        }

        public void Add(AutomationProperty property) {
            lock (this.IUIAutomationCacheRequest) {
                CheckAccess();
                this.IUIAutomationCacheRequest.AddProperty(propertyId: property.Id);
            }
        }

        public void Add(AutomationPattern pattern) {
            lock (this.IUIAutomationCacheRequest) {
                CheckAccess();
                this.IUIAutomationCacheRequest.AddPattern(patternId: pattern.Id);
            }
        }

        public CacheRequest Clone() {
            return new CacheRequest(other: this);
        }

        public void Pop() {
            if (_stack == null || _stack.Count == 0 || _stack.Peek() != this)
                throw new InvalidOperationException();
            _stack.Pop();
            Interlocked.Decrement(location: ref this._stackEntryCount);
        }

        public void Push() {
            if (_stack == null)
                _stack = new Stack<CacheRequest>();
            _stack.Push(item: this);
            Interlocked.Increment(location: ref this._stackEntryCount);
        }

        void CheckAccess() {
            if (this._stackEntryCount != 0)
                throw new InvalidOperationException(message: "Can't modify an activated cache request");
            if (this == _defaultCacheRequest)
                throw new InvalidOperationException(message: "Can't modify a default cache request");
        }

        class CacheRequestMartyr : IDisposable {
            CacheRequest _request;

            internal CacheRequestMartyr(CacheRequest request) {
                this._request = request;
            }

            void IDisposable.Dispose() {
                if (this._request == null)
                    return;
                this._request.Pop();
                this._request = null;
            }
        }
    }
}