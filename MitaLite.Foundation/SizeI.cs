// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.SizeI
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation {
    public struct SizeI {
        internal int _width;
        internal int _height;
        static readonly SizeI s_empty = CreateEmptySize();

        public static bool operator ==(SizeI size1, SizeI size2) => size1.Width == size2.Width && size1.Height == size2.Height;

        public static bool operator !=(SizeI size1, SizeI size2) => !(size1 == size2);

        public static bool Equals(SizeI size1, SizeI size2) {
            if (size1.IsEmpty)
                return size2.IsEmpty;
            return size1.Width.Equals(obj: size2.Width) && size1.Height.Equals(obj: size2.Height);
        }

        public override bool Equals(object o) => o != null && o is SizeI size2 && Equals(size1: this, size2: size2);

        public bool Equals(SizeI value) => Equals(size1: this, size2: value);

        public override int GetHashCode() => IsEmpty ? 0 : Width.GetHashCode() ^ Height.GetHashCode();

        public SizeI(int width, int height) {
            this._width = (double) width >= 0.0 && (double) height >= 0.0 ? width : throw new ArgumentException(message: "Size_WidthAndHeightCannotBeNegative");
            this._height = height;
        }

        public static SizeI Empty {
            get { return s_empty; }
        }

        public bool IsEmpty {
            get { return this._width < 0.0; }
        }

        public int Width {
            get { return this._width; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Size_CannotModifyEmptySize");
                this._width = (double) value >= 0.0 ? value : throw new ArgumentException(message: "Size_WidthCannotBeNegative");
            }
        }

        public int Height {
            get { return this._height; }
            set {
                if (IsEmpty)
                    throw new InvalidOperationException(message: "Size_CannotModifyEmptySize");
                this._height = (double) value >= 0.0 ? value : throw new ArgumentException(message: "Size_HeightCannotBeNegative");
            }
        }

        public static explicit operator PointI(SizeI size) => new PointI(x: size._width, y: size._height);

        static SizeI CreateEmptySize() => new SizeI {
            _width = 0,
            _height = 0
        };
    }
}