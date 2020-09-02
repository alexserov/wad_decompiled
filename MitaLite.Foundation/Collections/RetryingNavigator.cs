// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.RetryingNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Collections
{
  internal class RetryingNavigator : UINavigator
  {
    private UINavigator _containedNavigator;
    private TimeSpan _timeout;
    private int _retryCount;

    public RetryingNavigator(UINavigator navigator, TimeSpan timeout, int retryCount)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) navigator, nameof (navigator));
      this._containedNavigator = !(navigator is RetryingNavigator retryingNavigator) ? navigator.Duplicate() : retryingNavigator._containedNavigator.Duplicate();
      this._timeout = timeout;
      this._retryCount = retryCount;
    }

    public RetryingNavigator(RetryingNavigator previous)
      : base((UINavigator) previous)
    {
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous, nameof (previous));
      MS.Internal.Mita.Foundation.Utilities.Validate.ArgumentNotNull((object) previous._containedNavigator, "previous.containedNavigator");
      this._containedNavigator = previous._containedNavigator.Duplicate();
      this._timeout = previous._timeout;
      this._retryCount = previous._retryCount;
    }

    public override UINavigator Duplicate() => (UINavigator) new RetryingNavigator(this);

    public override IEnumerator<AutomationElement> GetEnumerator()
    {
      IEnumerator<AutomationElement> enumerator = this._containedNavigator.GetEnumerator();
      while (true)
      {
        try
        {
          if (!enumerator.MoveNext())
            break;
        }
        catch (Exception ex)
        {
          break;
        }
        yield return enumerator.Current;
      }
      enumerator = (IEnumerator<AutomationElement>) null;
    }

    public override AutomationElement this[int index]
    {
      get
      {
        int num = 0;
        while (num < this._retryCount)
        {
          IEnumerator<AutomationElement> enumerator = this.GetEnumerator();
          bool flag = true;
          for (int index1 = 0; index1 <= index; ++index1)
          {
            if (!enumerator.MoveNext())
            {
              flag = false;
              Thread.Sleep((int) (this._timeout.TotalMilliseconds / (double) this._retryCount));
              ++num;
              break;
            }
          }
          if (flag)
            return enumerator.Current;
        }
        return (AutomationElement) null;
      }
    }

    public override UIObjectFilter Filter => this._containedNavigator.Filter;

    public UINavigator ContainedNavigator => this._containedNavigator;

    public override string ToString() => StringResource.Get("RetryingNavigator_ToString_1", (object) this._containedNavigator.ToString());
  }
}
