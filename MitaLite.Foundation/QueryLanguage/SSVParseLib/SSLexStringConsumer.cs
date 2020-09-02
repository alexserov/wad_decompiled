// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexStringConsumer
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSLexStringConsumer : SSLexConsumer
  {
    public char[] m_string;

    public SSLexStringConsumer(string q_string)
    {
      this.m_string = q_string.ToCharArray();
      this.m_length = this.m_string.Length;
    }

    public override bool getNext()
    {
      bool flag = false;
      if (this.m_index < this.m_string.Length)
      {
        flag = true;
        this.m_current = this.m_string[this.m_index];
      }
      return flag;
    }
  }
}
