// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYaccTable
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.IO;

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSYaccTable
  {
    public int SSYaccTableHeaderSize = 20;
    public int SSYaccTableEntrySize = 8;
    public int SSYaccTableRowSize = 12;
    public SSYaccTableRow[] m_rows;
    public SSYaccTableProd[] m_prods;

    public SSYaccTable()
    {
    }

    public SSYaccTable(string q_file)
    {
      FileStream fileStream = File.Open(q_file, FileMode.Open, FileAccess.Read, FileShare.Read);
      int length1 = (int) fileStream.Length;
      byte[] numArray = new byte[length1];
      fileStream.Read(numArray, 0, length1);
      SSYaccTableHeader ssYaccTableHeader = new SSYaccTableHeader();
      ssYaccTableHeader.type = this.convertInt(numArray, 0);
      ssYaccTableHeader.prodOrLar = this.convertInt(numArray, 4);
      ssYaccTableHeader.numRows = this.convertInt(numArray, 8);
      ssYaccTableHeader.rowOffset = this.convertInt(numArray, 12);
      ssYaccTableHeader.prodOffset = this.convertInt(numArray, 16);
      int length2 = ssYaccTableHeader.numProds();
      this.m_prods = new SSYaccTableProd[length2];
      int prodOffset = ssYaccTableHeader.prodOffset;
      for (int index = 0; index < length2; ++index)
      {
        int q_size = this.convertInt(numArray, prodOffset);
        int q_leftside = this.convertInt(numArray, prodOffset + 4);
        this.m_prods[index] = new SSYaccTableProd(q_size, q_leftside);
        prodOffset += 8;
      }
      this.m_rows = new SSYaccTableRow[ssYaccTableHeader.numRows];
      int rowOffset = ssYaccTableHeader.rowOffset;
      for (int index = 0; index < ssYaccTableHeader.numRows; ++index)
      {
        int q_flags = this.convertInt(numArray, rowOffset);
        int q_goto = this.convertInt(numArray, rowOffset + 4);
        int q_action = this.convertInt(numArray, rowOffset + 8);
        this.m_rows[index] = new SSYaccTableRow(q_flags, q_goto, q_action, numArray, rowOffset + 12);
        rowOffset += this.SSYaccTableRowSize + this.SSYaccTableEntrySize * this.m_rows[index].numEntries();
      }
    }

    public int convertInt(byte[] b, int offset)
    {
      long num1 = (long) ((int) b[offset + 3] << 24) & 4278190080L;
      long num2 = (long) ((int) b[offset + 2] << 16 & 16711680);
      long num3 = (long) ((int) b[offset + 1] << 8 & 65280);
      long num4 = (long) b[offset];
      long num5 = num2;
      return (int) ((num1 | num5 | num3 | num4) & (long) uint.MaxValue);
    }

    public SSYaccTableRow lookupRow(int q_state) => this.m_rows[q_state];

    public SSYaccTableProd lookupProd(int q_index) => this.m_prods[q_index];

    public SSLexSubtable larTable(int q_entry) => throw new NotImplementedException("This should never be thrown.  Contact mitadev if it is.");
  }
}
