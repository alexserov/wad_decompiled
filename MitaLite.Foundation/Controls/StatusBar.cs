// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.StatusBar
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class StatusBar : StatusBar<UIObject> {
        static IFactory<StatusBar> _factory;

        public StatusBar(UIObject uiObject)
            : base(uiObject: uiObject, itemFactory: UIObject.Factory) {
        }

        internal StatusBar(AutomationElement element)
            : base(element: element, itemFactory: UIObject.Factory) {
        }

        public static IFactory<StatusBar> Factory {
            get {
                if (_factory == null)
                    _factory = new StatusBarFactory();
                return _factory;
            }
        }

        class StatusBarFactory : IFactory<StatusBar> {
            public StatusBar Create(UIObject element) {
                return new StatusBar(uiObject: element);
            }
        }
    }
}