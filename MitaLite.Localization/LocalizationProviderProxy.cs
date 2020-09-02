// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.LocalizationProviderProxy
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System.Globalization;

namespace MS.Internal.Mita.Localization {
    public class LocalizationProviderProxy {
        public delegate bool MatchesComparator(string key, string target);

        MatchesComparator _comparisonMethod;
        string _defaultContext;
        CultureInfo _defaultCulture;

        public LocalizationProviderProxy(ILocalizationProvider provider, string proxyName) {
            Validate.ArgumentNotNull(parameter: provider, parameterName: nameof(provider));
            Validate.ArgumentNotNull(parameter: proxyName, parameterName: nameof(proxyName));
            Provider = provider;
            RegisteredName = proxyName;
            DefaultCulture = CultureInfo.InvariantCulture;
            LocalizationManager.RegisterProvider(instance: this);
        }

        LocalizationProviderProxy() {
        }

        public ILocalizationProvider Provider { get; }

        public int DefaultProcessId { get; set; }

        public string DefaultContext {
            get { return this._defaultContext; }
            set {
                Validate.ArgumentNotNull(parameter: value, parameterName: nameof(value));
                this._defaultContext = value;
            }
        }

        public CultureInfo DefaultCulture {
            get { return this._defaultCulture; }
            set {
                Validate.ArgumentNotNull(parameter: value, parameterName: nameof(value));
                this._defaultCulture = value;
            }
        }

        public string RegisteredName { get; }

        public MatchesComparator ComparisonMethod {
            get { return this._comparisonMethod; }
            set {
                Validate.ArgumentNotNull(parameter: value, parameterName: nameof(value));
                this._comparisonMethod = value;
            }
        }

        public IStringResourceData LoadExplicit(string resourceKey) {
            return LoadExplicit(resourceKey: resourceKey, context: DefaultContext, processId: DefaultProcessId, culture: DefaultCulture);
        }

        public IStringResourceData LoadExplicit(
            string resourceKey,
            string context,
            int processId,
            CultureInfo culture) {
            return Provider.LoadExplicit(resourceKey: resourceKey, context: context, processId: processId, culture: culture);
        }

        public string TagAsLocalized(string nativeText) {
            return TagAsLocalized(nativeText: nativeText, context: DefaultContext, processId: DefaultProcessId, culture: DefaultCulture);
        }

        public string TagAsLocalized(string nativeText, string context) {
            return TagAsLocalized(nativeText: nativeText, context: context, processId: DefaultProcessId, culture: DefaultCulture);
        }

        public string TagAsLocalized(
            string nativeText,
            string context,
            int processId,
            CultureInfo culture) {
            return TaggedTextHelpers.CreateTaggedText(textType: TaggedType.LocalizableText, nativeText: nativeText, context: context, processId: processId, culture: culture, proxyName: RegisteredName);
        }

        public string TagAsFixed(string fixedText) {
            return TaggedTextHelpers.CreateTaggedText(textType: TaggedType.FixedText, nativeText: fixedText, context: DefaultContext, processId: DefaultProcessId, culture: DefaultCulture, proxyName: RegisteredName);
        }

        public string TagAsResourceKey(string resourceKey) {
            return TagAsResourceKey(resourceKey: resourceKey, context: DefaultContext, processId: DefaultProcessId, culture: DefaultCulture);
        }

        public string TagAsResourceKey(
            string resourceKey,
            string context,
            int processId,
            CultureInfo culture) {
            return TaggedTextHelpers.CreateTaggedText(textType: TaggedType.ResourceKey, nativeText: resourceKey, context: context, processId: processId, culture: culture, proxyName: RegisteredName);
        }

        public static bool ParseTaggedText(
            string tagged,
            out string native,
            out string proxyName,
            out string context,
            out int processId,
            ref CultureInfo culture,
            out TaggedType textType) {
            return TaggedTextHelpers.ParseTaggedText(tagged: tagged, native: out native, proxyName: out proxyName, context: out context, processId: out processId, culture: ref culture, textType: out textType);
        }
    }
}