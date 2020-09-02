// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Edit
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;
using System.Windows.Automation.Text;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class Edit : UIObject, IText, IValue
  {
    private IText _textPattern;
    private IValue _valuePattern;
    private static IFactory<Edit> _factory;

    public Edit(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal Edit(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize()
    {
      this._textPattern = (IText) new TextImplementation((UIObject) this);
      this._valuePattern = (IValue) new ValueImplementation((UIObject) this);
    }

    public virtual bool SupportsTextSelection => this._textPattern.SupportsTextSelection;

    public virtual TextPatternRange DocumentRange => this._textPattern.DocumentRange;

    public virtual TextPatternRange GetSelection() => this._textPattern.GetSelection();

    public virtual TextPatternRange RangeFromPoint(PointI screenLocation) => this._textPattern.RangeFromPoint(screenLocation);

    public virtual TextPatternRange RangeFromChild(UIObject childElement) => this._textPattern.RangeFromChild(childElement);

    public virtual TextPatternRange GetVisibleRange() => this._textPattern.GetVisibleRange();

    public virtual void SetValue(string value) => this._valuePattern.SetValue(value);

    public virtual string Value => this._valuePattern.Value;

    public bool IsReadOnly => this._valuePattern.IsReadOnly;

    public static IFactory<Edit> Factory
    {
      get
      {
        if (Edit._factory == null)
          Edit._factory = (IFactory<Edit>) new Edit.EditFactory();
        return Edit._factory;
      }
    }

    private class EditFactory : IFactory<Edit>
    {
      public Edit Create(UIObject element) => new Edit(element);
    }
  }
}
