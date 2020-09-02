// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexTableRow
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSLexTableRow
  {
    public int m_size;
    public SSLexTableRowEntry[] m_entries;

    public SSLexTableRow(int[] q_row, int q_index)
    {
      this.m_size = q_row[q_index];
      if (this.m_size > 0)
        this.m_entries = new SSLexTableRowEntry[this.m_size];
      ++q_index;
      for (int index = 0; index < this.m_size; ++index)
      {
        this.m_entries[index] = new SSLexTableRowEntry(q_row[q_index], q_row[q_index + 1], q_row[q_index + 2]);
        q_index += 3;
      }
    }

    public int lookup(int q_code)
    {
      for (int index = 0; index < this.m_size; ++index)
      {
        SSLexTableRowEntry entry = this.m_entries[index];
        if (q_code < entry.start())
          return -1;
        if (q_code <= entry.end())
          return entry.state();
      }
      return -1;
    }
  }
}
