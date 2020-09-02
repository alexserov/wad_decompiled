// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.PointerInputSource
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using MitaBroker.WebDriver.Actions.Enums;

namespace MitaBroker.WebDriver.Actions {
    internal class PointerInputSource : InputSource {
        public PointerInputSource(string id, PointerType pointerType) {
            Id = id;
            Type = InputSourceType.Pointer;
            PointerType = pointerType;
        }

        public PointerType PointerType { get; }

        public void PointerDown() {
            throw new NotImplementedException(message: "PointerInputSource PointerDown is not implemented");
        }

        public void PointerUp() {
            throw new NotImplementedException(message: "PointerInputSource PointerUp is not implemented");
        }

        public void PointerMove() {
            throw new NotImplementedException(message: "PointerInputSource PointerMove is not implemented");
        }

        public void PointerCancel() {
            throw new NotImplementedException(message: "PointerInputSource PointerCancel is not implemented");
        }
    }
}