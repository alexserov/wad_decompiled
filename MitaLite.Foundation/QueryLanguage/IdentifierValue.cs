// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.IdentifierValue
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Reflection;
using System.Text;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.QueryLanguage {
    internal class IdentifierValue : Value {
        readonly string _identifierName;

        public IdentifierValue(string lexeme) {
            this._identifierName = lexeme;
        }

        public override bool Validate(Type requiredType, StringBuilder errors) {
            var flag = false;
            if (this._identifierName.Equals(value: "null")) {
                if (requiredType.GetTypeInfo().IsValueType || requiredType.Equals(o: typeof(ControlType)))
                    errors.AppendLine(value: StringResource.Get(id: "ParameterTypeMismatch_2", (object) requiredType.FullName, (object) "null"));
                else
                    flag = true;
                return flag;
            }

            if (requiredType.Equals(o: typeof(object)))
                return true;
            var fieldInfo = !requiredType.Equals(o: typeof(ControlType)) ? TypeExtensions.GetField(type: requiredType, name: this._identifierName, bindingAttr: BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public) : TypeExtensions.GetField(type: requiredType, name: this._identifierName, bindingAttr: BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            if ((object) fieldInfo != null && fieldInfo.FieldType.Equals(o: requiredType))
                flag = true;
            errors.AppendLine(value: StringResource.Get(id: "Passed_Type_NotAllowed", (object) requiredType.FullName));
            return flag;
        }

        public override object GetValueObject(Type requiredType) {
            if (requiredType.GetTypeInfo().IsEnum) {
                TypeExtensions.GetField(type: requiredType, name: this._identifierName, bindingAttr: BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public);
                return Enum.Parse(enumType: requiredType, value: this._identifierName, ignoreCase: true);
            }

            return requiredType.Equals(o: typeof(ControlType)) ? TypeExtensions.GetField(type: requiredType, name: this._identifierName, bindingAttr: BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(obj: null) as ControlType : (object) null;
        }
    }
}