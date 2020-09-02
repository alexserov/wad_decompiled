// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.LocalizedStringsAdapter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Localization;

namespace MS.Internal.Mita.Foundation {
    internal class LocalizedStringsAdapter : ILocalizedStrings {
        readonly LocalizedStrings _localizedStrings;

        public LocalizedStringsAdapter(string taggedText) {
            this._localizedStrings = new LocalizedStrings(taggedText: taggedText);
        }

        public IStringResourceData[] GetTranslations() {
            return this._localizedStrings.GetTranslations();
        }

        public bool TranslationMatchFound(AutomationElement element, long index) {
            return this._localizedStrings.TranslationMatchFound(index: index);
        }
    }
}