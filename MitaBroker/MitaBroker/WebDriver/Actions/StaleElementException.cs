﻿// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.StaleElementException
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;

namespace MitaBroker.WebDriver.Actions {
    internal class StaleElementException : Exception {
        public StaleElementException(string message)
            : base(message: message) {
        }
    }
}