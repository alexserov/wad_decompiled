// Decompiled with JetBrains decompiler
// Type: System.Windows.Types.Rect
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Types {
    public struct Rect {
        internal double _x;
        internal double _y;
        internal double _width;
        internal double _height;

        public static bool operator ==(Rect rect1, Rect rect2) {
            return rect1.X == rect2.X && rect1.Y == rect2.Y && rect1.Width == rect2.Width && rect1.Height == rect2.Height;
        }

        public static bool operator !=(Rect rect1, Rect rect2) {
            return !(rect1 == rect2);
        }

        public static bool Equals(Rect rect1, Rect rect2) {
            if (rect1.IsEmpty)
                return rect2.IsEmpty;
            return rect1.X.Equals(obj: rect2.X) && rect1.Y.Equals(obj: rect2.Y) && rect1.Width.Equals(obj: rect2.Width) && rect1.Height.Equals(obj: rect2.Height);
        }

        public override bool Equals(object o) {
            return o != null && o is Rect rect2 && Equals(rect1: this, rect2: rect2);
        }

        public bool Equals(Rect value) {
            return Equals(rect1: this, rect2: value);
        }

        public override int GetHashCode() {
            if (IsEmpty)
                return 0;
            var hashCode1 = X.GetHashCode();
            var num1 = Y;
            var hashCode2 = num1.GetHashCode();
            var num2 = hashCode1 ^ hashCode2;
            num1 = Width;
            var hashCode3 = num1.GetHashCode();
            var num3 = num2 ^ hashCode3;
            num1 = Height;
            var hashCode4 = num1.GetHashCode();
            return num3 ^ hashCode4;
        }

        public Rect(Point location, Size size) {
            if (size.IsEmpty) {
                this = Empty;
            } else {
                this._x = location._x;
                this._y = location._y;
                this._width = size._width;
                this._height = size._height;
            }
        }

        public Rect(double x, double y, double width, double height) {
            if (width < 0.0 || height < 0.0)
                throw new ArgumentException(message: "Size_WidthAndHeightCannotBeNegative");
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
        }

        public Rect(Point point1, Point point2) {
            this._x = Math.Min(val1: point1._x, val2: point2._x);
            this._y = Math.Min(val1: point1._y, val2: point2._y);
            this._width = Math.Max(val1: Math.Max(val1: point1._x, val2: point2._x) - this._x, val2: 0.0);
            this._height = Math.Max(val1: Math.Max(val1: point1._y, val2: point2._y) - this._y, val2: 0.0);
        }

        public Rect(Size size) {
            if (size.IsEmpty) {
                this = Empty;
            } else {
                this._x = this._y = 0.0;
                this._width = size.Width;
                this._height = size.Height;
            }
        }

        public static Rect Empty { get; } = CreateEmptyRect();

        public bool IsEmpty {
            get { return this._width < 0.0; }
        }

        public Point Location {
            get { return new Point(x: this._x, y: this._y); }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                this._x = value._x;
                this._y = value._y;
            }
        }

        public Size Size {
            get { return IsEmpty ? Size.Empty : new Size(width: this._width, height: this._height); }
            set {
                if (value.IsEmpty) {
                    this = Empty;
                } else {
                    if (IsEmpty)
                        throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                    this._width = value._width;
                    this._height = value._height;
                }
            }
        }

        public double X {
            get { return this._x; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                this._x = value;
            }
        }

        public double Y {
            get { return this._y; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                this._y = value;
            }
        }

        public double Width {
            get { return this._width; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                this._width = value >= 0.0 ? value : throw new ArgumentException(message: "Size_WidthCannotBeNegative");
            }
        }

        public double Height {
            get { return this._height; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                this._height = value >= 0.0 ? value : throw new ArgumentException(message: "Size_HeightCannotBeNegative");
            }
        }

        public double Left {
            get { return this._x; }
        }

        public double Top {
            get { return this._y; }
        }

        public double Right {
            get { return IsEmpty ? double.NegativeInfinity : this._x + this._width; }
        }

        public double Bottom {
            get { return IsEmpty ? double.NegativeInfinity : this._y + this._height; }
        }

        public Point TopLeft {
            get { return new Point(x: Left, y: Top); }
        }

        public Point TopRight {
            get { return new Point(x: Right, y: Top); }
        }

        public Point BottomLeft {
            get { return new Point(x: Left, y: Bottom); }
        }

        public Point BottomRight {
            get { return new Point(x: Right, y: Bottom); }
        }

        public bool Contains(Point point) {
            return Contains(x: point._x, y: point._y);
        }

        public bool Contains(double x, double y) {
            return !IsEmpty && ContainsInternal(x: x, y: y);
        }

        public bool Contains(Rect rect) {
            return !IsEmpty && !rect.IsEmpty && this._x <= rect._x && this._y <= rect._y && this._x + this._width >= rect._x + rect._width && this._y + this._height >= rect._y + rect._height;
        }

        public bool IntersectsWith(Rect rect) {
            return !IsEmpty && !rect.IsEmpty && rect.Left <= Right && rect.Right >= Left && rect.Top <= Bottom && rect.Bottom >= Top;
        }

        public void Intersect(Rect rect) {
            if (!IntersectsWith(rect: rect)) {
                this = Empty;
            } else {
                var num1 = Math.Max(val1: Left, val2: rect.Left);
                var num2 = Math.Max(val1: Top, val2: rect.Top);
                this._width = Math.Max(val1: Math.Min(val1: Right, val2: rect.Right) - num1, val2: 0.0);
                this._height = Math.Max(val1: Math.Min(val1: Bottom, val2: rect.Bottom) - num2, val2: 0.0);
                this._x = num1;
                this._y = num2;
            }
        }

        public static Rect Intersect(Rect rect1, Rect rect2) {
            rect1.Intersect(rect: rect2);
            return rect1;
        }

        public void Union(Rect rect) {
            if (IsEmpty) {
                this = rect;
            } else {
                if (rect.IsEmpty)
                    return;
                var num1 = Math.Min(val1: Left, val2: rect.Left);
                var num2 = Math.Min(val1: Top, val2: rect.Top);
                this._width = rect.Width == double.PositiveInfinity || Width == double.PositiveInfinity ? double.PositiveInfinity : Math.Max(val1: Math.Max(val1: Right, val2: rect.Right) - num1, val2: 0.0);
                this._height = rect.Height == double.PositiveInfinity || Height == double.PositiveInfinity ? double.PositiveInfinity : Math.Max(val1: Math.Max(val1: Bottom, val2: rect.Bottom) - num2, val2: 0.0);
                this._x = num1;
                this._y = num2;
            }
        }

        public static Rect Union(Rect rect1, Rect rect2) {
            rect1.Union(rect: rect2);
            return rect1;
        }

        public void Union(Point point) {
            Union(rect: new Rect(point1: point, point2: point));
        }

        public static Rect Union(Rect rect, Point point) {
            rect.Union(rect: new Rect(point1: point, point2: point));
            return rect;
        }

        public void Offset(double offsetX, double offsetY) {
            if (IsEmpty)
                throw new InvalidOperationException(message: "Rect_CannotCallMethod");
            this._x += offsetX;
            this._y += offsetY;
        }

        public static Rect Offset(Rect rect, double offsetX, double offsetY) {
            rect.Offset(offsetX: offsetX, offsetY: offsetY);
            return rect;
        }

        public void Inflate(Size size) {
            Inflate(width: size._width, height: size._height);
        }

        public void Inflate(double width, double height) {
            if (IsEmpty)
                throw new InvalidOperationException(message: "Rect_CannotCallMethod");
            this._x -= width;
            this._y -= height;
            this._width += width;
            this._width += width;
            this._height += height;
            this._height += height;
            if (this._width >= 0.0 && this._height >= 0.0)
                return;
            this = Empty;
        }

        public override string ToString() {
            return string.Format(format: "Left:{0} Top:{1} Width:{2} Height:{3}", (object) this._x, (object) this._y, (object) this._width, (object) this._height);
        }

        public static Rect Inflate(Rect rect, Size size) {
            rect.Inflate(width: size._width, height: size._height);
            return rect;
        }

        public static Rect Inflate(Rect rect, double width, double height) {
            rect.Inflate(width: width, height: height);
            return rect;
        }

        public void Scale(double scaleX, double scaleY) {
            if (IsEmpty)
                return;
            this._x *= scaleX;
            this._y *= scaleY;
            this._width *= scaleX;
            this._height *= scaleY;
            if (scaleX < 0.0) {
                this._x += this._width;
                this._width *= -1.0;
            }

            if (scaleY >= 0.0)
                return;
            this._y += this._height;
            this._height *= -1.0;
        }

        bool ContainsInternal(double x, double y) {
            return x >= this._x && x - this._width <= this._x && y >= this._y && y - this._height <= this._y;
        }

        static Rect CreateEmptyRect() {
            return new Rect {
                _x = double.PositiveInfinity,
                _y = double.PositiveInfinity,
                _width = double.NegativeInfinity,
                _height = double.NegativeInfinity
            };
        }
    }
}