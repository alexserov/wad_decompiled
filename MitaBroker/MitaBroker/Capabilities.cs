// Decompiled with JetBrains decompiler
// Type: MitaBroker.Capabilities
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MitaBroker
{
  internal class Capabilities
  {
    private Dictionary<string, Dictionary<string, object>> capabilitiesCollection = new Dictionary<string, Dictionary<string, object>>();
    public const string SupportedCapabilities = "supportedCapabilities";
    public const string UnsupportedCapabilities = "unsupportedCapabilities";
    public const string UnsupportedRequiredCapabilities = "unsupportedRequiredCapabilities";
    private const string RequiredCapabilities = "requiredCapabilities";
    private const string DesiredCapabilities = "desiredCapabilities";
    public const string CapApp = "app";
    public const string CapAppArguments = "appArguments";
    public const string CapAppTopWindow = "appTopLevelWindow";
    public const string CapAppWorkingDir = "appWorkingDir";
    public const string CapPlatformName = "platformName";
    public const string CapPlatformVersion = "platformVersion";
    public const string CapWaitForAppLaunch = "ms:waitForAppLaunch";
    public const string CapExperimental = "ms:experimental-webdriver";
    public static readonly string[] supportedCapabilityEntries = new string[8]
    {
      "app",
      "appArguments",
      "appTopLevelWindow",
      "appWorkingDir",
      "platformName",
      "platformVersion",
      "ms:waitForAppLaunch",
      "ms:experimental-webdriver"
    };

    public Capabilities(string requestedCapabilitiesJSONString)
    {
      this.capabilitiesCollection.Add("supportedCapabilities", new Dictionary<string, object>());
      this.capabilitiesCollection.Add("unsupportedCapabilities", new Dictionary<string, object>());
      this.capabilitiesCollection.Add("unsupportedRequiredCapabilities", (Dictionary<string, object>) null);
      Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(requestedCapabilitiesJSONString);
      if (dictionary.ContainsKey("requiredCapabilities"))
        this.ProcessRequestedCapabilties(dictionary["requiredCapabilities"].ToString());
      if (this.capabilitiesCollection["unsupportedCapabilities"].Count > 0)
        this.capabilitiesCollection["unsupportedRequiredCapabilities"] = new Dictionary<string, object>((IDictionary<string, object>) this.capabilitiesCollection["unsupportedCapabilities"]);
      if (!dictionary.ContainsKey("desiredCapabilities"))
        return;
      this.ProcessRequestedCapabilties(dictionary["desiredCapabilities"].ToString());
    }

    public object GetCapabilityValue(string key, string capabilitySet)
    {
      object obj = (object) null;
      if (this.capabilitiesCollection.ContainsKey(capabilitySet) && this.capabilitiesCollection[capabilitySet].ContainsKey(key))
        obj = this.capabilitiesCollection[capabilitySet][key];
      return obj;
    }

    public Dictionary<string, object> GetCapabilities(string capabilitySet)
    {
      Dictionary<string, object> dictionary = (Dictionary<string, object>) null;
      if (this.capabilitiesCollection.ContainsKey(capabilitySet))
        dictionary = this.capabilitiesCollection[capabilitySet];
      return dictionary;
    }

    public bool IsExperimental()
    {
      try
      {
        object capabilityValue = this.GetCapabilityValue("ms:experimental-webdriver", "supportedCapabilities");
        return capabilityValue != null && Convert.ToBoolean(capabilityValue);
      }
      catch
      {
        return false;
      }
    }

    private void ProcessRequestedCapabilties(string capabilitiesJSONString)
    {
      Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(capabilitiesJSONString);
      foreach (string key in dictionary.Keys)
      {
        if (((IEnumerable<string>) Capabilities.supportedCapabilityEntries).Contains<string>(key) && !this.capabilitiesCollection["supportedCapabilities"].ContainsKey(key))
          this.capabilitiesCollection["supportedCapabilities"][key] = dictionary[key];
        else
          this.capabilitiesCollection["unsupportedCapabilities"][key] = dictionary[key];
      }
    }
  }
}
