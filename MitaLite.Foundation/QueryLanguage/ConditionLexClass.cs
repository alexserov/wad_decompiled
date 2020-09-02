// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.ConditionLexClass
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib;

namespace MS.Internal.Mita.Foundation.QueryLanguage
{
  internal class ConditionLexClass : SSLex
  {
    public const int ConditionLexExprMain = 0;
    public const int ConditionLexTokenIndentifier = 52;
    public const int ConditionLexTokenOr = 53;
    public const int ConditionLexTokenAnd = 54;
    public const int ConditionLexTokenNot = 55;
    public const int ConditionLexTokenOParen = 56;
    public const int ConditionLexTokenCParen = 57;
    public const int ConditionLexTokenOCurly = 58;
    public const int ConditionLexTokenCCurly = 59;
    public const int ConditionLexTokenEqual = 60;
    public const int ConditionLexTokenMinus = 61;
    public const int ConditionLexTokenPlus = 62;
    public const int ConditionLexTokenDot = 63;
    public const int ConditionLexTokenAt = 64;
    public const int ConditionLexTokenString = 65;
    public const int ConditionLexTokenInteger = 66;
    public const int ConditionLexTokenDouble = 67;
    public const int ConditionLexTokenFalse = 68;
    public const int ConditionLexTokenTrue = 69;

    public ConditionLexClass(SSLexTable q_table, SSLexConsumer q_consumer)
      : base(q_table, q_consumer)
    {
    }

    public string tokenToString(int q_token)
    {
      string str;
      switch (q_token)
      {
        case -2:
          str = "%error";
          break;
        case -1:
          str = "eof";
          break;
        case 52:
          str = "identifier";
          break;
        case 53:
          str = "or";
          break;
        case 54:
          str = "and";
          break;
        case 55:
          str = "not";
          break;
        case 56:
          str = "(";
          break;
        case 57:
          str = ")";
          break;
        case 58:
          str = "{";
          break;
        case 59:
          str = "}";
          break;
        case 60:
          str = "=";
          break;
        case 61:
          str = "-";
          break;
        case 62:
          str = "+";
          break;
        case 63:
          str = ".";
          break;
        case 64:
          str = "@";
          break;
        case 65:
          str = "string";
          break;
        case 66:
          str = "integer";
          break;
        case 67:
          str = "double";
          break;
        case 68:
          str = "false";
          break;
        case 69:
          str = "true";
          break;
        default:
          str = "Token Not Found";
          break;
      }
      return str;
    }

    public override bool error(SSLexLexeme q_lexeme)
    {
      q_lexeme.setToken(0);
      return true;
    }
  }
}
