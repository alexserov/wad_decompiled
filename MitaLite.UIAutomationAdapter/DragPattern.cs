// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.DragPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class DragPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<DragPattern, IUIAutomationDragPattern>(10030, "DragPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationDragPattern, DragPattern>(DragPattern.Wrap));
    public static readonly AutomationProperty IsGrabbedProperty = new AutomationProperty(30138, "DragPatternIdentifiers.IsGrabbedProperty");
    public static readonly AutomationProperty DropEffectProperty = new AutomationProperty(30139, "DragPatternIdentifiers.DropEffectProperty");
    public static readonly AutomationProperty DropEffectsProperty = new AutomationProperty(30140, "DragPatternIdentifiers.DropEffectsProperty");
    public static readonly AutomationProperty GrabbedItemsProperty = new AutomationProperty(30144, "DragPatternIdentifiers.GrabbedItemsProperty");
    private readonly IUIAutomationDragPattern _dragPattern;

    private DragPattern(AutomationElement element, IUIAutomationDragPattern dragPattern)
      : base(element)
      => this._dragPattern = dragPattern;

    internal static DragPattern Wrap(
      AutomationElement element,
      IUIAutomationDragPattern dragPattern) => new DragPattern(element, dragPattern);
  }
}
