// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ClientSideProviderDescription
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;

namespace System.Windows.Automation
{
  public struct ClientSideProviderDescription
  {
    private readonly string _className;
    private readonly ClientSideProviderFactoryCallback _callback;
    private readonly ClientSideProviderMatchIndicator _flags;
    private readonly string _imageName;
    private readonly List<EventMapping> _eventMappings;

    public ClientSideProviderDescription(
      ClientSideProviderFactoryCallback clientSideProviderFactoryCallback,
      string className)
      : this(clientSideProviderFactoryCallback, className, string.Empty, ClientSideProviderMatchIndicator.None, (IEnumerable<EventMapping>) null)
    {
    }

    public ClientSideProviderDescription(
      ClientSideProviderFactoryCallback clientSideProviderFactoryCallback,
      string className,
      IEnumerable<EventMapping> eventMappings)
      : this(clientSideProviderFactoryCallback, className, string.Empty, ClientSideProviderMatchIndicator.None, eventMappings)
    {
    }

    public ClientSideProviderDescription(
      ClientSideProviderFactoryCallback clientSideProviderFactoryCallback,
      string className,
      string imageName,
      ClientSideProviderMatchIndicator flags,
      IEnumerable<EventMapping> eventMappings)
    {
      this._callback = clientSideProviderFactoryCallback;
      this._className = className;
      this._imageName = imageName;
      this._flags = flags;
      this._eventMappings = eventMappings != null ? new List<EventMapping>(eventMappings) : new List<EventMapping>();
    }

    public string ClassName => this._className;

    public ClientSideProviderFactoryCallback ClientSideProviderFactoryCallback => this._callback;

    public ClientSideProviderMatchIndicator Flags => this._flags;

    public string ImageName => this._imageName;

    public IList<EventMapping> EventMappings => (IList<EventMapping>) this._eventMappings;
  }
}
