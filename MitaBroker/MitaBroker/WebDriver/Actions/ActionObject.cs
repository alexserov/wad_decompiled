// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.ActionObject
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System.Collections.Generic;

namespace MitaBroker.WebDriver.Actions {
    internal sealed class ActionObject {
        readonly Dictionary<string, object> dictionary;

        public ActionObject(string id, int type, int subtype) {
            Id = id;
            Type = type;
            Subtype = subtype;
            this.dictionary = new Dictionary<string, object>();
        }

        public string Id { get; }

        public int Type { get; }

        public int Subtype { get; set; }

        public ActionObject Copy() {
            return (ActionObject) MemberwiseClone();
        }

        public object Get(string key) {
            return this.dictionary[key: key];
        }

        public void Set(string key, object value) {
            this.dictionary[key: key] = value;
        }

        public bool Contains(string key) {
            return this.dictionary.ContainsKey(key: key);
        }
    }
}