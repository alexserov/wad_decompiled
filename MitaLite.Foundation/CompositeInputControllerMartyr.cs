// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.CompositeInputControllerMartyr
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    public class CompositeInputControllerMartyr : IDisposable {
        bool _disposed;
        readonly Stack<IDisposable> _inputControllerMartyrStack;
        readonly PointerInputType _previousInputType;

        public CompositeInputControllerMartyr()
            : this(previousInputType: PointerInputType.Mouse) {
        }

        public CompositeInputControllerMartyr(PointerInputType previousInputType) {
            this._inputControllerMartyrStack = new Stack<IDisposable>();
            this._previousInputType = previousInputType;
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        public void Add(IDisposable martyr) {
            this._inputControllerMartyrStack.Push(item: martyr);
        }

        protected virtual void Dispose(bool disposing) {
            try {
                if (this._disposed || !disposing)
                    return;
                lock (this._inputControllerMartyrStack) {
                    while (this._inputControllerMartyrStack.Count > 0)
                        this._inputControllerMartyrStack.Pop().Dispose();
                }

                InputController.ActiveInputType = this._previousInputType;
            } finally {
                this._disposed = true;
            }
        }
    }
}