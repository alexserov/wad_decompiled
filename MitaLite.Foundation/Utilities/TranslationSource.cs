// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.TranslationSource
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Utilities
{
  internal class TranslationSource
  {
    private ILocalizedStrings _localizedStrings;
    private long _translationIndex;

    private TranslationSource()
    {
    }

    public TranslationSource(ILocalizedStrings localizedStrings, long translationIndex)
    {
      this._localizedStrings = localizedStrings;
      this._translationIndex = translationIndex;
    }

    public void MatchFound(AutomationElement element) => this._localizedStrings.TranslationMatchFound(element, this._translationIndex);
  }
}
