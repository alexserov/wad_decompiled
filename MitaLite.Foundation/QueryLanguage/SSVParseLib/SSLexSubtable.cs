// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexSubtable
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSLexSubtable
  {
    public int m_size;
    public SSLexTableRow[] m_rows;
    public SSLexFinalState[] m_final;
    public const int SSLexStateInvalid = -1;

    public SSLexSubtable(int q_numRows, int[] q_rows, int[] q_final)
    {
      this.m_size = q_numRows;
      int q_index1 = 0;
      int q_index2 = 0;
      this.m_rows = new SSLexTableRow[this.m_size];
      this.m_final = new SSLexFinalState[this.m_size];
      for (int index = 0; index < this.m_size; ++index)
      {
        this.m_rows[index] = new SSLexTableRow(q_rows, q_index1);
        this.m_final[index] = new SSLexFinalState(q_final, q_index2);
        q_index2 += 3;
        q_index1 += q_rows[q_index1] * 3 + 1;
      }
    }

    public int lookup(int q_state, int q_next) => this.m_rows[q_state].lookup(q_next);

    public SSLexFinalState lookupFinal(int q_state) => this.m_final[q_state];
  }
}
