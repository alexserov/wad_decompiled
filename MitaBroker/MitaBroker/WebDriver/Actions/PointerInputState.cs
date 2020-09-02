// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.PointerInputState
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MitaBroker.WebDriver.Actions.Enums;
using System.Collections.Generic;

namespace MitaBroker.WebDriver.Actions
{
  internal class PointerInputState : NullInputState
  {
    public PointerType Subtype { get; set; }

    public List<int> Pressed { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public float? Pressure { get; set; }

    public int? Twist { get; set; }

    public int? TiltX { get; set; }

    public int? TiltY { get; set; }

    public double? Width { get; set; }

    public double? Height { get; set; }

    public int PointerId { get; set; }

    public PointerInputState(PointerType subtype)
    {
      this.Subtype = subtype;
      this.Pressed = new List<int>();
      this.X = this.Y = 0;
      this.Pressure = new float?();
      int? nullable1 = new int?();
      this.Twist = nullable1;
      this.TiltX = this.TiltY = nullable1;
      double? nullable2 = new double?();
      this.Height = nullable2;
      this.Width = nullable2;
      this.PointerId = -1;
    }
  }
}
