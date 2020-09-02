// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.TranslatedStrings
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Utilities {
    internal class TranslatedStrings {
        readonly Dictionary<string, IList<TranslationSource>> _translatedStrings;

        public TranslatedStrings() {
            this._translatedStrings = new Dictionary<string, IList<TranslationSource>>();
        }

        public void Add(string translatedString, ILocalizedStrings localizedStrings, long index) {
            IList<TranslationSource> translationSourceList;
            if (this._translatedStrings.ContainsKey(key: translatedString)) {
                translationSourceList = this._translatedStrings[key: translatedString];
            } else {
                translationSourceList = new List<TranslationSource>();
                this._translatedStrings[key: translatedString] = translationSourceList;
            }

            translationSourceList.Add(item: new TranslationSource(localizedStrings: localizedStrings, translationIndex: index));
        }

        public void MatchFound(AutomationElement element, string translatedString) {
            if (!this._translatedStrings.ContainsKey(key: translatedString))
                return;
            foreach (var translationSource in this._translatedStrings[key: translatedString])
                translationSource.MatchFound(element: element);
        }
    }
}