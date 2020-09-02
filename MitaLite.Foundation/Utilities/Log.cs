// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.Log
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation.Utilities {
    public static class Log {
        public static Action<string, object[]> OutImplementation = null;
        internal static Func<string, string> CreateUniqueObjectIdImplementation = s => string.Format(format: "{0}#{1}", arg0: s, arg1: objCount++);
        static long objCount;

        internal static void Out(string msg, params object[] args) {
            var outImplementation = OutImplementation;
            if (outImplementation == null)
                return;
            outImplementation(arg1: msg, arg2: args);
        }

        internal static string CreateUniqueObjectId(string baseName) {
            return CreateUniqueObjectIdImplementation(arg: baseName);
        }

        public static void DisplayVisualTree(UIObject root, uint maxDepth, uint depth = 0) {
            var uiObject = root;
            if (uiObject == null)
                uiObject = UIObject.Root;
            Out(msg: "{0}{1} - {2} - {3}", (object) Indent(count: depth), (object) uiObject, (object) uiObject.AutomationId, (object) uiObject.ControlType);
            if (depth >= maxDepth)
                return;
            ++depth;
            foreach (var child in uiObject.Children)
                DisplayVisualTree(root: child, maxDepth: maxDepth, depth: depth);
        }

        static string Indent(uint count) {
            return "".PadLeft(totalWidth: (int) count * 4);
        }
    }
}