// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Context
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public class Context {
        [ThreadStatic]
        static Stack<Context> _stack;

        static readonly Context _defaultContext = ControlContext;

        public Context(UICondition treeCondition) {
            Validate.ArgumentNotNull(parameter: treeCondition, parameterName: nameof(treeCondition));
            IsActivated = false;
            TreeCondition = treeCondition;
        }

        public bool IsActivated { get; set; }

        public UICondition TreeCondition { get; }

        public static Context Current {
            get { return Stack.Peek(); }
        }

        public static Context RawContext { get; } = new Context(treeCondition: UICondition.RawTree);

        public static Context ContentContext { get; } = new Context(treeCondition: UICondition.ContentTree);

        public static Context ControlContext { get; } = new Context(treeCondition: UICondition.ControlTree);

        internal static Stack<Context> Stack {
            get {
                if (_stack == null) {
                    _stack = new Stack<Context>();
                    _stack.Push(item: _defaultContext);
                }

                return _stack;
            }
        }

        public IDisposable Activate() {
            return new ContextMartyr(currentContext: this);
        }
    }
}