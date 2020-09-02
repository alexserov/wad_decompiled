// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.TransformPattern
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using UIAutomationClient;

namespace System.Windows.Automation {
    public class TransformPattern : BasePattern {
        public static readonly AutomationPattern Pattern = TransformPatternIdentifiers.Pattern;
        public static readonly AutomationProperty CanMoveProperty = TransformPatternIdentifiers.CanMoveProperty;
        public static readonly AutomationProperty CanResizeProperty = TransformPatternIdentifiers.CanResizeProperty;
        public static readonly AutomationProperty CanRotateProperty = TransformPatternIdentifiers.CanRotateProperty;
        readonly IUIAutomationTransformPattern _transformPattern;

        internal TransformPattern(
            AutomationElement element,
            IUIAutomationTransformPattern transformPattern)
            : base(el: element) {
            this._transformPattern = transformPattern;
        }

        public TransformPatternInformation Cached {
            get { return new TransformPatternInformation(el: this._el, useCache: true); }
        }

        public TransformPatternInformation Current {
            get { return new TransformPatternInformation(el: this._el, useCache: false); }
        }

        internal static TransformPattern Wrap(
            AutomationElement element,
            IUIAutomationTransformPattern transformPattern) {
            return new TransformPattern(element: element, transformPattern: transformPattern);
        }

        public void Move(double x, double y) {
            this._transformPattern.Move(x: x, y: y);
        }

        public void Resize(double width, double height) {
            this._transformPattern.Resize(width: width, height: height);
        }

        public void Rotate(double degrees) {
            this._transformPattern.Rotate(degrees: degrees);
        }

        public struct TransformPatternInformation {
            readonly AutomationElement _el;
            readonly bool _useCache;

            internal TransformPatternInformation(AutomationElement el, bool useCache) {
                this._el = el;
                this._useCache = useCache;
            }

            public bool CanMove {
                get { return (bool) this._el.GetPatternPropertyValue(property: CanMoveProperty, useCache: this._useCache); }
            }

            public bool CanResize {
                get { return (bool) this._el.GetPatternPropertyValue(property: CanResizeProperty, useCache: this._useCache); }
            }

            public bool CanRotate {
                get { return (bool) this._el.GetPatternPropertyValue(property: CanRotateProperty, useCache: this._useCache); }
            }
        }
    }
}