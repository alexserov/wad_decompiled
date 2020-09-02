// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Provider
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using Microsoft.Win32;
using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace MS.Internal.Mita.Foundation
{
  public class Provider
  {
    private Type _type;
    private string _friendlyName;
    private bool _isOnByDefault;
    private string _description;
    private bool _isLoaded;
    private static ProviderList _installedProviders;
    private const string _registrarTypeName = "RegistrarType";
    private const string _onByDefaultName = "OnByDefault";
    private const string _descriptionName = "Description";
    private const string _ProvidersKey = "SOFTWARE\\Microsoft\\Mita\\Providers";

    private Provider(Type type, string friendlyName, bool onByDefault, string description)
    {
      this._type = type;
      this._friendlyName = friendlyName;
      this._isOnByDefault = onByDefault;
      this._description = description;
    }

    public static void LoadProviders()
    {
      foreach (Provider installedProvider in Provider.InstalledProviders)
      {
        if (installedProvider.IsOnByDefault)
          installedProvider.Load();
      }
    }

    internal static void ReloadProviders()
    {
      foreach (Provider installedProvider in Provider.InstalledProviders)
      {
        if (installedProvider.IsLoaded || installedProvider.IsOnByDefault)
          installedProvider.Reload();
      }
    }

    public static ProviderList InstalledProviders
    {
      get
      {
        if (Provider._installedProviders == null)
        {
          List<Provider> providers = new List<Provider>();
          Microsoft.Win32.RegistryKey providersKey = Provider.GetProvidersKey(false);
          if (providersKey != null)
          {
            foreach (string subKeyName in providersKey.GetSubKeyNames())
              providers.Add(Provider.Read(providersKey, subKeyName));
          }
          Provider._installedProviders = new ProviderList(providers);
        }
        return Provider._installedProviders;
      }
    }

    internal static Provider Read(RegistryKey providersKey, string friendlyName)
    {
      RegistryKey registryKey = providersKey.OpenSubKey(friendlyName);
      string description = registryKey.GetValue("Description", (object) string.Empty) as string;
      bool result;
      string typeName = bool.TryParse(registryKey.GetValue("OnByDefault", (object) string.Empty) as string, out result) ? registryKey.GetValue("RegistrarType", (object) string.Empty) as string : throw new InvalidOperationException(StringResource.Get("InvalidProviderEntry"));
      Type type = !string.IsNullOrEmpty(typeName) ? Type.GetType(typeName, false) : throw new InvalidOperationException(StringResource.Get("InvalidProviderEntry"));
      if ((object) type == null)
        throw new InvalidOperationException(StringResource.Get("InvalidProviderEntry"));
      return new Provider(type, friendlyName, result, description);
    }

    internal static void Install(
      Type registrarType,
      string friendlyName,
      bool onByDefault,
      string description)
    {
      Validate.ArgumentNotNull((object) registrarType, nameof (registrarType));
      Validate.StringNeitherNullNorEmpty(friendlyName, nameof (friendlyName));
      Trace.WriteLine("Installing;");
      Trace.WriteLine(" friendlyName='" + friendlyName + "'");
      Trace.WriteLine(" registrarType='" + registrarType.AssemblyQualifiedName + "'");
      Trace.WriteLine(" onByDefault='" + onByDefault.ToString() + "'");
      Trace.WriteLine(" description='" + description);
      RegistryKey subKey = Provider.GetProvidersKey(true).CreateSubKey(friendlyName, true);
      subKey.SetValue("RegistrarType", (object) registrarType.AssemblyQualifiedName);
      subKey.SetValue("OnByDefault", (object) onByDefault);
      subKey.SetValue("Description", (object) description);
      Provider._installedProviders = (ProviderList) null;
    }

    internal static void Uninstall(string friendlyName)
    {
      Validate.StringNeitherNullNorEmpty(friendlyName, nameof (friendlyName));
      Trace.WriteLine("Uninstalling;");
      Trace.WriteLine(" friendlyName='" + friendlyName + "'");
      Provider.GetProvidersKey(true).DeleteSubKeyTree(friendlyName);
      Provider._installedProviders = (ProviderList) null;
    }

    public void Load()
    {
      if (this._isLoaded)
        return;
      this.Reload();
    }

    internal void Reload()
    {
      MethodInfo method = TypeExtensions.GetMethod(this._type, "Register");
      if ((object) method == null)
        throw new InvalidOperationException();
      try
      {
        method.Invoke((object) null, (object[]) null);
        this._isLoaded = true;
      }
      catch (TargetInvocationException ex)
      {
        throw new InvalidOperationException(StringResource.Get("UnableToLoadProvider"), (Exception) ex);
      }
    }

    public string Description => this._description;

    public string FriendlyName => this._friendlyName;

    public bool IsOnByDefault => this._isOnByDefault;

    public bool IsLoaded => this._isLoaded;

    internal static RegistryKey GetProvidersKey(bool openForWriting) => openForWriting ? Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Mita\\Providers", true) : Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Mita\\Providers", false);
  }
}
