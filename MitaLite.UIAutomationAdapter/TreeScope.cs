// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TreeScope
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Automation {
    [Flags]
    public enum TreeScope {
        Ancestors = 16, // 0x00000010
        Children = 2,
        Descendants = 4,
        Element = 1,
        Parent = 8,
        Subtree = Element | Descendants | Children // 0x00000007
    }
}