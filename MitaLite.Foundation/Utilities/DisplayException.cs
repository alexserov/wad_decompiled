// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.DisplayException
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Globalization;

namespace MS.Internal.Mita.Foundation.Utilities
{
  public class DisplayException : MitaException
  {
    private int _nativeErrorCode;

    public DisplayException()
    {
    }

    public DisplayException(string message)
      : base(message)
    {
    }

    public DisplayException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public DisplayException(string message, int windowsError)
      : base(message)
      => this._nativeErrorCode = windowsError;

    public override string Message => string.IsNullOrEmpty(base.Message) ? "Error: " + this._nativeErrorCode.ToString((IFormatProvider) CultureInfo.InvariantCulture) : base.Message + " : " + this._nativeErrorCode.ToString((IFormatProvider) CultureInfo.InvariantCulture);

    public int Win32Error => this._nativeErrorCode;
  }
}
