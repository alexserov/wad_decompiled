// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.InputSource
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MitaBroker.WebDriver.Actions.Enums;

namespace MitaBroker.WebDriver.Actions {
    internal abstract class InputSource {
        public string Id { get; protected set; }

        public InputSourceType Type { get; protected set; }
    }
}