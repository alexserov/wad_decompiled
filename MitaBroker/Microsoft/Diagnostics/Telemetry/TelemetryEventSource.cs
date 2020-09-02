// Decompiled with JetBrains decompiler
// Type: Microsoft.Diagnostics.Telemetry.TelemetryEventSource
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System.Diagnostics.Tracing;

namespace Microsoft.Diagnostics.Telemetry {
    internal class TelemetryEventSource : EventSource {
        public const EventKeywords Reserved44Keyword = (EventKeywords) 17592186044416;
        public const EventKeywords TelemetryKeyword = (EventKeywords) 35184372088832;
        public const EventKeywords MeasuresKeyword = (EventKeywords) 70368744177664;
        public const EventKeywords CriticalDataKeyword = (EventKeywords) 140737488355328;
        public const EventTags CostDeferredLatency = (EventTags) 262144;
        public const EventTags CoreData = (EventTags) 524288;
        public const EventTags InjectXToken = (EventTags) 1048576;
        public const EventTags RealtimeLatency = (EventTags) 2097152;
        public const EventTags NormalLatency = (EventTags) 4194304;
        public const EventTags CriticalPersistence = (EventTags) 8388608;
        public const EventTags NormalPersistence = (EventTags) 16777216;
        public const EventTags DropPii = (EventTags) 33554432;
        public const EventTags HashPii = (EventTags) 67108864;
        public const EventTags MarkPii = (EventTags) 134217728;
        public const EventFieldTags DropPiiField = (EventFieldTags) 67108864;
        public const EventFieldTags HashPiiField = (EventFieldTags) 134217728;

        static readonly string[] microsoftTelemetryTraits = new string[2] {
            "ETW_GROUP",
            "{4f50731a-89cf-4782-b3e0-dce8c90476ba}"
        };

        static readonly string[] windowsCoreTelemetryTraits = new string[2] {
            "ETW_GROUP",
            "{c7de053a-0c2e-4a44-91a2-5222ec2ecdf1}"
        };

        public TelemetryEventSource(string eventSourceName)
            : base(eventSourceName: eventSourceName, config: EventSourceSettings.EtwSelfDescribingEventFormat, traits: microsoftTelemetryTraits) {
        }

        protected TelemetryEventSource()
            : base(settings: EventSourceSettings.EtwSelfDescribingEventFormat, traits: microsoftTelemetryTraits) {
        }

        public TelemetryEventSource(string eventSourceName, TelemetryGroup telemetryGroup)
            : base(eventSourceName: eventSourceName, config: EventSourceSettings.EtwSelfDescribingEventFormat, traits: telemetryGroup == TelemetryGroup.WindowsCoreTelemetry ? windowsCoreTelemetryTraits : microsoftTelemetryTraits) {
        }

        public static EventSourceOptions TelemetryOptions() {
            return new EventSourceOptions {
                Keywords = (EventKeywords) 35184372088832
            };
        }

        public static EventSourceOptions MeasuresOptions() {
            return new EventSourceOptions {
                Keywords = (EventKeywords) 70368744177664
            };
        }
    }
}