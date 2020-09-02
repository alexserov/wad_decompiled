// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.NonPrintableMap
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MS.Internal.Mita.Foundation {
    internal class NonPrintableMap : KeyedCollection<string, NonPrintableMapItem> {
        public ICollection<string> Keys {
            get { return Dictionary != null ? Dictionary.Keys : new Collection<string>(list: this.Select(selector: item => GetKeyForItem(item)).ToArray()); }
        }

        protected override string GetKeyForItem(NonPrintableMapItem item) {
            return item.Name;
        }
    }
}