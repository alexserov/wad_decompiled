// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.LocalizedStrings2
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;
using MS.Internal.Mita.Localization;

namespace MS.Internal.Mita.Foundation {
    internal class LocalizedStrings2 : ILocalizedStrings {
        readonly AutomationElement _contextElement;
        readonly ILocalizationProvider2 _provider;
        bool _shouldSendFeedbackToProvider;
        IStringResourceData[] _translationTable;

        public LocalizedStrings2(AutomationElement contextElement, ILocalizationProvider2 provider) {
            Validate.ArgumentNotNull(parameter: contextElement, parameterName: "element");
            Validate.ArgumentNotNull(parameter: provider, parameterName: nameof(provider));
            this._provider = provider;
            this._contextElement = contextElement;
        }

        public IStringResourceData[] GetTranslations() {
            this._shouldSendFeedbackToProvider = true;
            this._translationTable = this._provider.RetrieveSimilarStrings(nativeText: this._provider.ToString(), contextElement: this._contextElement);
            return this._translationTable;
        }

        public bool TranslationMatchFound(AutomationElement element, long index) => this._shouldSendFeedbackToProvider && this._provider.FoundSimilarMatch(element: element, nativeText: this._provider.ToString(), index: index);
    }
}