// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ToggleButton
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Foundation.Waiters;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class ToggleButton : UIObject, IToggle
  {
    private IToggle _togglePattern;
    private static IFactory<ToggleButton> _factory;

    public ToggleButton(UIObject uiObject)
      : base(uiObject)
      => this.Initialize();

    public ToggleButton(AutomationElement element)
      : base(element)
      => this.Initialize();

    private void Initialize() => this._togglePattern = (IToggle) new ToggleImplementation((UIObject) this);

    public ToggleState ToggleState => this._togglePattern.ToggleState;

    public virtual void Toggle() => this._togglePattern.Toggle();

    public UIEventWaiter GetToggledWaiter() => this._togglePattern.GetToggledWaiter();

    public void Check()
    {
      int num = (int) ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("Toggle")) != ActionResult.Handled && !this.SetToggleState(ToggleState.On))
        throw new ActionException(StringResource.Get("ToggleButton_CheckFailed", (object) UIObject.SafeGetName((UIObject) this)));
    }

    public void Uncheck()
    {
      int num = (int) ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("WaitForReady"));
      if (ActionHandler.Invoke((UIObject) this, ActionEventArgs.GetDefault("Toggle")) != ActionResult.Handled && !this.SetToggleState(ToggleState.Off))
        throw new ActionException(StringResource.Get("ToggleButton_UncheckFailed", (object) UIObject.SafeGetName((UIObject) this)));
    }

    public static IFactory<ToggleButton> Factory
    {
      get
      {
        if (ToggleButton._factory == null)
          ToggleButton._factory = (IFactory<ToggleButton>) new ToggleButton.ToggleButtonFactory();
        return ToggleButton._factory;
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

    private class ToggleButtonFactory : IFactory<ToggleButton>
    {
      public ToggleButton Create(UIObject element) => new ToggleButton(element);
    }
  }
}
