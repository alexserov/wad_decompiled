// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.LocalizedStrings2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Localization;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  internal class LocalizedStrings2 : ILocalizedStrings
  {
    private IStringResourceData[] _translationTable;
    private ILocalizationProvider2 _provider;
    private AutomationElement _contextElement;
    private bool _shouldSendFeedbackToProvider;

    public LocalizedStrings2(AutomationElement contextElement, ILocalizationProvider2 provider)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) contextElement, "element");
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) provider, nameof (provider));
      this._provider = provider;
      this._contextElement = contextElement;
    }

    public IStringResourceData[] GetTranslations()
    {
      this._shouldSendFeedbackToProvider = true;
      this._translationTable = this._provider.RetrieveSimilarStrings(this._provider.ToString(), (object) this._contextElement);
      return this._translationTable;
    }

    public bool TranslationMatchFound(AutomationElement element, long index) => this._shouldSendFeedbackToProvider && this._provider.FoundSimilarMatch((object) element, this._provider.ToString(), index);
  }
}
