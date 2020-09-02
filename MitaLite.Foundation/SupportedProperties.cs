// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.SupportedProperties
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections;
using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  internal class SupportedProperties : IEnumerable<UIProperty>, IEnumerable
  {
    private UIObject _uiObject;

    internal SupportedProperties(UIObject uiObject) => this._uiObject = uiObject;

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public IEnumerator<UIProperty> GetEnumerator()
    {
      AutomationProperty[] automationPropertyArray = this._uiObject.AutomationElement.GetSupportedProperties();
      for (int index = 0; index < automationPropertyArray.Length; ++index)
      {
        AutomationProperty property = automationPropertyArray[index];
        if (property != null)
          yield return UIProperty.Get(property);
      }
      automationPropertyArray = (AutomationProperty[]) null;
    }
  }
}
