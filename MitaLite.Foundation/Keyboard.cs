// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Keyboard
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MS.Internal.Mita.Foundation {
    public class Keyboard : ITextInput {
        public enum KeyState {
            Up,
            Down,
            Toggled
        }

        const string specialChars = "+^%~(){}";
        static readonly Regex specialCharacterPattern = new Regex(pattern: "(?<specialChar>[" + Regex.Escape(str: "+^%~(){}") + "])", options: RegexOptions.Compiled);
        static readonly object sendKeysLock = new object();
        static readonly object classLock = new object();
        static Keyboard singletonInstance;
        internal static INativeMethods nativeMethods;
        readonly IInputDevice inputDevice;
        readonly IInputQueue inputQueue;
        readonly KeyTranslator keyTranslator = KeyTranslator.Instance;

        protected Keyboard() {
            nativeMethods = new NativeMethods();
            this.inputQueue = new InputQueue();
            this.inputDevice = new InputDeviceFactory().Get(type: INPUT_DEVICE_TYPE.KEYBOARD);
        }

        public static Keyboard Instance {
            get {
                if (singletonInstance == null)
                    lock (classLock) {
                        if (singletonInstance == null)
                            singletonInstance = new Keyboard();
                    }

                return singletonInstance;
            }
        }

        public bool TurnOffToggleKeys { get; set; } = true;

        public static int SendKeysDelay { get; set; } = 20;

        public void SendText(string text) {
            SendKeys(text: text);
        }

        void SendKeys(string text) {
            lock (sendKeysLock) {
                List<IInputAction> inputs;
                if (!ParseText(text: text, inputs: out inputs))
                    throw new ArgumentException(message: StringResource.Get(id: "SendKeysStringMalformed"), paramName: nameof(text));
                if (TurnOffToggleKeys) {
                    List<IInputAction> turnOffLockInputs;
                    List<IInputAction> restoreLockInputs;
                    GetLockToggleInputs(turnOffLockInputs: out turnOffLockInputs, restoreLockInputs: out restoreLockInputs);
                    inputs.InsertRange(index: 0, collection: turnOffLockInputs);
                    inputs.AddRange(collection: restoreLockInputs);
                }

                this.inputQueue.Process(inputDevice: this.inputDevice, inputList: inputs);
            }
        }

        public static KeyState GetKeyState(VirtualKey virtualKeyCode) {
            var keyState1 = new byte[256];
            var keyState2 = KeyState.Up;
            nativeMethods.GetKeyboardState(keyState: keyState1);
            var num = keyState1[(int) virtualKeyCode];
            if ((num & 1) == 1)
                keyState2 = KeyState.Toggled;
            else if ((num & 128) == 128)
                keyState2 = KeyState.Down;
            return keyState2;
        }

        public static string EscapeSpecialCharacters(string text) {
            return specialCharacterPattern.Replace(input: text, replacement: "{${specialChar}}");
        }

        void GetLockToggleInputs(
            out List<IInputAction> turnOffLockInputs,
            out List<IInputAction> restoreLockInputs) {
            turnOffLockInputs = new List<IInputAction>();
            restoreLockInputs = new List<IInputAction>();
            if (GetKeyState(virtualKeyCode: VirtualKey.VK_NUMLOCK) == KeyState.Toggled) {
                turnOffLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_NUMLOCK, action: KeyAction.Press, duration: SendKeysDelay));
                turnOffLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_NUMLOCK, action: KeyAction.Release, duration: SendKeysDelay));
                restoreLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_NUMLOCK, action: KeyAction.Press, duration: SendKeysDelay));
                restoreLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_NUMLOCK, action: KeyAction.Release, duration: SendKeysDelay));
            }

            if (GetKeyState(virtualKeyCode: VirtualKey.VK_CAPITAL) == KeyState.Toggled) {
                turnOffLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_CAPITAL, action: KeyAction.Press, duration: SendKeysDelay));
                turnOffLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_CAPITAL, action: KeyAction.Release, duration: SendKeysDelay));
                restoreLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_CAPITAL, action: KeyAction.Press, duration: SendKeysDelay));
                restoreLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_CAPITAL, action: KeyAction.Release, duration: SendKeysDelay));
            }

            if (GetKeyState(virtualKeyCode: VirtualKey.VK_SCROLL) == KeyState.Toggled) {
                turnOffLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_SCROLL, action: KeyAction.Press, duration: SendKeysDelay));
                turnOffLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_SCROLL, action: KeyAction.Release, duration: SendKeysDelay));
                restoreLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_SCROLL, action: KeyAction.Press, duration: SendKeysDelay));
                restoreLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_SCROLL, action: KeyAction.Release, duration: SendKeysDelay));
            }

            if (GetKeyState(virtualKeyCode: VirtualKey.VK_KANA) != KeyState.Toggled)
                return;
            turnOffLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_KANA, action: KeyAction.Press, duration: SendKeysDelay));
            turnOffLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_KANA, action: KeyAction.Release, duration: SendKeysDelay));
            restoreLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_KANA, action: KeyAction.Press, duration: SendKeysDelay));
            restoreLockInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_KANA, action: KeyAction.Release, duration: SendKeysDelay));
        }

        bool IsChar(char character, string characterSet) {
            return character != char.MinValue && characterSet.IndexOf(value: character) != -1;
        }

        bool IsSpecial(char character) {
            return IsChar(character: character, characterSet: "+^%~(){}");
        }

        bool ParseEOS(ParsePosition position) {
            return position.ReachedEnd;
        }

        bool ParseWhitespace(ParsePosition position) {
            if (!char.IsWhiteSpace(c: position.CurrentChar))
                return false;
            position.MoveNext();
            return true;
        }

        bool ParseWhitespaces(ParsePosition position) {
            if (!ParseWhitespace(position: position))
                return false;
            do {
                ;
            } while (ParseWhitespace(position: position));

            return true;
        }

        bool ParseDigit(ParsePosition position, out int digit) {
            digit = 0;
            if (!char.IsDigit(c: position.CurrentChar))
                return false;
            digit = Convert.ToByte(value: position.CurrentChar) - Convert.ToByte(value: '0');
            position.MoveNext();
            return true;
        }

        bool ParseNumber(ParsePosition position, ref int number) {
            var position1 = (ParsePosition) position.Clone();
            var num = 1;
            if (position1.CurrentChar == '+' || position1.CurrentChar == '-') {
                num = position1.CurrentChar == '+' ? 1 : -1;
                position1.MoveNext();
            }

            var digit = 0;
            if (!ParseDigit(position: position1, digit: out digit))
                return false;
            number = digit;
            position.Skip(count: position1.CurrentIndex);
            while (ParseDigit(position: position, digit: out digit))
                number = 10 * number + digit;
            number *= num;
            return true;
        }

        bool ParseDownUp(ParsePosition position, ref KeyAction keyAction) {
            if (position.GetRightString(length: 4) == "DOWN") {
                keyAction = KeyAction.Press;
                position.Skip(count: 4);
                return true;
            }

            if (!(position.GetRightString(length: 2) == "UP"))
                return false;
            keyAction = KeyAction.Release;
            position.Skip(count: 2);
            return true;
        }

        bool ParseCurlyPostfix(
            ParsePosition position,
            ref int repetitionCount,
            ref KeyAction keyAction) {
            return ParseNumber(position: position, number: ref repetitionCount) || ParseDownUp(position: position, keyAction: ref keyAction);
        }

        bool ParseSpecialChar(ParsePosition position, List<IInputAction> pressInputs) {
            if (!IsSpecial(character: position.CurrentChar))
                return false;
            pressInputs.AddRange(collection: Input.CreateKeyInput(character: position.CurrentChar, duration: SendKeysDelay));
            position.MoveNext();
            return true;
        }

        bool ParsePrintable(
            ParsePosition position,
            List<IInputAction> pressInputs,
            List<IInputAction> releaseInputs) {
            if (position.CurrentChar == char.MinValue || position.CurrentChar != '~' && IsSpecial(character: position.CurrentChar))
                return false;
            if (position.CurrentChar == '~') {
                pressInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_RETURN, action: KeyAction.Press, duration: SendKeysDelay));
                releaseInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_RETURN, action: KeyAction.Release, duration: SendKeysDelay));
            } else {
                var virtualAlphaNumericKey = this.keyTranslator.GetVirtualAlphaNumericKey(alphaNumKey: position.CurrentChar);
                if (virtualAlphaNumericKey != VirtualKey.VK_NONE)
                    pressInputs.AddRange(collection: Input.CreateKeyInput(key: virtualAlphaNumericKey, action: KeyAction.PressAndRelease, duration: SendKeysDelay));
                else
                    pressInputs.AddRange(collection: Input.CreateKeyInput(character: position.CurrentChar, duration: SendKeysDelay));
            }

            position.MoveNext();
            return true;
        }

        bool ParseNonPrintable(
            ParsePosition position,
            List<IInputAction> pressInputs,
            List<IInputAction> releaseInputs) {
            var separator = new char[1] {'}'};
            var strArray1 = position.ToString().Split(separator: (char[]) null, options: StringSplitOptions.RemoveEmptyEntries);
            if (strArray1.Length != 0) {
                var strArray2 = strArray1[0].Split(separator: separator, options: StringSplitOptions.RemoveEmptyEntries);
                if (strArray2.Length != 0) {
                    var str = strArray2[0];
                    if (this.keyTranslator.IsNonPrintableName(name: str)) {
                        pressInputs.AddRange(collection: Input.CreateKeyInput(key: this.keyTranslator.GetVirtualKey(keyName: str), action: KeyAction.Press, duration: SendKeysDelay));
                        releaseInputs.AddRange(collection: Input.CreateKeyInput(key: this.keyTranslator.GetVirtualKey(keyName: str), action: KeyAction.Release, duration: SendKeysDelay));
                        position.Skip(count: str.Length);
                        return true;
                    }
                }
            }

            return false;
        }

        bool ParseCurly(ParsePosition position, List<IInputAction> inputs) {
            if (position.CurrentChar != '{')
                return false;
            var position1 = (ParsePosition) position.Clone();
            position1.MoveNext();
            var pressInputs = new List<IInputAction>();
            var releaseInputs = new List<IInputAction>();
            if (!ParseSpecialChar(position: position1, pressInputs: pressInputs) && !ParseNonPrintable(position: position1, pressInputs: pressInputs, releaseInputs: releaseInputs) && !ParsePrintable(position: position1, pressInputs: pressInputs, releaseInputs: releaseInputs))
                return false;
            var repetitionCount = 1;
            var keyAction = KeyAction.PressAndRelease;
            if (ParseWhitespaces(position: position1) && !ParseCurlyPostfix(position: position1, repetitionCount: ref repetitionCount, keyAction: ref keyAction) || position1.CurrentChar != '}')
                return false;
            position1.MoveNext();
            position.Skip(count: position1.CurrentIndex);
            for (var index = 0; index < repetitionCount; ++index)
                switch (keyAction) {
                    case KeyAction.Press:
                        inputs.AddRange(collection: pressInputs);
                        break;
                    case KeyAction.Release:
                        inputs.AddRange(collection: releaseInputs);
                        break;
                    case KeyAction.PressAndRelease:
                        inputs.AddRange(collection: pressInputs);
                        inputs.AddRange(collection: releaseInputs);
                        break;
                }

            return true;
        }

        bool ParseBlock(ParsePosition position, List<IInputAction> inputs) {
            if (position.CurrentChar != '(')
                return false;
            var position1 = (ParsePosition) position.Clone();
            position1.MoveNext();
            var inputs1 = new List<IInputAction>();
            ParseInnerText(position: position1, inputs: inputs1);
            if (position1.CurrentChar != ')')
                return false;
            position1.MoveNext();
            position.Skip(count: position1.CurrentIndex);
            inputs.AddRange(collection: inputs1);
            return true;
        }

        bool ParseInnerText(ParsePosition position, List<IInputAction> inputs) {
            do {
                do {
                    ;
                } while (ParseModifiers(position: position, inputs: inputs));

                var pressInputs = new List<IInputAction>();
                var releaseInputs = new List<IInputAction>();
                if (ParsePrintable(position: position, pressInputs: pressInputs, releaseInputs: releaseInputs)) {
                    inputs.AddRange(collection: pressInputs);
                    inputs.AddRange(collection: releaseInputs);
                }
            } while (ParseCurly(position: position, inputs: inputs) || ParseBlock(position: position, inputs: inputs));

            return true;
        }

        bool ParseModifier(
            ParsePosition position,
            List<IInputAction> pressInputs,
            List<IInputAction> releaseInputs) {
            switch (position.CurrentChar) {
                case '%':
                    pressInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_MENU, action: KeyAction.Press, duration: SendKeysDelay));
                    releaseInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_MENU, action: KeyAction.Release, duration: SendKeysDelay));
                    break;
                case '+':
                    pressInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_SHIFT, action: KeyAction.Press, duration: SendKeysDelay));
                    releaseInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_SHIFT, action: KeyAction.Release, duration: SendKeysDelay));
                    break;
                case '^':
                    pressInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_CONTROL, action: KeyAction.Press, duration: SendKeysDelay));
                    releaseInputs.AddRange(collection: Input.CreateKeyInput(key: VirtualKey.VK_CONTROL, action: KeyAction.Release, duration: SendKeysDelay));
                    break;
                default:
                    return false;
            }

            position.MoveNext();
            return true;
        }

        bool ParseModifiers(ParsePosition position, List<IInputAction> inputs) {
            var position1 = (ParsePosition) position.Clone();
            var inputActionList = new List<IInputAction>();
            var releaseInputs = new List<IInputAction>();
            if (!ParseModifier(position: position1, pressInputs: inputActionList, releaseInputs: releaseInputs))
                return false;
            position.Skip(count: position1.CurrentIndex);
            do {
                ;
            } while (ParseModifier(position: position, pressInputs: inputActionList, releaseInputs: releaseInputs));

            if (!ParsePrintable(position: position, pressInputs: inputActionList, releaseInputs: releaseInputs) && !ParseCurly(position: position, inputs: inputActionList))
                ParseBlock(position: position, inputs: inputActionList);
            inputs.AddRange(collection: inputActionList);
            inputs.AddRange(collection: releaseInputs);
            return true;
        }

        internal bool ParseText(string text, out List<IInputAction> inputs) {
            using (var position = new ParsePosition(parseText: text)) {
                position.MoveNext();
                inputs = new List<IInputAction>();
                ParseInnerText(position: position, inputs: inputs);
                return ParseEOS(position: position);
            }
        }

        internal class ParsePosition : IEnumerator, IDisposable {
            readonly IEnumerator<char> charEnumerator;
            bool currentValid;
            readonly string parseText = string.Empty;

            ParsePosition() {
            }

            public ParsePosition(string parseText) {
                this.parseText = parseText;
                this.charEnumerator = ((IEnumerable<char>) parseText.ToCharArray()).GetEnumerator();
            }

            ParsePosition(string parseText, bool currentValid, int currentIndex)
                : this(parseText: parseText) {
                this.currentValid = currentValid;
                if (currentValid)
                    this.charEnumerator.MoveNext();
                CurrentIndex = currentIndex == -1 ? -1 : 0;
            }

            public char CurrentChar {
                get { return !this.currentValid ? char.MinValue : this.charEnumerator.Current; }
            }

            public int CurrentIndex { get; set; } = -1;

            public bool ReachedEnd {
                get { return !this.currentValid; }
            }

            public void Dispose() {
                this.charEnumerator.Dispose();
            }

            public bool MoveNext() {
                if (this.currentValid || CurrentIndex == -1)
                    ++CurrentIndex;
                this.currentValid = this.charEnumerator.MoveNext();
                return this.currentValid;
            }

            public object Current {
                get { return CurrentChar; }
            }

            public void Reset() {
                this.charEnumerator.Reset();
                this.currentValid = false;
                CurrentIndex = -1;
            }

            public object Clone() {
                return CurrentIndex == -1 ? new ParsePosition(parseText: this.parseText, currentValid: this.currentValid, currentIndex: CurrentIndex) : (object) new ParsePosition(parseText: this.parseText.Substring(startIndex: CurrentIndex), currentValid: this.currentValid, currentIndex: CurrentIndex);
            }

            public bool Skip(int count) {
                for (var index = 0; index < count; ++index)
                    if (!MoveNext())
                        return false;
                return true;
            }

            public string GetRightString(int length) {
                return GetRightString(startIndex: 0, length: length);
            }

            public string GetRightString(int startIndex, int length) {
                var stringBuilder = new StringBuilder(capacity: length);
                var parsePosition = (ParsePosition) Clone();
                parsePosition.Skip(count: startIndex);
                for (var index = 0; index < length && !parsePosition.ReachedEnd; ++index) {
                    stringBuilder.Append(value: parsePosition.CurrentChar);
                    parsePosition.MoveNext();
                }

                return stringBuilder.ToString();
            }

            public char GetRightCharacter(int index) {
                var parsePosition = (ParsePosition) Clone();
                parsePosition.Skip(count: index);
                return parsePosition.CurrentChar;
            }

            public override string ToString() {
                return CurrentIndex == -1 ? this.parseText.Substring(startIndex: 0) : this.parseText.Substring(startIndex: CurrentIndex);
            }
        }
    }
}