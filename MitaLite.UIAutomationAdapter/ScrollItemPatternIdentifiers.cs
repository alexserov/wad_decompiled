﻿// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ScrollItemPatternIdentifiers
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public static class ScrollItemPatternIdentifiers {
        public static readonly AutomationPattern Pattern = new AutomationPattern<ScrollItemPattern, IUIAutomationScrollItemPattern>(id: 10017, programmaticName: "ScrollItemPatternIdentifiers.Pattern", wrap: ScrollItemPattern.Wrap);
    }
}