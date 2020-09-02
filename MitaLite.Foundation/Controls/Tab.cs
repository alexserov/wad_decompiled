// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Tab
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class Tab : Tab<Tab, TabItem> {
        static IFactory<Tab> _factory;

        public Tab(UIObject uiObject)
            : base(uiObject: uiObject, itemFactory: TabItem.Factory) {
        }

        internal Tab(AutomationElement element)
            : base(element: element, itemFactory: TabItem.Factory) {
        }

        public static IFactory<Tab> Factory {
            get {
                if (_factory == null)
                    _factory = new TabFactory();
                return _factory;
            }
        }

        class TabFactory : IFactory<Tab> {
            public Tab Create(UIObject element) => new Tab(uiObject: element);
        }
    }
}