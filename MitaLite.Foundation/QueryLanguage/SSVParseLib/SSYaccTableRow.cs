// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYaccTableRow
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSYaccTableRow {
        public int m_action;
        public SSYaccTableRowEntry[] m_entries;
        public bool m_error;
        public int m_goto;
        public bool m_sync;
        public bool m_syncAll;
        public int SSYaccTableEntrySize = 8;
        public int SSYaccTableRowFlagError = 2;
        public int SSYaccTableRowFlagSync = 1;
        public int SSYaccTableRowFlagSyncAll = 4;

        public SSYaccTableRow(int[] q_data, int q_index) {
            this.m_action = q_data[q_index];
            this.m_goto = q_data[q_index + 1];
            this.m_error = q_data[q_index + 2] != 0;
            this.m_syncAll = q_data[q_index + 3] != 0;
            this.m_sync = q_data[q_index + 4] != 0;
            this.m_entries = new SSYaccTableRowEntry[numEntries()];
            q_index += 5;
            for (var index = 0; index < numEntries(); ++index) {
                this.m_entries[index] = new SSYaccTableRowEntry(q_token: q_data[q_index], q_entry: q_data[q_index + 1], q_action: q_data[q_index + 2], q_sync: q_data[q_index + 3]);
                q_index += 4;
            }
        }

        public SSYaccTableRow(int q_flags, int q_goto, int q_action, byte[] q_b, int q_index) {
            this.m_action = q_action;
            this.m_goto = q_goto;
            this.m_error = (q_flags & this.SSYaccTableRowFlagError) != 0;
            this.m_syncAll = (q_flags & this.SSYaccTableRowFlagSyncAll) != 0;
            this.m_sync = (q_flags & this.SSYaccTableRowFlagSync) != 0;
            this.m_entries = new SSYaccTableRowEntry[numEntries()];
            var num = numEntries();
            for (var index = 0; index < num; ++index) {
                var q_entry = convertInt(b: q_b, offset: q_index);
                var q_token = convertInt(b: q_b, offset: q_index + 4);
                this.m_entries[index] = new SSYaccTableRowEntry(q_entry: q_entry, q_token: q_token);
                q_index += this.SSYaccTableEntrySize;
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

        public SSYaccTableRowEntry lookupEntry(int q_index) {
            return this.m_entries[q_index];
        }

        public SSYaccTableRowEntry lookupAction(int q_index) {
            for (var index = 0; index < this.m_action; ++index)
                if (this.m_entries[index].token() == q_index)
                    return this.m_entries[index];
            return null;
        }

        public SSYaccTableRowEntry lookupGoto(int q_index) {
            for (var action = this.m_action; action < this.m_action + this.m_goto; ++action)
                if (this.m_entries[action].token() == q_index)
                    return this.m_entries[action];
            return null;
        }

        public SSYaccTableRowEntry lookupError() {
            return !hasError() ? null : this.m_entries[this.m_goto + this.m_action];
        }

        public bool hasError() {
            return this.m_error;
        }

        public bool hasSync() {
            return this.m_sync;
        }

        public bool hasSyncAll() {
            return this.m_syncAll;
        }

        public int action() {
            return this.m_action;
        }

        public int numEntries() {
            return this.m_goto + this.m_action + (hasError() ? 1 : 0);
        }
    }
}