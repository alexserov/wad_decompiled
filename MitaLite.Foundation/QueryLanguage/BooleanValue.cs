// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.BooleanValue
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage
{
  internal class BooleanValue : Value
  {
    private bool _booleanValue;

    public BooleanValue(bool value) => this._booleanValue = value;

    public override bool Validate(Type requiredType, StringBuilder errors)
    {
      if (requiredType.Equals(typeof (object)) || requiredType.Equals(typeof (bool)))
        return true;
      errors.AppendLine(StringResource.Get("ParameterTypeMismatch_2", (object) requiredType.FullName, (object) typeof (bool).FullName));
      return false;
    }

    public override object GetValueObject(Type requiredType) => (object) this._booleanValue;
  }
}
