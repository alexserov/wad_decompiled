// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UICondition
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.QueryLanguage;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public class UICondition {
        Condition _globalizedCondition;
        Dictionary<AutomationProperty, TranslatedStrings> _propertyValueTranslations;

        internal UICondition(Condition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            GlobalizableCondition = GlobalizableCondition.Create(condition: condition);
            this._propertyValueTranslations = new Dictionary<AutomationProperty, TranslatedStrings>();
        }

        internal UICondition(GlobalizableCondition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            GlobalizableCondition = condition;
            this._propertyValueTranslations = new Dictionary<AutomationProperty, TranslatedStrings>();
        }

        public static UICondition True { get; } = new UICondition(condition: Condition.TrueCondition);

        public static UICondition False { get; } = new UICondition(condition: Condition.FalseCondition);

        public static UICondition RawTree { get; } = new UICondition(condition: Automation.RawViewCondition);

        public static UICondition ControlTree { get; } = new UICondition(condition: Automation.ControlViewCondition);

        public static UICondition ContentTree { get; } = new UICondition(condition: Automation.ContentViewCondition);

        internal Condition Condition {
            get { return GlobalizableCondition.Condition; }
        }

        internal GlobalizableCondition GlobalizableCondition { get; }

        public static UICondition Create(UIProperty property, object value) {
            Validate.ArgumentNotNull(parameter: property, parameterName: nameof(property));
            return new UICondition(condition: new GlobalizablePropertyCondition(property: property.Property, value: value));
        }

        public static UICondition Create(string query, params object[] objects) {
            return new UICondition(condition: UIQuery.Parse(query: query, objects: objects));
        }

        public static UICondition CreateFromId(string automationId) {
            return new UICondition(condition: new GlobalizablePropertyCondition(property: AutomationElement.AutomationIdProperty, value: automationId));
        }

        public static UICondition CreateFromName(string name) {
            return new UICondition(condition: new GlobalizablePropertyCondition(property: AutomationElement.NameProperty, value: name));
        }

        public static UICondition CreateFromClassName(string className) {
            return new UICondition(condition: new GlobalizablePropertyCondition(property: AutomationElement.ClassNameProperty, value: className));
        }

        public UICondition AndWith(string newCondition, params object[] objects) {
            return new UICondition(condition: new GlobalizableAndCondition(GlobalizableCondition, UIQuery.Parse(query: newCondition, objects: objects)));
        }

        public UICondition AndWith(UICondition newCondition) {
            Validate.ArgumentNotNull(parameter: newCondition, parameterName: nameof(newCondition));
            Validate.ArgumentNotNull(parameter: newCondition.GlobalizableCondition, parameterName: "newCondition.GlobalizableCondition");
            return new UICondition(condition: new GlobalizableAndCondition(GlobalizableCondition, newCondition.GlobalizableCondition));
        }

        public UICondition OrWith(string newCondition, params object[] objects) {
            return new UICondition(condition: new GlobalizableOrCondition(GlobalizableCondition, UIQuery.Parse(query: newCondition, objects: objects)));
        }

        public UICondition OrWith(UICondition newCondition) {
            Validate.ArgumentNotNull(parameter: newCondition, parameterName: nameof(newCondition));
            Validate.ArgumentNotNull(parameter: newCondition.GlobalizableCondition, parameterName: "newCondition.GlobalizableCondition");
            return new UICondition(condition: new GlobalizableOrCondition(GlobalizableCondition, newCondition.GlobalizableCondition));
        }

        public UICondition Negate() {
            return new UICondition(condition: new NotCondition(condition: GlobalizableCondition.Condition));
        }

        public static UICondition operator &(UICondition condition1, UICondition condition2) {
            Validate.ArgumentNotNull(parameter: condition1, parameterName: nameof(condition1));
            return condition1.AndWith(newCondition: condition2);
        }

        public static UICondition operator |(UICondition condition1, UICondition condition2) {
            Validate.ArgumentNotNull(parameter: condition1, parameterName: nameof(condition1));
            return condition1.OrWith(newCondition: condition2);
        }

        public static UICondition operator !(UICondition condition) {
            Validate.ArgumentNotNull(parameter: condition, parameterName: nameof(condition));
            return condition.Negate();
        }

        internal bool Matches(AutomationElement element) {
            if (GlobalizableCondition.Condition == Condition.TrueCondition)
                return true;
            return null != FindFirst(root: element, scope: Scope.Element, cr: new CacheRequest {
                TreeFilter = Automation.RawViewCondition
            });
        }

        internal AutomationElement FindFirst(
            AutomationElement root,
            Scope scope,
            CacheRequest cr) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            if (this._globalizedCondition == null)
                this._globalizedCondition = GlobalizableCondition.GlobalizeCondition(element: root, propertyValueTranslations: ref this._propertyValueTranslations);
            var first = root.FindFirst(scope: (TreeScope) scope, condition: this._globalizedCondition, cr: cr);
            SendGlobalizerFeedback(element: first);
            return first;
        }

        internal AutomationElementCollection FindAll(
            AutomationElement root,
            Scope scope,
            CacheRequest cr) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            if (this._globalizedCondition == null)
                this._globalizedCondition = GlobalizableCondition.GlobalizeCondition(element: root, propertyValueTranslations: ref this._propertyValueTranslations);
            var all = root.FindAll(scope: (TreeScope) scope, condition: this._globalizedCondition, cr: cr);
            foreach (AutomationElement element in all)
                SendGlobalizerFeedback(element: element);
            return all;
        }

        public override string ToString() {
            var stringBuilder = new StringBuilder();
            BuildDescription(condition: GlobalizableCondition.Condition, stringBuilder: stringBuilder);
            return stringBuilder.ToString();
        }

        internal static string ToString(Condition condition) {
            var stringBuilder = new StringBuilder();
            BuildDescription(condition: condition, stringBuilder: stringBuilder);
            return stringBuilder.ToString();
        }

        static void BuildDescription(Condition condition, StringBuilder stringBuilder) {
            if (condition == Condition.TrueCondition)
                stringBuilder.Append(value: "True");
            else if (condition == Condition.FalseCondition)
                stringBuilder.Append(value: "False");
            else
                switch (condition) {
                    case PropertyCondition propertyCondition:
                        stringBuilder.Append(value: UIProperty.Get(property: propertyCondition.Property));
                        stringBuilder.Append(value: " = ");
                        stringBuilder.Append(value: propertyCondition.Value);
                        break;
                    case AndCondition andCondition:
                        var conditions1 = andCondition.GetConditions();
                        stringBuilder.Append(value: "(");
                        for (var index = 0; index < conditions1.Length; ++index) {
                            BuildDescription(condition: conditions1[index], stringBuilder: stringBuilder);
                            if (index == conditions1.Length - 1)
                                stringBuilder.Append(value: ")");
                            else
                                stringBuilder.Append(value: " and ");
                        }

                        break;
                    case OrCondition orCondition:
                        var conditions2 = orCondition.GetConditions();
                        stringBuilder.Append(value: "(");
                        for (var index = 0; index < conditions2.Length; ++index) {
                            BuildDescription(condition: conditions2[index], stringBuilder: stringBuilder);
                            if (index == conditions2.Length - 1)
                                stringBuilder.Append(value: ")");
                            else
                                stringBuilder.Append(value: " or ");
                        }

                        break;
                    case NotCondition notCondition:
                        stringBuilder.Append(value: "Not ");
                        BuildDescription(condition: notCondition.Condition, stringBuilder: stringBuilder);
                        break;
                    default:
                        stringBuilder.Append(value: "[Unknown UICondition]");
                        break;
                }
        }

        void SendGlobalizerFeedback(AutomationElement element) {
            if (!(null != element))
                return;
            foreach (var valueTranslation in this._propertyValueTranslations)
                valueTranslation.Value.MatchFound(element: element, translatedString: (string) element.GetCurrentPropertyValue(property: valueTranslation.Key));
        }

        public static bool IsGlobalizableProperty(UIProperty property) {
            return property != null ? IsGlobalizableProperty(property: property.Property) : throw new ArgumentNullException(paramName: nameof(property));
        }

        internal static bool IsGlobalizableProperty(AutomationProperty property) {
            var flag = false;
            if (property == AutomationElement.AcceleratorKeyProperty || property == AutomationElement.AccessKeyProperty || property == AutomationElement.HelpTextProperty || property == AutomationElement.LocalizedControlTypeProperty || property == AutomationElement.NameProperty || property == ValuePattern.ValueProperty)
                flag = true;
            return flag;
        }
    }
}