// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Provider
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public class Provider {
        const string _registrarTypeName = "RegistrarType";
        const string _onByDefaultName = "OnByDefault";
        const string _descriptionName = "Description";
        const string _ProvidersKey = "SOFTWARE\\Microsoft\\Mita\\Providers";
        static ProviderList _installedProviders;
        readonly Type _type;

        Provider(Type type, string friendlyName, bool onByDefault, string description) {
            this._type = type;
            FriendlyName = friendlyName;
            IsOnByDefault = onByDefault;
            Description = description;
        }

        public static ProviderList InstalledProviders {
            get {
                if (_installedProviders == null) {
                    var providers = new List<Provider>();
                    var providersKey = GetProvidersKey(openForWriting: false);
                    if (providersKey != null)
                        foreach (var subKeyName in providersKey.GetSubKeyNames())
                            providers.Add(item: Read(providersKey: providersKey, friendlyName: subKeyName));
                    _installedProviders = new ProviderList(providers: providers);
                }

                return _installedProviders;
            }
        }

        public string Description { get; }

        public string FriendlyName { get; }

        public bool IsOnByDefault { get; }

        public bool IsLoaded { get; set; }

        public static void LoadProviders() {
            foreach (var installedProvider in InstalledProviders)
                if (installedProvider.IsOnByDefault)
                    installedProvider.Load();
        }

        internal static void ReloadProviders() {
            foreach (var installedProvider in InstalledProviders)
                if (installedProvider.IsLoaded || installedProvider.IsOnByDefault)
                    installedProvider.Reload();
        }

        internal static Provider Read(RegistryKey providersKey, string friendlyName) {
            var registryKey = providersKey.OpenSubKey(name: friendlyName);
            var description = registryKey.GetValue(name: "Description", defaultValue: string.Empty) as string;
            bool result;
            var typeName = bool.TryParse(value: registryKey.GetValue(name: "OnByDefault", defaultValue: string.Empty) as string, result: out result) ? registryKey.GetValue(name: "RegistrarType", defaultValue: string.Empty) as string : throw new InvalidOperationException(message: StringResource.Get(id: "InvalidProviderEntry"));
            var type = !string.IsNullOrEmpty(value: typeName) ? Type.GetType(typeName: typeName, throwOnError: false) : throw new InvalidOperationException(message: StringResource.Get(id: "InvalidProviderEntry"));
            if ((object) type == null)
                throw new InvalidOperationException(message: StringResource.Get(id: "InvalidProviderEntry"));
            return new Provider(type: type, friendlyName: friendlyName, onByDefault: result, description: description);
        }

        internal static void Install(
            Type registrarType,
            string friendlyName,
            bool onByDefault,
            string description) {
            Validate.ArgumentNotNull(parameter: registrarType, parameterName: nameof(registrarType));
            Validate.StringNeitherNullNorEmpty(parameter: friendlyName, parameterName: nameof(friendlyName));
            Trace.WriteLine(message: "Installing;");
            Trace.WriteLine(message: " friendlyName='" + friendlyName + "'");
            Trace.WriteLine(message: " registrarType='" + registrarType.AssemblyQualifiedName + "'");
            Trace.WriteLine(message: " onByDefault='" + onByDefault + "'");
            Trace.WriteLine(message: " description='" + description);
            var subKey = GetProvidersKey(openForWriting: true).CreateSubKey(subkey: friendlyName, writable: true);
            subKey.SetValue(name: "RegistrarType", value: registrarType.AssemblyQualifiedName);
            subKey.SetValue(name: "OnByDefault", value: onByDefault);
            subKey.SetValue(name: "Description", value: description);
            _installedProviders = null;
        }

        internal static void Uninstall(string friendlyName) {
            Validate.StringNeitherNullNorEmpty(parameter: friendlyName, parameterName: nameof(friendlyName));
            Trace.WriteLine(message: "Uninstalling;");
            Trace.WriteLine(message: " friendlyName='" + friendlyName + "'");
            GetProvidersKey(openForWriting: true).DeleteSubKeyTree(subkey: friendlyName);
            _installedProviders = null;
        }

        public void Load() {
            if (IsLoaded)
                return;
            Reload();
        }

        internal void Reload() {
            var method = TypeExtensions.GetMethod(type: this._type, name: "Register");
            if ((object) method == null)
                throw new InvalidOperationException();
            try {
                method.Invoke(obj: null, parameters: null);
                IsLoaded = true;
            } catch (TargetInvocationException ex) {
                throw new InvalidOperationException(message: StringResource.Get(id: "UnableToLoadProvider"), innerException: ex);
            }
        }

        internal static RegistryKey GetProvidersKey(bool openForWriting) {
            return openForWriting ? Registry.LocalMachine.CreateSubKey(subkey: "SOFTWARE\\Microsoft\\Mita\\Providers", writable: true) : Registry.LocalMachine.OpenSubKey(name: "SOFTWARE\\Microsoft\\Mita\\Providers", writable: false);
        }
    }
}