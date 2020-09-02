﻿// Decompiled with JetBrains decompiler
// Type: System.Windows.Types.Rect
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

namespace System.Windows.Types
{
  public struct Rect
  {
    internal double _x;
    internal double _y;
    internal double _width;
    internal double _height;
    private static readonly Rect s_empty = Rect.CreateEmptyRect();

    public static bool operator ==(Rect rect1, Rect rect2) => rect1.X == rect2.X && rect1.Y == rect2.Y && rect1.Width == rect2.Width && rect1.Height == rect2.Height;

    public static bool operator !=(Rect rect1, Rect rect2) => !(rect1 == rect2);

    public static bool Equals(Rect rect1, Rect rect2)
    {
      if (rect1.IsEmpty)
        return rect2.IsEmpty;
      return rect1.X.Equals(rect2.X) && rect1.Y.Equals(rect2.Y) && rect1.Width.Equals(rect2.Width) && rect1.Height.Equals(rect2.Height);
    }

    public override bool Equals(object o) => o != null && o is Rect rect2 && Rect.Equals(this, rect2);

    public bool Equals(Rect value) => Rect.Equals(this, value);

    public override int GetHashCode()
    {
      if (this.IsEmpty)
        return 0;
      int hashCode1 = this.X.GetHashCode();
      double num1 = this.Y;
      int hashCode2 = num1.GetHashCode();
      int num2 = hashCode1 ^ hashCode2;
      num1 = this.Width;
      int hashCode3 = num1.GetHashCode();
      int num3 = num2 ^ hashCode3;
      num1 = this.Height;
      int hashCode4 = num1.GetHashCode();
      return num3 ^ hashCode4;
    }

    public Rect(Point location, Size size)
    {
      if (size.IsEmpty)
      {
        this = Rect.s_empty;
      }
      else
      {
        this._x = location._x;
        this._y = location._y;
        this._width = size._width;
        this._height = size._height;
      }
    }

    public Rect(double x, double y, double width, double height)
    {
      if (width < 0.0 || height < 0.0)
        throw new ArgumentException("Size_WidthAndHeightCannotBeNegative");
      this._x = x;
      this._y = y;
      this._width = width;
      this._height = height;
    }

    public Rect(Point point1, Point point2)
    {
      this._x = Math.Min(point1._x, point2._x);
      this._y = Math.Min(point1._y, point2._y);
      this._width = Math.Max(Math.Max(point1._x, point2._x) - this._x, 0.0);
      this._height = Math.Max(Math.Max(point1._y, point2._y) - this._y, 0.0);
    }

    public Rect(Size size)
    {
      if (size.IsEmpty)
      {
        this = Rect.s_empty;
      }
      else
      {
        this._x = this._y = 0.0;
        this._width = size.Width;
        this._height = size.Height;
      }
    }

    public static Rect Empty => Rect.s_empty;

    public bool IsEmpty => this._width < 0.0;

    public Point Location
    {
      get => new Point(this._x, this._y);
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
        this._x = value._x;
        this._y = value._y;
      }
    }

    public Size Size
    {
      get => this.IsEmpty ? Size.Empty : new Size(this._width, this._height);
      set
      {
        if (value.IsEmpty)
        {
          this = Rect.s_empty;
        }
        else
        {
          if (this.IsEmpty)
            throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
          this._width = value._width;
          this._height = value._height;
        }
      }
    }

    public double X
    {
      get => this._x;
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
        this._x = value;
      }
    }

    public double Y
    {
      get => this._y;
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
        this._y = value;
      }
    }

    public double Width
    {
      get => this._width;
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
        this._width = value >= 0.0 ? value : throw new ArgumentException("Size_WidthCannotBeNegative");
      }
    }

    public double Height
    {
      get => this._height;
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
        this._height = value >= 0.0 ? value : throw new ArgumentException("Size_HeightCannotBeNegative");
      }
    }

    public double Left => this._x;

    public double Top => this._y;

    public double Right => this.IsEmpty ? double.NegativeInfinity : this._x + this._width;

    public double Bottom => this.IsEmpty ? double.NegativeInfinity : this._y + this._height;

    public Point TopLeft => new Point(this.Left, this.Top);

    public Point TopRight => new Point(this.Right, this.Top);

    public Point BottomLeft => new Point(this.Left, this.Bottom);

    public Point BottomRight => new Point(this.Right, this.Bottom);

    public bool Contains(Point point) => this.Contains(point._x, point._y);

    public bool Contains(double x, double y) => !this.IsEmpty && this.ContainsInternal(x, y);

    public bool Contains(Rect rect) => !this.IsEmpty && !rect.IsEmpty && (this._x <= rect._x && this._y <= rect._y) && this._x + this._width >= rect._x + rect._width && this._y + this._height >= rect._y + rect._height;

    public bool IntersectsWith(Rect rect) => !this.IsEmpty && !rect.IsEmpty && (rect.Left <= this.Right && rect.Right >= this.Left) && rect.Top <= this.Bottom && rect.Bottom >= this.Top;

    public void Intersect(Rect rect)
    {
      if (!this.IntersectsWith(rect))
      {
        this = Rect.Empty;
      }
      else
      {
        double num1 = Math.Max(this.Left, rect.Left);
        double num2 = Math.Max(this.Top, rect.Top);
        this._width = Math.Max(Math.Min(this.Right, rect.Right) - num1, 0.0);
        this._height = Math.Max(Math.Min(this.Bottom, rect.Bottom) - num2, 0.0);
        this._x = num1;
        this._y = num2;
      }
    }

    public static Rect Intersect(Rect rect1, Rect rect2)
    {
      rect1.Intersect(rect2);
      return rect1;
    }

    public void Union(Rect rect)
    {
      if (this.IsEmpty)
      {
        this = rect;
      }
      else
      {
        if (rect.IsEmpty)
          return;
        double num1 = Math.Min(this.Left, rect.Left);
        double num2 = Math.Min(this.Top, rect.Top);
        this._width = rect.Width == double.PositiveInfinity || this.Width == double.PositiveInfinity ? double.PositiveInfinity : Math.Max(Math.Max(this.Right, rect.Right) - num1, 0.0);
        this._height = rect.Height == double.PositiveInfinity || this.Height == double.PositiveInfinity ? double.PositiveInfinity : Math.Max(Math.Max(this.Bottom, rect.Bottom) - num2, 0.0);
        this._x = num1;
        this._y = num2;
      }
    }

    public static Rect Union(Rect rect1, Rect rect2)
    {
      rect1.Union(rect2);
      return rect1;
    }

    public void Union(Point point) => this.Union(new Rect(point, point));

    public static Rect Union(Rect rect, Point point)
    {
      rect.Union(new Rect(point, point));
      return rect;
    }

    public void Offset(double offsetX, double offsetY)
    {
      if (this.IsEmpty)
        throw new InvalidOperationException("Rect_CannotCallMethod");
      this._x += offsetX;
      this._y += offsetY;
    }

    public static Rect Offset(Rect rect, double offsetX, double offsetY)
    {
      rect.Offset(offsetX, offsetY);
      return rect;
    }

    public void Inflate(Size size) => this.Inflate(size._width, size._height);

    public void Inflate(double width, double height)
    {
      if (this.IsEmpty)
        throw new InvalidOperationException("Rect_CannotCallMethod");
      this._x -= width;
      this._y -= height;
      this._width += width;
      this._width += width;
      this._height += height;
      this._height += height;
      if (this._width >= 0.0 && this._height >= 0.0)
        return;
      this = Rect.s_empty;
    }

    public override string ToString() => string.Format("Left:{0} Top:{1} Width:{2} Height:{3}", (object) this._x, (object) this._y, (object) this._width, (object) this._height);

    public static Rect Inflate(Rect rect, Size size)
    {
      rect.Inflate(size._width, size._height);
      return rect;
    }

    public static Rect Inflate(Rect rect, double width, double height)
    {
      rect.Inflate(width, height);
      return rect;
    }

    public void Scale(double scaleX, double scaleY)
    {
      if (this.IsEmpty)
        return;
      this._x *= scaleX;
      this._y *= scaleY;
      this._width *= scaleX;
      this._height *= scaleY;
      if (scaleX < 0.0)
      {
        this._x += this._width;
        this._width *= -1.0;
      }
      if (scaleY >= 0.0)
        return;
      this._y += this._height;
      this._height *= -1.0;
    }

    private bool ContainsInternal(double x, double y) => x >= this._x && x - this._width <= this._x && y >= this._y && y - this._height <= this._y;

    private static Rect CreateEmptyRect() => new Rect()
    {
      _x = double.PositiveInfinity,
      _y = double.PositiveInfinity,
      _width = double.NegativeInfinity,
      _height = double.NegativeInfinity
    };
  }
}
