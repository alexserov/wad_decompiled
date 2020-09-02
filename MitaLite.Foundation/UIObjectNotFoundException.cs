// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UIObjectNotFoundException
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Globalization;

namespace MS.Internal.Mita.Foundation
{
  public class UIObjectNotFoundException : MitaException
  {
    private string _searchDomain;
    private string _searchCriteria;

    public UIObjectNotFoundException()
      : base(StringResource.Get("UIObjectNotFound"))
    {
    }

    public UIObjectNotFoundException(string message)
      : base(message)
    {
    }

    public UIObjectNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public UIObjectNotFoundException(string searchDomain, UICondition condition)
    {
      Validate.ArgumentNotNull((object) condition, nameof (condition));
      this.Initialize(searchDomain, condition.ToString());
    }

    public UIObjectNotFoundException(string searchDomain, int index) => this.Initialize(searchDomain, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Instance number {0}", (object) index));

    public UIObjectNotFoundException(string searchDomain, UIObject uiObject)
    {
      Validate.ArgumentNotNull((object) uiObject, nameof (uiObject));
      this.Initialize(searchDomain, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "The UIObject '{0}'", (object) uiObject.ToString()));
    }

    public UIObjectNotFoundException(string searchDomain, Predicate<UIObject> filterCallback) => this.Initialize(searchDomain, "Elements matching a Predicate<UIObject> callback");

    public UIObjectNotFoundException(string searchDomain, string searchCriteria) => this.Initialize(searchDomain, searchCriteria);

    private void Initialize(string searchDomain, string searchCriteria)
    {
      Validate.ArgumentNotNull((object) searchDomain, nameof (searchDomain));
      Validate.ArgumentNotNull((object) searchCriteria, nameof (searchCriteria));
      this._searchDomain = searchDomain;
      this._searchCriteria = searchCriteria;
    }

    public override string Message
    {
      get
      {
        if (string.IsNullOrEmpty(this._searchCriteria) || string.IsNullOrEmpty(this._searchDomain))
          return base.Message;
        return StringResource.Get("UIObjectNotFound_2", (object) this._searchCriteria, (object) this._searchDomain);
      }
    }
  }
}
