// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYaccStackElement
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSYaccStackElement {
        public SSLexLexeme m_lexeme;
        public int m_state;
        public SSYaccStackElement[] m_subTree;
        public int m_subTreeSize;

        public SSYaccStackElement() {
            this.m_state = 0;
            this.m_lexeme = null;
            this.m_subTree = null;
        }

        public int state() {
            return this.m_state;
        }

        public SSLexLexeme lexeme() {
            return this.m_lexeme;
        }

        public void setState(int q_state) {
            this.m_state = q_state;
        }

        public void setLexeme(SSLexLexeme q_lexeme) {
            this.m_lexeme = q_lexeme;
        }

        public void createSubTree(int q_size) {
            this.m_subTreeSize = q_size;
            if (q_size <= 0)
                return;
            this.m_subTree = new SSYaccStackElement[q_size];
        }

        public bool addSubTree(int q_index, SSYaccStackElement q_ele) {
            if (q_index < 0 || q_index >= this.m_subTreeSize)
                return false;
            this.m_subTree[q_index] = q_ele;
            return true;
        }

        public SSYaccStackElement getSubTree(int q_index) {
            return q_index < 0 || q_index >= this.m_subTreeSize ? null : this.m_subTree[q_index];
        }

        public int getSubTreeSize() {
            return this.m_subTreeSize;
        }
    }
}