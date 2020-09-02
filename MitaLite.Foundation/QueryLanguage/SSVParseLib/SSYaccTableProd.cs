// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYaccTableProd
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSYaccTableProd {
        public int m_leftside;
        public int m_size;

        public SSYaccTableProd(int q_size, int q_leftside) {
            this.m_size = q_size;
            this.m_leftside = q_leftside;
        }

        public int size() {
            return this.m_size;
        }

        public int leftside() {
            return this.m_leftside;
        }
    }
}