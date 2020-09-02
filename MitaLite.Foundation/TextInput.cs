// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.TextInput
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation
{
  public static class TextInput
  {
    private static Stack<ITextInput> _textInputStack = new Stack<ITextInput>();

    public static void SendText(string text) => TextInput.Current.SendText(text);

    public static IDisposable Activate(ITextInput textInput) => (IDisposable) new InputControllerMartyr<ITextInput>(TextInput._textInputStack, textInput);

    public static ITextInput Current
    {
      get
      {
        if (TextInput._textInputStack.Count == 0)
        {
          lock (TextInput._textInputStack)
          {
            if (TextInput._textInputStack.Count == 0)
              TextInput._textInputStack.Push((ITextInput) Keyboard.Instance);
          }
        }
        return TextInput._textInputStack.Peek();
      }
    }
  }
}
