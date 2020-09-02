// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.CheckBox
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class CheckBox : UIObject, IToggle
  {
    private IToggle _togglePattern;
    private static IFactory<CheckBox> _factory;

    public CheckBox(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    internal CheckBox(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._togglePattern = (IToggle) new ToggleImplementation((UIObject) this);

    public UIEventWaiter GetToggledWaiter() => this._togglePattern.GetToggledWaiter();

    public virtual void Toggle() => this._togglePattern.Toggle();

    public virtual ToggleState ToggleState => this._togglePattern.ToggleState;

    public void Check()
    {
      int num = (int) ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault(nameof (Check))) != ActionResult.Handled && !this.SetToggleState(ToggleState.On))
        throw new ActionException(StringResource.Get("CheckBox_CheckFailed", (object) UIObject.SafeGetName((UIObject) this)));
    }

    public void Uncheck()
    {
      int num = (int) ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault(nameof (Uncheck))) != ActionResult.Handled && !this.SetToggleState(ToggleState.Off))
        throw new ActionException(StringResource.Get("CheckBox_UncheckFailed", (object) UIObject.SafeGetName((UIObject) this)));
    }

    public static IFactory<CheckBox> Factory
    {
      get
      {
        if (CheckBox._factory == null)
          CheckBox._factory = (IFactory<CheckBox>) new CheckBox.CheckBoxFactory();
        return CheckBox._factory;
      }
    }

    private bool SetToggleState(ToggleState toggleState)
    {
      for (int index = 0; index < 3; ++index)
      {
        if (this._togglePattern.ToggleState == toggleState)
          return true;
        if (index < 2)
        {
          using (PropertyChangedEventWaiter changedEventWaiter = new PropertyChangedEventWaiter((UIObject) this, Scope.Element, new UIProperty[1]
          {
            UIProperty.Get("Toggle.ToggleState")
          }))
          {
            this.Toggle();
            changedEventWaiter.TryWait(500);
          }
        }
      }
      return false;
    }

    private class CheckBoxFactory : IFactory<CheckBox>
    {
      public CheckBox Create(UIObject element) => new CheckBox(element);
    }
  }
}
