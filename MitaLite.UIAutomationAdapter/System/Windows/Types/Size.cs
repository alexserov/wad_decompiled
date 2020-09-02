// Decompiled with JetBrains decompiler
// Type: System.Windows.Types.Size
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Types
{
  public struct Size
  {
    internal double _width;
    internal double _height;
    private static readonly Size s_empty = Size.CreateEmptySize();

    public static bool operator ==(Size size1, Size size2) => size1.Width == size2.Width && size1.Height == size2.Height;

    public static bool operator !=(Size size1, Size size2) => !(size1 == size2);

    public static bool Equals(Size size1, Size size2)
    {
      if (size1.IsEmpty)
        return size2.IsEmpty;
      return size1.Width.Equals(size2.Width) && size1.Height.Equals(size2.Height);
    }

    public override bool Equals(object o) => o != null && o is Size size2 && Size.Equals(this, size2);

    public bool Equals(Size value) => Size.Equals(this, value);

    public override int GetHashCode() => this.IsEmpty ? 0 : this.Width.GetHashCode() ^ this.Height.GetHashCode();

    public Size(double width, double height)
    {
      this._width = width >= 0.0 && height >= 0.0 ? width : throw new ArgumentException("Size_WidthAndHeightCannotBeNegative");
      this._height = height;
    }

    public static Size Empty => Size.s_empty;

    public bool IsEmpty => this._width < 0.0;

    public double Width
    {
      get => this._width;
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Size_CannotModifyEmptySize");
        this._width = value >= 0.0 ? value : throw new ArgumentException("Size_WidthCannotBeNegative");
      }
    }

    public double Height
    {
      get => this._height;
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Size_CannotModifyEmptySize");
        this._height = value >= 0.0 ? value : throw new ArgumentException("Size_HeightCannotBeNegative");
      }
    }

    public static explicit operator Point(Size size) => new Point(size._width, size._height);

    private static Size CreateEmptySize() => new Size()
    {
      _width = double.NegativeInfinity,
      _height = double.NegativeInfinity
    };
  }
}
