// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ValuePattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation
{
  public class ValuePattern : BasePattern
  {
    public static readonly AutomationPattern Pattern = ValuePatternIdentifiers.Pattern;
    public static readonly AutomationProperty ValueProperty = ValuePatternIdentifiers.ValueProperty;
    public static readonly AutomationProperty IsReadOnlyProperty = ValuePatternIdentifiers.IsReadOnlyProperty;
    private readonly IUIAutomationValuePattern _valuePattern;

    private ValuePattern(AutomationElement element, IUIAutomationValuePattern valuePattern)
      : base(element)
      => this._valuePattern = valuePattern;

    internal static ValuePattern Wrap(
      AutomationElement element,
      IUIAutomationValuePattern valuePattern) => new ValuePattern(element, valuePattern);

    public void SetValue(string value) => this._valuePattern.SetValue(value);

    public ValuePattern.ValuePatternInformation Cached => new ValuePattern.ValuePatternInformation(this._el, true);

    public ValuePattern.ValuePatternInformation Current => new ValuePattern.ValuePatternInformation(this._el, false);

    public struct ValuePatternInformation
    {
      private AutomationElement _el;
      private bool _useCache;

      internal ValuePatternInformation(AutomationElement el, bool useCache)
      {
        this._el = el;
        this._useCache = useCache;
      }

      public string Value => this._el.GetPatternPropertyValue(ValuePattern.ValueProperty, this._useCache).ToString();

      public bool IsReadOnly => (bool) this._el.GetPatternPropertyValue(ValuePattern.IsReadOnlyProperty, this._useCache);
    }
  }
}
