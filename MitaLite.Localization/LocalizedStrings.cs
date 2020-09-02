// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.LocalizedStrings
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Globalization;
using System.Text;

namespace MS.Internal.Mita.Localization
{
  public class LocalizedStrings
  {
    private IStringResourceData[] _translationTable;
    private string _native;
    private string _context;
    private int _processId;
    private CultureInfo _culture;
    private TaggedType _textType;
    private LocalizationProviderProxy _providerProxy;
    private bool _shouldSendFeedbackToProvider;

    public LocalizedStrings(string taggedText)
    {
      string proxyName;
      if (!TaggedTextHelpers.ParseTaggedText(taggedText, out this._native, out proxyName, out this._context, out this._processId, ref this._culture, out this._textType))
        return;
      this._providerProxy = LocalizationManager.GetInstance(proxyName);
    }

    public int ProcessId => this._processId;

    public string Context => this._context;

    public CultureInfo Culture => this._culture;

    public LocalizationProviderProxy ProviderProxy => this._providerProxy;

    public string NativeText => this._native;

    public IStringResourceData[] GetTranslations()
    {
      if (this._translationTable == null)
      {
        switch (this._textType)
        {
          case TaggedType.LocalizableText:
            this._translationTable = this.ProviderProxy.Provider.RetrieveSimilarStrings(this.NativeText, this.Context, this.ProcessId, this.Culture);
            this._shouldSendFeedbackToProvider = true;
            break;
          case TaggedType.ResourceKey:
            IStringResourceData stringResourceData = this.ProviderProxy.LoadExplicit(this.NativeText, this.Context, this.ProcessId, this.Culture);
            this._translationTable = new IStringResourceData[1];
            this._translationTable[0] = stringResourceData;
            break;
          default:
            this._translationTable = new IStringResourceData[1];
            this._translationTable[0] = (IStringResourceData) new StringResourceData(this.NativeText);
            break;
        }
      }
      return this._translationTable;
    }

    public string GetTranslationsDebug()
    {
      IStringResourceData[] translations = this.GetTranslations();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < translations.Length; ++index)
      {
        IStringResourceData stringResourceData = translations[index];
        stringBuilder.AppendFormat("{0}>Raw:{1},Parsed:{2},Debug:{3};", (object) index, (object) stringResourceData.Raw(), (object) stringResourceData.Parsed(), (object) stringResourceData.DebugInfo());
      }
      return stringBuilder.ToString();
    }

    public bool TranslationMatchFound(long index) => this._shouldSendFeedbackToProvider && this.ProviderProxy.Provider.FoundSimilarMatch(this._native, this._context, index);

    public bool Matches(string target)
    {
      bool flag = false;
      IStringResourceData[] translations = this.GetTranslations();
      for (int index = 0; index < translations.Length; ++index)
      {
        if (this.ProviderProxy.ComparisonMethod == null)
        {
          if (translations[index].ToString() == target)
          {
            flag = true;
            this.TranslationMatchFound((long) index);
          }
        }
        else if (this.ProviderProxy.ComparisonMethod(translations[index].ToString(), target))
        {
          flag = true;
          this.TranslationMatchFound((long) index);
        }
      }
      return flag;
    }

    public bool Matches(LocalizedStrings translations)
    {
      Validate.ArgumentNotNull((object) translations, nameof (translations));
      bool flag = false;
      foreach (object translation in translations.GetTranslations())
      {
        if (this.Matches(translation.ToString()))
          flag = true;
      }
      return flag;
    }

    public override string ToString()
    {
      IStringResourceData[] translations = this.GetTranslations();
      StringBuilder stringBuilder = new StringBuilder(this.NativeText, (this.NativeText.Length + 1) * (translations.Length + 1));
      for (int index = 0; index < translations.Length; ++index)
        stringBuilder.AppendFormat(";{0}", (object) translations[index].ToString());
      return stringBuilder.ToString();
    }

    private LocalizedStrings()
    {
    }
  }
}
