// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.IdentifierValue
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Reflection;
using System.Text;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.QueryLanguage
{
  internal class IdentifierValue : Value
  {
    private string _identifierName;

    public IdentifierValue(string lexeme) => this._identifierName = lexeme;

    public override bool Validate(Type requiredType, StringBuilder errors)
    {
      bool flag = false;
      if (this._identifierName.Equals("null"))
      {
        if (requiredType.GetTypeInfo().IsValueType || requiredType.Equals(typeof (ControlType)))
          errors.AppendLine(StringResource.Get("ParameterTypeMismatch_2", (object) requiredType.FullName, (object) "null"));
        else
          flag = true;
        return flag;
      }
      if (requiredType.Equals(typeof (object)))
        return true;
      FieldInfo fieldInfo = !requiredType.Equals(typeof (ControlType)) ? TypeExtensions.GetField(requiredType, this._identifierName, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public) : TypeExtensions.GetField(requiredType, this._identifierName, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
      if ((object) fieldInfo != null && fieldInfo.FieldType.Equals(requiredType))
        flag = true;
      errors.AppendLine(StringResource.Get("Passed_Type_NotAllowed", (object) requiredType.FullName));
      return flag;
    }

    public override object GetValueObject(Type requiredType)
    {
      if (requiredType.GetTypeInfo().IsEnum)
      {
        TypeExtensions.GetField(requiredType, this._identifierName, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public);
        return Enum.Parse(requiredType, this._identifierName, true);
      }
      return requiredType.Equals(typeof (ControlType)) ? (object) (TypeExtensions.GetField(requiredType, this._identifierName, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue((object) null) as ControlType) : (object) null;
    }
  }
}
