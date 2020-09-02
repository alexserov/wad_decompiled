// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYacc
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSYacc
  {
    public const int SSYaccActionShift = 0;
    public const int SSYaccActionError = 1;
    public const int SSYaccActionReduce = 2;
    public const int SSYaccActionAccept = 3;
    public const int SSYaccActionConflict = 4;
    public int m_cache;
    public int m_state;
    public SSLex m_lex;
    public int m_action;
    public int m_leftside;
    public bool m_error;
    public bool m_abort;
    public int m_production;
    public SSYaccStack m_stack;
    public SSYaccTable m_table;
    public int m_productionSize;
    public bool m_endOfInput;
    public SSLexLexeme m_endLexeme;
    public SSLexLexeme m_lookahead;
    public SSLexLexeme m_larLookahead;
    public SSLexSubtable m_lexSubtable;
    public SSYaccStackElement m_element;
    public SSYaccStackElement m_treeRoot;
    public SSYaccCache m_lexemeCache;
    public const int m_eofToken = -1;
    public const int m_errorToken = -2;
    public const int SSYaccLexemeCacheMax = -1;

    public SSYacc(SSYaccTable q_table, SSLex q_lex)
    {
      this.m_cache = 0;
      this.m_lex = q_lex;
      this.m_abort = false;
      this.m_error = false;
      this.m_table = q_table;
      this.m_endOfInput = false;
      this.m_endLexeme = new SSLexLexeme("eof", -1);
      this.m_endLexeme.setToken(-1);
      this.m_stack = new SSYaccStack(5, 5);
      this.m_lexemeCache = new SSYaccCache();
      this.m_element = this.stackElement();
      this.push();
    }

    public virtual SSYaccStackElement reduce(int q_prod, int q_length) => this.stackElement();

    public virtual SSLexLexeme nextLexeme() => this.m_lex.next();

    public virtual SSYaccStackElement stackElement() => new SSYaccStackElement();

    public virtual SSYaccStackElement shift(SSLexLexeme q_lexeme) => this.stackElement();

    public bool larLookahead(SSLexLexeme q_lex) => false;

    public virtual bool error(int q_state, SSLexLexeme q_look) => this.syncErr();

    public bool larError(int q_state, SSLexLexeme q_look, SSLexLexeme q_larLook) => this.error(q_state, q_look);

    public bool parse()
    {
      if (this.doGetLexeme(true))
        return true;
      while (!this.m_abort)
      {
        switch (this.m_action)
        {
          case 0:
            if (this.doShift())
              return true;
            continue;
          case 1:
            if (this.doError())
              return true;
            continue;
          case 2:
            if (this.doReduce())
              return true;
            continue;
          case 3:
            this.m_treeRoot = this.m_element;
            return this.m_error;
          case 4:
            if (this.doConflict())
              return true;
            continue;
          default:
            return true;
        }
      }
      return true;
    }

    public bool doShift()
    {
      this.m_element = this.shift(this.m_lookahead);
      if (this.m_element == null)
        return true;
      this.m_element.setLexeme(this.m_lookahead);
      this.m_element.setState(this.m_state);
      this.push();
      return this.doGetLexeme(true);
    }

    public bool doReduce()
    {
      this.m_element = this.reduce(this.m_production, this.m_productionSize);
      if (this.m_element == null)
        return true;
      this.pop(this.m_productionSize);
      return this.goTo(this.m_leftside);
    }

    public bool doError()
    {
      this.m_error = true;
      return this.error(this.m_state, this.m_lookahead);
    }

    public bool doLarError()
    {
      this.m_error = true;
      return this.larError(this.m_state, this.m_lookahead, this.m_larLookahead);
    }

    public SSLexLexeme getLexemeCache()
    {
      SSLexLexeme ssLexLexeme = (SSLexLexeme) null;
      if (this.m_cache != -1 && this.m_lexemeCache.hasElements())
        ssLexLexeme = (SSLexLexeme) this.m_lexemeCache.Dequeue();
      if (ssLexLexeme == null)
      {
        this.m_cache = -1;
        ssLexLexeme = this.nextLexeme() ?? this.m_endLexeme;
        this.m_lexemeCache.Enqueue((object) ssLexLexeme);
      }
      return ssLexLexeme;
    }

    public bool doConflict()
    {
      this.m_cache = 0;
      int q_state = this.m_lexSubtable.lookup(0, this.m_lookahead.token());
      while ((this.m_larLookahead = this.getLexemeCache()) != null)
      {
        q_state = this.m_lexSubtable.lookup(q_state, this.m_larLookahead.token());
        if (q_state != -1)
        {
          SSLexFinalState ssLexFinalState = this.m_lexSubtable.lookupFinal(q_state);
          if (ssLexFinalState.isFinal())
          {
            if (ssLexFinalState.isReduce())
            {
              this.m_production = ssLexFinalState.token();
              SSYaccTableProd ssYaccTableProd = this.m_table.lookupProd(this.m_production);
              this.m_leftside = ssYaccTableProd.leftside();
              this.m_productionSize = ssYaccTableProd.size();
              return this.doReduce();
            }
            this.m_state = ssLexFinalState.token();
            return this.doShift();
          }
        }
        else
          break;
      }
      return this.doLarError();
    }

    public bool doGetLexeme(bool q_look)
    {
      if ((this.m_lookahead = this.m_lexemeCache.remove()) == null)
        return this.getLexeme(q_look);
      if (this.larLookahead(this.m_lookahead))
        return true;
      if (q_look)
        this.lookupAction(this.m_state, this.m_lookahead.token());
      return false;
    }

    public bool getLexeme(bool q_look)
    {
      if (this.m_endOfInput)
        return true;
      this.m_lookahead = this.nextLexeme();
      if (this.m_lookahead == null)
      {
        this.m_endOfInput = true;
        this.m_lookahead = this.m_endLexeme;
      }
      if (q_look)
        this.lookupAction(this.m_state, this.m_lookahead.token());
      return false;
    }

    public bool goTo(int q_goto)
    {
      if (this.lookupGoto(this.m_state, this.m_leftside))
        return true;
      this.m_element.setState(this.m_state);
      this.push();
      this.lookupAction(this.m_state, this.m_lookahead.token());
      return false;
    }

    public bool syncErr()
    {
      SSYaccSet ssYaccSet = new SSYaccSet();
      for (int index = 0; index < this.m_stack.getSize(); ++index)
      {
        SSYaccTableRow ssYaccTableRow1 = this.m_table.lookupRow(((SSYaccStackElement) this.m_stack.elementAt(index)).state());
        if (ssYaccTableRow1.hasSync() || ssYaccTableRow1.hasSyncAll())
        {
          for (int q_index = 0; q_index < ssYaccTableRow1.action(); ++q_index)
          {
            SSYaccTableRowEntry yaccTableRowEntry = ssYaccTableRow1.lookupEntry(q_index);
            if (ssYaccTableRow1.hasSyncAll() || yaccTableRowEntry.hasSync())
            {
              int num = yaccTableRowEntry.token();
              ssYaccSet.add((object) num);
            }
          }
        }
        if (ssYaccTableRow1.hasError())
        {
          SSYaccTableRow ssYaccTableRow2 = this.m_table.lookupRow(ssYaccTableRow1.lookupError().entry());
          for (int q_index = 0; q_index < ssYaccTableRow2.action(); ++q_index)
          {
            int num = ssYaccTableRow2.lookupEntry(q_index).token();
            ssYaccSet.add((object) num);
          }
        }
      }
      if (ssYaccSet.Count == 0)
        return true;
      while (!ssYaccSet.locate(this.m_lookahead.token()))
      {
        if (this.doGetLexeme(false))
          return true;
      }
      SSYaccTableRow ssYaccTableRow;
      while (true)
      {
        ssYaccTableRow = this.m_table.lookupRow(this.m_state);
        if (ssYaccTableRow.hasError())
        {
          this.lookupAction(ssYaccTableRow.lookupError().entry(), this.m_lookahead.token());
          if (this.m_action != 1)
            break;
        }
        if (ssYaccTableRow.hasSyncAll())
        {
          this.lookupAction(this.m_state, this.m_lookahead.token());
          if (this.m_action != 1)
            goto label_26;
        }
        else if (ssYaccTableRow.hasSync() && ssYaccTableRow.lookupAction(this.m_lookahead.token()) != null)
          goto label_24;
        this.pop(1);
      }
      SSLexLexeme q_lexeme = new SSLexLexeme("%error", -2);
      this.m_element = this.stackElement();
      this.m_element.setLexeme(q_lexeme);
      this.m_element.setState(ssYaccTableRow.lookupError().entry());
      this.push();
      goto label_26;
label_24:
      this.lookupAction(this.m_state, this.m_lookahead.token());
label_26:
      return false;
    }

    public void lookupAction(int q_state, int q_token)
    {
      SSYaccTableRowEntry yaccTableRowEntry = this.m_table.lookupRow(q_state).lookupAction(q_token);
      if (yaccTableRowEntry == null)
      {
        this.m_action = 1;
      }
      else
      {
        switch (this.m_action = yaccTableRowEntry.action())
        {
          case 0:
            this.m_state = yaccTableRowEntry.entry();
            break;
          case 2:
            SSYaccTableProd ssYaccTableProd = this.m_table.lookupProd(yaccTableRowEntry.entry());
            this.m_production = yaccTableRowEntry.entry();
            this.m_leftside = ssYaccTableProd.leftside();
            this.m_productionSize = ssYaccTableProd.size();
            break;
          case 4:
            this.m_lexSubtable = this.m_table.larTable(yaccTableRowEntry.entry());
            break;
        }
      }
    }

    public bool lookupGoto(int q_state, int q_token)
    {
      SSYaccTableRowEntry yaccTableRowEntry = this.m_table.lookupRow(q_state).lookupGoto(q_token);
      if (yaccTableRowEntry == null)
        return true;
      this.m_state = yaccTableRowEntry.entry();
      return false;
    }

    public bool push()
    {
      this.m_stack.push((object) this.m_element);
      return true;
    }

    public bool push(SSYaccStackElement q_element)
    {
      this.m_stack.push((object) q_element);
      return true;
    }

    public bool pop(int q_pop)
    {
      for (int index = 0; index < q_pop; ++index)
        this.m_stack.pop();
      this.m_state = ((SSYaccStackElement) this.m_stack.peek()).state();
      return false;
    }

    public SSYaccStackElement elementFromProduction(int q_index)
    {
      int index = this.m_stack.getSize() - this.m_productionSize + q_index;
      return index < 0 || index >= this.m_stack.getSize() ? (SSYaccStackElement) null : (SSYaccStackElement) this.m_stack.elementAt(index);
    }

    public void setAbort() => this.m_abort = true;

    public bool wasAborted() => this.m_abort;

    public bool wasError() => this.m_error;

    public SSYaccStackElement addSubTree()
    {
      SSYaccStackElement yaccStackElement = this.stackElement();
      yaccStackElement.createSubTree(this.m_productionSize);
      for (int q_index = 0; q_index < this.m_productionSize; ++q_index)
        yaccStackElement.addSubTree(q_index, this.elementFromProduction(q_index));
      return yaccStackElement;
    }

    public SSYaccStackElement treeRoot() => this.m_treeRoot;
  }
}
