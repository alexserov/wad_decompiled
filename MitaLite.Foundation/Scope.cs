﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Scope
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation {
    public enum Scope {
        Element = 1,
        Children = 2,
        Descendants = 4,
        Subtree = 7,
        Parent = 8,
        Ancestors = 16 // 0x00000010
    }
}