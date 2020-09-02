// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.SupportedProperties
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    internal class SupportedProperties : IEnumerable<UIProperty>, IEnumerable {
        readonly UIObject _uiObject;

        internal SupportedProperties(UIObject uiObject) {
            this._uiObject = uiObject;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<UIProperty> GetEnumerator() {
            var automationPropertyArray = this._uiObject.AutomationElement.GetSupportedProperties();
            for (var index = 0; index < automationPropertyArray.Length; ++index) {
                var property = automationPropertyArray[index];
                if (property != null)
                    yield return UIProperty.Get(property: property);
            }

            automationPropertyArray = null;
        }
    }
}