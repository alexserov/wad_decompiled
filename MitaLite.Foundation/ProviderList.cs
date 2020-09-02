// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.ProviderList
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  public class ProviderList : IEnumerable<Provider>, IEnumerable
  {
    private List<Provider> _providers;

    internal ProviderList(List<Provider> providers) => this._providers = providers;

    public Provider this[string friendlyName]
    {
      get
      {
        Provider provider = (Provider) null;
        if (!this.TryGetProvider(friendlyName, out provider))
          throw new InvalidOperationException();
        return provider;
      }
    }

    public bool TryGetProvider(string friendlyName, out Provider provider)
    {
      foreach (Provider provider1 in this._providers)
      {
        if (string.Equals(provider1.FriendlyName, friendlyName, StringComparison.Ordinal))
        {
          provider = provider1;
          return true;
        }
      }
      provider = (Provider) null;
      return false;
    }

    public IEnumerator<Provider> GetEnumerator()
    {
      foreach (Provider provider in this._providers)
        yield return provider;
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
