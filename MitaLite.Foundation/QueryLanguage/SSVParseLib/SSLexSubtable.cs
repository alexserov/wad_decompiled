// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexSubtable
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSLexSubtable {
        public const int SSLexStateInvalid = -1;
        public SSLexFinalState[] m_final;
        public SSLexTableRow[] m_rows;
        public int m_size;

        public SSLexSubtable(int q_numRows, int[] q_rows, int[] q_final) {
            this.m_size = q_numRows;
            var q_index1 = 0;
            var q_index2 = 0;
            this.m_rows = new SSLexTableRow[this.m_size];
            this.m_final = new SSLexFinalState[this.m_size];
            for (var index = 0; index < this.m_size; ++index) {
                this.m_rows[index] = new SSLexTableRow(q_row: q_rows, q_index: q_index1);
                this.m_final[index] = new SSLexFinalState(q_final: q_final, q_index: q_index2);
                q_index2 += 3;
                q_index1 += q_rows[q_index1] * 3 + 1;
            }
        }

        public int lookup(int q_state, int q_next) {
            return this.m_rows[q_state].lookup(q_code: q_next);
        }

        public SSLexFinalState lookupFinal(int q_state) {
            return this.m_final[q_state];
        }
    }
}