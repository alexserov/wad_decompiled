// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYaccTableRowEntry
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSYaccTableRowEntry {
        public const int SSYaccActionShift = 0;
        public const int SSYaccActionError = 1;
        public const int SSYaccActionReduce = 2;
        public const int SSYaccActionAccept = 3;
        public const int SSYaccActionConflict = 4;
        public const uint SSYaccTableEntryFlagSync = 2147483648;
        public const int SSYaccTableEntryFlagShift = 1073741824;
        public const int SSYaccTableEntryFlagReduce = 536870912;
        public const int SSYaccTableEntryFlagAccept = 268435456;
        public const int SSYaccTableEntryFlagConflict = 134217728;
        public const uint SSYaccTableEntryFlagMask = 4160749568;
        public int m_action;
        public int m_entry;
        public bool m_sync;
        public int m_token;

        public SSYaccTableRowEntry(int q_token, int q_entry, int q_action, int q_sync) {
            this.m_token = q_token;
            this.m_entry = q_entry;
            this.m_action = q_action;
            this.m_sync = q_sync != 0;
        }

        public SSYaccTableRowEntry(int q_entry, int q_token) {
            this.m_token = q_token;
            this.m_entry = (int) (q_entry & 134217727L);
            if ((q_entry & 1073741824) != 0)
                this.m_action = 0;
            else if ((q_entry & 536870912) != 0)
                this.m_action = 2;
            else if ((q_entry & 268435456) != 0)
                this.m_action = 3;
            else if ((q_entry & 134217728) != 0)
                this.m_action = 4;
            this.m_sync = (q_entry & 2147483648L) != 0L;
        }

        public int action() {
            return this.m_action;
        }

        public int entry() {
            return this.m_entry;
        }

        public int token() {
            return this.m_token;
        }

        public bool hasSync() {
            return this.m_sync;
        }
    }
}