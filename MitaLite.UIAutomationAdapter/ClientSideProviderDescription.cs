// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ClientSideProviderDescription
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;

namespace System.Windows.Automation {
    public struct ClientSideProviderDescription {
        readonly List<EventMapping> _eventMappings;

        public ClientSideProviderDescription(
            ClientSideProviderFactoryCallback clientSideProviderFactoryCallback,
            string className)
            : this(clientSideProviderFactoryCallback: clientSideProviderFactoryCallback, className: className, imageName: string.Empty, flags: ClientSideProviderMatchIndicator.None, eventMappings: null) {
        }

        public ClientSideProviderDescription(
            ClientSideProviderFactoryCallback clientSideProviderFactoryCallback,
            string className,
            IEnumerable<EventMapping> eventMappings)
            : this(clientSideProviderFactoryCallback: clientSideProviderFactoryCallback, className: className, imageName: string.Empty, flags: ClientSideProviderMatchIndicator.None, eventMappings: eventMappings) {
        }

        public ClientSideProviderDescription(
            ClientSideProviderFactoryCallback clientSideProviderFactoryCallback,
            string className,
            string imageName,
            ClientSideProviderMatchIndicator flags,
            IEnumerable<EventMapping> eventMappings) {
            this.ClientSideProviderFactoryCallback = clientSideProviderFactoryCallback;
            this.ClassName = className;
            this.ImageName = imageName;
            this.Flags = flags;
            this._eventMappings = eventMappings != null ? new List<EventMapping>(collection: eventMappings) : new List<EventMapping>();
        }

        public string ClassName { get; }

        public ClientSideProviderFactoryCallback ClientSideProviderFactoryCallback { get; }

        public ClientSideProviderMatchIndicator Flags { get; }

        public string ImageName { get; }

        public IList<EventMapping> EventMappings {
            get { return this._eventMappings; }
        }
    }
}