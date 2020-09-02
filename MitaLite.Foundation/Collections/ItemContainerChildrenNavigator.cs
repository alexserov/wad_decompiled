// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.ItemContainerChildrenNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Patterns;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class ItemContainerChildrenNavigator : UINavigator
  {
    private AutomationElement _root;
    private TreeWalker _treeWalker;
    private bool _autoRealize;

    public ItemContainerChildrenNavigator(UIObject root, UICondition treeCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      this.Initialize(root.AutomationElement, treeCondition);
    }

    public ItemContainerChildrenNavigator(AutomationElement root, UICondition treeCondition) => this.Initialize(root, treeCondition);

    public ItemContainerChildrenNavigator(ItemContainerChildrenNavigator previous)
      : base((UINavigator) previous)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous, nameof (previous));
      this._autoRealize = previous._autoRealize;
      this._root = previous._root;
      this._treeWalker = previous._treeWalker;
    }

    private void Initialize(AutomationElement root, UICondition treeCondition)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) root, nameof (root));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) treeCondition, nameof (treeCondition));
      this._autoRealize = UICollection.AutoRealize;
      this._root = root;
      this._treeWalker = new TreeWalker(treeCondition.Condition);
    }

    public override UINavigator Duplicate() => (UINavigator) new ItemContainerChildrenNavigator(this);

    public override IEnumerator<AutomationElement> GetEnumerator()
    {
      ItemContainerChildrenNavigator childrenNavigator = this;
      ItemContainerImplementation icPattern = new ItemContainerImplementation(new UIObject(childrenNavigator._root));
      UIObject current = (UIObject) null;
      bool flag = false;
      if (childrenNavigator.Filter.UICondition.Condition == Condition.TrueCondition)
      {
        for (current = icPattern.FindItemByProperty(current, UIProperty.Get(AutomationElement.SearchVirtualItemsProperty), (object) null); current != (UIObject) null; current = icPattern.FindItemByProperty(current, UIProperty.Get(AutomationElement.SearchVirtualItemsProperty), (object) null))
        {
          if (childrenNavigator._autoRealize)
            ItemContainerChildrenNavigator.Realize(current);
          yield return current.AutomationElement;
        }
      }
      else
      {
        PropertyCondition propertyCondition = childrenNavigator.GetPropertyConditionFromFilter();
        if (propertyCondition == null)
        {
          for (current = icPattern.FindItemByProperty(current, UIProperty.Get(AutomationElement.SearchVirtualItemsProperty), (object) null); current != (UIObject) null; current = icPattern.FindItemByProperty(current, UIProperty.Get(AutomationElement.SearchVirtualItemsProperty), (object) null))
          {
            if (childrenNavigator._autoRealize)
              ItemContainerChildrenNavigator.Realize(current);
            if (childrenNavigator.Filter.Matches(current.AutomationElement))
              yield return current.AutomationElement;
          }
        }
        else
        {
          AutomationProperty property = propertyCondition.Property;
          try
          {
            current = icPattern.FindItemByProperty(current, UIProperty.Get(property), propertyCondition.Value);
          }
          catch (COMException ex)
          {
            if (ex.HResult == -2147467259)
              flag = true;
          }
          if (flag)
          {
            for (current = icPattern.FindItemByProperty(current, UIProperty.Get(AutomationElement.SearchVirtualItemsProperty), (object) null); current != (UIObject) null; current = icPattern.FindItemByProperty(current, UIProperty.Get(AutomationElement.SearchVirtualItemsProperty), (object) null))
            {
              if (childrenNavigator._autoRealize)
                ItemContainerChildrenNavigator.Realize(current);
              if (childrenNavigator.Filter.Matches(current.AutomationElement))
                yield return current.AutomationElement;
            }
          }
          else
          {
            current = (UIObject) null;
            for (current = icPattern.FindItemByProperty(current, UIProperty.Get(property), propertyCondition.Value); current != (UIObject) null; current = icPattern.FindItemByProperty(current, UIProperty.Get(property), propertyCondition.Value))
            {
              if (childrenNavigator._autoRealize)
                ItemContainerChildrenNavigator.Realize(current);
              yield return current.AutomationElement;
            }
          }
          property = (AutomationProperty) null;
        }
        propertyCondition = (PropertyCondition) null;
      }
    }

    public override string ToString() => StringResource.Get("ItemContainerChildrenNavigator_ToString_1", (object) new UIObject(this._root).ToString());

    private PropertyCondition GetPropertyConditionFromFilter()
    {
      if (!(this.Filter.UICondition.Condition is AndCondition condition))
        return (PropertyCondition) null;
      Condition[] conditions = condition.GetConditions();
      return conditions.Length > 2 ? (PropertyCondition) null : conditions[1] as PropertyCondition;
    }

    private static void Realize(UIObject uiObject)
    {
      if (!uiObject.IsVirtualizedItemPatternAvailable)
        return;
      new VirtualizedItemImplementation(uiObject).Realize();
    }
  }
}
