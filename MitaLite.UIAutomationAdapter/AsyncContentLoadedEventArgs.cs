// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.AsyncContentLoadedEventArgs
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation {
    public sealed class AsyncContentLoadedEventArgs : AutomationEventArgs {
        public AsyncContentLoadedEventArgs(
            AsyncContentLoadedState asyncContentState,
            double percentComplete)
            : base(eventId: AutomationElementIdentifiers.AsyncContentLoadedEvent) {
            AsyncContentLoadedState = asyncContentState;
            PercentComplete = percentComplete;
        }

        public AsyncContentLoadedState AsyncContentLoadedState { get; }

        public double PercentComplete { get; }
    }
}