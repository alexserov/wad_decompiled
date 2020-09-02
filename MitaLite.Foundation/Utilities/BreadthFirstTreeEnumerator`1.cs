// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.BreadthFirstTreeEnumerator`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation.Utilities {
    internal class BreadthFirstTreeEnumerator<I> : IEnumerator<I>, IEnumerator, IDisposable {
        I _currentElement;
        bool _disposed;
        Queue<I> _elementQueue;
        TreeEnumerationState _enumerationState;
        bool _ignoreRoot;
        ITreeNavigator<I> _navigator;

        public BreadthFirstTreeEnumerator(I root, ITreeNavigator<I> navigator) {
            Initialize(root: root, navigator: navigator, ignoreRoot: false);
        }

        public BreadthFirstTreeEnumerator(BreadthFirstTreeEnumerator<I> previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            Initialize(root: previous.Root, navigator: previous._navigator, ignoreRoot: previous._ignoreRoot);
        }

        public BreadthFirstTreeEnumerator(I root, ITreeNavigator<I> navigator, bool ignoreRoot) {
            Initialize(root: root, navigator: navigator, ignoreRoot: ignoreRoot);
        }

        public I Root { get; set; }

        public virtual void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        public bool MoveNext() {
            var flag = true;
            if (this._disposed)
                throw new ObjectDisposedException(objectName: nameof(BreadthFirstTreeEnumerator<I>));
            switch (this._enumerationState) {
                case TreeEnumerationState.BeforeEnumeration:
                    if (!this._ignoreRoot) {
                        this._currentElement = Root;
                        this._enumerationState = TreeEnumerationState.AtRoot;
                    } else {
                        this._currentElement = this._navigator.GetFirstChild(current: Root);
                        this._enumerationState = TreeEnumerationState.InEnumeration;
                    }

                    if (this._currentElement == null) {
                        this._enumerationState = TreeEnumerationState.AfterEnumeration;
                        flag = false;
                        break;
                    }

                    var firstChild1 = this._navigator.GetFirstChild(current: this._currentElement);
                    if (firstChild1 != null) {
                        this._elementQueue.Enqueue(item: firstChild1);
                    }

                    break;
                case TreeEnumerationState.AtRoot:
                    this._currentElement = this._elementQueue.Dequeue();
                    var firstChild2 = this._navigator.GetFirstChild(current: this._currentElement);
                    if (firstChild2 != null)
                        this._elementQueue.Enqueue(item: firstChild2);
                    this._enumerationState = TreeEnumerationState.InEnumeration;
                    break;
                case TreeEnumerationState.InEnumeration:
                    this._currentElement = this._navigator.GetNextSibling(current: this._currentElement);
                    if (this._currentElement == null) {
                        if (this._elementQueue.Count == 0) {
                            this._enumerationState = TreeEnumerationState.AfterEnumeration;
                            flag = false;
                            break;
                        }

                        this._currentElement = this._elementQueue.Dequeue();
                        var firstChild3 = this._navigator.GetFirstChild(current: this._currentElement);
                        if (firstChild3 != null) this._elementQueue.Enqueue(item: firstChild3);
                        break;
                    }

                    var firstChild4 = this._navigator.GetFirstChild(current: this._currentElement);
                    if (firstChild4 != null) this._elementQueue.Enqueue(item: firstChild4);
                    break;
                case TreeEnumerationState.AfterEnumeration:
                    flag = false;
                    break;
            }

            return flag;
        }

        object IEnumerator.Current {
            get { return Current; }
        }

        public I Current {
            get {
                if (this._disposed)
                    throw new ObjectDisposedException(objectName: nameof(BreadthFirstTreeEnumerator<I>));
                return this._currentElement;
            }
        }

        public void Reset() {
            if (this._disposed)
                throw new ObjectDisposedException(objectName: nameof(BreadthFirstTreeEnumerator<I>));
            this._elementQueue = new Queue<I>();
            this._currentElement = default;
            this._enumerationState = TreeEnumerationState.BeforeEnumeration;
        }

        void Initialize(I root, ITreeNavigator<I> navigator, bool ignoreRoot) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Validate.ArgumentNotNull(parameter: navigator, parameterName: nameof(navigator));
            Root = root;
            this._navigator = navigator;
            this._ignoreRoot = ignoreRoot;
            Reset();
            this._disposed = false;
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing)
                this._elementQueue.Clear();
            this._elementQueue = null;
            this._disposed = true;
        }
    }
}