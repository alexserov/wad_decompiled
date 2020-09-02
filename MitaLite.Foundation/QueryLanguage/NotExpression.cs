// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.NotExpression
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage {
    internal class NotExpression : Expression {
        readonly Expression _expression;

        public NotExpression(Expression expression) {
            this._expression = expression;
        }

        public override GlobalizableCondition GetCondition() {
            return new GlobalizableNotCondition(condition: this._expression.GetCondition());
        }

        public override bool Validate(StringBuilder errors) {
            return this._expression.Validate(errors: errors);
        }
    }
}