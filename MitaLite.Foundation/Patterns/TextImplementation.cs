// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.TextImplementation
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Types;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class TextImplementation : PatternImplementation<TextPattern>, IText
  {
    public TextImplementation(UIObject uiObject)
      : base(uiObject, TextPattern.Pattern)
    {
    }

    public bool SupportsTextSelection
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (SupportsTextSelection)), out overridden) == ActionResult.Handled ? (bool) overridden : (uint) this.Pattern.SupportedTextSelection > 0U;
      }
    }

    public TextPatternRange DocumentRange
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (DocumentRange)), out overridden) == ActionResult.Handled ? (TextPatternRange) overridden : this.Pattern.DocumentRange;
      }
    }

    public TextPatternRange GetSelection()
    {
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      object overridden;
      if (ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (GetSelection)), out overridden) == ActionResult.Handled)
        return (TextPatternRange) overridden;
      TextPatternRange[] selection = this.Pattern.GetSelection();
      return selection != null && selection.Length != 0 ? selection[0] : (TextPatternRange) null;
    }

    public TextPatternRange RangeFromPoint(PointI screenLocation)
    {
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      object overridden;
      return ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (RangeFromPoint), new object[1]
      {
        (object) screenLocation
      }), out overridden) == ActionResult.Handled ? (TextPatternRange) overridden : this.Pattern.RangeFromPoint(new Point((double) screenLocation.X, (double) screenLocation.Y));
    }

    public TextPatternRange RangeFromChild(UIObject childElement)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) childElement, nameof (childElement));
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      object overridden;
      return ActionHandler.Invoke(this.UIObject, new ActionEventArgs(nameof (RangeFromChild), new object[1]
      {
        (object) childElement
      }), out overridden) == ActionResult.Handled ? (TextPatternRange) overridden : this.Pattern.RangeFromChild(childElement.AutomationElement);
    }

    public TextPatternRange GetVisibleRange()
    {
      int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
      object overridden;
      if (ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (GetVisibleRange)), out overridden) == ActionResult.Handled)
        return (TextPatternRange) overridden;
      TextPatternRange[] visibleRanges = this.Pattern.GetVisibleRanges();
      return visibleRanges != null && visibleRanges.Length != 0 ? visibleRanges[0] : (TextPatternRange) null;
    }
  }
}
