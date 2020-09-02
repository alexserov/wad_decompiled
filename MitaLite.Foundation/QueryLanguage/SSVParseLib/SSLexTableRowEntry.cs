// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexTableRowEntry
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSLexTableRowEntry {
        public int m_end;
        public int m_start;
        public int m_state;

        public SSLexTableRowEntry(int q_start, int q_end, int q_state) {
            this.m_end = q_end;
            this.m_start = q_start;
            this.m_state = q_state;
        }

        public int end() {
            return this.m_end;
        }

        public int start() {
            return this.m_start;
        }

        public int state() {
            return this.m_state;
        }
    }
}