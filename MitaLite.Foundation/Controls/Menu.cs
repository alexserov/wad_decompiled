// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Menu
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class Menu : UIObject {
        static IFactory<Menu> _factory;

        public Menu(UIObject uiObject)
            : base(uiObject: uiObject) {
        }

        internal Menu(AutomationElement element)
            : base(element: element) {
        }

        public static IFactory<Menu> Factory {
            get {
                if (_factory == null)
                    _factory = new MenuFactory();
                return _factory;
            }
        }

        class MenuFactory : IFactory<Menu> {
            public Menu Create(UIObject element) {
                return new Menu(uiObject: element);
            }
        }
    }
}