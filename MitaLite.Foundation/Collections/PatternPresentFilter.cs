// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.PatternPresentFilter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class PatternPresentFilter : IFilter<AutomationElement>
  {
    private AutomationPattern _pattern;

    public PatternPresentFilter(AutomationPattern pattern)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) pattern, nameof (pattern));
      this._pattern = pattern;
    }

    public bool Matches(AutomationElement item)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) item, nameof (item));
      object patternObject = (object) null;
      return item.TryGetCurrentPattern(this._pattern, out patternObject);
    }

    public override string ToString() => StringResource.Get("PatternPresentFilter_ToString_1", (object) this._pattern.ToString());
  }
}
