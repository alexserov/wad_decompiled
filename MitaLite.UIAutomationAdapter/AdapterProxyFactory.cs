// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AdapterProxyFactory
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;
using UIAutomationClient;

namespace System.Windows.Automation
{
  internal class AdapterProxyFactory : IUIAutomationProxyFactory
  {
    private ClientSideProviderDescription _proxyDescription;

    internal AdapterProxyFactory(ClientSideProviderDescription proxyDescription) => this._proxyDescription = proxyDescription;

    private void RegisterEvents(IUIAutomationProxyFactoryEntry factoryEntry)
    {
      foreach (EventMapping eventMapping in (IEnumerable<EventMapping>) this._proxyDescription.EventMappings)
        factoryEntry.SetWinEventsForAutomationEvent(eventMapping.EventId, eventMapping.PropertyId, eventMapping.WinEvents);
    }

    public void Register()
    {
      IUIAutomationProxyFactoryEntry proxyFactoryEntry = System.Windows.Automation.Automation.AutomationClass.CreateProxyFactoryEntry((IUIAutomationProxyFactory) this);
      this.RegisterEvents(proxyFactoryEntry);
      proxyFactoryEntry.ClassName = this._proxyDescription.ClassName.ToLowerInvariant();
      if (!string.IsNullOrEmpty(this._proxyDescription.ImageName))
        proxyFactoryEntry.ImageName = this._proxyDescription.ImageName.ToLowerInvariant();
      proxyFactoryEntry.AllowSubstringMatch = (this._proxyDescription.Flags & ClientSideProviderMatchIndicator.AllowSubstringMatch) != ClientSideProviderMatchIndicator.None ? 1 : 0;
      proxyFactoryEntry.CanCheckBaseClass = (this._proxyDescription.Flags & ClientSideProviderMatchIndicator.DisallowBaseClassNameMatch) != ClientSideProviderMatchIndicator.None ? 0 : 1;
      IUIAutomationProxyFactoryMapping proxyFactoryMapping = System.Windows.Automation.Automation.AutomationClass.ProxyFactoryMapping;
      uint count = proxyFactoryMapping.count;
      for (uint index = 0; index < count; ++index)
      {
        string proxyFactoryId = proxyFactoryMapping.GetEntry(index).ProxyFactory.ProxyFactoryId;
        if (-1 == proxyFactoryId.IndexOf("Non-Control", StringComparison.OrdinalIgnoreCase) && -1 == proxyFactoryId.IndexOf("Container", StringComparison.OrdinalIgnoreCase))
        {
          proxyFactoryMapping.InsertEntry(index, proxyFactoryEntry);
          break;
        }
      }
    }

    public IRawElementProviderSimple CreateProvider(
      IntPtr hwnd,
      int idObject,
      int idChild) => this._proxyDescription.ClientSideProviderFactoryCallback(hwnd, idChild, idObject);

    public string ProxyFactoryId => nameof (AdapterProxyFactory);
  }
}
