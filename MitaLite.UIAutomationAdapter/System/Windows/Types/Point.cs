// Decompiled with JetBrains decompiler
// Type: System.Windows.Types.Point
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Types
{
  public struct Point : IFormattable
  {
    internal double _x;
    internal double _y;

    public static bool operator ==(Point point1, Point point2) => point1.X == point2.X && point1.Y == point2.Y;

    public static bool operator !=(Point point1, Point point2) => !(point1 == point2);

    public static bool Equals(Point point1, Point point2) => point1.X.Equals(point2.X) && point1.Y.Equals(point2.Y);

    public override bool Equals(object o) => o != null && o is Point point2 && Point.Equals(this, point2);

    public bool Equals(Point value) => Point.Equals(this, value);

    public override int GetHashCode()
    {
      double num = this.X;
      int hashCode1 = num.GetHashCode();
      num = this.Y;
      int hashCode2 = num.GetHashCode();
      return hashCode1 ^ hashCode2;
    }

    public double X
    {
      get => this._x;
      set => this._x = value;
    }

    public double Y
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

    public Point(double x, double y)
    {
      this._x = x;
      this._y = y;
    }

    public void Offset(double offsetX, double offsetY)
    {
      this._x += offsetX;
      this._y += offsetY;
    }

    public static explicit operator Size(Point point) => new Size(Math.Abs(point._x), Math.Abs(point._y));
  }
}
