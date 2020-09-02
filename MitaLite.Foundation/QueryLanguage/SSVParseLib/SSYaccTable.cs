// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYaccTable
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.IO;

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSYaccTable {
        public SSYaccTableProd[] m_prods;
        public SSYaccTableRow[] m_rows;
        public int SSYaccTableEntrySize = 8;
        public int SSYaccTableHeaderSize = 20;
        public int SSYaccTableRowSize = 12;

        public SSYaccTable() {
        }

        public SSYaccTable(string q_file) {
            var fileStream = File.Open(path: q_file, mode: FileMode.Open, access: FileAccess.Read, share: FileShare.Read);
            var length1 = (int) fileStream.Length;
            var numArray = new byte[length1];
            fileStream.Read(array: numArray, offset: 0, count: length1);
            var ssYaccTableHeader = new SSYaccTableHeader();
            ssYaccTableHeader.type = convertInt(b: numArray, offset: 0);
            ssYaccTableHeader.prodOrLar = convertInt(b: numArray, offset: 4);
            ssYaccTableHeader.numRows = convertInt(b: numArray, offset: 8);
            ssYaccTableHeader.rowOffset = convertInt(b: numArray, offset: 12);
            ssYaccTableHeader.prodOffset = convertInt(b: numArray, offset: 16);
            var length2 = ssYaccTableHeader.numProds();
            this.m_prods = new SSYaccTableProd[length2];
            var prodOffset = ssYaccTableHeader.prodOffset;
            for (var index = 0; index < length2; ++index) {
                var q_size = convertInt(b: numArray, offset: prodOffset);
                var q_leftside = convertInt(b: numArray, offset: prodOffset + 4);
                this.m_prods[index] = new SSYaccTableProd(q_size: q_size, q_leftside: q_leftside);
                prodOffset += 8;
            }

            this.m_rows = new SSYaccTableRow[ssYaccTableHeader.numRows];
            var rowOffset = ssYaccTableHeader.rowOffset;
            for (var index = 0; index < ssYaccTableHeader.numRows; ++index) {
                var q_flags = convertInt(b: numArray, offset: rowOffset);
                var q_goto = convertInt(b: numArray, offset: rowOffset + 4);
                var q_action = convertInt(b: numArray, offset: rowOffset + 8);
                this.m_rows[index] = new SSYaccTableRow(q_flags: q_flags, q_goto: q_goto, q_action: q_action, q_b: numArray, q_index: rowOffset + 12);
                rowOffset += this.SSYaccTableRowSize + this.SSYaccTableEntrySize * this.m_rows[index].numEntries();
            }
        }

        public int convertInt(byte[] b, int offset) {
            var num1 = (b[offset + 3] << 24) & 4278190080L;
            long num2 = (b[offset + 2] << 16) & 16711680;
            long num3 = (b[offset + 1] << 8) & 65280;
            long num4 = b[offset];
            var num5 = num2;
            return (int) ((num1 | num5 | num3 | num4) & uint.MaxValue);
        }

        public SSYaccTableRow lookupRow(int q_state) {
            return this.m_rows[q_state];
        }

        public SSYaccTableProd lookupProd(int q_index) {
            return this.m_prods[q_index];
        }

        public SSLexSubtable larTable(int q_entry) {
            throw new NotImplementedException(message: "This should never be thrown.  Contact mitadev if it is.");
        }
    }
}