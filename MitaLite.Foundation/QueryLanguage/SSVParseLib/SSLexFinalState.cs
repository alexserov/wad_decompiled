// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexFinalState
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSLexFinalState
  {
    public static int m_flagContextStart = 1;
    public static int m_flagStartOfLine = 2;
    public static int m_flagPop = 8;
    public static int m_flagFinal = 16;
    public static int m_flagPush = 32;
    public static int m_flagIgnore = 64;
    public static int m_flagContextEnd = 128;
    public static int m_flagReduce = 256;
    public static int m_flagKeyword = 512;
    public static int m_flagParseToken = 1024;
    public int m_flags;
    public int m_token;
    public int m_pushIndex;

    public SSLexFinalState(int[] q_final, int q_index)
    {
      this.m_token = q_final[q_index];
      this.m_pushIndex = q_final[q_index + 1];
      this.m_flags = q_final[q_index + 2];
    }

    public int token() => this.m_token;

    public int pushIndex() => this.m_pushIndex;

    public bool isPop() => (this.m_flags & SSLexFinalState.m_flagPop) != 0;

    public bool isPush() => (this.m_flags & SSLexFinalState.m_flagPush) != 0;

    public bool isFinal() => (this.m_flags & SSLexFinalState.m_flagFinal) != 0;

    public bool isIgnore() => (this.m_flags & SSLexFinalState.m_flagIgnore) != 0;

    public bool isReduce() => (this.m_flags & SSLexFinalState.m_flagReduce) != 0;

    public bool isContextEnd() => (this.m_flags & SSLexFinalState.m_flagContextEnd) != 0;

    public bool isStartOfLine() => (this.m_flags & SSLexFinalState.m_flagStartOfLine) != 0;

    public bool isContextStart() => (this.m_flags & SSLexFinalState.m_flagContextStart) != 0;

    public bool isKeyword() => (this.m_flags & SSLexFinalState.m_flagKeyword) != 0;

    public bool isParseToken() => (this.m_flags & SSLexFinalState.m_flagParseToken) != 0;
  }
}
