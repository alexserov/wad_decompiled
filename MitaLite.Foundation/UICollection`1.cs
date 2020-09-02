// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UICollection`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public class UICollection<I> : UICollection, IList<I>, ICollection<I>, IEnumerable<I>, IEnumerable
        where I : UIObject {
        internal UICollection(UINavigator navigator, IFactory<I> factory) {
            Validate.ArgumentNotNull(parameter: navigator, parameterName: nameof(navigator));
            Validate.ArgumentNotNull(parameter: factory, parameterName: nameof(factory));
            Navigator = !(Timeout != TimeSpan.Zero) ? navigator.Duplicate() : new RetryingNavigator(navigator: navigator, timeout: Timeout, retryCount: RetryCount);
            Factory = factory;
        }

        public UICollection(UICollection<I> previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            Validate.ArgumentNotNull(parameter: previous.Factory, parameterName: "previous.factory");
            Validate.ArgumentNotNull(parameter: previous.Navigator, parameterName: "previous.navigator");
            Navigator = previous.Navigator.Duplicate();
            Factory = previous.Factory;
        }

        public I this[int index] {
            get {
                var i = SafeGetItem(index: index);
                return !(null == i) ? i : throw new ArgumentOutOfRangeException(paramName: nameof(index), actualValue: index, message: "Out of range");
            }
            set { throw new NotSupportedException(message: StringResource.Get(id: "CannotModifyCollection")); }
        }

        public I this[string automationId] {
            get { return Find(automationId: automationId); }
        }

        internal UINavigator Navigator { get; }

        protected IFactory<I> Factory { get; }

        public void Add(I item) {
            throw new NotSupportedException(message: StringResource.Get(id: "CannotModifyCollection"));
        }

        public void Clear() {
            throw new NotSupportedException(message: StringResource.Get(id: "CannotModifyCollection"));
        }

        bool ICollection<I>.Contains(I item) {
            Validate.ArgumentNotNull(parameter: item, parameterName: nameof(item));
            var automationElement = item.AutomationElement;
            foreach (var el2 in Navigator)
                if (Automation.Compare(el1: automationElement, el2: el2))
                    return true;
            return false;
        }

        public bool Remove(I item) {
            throw new NotSupportedException(message: StringResource.Get(id: "CannotModifyCollection"));
        }

        public bool IsReadOnly {
            get { return true; }
        }

        public int Count {
            get {
                var num = 0;
                try {
                    IEnumerator enumerator = Navigator.GetEnumerator();
                    while (enumerator.MoveNext())
                        ++num;
                    return num;
                } catch (COMException ex) {
                    return num;
                }
            }
        }

        public void CopyTo(I[] array, int arrayIndex) {
            Validate.ArgumentNotNull(parameter: array, parameterName: nameof(array));
            foreach (var i in this)
                array[arrayIndex++] = i;
        }

        public void RemoveAt(int index) {
            throw new NotSupportedException(message: StringResource.Get(id: "CannotModifyCollection"));
        }

        public void Insert(int index, I item) {
            throw new NotSupportedException(message: StringResource.Get(id: "CannotModifyCollection"));
        }

        I IList<I>.this[int index] {
            get { return this[index: index]; }
            set { throw new NotSupportedException(message: StringResource.Get(id: "CannotModifyCollection")); }
        }

        public int IndexOf(I item) {
            Validate.ArgumentNotNull(parameter: item, parameterName: nameof(item));
            Validate.ArgumentNotNull(parameter: item.AutomationElement, parameterName: "item.AutomationElement");
            var automationElement = item.AutomationElement;
            var num = -1;
            foreach (var el2 in Navigator) {
                ++num;
                if (Automation.Compare(el1: automationElement, el2: el2))
                    return num;
            }

            throw new UIObjectNotFoundException(searchDomain: ToString(), uiObject: item);
        }

        public IEnumerator<I> GetEnumerator() {
            foreach (var element in Navigator)
                yield return Factory.Create(element: new UIObject(element: element));
        }

        IEnumerator IEnumerable.GetEnumerator() {
            foreach (var element in Navigator)
                yield return Factory.Create(element: new UIObject(element: element));
        }

        public virtual UICollection<I> Duplicate() {
            return new UICollection<I>(previous: this);
        }

        public UICollection<I> ToStaticCollection() {
            return new UICollection<I>(navigator: Navigator.ToStaticNavigator(), factory: Factory);
        }

        public void AddFilter(UIProperty uiProperty, object value) {
            Navigator.AddFilter(condition: UICondition.Create(property: uiProperty, value: value));
        }

        public void AddFilter(UICondition condition) {
            Navigator.AddFilter(condition: condition);
        }

        public void AddFilter(IFilter<UIObject> filter) {
            AddFilter(filter: new FilterConverter(uiObjectFilter: filter));
        }

        internal void AddFilter(IFilter<AutomationElement> filter) {
            Navigator.AddFilter(filter: filter);
        }

        public bool Contains(string automationId) {
            return Contains(condition: UICondition.CreateFromId(automationId: automationId));
        }

        public bool Contains(UIProperty uiProperty, object value) {
            return Contains(condition: UICondition.Create(property: uiProperty, value: value));
        }

        public bool Contains(UICondition condition) {
            return !(null == SafeFind(condition: condition));
        }

        public bool Contains(IFilter<UIObject> filter) {
            return Contains(filter: new FilterConverter(uiObjectFilter: filter));
        }

        internal bool Contains(IFilter<AutomationElement> filter) {
            return !(null == SafeFind(filter: filter));
        }

        public bool TryFind(string automationId, out I element) {
            return TryFind(condition: UICondition.CreateFromId(automationId: automationId), element: out element);
        }

        public bool TryFind(UIProperty uiProperty, object value, out I element) {
            return TryFind(condition: UICondition.Create(property: uiProperty, value: value), element: out element);
        }

        public bool TryFind(UICondition condition, out I element) {
            element = SafeFind(condition: condition);
            return !(null == element);
        }

        public bool TryFind(IFilter<UIObject> filter, out I element) {
            return TryFind(filter: new FilterConverter(uiObjectFilter: filter), element: out element);
        }

        internal bool TryFind(IFilter<AutomationElement> filter, out I element) {
            element = SafeFind(filter: filter);
            return !(null == element);
        }

        public I Find(string automationId) {
            return Find(condition: UICondition.CreateFromId(automationId: automationId));
        }

        public I Find(UIProperty uiProperty, object value) {
            return Find(condition: UICondition.Create(property: uiProperty, value: value));
        }

        public I Find(UICondition condition) {
            I element;
            if (!TryFind(condition: condition, element: out element))
                throw new UIObjectNotFoundException(searchDomain: ToString(), condition: condition);
            return element;
        }

        public I Find(IFilter<UIObject> filter) {
            return Find(filter: new FilterConverter(uiObjectFilter: filter));
        }

        internal I Find(IFilter<AutomationElement> filter) {
            I element;
            if (!TryFind(filter: filter, element: out element))
                throw new UIObjectNotFoundException();
            return element;
        }

        public UICollection<I> FindMultiple(UIProperty uiProperty, object value) {
            return FindMultiple(condition: UICondition.Create(property: uiProperty, value: value));
        }

        public UICollection<I> FindMultiple(UICondition condition) {
            var uiCollection = Duplicate();
            uiCollection.AddFilter(condition: condition);
            return uiCollection;
        }

        public UICollection<I> FindMultiple(IFilter<UIObject> filter) {
            return FindMultiple(filter: new FilterConverter(uiObjectFilter: filter));
        }

        internal UICollection<I> FindMultiple(IFilter<AutomationElement> filter) {
            var uiCollection = Duplicate();
            uiCollection.AddFilter(filter: filter);
            return uiCollection;
        }

        protected I SafeGetItem(int index) {
            try {
                var element = Navigator[index: index];
                return element != (AutomationElement) null ? Factory.Create(element: new UIObject(element: element)) : default;
            } catch (ElementNotAvailableException ex) {
                return default;
            } catch (COMException ex) {
                return default;
            }
        }

        protected I SafeFind(UICondition condition) {
            var uiCollection = Duplicate();
            uiCollection.AddFilter(condition: condition);
            return uiCollection.SafeGetItem(index: 0);
        }

        internal I SafeFind(IFilter<AutomationElement> filter) {
            var uiCollection = Duplicate();
            uiCollection.AddFilter(filter: filter);
            return uiCollection.SafeGetItem(index: 0);
        }

        public override string ToString() {
            return StringResource.Get(id: "UICollection_ToString_2", (object) typeof(I).ToString(), (object) Navigator.ToString());
        }
    }
}