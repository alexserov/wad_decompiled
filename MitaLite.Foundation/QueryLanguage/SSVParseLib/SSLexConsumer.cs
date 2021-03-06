﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexConsumer
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal abstract class SSLexConsumer {
        public char m_bof;
        public char[] m_buffer = new char[4096];
        public int m_bufferIndex;
        public int m_consumed;
        public char m_current;
        public bool m_endOfData;
        public int m_index;
        public int m_length;
        public int m_line;
        public int m_offset;
        public int m_scanLine;
        public int m_scanOffset;
        public int m_start;

        public abstract bool getNext();

        public bool next() {
            if (this.m_endOfData)
                return false;
            if ((this.m_current = this.m_bof) != char.MinValue) {
                this.m_bof = char.MinValue;
                return true;
            }

            if (this.m_bufferIndex >= this.m_buffer.Length) {
                if (!getNext()) {
                    ++this.m_index;
                    ++this.m_bufferIndex;
                    this.m_endOfData = true;
                    return false;
                }

                ++this.m_index;
                ++this.m_bufferIndex;
                var chArray = new char[this.m_buffer.Length + 4096];
                this.m_buffer.CopyTo(array: chArray, index: 0);
                this.m_buffer = chArray;
                this.m_buffer[this.m_bufferIndex] = this.m_current;
            } else {
                if (!getNext()) {
                    ++this.m_index;
                    ++this.m_bufferIndex;
                    this.m_endOfData = true;
                    return false;
                }

                ++this.m_index;
                this.m_buffer[this.m_bufferIndex++] = this.m_current;
            }

            if (this.m_current == '\n') {
                ++this.m_scanLine;
                this.m_scanOffset = 1;
            } else {
                ++this.m_scanOffset;
            }

            return true;
        }

        public int line() {
            return this.m_line;
        }

        public int offset() {
            return this.m_offset;
        }

        public char getCurrent() {
            return this.m_current;
        }

        public int absoluteOffset() {
            return this.m_start;
        }

        public SSLexMark mark() {
            return new SSLexMark(q_line: this.m_scanLine, q_offset: this.m_scanOffset, q_index: this.m_index);
        }

        public void flushEndOfLine(SSLexMark q_mark) {
            --q_mark.m_line;
            --q_mark.m_index;
        }

        public void flushStartOfLine(SSLexMark q_mark) {
            ++this.m_line;
            ++this.m_start;
            --q_mark.m_line;
            this.m_offset = 1;
        }

        public virtual void flushLexeme(SSLexMark q_mark) {
            this.m_start = this.m_index = q_mark.m_index;
            this.m_line += q_mark.m_line;
            this.m_offset = q_mark.m_offset;
            this.m_scanLine = 0;
            this.m_scanOffset = q_mark.m_offset;
            this.m_bufferIndex = 0;
        }

        public void flushLexeme() {
            this.m_start = this.m_index;
            this.m_line += this.m_scanLine;
            this.m_offset = this.m_scanOffset;
            this.m_scanLine = 0;
            this.m_bufferIndex = 0;
        }

        public int lexemeLength() {
            return this.m_index - this.m_start;
        }

        public int lexemeLength(SSLexMark q_mark) {
            return q_mark.index() - this.m_start;
        }

        public char[] lexemeBuffer() {
            var chArray = new char[lexemeLength()];
            for (var index = 0; index < this.m_bufferIndex - 1; ++index)
                chArray[index] = this.m_buffer[index];
            return chArray;
        }

        public char[] lexemeBuffer(SSLexMark q_mark) {
            var length = lexemeLength(q_mark: q_mark);
            var chArray = new char[length];
            for (var index = 0; index < length; ++index)
                chArray[index] = this.m_buffer[index];
            return chArray;
        }
    }
}