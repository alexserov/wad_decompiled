// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.PropertyExpression
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage
{
  internal class PropertyExpression : Expression
  {
    private PropertyName _propertyName;
    private Value _propertyValue;

    public PropertyExpression(PropertyName propertyName, Value propertyValue)
    {
      this._propertyName = propertyName;
      this._propertyValue = propertyValue;
    }

    public override GlobalizableCondition GetCondition() => (GlobalizableCondition) new GlobalizablePropertyCondition(this._propertyName.GetUIProperty().Property, this._propertyValue.GetValueObject(this._propertyName.GetUIProperty().Type));

    public override bool Validate(StringBuilder errors) => this._propertyName.Validate(errors) & this._propertyValue.Validate(this._propertyName.GetUIProperty().Type, errors);
  }
}
