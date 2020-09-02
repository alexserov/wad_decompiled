// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.RectangleI
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation
{
  public struct RectangleI
  {
    internal int _x;
    internal int _y;
    internal int _width;
    internal int _height;
    private static readonly RectangleI s_empty = RectangleI.CreateEmptyRect();

    public static bool operator ==(RectangleI rect1, RectangleI rect2) => rect1.X == rect2.X && rect1.Y == rect2.Y && rect1.Width == rect2.Width && rect1.Height == rect2.Height;

    public static bool operator !=(RectangleI rect1, RectangleI rect2) => !(rect1 == rect2);

    public static bool Equals(RectangleI rect1, RectangleI rect2)
    {
      if (rect1.IsEmpty)
        return rect2.IsEmpty;
      return rect1.X.Equals(rect2.X) && rect1.Y.Equals(rect2.Y) && rect1.Width.Equals(rect2.Width) && rect1.Height.Equals(rect2.Height);
    }

    public override bool Equals(object o) => o != null && o is RectangleI rect2 && RectangleI.Equals(this, rect2);

    public bool Equals(RectangleI value) => RectangleI.Equals(this, value);

    public override int GetHashCode()
    {
      if (this.IsEmpty)
        return 0;
      int hashCode1 = this.X.GetHashCode();
      int num1 = this.Y;
      int hashCode2 = num1.GetHashCode();
      int num2 = hashCode1 ^ hashCode2;
      num1 = this.Width;
      int hashCode3 = num1.GetHashCode();
      int num3 = num2 ^ hashCode3;
      num1 = this.Height;
      int hashCode4 = num1.GetHashCode();
      return num3 ^ hashCode4;
    }

    public RectangleI(PointI location, SizeI size)
    {
      if (size.IsEmpty)
      {
        this = RectangleI.s_empty;
      }
      else
      {
        this._x = location._x;
        this._y = location._y;
        this._width = size._width;
        this._height = size._height;
      }
    }

    public RectangleI(int x, int y, int width, int height)
    {
      if ((double) width < 0.0 || (double) height < 0.0)
        throw new ArgumentException("Size_WidthAndHeightCannotBeNegative");
      this._x = x;
      this._y = y;
      this._width = width;
      this._height = height;
    }

    public RectangleI(PointI point1, PointI point2)
    {
      this._x = Math.Min(point1._x, point2._x);
      this._y = Math.Min(point1._y, point2._y);
      this._width = Math.Max(Math.Max(point1._x, point2._x) - this._x, 0);
      this._height = Math.Max(Math.Max(point1._y, point2._y) - this._y, 0);
    }

    public RectangleI(SizeI size)
    {
      if (size.IsEmpty)
      {
        this = RectangleI.s_empty;
      }
      else
      {
        this._x = this._y = 0;
        this._width = size.Width;
        this._height = size.Height;
      }
    }

    public static RectangleI Empty => RectangleI.s_empty;

    public bool IsEmpty => (double) this._width < 0.0;

    public PointI Location
    {
      get => new PointI(this._x, this._y);
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
        this._x = value._x;
        this._y = value._y;
      }
    }

    public SizeI Size
    {
      get => this.IsEmpty ? SizeI.Empty : new SizeI(this._width, this._height);
      set
      {
        if (value.IsEmpty)
        {
          this = RectangleI.s_empty;
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

    public int X
    {
      get => this._x;
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
        this._x = value;
      }
    }

    public int Y
    {
      get => this._y;
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
        this._y = value;
      }
    }

    public int Width
    {
      get => this._width;
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
        this._width = (double) value >= 0.0 ? value : throw new ArgumentException("Size_WidthCannotBeNegative");
      }
    }

    public int Height
    {
      get => this._height;
      set
      {
        if (this.IsEmpty)
          throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
        this._height = (double) value >= 0.0 ? value : throw new ArgumentException("Size_HeightCannotBeNegative");
      }
    }

    public int Left => this._x;

    public int Top => this._y;

    public int Right => this._x + this._width;

    public int Bottom => this._y + this._height;

    public PointI TopLeft => new PointI(this.Left, this.Top);

    public PointI TopRight => new PointI(this.Right, this.Top);

    public PointI BottomLeft => new PointI(this.Left, this.Bottom);

    public PointI BottomRight => new PointI(this.Right, this.Bottom);

    public bool Contains(PointI point) => this.Contains(point._x, point._y);

    public bool Contains(int x, int y) => !this.IsEmpty && this.ContainsInternal(x, y);

    public bool Contains(RectangleI rect) => !this.IsEmpty && !rect.IsEmpty && (this._x <= rect._x && this._y <= rect._y) && this._x + this._width >= rect._x + rect._width && this._y + this._height >= rect._y + rect._height;

    public bool IntersectsWith(RectangleI rect) => !this.IsEmpty && !rect.IsEmpty && (rect.Left <= this.Right && rect.Right >= this.Left) && rect.Top <= this.Bottom && rect.Bottom >= this.Top;

    public void Intersect(RectangleI rect)
    {
      if (!this.IntersectsWith(rect))
      {
        this = RectangleI.Empty;
      }
      else
      {
        int num1 = Math.Max(this.Left, rect.Left);
        int num2 = Math.Max(this.Top, rect.Top);
        this._width = Math.Max(Math.Min(this.Right, rect.Right) - num1, 0);
        this._height = Math.Max(Math.Min(this.Bottom, rect.Bottom) - num2, 0);
        this._x = num1;
        this._y = num2;
      }
    }

    public static RectangleI Intersect(RectangleI rect1, RectangleI rect2)
    {
      rect1.Intersect(rect2);
      return rect1;
    }

    public void Union(RectangleI rect)
    {
      if (this.IsEmpty)
      {
        this = rect;
      }
      else
      {
        if (rect.IsEmpty)
          return;
        int num1 = Math.Min(this.Left, rect.Left);
        int num2 = Math.Min(this.Top, rect.Top);
        this._width = Math.Max(Math.Max(this.Right, rect.Right) - num1, 0);
        this._height = Math.Max(Math.Max(this.Bottom, rect.Bottom) - num2, 0);
        this._x = num1;
        this._y = num2;
      }
    }

    public static RectangleI Union(RectangleI rect1, RectangleI rect2)
    {
      rect1.Union(rect2);
      return rect1;
    }

    public void Union(PointI point) => this.Union(new RectangleI(point, point));

    public static RectangleI Union(RectangleI rect, PointI point)
    {
      rect.Union(new RectangleI(point, point));
      return rect;
    }

    public void Offset(int offsetX, int offsetY)
    {
      if (this.IsEmpty)
        throw new InvalidOperationException("Rect_CannotCallMethod");
      this._x += offsetX;
      this._y += offsetY;
    }

    public static RectangleI Offset(RectangleI rect, int offsetX, int offsetY)
    {
      rect.Offset(offsetX, offsetY);
      return rect;
    }

    public void Inflate(SizeI size) => this.Inflate(size._width, size._height);

    public void Inflate(int width, int height)
    {
      if (this.IsEmpty)
        throw new InvalidOperationException("Rect_CannotCallMethod");
      this._x -= width;
      this._y -= height;
      this._width += width;
      this._width += width;
      this._height += height;
      this._height += height;
      if ((double) this._width >= 0.0 && (double) this._height >= 0.0)
        return;
      this = RectangleI.s_empty;
    }

    public override string ToString() => string.Format("Left:{0} Top:{1} Width:{2} Height:{3}", (object) this._x, (object) this._y, (object) this._width, (object) this._height);

    public static RectangleI Inflate(RectangleI rect, SizeI size)
    {
      rect.Inflate(size._width, size._height);
      return rect;
    }

    public static RectangleI Inflate(RectangleI rect, int width, int height)
    {
      rect.Inflate(width, height);
      return rect;
    }

    public void Scale(int scaleX, int scaleY)
    {
      if (this.IsEmpty)
        return;
      this._x *= scaleX;
      this._y *= scaleY;
      this._width *= scaleX;
      this._height *= scaleY;
      if ((double) scaleX < 0.0)
      {
        this._x += this._width;
        this._width *= -1;
      }
      if ((double) scaleY >= 0.0)
        return;
      this._y += this._height;
      this._height *= -1;
    }

    private bool ContainsInternal(int x, int y) => x >= this._x && x - this._width <= this._x && y >= this._y && y - this._height <= this._y;

    private static RectangleI CreateEmptyRect() => new RectangleI()
    {
      _x = 0,
      _y = 0,
      _width = 0,
      _height = 0
    };
  }
}
