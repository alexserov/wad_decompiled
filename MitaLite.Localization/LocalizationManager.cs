// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.LocalizationManager
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Collections.Generic;

namespace MS.Internal.Mita.Localization
{
  public static class LocalizationManager
  {
    private static SortedDictionary<string, LocalizationProviderProxy> _registeredProviders = new SortedDictionary<string, LocalizationProviderProxy>();
    private static object providersLock = new object();

    public static LocalizationProviderProxy RegisterProvider(
      ILocalizationProvider provider,
      string proxyName) => new LocalizationProviderProxy(provider, proxyName);

    public static LocalizationProviderProxy RegisterProvider(
      LocalizationProviderProxy instance)
    {
      Validate.ArgumentNotNull((object) instance, nameof (instance));
      lock (LocalizationManager.providersLock)
      {
        if (LocalizationManager._registeredProviders.ContainsKey(instance.RegisteredName))
          throw new LocalizationManagerException(StringResource.Get("ProviderAlreadyRegistered"));
        LocalizationManager._registeredProviders.Add(instance.RegisteredName, instance);
      }
      return instance;
    }

    public static LocalizationProviderProxy GetInstance(string proxyName)
    {
      LocalizationProviderProxy localizationProviderProxy = (LocalizationProviderProxy) null;
      lock (LocalizationManager.providersLock)
      {
        if (LocalizationManager._registeredProviders.ContainsKey(proxyName))
          LocalizationManager._registeredProviders.TryGetValue(proxyName, out localizationProviderProxy);
        else
          localizationProviderProxy = proxyName == string.Empty ? new LocalizationProviderProxy((ILocalizationProvider) new NonLocalizingProvider(), string.Empty) : throw new LocalizationManagerException(StringResource.Get("ProviderNotRegistered"));
      }
      return localizationProviderProxy;
    }

    public static void UnregisterProvider(string proxyName)
    {
      lock (LocalizationManager.providersLock)
      {
        if (!LocalizationManager._registeredProviders.ContainsKey(proxyName))
          throw new LocalizationManagerException(StringResource.Get("UnRegisterNonExistentProvider", (object) proxyName));
        LocalizationManager._registeredProviders.Remove(proxyName);
      }
    }
  }
}
