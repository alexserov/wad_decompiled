// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.EventMapping
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation
{
  public class EventMapping
  {
    private readonly int eventId;
    private readonly int propertyId;
    private readonly uint[] winEvents;

    public EventMapping(int eventId, int propertyId, params uint[] winEvents)
    {
      if (eventId < 20000 || eventId > 20031)
        throw new ArgumentOutOfRangeException(nameof (eventId));
      if (propertyId < 0)
        throw new ArgumentOutOfRangeException(nameof (propertyId));
      if (winEvents == null)
        throw new ArgumentNullException(nameof (winEvents));
      if (winEvents.Length == 0)
        throw new ArgumentException("'winEvents' array cannot be empty.");
      foreach (uint winEvent in winEvents)
      {
        if (winEvent < 1U || winEvent > (uint) int.MaxValue)
          throw new ArgumentOutOfRangeException(nameof (winEvents), string.Format("The 'winEvents' element value {0} is outside the permitted range.", (object) winEvent));
      }
      this.eventId = eventId;
      this.propertyId = propertyId;
      this.winEvents = winEvents;
    }

    public int EventId => this.eventId;

    public int PropertyId => this.propertyId;

    public uint[] WinEvents => this.winEvents;
  }
}
