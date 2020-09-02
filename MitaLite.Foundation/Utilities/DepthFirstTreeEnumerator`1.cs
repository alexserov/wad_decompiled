// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.DepthFirstTreeEnumerator`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation.Utilities {
    internal class DepthFirstTreeEnumerator<I> : IEnumerator<I>, IEnumerator, IDisposable {
        I _currentElement;
        bool _disposed;
        Stack<I> _elementStack;
        TreeEnumerationState _enumerationState;
        bool _ignoreRoot;
        ITreeNavigator<I> _navigator;

        public DepthFirstTreeEnumerator(I root, ITreeNavigator<I> navigator) {
            Initialize(root: root, navigator: navigator, ignoreRoot: false);
        }

        public DepthFirstTreeEnumerator(DepthFirstTreeEnumerator<I> previous) {
            Validate.ArgumentNotNull(parameter: previous, parameterName: nameof(previous));
            Initialize(root: previous.Root, navigator: previous._navigator, ignoreRoot: previous._ignoreRoot);
        }

        public DepthFirstTreeEnumerator(I root, ITreeNavigator<I> navigator, bool ignoreRoot) {
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
                throw new ObjectDisposedException(objectName: nameof(DepthFirstTreeEnumerator<I>));
            switch (this._enumerationState) {
                case TreeEnumerationState.BeforeEnumeration:
                    if (!this._ignoreRoot) {
                        this._currentElement = this.Root;
                    } else {
                        this._currentElement = this._navigator.GetFirstChild(current: this.Root);
                        if (this._currentElement == null) {
                            this._enumerationState = TreeEnumerationState.AfterEnumeration;
                            flag = false;
                            break;
                        }

                        var nextSibling = this._navigator.GetNextSibling(current: this._currentElement);
                        if (nextSibling != null)
                            this._elementStack.Push(item: nextSibling);
                    }

                    var firstChild1 = this._navigator.GetFirstChild(current: this._currentElement);
                    if (firstChild1 != null)
                        this._elementStack.Push(item: firstChild1);
                    this._enumerationState = TreeEnumerationState.InEnumeration;
                    break;
                case TreeEnumerationState.InEnumeration:
                    if (this._elementStack.Count == 0) {
                        this._currentElement = default;
                        this._enumerationState = TreeEnumerationState.AfterEnumeration;
                        flag = false;
                        break;
                    }

                    this._currentElement = this._elementStack.Pop();
                    var nextSibling1 = this._navigator.GetNextSibling(current: this._currentElement);
                    if (nextSibling1 != null)
                        this._elementStack.Push(item: nextSibling1);
                    var firstChild2 = this._navigator.GetFirstChild(current: this._currentElement);
                    if (firstChild2 != null) {
                        this._elementStack.Push(item: firstChild2);
                    }

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
                    throw new ObjectDisposedException(objectName: nameof(DepthFirstTreeEnumerator<I>));
                return this._currentElement;
            }
        }

        public void Reset() {
            if (this._disposed)
                throw new ObjectDisposedException(objectName: nameof(DepthFirstTreeEnumerator<I>));
            this._elementStack = new Stack<I>();
            this._currentElement = default;
            this._enumerationState = TreeEnumerationState.BeforeEnumeration;
        }

        void Initialize(I root, ITreeNavigator<I> navigator, bool ignoreRoot) {
            Validate.ArgumentNotNull(parameter: root, parameterName: nameof(root));
            Validate.ArgumentNotNull(parameter: navigator, parameterName: nameof(navigator));
            this.Root = root;
            this._navigator = navigator;
            this._ignoreRoot = ignoreRoot;
            Reset();
            this._disposed = false;
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing)
                this._elementStack.Clear();
            this._elementStack = null;
            this._disposed = true;
        }
    }
}