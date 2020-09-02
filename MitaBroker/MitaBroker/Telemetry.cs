// Decompiled with JetBrains decompiler
// Type: MitaBroker.Telemetry
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using Microsoft.Diagnostics.Telemetry;
using Microsoft.Diagnostics.Telemetry.Internal;
using System.Diagnostics.Tracing;

namespace MitaBroker
{
  internal class Telemetry
  {
    private static EventSourceOptions TelemetryCriticalOption = new EventSourceOptions()
    {
      Keywords = (EventKeywords) 140737488355328
    };

    public static void LogRequest(
      string codePath,
      string locatorStrategy,
      string timeElapsed,
      string guid,
      string result)
    {
      using (EventSource eventSource = (EventSource) new TelemetryEventSource("Microsoft.Windows.WinAppDriver"))
        eventSource.Write("SearchElement", MitaBroker.Telemetry.TelemetryCriticalOption, new
        {
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
