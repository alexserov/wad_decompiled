// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.LocalizedStrings
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Globalization;
using System.Text;

namespace MS.Internal.Mita.Localization {
    public class LocalizedStrings {
        readonly string _context;
        readonly CultureInfo _culture;
        readonly string _native;
        readonly int _processId;
        bool _shouldSendFeedbackToProvider;
        readonly TaggedType _textType;
        IStringResourceData[] _translationTable;

        public LocalizedStrings(string taggedText) {
            string proxyName;
            if (!TaggedTextHelpers.ParseTaggedText(tagged: taggedText, native: out this._native, proxyName: out proxyName, context: out this._context, processId: out this._processId, culture: ref this._culture, textType: out this._textType))
                return;
            ProviderProxy = LocalizationManager.GetInstance(proxyName: proxyName);
        }

        LocalizedStrings() {
        }

        public int ProcessId {
            get { return this._processId; }
        }

        public string Context {
            get { return this._context; }
        }

        public CultureInfo Culture {
            get { return this._culture; }
        }

        public LocalizationProviderProxy ProviderProxy { get; }

        public string NativeText {
            get { return this._native; }
        }

        public IStringResourceData[] GetTranslations() {
            if (this._translationTable == null)
                switch (this._textType) {
                    case TaggedType.LocalizableText:
                        this._translationTable = ProviderProxy.Provider.RetrieveSimilarStrings(nativeText: NativeText, context: Context, processId: ProcessId, culture: Culture);
                        this._shouldSendFeedbackToProvider = true;
                        break;
                    case TaggedType.ResourceKey:
                        var stringResourceData = ProviderProxy.LoadExplicit(resourceKey: NativeText, context: Context, processId: ProcessId, culture: Culture);
                        this._translationTable = new IStringResourceData[1];
                        this._translationTable[0] = stringResourceData;
                        break;
                    default:
                        this._translationTable = new IStringResourceData[1];
                        this._translationTable[0] = new StringResourceData(rawText: NativeText);
                        break;
                }

            return this._translationTable;
        }

        public string GetTranslationsDebug() {
            var translations = GetTranslations();
            var stringBuilder = new StringBuilder();
            for (var index = 0; index < translations.Length; ++index) {
                var stringResourceData = translations[index];
                stringBuilder.AppendFormat(format: "{0}>Raw:{1},Parsed:{2},Debug:{3};", (object) index, (object) stringResourceData.Raw(), (object) stringResourceData.Parsed(), (object) stringResourceData.DebugInfo());
            }

            return stringBuilder.ToString();
        }

        public bool TranslationMatchFound(long index) {
            return this._shouldSendFeedbackToProvider && ProviderProxy.Provider.FoundSimilarMatch(nativeText: this._native, context: this._context, index: index);
        }

        public bool Matches(string target) {
            var flag = false;
            var translations = GetTranslations();
            for (var index = 0; index < translations.Length; ++index)
                if (ProviderProxy.ComparisonMethod == null) {
                    if (translations[index].ToString() == target) {
                        flag = true;
                        TranslationMatchFound(index: index);
                    }
                } else if (ProviderProxy.ComparisonMethod(key: translations[index].ToString(), target: target)) {
                    flag = true;
                    TranslationMatchFound(index: index);
                }

            return flag;
        }

        public bool Matches(LocalizedStrings translations) {
            Validate.ArgumentNotNull(parameter: translations, parameterName: nameof(translations));
            var flag = false;
            foreach (object translation in translations.GetTranslations())
                if (Matches(target: translation.ToString()))
                    flag = true;
            return flag;
        }

        public override string ToString() {
            var translations = GetTranslations();
            var stringBuilder = new StringBuilder(value: NativeText, capacity: (NativeText.Length + 1) * (translations.Length + 1));
            for (var index = 0; index < translations.Length; ++index)
                stringBuilder.AppendFormat(format: ";{0}", arg0: translations[index]);
            return stringBuilder.ToString();
        }
    }
}