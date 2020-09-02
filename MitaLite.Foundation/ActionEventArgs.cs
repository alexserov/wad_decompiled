// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.ActionEventArgs
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  public class ActionEventArgs : EventArgs
  {
    private readonly string action;
    private readonly List<object> parameters;
    private static ConcurrentDictionary<string, ActionEventArgs> defaults = new ConcurrentDictionary<string, ActionEventArgs>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    public ActionEventArgs(string action, params object[] args)
    {
      Validate.StringNeitherNullNorEmpty(action, nameof (action));
      this.action = action;
      if (args != null)
        this.parameters = new List<object>((IEnumerable<object>) args);
      else
        this.parameters = new List<object>();
    }

    public string ActionString => this.action;

    public IList<object> Parameters => (IList<object>) this.parameters.AsReadOnly();

    public static ActionEventArgs GetDefault(string action) => ActionEventArgs.defaults.GetOrAdd(action, new ActionEventArgs(action, Array.Empty<object>()));
  }
}
