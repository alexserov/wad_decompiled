// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.DropTargetPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class DropTargetPattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = (AutomationPattern) new AutomationPattern<DropTargetPattern, IUIAutomationDropTargetPattern>(10031, "DropTargetPatternIdentifiers.Pattern", new Func<AutomationElement, IUIAutomationDropTargetPattern, DropTargetPattern>(DropTargetPattern.Wrap));
    public static readonly AutomationProperty DropTargetEffectProperty = new AutomationProperty(30142, "DropTargetPatternIdentifiers.DropTargetEffectProperty");
    public static readonly AutomationProperty DropTargetEffectsProperty = new AutomationProperty(30143, "DropTargetPatternIdentifiers.DropTargetEffectsProperty");
    private readonly IUIAutomationDropTargetPattern _dropTargetPattern;

    private DropTargetPattern(
      AutomationElement element,
      IUIAutomationDropTargetPattern dropTargetPattern)
      : base(element)
      => this._dropTargetPattern = dropTargetPattern;

    internal static DropTargetPattern Wrap(
      AutomationElement element,
      IUIAutomationDropTargetPattern dropTargetPattern) => new DropTargetPattern(element, dropTargetPattern);
  }
}
