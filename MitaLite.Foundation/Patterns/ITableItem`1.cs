﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Patterns.ITableItem`1
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.Patterns
{
  public interface ITableItem<C> : IGridItem<C> where C : UIObject
  {
    UICollection<UIObject> RowHeaderItems { get; }

    UICollection<UIObject> ColumnHeaderItems { get; }
  }
}