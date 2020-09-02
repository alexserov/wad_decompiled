// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.KeyInputState
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System.Collections.Generic;

namespace MitaBroker.WebDriver.Actions {
    internal class KeyInputState : NullInputState {
        public KeyInputState() {
            Pressed = new HashSet<string>();
            Alt = Shift = Ctrl = Meta = false;
        }

        public HashSet<string> Pressed { get; set; }

        public bool Alt { get; set; }

        public bool Shift { get; set; }

        public bool Ctrl { get; set; }

        public bool Meta { get; set; }
    }
}