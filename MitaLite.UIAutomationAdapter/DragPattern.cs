// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.DragPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class DragPattern : BasePattern {
        public static readonly AutomationPattern Pattern = new AutomationPattern<DragPattern, IUIAutomationDragPattern>(id: 10030, programmaticName: "DragPatternIdentifiers.Pattern", wrap: Wrap);
        public static readonly AutomationProperty IsGrabbedProperty = new AutomationProperty(id: 30138, programmaticName: "DragPatternIdentifiers.IsGrabbedProperty");
        public static readonly AutomationProperty DropEffectProperty = new AutomationProperty(id: 30139, programmaticName: "DragPatternIdentifiers.DropEffectProperty");
        public static readonly AutomationProperty DropEffectsProperty = new AutomationProperty(id: 30140, programmaticName: "DragPatternIdentifiers.DropEffectsProperty");
        public static readonly AutomationProperty GrabbedItemsProperty = new AutomationProperty(id: 30144, programmaticName: "DragPatternIdentifiers.GrabbedItemsProperty");
        readonly IUIAutomationDragPattern _dragPattern;

        DragPattern(AutomationElement element, IUIAutomationDragPattern dragPattern)
            : base(el: element) {
            this._dragPattern = dragPattern;
        }

        internal static DragPattern Wrap(
            AutomationElement element,
            IUIAutomationDragPattern dragPattern) {
            return new DragPattern(element: element, dragPattern: dragPattern);
        }
    }
}