// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TreeWalker
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public sealed class TreeWalker {
        public static readonly TreeWalker RawViewWalker = new TreeWalker(condition: Automation.RawViewCondition);
        public static readonly TreeWalker ControlViewWalker = new TreeWalker(condition: Automation.ControlViewCondition);
        public static readonly TreeWalker ContentViewWalker = new TreeWalker(condition: Automation.ContentViewCondition);
        readonly IUIAutomationTreeWalker _treewalker;

        public TreeWalker(Condition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            Condition = condition;
            this._treewalker = Automation.AutomationClass.CreateTreeWalker(pCondition: condition.IUIAutomationCondition);
        }

        public Condition Condition { get; }

        public AutomationElement GetParent(AutomationElement element) {
            var elementBuildCache = this._treewalker.GetParentElementBuildCache(element: element.IUIAutomationElement, cacheRequest: AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
            return elementBuildCache != null ? new AutomationElement(autoElement: elementBuildCache) : null;
        }

        public AutomationElement GetFirstChild(AutomationElement element) {
            var elementBuildCache = this._treewalker.GetFirstChildElementBuildCache(element: element.IUIAutomationElement, cacheRequest: AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
            return elementBuildCache != null ? new AutomationElement(autoElement: elementBuildCache) : null;
        }

        public AutomationElement GetLastChild(AutomationElement element) {
            var elementBuildCache = this._treewalker.GetLastChildElementBuildCache(element: element.IUIAutomationElement, cacheRequest: AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
            return elementBuildCache != null ? new AutomationElement(autoElement: elementBuildCache) : null;
        }

        public AutomationElement GetNextSibling(AutomationElement element) {
            var elementBuildCache = this._treewalker.GetNextSiblingElementBuildCache(element: element.IUIAutomationElement, cacheRequest: AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
            return elementBuildCache != null ? new AutomationElement(autoElement: elementBuildCache) : null;
        }

        public AutomationElement GetPreviousSibling(AutomationElement element) {
            var elementBuildCache = this._treewalker.GetPreviousSiblingElementBuildCache(element: element.IUIAutomationElement, cacheRequest: AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
            return elementBuildCache != null ? new AutomationElement(autoElement: elementBuildCache) : null;
        }

        public AutomationElement Normalize(AutomationElement element) {
            var autoElement = this._treewalker.NormalizeElementBuildCache(element: element.IUIAutomationElement, cacheRequest: AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
            return autoElement != null ? new AutomationElement(autoElement: autoElement) : null;
        }

        public AutomationElement GetParent(
            AutomationElement element,
            CacheRequest request) {
            var elementBuildCache = this._treewalker.GetParentElementBuildCache(element: element.IUIAutomationElement, cacheRequest: request.IUIAutomationCacheRequest);
            return elementBuildCache != null ? new AutomationElement(autoElement: elementBuildCache) : null;
        }

        public AutomationElement GetFirstChild(
            AutomationElement element,
            CacheRequest request) {
            var elementBuildCache = this._treewalker.GetFirstChildElementBuildCache(element: element.IUIAutomationElement, cacheRequest: request.IUIAutomationCacheRequest);
            return elementBuildCache != null ? new AutomationElement(autoElement: elementBuildCache) : null;
        }

        public AutomationElement GetLastChild(
            AutomationElement element,
            CacheRequest request) {
            var elementBuildCache = this._treewalker.GetLastChildElementBuildCache(element: element.IUIAutomationElement, cacheRequest: request.IUIAutomationCacheRequest);
            return elementBuildCache != null ? new AutomationElement(autoElement: elementBuildCache) : null;
        }

        public AutomationElement GetNextSibling(
            AutomationElement element,
            CacheRequest request) {
            var elementBuildCache = this._treewalker.GetNextSiblingElementBuildCache(element: element.IUIAutomationElement, cacheRequest: request.IUIAutomationCacheRequest);
            return elementBuildCache != null ? new AutomationElement(autoElement: elementBuildCache) : null;
        }

        public AutomationElement GetPreviousSibling(
            AutomationElement element,
            CacheRequest request) {
            var elementBuildCache = this._treewalker.GetPreviousSiblingElementBuildCache(element: element.IUIAutomationElement, cacheRequest: request.IUIAutomationCacheRequest);
            return elementBuildCache != null ? new AutomationElement(autoElement: elementBuildCache) : null;
        }

        public AutomationElement Normalize(
            AutomationElement element,
            CacheRequest request) {
            var autoElement = this._treewalker.NormalizeElementBuildCache(element: element.IUIAutomationElement, cacheRequest: request.IUIAutomationCacheRequest);
            return autoElement != null ? new AutomationElement(autoElement: autoElement) : null;
        }
    }
}