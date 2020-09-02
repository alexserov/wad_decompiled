// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.ConditionYaccClass
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Text;
using MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib;

namespace MS.Internal.Mita.Foundation.QueryLanguage {
    internal class ConditionYaccClass : SSYacc {
        public const int ConditionYaccProdStart = 1;
        public const int ConditionYaccProdExprNot = 2;
        public const int ConditionYaccProdExprAnd = 3;
        public const int ConditionYaccProdExprOr = 4;
        public const int ConditionYaccProdExprNested = 5;
        public const int ConditionYaccProdExprTrue = 6;
        public const int ConditionYaccProdExprFalse = 7;
        public const int ConditionYaccProdExprEqualsLeft = 8;
        public const int ConditionYaccProdExprEqualsRight = 9;
        public const int ConditionYaccProdPropertySimple = 10;
        public const int ConditionYaccProdPropertyQualified = 11;
        public const int ConditionYaccProdValueObject = 12;
        public const int ConditionYaccProdValueString = 13;
        public const int ConditionYaccProdValueTrue = 14;
        public const int ConditionYaccProdValueFalse = 15;
        public const int ConditionYaccProdValueControlType = 16;
        public const int ConditionYaccProdValueNumber = 17;
        public const int ConditionYaccProdNumberNegative = 18;
        public const int ConditionYaccProdNumberPositive = 19;
        public const int ConditionYaccProdValueInteger = 20;
        public const int ConditionYaccProdValueDouble = 21;
        readonly object[] parameterObjects;

        public ConditionYaccClass(SSYaccTable q_table, SSLex q_lex, object[] parameterObjects)
            : base(q_table: q_table, q_lex: q_lex) {
            this.parameterObjects = parameterObjects;
        }

        public string Error { get; set; } = "Unknown error";

        public override SSYaccStackElement reduce(int q_prod, int q_size) {
            switch (q_prod) {
                case 1:
                    return elementFromProduction(q_index: 0);
                case 2:
                    return new NotExpression(expression: (Expression) elementFromProduction(q_index: 1));
                case 3:
                    return new AndExpression(leftExpression: (Expression) elementFromProduction(q_index: 0), rightExpression: (Expression) elementFromProduction(q_index: 2));
                case 4:
                    return new OrExpression(leftExpression: (Expression) elementFromProduction(q_index: 0), rightExpression: (Expression) elementFromProduction(q_index: 2));
                case 5:
                    return elementFromProduction(q_index: 1);
                case 6:
                    return new BooleanLiteralExpression(value: true);
                case 7:
                    return new BooleanLiteralExpression(value: false);
                case 8:
                    return new PropertyExpression(propertyName: (PropertyName) elementFromProduction(q_index: 0), propertyValue: (Value) elementFromProduction(q_index: 2));
                case 9:
                    return new PropertyExpression(propertyName: (PropertyName) elementFromProduction(q_index: 2), propertyValue: (Value) elementFromProduction(q_index: 0));
                case 10:
                    return new PropertyName(lexeme: new string(value: elementFromProduction(q_index: 1).lexeme().lexeme()));
                case 11:
                    return new PropertyName(patternName: new string(value: elementFromProduction(q_index: 1).lexeme().lexeme()), propertyName: new string(value: elementFromProduction(q_index: 3).lexeme().lexeme()));
                case 12:
                    return new ObjectValue(lexeme: new string(value: elementFromProduction(q_index: 1).lexeme().lexeme()), parameterObjects: this.parameterObjects);
                case 13:
                    return new StringValue(lexeme: new string(value: elementFromProduction(q_index: 0).lexeme().lexeme()));
                case 14:
                    return new BooleanValue(value: true);
                case 15:
                    return new BooleanValue(value: false);
                case 16:
                    return new IdentifierValue(lexeme: new string(value: elementFromProduction(q_index: 0).lexeme().lexeme()));
                case 17:
                    return elementFromProduction(q_index: 0);
                case 18:
                    var number = (Number) elementFromProduction(q_index: 1);
                    number.Negate();
                    return number;
                case 19:
                    return elementFromProduction(q_index: 1);
                case 20:
                    return new IntegerValue(lexeme: new string(value: elementFromProduction(q_index: 0).lexeme().lexeme()));
                case 21:
                    return new DoubleValue(lexeme: new string(value: elementFromProduction(q_index: 0).lexeme().lexeme()));
                default:
                    return stackElement();
            }
        }

        public override bool error(int q_state, SSLexLexeme q_look) {
            var num = q_look.offset();
            var str = "^";
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(value: str.PadLeft(totalWidth: num + 1));
            stringBuilder.AppendLine(value: "Parse error at offset " + num);
            this.Error = stringBuilder.ToString();
            return base.error(q_state: q_state, q_look: q_look);
        }
    }
}