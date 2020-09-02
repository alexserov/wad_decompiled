// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.DelegateFilter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class DelegateFilter : IFilter<AutomationElement>
  {
    private Predicate<UIObject> _callback;

    public DelegateFilter(Predicate<UIObject> callback)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) callback, nameof (callback));
      this._callback = callback;
    }

    public bool Matches(AutomationElement item) => this._callback(new UIObject(item));

    public override string ToString() => StringResource.Get("DelegateFilter_ToString");
  }
}
