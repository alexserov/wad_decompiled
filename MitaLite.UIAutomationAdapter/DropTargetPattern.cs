// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.DropTargetPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class DropTargetPattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<DropTargetPattern, IUIAutomationDropTargetPattern>(id: 10031, programmaticName: "DropTargetPatternIdentifiers.Pattern", wrap: Wrap);
        public static readonly AutomationProperty DropTargetEffectProperty = new AutomationProperty(id: 30142, programmaticName: "DropTargetPatternIdentifiers.DropTargetEffectProperty");
        public static readonly AutomationProperty DropTargetEffectsProperty = new AutomationProperty(id: 30143, programmaticName: "DropTargetPatternIdentifiers.DropTargetEffectsProperty");
        readonly IUIAutomationDropTargetPattern _dropTargetPattern;

        DropTargetPattern(
            AutomationElement element,
            IUIAutomationDropTargetPattern dropTargetPattern)
            : base(el: element) {
            this._dropTargetPattern = dropTargetPattern;
        }

        internal static DropTargetPattern Wrap(
            AutomationElement element,
            IUIAutomationDropTargetPattern dropTargetPattern) {
            return new DropTargetPattern(element: element, dropTargetPattern: dropTargetPattern);
        }
    }
}