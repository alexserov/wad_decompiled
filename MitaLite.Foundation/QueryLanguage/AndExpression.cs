// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.AndExpression
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage {
    internal class AndExpression : Expression {
        readonly Expression _leftExpression;
        readonly Expression _rightExpression;

        public AndExpression(Expression leftExpression, Expression rightExpression) {
            this._leftExpression = leftExpression;
            this._rightExpression = rightExpression;
        }

        public override GlobalizableCondition GetCondition() {
            return new GlobalizableAndCondition(this._leftExpression.GetCondition(), this._rightExpression.GetCondition());
        }

        public override bool Validate(StringBuilder errors) {
            return this._leftExpression.Validate(errors: errors) & this._rightExpression.Validate(errors: errors);
        }
    }
}