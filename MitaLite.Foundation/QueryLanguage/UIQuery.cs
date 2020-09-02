// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.UIQuery
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib;
using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage
{
  internal static class UIQuery
  {
    private static ConditionLexTable _lexTable = new ConditionLexTable();
    private static ConditionYaccTable _yaccTable = new ConditionYaccTable();

    public static GlobalizableCondition Parse(
      string query,
      params object[] objects)
    {
      StringBuilder errors = new StringBuilder();
      return (UIQuery.GetExpression(query, errors, objects) ?? throw new UIQueryException(errors.ToString())).GetCondition();
    }

    public static string Validate(string query, params object[] objects)
    {
      StringBuilder errors = new StringBuilder();
      string str = (string) null;
      if (UIQuery.GetExpression(query, errors, objects) == null)
        str = errors.ToString();
      return str;
    }

    private static Expression GetExpression(
      string query,
      StringBuilder errors,
      object[] parameterObjects)
    {
      SSLexStringConsumer lexStringConsumer = new SSLexStringConsumer(query);
      SSLex q_lex = (SSLex) new ConditionLexClass((SSLexTable) UIQuery._lexTable, (SSLexConsumer) lexStringConsumer);
      ConditionYaccClass conditionYaccClass = new ConditionYaccClass((SSYaccTable) UIQuery._yaccTable, q_lex, parameterObjects);
      Expression expression = (Expression) null;
      if (conditionYaccClass.parse())
      {
        errors.AppendLine(query);
        errors.Append(conditionYaccClass.Error);
      }
      else
      {
        expression = (Expression) conditionYaccClass.treeRoot();
        if (!expression.Validate(errors))
          expression = (Expression) null;
      }
      return expression;
    }
  }
}
