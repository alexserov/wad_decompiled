// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.EventMapping
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation {
    public class EventMapping {
        public EventMapping(int eventId, int propertyId, params uint[] winEvents) {
            if (eventId < 20000 || eventId > 20031)
                throw new ArgumentOutOfRangeException(paramName: nameof(eventId));
            if (propertyId < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(propertyId));
            if (winEvents == null)
                throw new ArgumentNullException(paramName: nameof(winEvents));
            if (winEvents.Length == 0)
                throw new ArgumentException(message: "'winEvents' array cannot be empty.");
            foreach (var winEvent in winEvents)
                if (winEvent < 1U || winEvent > int.MaxValue)
                    throw new ArgumentOutOfRangeException(paramName: nameof(winEvents), message: string.Format(format: "The 'winEvents' element value {0} is outside the permitted range.", arg0: winEvent));
            this.EventId = eventId;
            this.PropertyId = propertyId;
            this.WinEvents = winEvents;
        }

        public int EventId { get; }

        public int PropertyId { get; }

        public uint[] WinEvents { get; }
    }
}