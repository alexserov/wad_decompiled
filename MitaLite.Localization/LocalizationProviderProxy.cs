// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.LocalizationProviderProxy
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Globalization;

namespace MS.Internal.Mita.Localization
{
  public class LocalizationProviderProxy
  {
    private string _registeredName;
    private ILocalizationProvider _providerInstance;
    private int _defaultProcessId;
    private string _defaultContext;
    private CultureInfo _defaultCulture;
    private LocalizationProviderProxy.MatchesComparator _comparisonMethod;

    public LocalizationProviderProxy(ILocalizationProvider provider, string proxyName)
    {
      Validate.ArgumentNotNull((object) provider, nameof (provider));
      Validate.ArgumentNotNull((object) proxyName, nameof (proxyName));
      this._providerInstance = provider;
      this._registeredName = proxyName;
      this.DefaultCulture = CultureInfo.InvariantCulture;
      LocalizationManager.RegisterProvider(this);
    }

    public ILocalizationProvider Provider => this._providerInstance;

    public int DefaultProcessId
    {
      get => this._defaultProcessId;
      set => this._defaultProcessId = value;
    }

    public string DefaultContext
    {
      get => this._defaultContext;
      set
      {
        Validate.ArgumentNotNull((object) value, nameof (value));
        this._defaultContext = value;
      }
    }

    public CultureInfo DefaultCulture
    {
      get => this._defaultCulture;
      set
      {
        Validate.ArgumentNotNull((object) value, nameof (value));
        this._defaultCulture = value;
      }
    }

    public string RegisteredName => this._registeredName;

    public IStringResourceData LoadExplicit(string resourceKey) => this.LoadExplicit(resourceKey, this.DefaultContext, this.DefaultProcessId, this.DefaultCulture);

    public IStringResourceData LoadExplicit(
      string resourceKey,
      string context,
      int processId,
      CultureInfo culture) => this.Provider.LoadExplicit(resourceKey, context, processId, culture);

    public string TagAsLocalized(string nativeText) => this.TagAsLocalized(nativeText, this.DefaultContext, this.DefaultProcessId, this.DefaultCulture);

    public string TagAsLocalized(string nativeText, string context) => this.TagAsLocalized(nativeText, context, this.DefaultProcessId, this.DefaultCulture);

    public string TagAsLocalized(
      string nativeText,
      string context,
      int processId,
      CultureInfo culture) => TaggedTextHelpers.CreateTaggedText(TaggedType.LocalizableText, nativeText, context, processId, culture, this.RegisteredName);

    public string TagAsFixed(string fixedText) => TaggedTextHelpers.CreateTaggedText(TaggedType.FixedText, fixedText, this.DefaultContext, this.DefaultProcessId, this.DefaultCulture, this.RegisteredName);

    public string TagAsResourceKey(string resourceKey) => this.TagAsResourceKey(resourceKey, this.DefaultContext, this.DefaultProcessId, this.DefaultCulture);

    public string TagAsResourceKey(
      string resourceKey,
      string context,
      int processId,
      CultureInfo culture) => TaggedTextHelpers.CreateTaggedText(TaggedType.ResourceKey, resourceKey, context, processId, culture, this.RegisteredName);

    public static bool ParseTaggedText(
      string tagged,
      out string native,
      out string proxyName,
      out string context,
      out int processId,
      ref CultureInfo culture,
      out TaggedType textType) => TaggedTextHelpers.ParseTaggedText(tagged, out native, out proxyName, out context, out processId, ref culture, out textType);

    public LocalizationProviderProxy.MatchesComparator ComparisonMethod
    {
      get => this._comparisonMethod;
      set
      {
        Validate.ArgumentNotNull((object) value, nameof (value));
        this._comparisonMethod = value;
      }
    }

    private LocalizationProviderProxy()
    {
    }

    public delegate bool MatchesComparator(string key, string target);
  }
}
