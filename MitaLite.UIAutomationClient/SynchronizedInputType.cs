// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.SynchronizedInputType
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

namespace UIAutomationClient {
    public enum SynchronizedInputType {
        SynchronizedInputType_KeyUp = 1,
        SynchronizedInputType_KeyDown = 2,
        SynchronizedInputType_LeftMouseUp = 4,
        SynchronizedInputType_LeftMouseDown = 8,
        SynchronizedInputType_RightMouseUp = 16, // 0x00000010
        SynchronizedInputType_RightMouseDown = 32 // 0x00000020
    }
}