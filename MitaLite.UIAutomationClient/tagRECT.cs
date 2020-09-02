// Decompiled with JetBrains decompiler
// Type: UIAutomationClient.tagRECT
// Assembly: MitaLite.UIAutomationClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3836D12D-FB98-4220-906F-A977A4708DDF
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationClient.dll

using System.Runtime.InteropServices;

namespace UIAutomationClient {
    [StructLayout(layoutKind: LayoutKind.Sequential, Pack = 4)]
    public struct tagRECT {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}