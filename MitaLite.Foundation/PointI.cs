// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.PointI
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation
{
  public struct PointI : IFormattable
  {
    internal int _x;
    internal int _y;

    public static bool operator ==(PointI point1, PointI point2) => point1.X == point2.X && point1.Y == point2.Y;

    public static bool operator !=(PointI point1, PointI point2) => !(point1 == point2);

    public static bool Equals(PointI point1, PointI point2) => point1.X.Equals(point2.X) && point1.Y.Equals(point2.Y);

    public override bool Equals(object o) => o != null && o is PointI point2 && PointI.Equals(this, point2);

    public bool Equals(PointI value) => PointI.Equals(this, value);

    public override int GetHashCode()
    {
      int num = this.X;
      int hashCode1 = num.GetHashCode();
      num = this.Y;
      int hashCode2 = num.GetHashCode();
      return hashCode1 ^ hashCode2;
    }

    public int X
    {
      get => this._x;
      set => this._x = value;
    }

    public int Y
    {
      get => this._y;
      set => this._y = value;
    }

    public override string ToString() => this.ConvertToString((string) null, (IFormatProvider) null);

    public string ToString(IFormatProvider provider) => this.ConvertToString((string) null, provider);

    string IFormattable.ToString(string format, IFormatProvider provider) => this.ConvertToString(format, provider);

    internal string ConvertToString(string format, IFormatProvider provider)
    {
      char ch = ',';
      return string.Format(provider, "{1:" + format + "}{0}{2:" + format + "}", new object[3]
      {
        (object) ch,
        (object) this._x,
        (object) this._y
      });
    }

    public PointI(int x, int y)
    {
      this._x = x;
      this._y = y;
    }

    public void Offset(int offsetX, int offsetY)
    {
      this._x += offsetX;
      this._y += offsetY;
    }

    public static explicit operator SizeI(PointI point) => new SizeI(Math.Abs(point._x), Math.Abs(point._y));
  }
}
