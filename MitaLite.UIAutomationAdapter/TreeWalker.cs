// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TreeWalker
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public sealed class TreeWalker
  {
    public static readonly TreeWalker RawViewWalker = new TreeWalker(System.Windows.Automation.Automation.RawViewCondition);
    public static readonly TreeWalker ControlViewWalker = new TreeWalker(System.Windows.Automation.Automation.ControlViewCondition);
    public static readonly TreeWalker ContentViewWalker = new TreeWalker(System.Windows.Automation.Automation.ContentViewCondition);
    private Condition _condition;
    private IUIAutomationTreeWalker _treewalker;

    public TreeWalker(Condition condition)
    {
      Validate.ArgumentNotNull((object) condition, nameof (condition));
      this._condition = condition;
      this._treewalker = System.Windows.Automation.Automation.AutomationClass.CreateTreeWalker(condition.IUIAutomationCondition);
    }

    public AutomationElement GetParent(AutomationElement element)
    {
      IUIAutomationElement elementBuildCache = this._treewalker.GetParentElementBuildCache(element.IUIAutomationElement, AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
      return elementBuildCache != null ? new AutomationElement(elementBuildCache) : (AutomationElement) null;
    }

    public AutomationElement GetFirstChild(AutomationElement element)
    {
      IUIAutomationElement elementBuildCache = this._treewalker.GetFirstChildElementBuildCache(element.IUIAutomationElement, AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
      return elementBuildCache != null ? new AutomationElement(elementBuildCache) : (AutomationElement) null;
    }

    public AutomationElement GetLastChild(AutomationElement element)
    {
      IUIAutomationElement elementBuildCache = this._treewalker.GetLastChildElementBuildCache(element.IUIAutomationElement, AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
      return elementBuildCache != null ? new AutomationElement(elementBuildCache) : (AutomationElement) null;
    }

    public AutomationElement GetNextSibling(AutomationElement element)
    {
      IUIAutomationElement elementBuildCache = this._treewalker.GetNextSiblingElementBuildCache(element.IUIAutomationElement, AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
      return elementBuildCache != null ? new AutomationElement(elementBuildCache) : (AutomationElement) null;
    }

    public AutomationElement GetPreviousSibling(AutomationElement element)
    {
      IUIAutomationElement elementBuildCache = this._treewalker.GetPreviousSiblingElementBuildCache(element.IUIAutomationElement, AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
      return elementBuildCache != null ? new AutomationElement(elementBuildCache) : (AutomationElement) null;
    }

    public AutomationElement Normalize(AutomationElement element)
    {
      IUIAutomationElement autoElement = this._treewalker.NormalizeElementBuildCache(element.IUIAutomationElement, AutomationElement.DefaultCacheRequest.IUIAutomationCacheRequest);
      return autoElement != null ? new AutomationElement(autoElement) : (AutomationElement) null;
    }

    public AutomationElement GetParent(
      AutomationElement element,
      CacheRequest request)
    {
      IUIAutomationElement elementBuildCache = this._treewalker.GetParentElementBuildCache(element.IUIAutomationElement, request.IUIAutomationCacheRequest);
      return elementBuildCache != null ? new AutomationElement(elementBuildCache) : (AutomationElement) null;
    }

    public AutomationElement GetFirstChild(
      AutomationElement element,
      CacheRequest request)
    {
      IUIAutomationElement elementBuildCache = this._treewalker.GetFirstChildElementBuildCache(element.IUIAutomationElement, request.IUIAutomationCacheRequest);
      return elementBuildCache != null ? new AutomationElement(elementBuildCache) : (AutomationElement) null;
    }

    public AutomationElement GetLastChild(
      AutomationElement element,
      CacheRequest request)
    {
      IUIAutomationElement elementBuildCache = this._treewalker.GetLastChildElementBuildCache(element.IUIAutomationElement, request.IUIAutomationCacheRequest);
      return elementBuildCache != null ? new AutomationElement(elementBuildCache) : (AutomationElement) null;
    }

    public AutomationElement GetNextSibling(
      AutomationElement element,
      CacheRequest request)
    {
      IUIAutomationElement elementBuildCache = this._treewalker.GetNextSiblingElementBuildCache(element.IUIAutomationElement, request.IUIAutomationCacheRequest);
      return elementBuildCache != null ? new AutomationElement(elementBuildCache) : (AutomationElement) null;
    }

    public AutomationElement GetPreviousSibling(
      AutomationElement element,
      CacheRequest request)
    {
      IUIAutomationElement elementBuildCache = this._treewalker.GetPreviousSiblingElementBuildCache(element.IUIAutomationElement, request.IUIAutomationCacheRequest);
      return elementBuildCache != null ? new AutomationElement(elementBuildCache) : (AutomationElement) null;
    }

    public AutomationElement Normalize(
      AutomationElement element,
      CacheRequest request)
    {
      IUIAutomationElement autoElement = this._treewalker.NormalizeElementBuildCache(element.IUIAutomationElement, request.IUIAutomationCacheRequest);
      return autoElement != null ? new AutomationElement(autoElement) : (AutomationElement) null;
    }

    public Condition Condition => this._condition;
  }
}
