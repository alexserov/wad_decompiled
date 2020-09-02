// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UIObjectNotFoundException
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public class UIObjectNotFoundException : MitaException {
        string _searchCriteria;
        string _searchDomain;

        public UIObjectNotFoundException()
            : base(message: StringResource.Get(id: "UIObjectNotFound")) {
        }

        public UIObjectNotFoundException(string message)
            : base(message: message) {
        }

        public UIObjectNotFoundException(string message, Exception innerException)
            : base(message: message, innerException: innerException) {
        }

        public UIObjectNotFoundException(string searchDomain, UICondition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            Initialize(searchDomain: searchDomain, searchCriteria: condition.ToString());
        }

        public UIObjectNotFoundException(string searchDomain, int index) {
            Initialize(searchDomain: searchDomain, searchCriteria: string.Format(provider: CultureInfo.InvariantCulture, format: "Instance number {0}", arg0: index));
        }

        public UIObjectNotFoundException(string searchDomain, UIObject uiObject) {
            Validate.ArgumentNotNull(parameter: uiObject, parameterName: nameof(uiObject));
            Initialize(searchDomain: searchDomain, searchCriteria: string.Format(provider: CultureInfo.InvariantCulture, format: "The UIObject '{0}'", arg0: uiObject.ToString()));
        }

        public UIObjectNotFoundException(string searchDomain, Predicate<UIObject> filterCallback) {
            Initialize(searchDomain: searchDomain, searchCriteria: "Elements matching a Predicate<UIObject> callback");
        }

        public UIObjectNotFoundException(string searchDomain, string searchCriteria) {
            Initialize(searchDomain: searchDomain, searchCriteria: searchCriteria);
        }

        public override string Message {
            get {
                if (string.IsNullOrEmpty(value: this._searchCriteria) || string.IsNullOrEmpty(value: this._searchDomain))
                    return base.Message;
                return StringResource.Get(id: "UIObjectNotFound_2", (object) this._searchCriteria, (object) this._searchDomain);
            }
        }

        void Initialize(string searchDomain, string searchCriteria) {
            Validate.ArgumentNotNull(parameter: searchDomain, parameterName: nameof(searchDomain));
            Validate.ArgumentNotNull(parameter: searchCriteria, parameterName: nameof(searchCriteria));
            this._searchDomain = searchDomain;
            this._searchCriteria = searchCriteria;
        }
    }
}