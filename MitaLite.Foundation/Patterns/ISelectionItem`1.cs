// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ISelectionItem`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Waiters;

namespace MS.Internal.Mita.Foundation.Patterns {
    public interface ISelectionItem<C> where C : UIObject {
        bool IsSelected { get; }

        C SelectionContainer { get; }
        void Select();

        UIEventWaiter GetSelectedWaiter();

        void AddToSelection();

        UIEventWaiter GetAddedToSelectionWaiter();

        void RemoveFromSelection();

        UIEventWaiter GetRemovedFromSelectionWaiter();
    }
}