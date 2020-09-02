// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexTable
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;
using System.IO;

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSLexTable
  {
    public int SSLexTableHeaderSize = 36;
    public int SSLexDfaTableHeaderSize = 40;
    public int SSLexDfaKeywordTableHeaderSize = 40;
    public int SSLexDfaClassTableHeaderSize = 12;
    public int SSLexDfaClassTableEntryHeaderSize = 8;
    public Stack<object> m_stack;
    public int m_classes;
    public int m_classMin;
    public int m_classMax;
    public SSLexSubtable[] m_subTables;
    public SSLexKeyTable[] m_keyTables;
    public SSLexCharacterClass[] m_charClassTables;
    public const int SSLexStateInvalid = -1;

    public SSLexTable() => this.m_stack = new Stack<object>();

    public SSLexTable(string q_file)
    {
      this.m_classMin = int.MaxValue;
      this.m_classMax = -134217726;
      this.m_stack = new Stack<object>();
      FileStream fileStream = File.Open(q_file, FileMode.Open, FileAccess.Read, FileShare.Read);
      byte[] numArray1 = new byte[512];
      int num1 = fileStream.Read(numArray1, 0, this.SSLexTableHeaderSize);
      SSLexTableHeader ssLexTableHeader = new SSLexTableHeader();
      ssLexTableHeader.size = this.convertInt(numArray1, 0);
      ssLexTableHeader.type = this.convertInt(numArray1, 4);
      for (int index = 0; index < ssLexTableHeader.reserved.GetLength(0); ++index)
        ssLexTableHeader.reserved[index] = this.convertInt(numArray1, index * 4 + 8);
      this.m_subTables = new SSLexSubtable[ssLexTableHeader.size];
      for (int index1 = 0; index1 < ssLexTableHeader.size; ++index1)
      {
        SSLexDfaTableHeader lexDfaTableHeader = new SSLexDfaTableHeader();
        num1 = fileStream.Read(numArray1, 0, this.SSLexDfaTableHeaderSize);
        lexDfaTableHeader.type = this.convertInt(numArray1, 0);
        lexDfaTableHeader.size = this.convertInt(numArray1, 4);
        for (int index2 = 0; index2 < lexDfaTableHeader.reserved.GetLength(0); ++index2)
          lexDfaTableHeader.reserved[index2] = this.convertInt(numArray1, index2 * 4 + 8);
        byte[] numArray2 = new byte[lexDfaTableHeader.size];
        fileStream.Read(numArray2, 0, lexDfaTableHeader.size - this.SSLexDfaTableHeaderSize);
        int q_numRows = this.convertInt(numArray2, 0);
        this.convertInt(numArray2, 4);
        int num2 = this.convertInt(numArray2, 8);
        int num3 = this.convertInt(numArray2, 12);
        int num4 = num2 - this.SSLexDfaTableHeaderSize;
        int num5 = num3 - this.SSLexDfaTableHeaderSize;
        int[] q_final = new int[q_numRows * 3];
        int length = 0;
        int offset1 = num4;
        for (int index2 = 0; index2 < q_numRows; ++index2)
        {
          int offset2 = this.convertInt(numArray2, offset1) - this.SSLexDfaTableHeaderSize;
          length += this.convertInt(numArray2, offset2) * 3 + 1;
          offset1 += 4;
        }
        int[] q_rows = new int[length];
        int offset3 = num4;
        int num6 = 0;
        for (int index2 = 0; index2 < q_numRows; ++index2)
        {
          int offset2 = this.convertInt(numArray2, offset3) - this.SSLexDfaTableHeaderSize;
          int num7 = this.convertInt(numArray2, offset2);
          int offset4 = offset2 + 4;
          q_rows[num6++] = num7;
          for (int index3 = 0; index3 < num7; ++index3)
          {
            int[] numArray3 = q_rows;
            int index4 = num6;
            int num8 = index4 + 1;
            int num9 = this.convertInt(numArray2, offset4);
            numArray3[index4] = num9;
            int offset5 = offset4 + 4;
            int[] numArray4 = q_rows;
            int index5 = num8;
            int num10 = index5 + 1;
            int num11 = this.convertInt(numArray2, offset5);
            numArray4[index5] = num11;
            int offset6 = offset5 + 4;
            int[] numArray5 = q_rows;
            int index6 = num10;
            num6 = index6 + 1;
            int num12 = this.convertInt(numArray2, offset6);
            numArray5[index6] = num12;
            offset4 = offset6 + 4;
          }
          offset3 += 4;
        }
        int num13 = num5;
        int num14 = 0;
        for (int index2 = 0; index2 < q_numRows; ++index2)
        {
          int offset2 = num13;
          int[] numArray3 = q_final;
          int index3 = num14;
          int num7 = index3 + 1;
          int num8 = this.convertInt(numArray2, offset2);
          numArray3[index3] = num8;
          int offset4 = offset2 + 4;
          int[] numArray4 = q_final;
          int index4 = num7;
          int num9 = index4 + 1;
          int num10 = this.convertInt(numArray2, offset4);
          numArray4[index4] = num10;
          int offset5 = offset4 + 4;
          int[] numArray5 = q_final;
          int index5 = num9;
          num14 = index5 + 1;
          int num11 = this.convertInt(numArray2, offset5);
          numArray5[index5] = num11;
          num13 += 28;
        }
        this.m_subTables[index1] = new SSLexSubtable(q_numRows, q_rows, q_final);
      }
      int length1 = ssLexTableHeader.reserved[0];
      if (length1 > 0)
        this.m_keyTables = new SSLexKeyTable[length1];
      for (int index1 = 0; index1 < length1; ++index1)
      {
        num1 = fileStream.Read(numArray1, 0, this.SSLexDfaKeywordTableHeaderSize);
        int length2 = this.convertInt(numArray1, 0);
        int length3 = this.convertInt(numArray1, 4);
        byte[] numArray2 = new byte[length2];
        num1 = fileStream.Read(numArray2, 0, length2 - this.SSLexDfaKeywordTableHeaderSize);
        int offset = 40;
        int[] q_index = new int[length3 * 3];
        string[] q_keys = new string[length3];
        for (int index2 = 0; index2 < length3; ++index2)
        {
          int index3 = 3 * index2;
          q_index[index3] = this.convertInt(numArray2, offset);
          q_index[index3 + 1] = numArray2[offset + 4] != (byte) 0 ? 1 : 0;
          q_index[index3 + 2] = this.convertInt(numArray2, offset + 13);
          int startIndex = this.convertInt(numArray2, offset + 5) - this.SSLexDfaKeywordTableHeaderSize;
          int length4 = 0;
          for (int index4 = startIndex; numArray2[index4] != (byte) 0; ++index4)
            ++length4;
          char[] chArray = new char[length4];
          for (index1 = 0; index1 < length4; ++index1)
            chArray[index1] = (char) numArray2[index1];
          q_keys[index2] = new string(chArray, startIndex, length4);
          offset += 41;
        }
        this.m_keyTables[index1] = new SSLexKeyTable(q_index, q_keys);
      }
      this.m_classes = ssLexTableHeader.reserved[1];
      if (this.m_classes != 0)
        this.m_charClassTables = new SSLexCharacterClass[this.m_classes];
      for (int index1 = 0; index1 < this.m_classes; ++index1)
      {
        num1 = fileStream.Read(numArray1, 0, this.SSLexDfaClassTableHeaderSize);
        int q_min = this.convertInt(numArray1, 0);
        int q_max = this.convertInt(numArray1, 4);
        int q_size = this.convertInt(numArray1, 8);
        int count = q_size * this.SSLexDfaClassTableEntryHeaderSize;
        int length2 = q_size * 2;
        byte[] numArray2 = new byte[count];
        num1 = fileStream.Read(numArray2, 0, count);
        int[] q_array = new int[length2];
        for (int index2 = 0; index2 < length2; ++index2)
          q_array[index2] = this.convertInt(numArray2, index2 * 4);
        this.m_charClassTables[index1] = new SSLexCharacterClass(q_size, q_min, q_max, q_array);
        if (q_min < this.m_classMin)
          this.m_classMin = q_min;
        if (q_min > this.m_classMax)
          this.m_classMax = q_min;
        if (q_max < this.m_classMin)
          this.m_classMax = q_max;
        if (q_max > this.m_classMax)
          this.m_classMax = q_max;
      }
      this.pushSubtable(0);
    }

    public bool translateClass(char[] q_char)
    {
      char ch = q_char[0];
      if ((int) ch < this.m_classMin || (int) ch > this.m_classMax)
        return false;
      for (int index = 0; index < this.m_classes; ++index)
      {
        if (this.m_charClassTables[index].translate(q_char))
          return true;
      }
      return false;
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

    public void findKeyword(SSLexLexeme z_lexeme)
    {
      int num1 = 0;
      int num2 = this.m_keyTables[0].m_keys.Length;
      while (num2 > num1)
      {
        int index = num1 + (num2 - num1) / 2;
        string key = this.m_keyTables[0].m_keys[index];
        string str = new string(z_lexeme.lexeme());
        int num3 = this.m_keyTables[0].m_index[index * 3 + 1] != 1 ? str.CompareTo(key) : str.ToLower().CompareTo(key);
        if (num3 < 0)
        {
          if (num1 == index)
            break;
          num2 = index;
        }
        else
        {
          if (num3 == 0)
          {
            z_lexeme.setToken(this.m_keyTables[0].m_index[index * 3]);
            break;
          }
          if (num2 == index + 1)
            break;
          num1 = index;
        }
      }
    }

    public void gotoSubtable(int q_index)
    {
      this.m_stack.Pop();
      this.m_stack.Push((object) this.m_subTables[q_index]);
    }

    public void pushSubtable(int q_index) => this.m_stack.Push((object) this.m_subTables[q_index]);

    public void popSubtable() => this.m_stack.Pop();

    public int lookup(int q_state, int q_next) => ((SSLexSubtable) this.m_stack.Peek()).lookup(q_state, q_next);

    public SSLexFinalState lookupFinal(int q_state) => ((SSLexSubtable) this.m_stack.Peek()).lookupFinal(q_state);
  }
}
