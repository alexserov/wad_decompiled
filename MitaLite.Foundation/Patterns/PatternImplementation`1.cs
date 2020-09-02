// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.PatternImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Patterns
{
  public class PatternImplementation<P> where P : class
  {
    private AutomationPattern _patternIdentifier;
    private UIObject _uiObject;
    private P _pattern;

    public PatternImplementation(UIObject uiObject, AutomationPattern patternIdentifier)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) uiObject, nameof (uiObject));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) patternIdentifier, nameof (patternIdentifier));
      this._uiObject = uiObject;
      this._patternIdentifier = patternIdentifier;
    }

    public bool IsAvailable
    {
      get
      {
        int num = (int) ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault("WaitForReady"));
        object overridden;
        return ActionHandler.Invoke(this.UIObject, ActionEventArgs.GetDefault(nameof (IsAvailable)), out overridden) == ActionResult.Handled ? (bool) overridden : this.UIObject.AutomationElement.TryGetCurrentPattern(this._patternIdentifier, out object _);
      }
    }

    protected P Pattern
    {
      get
      {
        if ((object) this._pattern == null)
        {
          object patternObject;
          if (!this.UIObject.AutomationElement.TryGetCurrentPattern(this._patternIdentifier, out patternObject))
            throw new PatternNotFoundException(StringResource.Get("PatternNotFound_2", (object) typeof (P).Name, (object) this._uiObject.ToString()));
          this._pattern = patternObject as P;
          if ((object) this._pattern == null)
            throw new PatternNotFoundException(StringResource.Get("PatternNotFound_2", (object) typeof (P).Name, (object) this._uiObject.ToString()));
        }
        return this._pattern;
      }
    }

    protected UIObject UIObject => this._uiObject;
  }
}
