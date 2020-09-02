// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.LocalizedStringsAdapter
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Localization;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  internal class LocalizedStringsAdapter : ILocalizedStrings
  {
    private LocalizedStrings _localizedStrings;

    public LocalizedStringsAdapter(string taggedText) => this._localizedStrings = new LocalizedStrings(taggedText);

    public IStringResourceData[] GetTranslations() => this._localizedStrings.GetTranslations();

    public bool TranslationMatchFound(AutomationElement element, long index) => this._localizedStrings.TranslationMatchFound(index);
  }
}
