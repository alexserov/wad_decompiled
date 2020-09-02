// Decompiled with JetBrains decompiler
// Type: UIAutomationAdapter.Utilities.ArrayMarshaller
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System;
using System.Linq;

namespace UIAutomationAdapter.Utilities
{
  internal static class ArrayMarshaller
  {
    public static T[] ToTypedArray<T>(this Array value) => value.Cast<T>().ToArray<T>();
  }
}
