// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.ObjectValue
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;
using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage {
    internal class ObjectValue : Value {
        readonly int _index;
        readonly object[] _parameterObjects;

        public ObjectValue(string lexeme, object[] parameterObjects) {
            this._parameterObjects = parameterObjects;
            if (lexeme.StartsWith(value: "0x", comparisonType: StringComparison.OrdinalIgnoreCase))
                this._index = int.Parse(s: lexeme.Substring(startIndex: 2), style: NumberStyles.HexNumber, provider: CultureInfo.InvariantCulture);
            else
                this._index = int.Parse(s: lexeme, provider: CultureInfo.InvariantCulture);
        }

        public override bool Validate(Type requiredType, StringBuilder errors) {
            bool flag;
            if (this._index >= 0 && this._index < this._parameterObjects.Length) {
                flag = true;
            } else {
                errors.AppendLine(value: StringResource.Get(id: "InvalidIndex"));
                flag = false;
            }

            return flag;
        }

        public override object GetValueObject(Type requiredType) {
            if (this._index >= 0 && this._index < this._parameterObjects.Length)
                return this._parameterObjects[this._index];
            throw new UIQueryException(message: StringResource.Get(id: "InvalidIndex"));
        }
    }
}