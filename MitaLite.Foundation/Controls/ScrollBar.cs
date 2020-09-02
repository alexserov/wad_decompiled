// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ScrollBar
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class ScrollBar : UIObject, IRangeValue
  {
    private static IFactory<ScrollBar> _factory;
    private IRangeValue _rangeValuePattern;

    public ScrollBar(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal ScrollBar(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this.RangeValueProvider = (IRangeValue) new RangeValueImplementation((UIObject) this);

    public virtual void SetValue(double value) => this.RangeValueProvider.SetValue(value);

    public virtual double Value => this.RangeValueProvider.Value;

    public virtual bool IsReadOnly => this.RangeValueProvider.IsReadOnly;

    public virtual double Minimum => this.RangeValueProvider.Minimum;

    public virtual double Maximum => this.RangeValueProvider.Maximum;

    public virtual double LargeChange => this.RangeValueProvider.LargeChange;

    public virtual double SmallChange => this.RangeValueProvider.SmallChange;

    public static IFactory<ScrollBar> Factory
    {
      get
      {
        if (ScrollBar._factory == null)
          ScrollBar._factory = (IFactory<ScrollBar>) new ScrollBar.ScrollBarFactory();
        return ScrollBar._factory;
      }
    }

    protected IRangeValue RangeValueProvider
    {
      get => this._rangeValuePattern;
      set => this._rangeValuePattern = value;
    }

    private class ScrollBarFactory : IFactory<ScrollBar>
    {
      public ScrollBar Create(UIObject element) => new ScrollBar(element);
    }
  }
}
