// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.ItemContainerChildrenNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class ItemContainerChildrenNavigator : UINavigator {
        bool _autoRealize;
        AutomationElement _root;
        TreeWalker _treeWalker;

        public ItemContainerChildrenNavigator(UIObject root, UICondition treeCondition) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Initialize(root: root.AutomationElement, treeCondition: treeCondition);
        }

        public ItemContainerChildrenNavigator(AutomationElement root, UICondition treeCondition) {
            Initialize(root: root, treeCondition: treeCondition);
        }

        public ItemContainerChildrenNavigator(ItemContainerChildrenNavigator previous)
            : base(previous: previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            this._autoRealize = previous._autoRealize;
            this._root = previous._root;
            this._treeWalker = previous._treeWalker;
        }

        void Initialize(AutomationElement root, UICondition treeCondition) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Validate.ArgumentNotNull(parameter: treeCondition, parameterName: nameof(treeCondition));
            this._autoRealize = UICollection.AutoRealize;
            this._root = root;
            this._treeWalker = new TreeWalker(condition: treeCondition.Condition);
        }

        public override UINavigator Duplicate() {
            return new ItemContainerChildrenNavigator(previous: this);
        }

        public override IEnumerator<AutomationElement> GetEnumerator() {
            var childrenNavigator = this;
            var icPattern = new ItemContainerImplementation(uiObject: new UIObject(element: childrenNavigator._root));
            UIObject current = null;
            var flag = false;
            if (childrenNavigator.Filter.UICondition.Condition == Condition.TrueCondition) {
                for (current = icPattern.FindItemByProperty(uiObject: current, uiProperty: UIProperty.Get(property: AutomationElement.SearchVirtualItemsProperty), value: null); current != (UIObject) null; current = icPattern.FindItemByProperty(uiObject: current, uiProperty: UIProperty.Get(property: AutomationElement.SearchVirtualItemsProperty), value: null)) {
                    if (childrenNavigator._autoRealize)
                        Realize(uiObject: current);
                    yield return current.AutomationElement;
                }
            } else {
                var propertyCondition = childrenNavigator.GetPropertyConditionFromFilter();
                if (propertyCondition == null) {
                    for (current = icPattern.FindItemByProperty(uiObject: current, uiProperty: UIProperty.Get(property: AutomationElement.SearchVirtualItemsProperty), value: null); current != (UIObject) null; current = icPattern.FindItemByProperty(uiObject: current, uiProperty: UIProperty.Get(property: AutomationElement.SearchVirtualItemsProperty), value: null)) {
                        if (childrenNavigator._autoRealize)
                            Realize(uiObject: current);
                        if (childrenNavigator.Filter.Matches(element: current.AutomationElement))
                            yield return current.AutomationElement;
                    }
                } else {
                    var property = propertyCondition.Property;
                    try {
                        current = icPattern.FindItemByProperty(uiObject: current, uiProperty: UIProperty.Get(property: property), value: propertyCondition.Value);
                    } catch (COMException ex) {
                        if (ex.HResult == -2147467259)
                            flag = true;
                    }

                    if (flag) {
                        for (current = icPattern.FindItemByProperty(uiObject: current, uiProperty: UIProperty.Get(property: AutomationElement.SearchVirtualItemsProperty), value: null); current != (UIObject) null; current = icPattern.FindItemByProperty(uiObject: current, uiProperty: UIProperty.Get(property: AutomationElement.SearchVirtualItemsProperty), value: null)) {
                            if (childrenNavigator._autoRealize)
                                Realize(uiObject: current);
                            if (childrenNavigator.Filter.Matches(element: current.AutomationElement))
                                yield return current.AutomationElement;
                        }
                    } else {
                        current = null;
                        for (current = icPattern.FindItemByProperty(uiObject: current, uiProperty: UIProperty.Get(property: property), value: propertyCondition.Value); current != (UIObject) null; current = icPattern.FindItemByProperty(uiObject: current, uiProperty: UIProperty.Get(property: property), value: propertyCondition.Value)) {
                            if (childrenNavigator._autoRealize)
                                Realize(uiObject: current);
                            yield return current.AutomationElement;
                        }
                    }

                    property = null;
                }

                propertyCondition = null;
            }
        }

        public override string ToString() {
            return StringResource.Get(id: "ItemContainerChildrenNavigator_ToString_1", (object) new UIObject(element: this._root).ToString());
        }

        PropertyCondition GetPropertyConditionFromFilter() {
            if (!(Filter.UICondition.Condition is AndCondition condition))
                return null;
            var conditions = condition.GetConditions();
            return conditions.Length > 2 ? null : conditions[1] as PropertyCondition;
        }

        static void Realize(UIObject uiObject) {
            if (!uiObject.IsVirtualizedItemPatternAvailable)
                return;
            new VirtualizedItemImplementation(uiObject: uiObject).Realize();
        }
    }
}