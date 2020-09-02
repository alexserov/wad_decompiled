// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.RectangleI
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation {
    public struct RectangleI {
        internal int _x;
        internal int _y;
        internal int _width;
        internal int _height;

        public static bool operator ==(RectangleI rect1, RectangleI rect2) {
            return rect1.X == rect2.X && rect1.Y == rect2.Y && rect1.Width == rect2.Width && rect1.Height == rect2.Height;
        }

        public static bool operator !=(RectangleI rect1, RectangleI rect2) {
            return !(rect1 == rect2);
        }

        public static bool Equals(RectangleI rect1, RectangleI rect2) {
            if (rect1.IsEmpty)
                return rect2.IsEmpty;
            return rect1.X.Equals(obj: rect2.X) && rect1.Y.Equals(obj: rect2.Y) && rect1.Width.Equals(obj: rect2.Width) && rect1.Height.Equals(obj: rect2.Height);
        }

        public override bool Equals(object o) {
            return o != null && o is RectangleI rect2 && Equals(rect1: this, rect2: rect2);
        }

        public bool Equals(RectangleI value) {
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

        public RectangleI(PointI location, SizeI size) {
            if (size.IsEmpty) {
                this = Empty;
            } else {
                this._x = location._x;
                this._y = location._y;
                this._width = size._width;
                this._height = size._height;
            }
        }

        public RectangleI(int x, int y, int width, int height) {
            if (width < 0.0 || height < 0.0)
                throw new ArgumentException(message: "Size_WidthAndHeightCannotBeNegative");
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
        }

        public RectangleI(PointI point1, PointI point2) {
            this._x = Math.Min(val1: point1._x, val2: point2._x);
            this._y = Math.Min(val1: point1._y, val2: point2._y);
            this._width = Math.Max(val1: Math.Max(val1: point1._x, val2: point2._x) - this._x, val2: 0);
            this._height = Math.Max(val1: Math.Max(val1: point1._y, val2: point2._y) - this._y, val2: 0);
        }

        public RectangleI(SizeI size) {
            if (size.IsEmpty) {
                this = Empty;
            } else {
                this._x = this._y = 0;
                this._width = size.Width;
                this._height = size.Height;
            }
        }

        public static RectangleI Empty { get; } = CreateEmptyRect();

        public bool IsEmpty {
            get { return this._width < 0.0; }
        }

        public PointI Location {
            get { return new PointI(x: this._x, y: this._y); }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                this._x = value._x;
                this._y = value._y;
            }
        }

        public SizeI Size {
            get { return IsEmpty ? SizeI.Empty : new SizeI(width: this._width, height: this._height); }
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

        public int X {
            get { return this._x; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                this._x = value;
            }
        }

        public int Y {
            get { return this._y; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                this._y = value;
            }
        }

        public int Width {
            get { return this._width; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                this._width = (double) value >= 0.0 ? value : throw new ArgumentException(message: "Size_WidthCannotBeNegative");
            }
        }

        public int Height {
            get { return this._height; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Rect_CannotModifyEmptyRect");
                this._height = (double) value >= 0.0 ? value : throw new ArgumentException(message: "Size_HeightCannotBeNegative");
            }
        }

        public int Left {
            get { return this._x; }
        }

        public int Top {
            get { return this._y; }
        }

        public int Right {
            get { return this._x + this._width; }
        }

        public int Bottom {
            get { return this._y + this._height; }
        }

        public PointI TopLeft {
            get { return new PointI(x: Left, y: Top); }
        }

        public PointI TopRight {
            get { return new PointI(x: Right, y: Top); }
        }

        public PointI BottomLeft {
            get { return new PointI(x: Left, y: Bottom); }
        }

        public PointI BottomRight {
            get { return new PointI(x: Right, y: Bottom); }
        }

        public bool Contains(PointI point) {
            return Contains(x: point._x, y: point._y);
        }

        public bool Contains(int x, int y) {
            return !IsEmpty && ContainsInternal(x: x, y: y);
        }

        public bool Contains(RectangleI rect) {
            return !IsEmpty && !rect.IsEmpty && this._x <= rect._x && this._y <= rect._y && this._x + this._width >= rect._x + rect._width && this._y + this._height >= rect._y + rect._height;
        }

        public bool IntersectsWith(RectangleI rect) {
            return !IsEmpty && !rect.IsEmpty && rect.Left <= Right && rect.Right >= Left && rect.Top <= Bottom && rect.Bottom >= Top;
        }

        public void Intersect(RectangleI rect) {
            if (!IntersectsWith(rect: rect)) {
                this = Empty;
            } else {
                var num1 = Math.Max(val1: Left, val2: rect.Left);
                var num2 = Math.Max(val1: Top, val2: rect.Top);
                this._width = Math.Max(val1: Math.Min(val1: Right, val2: rect.Right) - num1, val2: 0);
                this._height = Math.Max(val1: Math.Min(val1: Bottom, val2: rect.Bottom) - num2, val2: 0);
                this._x = num1;
                this._y = num2;
            }
        }

        public static RectangleI Intersect(RectangleI rect1, RectangleI rect2) {
            rect1.Intersect(rect: rect2);
            return rect1;
        }

        public void Union(RectangleI rect) {
            if (IsEmpty) {
                this = rect;
            } else {
                if (rect.IsEmpty)
                    return;
                var num1 = Math.Min(val1: Left, val2: rect.Left);
                var num2 = Math.Min(val1: Top, val2: rect.Top);
                this._width = Math.Max(val1: Math.Max(val1: Right, val2: rect.Right) - num1, val2: 0);
                this._height = Math.Max(val1: Math.Max(val1: Bottom, val2: rect.Bottom) - num2, val2: 0);
                this._x = num1;
                this._y = num2;
            }
        }

        public static RectangleI Union(RectangleI rect1, RectangleI rect2) {
            rect1.Union(rect: rect2);
            return rect1;
        }

        public void Union(PointI point) {
            Union(rect: new RectangleI(point1: point, point2: point));
        }

        public static RectangleI Union(RectangleI rect, PointI point) {
            rect.Union(rect: new RectangleI(point1: point, point2: point));
            return rect;
        }

        public void Offset(int offsetX, int offsetY) {
            if (IsEmpty)
                throw new InvalidOperationException(message: "Rect_CannotCallMethod");
            this._x += offsetX;
            this._y += offsetY;
        }

        public static RectangleI Offset(RectangleI rect, int offsetX, int offsetY) {
            rect.Offset(offsetX: offsetX, offsetY: offsetY);
            return rect;
        }

        public void Inflate(SizeI size) {
            Inflate(width: size._width, height: size._height);
        }

        public void Inflate(int width, int height) {
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

        public static RectangleI Inflate(RectangleI rect, SizeI size) {
            rect.Inflate(width: size._width, height: size._height);
            return rect;
        }

        public static RectangleI Inflate(RectangleI rect, int width, int height) {
            rect.Inflate(width: width, height: height);
            return rect;
        }

        public void Scale(int scaleX, int scaleY) {
            if (IsEmpty)
                return;
            this._x *= scaleX;
            this._y *= scaleY;
            this._width *= scaleX;
            this._height *= scaleY;
            if (scaleX < 0.0) {
                this._x += this._width;
                this._width *= -1;
            }

            if (scaleY >= 0.0)
                return;
            this._y += this._height;
            this._height *= -1;
        }

        bool ContainsInternal(int x, int y) {
            return x >= this._x && x - this._width <= this._x && y >= this._y && y - this._height <= this._y;
        }

        static RectangleI CreateEmptyRect() {
            return new RectangleI {
                _x = 0,
                _y = 0,
                _width = 0,
                _height = 0
            };
        }
    }
}