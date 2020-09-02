// Decompiled with JetBrains decompiler
// Type: System.Windows.Types.Point
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Types {
    public struct Point : IFormattable {
        internal double _x;
        internal double _y;

        public static bool operator ==(Point point1, Point point2) {
            return point1.X == point2.X && point1.Y == point2.Y;
        }

        public static bool operator !=(Point point1, Point point2) {
            return !(point1 == point2);
        }

        public static bool Equals(Point point1, Point point2) {
            return point1.X.Equals(obj: point2.X) && point1.Y.Equals(obj: point2.Y);
        }

        public override bool Equals(object o) {
            return o != null && o is Point point2 && Equals(point1: this, point2: point2);
        }

        public bool Equals(Point value) {
            return Equals(point1: this, point2: value);
        }

        public override int GetHashCode() {
            var num = X;
            var hashCode1 = num.GetHashCode();
            num = Y;
            var hashCode2 = num.GetHashCode();
            return hashCode1 ^ hashCode2;
        }

        public double X {
            get { return this._x; }
            set { this._x = value; }
        }

        public double Y {
            get { return this._y; }
            set { this._y = value; }
        }

        public override string ToString() {
            return ConvertToString(format: null, provider: null);
        }

        public string ToString(IFormatProvider provider) {
            return ConvertToString(format: null, provider: provider);
        }

        string IFormattable.ToString(string format, IFormatProvider provider) {
            return ConvertToString(format: format, provider: provider);
        }

        internal string ConvertToString(string format, IFormatProvider provider) {
            var ch = ',';
            return string.Format(provider: provider, format: "{1:" + format + "}{0}{2:" + format + "}", args: new object[3] {
                ch,
                this._x,
                this._y
            });
        }

        public Point(double x, double y) {
            this._x = x;
            this._y = y;
        }

        public void Offset(double offsetX, double offsetY) {
            this._x += offsetX;
            this._y += offsetY;
        }

        public static explicit operator Size(Point point) {
            return new Size(width: Math.Abs(value: point._x), height: Math.Abs(value: point._y));
        }
    }
}