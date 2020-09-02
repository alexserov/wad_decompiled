// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Waiters.PropertyChangedEventSource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Waiters
{
  public class PropertyChangedEventSource : EventSource
  {
    private AutomationProperty[] _properties;
    private UIObject _root;
    private Scope _scope;
    private WeakReference _sinkReference;
    private AutomationPropertyChangedEventHandler _handlingDelegate;

    public PropertyChangedEventSource(UIObject root, Scope scope)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) scope, nameof (scope));
      this._root = Cache.PopulateDefaultCache(root);
      this._scope = scope;
      List<AutomationProperty> automationPropertyList = new List<AutomationProperty>();
      foreach (UIProperty allProperty in UIProperty.AllProperties)
        automationPropertyList.Add(allProperty.Property);
      this._properties = new AutomationProperty[automationPropertyList.Count];
      automationPropertyList.CopyTo(this._properties);
    }

    public PropertyChangedEventSource(UIObject root, Scope scope, params UIProperty[] uiProperties)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) scope, nameof (scope));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) uiProperties, nameof (uiProperties));
      this._root = root;
      this._scope = scope;
      this._properties = new AutomationProperty[uiProperties.Length];
      for (int index = 0; index < uiProperties.Length; ++index)
      {
        if (uiProperties[index] != null && uiProperties[index].Property != null)
          this._properties[index] = uiProperties[index].Property;
      }
    }

    protected override void Dispose(bool disposing) => base.Dispose(disposing);

    public override void Start(IEventSink sink)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) sink, nameof (sink));
      this.Stop();
      this._sinkReference = new WeakReference((object) sink);
      this._sinkReference.Target = (object) sink;
      this._handlingDelegate = new AutomationPropertyChangedEventHandler(this.Handler);
      System.Windows.Automation.Automation.AddAutomationPropertyChangedEventHandler(this._root.AutomationElement, (TreeScope) this._scope, this._handlingDelegate, this._properties);
      Log.Out("{0} Started", (object) this.ToString());
    }

    public override void Stop()
    {
      if (!this.IsStarted)
        return;
      System.Windows.Automation.Automation.RemoveAutomationPropertyChangedEventHandler(this._root.AutomationElement, this._handlingDelegate);
      this._handlingDelegate = (AutomationPropertyChangedEventHandler) null;
      this._sinkReference = (WeakReference) null;
      Log.Out("{0} Stopped", (object) this.ToString());
    }

    public override bool IsStarted => this._handlingDelegate != null;

    private void Handler(object sender, EventArgs e)
    {
      Log.Out("{0} saw event {1}", (object) this.ToString(), e != null ? (object) e.ToString() : (object) "null");
      if (this._sinkReference == null || !(this._sinkReference.Target is IEventSink target))
        return;
      target.SinkEvent(new WaiterEventArgs(sender, e));
    }
  }
}
