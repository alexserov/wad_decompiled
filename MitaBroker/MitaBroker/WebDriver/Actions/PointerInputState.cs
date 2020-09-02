// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.PointerInputState
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System.Collections.Generic;
using MitaBroker.WebDriver.Actions.Enums;

namespace MitaBroker.WebDriver.Actions {
    internal class PointerInputState : NullInputState {
        public PointerInputState(PointerType subtype) {
            Subtype = subtype;
            Pressed = new List<int>();
            X = Y = 0;
            Pressure = new float?();
            var nullable1 = new int?();
            Twist = nullable1;
            TiltX = TiltY = nullable1;
            var nullable2 = new double?();
            Height = nullable2;
            Width = nullable2;
            PointerId = -1;
        }

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
    }
}