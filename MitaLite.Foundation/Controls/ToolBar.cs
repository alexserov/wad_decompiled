﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ToolBar
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class ToolBar : UIObject {
        static IFactory<ToolBar> _factory;

        public ToolBar(UIObject uiObject)
            : base(uiObject: uiObject) {
        }

        internal ToolBar(AutomationElement element)
            : base(element: element) {
        }

        public static IFactory<ToolBar> Factory {
            get {
                if (_factory == null)
                    _factory = new ToolBarFactory();
                return _factory;
            }
        }

        class ToolBarFactory : IFactory<ToolBar> {
            public ToolBar Create(UIObject element) {
                return new ToolBar(uiObject: element);
            }
        }
    }
}