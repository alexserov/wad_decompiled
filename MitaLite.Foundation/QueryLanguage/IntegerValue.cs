// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.IntegerValue
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage
{
  internal class IntegerValue : Number
  {
    private string _lexeme;
    private bool _negative;

    public IntegerValue(string lexeme) => this._lexeme = lexeme;

    public override bool Validate(Type requiredType, StringBuilder errors)
    {
      if (requiredType.Equals(typeof (object)) || requiredType.Equals(typeof (int)))
      {
        try
        {
          this.Parse();
          return true;
        }
        catch (OverflowException ex)
        {
          errors.AppendLine(StringResource.Get("IntegralTooLarge"));
        }
      }
      errors.AppendLine(StringResource.Get("ParameterTypeMismatch_2", (object) requiredType.FullName, (object) typeof (int).FullName));
      return false;
    }

    public override object GetValueObject(Type requiredType)
    {
      int num;
      try
      {
        num = this.Parse();
      }
      catch (OverflowException ex)
      {
        throw new UIQueryException(StringResource.Get("IntegralTooLarge"), (Exception) ex);
      }
      return (object) (this._negative ? -num : num);
    }

    public override void Negate() => this._negative = !this._negative;

    private int Parse() => !this._lexeme.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? int.Parse(this._lexeme, (IFormatProvider) CultureInfo.InvariantCulture) : int.Parse(this._lexeme.Substring(2), NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture);
  }
}
