// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ToggleSwitch
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class ToggleSwitch : UIObject, IToggle
  {
    private IToggle _togglePattern;
    private static IFactory<ToggleSwitch> _factory;

    public ToggleSwitch(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    public ToggleSwitch(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._togglePattern = (IToggle) new ToggleImplementation((UIObject) this);

    public ToggleState ToggleState => this._togglePattern.ToggleState;

    public virtual void Toggle() => this._togglePattern.Toggle();

    public UIEventWaiter GetToggledWaiter() => this._togglePattern.GetToggledWaiter();

    public void TurnOn()
    {
      int num = (int) ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("Toggle")) != ActionResult.Handled && !this.SetToggleState(ToggleState.On))
        throw new ActionException(StringResource.Get("ToggleSwitch_TurnOnFailed", (object) UIObject.SafeGetName((UIObject) this)));
    }

    public void TurnOff()
    {
      int num = (int) ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("Toggle")) != ActionResult.Handled && !this.SetToggleState(ToggleState.Off))
        throw new ActionException(StringResource.Get("ToggleSwitch_TurnOffFailed", (object) UIObject.SafeGetName((UIObject) this)));
    }

    public static IFactory<ToggleSwitch> Factory
    {
      get
      {
        if (ToggleSwitch._factory == null)
          ToggleSwitch._factory = (IFactory<ToggleSwitch>) new ToggleSwitch.ToggleSwitchFactory();
        return ToggleSwitch._factory;
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
            if (!changedEventWaiter.TryWait())
              Log.Out("ToggleState did not change before timeout.");
          }
        }
      }
      return false;
    }

    private class ToggleSwitchFactory : IFactory<ToggleSwitch>
    {
      public ToggleSwitch Create(UIObject element) => new ToggleSwitch(element);
    }
  }
}
