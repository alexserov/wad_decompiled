// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.BreadthFirstTreeEnumerator`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation.Utilities
{
  internal class BreadthFirstTreeEnumerator<I> : IEnumerator<I>, IEnumerator, IDisposable
  {
    private ITreeNavigator<I> _navigator;
    private Queue<I> _elementQueue;
    private I _root;
    private I _currentElement;
    private TreeEnumerationState _enumerationState;
    private bool _ignoreRoot;
    private bool _disposed;

    public BreadthFirstTreeEnumerator(I root, ITreeNavigator<I> navigator) => this.Initialize(root, navigator, false);

    public BreadthFirstTreeEnumerator(BreadthFirstTreeEnumerator<I> previous)
    {
      Validate.ArgumentNotNull((object) previous, nameof (previous));
      this.Initialize(previous._root, previous._navigator, previous._ignoreRoot);
    }

    public BreadthFirstTreeEnumerator(I root, ITreeNavigator<I> navigator, bool ignoreRoot) => this.Initialize(root, navigator, ignoreRoot);

    private void Initialize(I root, ITreeNavigator<I> navigator, bool ignoreRoot)
    {
      Validate.ArgumentNotNull((object) root, nameof (root));
      Validate.ArgumentNotNull((object) navigator, nameof (navigator));
      this._root = root;
      this._navigator = navigator;
      this._ignoreRoot = ignoreRoot;
      this.Reset();
      this._disposed = false;
    }

    public virtual void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
        this._elementQueue.Clear();
      this._elementQueue = (Queue<I>) null;
      this._disposed = true;
    }

    public bool MoveNext()
    {
      bool flag = true;
      if (this._disposed)
        throw new ObjectDisposedException(nameof (BreadthFirstTreeEnumerator<I>));
      switch (this._enumerationState)
      {
        case TreeEnumerationState.BeforeEnumeration:
          if (!this._ignoreRoot)
          {
            this._currentElement = this._root;
            this._enumerationState = TreeEnumerationState.AtRoot;
          }
          else
          {
            this._currentElement = this._navigator.GetFirstChild(this._root);
            this._enumerationState = TreeEnumerationState.InEnumeration;
          }
          if ((object) this._currentElement == null)
          {
            this._enumerationState = TreeEnumerationState.AfterEnumeration;
            flag = false;
            break;
          }
          I firstChild1 = this._navigator.GetFirstChild(this._currentElement);
          if ((object) firstChild1 != null)
          {
            this._elementQueue.Enqueue(firstChild1);
            break;
          }
          break;
        case TreeEnumerationState.AtRoot:
          this._currentElement = this._elementQueue.Dequeue();
          I firstChild2 = this._navigator.GetFirstChild(this._currentElement);
          if ((object) firstChild2 != null)
            this._elementQueue.Enqueue(firstChild2);
          this._enumerationState = TreeEnumerationState.InEnumeration;
          break;
        case TreeEnumerationState.InEnumeration:
          this._currentElement = this._navigator.GetNextSibling(this._currentElement);
          if ((object) this._currentElement == null)
          {
            if (this._elementQueue.Count == 0)
            {
              this._enumerationState = TreeEnumerationState.AfterEnumeration;
              flag = false;
              break;
            }
            this._currentElement = this._elementQueue.Dequeue();
            I firstChild3 = this._navigator.GetFirstChild(this._currentElement);
            if ((object) firstChild3 != null)
            {
              this._elementQueue.Enqueue(firstChild3);
              break;
            }
            break;
          }
          I firstChild4 = this._navigator.GetFirstChild(this._currentElement);
          if ((object) firstChild4 != null)
          {
            this._elementQueue.Enqueue(firstChild4);
            break;
          }
          break;
        case TreeEnumerationState.AfterEnumeration:
          flag = false;
          break;
      }
      return flag;
    }

    object IEnumerator.Current => (object) this.Current;

    public I Current
    {
      get
      {
        if (this._disposed)
          throw new ObjectDisposedException(nameof (BreadthFirstTreeEnumerator<I>));
        return this._currentElement;
      }
    }

    public void Reset()
    {
      if (this._disposed)
        throw new ObjectDisposedException(nameof (BreadthFirstTreeEnumerator<I>));
      this._elementQueue = new Queue<I>();
      this._currentElement = default (I);
      this._enumerationState = TreeEnumerationState.BeforeEnumeration;
    }

    public I Root => this._root;
  }
}
