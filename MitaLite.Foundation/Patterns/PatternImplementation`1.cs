// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.PatternImplementation`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Patterns {
    public class PatternImplementation<P> where P : class {
        P _pattern;
        readonly AutomationPattern _patternIdentifier;

        public PatternImplementation(UIObject uiObject, AutomationPattern patternIdentifier) {
            Validate.ArgumentNotNull(parameter: uiObject, parameterName: nameof(uiObject));
            Validate.ArgumentNotNull(parameter: patternIdentifier, parameterName: nameof(patternIdentifier));
            UIObject = uiObject;
            this._patternIdentifier = patternIdentifier;
        }

        public bool IsAvailable {
            get {
                var num = (int) ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: "WaitForReady"));
                object overridden;
                return ActionHandler.Invoke(sender: UIObject, actionInfo: ActionEventArgs.GetDefault(action: nameof(IsAvailable)), overridden: out overridden) == ActionResult.Handled ? (bool) overridden : UIObject.AutomationElement.TryGetCurrentPattern(pattern: this._patternIdentifier, patternObject: out var _);
            }
        }

        protected P Pattern {
            get {
                if (this._pattern == null) {
                    object patternObject;
                    if (!UIObject.AutomationElement.TryGetCurrentPattern(pattern: this._patternIdentifier, patternObject: out patternObject))
                        throw new PatternNotFoundException(message: StringResource.Get(id: "PatternNotFound_2", (object) typeof(P).Name, (object) UIObject.ToString()));
                    this._pattern = patternObject as P;
                    if (this._pattern == null)
                        throw new PatternNotFoundException(message: StringResource.Get(id: "PatternNotFound_2", (object) typeof(P).Name, (object) UIObject.ToString()));
                }

                return this._pattern;
            }
        }

        protected UIObject UIObject { get; }
    }
}