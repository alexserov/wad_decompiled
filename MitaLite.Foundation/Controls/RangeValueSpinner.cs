// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.RangeValueSpinner
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class RangeValueSpinner : UIObject, IRangeValue
  {
    private IRangeValue _rangeValuePattern;
    private static IFactory<RangeValueSpinner> _factory;

    public RangeValueSpinner(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal RangeValueSpinner(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._rangeValuePattern = (IRangeValue) new RangeValueImplementation((UIObject) this);

    public virtual void SetValue(double value) => this._rangeValuePattern.SetValue(value);

    public virtual double Value => this._rangeValuePattern.Value;

    public virtual bool IsReadOnly => this._rangeValuePattern.IsReadOnly;

    public virtual double Minimum => this._rangeValuePattern.Minimum;

    public virtual double Maximum => this._rangeValuePattern.Maximum;

    public virtual double LargeChange => this._rangeValuePattern.LargeChange;

    public virtual double SmallChange => this._rangeValuePattern.SmallChange;

    public static IFactory<RangeValueSpinner> Factory
    {
      get
      {
        if (RangeValueSpinner._factory == null)
          RangeValueSpinner._factory = (IFactory<RangeValueSpinner>) new RangeValueSpinner.RangeValueSpinnerFactory();
        return RangeValueSpinner._factory;
      }
    }

    private class RangeValueSpinnerFactory : IFactory<RangeValueSpinner>
    {
      public RangeValueSpinner Create(UIObject element) => new RangeValueSpinner(element);
    }
  }
}
