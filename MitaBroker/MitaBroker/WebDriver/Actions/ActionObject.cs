// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.ActionObject
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System.Collections.Generic;

namespace MitaBroker.WebDriver.Actions
{
  internal sealed class ActionObject
  {
    private Dictionary<string, object> dictionary;

    public string Id { get; private set; }

    public int Type { get; private set; }

    public int Subtype { get; set; }

    public ActionObject(string id, int type, int subtype)
    {
      this.Id = id;
      this.Type = type;
      this.Subtype = subtype;
      this.dictionary = new Dictionary<string, object>();
    }

    public ActionObject Copy() => (ActionObject) this.MemberwiseClone();

    public object Get(string key) => this.dictionary[key];

    public void Set(string key, object value) => this.dictionary[key] = value;

    public bool Contains(string key) => this.dictionary.ContainsKey(key);
  }
}
