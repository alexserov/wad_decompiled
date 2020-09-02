// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.IWindow
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Patterns {
    public interface IWindow {
        bool CanMaximize { get; }

        bool CanMinimize { get; }

        bool IsModal { get; }

        WindowVisualState WindowVisualState { get; }

        WindowInteractionState WindowInteractionState { get; }

        bool IsTopmost { get; }
        void SetWindowVisualState(WindowVisualState state);

        void Close();

        void WaitForInputIdle(int milliseconds);

        UIEventWaiter GetWindowClosedWaiter();
    }
}