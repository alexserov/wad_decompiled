// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.MenuBar
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Patterns;

namespace MS.Internal.Mita.Foundation.Controls {
    public class MenuBar : UIObject, IContainer<SubmenuItem> {
        static IFactory<MenuBar> _factory;
        static readonly UICondition _treeCondition = UICondition.Create(query: "@ControlType=MenuBar Or @ControlType=MenuItem");

        public MenuBar(UIObject uiObject)
            : base(uiObject: uiObject) {
        }

        internal MenuBar(AutomationElement element)
            : base(element: element) {
        }

        public static IFactory<MenuBar> Factory {
            get {
                if (_factory == null)
                    _factory = new MenuBarFactory();
                return _factory;
            }
        }

        public UICollection<SubmenuItem> Items {
            get { return new UIChildren<SubmenuItem>(root: this, treeCondition: _treeCondition, factory: SubmenuItem.Factory); }
        }

        class MenuBarFactory : IFactory<MenuBar> {
            public MenuBar Create(UIObject element) {
                return new MenuBar(uiObject: element);
            }
        }
    }
}