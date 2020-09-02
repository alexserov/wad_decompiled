// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.DoubleValue
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage {
    internal class DoubleValue : Number {
        readonly string _lexeme;
        bool _negative;

        public DoubleValue(string lexeme) {
            this._lexeme = lexeme;
        }

        public override bool Validate(Type requiredType, StringBuilder errors) {
            if (requiredType.Equals(o: typeof(object)) || requiredType.Equals(o: typeof(double)))
                try {
                    double.Parse(s: this._lexeme, provider: CultureInfo.InvariantCulture);
                    return true;
                } catch (OverflowException ex) {
                    errors.AppendLine(value: StringResource.Get(id: "DoubleTooLarge"));
                }

            errors.AppendLine(value: StringResource.Get(id: "ParameterTypeMismatch_2", (object) requiredType.FullName, (object) typeof(double).FullName));
            return false;
        }

        public override object GetValueObject(Type requiredType) {
            double num;
            try {
                num = double.Parse(s: this._lexeme, provider: CultureInfo.InvariantCulture);
            } catch (OverflowException ex) {
                throw new UIQueryException(message: StringResource.Get(id: "DoubleTooLarge"), innerException: ex);
            }

            return this._negative ? -num : num;
        }

        public override void Negate() {
            this._negative = !this._negative;
        }
    }
}