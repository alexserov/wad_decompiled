// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.LocalizationManager
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Collections.Generic;

namespace MS.Internal.Mita.Localization {
    public static class LocalizationManager {
        static readonly SortedDictionary<string, LocalizationProviderProxy> _registeredProviders = new SortedDictionary<string, LocalizationProviderProxy>();
        static readonly object providersLock = new object();

        public static LocalizationProviderProxy RegisterProvider(
            ILocalizationProvider provider,
            string proxyName) {
            return new LocalizationProviderProxy(provider: provider, proxyName: proxyName);
        }

        public static LocalizationProviderProxy RegisterProvider(
            LocalizationProviderProxy instance) {
            Validate.ArgumentNotNull(parameter: instance, parameterName: nameof(instance));
            lock (providersLock) {
                if (_registeredProviders.ContainsKey(key: instance.RegisteredName))
                    throw new LocalizationManagerException(message: StringResource.Get(id: "ProviderAlreadyRegistered"));
                _registeredProviders.Add(key: instance.RegisteredName, value: instance);
            }

            return instance;
        }

        public static LocalizationProviderProxy GetInstance(string proxyName) {
            LocalizationProviderProxy localizationProviderProxy = null;
            lock (providersLock) {
                if (_registeredProviders.ContainsKey(key: proxyName))
                    _registeredProviders.TryGetValue(key: proxyName, value: out localizationProviderProxy);
                else
                    localizationProviderProxy = proxyName == string.Empty ? new LocalizationProviderProxy(provider: new NonLocalizingProvider(), proxyName: string.Empty) : throw new LocalizationManagerException(message: StringResource.Get(id: "ProviderNotRegistered"));
            }

            return localizationProviderProxy;
        }

        public static void UnregisterProvider(string proxyName) {
            lock (providersLock) {
                if (!_registeredProviders.ContainsKey(key: proxyName))
                    throw new LocalizationManagerException(message: StringResource.Get(id: "UnRegisterNonExistentProvider", (object) proxyName));
                _registeredProviders.Remove(key: proxyName);
            }
        }
    }
}