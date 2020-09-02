// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLex
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSLex {
        public SSLexConsumer m_consumer;
        public char[] m_currentChar;
        public int m_state;
        public SSLexTable m_table;

        public SSLex(SSLexTable q_table, SSLexConsumer q_consumer) {
            this.m_table = q_table;
            this.m_consumer = q_consumer;
            this.m_currentChar = new char[1];
        }

        public SSLexTable table() {
            return this.m_table;
        }

        public SSLexConsumer consumer() {
            return this.m_consumer;
        }

        public virtual bool error(SSLexLexeme q_lexeme) {
            return false;
        }

        public virtual bool complete(SSLexLexeme q_lexeme) {
            return true;
        }

        public SSLexLexeme next() {
            SSLexLexeme ssLexLexeme = null;
            while (true) {
                SSLexMark q_mark;
                SSLexFinalState q_final;
                do {
                    this.m_state = 0;
                    var flag = false;
                    q_mark = null;
                    q_final = this.m_table.lookupFinal(q_state: this.m_state);
                    if (q_final.isFinal())
                        this.m_consumer.mark();
                    while (this.m_consumer.next()) {
                        flag = true;
                        this.m_currentChar[0] = this.m_consumer.getCurrent();
                        this.m_table.translateClass(q_char: this.m_currentChar);
                        this.m_state = this.m_table.lookup(q_state: this.m_state, q_next: this.m_currentChar[0]);
                        if (this.m_state != -1) {
                            var ssLexFinalState = this.m_table.lookupFinal(q_state: this.m_state);
                            if (ssLexFinalState.isFinal()) {
                                q_mark = this.m_consumer.mark();
                                q_final = ssLexFinalState;
                            }

                            if (ssLexFinalState.isContextStart())
                                this.m_consumer.mark();
                        } else {
                            break;
                        }
                    }

                    if (flag) {
                        if (q_final.isContextEnd() && q_mark != null)
                            this.m_consumer.flushEndOfLine(q_mark: q_mark);
                        if (q_final.isIgnore() && q_mark != null) {
                            this.m_consumer.flushLexeme(q_mark: q_mark);
                            if (q_final.isPop() && q_final.isPush())
                                this.m_table.gotoSubtable(q_index: q_final.pushIndex());
                            else if (q_final.isPop())
                                this.m_table.popSubtable();
                        } else {
                            goto label_19;
                        }
                    } else {
                        goto label_34;
                    }
                } while (!q_final.isPush());

                this.m_table.pushSubtable(q_index: q_final.pushIndex());
                continue;
                label_19:
                if (!q_final.isFinal() || q_mark == null) {
                    ssLexLexeme = new SSLexLexeme(q_consumer: this.m_consumer);
                    if (!error(q_lexeme: ssLexLexeme)) {
                        this.m_consumer.flushLexeme();
                        ssLexLexeme = null;
                    } else {
                        break;
                    }
                } else {
                    if (q_final.isPop() && q_final.isPush())
                        this.m_table.gotoSubtable(q_index: q_final.pushIndex());
                    else if (q_final.isPop())
                        this.m_table.popSubtable();
                    else if (q_final.isPush())
                        this.m_table.pushSubtable(q_index: q_final.pushIndex());
                    if (q_final.isStartOfLine() && this.m_consumer.line() != 0 && this.m_consumer.offset() != 0)
                        this.m_consumer.flushStartOfLine(q_mark: q_mark);
                    ssLexLexeme = new SSLexLexeme(q_consumer: this.m_consumer, q_final: q_final, q_mark: q_mark);
                    if (q_final.isKeyword())
                        this.m_table.findKeyword(z_lexeme: ssLexLexeme);
                    this.m_consumer.flushLexeme(q_mark: q_mark);
                    if (!complete(q_lexeme: ssLexLexeme))
                        ssLexLexeme = null;
                    else
                        break;
                }
            }

            label_34:
            return ssLexLexeme;
        }
    }
}