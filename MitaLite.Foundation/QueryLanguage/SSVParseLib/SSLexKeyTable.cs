// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexKeyTable
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSLexKeyTable {
        public int[] m_index;
        public string[] m_keys;

        public SSLexKeyTable(int[] q_index, string[] q_keys) {
            this.m_index = q_index;
            this.m_keys = q_keys;
        }
    }
}