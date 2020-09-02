// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.IntegerValue
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage {
    internal class IntegerValue : Number {
        readonly string _lexeme;
        bool _negative;

        public IntegerValue(string lexeme) {
            this._lexeme = lexeme;
        }

        public override bool Validate(Type requiredType, StringBuilder errors) {
            if (requiredType.Equals(o: typeof(object)) || requiredType.Equals(o: typeof(int)))
                try {
                    Parse();
                    return true;
                } catch (OverflowException ex) {
                    errors.AppendLine(value: StringResource.Get(id: "IntegralTooLarge"));
                }

            errors.AppendLine(value: StringResource.Get(id: "ParameterTypeMismatch_2", (object) requiredType.FullName, (object) typeof(int).FullName));
            return false;
        }

        public override object GetValueObject(Type requiredType) {
            int num;
            try {
                num = Parse();
            } catch (OverflowException ex) {
                throw new UIQueryException(message: StringResource.Get(id: "IntegralTooLarge"), innerException: ex);
            }

            return this._negative ? -num : num;
        }

        public override void Negate() {
            this._negative = !this._negative;
        }

        int Parse() {
            return !this._lexeme.StartsWith(value: "0x", comparisonType: StringComparison.OrdinalIgnoreCase) ? int.Parse(s: this._lexeme, provider: CultureInfo.InvariantCulture) : int.Parse(s: this._lexeme.Substring(startIndex: 2), style: NumberStyles.HexNumber, provider: CultureInfo.InvariantCulture);
        }
    }
}