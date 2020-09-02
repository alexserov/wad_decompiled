// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexFileConsumer
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.IO;

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSLexFileConsumer : SSLexConsumer {
        public FileStream m_fileStream;
        public bool m_first;
        public bool m_reversedUnicode;
        public bool m_unicode;

        public SSLexFileConsumer(string q_name, bool q_unicode) {
            this.m_first = true;
            this.m_unicode = q_unicode;
            this.m_reversedUnicode = true;
            this.m_fileStream = File.Open(path: q_name, mode: FileMode.Open, access: FileAccess.Read, share: FileShare.Read);
        }

        public bool readByte() {
            try {
                var num = this.m_fileStream.ReadByte();
                if (num == -1)
                    return false;
                this.m_current = (char) num;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public bool readUnicodeByte() {
            if (!readByte())
                return false;
            var current = this.m_current;
            if (!readByte())
                return false;
            if (this.m_reversedUnicode)
                this.m_current = (char) (((uint) this.m_current << 8) | current);
            else
                this.m_current = (char) (((uint) current << 8) | this.m_current);
            return true;
        }

        public override bool getNext() {
            if (this.m_first && this.m_unicode) {
                this.m_first = false;
                if (!readByte())
                    return false;
                if (this.m_current != 'ÿ' && this.m_current != 'þ')
                    return true;
                var current = this.m_current;
                if (!readByte())
                    return false;
                if (current == 'ÿ' && this.m_current == 'þ') {
                    this.m_reversedUnicode = true;
                    return readUnicodeByte();
                }

                if (current == 'þ' && this.m_current == 'ÿ') {
                    this.m_reversedUnicode = false;
                    return readUnicodeByte();
                }

                this.m_current = (char) (((uint) this.m_current << 8) | current);
                return true;
            }

            return this.m_unicode ? readUnicodeByte() : readByte();
        }

        public override void flushLexeme(SSLexMark q_mark) {
            this.m_start = this.m_index = q_mark.m_index;
            this.m_line += q_mark.m_line;
            this.m_offset = q_mark.m_offset;
            this.m_scanLine = 0;
            this.m_scanOffset = q_mark.m_offset;
            this.m_bufferIndex = 0;
            this.m_fileStream.Position = this.m_index;
        }
    }
}