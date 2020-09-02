// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.Log
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation.Utilities
{
  public static class Log
  {
    public static Action<string, object[]> OutImplementation = (Action<string, object[]>) null;
    internal static Func<string, string> CreateUniqueObjectIdImplementation = (Func<string, string>) (s => string.Format("{0}#{1}", (object) s, (object) Log.objCount++));
    private static long objCount = 0;

    internal static void Out(string msg, params object[] args)
    {
      Action<string, object[]> outImplementation = Log.OutImplementation;
      if (outImplementation == null)
        return;
      outImplementation(msg, args);
    }

    internal static string CreateUniqueObjectId(string baseName) => Log.CreateUniqueObjectIdImplementation(baseName);

    public static void DisplayVisualTree(UIObject root, uint maxDepth, uint depth = 0)
    {
      UIObject uiObject = root;
      if (uiObject == (UIObject) null)
        uiObject = UIObject.Root;
      Log.Out("{0}{1} - {2} - {3}", (object) Log.Indent(depth), (object) uiObject, (object) uiObject.AutomationId, (object) uiObject.ControlType);
      if (depth >= maxDepth)
        return;
      ++depth;
      foreach (UIObject child in uiObject.Children)
        Log.DisplayVisualTree(child, maxDepth, depth);
    }

    private static string Indent(uint count) => "".PadLeft((int) count * 4);
  }
}
