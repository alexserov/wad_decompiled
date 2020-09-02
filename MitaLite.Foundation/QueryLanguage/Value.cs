// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.Value
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Text;
using MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib;

namespace MS.Internal.Mita.Foundation.QueryLanguage {
    internal abstract class Value : SSYaccStackElement {
        public abstract bool Validate(Type requiredType, StringBuilder errors);

        public abstract object GetValueObject(Type requiredType);
    }
}