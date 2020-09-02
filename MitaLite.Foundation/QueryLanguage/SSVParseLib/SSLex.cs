// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLex
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSLex
  {
    public int m_state;
    public char[] m_currentChar;
    public SSLexTable m_table;
    public SSLexConsumer m_consumer;

    public SSLex(SSLexTable q_table, SSLexConsumer q_consumer)
    {
      this.m_table = q_table;
      this.m_consumer = q_consumer;
      this.m_currentChar = new char[1];
    }

    public SSLexTable table() => this.m_table;

    public SSLexConsumer consumer() => this.m_consumer;

    public virtual bool error(SSLexLexeme q_lexeme) => false;

    public virtual bool complete(SSLexLexeme q_lexeme) => true;

    public SSLexLexeme next()
    {
      SSLexLexeme ssLexLexeme = (SSLexLexeme) null;
      while (true)
      {
        SSLexMark q_mark;
        SSLexFinalState q_final;
        do
        {
          this.m_state = 0;
          bool flag = false;
          q_mark = (SSLexMark) null;
          q_final = this.m_table.lookupFinal(this.m_state);
          if (q_final.isFinal())
            this.m_consumer.mark();
          while (this.m_consumer.next())
          {
            flag = true;
            this.m_currentChar[0] = this.m_consumer.getCurrent();
            this.m_table.translateClass(this.m_currentChar);
            this.m_state = this.m_table.lookup(this.m_state, (int) this.m_currentChar[0]);
            if (this.m_state != -1)
            {
              SSLexFinalState ssLexFinalState = this.m_table.lookupFinal(this.m_state);
              if (ssLexFinalState.isFinal())
              {
                q_mark = this.m_consumer.mark();
                q_final = ssLexFinalState;
              }
              if (ssLexFinalState.isContextStart())
                this.m_consumer.mark();
            }
            else
              break;
          }
          if (flag)
          {
            if (q_final.isContextEnd() && q_mark != null)
              this.m_consumer.flushEndOfLine(q_mark);
            if (q_final.isIgnore() && q_mark != null)
            {
              this.m_consumer.flushLexeme(q_mark);
              if (q_final.isPop() && q_final.isPush())
                this.m_table.gotoSubtable(q_final.pushIndex());
              else if (q_final.isPop())
                this.m_table.popSubtable();
            }
            else
              goto label_19;
          }
          else
            goto label_34;
        }
        while (!q_final.isPush());
        this.m_table.pushSubtable(q_final.pushIndex());
        continue;
label_19:
        if (!q_final.isFinal() || q_mark == null)
        {
          ssLexLexeme = new SSLexLexeme(this.m_consumer);
          if (!this.error(ssLexLexeme))
          {
            this.m_consumer.flushLexeme();
            ssLexLexeme = (SSLexLexeme) null;
          }
          else
            break;
        }
        else
        {
          if (q_final.isPop() && q_final.isPush())
            this.m_table.gotoSubtable(q_final.pushIndex());
          else if (q_final.isPop())
            this.m_table.popSubtable();
          else if (q_final.isPush())
            this.m_table.pushSubtable(q_final.pushIndex());
          if (q_final.isStartOfLine() && this.m_consumer.line() != 0 && this.m_consumer.offset() != 0)
            this.m_consumer.flushStartOfLine(q_mark);
          ssLexLexeme = new SSLexLexeme(this.m_consumer, q_final, q_mark);
          if (q_final.isKeyword())
            this.m_table.findKeyword(ssLexLexeme);
          this.m_consumer.flushLexeme(q_mark);
          if (!this.complete(ssLexLexeme))
            ssLexLexeme = (SSLexLexeme) null;
          else
            break;
        }
      }
label_34:
      return ssLexLexeme;
    }
  }
}
