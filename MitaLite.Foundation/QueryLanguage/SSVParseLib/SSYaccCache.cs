// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSYaccCache
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib {
    internal class SSYaccCache : Queue<object> {
        public bool hasElements() {
            return Count != 0;
        }

        public SSLexLexeme remove() {
            SSLexLexeme ssLexLexeme = null;
            if (Count != 0)
                ssLexLexeme = (SSLexLexeme) Dequeue();
            return ssLexLexeme;
        }
    }
}