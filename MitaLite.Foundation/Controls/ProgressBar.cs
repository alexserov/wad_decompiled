// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ProgressBar
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class ProgressBar : UIObject, IValue
  {
    private IValue _valuePattern;
    private static IFactory<ProgressBar> _factory;

    public ProgressBar(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal ProgressBar(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._valuePattern = (IValue) new ValueImplementation((UIObject) this);

    public virtual void SetValue(string value) => this._valuePattern.SetValue(value);

    public virtual string Value => this._valuePattern.Value;

    public virtual bool IsReadOnly => this._valuePattern.IsReadOnly;

    public static IFactory<ProgressBar> Factory
    {
      get
      {
        if (ProgressBar._factory == null)
          ProgressBar._factory = (IFactory<ProgressBar>) new ProgressBar.ProgressBarFactory();
        return ProgressBar._factory;
      }
    }

    private class ProgressBarFactory : IFactory<ProgressBar>
    {
      public ProgressBar Create(UIObject element) => new ProgressBar(element);
    }
  }
}
