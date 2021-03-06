﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.Image
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls {
    public class Image : UIObject {
        static IFactory<Image> _factory;

        public Image(UIObject uiObject)
            : base(uiObject: uiObject) {
        }

        internal Image(AutomationElement element)
            : base(element: element) {
        }

        public static IFactory<Image> Factory {
            get {
                if (_factory == null)
                    _factory = new ImageFactory();
                return _factory;
            }
        }

        class ImageFactory : IFactory<Image> {
            public Image Create(UIObject element) {
                return new Image(uiObject: element);
            }
        }
    }
}