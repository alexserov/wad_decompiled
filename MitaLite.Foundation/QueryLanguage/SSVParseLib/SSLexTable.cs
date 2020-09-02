// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexTable
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.IO;

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSLexTable {
        public const int SSLexStateInvalid = -1;
        public SSLexCharacterClass[] m_charClassTables;
        public int m_classes;
        public int m_classMax;
        public int m_classMin;
        public SSLexKeyTable[] m_keyTables;
        public Stack<object> m_stack;
        public SSLexSubtable[] m_subTables;
        public int SSLexDfaClassTableEntryHeaderSize = 8;
        public int SSLexDfaClassTableHeaderSize = 12;
        public int SSLexDfaKeywordTableHeaderSize = 40;
        public int SSLexDfaTableHeaderSize = 40;
        public int SSLexTableHeaderSize = 36;

        public SSLexTable() {
            this.m_stack = new Stack<object>();
        }

        public SSLexTable(string q_file) {
            this.m_classMin = int.MaxValue;
            this.m_classMax = -134217726;
            this.m_stack = new Stack<object>();
            var fileStream = File.Open(path: q_file, mode: FileMode.Open, access: FileAccess.Read, share: FileShare.Read);
            var numArray1 = new byte[512];
            var num1 = fileStream.Read(array: numArray1, offset: 0, count: this.SSLexTableHeaderSize);
            var ssLexTableHeader = new SSLexTableHeader();
            ssLexTableHeader.size = convertInt(b: numArray1, offset: 0);
            ssLexTableHeader.type = convertInt(b: numArray1, offset: 4);
            for (var index = 0; index < ssLexTableHeader.reserved.GetLength(dimension: 0); ++index)
                ssLexTableHeader.reserved[index] = convertInt(b: numArray1, offset: index * 4 + 8);
            this.m_subTables = new SSLexSubtable[ssLexTableHeader.size];
            for (var index1 = 0; index1 < ssLexTableHeader.size; ++index1) {
                var lexDfaTableHeader = new SSLexDfaTableHeader();
                num1 = fileStream.Read(array: numArray1, offset: 0, count: this.SSLexDfaTableHeaderSize);
                lexDfaTableHeader.type = convertInt(b: numArray1, offset: 0);
                lexDfaTableHeader.size = convertInt(b: numArray1, offset: 4);
                for (var index2 = 0; index2 < lexDfaTableHeader.reserved.GetLength(dimension: 0); ++index2)
                    lexDfaTableHeader.reserved[index2] = convertInt(b: numArray1, offset: index2 * 4 + 8);
                var numArray2 = new byte[lexDfaTableHeader.size];
                fileStream.Read(array: numArray2, offset: 0, count: lexDfaTableHeader.size - this.SSLexDfaTableHeaderSize);
                var q_numRows = convertInt(b: numArray2, offset: 0);
                convertInt(b: numArray2, offset: 4);
                var num2 = convertInt(b: numArray2, offset: 8);
                var num3 = convertInt(b: numArray2, offset: 12);
                var num4 = num2 - this.SSLexDfaTableHeaderSize;
                var num5 = num3 - this.SSLexDfaTableHeaderSize;
                var q_final = new int[q_numRows * 3];
                var length = 0;
                var offset1 = num4;
                for (var index2 = 0; index2 < q_numRows; ++index2) {
                    var offset2 = convertInt(b: numArray2, offset: offset1) - this.SSLexDfaTableHeaderSize;
                    length += convertInt(b: numArray2, offset: offset2) * 3 + 1;
                    offset1 += 4;
                }

                var q_rows = new int[length];
                var offset3 = num4;
                var num6 = 0;
                for (var index2 = 0; index2 < q_numRows; ++index2) {
                    var offset2 = convertInt(b: numArray2, offset: offset3) - this.SSLexDfaTableHeaderSize;
                    var num7 = convertInt(b: numArray2, offset: offset2);
                    var offset4 = offset2 + 4;
                    q_rows[num6++] = num7;
                    for (var index3 = 0; index3 < num7; ++index3) {
                        var numArray3 = q_rows;
                        var index4 = num6;
                        var num8 = index4 + 1;
                        var num9 = convertInt(b: numArray2, offset: offset4);
                        numArray3[index4] = num9;
                        var offset5 = offset4 + 4;
                        var numArray4 = q_rows;
                        var index5 = num8;
                        var num10 = index5 + 1;
                        var num11 = convertInt(b: numArray2, offset: offset5);
                        numArray4[index5] = num11;
                        var offset6 = offset5 + 4;
                        var numArray5 = q_rows;
                        var index6 = num10;
                        num6 = index6 + 1;
                        var num12 = convertInt(b: numArray2, offset: offset6);
                        numArray5[index6] = num12;
                        offset4 = offset6 + 4;
                    }

                    offset3 += 4;
                }

                var num13 = num5;
                var num14 = 0;
                for (var index2 = 0; index2 < q_numRows; ++index2) {
                    var offset2 = num13;
                    var numArray3 = q_final;
                    var index3 = num14;
                    var num7 = index3 + 1;
                    var num8 = convertInt(b: numArray2, offset: offset2);
                    numArray3[index3] = num8;
                    var offset4 = offset2 + 4;
                    var numArray4 = q_final;
                    var index4 = num7;
                    var num9 = index4 + 1;
                    var num10 = convertInt(b: numArray2, offset: offset4);
                    numArray4[index4] = num10;
                    var offset5 = offset4 + 4;
                    var numArray5 = q_final;
                    var index5 = num9;
                    num14 = index5 + 1;
                    var num11 = convertInt(b: numArray2, offset: offset5);
                    numArray5[index5] = num11;
                    num13 += 28;
                }

                this.m_subTables[index1] = new SSLexSubtable(q_numRows: q_numRows, q_rows: q_rows, q_final: q_final);
            }

            var length1 = ssLexTableHeader.reserved[0];
            if (length1 > 0)
                this.m_keyTables = new SSLexKeyTable[length1];
            for (var index1 = 0; index1 < length1; ++index1) {
                num1 = fileStream.Read(array: numArray1, offset: 0, count: this.SSLexDfaKeywordTableHeaderSize);
                var length2 = convertInt(b: numArray1, offset: 0);
                var length3 = convertInt(b: numArray1, offset: 4);
                var numArray2 = new byte[length2];
                num1 = fileStream.Read(array: numArray2, offset: 0, count: length2 - this.SSLexDfaKeywordTableHeaderSize);
                var offset = 40;
                var q_index = new int[length3 * 3];
                var q_keys = new string[length3];
                for (var index2 = 0; index2 < length3; ++index2) {
                    var index3 = 3 * index2;
                    q_index[index3] = convertInt(b: numArray2, offset: offset);
                    q_index[index3 + 1] = numArray2[offset + 4] != (byte) 0 ? 1 : 0;
                    q_index[index3 + 2] = convertInt(b: numArray2, offset: offset + 13);
                    var startIndex = convertInt(b: numArray2, offset: offset + 5) - this.SSLexDfaKeywordTableHeaderSize;
                    var length4 = 0;
                    for (var index4 = startIndex; numArray2[index4] != (byte) 0; ++index4)
                        ++length4;
                    var chArray = new char[length4];
                    for (index1 = 0; index1 < length4; ++index1)
                        chArray[index1] = (char) numArray2[index1];
                    q_keys[index2] = new string(value: chArray, startIndex: startIndex, length: length4);
                    offset += 41;
                }

                this.m_keyTables[index1] = new SSLexKeyTable(q_index: q_index, q_keys: q_keys);
            }

            this.m_classes = ssLexTableHeader.reserved[1];
            if (this.m_classes != 0)
                this.m_charClassTables = new SSLexCharacterClass[this.m_classes];
            for (var index1 = 0; index1 < this.m_classes; ++index1) {
                num1 = fileStream.Read(array: numArray1, offset: 0, count: this.SSLexDfaClassTableHeaderSize);
                var q_min = convertInt(b: numArray1, offset: 0);
                var q_max = convertInt(b: numArray1, offset: 4);
                var q_size = convertInt(b: numArray1, offset: 8);
                var count = q_size * this.SSLexDfaClassTableEntryHeaderSize;
                var length2 = q_size * 2;
                var numArray2 = new byte[count];
                num1 = fileStream.Read(array: numArray2, offset: 0, count: count);
                var q_array = new int[length2];
                for (var index2 = 0; index2 < length2; ++index2)
                    q_array[index2] = convertInt(b: numArray2, offset: index2 * 4);
                this.m_charClassTables[index1] = new SSLexCharacterClass(q_size: q_size, q_min: q_min, q_max: q_max, q_array: q_array);
                if (q_min < this.m_classMin)
                    this.m_classMin = q_min;
                if (q_min > this.m_classMax)
                    this.m_classMax = q_min;
                if (q_max < this.m_classMin)
                    this.m_classMax = q_max;
                if (q_max > this.m_classMax)
                    this.m_classMax = q_max;
            }

            pushSubtable(q_index: 0);
        }

        public bool translateClass(char[] q_char) {
            var ch = q_char[0];
            if (ch < this.m_classMin || ch > this.m_classMax)
                return false;
            for (var index = 0; index < this.m_classes; ++index)
                if (this.m_charClassTables[index].translate(q_char: q_char))
                    return true;
            return false;
        }

        public int convertInt(byte[] b, int offset) {
            var num1 = (b[offset + 3] << 24) & 4278190080L;
            long num2 = (b[offset + 2] << 16) & 16711680;
            long num3 = (b[offset + 1] << 8) & 65280;
            long num4 = b[offset];
            var num5 = num2;
            return (int) ((num1 | num5 | num3 | num4) & uint.MaxValue);
        }

        public void findKeyword(SSLexLexeme z_lexeme) {
            var num1 = 0;
            var num2 = this.m_keyTables[0].m_keys.Length;
            while (num2 > num1) {
                var index = num1 + (num2 - num1) / 2;
                var key = this.m_keyTables[0].m_keys[index];
                var str = new string(value: z_lexeme.lexeme());
                var num3 = this.m_keyTables[0].m_index[index * 3 + 1] != 1 ? str.CompareTo(strB: key) : str.ToLower().CompareTo(strB: key);
                if (num3 < 0) {
                    if (num1 == index)
                        break;
                    num2 = index;
                } else {
                    if (num3 == 0) {
                        z_lexeme.setToken(q_token: this.m_keyTables[0].m_index[index * 3]);
                        break;
                    }

                    if (num2 == index + 1)
                        break;
                    num1 = index;
                }
            }
        }

        public void gotoSubtable(int q_index) {
            this.m_stack.Pop();
            this.m_stack.Push(item: this.m_subTables[q_index]);
        }

        public void pushSubtable(int q_index) {
            this.m_stack.Push(item: this.m_subTables[q_index]);
        }

        public void popSubtable() {
            this.m_stack.Pop();
        }

        public int lookup(int q_state, int q_next) {
            return ((SSLexSubtable) this.m_stack.Peek()).lookup(q_state: q_state, q_next: q_next);
        }

        public SSLexFinalState lookupFinal(int q_state) {
            return ((SSLexSubtable) this.m_stack.Peek()).lookupFinal(q_state: q_state);
        }
    }
}