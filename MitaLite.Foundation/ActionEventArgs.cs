// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.ActionEventArgs
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public class ActionEventArgs : EventArgs {
        static readonly ConcurrentDictionary<string, ActionEventArgs> defaults = new ConcurrentDictionary<string, ActionEventArgs>(comparer: StringComparer.OrdinalIgnoreCase);
        readonly List<object> parameters;

        public ActionEventArgs(string action, params object[] args) {
            Validate.StringNeitherNullNorEmpty(parameter: action, parameterName: nameof(action));
            this.ActionString = action;
            if (args != null)
                this.parameters = new List<object>(collection: args);
            else
                this.parameters = new List<object>();
        }

        public string ActionString { get; }

        public IList<object> Parameters {
            get { return this.parameters.AsReadOnly(); }
        }

        public static ActionEventArgs GetDefault(string action) {
            return defaults.GetOrAdd(key: action, value: new ActionEventArgs(action: action, args: Array.Empty<object>()));
        }
    }
}