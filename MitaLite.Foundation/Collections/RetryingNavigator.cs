// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Collections.RetryingNavigator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation.Collections {
    internal class RetryingNavigator : UINavigator {
        readonly int _retryCount;
        readonly TimeSpan _timeout;

        public RetryingNavigator(UINavigator navigator, TimeSpan timeout, int retryCount) {
            Validate.ArgumentNotNull(parameter: navigator, parameterName: nameof(navigator));
            ContainedNavigator = !(navigator is RetryingNavigator retryingNavigator) ? navigator.Duplicate() : retryingNavigator.ContainedNavigator.Duplicate();
            this._timeout = timeout;
            this._retryCount = retryCount;
        }

        public RetryingNavigator(RetryingNavigator previous)
            : base(previous: previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            Validate.ArgumentNotNull(parameter: previous.ContainedNavigator, parameterName: "previous.containedNavigator");
            ContainedNavigator = previous.ContainedNavigator.Duplicate();
            this._timeout = previous._timeout;
            this._retryCount = previous._retryCount;
        }

        public override AutomationElement this[int index] {
            get {
                var num = 0;
                while (num < this._retryCount) {
                    var enumerator = GetEnumerator();
                    var flag = true;
                    for (var index1 = 0; index1 <= index; ++index1)
                        if (!enumerator.MoveNext()) {
                            flag = false;
                            Thread.Sleep(millisecondsTimeout: (int) (this._timeout.TotalMilliseconds / this._retryCount));
                            ++num;
                            break;
                        }

                    if (flag)
                        return enumerator.Current;
                }

                return null;
            }
        }

        public override UIObjectFilter Filter {
            get { return ContainedNavigator.Filter; }
        }

        public UINavigator ContainedNavigator { get; }

        public override UINavigator Duplicate() {
            return new RetryingNavigator(previous: this);
        }

        public override IEnumerator<AutomationElement> GetEnumerator() {
            var enumerator = ContainedNavigator.GetEnumerator();
            while (true) {
                try {
                    if (!enumerator.MoveNext())
                        break;
                } catch (Exception ex) {
                    break;
                }

                yield return enumerator.Current;
            }

            enumerator = null;
        }

        public override string ToString() {
            return StringResource.Get(id: "RetryingNavigator_ToString_1", (object) ContainedNavigator.ToString());
        }
    }
}