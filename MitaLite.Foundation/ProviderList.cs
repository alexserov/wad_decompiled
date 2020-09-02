// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.ProviderList
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    public class ProviderList : IEnumerable<Provider>, IEnumerable {
        readonly List<Provider> _providers;

        internal ProviderList(List<Provider> providers) {
            this._providers = providers;
        }

        public Provider this[string friendlyName] {
            get {
                Provider provider = null;
                if (!TryGetProvider(friendlyName: friendlyName, provider: out provider))
                    throw new InvalidOperationException();
                return provider;
            }
        }

        public IEnumerator<Provider> GetEnumerator() {
            foreach (var provider in this._providers)
                yield return provider;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public bool TryGetProvider(string friendlyName, out Provider provider) {
            foreach (var provider1 in this._providers)
                if (string.Equals(a: provider1.FriendlyName, b: friendlyName, comparisonType: StringComparison.Ordinal)) {
                    provider = provider1;
                    return true;
                }

            provider = null;
            return false;
        }
    }
}