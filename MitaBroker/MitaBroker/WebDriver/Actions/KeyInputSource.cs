// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.KeyInputSource
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using MitaBroker.WebDriver.Actions.Enums;

namespace MitaBroker.WebDriver.Actions {
    internal class KeyInputSource : InputSource {
        public KeyInputSource(string id) {
            Id = id;
            Type = InputSourceType.Key;
        }

        public void KeyDown() {
            throw new NotImplementedException(message: "KeyInputSource KeyDown is not implemented");
        }

        public void KeyUp() {
            throw new NotImplementedException(message: "KeyInputSource KeyUp is not implemented");
        }
    }
}