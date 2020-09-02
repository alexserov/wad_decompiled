// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UICondition
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.QueryLanguage;
using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  public class UICondition
  {
    private GlobalizableCondition _condition;
    private Condition _globalizedCondition;
    private Dictionary<AutomationProperty, TranslatedStrings> _propertyValueTranslations;
    private static readonly UICondition _trueCondition = new UICondition(Condition.TrueCondition);
    private static readonly UICondition _falseCondition = new UICondition(Condition.FalseCondition);
    private static readonly UICondition _rawTreeCondition = new UICondition(System.Windows.Automation.Automation.RawViewCondition);
    private static readonly UICondition _controlTreeCondition = new UICondition(System.Windows.Automation.Automation.ControlViewCondition);
    private static readonly UICondition _contentTreeCondition = new UICondition(System.Windows.Automation.Automation.ContentViewCondition);

    internal UICondition(Condition condition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition, nameof (condition));
      this._condition = GlobalizableCondition.Create(condition);
      this._propertyValueTranslations = new Dictionary<AutomationProperty, TranslatedStrings>();
    }

    internal UICondition(GlobalizableCondition condition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition, nameof (condition));
      this._condition = condition;
      this._propertyValueTranslations = new Dictionary<AutomationProperty, TranslatedStrings>();
    }

    public static UICondition Create(UIProperty property, object value)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) property, nameof (property));
      return new UICondition((GlobalizableCondition) new GlobalizablePropertyCondition(property.Property, value));
    }

    public static UICondition Create(string query, params object[] objects) => new UICondition(UIQuery.Parse(query, objects));

    public static UICondition CreateFromId(string automationId) => new UICondition((GlobalizableCondition) new GlobalizablePropertyCondition(AutomationElement.AutomationIdProperty, (object) automationId));

    public static UICondition CreateFromName(string name) => new UICondition((GlobalizableCondition) new GlobalizablePropertyCondition(AutomationElement.NameProperty, (object) name));

    public static UICondition CreateFromClassName(string className) => new UICondition((GlobalizableCondition) new GlobalizablePropertyCondition(AutomationElement.ClassNameProperty, (object) className));

    public UICondition AndWith(string newCondition, params object[] objects) => new UICondition((GlobalizableCondition) new GlobalizableAndCondition(new GlobalizableCondition[2]
    {
      this._condition,
      UIQuery.Parse(newCondition, objects)
    }));

    public UICondition AndWith(UICondition newCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) newCondition, nameof (newCondition));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) newCondition.GlobalizableCondition, "newCondition.GlobalizableCondition");
      return new UICondition((GlobalizableCondition) new GlobalizableAndCondition(new GlobalizableCondition[2]
      {
        this._condition,
        newCondition.GlobalizableCondition
      }));
    }

    public UICondition OrWith(string newCondition, params object[] objects) => new UICondition((GlobalizableCondition) new GlobalizableOrCondition(new GlobalizableCondition[2]
    {
      this._condition,
      UIQuery.Parse(newCondition, objects)
    }));

    public UICondition OrWith(UICondition newCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) newCondition, nameof (newCondition));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) newCondition.GlobalizableCondition, "newCondition.GlobalizableCondition");
      return new UICondition((GlobalizableCondition) new GlobalizableOrCondition(new GlobalizableCondition[2]
      {
        this._condition,
        newCondition.GlobalizableCondition
      }));
    }

    public UICondition Negate() => new UICondition((Condition) new NotCondition(this._condition.Condition));

    public static UICondition operator &(UICondition condition1, UICondition condition2)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition1, nameof (condition1));
      return condition1.AndWith(condition2);
    }

    public static UICondition operator |(UICondition condition1, UICondition condition2)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition1, nameof (condition1));
      return condition1.OrWith(condition2);
    }

    public static UICondition operator !(UICondition condition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) condition, nameof (condition));
      return condition.Negate();
    }

    internal bool Matches(AutomationElement element)
    {
      if (this._condition.Condition == Condition.TrueCondition)
        return true;
      return (AutomationElement) null != this.FindFirst(element, Scope.Element, new CacheRequest()
      {
        TreeFilter = System.Windows.Automation.Automation.RawViewCondition
      });
    }

    internal AutomationElement FindFirst(
      AutomationElement root,
      Scope scope,
      CacheRequest cr)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      if (this._globalizedCondition == null)
        this._globalizedCondition = this._condition.GlobalizeCondition(root, ref this._propertyValueTranslations);
      AutomationElement first = root.FindFirst((TreeScope) scope, this._globalizedCondition, cr);
      this.SendGlobalizerFeedback(first);
      return first;
    }

    internal AutomationElementCollection FindAll(
      AutomationElement root,
      Scope scope,
      CacheRequest cr)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      if (this._globalizedCondition == null)
        this._globalizedCondition = this._condition.GlobalizeCondition(root, ref this._propertyValueTranslations);
      AutomationElementCollection all = root.FindAll((TreeScope) scope, this._globalizedCondition, cr);
      foreach (AutomationElement element in all)
        this.SendGlobalizerFeedback(element);
      return all;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      UICondition.BuildDescription(this._condition.Condition, stringBuilder);
      return stringBuilder.ToString();
    }

    internal static string ToString(Condition condition)
    {
      StringBuilder stringBuilder = new StringBuilder();
      UICondition.BuildDescription(condition, stringBuilder);
      return stringBuilder.ToString();
    }

    private static void BuildDescription(Condition condition, StringBuilder stringBuilder)
    {
      if (condition == Condition.TrueCondition)
        stringBuilder.Append("True");
      else if (condition == Condition.FalseCondition)
      {
        stringBuilder.Append("False");
      }
      else
      {
        switch (condition)
        {
          case PropertyCondition propertyCondition:
            stringBuilder.Append(UIProperty.Get(propertyCondition.Property).ToString());
            stringBuilder.Append(" = ");
            stringBuilder.Append(propertyCondition.Value.ToString());
            break;
          case AndCondition andCondition:
            Condition[] conditions1 = andCondition.GetConditions();
            stringBuilder.Append("(");
            for (int index = 0; index < conditions1.Length; ++index)
            {
              UICondition.BuildDescription(conditions1[index], stringBuilder);
              if (index == conditions1.Length - 1)
                stringBuilder.Append(")");
              else
                stringBuilder.Append(" and ");
            }
            break;
          case OrCondition orCondition:
            Condition[] conditions2 = orCondition.GetConditions();
            stringBuilder.Append("(");
            for (int index = 0; index < conditions2.Length; ++index)
            {
              UICondition.BuildDescription(conditions2[index], stringBuilder);
              if (index == conditions2.Length - 1)
                stringBuilder.Append(")");
              else
                stringBuilder.Append(" or ");
            }
            break;
          case NotCondition notCondition:
            stringBuilder.Append("Not ");
            UICondition.BuildDescription(notCondition.Condition, stringBuilder);
            break;
          default:
            stringBuilder.Append("[Unknown UICondition]");
            break;
        }
      }
    }

    private void SendGlobalizerFeedback(AutomationElement element)
    {
      if (!((AutomationElement) null != element))
        return;
      foreach (KeyValuePair<AutomationProperty, TranslatedStrings> valueTranslation in this._propertyValueTranslations)
        valueTranslation.Value.MatchFound(element, (string) element.GetCurrentPropertyValue(valueTranslation.Key));
    }

    public static bool IsGlobalizableProperty(UIProperty property) => property != null ? UICondition.IsGlobalizableProperty(property.Property) : throw new ArgumentNullException(nameof (property));

    internal static bool IsGlobalizableProperty(AutomationProperty property)
    {
      bool flag = false;
      if (property == AutomationElement.AcceleratorKeyProperty || property == AutomationElement.AccessKeyProperty || (property == AutomationElement.HelpTextProperty || property == AutomationElement.LocalizedControlTypeProperty) || (property == AutomationElement.NameProperty || property == ValuePattern.ValueProperty))
        flag = true;
      return flag;
    }

    public static UICondition True => UICondition._trueCondition;

    public static UICondition False => UICondition._falseCondition;

    public static UICondition RawTree => UICondition._rawTreeCondition;

    public static UICondition ControlTree => UICondition._controlTreeCondition;

    public static UICondition ContentTree => UICondition._contentTreeCondition;

    internal Condition Condition => this._condition.Condition;

    internal GlobalizableCondition GlobalizableCondition => this._condition;
  }
}
