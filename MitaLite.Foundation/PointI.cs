// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.PointI
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation {
    public struct PointI : IFormattable {
        internal int _x;
        internal int _y;

        public static bool operator ==(PointI point1, PointI point2) {
            return point1.X == point2.X && point1.Y == point2.Y;
        }

        public static bool operator !=(PointI point1, PointI point2) {
            return !(point1 == point2);
        }

        public static bool Equals(PointI point1, PointI point2) {
            return point1.X.Equals(obj: point2.X) && point1.Y.Equals(obj: point2.Y);
        }

        public override bool Equals(object o) {
            return o != null && o is PointI point2 && Equals(point1: this, point2: point2);
        }

        public bool Equals(PointI value) {
            return Equals(point1: this, point2: value);
        }

        public override int GetHashCode() {
            var num = X;
            var hashCode1 = num.GetHashCode();
            num = Y;
            var hashCode2 = num.GetHashCode();
            return hashCode1 ^ hashCode2;
        }

        public int X {
            get { return this._x; }
            set { this._x = value; }
        }

        public int Y {
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

        public PointI(int x, int y) {
            this._x = x;
            this._y = y;
        }

        public void Offset(int offsetX, int offsetY) {
            this._x += offsetX;
            this._y += offsetY;
        }

        public static explicit operator SizeI(PointI point) {
            return new SizeI(width: Math.Abs(value: point._x), height: Math.Abs(value: point._y));
        }
    }
}