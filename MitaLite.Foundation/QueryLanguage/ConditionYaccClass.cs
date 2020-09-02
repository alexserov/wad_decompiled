// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.ConditionYaccClass
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib;
using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage
{
  internal class ConditionYaccClass : SSYacc
  {
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
    private object[] parameterObjects;
    private string m_Error = "Unknown error";

    public ConditionYaccClass(SSYaccTable q_table, SSLex q_lex, object[] parameterObjects)
      : base(q_table, q_lex)
      => this.parameterObjects = parameterObjects;

    public override SSYaccStackElement reduce(int q_prod, int q_size)
    {
      switch (q_prod)
      {
        case 1:
          return this.elementFromProduction(0);
        case 2:
          return (SSYaccStackElement) new NotExpression((Expression) this.elementFromProduction(1));
        case 3:
          return (SSYaccStackElement) new AndExpression((Expression) this.elementFromProduction(0), (Expression) this.elementFromProduction(2));
        case 4:
          return (SSYaccStackElement) new OrExpression((Expression) this.elementFromProduction(0), (Expression) this.elementFromProduction(2));
        case 5:
          return this.elementFromProduction(1);
        case 6:
          return (SSYaccStackElement) new BooleanLiteralExpression(true);
        case 7:
          return (SSYaccStackElement) new BooleanLiteralExpression(false);
        case 8:
          return (SSYaccStackElement) new PropertyExpression((PropertyName) this.elementFromProduction(0), (Value) this.elementFromProduction(2));
        case 9:
          return (SSYaccStackElement) new PropertyExpression((PropertyName) this.elementFromProduction(2), (Value) this.elementFromProduction(0));
        case 10:
          return (SSYaccStackElement) new PropertyName(new string(this.elementFromProduction(1).lexeme().lexeme()));
        case 11:
          return (SSYaccStackElement) new PropertyName(new string(this.elementFromProduction(1).lexeme().lexeme()), new string(this.elementFromProduction(3).lexeme().lexeme()));
        case 12:
          return (SSYaccStackElement) new ObjectValue(new string(this.elementFromProduction(1).lexeme().lexeme()), this.parameterObjects);
        case 13:
          return (SSYaccStackElement) new StringValue(new string(this.elementFromProduction(0).lexeme().lexeme()));
        case 14:
          return (SSYaccStackElement) new BooleanValue(true);
        case 15:
          return (SSYaccStackElement) new BooleanValue(false);
        case 16:
          return (SSYaccStackElement) new IdentifierValue(new string(this.elementFromProduction(0).lexeme().lexeme()));
        case 17:
          return this.elementFromProduction(0);
        case 18:
          Number number = (Number) this.elementFromProduction(1);
          number.Negate();
          return (SSYaccStackElement) number;
        case 19:
          return this.elementFromProduction(1);
        case 20:
          return (SSYaccStackElement) new IntegerValue(new string(this.elementFromProduction(0).lexeme().lexeme()));
        case 21:
          return (SSYaccStackElement) new DoubleValue(new string(this.elementFromProduction(0).lexeme().lexeme()));
        default:
          return this.stackElement();
      }
    }

    public override bool error(int q_state, SSLexLexeme q_look)
    {
      int num = q_look.offset();
      string str = "^";
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(str.PadLeft(num + 1));
      stringBuilder.AppendLine("Parse error at offset " + (object) num);
      this.m_Error = stringBuilder.ToString();
      return base.error(q_state, q_look);
    }

    public string Error => this.m_Error;
  }
}
