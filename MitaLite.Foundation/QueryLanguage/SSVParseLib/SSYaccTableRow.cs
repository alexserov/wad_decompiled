// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYaccTableRow
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSYaccTableRow
  {
    public int SSYaccTableEntrySize = 8;
    public int SSYaccTableRowFlagSync = 1;
    public int SSYaccTableRowFlagError = 2;
    public int SSYaccTableRowFlagSyncAll = 4;
    public int m_goto;
    public int m_action;
    public bool m_sync;
    public bool m_error;
    public bool m_syncAll;
    public SSYaccTableRowEntry[] m_entries;

    public SSYaccTableRow(int[] q_data, int q_index)
    {
      this.m_action = q_data[q_index];
      this.m_goto = q_data[q_index + 1];
      this.m_error = q_data[q_index + 2] != 0;
      this.m_syncAll = q_data[q_index + 3] != 0;
      this.m_sync = q_data[q_index + 4] != 0;
      this.m_entries = new SSYaccTableRowEntry[this.numEntries()];
      q_index += 5;
      for (int index = 0; index < this.numEntries(); ++index)
      {
        this.m_entries[index] = new SSYaccTableRowEntry(q_data[q_index], q_data[q_index + 1], q_data[q_index + 2], q_data[q_index + 3]);
        q_index += 4;
      }
    }

    public SSYaccTableRow(int q_flags, int q_goto, int q_action, byte[] q_b, int q_index)
    {
      this.m_action = q_action;
      this.m_goto = q_goto;
      this.m_error = (q_flags & this.SSYaccTableRowFlagError) != 0;
      this.m_syncAll = (q_flags & this.SSYaccTableRowFlagSyncAll) != 0;
      this.m_sync = (q_flags & this.SSYaccTableRowFlagSync) != 0;
      this.m_entries = new SSYaccTableRowEntry[this.numEntries()];
      int num = this.numEntries();
      for (int index = 0; index < num; ++index)
      {
        int q_entry = this.convertInt(q_b, q_index);
        int q_token = this.convertInt(q_b, q_index + 4);
        this.m_entries[index] = new SSYaccTableRowEntry(q_entry, q_token);
        q_index += this.SSYaccTableEntrySize;
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

    public SSYaccTableRowEntry lookupEntry(int q_index) => this.m_entries[q_index];

    public SSYaccTableRowEntry lookupAction(int q_index)
    {
      for (int index = 0; index < this.m_action; ++index)
      {
        if (this.m_entries[index].token() == q_index)
          return this.m_entries[index];
      }
      return (SSYaccTableRowEntry) null;
    }

    public SSYaccTableRowEntry lookupGoto(int q_index)
    {
      for (int action = this.m_action; action < this.m_action + this.m_goto; ++action)
      {
        if (this.m_entries[action].token() == q_index)
          return this.m_entries[action];
      }
      return (SSYaccTableRowEntry) null;
    }

    public SSYaccTableRowEntry lookupError() => !this.hasError() ? (SSYaccTableRowEntry) null : this.m_entries[this.m_goto + this.m_action];

    public bool hasError() => this.m_error;

    public bool hasSync() => this.m_sync;

    public bool hasSyncAll() => this.m_syncAll;

    public int action() => this.m_action;

    public int numEntries() => this.m_goto + this.m_action + (this.hasError() ? 1 : 0);
  }
}
