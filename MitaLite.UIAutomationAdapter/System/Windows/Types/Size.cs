// Decompiled with JetBrains decompiler
// Type: System.Windows.Types.Size
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Types {
    public struct Size {
        internal double _width;
        internal double _height;
        static readonly Size s_empty = CreateEmptySize();

        public static bool operator ==(Size size1, Size size2) {
            return size1.Width == size2.Width && size1.Height == size2.Height;
        }

        public static bool operator !=(Size size1, Size size2) {
            return !(size1 == size2);
        }

        public static bool Equals(Size size1, Size size2) {
            if (size1.IsEmpty)
                return size2.IsEmpty;
            return size1.Width.Equals(obj: size2.Width) && size1.Height.Equals(obj: size2.Height);
        }

        public override bool Equals(object o) {
            return o != null && o is Size size2 && Equals(size1: this, size2: size2);
        }

        public bool Equals(Size value) {
            return Equals(size1: this, size2: value);
        }

        public override int GetHashCode() {
            return IsEmpty ? 0 : Width.GetHashCode() ^ Height.GetHashCode();
        }

        public Size(double width, double height) {
            this._width = width >= 0.0 && height >= 0.0 ? width : throw new ArgumentException(message: "Size_WidthAndHeightCannotBeNegative");
            this._height = height;
        }

        public static Size Empty {
            get { return s_empty; }
        }

        public bool IsEmpty {
            get { return this._width < 0.0; }
        }

        public double Width {
            get { return this._width; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Size_CannotModifyEmptySize");
                this._width = value >= 0.0 ? value : throw new ArgumentException(message: "Size_WidthCannotBeNegative");
            }
        }

        public double Height {
            get { return this._height; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Size_CannotModifyEmptySize");
                this._height = value >= 0.0 ? value : throw new ArgumentException(message: "Size_HeightCannotBeNegative");
            }
        }

        public static explicit operator Point(Size size) {
            return new Point(x: size._width, y: size._height);
        }

        static Size CreateEmptySize() {
            return new Size {
                _width = double.NegativeInfinity,
                _height = double.NegativeInfinity
            };
        }
    }
}