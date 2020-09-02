// Decompiled with JetBrains decompiler
// Type: MitaBroker.Capabilities
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MitaBroker {
    internal class Capabilities {
        public const string SupportedCapabilities = "supportedCapabilities";
        public const string UnsupportedCapabilities = "unsupportedCapabilities";
        public const string UnsupportedRequiredCapabilities = "unsupportedRequiredCapabilities";
        const string RequiredCapabilities = "requiredCapabilities";
        const string DesiredCapabilities = "desiredCapabilities";
        public const string CapApp = "app";
        public const string CapAppArguments = "appArguments";
        public const string CapAppTopWindow = "appTopLevelWindow";
        public const string CapAppWorkingDir = "appWorkingDir";
        public const string CapPlatformName = "platformName";
        public const string CapPlatformVersion = "platformVersion";
        public const string CapWaitForAppLaunch = "ms:waitForAppLaunch";
        public const string CapExperimental = "ms:experimental-webdriver";

        public static readonly string[] supportedCapabilityEntries = new string[8] {
            "app",
            "appArguments",
            "appTopLevelWindow",
            "appWorkingDir",
            "platformName",
            "platformVersion",
            "ms:waitForAppLaunch",
            "ms:experimental-webdriver"
        };

        readonly Dictionary<string, Dictionary<string, object>> capabilitiesCollection = new Dictionary<string, Dictionary<string, object>>();

        public Capabilities(string requestedCapabilitiesJSONString) {
            this.capabilitiesCollection.Add(key: "supportedCapabilities", value: new Dictionary<string, object>());
            this.capabilitiesCollection.Add(key: "unsupportedCapabilities", value: new Dictionary<string, object>());
            this.capabilitiesCollection.Add(key: "unsupportedRequiredCapabilities", value: null);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(value: requestedCapabilitiesJSONString);
            if (dictionary.ContainsKey(key: "requiredCapabilities"))
                ProcessRequestedCapabilties(capabilitiesJSONString: dictionary[key: "requiredCapabilities"].ToString());
            if (this.capabilitiesCollection[key: "unsupportedCapabilities"].Count > 0)
                this.capabilitiesCollection[key: "unsupportedRequiredCapabilities"] = new Dictionary<string, object>(dictionary: this.capabilitiesCollection[key: "unsupportedCapabilities"]);
            if (!dictionary.ContainsKey(key: "desiredCapabilities"))
                return;
            ProcessRequestedCapabilties(capabilitiesJSONString: dictionary[key: "desiredCapabilities"].ToString());
        }

        public object GetCapabilityValue(string key, string capabilitySet) {
            object obj = null;
            if (this.capabilitiesCollection.ContainsKey(key: capabilitySet) && this.capabilitiesCollection[key: capabilitySet].ContainsKey(key: key))
                obj = this.capabilitiesCollection[key: capabilitySet][key: key];
            return obj;
        }

        public Dictionary<string, object> GetCapabilities(string capabilitySet) {
            Dictionary<string, object> dictionary = null;
            if (this.capabilitiesCollection.ContainsKey(key: capabilitySet))
                dictionary = this.capabilitiesCollection[key: capabilitySet];
            return dictionary;
        }

        public bool IsExperimental() {
            try {
                var capabilityValue = GetCapabilityValue(key: "ms:experimental-webdriver", capabilitySet: "supportedCapabilities");
                return capabilityValue != null && Convert.ToBoolean(value: capabilityValue);
            } catch {
                return false;
            }
        }

        void ProcessRequestedCapabilties(string capabilitiesJSONString) {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(value: capabilitiesJSONString);
            foreach (var key in dictionary.Keys)
                if (supportedCapabilityEntries.Contains(value: key) && !this.capabilitiesCollection[key: "supportedCapabilities"].ContainsKey(key: key))
                    this.capabilitiesCollection[key: "supportedCapabilities"][key: key] = dictionary[key: key];
                else
                    this.capabilitiesCollection[key: "unsupportedCapabilities"][key: key] = dictionary[key: key];
        }
    }
}