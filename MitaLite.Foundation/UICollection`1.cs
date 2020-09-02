// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.UICollection`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation
{
  public class UICollection<I> : UICollection, IList<I>, ICollection<I>, IEnumerable<I>, IEnumerable
    where I : UIObject
  {
    private UINavigator _navigator;
    private IFactory<I> _factory;

    internal UICollection(UINavigator navigator, IFactory<I> factory)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) navigator, nameof (navigator));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) factory, nameof (factory));
      this._navigator = !(UICollection.Timeout != TimeSpan.Zero) ? navigator.Duplicate() : (UINavigator) new RetryingNavigator(navigator, UICollection.Timeout, UICollection.RetryCount);
      this._factory = factory;
    }

    public UICollection(UICollection<I> previous)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous, nameof (previous));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous._factory, "previous.factory");
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous._navigator, "previous.navigator");
      this._navigator = previous._navigator.Duplicate();
      this._factory = previous._factory;
    }

    public virtual UICollection<I> Duplicate() => new UICollection<I>(this);

    public void Add(I item) => throw new NotSupportedException(StringResource.Get("CannotModifyCollection"));

    public void Clear() => throw new NotSupportedException(StringResource.Get("CannotModifyCollection"));

    bool ICollection<I>.Contains(I item)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) item, nameof (item));
      AutomationElement automationElement = item.AutomationElement;
      foreach (AutomationElement el2 in this.Navigator)
      {
        if (System.Windows.Automation.Automation.Compare(automationElement, el2))
          return true;
      }
      return false;
    }

    public bool Remove(I item) => throw new NotSupportedException(StringResource.Get("CannotModifyCollection"));

    public bool IsReadOnly => true;

    public int Count
    {
      get
      {
        int num = 0;
        try
        {
          IEnumerator enumerator = (IEnumerator) this.Navigator.GetEnumerator();
          while (enumerator.MoveNext())
            ++num;
          return num;
        }
        catch (COMException ex)
        {
          return num;
        }
      }
    }

    public void CopyTo(I[] array, int arrayIndex)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) array, nameof (array));
      foreach (I i in this)
        array[arrayIndex++] = i;
    }

    public void RemoveAt(int index) => throw new NotSupportedException(StringResource.Get("CannotModifyCollection"));

    public void Insert(int index, I item) => throw new NotSupportedException(StringResource.Get("CannotModifyCollection"));

    I IList<I>.this[int index]
    {
      get => this[index];
      set => throw new NotSupportedException(StringResource.Get("CannotModifyCollection"));
    }

    public int IndexOf(I item)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) item, nameof (item));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) item.AutomationElement, "item.AutomationElement");
      AutomationElement automationElement = item.AutomationElement;
      int num = -1;
      foreach (AutomationElement el2 in this.Navigator)
      {
        ++num;
        if (System.Windows.Automation.Automation.Compare(automationElement, el2))
          return num;
      }
      throw new UIObjectNotFoundException(this.ToString(), (UIObject) item);
    }

    public IEnumerator<I> GetEnumerator()
    {
      foreach (AutomationElement element in this._navigator)
        yield return this._factory.Create(new UIObject(element));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      foreach (AutomationElement element in this._navigator)
        yield return (object) this._factory.Create(new UIObject(element));
    }

    public UICollection<I> ToStaticCollection() => new UICollection<I>(this.Navigator.ToStaticNavigator(), this._factory);

    public void AddFilter(UIProperty uiProperty, object value) => this._navigator.AddFilter(UICondition.Create(uiProperty, value));

    public void AddFilter(UICondition condition) => this._navigator.AddFilter(condition);

    public void AddFilter(IFilter<UIObject> filter) => this.AddFilter((IFilter<AutomationElement>) new FilterConverter(filter));

    internal void AddFilter(IFilter<AutomationElement> filter) => this._navigator.AddFilter(filter);

    public I this[int index]
    {
      get
      {
        I i = this.SafeGetItem(index);
        return !((UIObject) null == (UIObject) i) ? i : throw new ArgumentOutOfRangeException(nameof (index), (object) index, "Out of range");
      }
      set => throw new NotSupportedException(StringResource.Get("CannotModifyCollection"));
    }

    public I this[string automationId] => this.Find(automationId);

    public bool Contains(string automationId) => this.Contains(UICondition.CreateFromId(automationId));

    public bool Contains(UIProperty uiProperty, object value) => this.Contains(UICondition.Create(uiProperty, value));

    public bool Contains(UICondition condition) => !((UIObject) null == (UIObject) this.SafeFind(condition));

    public bool Contains(IFilter<UIObject> filter) => this.Contains((IFilter<AutomationElement>) new FilterConverter(filter));

    internal bool Contains(IFilter<AutomationElement> filter) => !((UIObject) null == (UIObject) this.SafeFind(filter));

    public bool TryFind(string automationId, out I element) => this.TryFind(UICondition.CreateFromId(automationId), out element);

    public bool TryFind(UIProperty uiProperty, object value, out I element) => this.TryFind(UICondition.Create(uiProperty, value), out element);

    public bool TryFind(UICondition condition, out I element)
    {
      element = this.SafeFind(condition);
      return !((UIObject) null == (UIObject) element);
    }

    public bool TryFind(IFilter<UIObject> filter, out I element) => this.TryFind((IFilter<AutomationElement>) new FilterConverter(filter), out element);

    internal bool TryFind(IFilter<AutomationElement> filter, out I element)
    {
      element = this.SafeFind(filter);
      return !((UIObject) null == (UIObject) element);
    }

    public I Find(string automationId) => this.Find(UICondition.CreateFromId(automationId));

    public I Find(UIProperty uiProperty, object value) => this.Find(UICondition.Create(uiProperty, value));

    public I Find(UICondition condition)
    {
      I element;
      if (!this.TryFind(condition, out element))
        throw new UIObjectNotFoundException(this.ToString(), condition);
      return element;
    }

    public I Find(IFilter<UIObject> filter) => this.Find((IFilter<AutomationElement>) new FilterConverter(filter));

    internal I Find(IFilter<AutomationElement> filter)
    {
      I element;
      if (!this.TryFind(filter, out element))
        throw new UIObjectNotFoundException();
      return element;
    }

    public UICollection<I> FindMultiple(UIProperty uiProperty, object value) => this.FindMultiple(UICondition.Create(uiProperty, value));

    public UICollection<I> FindMultiple(UICondition condition)
    {
      UICollection<I> uiCollection = this.Duplicate();
      uiCollection.AddFilter(condition);
      return uiCollection;
    }

    public UICollection<I> FindMultiple(IFilter<UIObject> filter) => this.FindMultiple((IFilter<AutomationElement>) new FilterConverter(filter));

    internal UICollection<I> FindMultiple(IFilter<AutomationElement> filter)
    {
      UICollection<I> uiCollection = this.Duplicate();
      uiCollection.AddFilter(filter);
      return uiCollection;
    }

    protected I SafeGetItem(int index)
    {
      try
      {
        AutomationElement element = this._navigator[index];
        return element != (AutomationElement) null ? this._factory.Create(new UIObject(element)) : default (I);
      }
      catch (ElementNotAvailableException ex)
      {
        return default (I);
      }
      catch (COMException ex)
      {
        return default (I);
      }
    }

    protected I SafeFind(UICondition condition)
    {
      UICollection<I> uiCollection = this.Duplicate();
      uiCollection.AddFilter(condition);
      return uiCollection.SafeGetItem(0);
    }

    internal I SafeFind(IFilter<AutomationElement> filter)
    {
      UICollection<I> uiCollection = this.Duplicate();
      uiCollection.AddFilter(filter);
      return uiCollection.SafeGetItem(0);
    }

    public override string ToString() => StringResource.Get("UICollection_ToString_2", (object) typeof (I).ToString(), (object) this._navigator.ToString());

    internal UINavigator Navigator => this._navigator;

    protected IFactory<I> Factory => this._factory;
  }
}
