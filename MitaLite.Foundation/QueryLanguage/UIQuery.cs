// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.UIQuery
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Text;
using MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib;

namespace MS.Internal.Mita.Foundation.QueryLanguage {
    internal static class UIQuery {
        static readonly ConditionLexTable _lexTable = new ConditionLexTable();
        static readonly ConditionYaccTable _yaccTable = new ConditionYaccTable();

        public static GlobalizableCondition Parse(
            string query,
            params object[] objects) {
            var errors = new StringBuilder();
            return (GetExpression(query: query, errors: errors, parameterObjects: objects) ?? throw new UIQueryException(message: errors.ToString())).GetCondition();
        }

        public static string Validate(string query, params object[] objects) {
            var errors = new StringBuilder();
            string str = null;
            if (GetExpression(query: query, errors: errors, parameterObjects: objects) == null)
                str = errors.ToString();
            return str;
        }

        static Expression GetExpression(
            string query,
            StringBuilder errors,
            object[] parameterObjects) {
            var lexStringConsumer = new SSLexStringConsumer(q_string: query);
            SSLex q_lex = new ConditionLexClass(q_table: _lexTable, q_consumer: lexStringConsumer);
            var conditionYaccClass = new ConditionYaccClass(q_table: _yaccTable, q_lex: q_lex, parameterObjects: parameterObjects);
            Expression expression = null;
            if (conditionYaccClass.parse()) {
                errors.AppendLine(value: query);
                errors.Append(value: conditionYaccClass.Error);
            } else {
                expression = (Expression) conditionYaccClass.treeRoot();
                if (!expression.Validate(errors: errors))
                    expression = null;
            }

            return expression;
        }
    }
}