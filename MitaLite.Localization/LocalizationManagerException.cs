﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Localization.LocalizationManagerException
// Assembly: MitaLite.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FE6CFDB-BB88-427C-96A4-C26318ECB83B
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Localization.dll

using System;

namespace MS.Internal.Mita.Localization
{
  public class LocalizationManagerException : Exception
  {
    public LocalizationManagerException()
    {
    }

    public LocalizationManagerException(string message)
      : base(message)
    {
    }

    public LocalizationManagerException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
