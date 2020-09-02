// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.DoubleValue
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage
{
  internal class DoubleValue : Number
  {
    private string _lexeme;
    private bool _negative;

    public DoubleValue(string lexeme) => this._lexeme = lexeme;

    public override bool Validate(Type requiredType, StringBuilder errors)
    {
      if (requiredType.Equals(typeof (object)) || requiredType.Equals(typeof (double)))
      {
        try
        {
          double.Parse(this._lexeme, (IFormatProvider) CultureInfo.InvariantCulture);
          return true;
        }
        catch (OverflowException ex)
        {
          errors.AppendLine(StringResource.Get("DoubleTooLarge"));
        }
      }
      errors.AppendLine(StringResource.Get("ParameterTypeMismatch_2", (object) requiredType.FullName, (object) typeof (double).FullName));
      return false;
    }

    public override object GetValueObject(Type requiredType)
    {
      double num;
      try
      {
        num = double.Parse(this._lexeme, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (OverflowException ex)
      {
        throw new UIQueryException(StringResource.Get("DoubleTooLarge"), (Exception) ex);
      }
      return (object) (this._negative ? -num : num);
    }

    public override void Negate() => this._negative = !this._negative;
  }
}
