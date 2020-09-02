// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.TextInput
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    public static class TextInput {
        static readonly Stack<ITextInput> _textInputStack = new Stack<ITextInput>();

        public static ITextInput Current {
            get {
                if (_textInputStack.Count == 0)
                    lock (_textInputStack) {
                        if (_textInputStack.Count == 0)
                            _textInputStack.Push(item: Keyboard.Instance);
                    }

                return _textInputStack.Peek();
            }
        }

        public static void SendText(string text) {
            Current.SendText(text: text);
        }

        public static IDisposable Activate(ITextInput textInput) {
            return new InputControllerMartyr<ITextInput>(inputStack: _textInputStack, inputController: textInput);
        }
    }
}