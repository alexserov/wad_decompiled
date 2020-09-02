// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYacc
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSYacc {
        public const int SSYaccActionShift = 0;
        public const int SSYaccActionError = 1;
        public const int SSYaccActionReduce = 2;
        public const int SSYaccActionAccept = 3;
        public const int SSYaccActionConflict = 4;
        public const int m_eofToken = -1;
        public const int m_errorToken = -2;
        public const int SSYaccLexemeCacheMax = -1;
        public bool m_abort;
        public int m_action;
        public int m_cache;
        public SSYaccStackElement m_element;
        public SSLexLexeme m_endLexeme;
        public bool m_endOfInput;
        public bool m_error;
        public SSLexLexeme m_larLookahead;
        public int m_leftside;
        public SSLex m_lex;
        public SSYaccCache m_lexemeCache;
        public SSLexSubtable m_lexSubtable;
        public SSLexLexeme m_lookahead;
        public int m_production;
        public int m_productionSize;
        public SSYaccStack m_stack;
        public int m_state;
        public SSYaccTable m_table;
        public SSYaccStackElement m_treeRoot;

        public SSYacc(SSYaccTable q_table, SSLex q_lex) {
            this.m_cache = 0;
            this.m_lex = q_lex;
            this.m_abort = false;
            this.m_error = false;
            this.m_table = q_table;
            this.m_endOfInput = false;
            this.m_endLexeme = new SSLexLexeme(q_lexeme: "eof", q_token: -1);
            this.m_endLexeme.setToken(q_token: -1);
            this.m_stack = new SSYaccStack(q_size: 5, q_inc: 5);
            this.m_lexemeCache = new SSYaccCache();
            this.m_element = stackElement();
            push();
        }

        public virtual SSYaccStackElement reduce(int q_prod, int q_length) {
            return stackElement();
        }

        public virtual SSLexLexeme nextLexeme() {
            return this.m_lex.next();
        }

        public virtual SSYaccStackElement stackElement() {
            return new SSYaccStackElement();
        }

        public virtual SSYaccStackElement shift(SSLexLexeme q_lexeme) {
            return stackElement();
        }

        public bool larLookahead(SSLexLexeme q_lex) {
            return false;
        }

        public virtual bool error(int q_state, SSLexLexeme q_look) {
            return syncErr();
        }

        public bool larError(int q_state, SSLexLexeme q_look, SSLexLexeme q_larLook) {
            return error(q_state: q_state, q_look: q_look);
        }

        public bool parse() {
            if (doGetLexeme(q_look: true))
                return true;
            while (!this.m_abort)
                switch (this.m_action) {
                    case 0:
                        if (doShift())
                            return true;
                        continue;
                    case 1:
                        if (doError())
                            return true;
                        continue;
                    case 2:
                        if (doReduce())
                            return true;
                        continue;
                    case 3:
                        this.m_treeRoot = this.m_element;
                        return this.m_error;
                    case 4:
                        if (doConflict())
                            return true;
                        continue;
                    default:
                        return true;
                }

            return true;
        }

        public bool doShift() {
            this.m_element = shift(q_lexeme: this.m_lookahead);
            if (this.m_element == null)
                return true;
            this.m_element.setLexeme(q_lexeme: this.m_lookahead);
            this.m_element.setState(q_state: this.m_state);
            push();
            return doGetLexeme(q_look: true);
        }

        public bool doReduce() {
            this.m_element = reduce(q_prod: this.m_production, q_length: this.m_productionSize);
            if (this.m_element == null)
                return true;
            pop(q_pop: this.m_productionSize);
            return goTo(q_goto: this.m_leftside);
        }

        public bool doError() {
            this.m_error = true;
            return error(q_state: this.m_state, q_look: this.m_lookahead);
        }

        public bool doLarError() {
            this.m_error = true;
            return larError(q_state: this.m_state, q_look: this.m_lookahead, q_larLook: this.m_larLookahead);
        }

        public SSLexLexeme getLexemeCache() {
            SSLexLexeme ssLexLexeme = null;
            if (this.m_cache != -1 && this.m_lexemeCache.hasElements())
                ssLexLexeme = (SSLexLexeme) this.m_lexemeCache.Dequeue();
            if (ssLexLexeme == null) {
                this.m_cache = -1;
                ssLexLexeme = nextLexeme() ?? this.m_endLexeme;
                this.m_lexemeCache.Enqueue(item: ssLexLexeme);
            }

            return ssLexLexeme;
        }

        public bool doConflict() {
            this.m_cache = 0;
            var q_state = this.m_lexSubtable.lookup(q_state: 0, q_next: this.m_lookahead.token());
            while ((this.m_larLookahead = getLexemeCache()) != null) {
                q_state = this.m_lexSubtable.lookup(q_state: q_state, q_next: this.m_larLookahead.token());
                if (q_state != -1) {
                    var ssLexFinalState = this.m_lexSubtable.lookupFinal(q_state: q_state);
                    if (ssLexFinalState.isFinal()) {
                        if (ssLexFinalState.isReduce()) {
                            this.m_production = ssLexFinalState.token();
                            var ssYaccTableProd = this.m_table.lookupProd(q_index: this.m_production);
                            this.m_leftside = ssYaccTableProd.leftside();
                            this.m_productionSize = ssYaccTableProd.size();
                            return doReduce();
                        }

                        this.m_state = ssLexFinalState.token();
                        return doShift();
                    }
                } else {
                    break;
                }
            }

            return doLarError();
        }

        public bool doGetLexeme(bool q_look) {
            if ((this.m_lookahead = this.m_lexemeCache.remove()) == null)
                return getLexeme(q_look: q_look);
            if (larLookahead(q_lex: this.m_lookahead))
                return true;
            if (q_look)
                lookupAction(q_state: this.m_state, q_token: this.m_lookahead.token());
            return false;
        }

        public bool getLexeme(bool q_look) {
            if (this.m_endOfInput)
                return true;
            this.m_lookahead = nextLexeme();
            if (this.m_lookahead == null) {
                this.m_endOfInput = true;
                this.m_lookahead = this.m_endLexeme;
            }

            if (q_look)
                lookupAction(q_state: this.m_state, q_token: this.m_lookahead.token());
            return false;
        }

        public bool goTo(int q_goto) {
            if (lookupGoto(q_state: this.m_state, q_token: this.m_leftside))
                return true;
            this.m_element.setState(q_state: this.m_state);
            push();
            lookupAction(q_state: this.m_state, q_token: this.m_lookahead.token());
            return false;
        }

        public bool syncErr() {
            var ssYaccSet = new SSYaccSet();
            for (var index = 0; index < this.m_stack.getSize(); ++index) {
                var ssYaccTableRow1 = this.m_table.lookupRow(q_state: ((SSYaccStackElement) this.m_stack.elementAt(index: index)).state());
                if (ssYaccTableRow1.hasSync() || ssYaccTableRow1.hasSyncAll())
                    for (var q_index = 0; q_index < ssYaccTableRow1.action(); ++q_index) {
                        var yaccTableRowEntry = ssYaccTableRow1.lookupEntry(q_index: q_index);
                        if (ssYaccTableRow1.hasSyncAll() || yaccTableRowEntry.hasSync()) {
                            var num = yaccTableRowEntry.token();
                            ssYaccSet.add(q_object: num);
                        }
                    }

                if (ssYaccTableRow1.hasError()) {
                    var ssYaccTableRow2 = this.m_table.lookupRow(q_state: ssYaccTableRow1.lookupError().entry());
                    for (var q_index = 0; q_index < ssYaccTableRow2.action(); ++q_index) {
                        var num = ssYaccTableRow2.lookupEntry(q_index: q_index).token();
                        ssYaccSet.add(q_object: num);
                    }
                }
            }

            if (ssYaccSet.Count == 0)
                return true;
            while (!ssYaccSet.locate(q_locate: this.m_lookahead.token()))
                if (doGetLexeme(q_look: false))
                    return true;
            SSYaccTableRow ssYaccTableRow;
            while (true) {
                ssYaccTableRow = this.m_table.lookupRow(q_state: this.m_state);
                if (ssYaccTableRow.hasError()) {
                    lookupAction(q_state: ssYaccTableRow.lookupError().entry(), q_token: this.m_lookahead.token());
                    if (this.m_action != 1)
                        break;
                }

                if (ssYaccTableRow.hasSyncAll()) {
                    lookupAction(q_state: this.m_state, q_token: this.m_lookahead.token());
                    if (this.m_action != 1)
                        goto label_26;
                } else if (ssYaccTableRow.hasSync() && ssYaccTableRow.lookupAction(q_index: this.m_lookahead.token()) != null) {
                    goto label_24;
                }

                pop(q_pop: 1);
            }

            var q_lexeme = new SSLexLexeme(q_lexeme: "%error", q_token: -2);
            this.m_element = stackElement();
            this.m_element.setLexeme(q_lexeme: q_lexeme);
            this.m_element.setState(q_state: ssYaccTableRow.lookupError().entry());
            push();
            goto label_26;
            label_24:
            lookupAction(q_state: this.m_state, q_token: this.m_lookahead.token());
            label_26:
            return false;
        }

        public void lookupAction(int q_state, int q_token) {
            var yaccTableRowEntry = this.m_table.lookupRow(q_state: q_state).lookupAction(q_index: q_token);
            if (yaccTableRowEntry == null)
                this.m_action = 1;
            else
                switch (this.m_action = yaccTableRowEntry.action()) {
                    case 0:
                        this.m_state = yaccTableRowEntry.entry();
                        break;
                    case 2:
                        var ssYaccTableProd = this.m_table.lookupProd(q_index: yaccTableRowEntry.entry());
                        this.m_production = yaccTableRowEntry.entry();
                        this.m_leftside = ssYaccTableProd.leftside();
                        this.m_productionSize = ssYaccTableProd.size();
                        break;
                    case 4:
                        this.m_lexSubtable = this.m_table.larTable(q_entry: yaccTableRowEntry.entry());
                        break;
                }
        }

        public bool lookupGoto(int q_state, int q_token) {
            var yaccTableRowEntry = this.m_table.lookupRow(q_state: q_state).lookupGoto(q_index: q_token);
            if (yaccTableRowEntry == null)
                return true;
            this.m_state = yaccTableRowEntry.entry();
            return false;
        }

        public bool push() {
            this.m_stack.push(q_ele: this.m_element);
            return true;
        }

        public bool push(SSYaccStackElement q_element) {
            this.m_stack.push(q_ele: q_element);
            return true;
        }

        public bool pop(int q_pop) {
            for (var index = 0; index < q_pop; ++index)
                this.m_stack.pop();
            this.m_state = ((SSYaccStackElement) this.m_stack.peek()).state();
            return false;
        }

        public SSYaccStackElement elementFromProduction(int q_index) {
            var index = this.m_stack.getSize() - this.m_productionSize + q_index;
            return index < 0 || index >= this.m_stack.getSize() ? null : (SSYaccStackElement) this.m_stack.elementAt(index: index);
        }

        public void setAbort() {
            this.m_abort = true;
        }

        public bool wasAborted() {
            return this.m_abort;
        }

        public bool wasError() {
            return this.m_error;
        }

        public SSYaccStackElement addSubTree() {
            var yaccStackElement = stackElement();
            yaccStackElement.createSubTree(q_size: this.m_productionSize);
            for (var q_index = 0; q_index < this.m_productionSize; ++q_index)
                yaccStackElement.addSubTree(q_index: q_index, q_ele: elementFromProduction(q_index: q_index));
            return yaccStackElement;
        }

        public SSYaccStackElement treeRoot() {
            return this.m_treeRoot;
        }
    }
}