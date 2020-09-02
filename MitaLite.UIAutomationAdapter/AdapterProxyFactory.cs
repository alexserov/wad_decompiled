// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AdapterProxyFactory
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    internal class AdapterProxyFactory : IUIAutomationProxyFactory {
        ClientSideProviderDescription _proxyDescription;

        internal AdapterProxyFactory(ClientSideProviderDescription proxyDescription) {
            this._proxyDescription = proxyDescription;
        }

        public IRawElementProviderSimple CreateProvider(
            IntPtr hwnd,
            int idObject,
            int idChild) {
            return this._proxyDescription.ClientSideProviderFactoryCallback(hwnd: hwnd, idChild: idChild, idObject: idObject);
        }

        public string ProxyFactoryId {
            get { return nameof(AdapterProxyFactory); }
        }

        void RegisterEvents(IUIAutomationProxyFactoryEntry factoryEntry) {
            foreach (var eventMapping in this._proxyDescription.EventMappings)
                factoryEntry.SetWinEventsForAutomationEvent(eventId: eventMapping.EventId, propertyId: eventMapping.PropertyId, winEvents: eventMapping.WinEvents);
        }

        public void Register() {
            var proxyFactoryEntry = Automation.AutomationClass.CreateProxyFactoryEntry(factory: this);
            RegisterEvents(factoryEntry: proxyFactoryEntry);
            proxyFactoryEntry.ClassName = this._proxyDescription.ClassName.ToLowerInvariant();
            if (!string.IsNullOrEmpty(value: this._proxyDescription.ImageName))
                proxyFactoryEntry.ImageName = this._proxyDescription.ImageName.ToLowerInvariant();
            proxyFactoryEntry.AllowSubstringMatch = (this._proxyDescription.Flags & ClientSideProviderMatchIndicator.AllowSubstringMatch) != ClientSideProviderMatchIndicator.None ? 1 : 0;
            proxyFactoryEntry.CanCheckBaseClass = (this._proxyDescription.Flags & ClientSideProviderMatchIndicator.DisallowBaseClassNameMatch) != ClientSideProviderMatchIndicator.None ? 0 : 1;
            var proxyFactoryMapping = Automation.AutomationClass.ProxyFactoryMapping;
            var count = proxyFactoryMapping.count;
            for (uint index = 0; index < count; ++index) {
                var proxyFactoryId = proxyFactoryMapping.GetEntry(index: index).ProxyFactory.ProxyFactoryId;
                if (-1 == proxyFactoryId.IndexOf(value: "Non-Control", comparisonType: StringComparison.OrdinalIgnoreCase) && -1 == proxyFactoryId.IndexOf(value: "Container", comparisonType: StringComparison.OrdinalIgnoreCase)) {
                    proxyFactoryMapping.InsertEntry(before: index, factory: proxyFactoryEntry);
                    break;
                }
            }
        }
    }
}