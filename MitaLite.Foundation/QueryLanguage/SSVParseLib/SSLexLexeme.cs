// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexLexeme
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSLexLexeme {
        public int m_length;
        public char[] m_lexeme;
        public int m_line;
        public int m_offset;
        public int m_parseToken;
        public int m_token;

        public SSLexLexeme() {
            this.m_line = 0;
            this.m_token = 0;
            this.m_length = 0;
            this.m_offset = 0;
        }

        public SSLexLexeme(string q_lexeme, int q_token) {
            this.m_line = 0;
            this.m_token = q_token;
            this.m_offset = 0;
            this.m_parseToken = 0;
            this.m_length = q_lexeme.Length;
            this.m_lexeme = q_lexeme.ToCharArray();
        }

        public SSLexLexeme(SSLexConsumer q_consumer) {
            this.m_token = 0;
            this.m_line = q_consumer.line();
            this.m_offset = q_consumer.offset();
            this.m_length = q_consumer.lexemeLength();
            this.m_lexeme = q_consumer.lexemeBuffer();
        }

        public SSLexLexeme(SSLexConsumer q_consumer, SSLexFinalState q_final, SSLexMark q_mark) {
            this.m_token = q_final.token();
            this.m_line = q_consumer.line();
            this.m_offset = q_consumer.offset();
            this.m_lexeme = q_consumer.lexemeBuffer(q_mark: q_mark);
            this.m_length = q_consumer.lexemeLength(q_mark: q_mark);
        }

        public int line() {
            return this.m_line;
        }

        public int token() {
            return this.m_token;
        }

        public int offset() {
            return this.m_offset;
        }

        public int length() {
            return this.m_length;
        }

        public int parseToken() {
            return this.m_parseToken;
        }

        public char[] lexeme() {
            return this.m_lexeme;
        }

        public void setLine(int q_line) {
            this.m_line = q_line;
        }

        public void setToken(int q_token) {
            this.m_token = q_token;
        }

        public void setOffset(int q_offset) {
            this.m_offset = q_offset;
        }

        public void setLength(int q_length) {
            this.m_length = q_length;
        }

        public void setparseToken(int q_parseToken) {
            this.m_parseToken = q_parseToken;
        }

        public void setLexeme(char[] q_lexeme) {
            this.m_lexeme = q_lexeme;
        }
    }
}