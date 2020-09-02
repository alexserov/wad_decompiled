// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexMark
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSLexMark
  {
    public int m_line;
    public int m_index;
    public int m_offset;

    public SSLexMark()
    {
    }

    public SSLexMark(int q_line, int q_offset, int q_index)
    {
      this.m_index = q_index;
      this.m_line = q_line;
      this.m_offset = q_offset;
    }

    public int index() => this.m_index;
  }
}
