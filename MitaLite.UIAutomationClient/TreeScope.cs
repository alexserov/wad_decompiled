// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.TreeScope
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

namespace UIAutomationClient {
    public enum TreeScope {
        TreeScope_Element = 1,
        TreeScope_Children = 2,
        TreeScope_Descendants = 4,
        TreeScope_Subtree = 7,
        TreeScope_Parent = 8,
        TreeScope_Ancestors = 16 // 0x00000010
    }
}