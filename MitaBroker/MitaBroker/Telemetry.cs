// Decompiled with JetBrains decompiler
// Type: MitaBroker.Telemetry
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System.Diagnostics.Tracing;
using Microsoft.Diagnostics.Telemetry;
using Microsoft.Diagnostics.Telemetry.Internal;

namespace MitaBroker {
    internal class Telemetry {
        static readonly EventSourceOptions TelemetryCriticalOption = new EventSourceOptions {
            Keywords = (EventKeywords) 140737488355328
        };

        public static void LogRequest(
            string codePath,
            string locatorStrategy,
            string timeElapsed,
            string guid,
            string result) {
            using (EventSource eventSource = new TelemetryEventSource(eventSourceName: "Microsoft.Windows.WinAppDriver")) {
                eventSource.Write(eventName: "SearchElement", options: TelemetryCriticalOption, data: new {
                    _1 = PartA_PrivTags.ProductAndServicePerformance,
                    CodePath = codePath,
                    LocatorStrategy = locatorStrategy,
                    RequestTime = timeElapsed,
                    GUID = guid,
                    Result = result
                });
            }
        }
    }
}