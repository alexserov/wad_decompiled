// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.PointerData
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

namespace MS.Internal.Mita.Foundation {
    public struct PointerData {
        public POINTER_FLAGS flags;
        public PointI location;
        public uint pointerId;
        public POINTER_INPUT_TYPE pointerType;
        public POINTER_PRESSED_BUTTON pressedButton;
        public int? width;
        public int? height;
        public int? tiltX;
        public int? tiltY;
        public uint? twist;
        public uint? pressure;

        public override string ToString() {
            return string.Format(format: "ID: {0} ({1}, {2}) flags: {3} pressedButton: {4}", (object) this.pointerId, (object) this.location.X, (object) this.location.Y, (object) this.flags, (object) this.pressedButton) + (this.pressure.HasValue ? string.Format(format: " pressure: {0}", arg0: this.pressure.Value) : string.Empty) + (this.twist.HasValue ? string.Format(format: " twist: {0}", arg0: this.twist.Value) : string.Empty) + (this.tiltX.HasValue ? string.Format(format: " tiltX: {0}", arg0: this.tiltX.Value) : string.Empty) + (this.tiltY.HasValue ? string.Format(format: " tiltY: {0}", arg0: this.tiltY.Value) : string.Empty) + (this.width.HasValue ? string.Format(format: " width: {0}", arg0: this.width.Value) : string.Empty) + (this.height.HasValue ? string.Format(format: " height: {0}", arg0: this.height.Value) : string.Empty);
        }
    }
}