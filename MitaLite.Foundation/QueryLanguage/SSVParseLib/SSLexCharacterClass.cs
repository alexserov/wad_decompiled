// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexCharacterClass
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSLexCharacterClass
  {
    public int m_size;
    public int m_min;
    public int m_max;
    public int[] m_array;

    public SSLexCharacterClass(int q_size, int q_min, int q_max, int[] q_array)
    {
      this.m_size = q_size;
      this.m_min = q_min;
      this.m_max = q_max;
      this.m_array = q_array;
    }

    public SSLexCharacterClass(int[] q_array)
    {
      this.m_min = q_array[0];
      this.m_max = q_array[1];
      this.m_size = q_array[2];
      this.m_array = new int[this.m_size * 2];
      for (int index = 0; index < this.m_size * 2; ++index)
        this.m_array[index] = q_array[index + 3];
    }

    public bool translate(char[] q_char)
    {
      char ch = q_char[0];
      if ((int) ch < this.m_min || (int) ch > this.m_max)
        return false;
      for (int index = 0; index < this.m_size; ++index)
      {
        if ((int) ch >= this.m_array[index * 2] && (int) ch <= this.m_array[index * 2 + 1])
        {
          q_char[0] = (char) this.m_min;
          return true;
        }
      }
      return false;
    }
  }
}
