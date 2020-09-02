// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib.SSLexFileConsumer
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.IO;

namespace MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib
{
  internal class SSLexFileConsumer : SSLexConsumer
  {
    public bool m_first;
    public bool m_unicode;
    public bool m_reversedUnicode;
    public FileStream m_fileStream;

    public SSLexFileConsumer(string q_name, bool q_unicode)
    {
      this.m_first = true;
      this.m_unicode = q_unicode;
      this.m_reversedUnicode = true;
      this.m_fileStream = File.Open(q_name, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    public bool readByte()
    {
      try
      {
        int num = this.m_fileStream.ReadByte();
        if (num == -1)
          return false;
        this.m_current = (char) num;
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool readUnicodeByte()
    {
      if (!this.readByte())
        return false;
      char current = this.m_current;
      if (!this.readByte())
        return false;
      if (this.m_reversedUnicode)
        this.m_current = (char) ((uint) this.m_current << 8 | (uint) current);
      else
        this.m_current = (char) ((uint) current << 8 | (uint) this.m_current);
      return true;
    }

    public override bool getNext()
    {
      if (this.m_first && this.m_unicode)
      {
        this.m_first = false;
        if (!this.readByte())
          return false;
        if (this.m_current != 'ÿ' && this.m_current != 'þ')
          return true;
        char current = this.m_current;
        if (!this.readByte())
          return false;
        if (current == 'ÿ' && this.m_current == 'þ')
        {
          this.m_reversedUnicode = true;
          return this.readUnicodeByte();
        }
        if (current == 'þ' && this.m_current == 'ÿ')
        {
          this.m_reversedUnicode = false;
          return this.readUnicodeByte();
        }
        this.m_current = (char) ((uint) this.m_current << 8 | (uint) current);
        return true;
      }
      return this.m_unicode ? this.readUnicodeByte() : this.readByte();
    }

    public override void flushLexeme(SSLexMark q_mark)
    {
      this.m_start = this.m_index = q_mark.m_index;
      this.m_line += q_mark.m_line;
      this.m_offset = q_mark.m_offset;
      this.m_scanLine = 0;
      this.m_scanOffset = q_mark.m_offset;
      this.m_bufferIndex = 0;
      this.m_fileStream.Position = (long) this.m_index;
    }
  }
}
