﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.BooleanLiteralExpression
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Text;
using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.QueryLanguage {
    internal class BooleanLiteralExpression : Expression {
        readonly bool _value;

        public BooleanLiteralExpression(bool value) {
            this._value = value;
        }

        public override GlobalizableCondition GetCondition() {
            return !this._value ? new GlobalizableOtherConditions(condition: Condition.FalseCondition) : (GlobalizableCondition) new GlobalizableOtherConditions(condition: Condition.TrueCondition);
        }

        public override bool Validate(StringBuilder errors) {
            return true;
        }
    }
}