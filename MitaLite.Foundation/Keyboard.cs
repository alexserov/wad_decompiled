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

namespace MS.Internal.Mita.Foundation
{
  public class Keyboard : ITextInput
  {
    private bool turnOffToggleKeys = true;
    private static int sendKeysDelay = 20;
    private const string specialChars = "+^%~(){}";
    private static Regex specialCharacterPattern = new Regex("(?<specialChar>[" + Regex.Escape("+^%~(){}") + "])", RegexOptions.Compiled);
    private static object sendKeysLock = new object();
    private KeyTranslator keyTranslator = KeyTranslator.Instance;
    private static object classLock = new object();
    private static Keyboard singletonInstance;
    internal static INativeMethods nativeMethods = (INativeMethods) null;
    private IInputQueue inputQueue;
    private IInputDevice inputDevice;

    protected Keyboard()
    {
      Keyboard.nativeMethods = (INativeMethods) new NativeMethods();
      this.inputQueue = (IInputQueue) new InputQueue();
      this.inputDevice = new InputDeviceFactory().Get(INPUT_DEVICE_TYPE.KEYBOARD);
    }

    public static Keyboard Instance
    {
      get
      {
        if (Keyboard.singletonInstance == null)
        {
          lock (Keyboard.classLock)
          {
            if (Keyboard.singletonInstance == null)
              Keyboard.singletonInstance = new Keyboard();
          }
        }
        return Keyboard.singletonInstance;
      }
    }

    public void SendText(string text) => this.SendKeys(text);

    private void SendKeys(string text)
    {
      lock (Keyboard.sendKeysLock)
      {
        List<IInputAction> inputs;
        if (!this.ParseText(text, out inputs))
          throw new ArgumentException(StringResource.Get("SendKeysStringMalformed"), nameof (text));
        if (this.turnOffToggleKeys)
        {
          List<IInputAction> turnOffLockInputs;
          List<IInputAction> restoreLockInputs;
          this.GetLockToggleInputs(out turnOffLockInputs, out restoreLockInputs);
          inputs.InsertRange(0, (IEnumerable<IInputAction>) turnOffLockInputs);
          inputs.AddRange((IEnumerable<IInputAction>) restoreLockInputs);
        }
        this.inputQueue.Process(this.inputDevice, (IList<IInputAction>) inputs);
      }
    }

    public static Keyboard.KeyState GetKeyState(VirtualKey virtualKeyCode)
    {
      byte[] keyState1 = new byte[256];
      Keyboard.KeyState keyState2 = Keyboard.KeyState.Up;
      Keyboard.nativeMethods.GetKeyboardState(keyState1);
      byte num = keyState1[(int) virtualKeyCode];
      if (((int) num & 1) == 1)
        keyState2 = Keyboard.KeyState.Toggled;
      else if (((int) num & 128) == 128)
        keyState2 = Keyboard.KeyState.Down;
      return keyState2;
    }

    public static string EscapeSpecialCharacters(string text) => Keyboard.specialCharacterPattern.Replace(text, "{${specialChar}}");

    public bool TurnOffToggleKeys
    {
      get => this.turnOffToggleKeys;
      set => this.turnOffToggleKeys = value;
    }

    public static int SendKeysDelay
    {
      get => Keyboard.sendKeysDelay;
      set => Keyboard.sendKeysDelay = value;
    }

    private void GetLockToggleInputs(
      out List<IInputAction> turnOffLockInputs,
      out List<IInputAction> restoreLockInputs)
    {
      turnOffLockInputs = new List<IInputAction>();
      restoreLockInputs = new List<IInputAction>();
      if (Keyboard.GetKeyState(VirtualKey.VK_NUMLOCK) == Keyboard.KeyState.Toggled)
      {
        turnOffLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_NUMLOCK, KeyAction.Press, Keyboard.sendKeysDelay));
        turnOffLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_NUMLOCK, KeyAction.Release, Keyboard.sendKeysDelay));
        restoreLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_NUMLOCK, KeyAction.Press, Keyboard.sendKeysDelay));
        restoreLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_NUMLOCK, KeyAction.Release, Keyboard.sendKeysDelay));
      }
      if (Keyboard.GetKeyState(VirtualKey.VK_CAPITAL) == Keyboard.KeyState.Toggled)
      {
        turnOffLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_CAPITAL, KeyAction.Press, Keyboard.sendKeysDelay));
        turnOffLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_CAPITAL, KeyAction.Release, Keyboard.sendKeysDelay));
        restoreLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_CAPITAL, KeyAction.Press, Keyboard.sendKeysDelay));
        restoreLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_CAPITAL, KeyAction.Release, Keyboard.sendKeysDelay));
      }
      if (Keyboard.GetKeyState(VirtualKey.VK_SCROLL) == Keyboard.KeyState.Toggled)
      {
        turnOffLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_SCROLL, KeyAction.Press, Keyboard.sendKeysDelay));
        turnOffLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_SCROLL, KeyAction.Release, Keyboard.sendKeysDelay));
        restoreLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_SCROLL, KeyAction.Press, Keyboard.sendKeysDelay));
        restoreLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_SCROLL, KeyAction.Release, Keyboard.sendKeysDelay));
      }
      if (Keyboard.GetKeyState(VirtualKey.VK_KANA) != Keyboard.KeyState.Toggled)
        return;
      turnOffLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_KANA, KeyAction.Press, Keyboard.sendKeysDelay));
      turnOffLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_KANA, KeyAction.Release, Keyboard.sendKeysDelay));
      restoreLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_KANA, KeyAction.Press, Keyboard.sendKeysDelay));
      restoreLockInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_KANA, KeyAction.Release, Keyboard.sendKeysDelay));
    }

    private bool IsChar(char character, string characterSet) => character != char.MinValue && characterSet.IndexOf(character) != -1;

    private bool IsSpecial(char character) => this.IsChar(character, "+^%~(){}");

    private bool ParseEOS(Keyboard.ParsePosition position) => position.ReachedEnd;

    private bool ParseWhitespace(Keyboard.ParsePosition position)
    {
      if (!char.IsWhiteSpace(position.CurrentChar))
        return false;
      position.MoveNext();
      return true;
    }

    private bool ParseWhitespaces(Keyboard.ParsePosition position)
    {
      if (!this.ParseWhitespace(position))
        return false;
      do
        ;
      while (this.ParseWhitespace(position));
      return true;
    }

    private bool ParseDigit(Keyboard.ParsePosition position, out int digit)
    {
      digit = 0;
      if (!char.IsDigit(position.CurrentChar))
        return false;
      digit = (int) Convert.ToByte(position.CurrentChar) - (int) Convert.ToByte('0');
      position.MoveNext();
      return true;
    }

    private bool ParseNumber(Keyboard.ParsePosition position, ref int number)
    {
      Keyboard.ParsePosition position1 = (Keyboard.ParsePosition) position.Clone();
      int num = 1;
      if (position1.CurrentChar == '+' || position1.CurrentChar == '-')
      {
        num = position1.CurrentChar == '+' ? 1 : -1;
        position1.MoveNext();
      }
      int digit = 0;
      if (!this.ParseDigit(position1, out digit))
        return false;
      number = digit;
      position.Skip(position1.CurrentIndex);
      while (this.ParseDigit(position, out digit))
        number = 10 * number + digit;
      number *= num;
      return true;
    }

    private bool ParseDownUp(Keyboard.ParsePosition position, ref KeyAction keyAction)
    {
      if (position.GetRightString(4) == "DOWN")
      {
        keyAction = KeyAction.Press;
        position.Skip(4);
        return true;
      }
      if (!(position.GetRightString(2) == "UP"))
        return false;
      keyAction = KeyAction.Release;
      position.Skip(2);
      return true;
    }

    private bool ParseCurlyPostfix(
      Keyboard.ParsePosition position,
      ref int repetitionCount,
      ref KeyAction keyAction) => this.ParseNumber(position, ref repetitionCount) || this.ParseDownUp(position, ref keyAction);

    private bool ParseSpecialChar(Keyboard.ParsePosition position, List<IInputAction> pressInputs)
    {
      if (!this.IsSpecial(position.CurrentChar))
        return false;
      pressInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(position.CurrentChar, Keyboard.sendKeysDelay));
      position.MoveNext();
      return true;
    }

    private bool ParsePrintable(
      Keyboard.ParsePosition position,
      List<IInputAction> pressInputs,
      List<IInputAction> releaseInputs)
    {
      if (position.CurrentChar == char.MinValue || position.CurrentChar != '~' && this.IsSpecial(position.CurrentChar))
        return false;
      if (position.CurrentChar == '~')
      {
        pressInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_RETURN, KeyAction.Press, Keyboard.sendKeysDelay));
        releaseInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_RETURN, KeyAction.Release, Keyboard.sendKeysDelay));
      }
      else
      {
        VirtualKey virtualAlphaNumericKey = this.keyTranslator.GetVirtualAlphaNumericKey(position.CurrentChar);
        if (virtualAlphaNumericKey != VirtualKey.VK_NONE)
          pressInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(virtualAlphaNumericKey, KeyAction.PressAndRelease, Keyboard.sendKeysDelay));
        else
          pressInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(position.CurrentChar, Keyboard.sendKeysDelay));
      }
      position.MoveNext();
      return true;
    }

    private bool ParseNonPrintable(
      Keyboard.ParsePosition position,
      List<IInputAction> pressInputs,
      List<IInputAction> releaseInputs)
    {
      char[] separator = new char[1]{ '}' };
      string[] strArray1 = position.ToString().Split((char[]) null, StringSplitOptions.RemoveEmptyEntries);
      if (strArray1.Length != 0)
      {
        string[] strArray2 = strArray1[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
        if (strArray2.Length != 0)
        {
          string str = strArray2[0];
          if (this.keyTranslator.IsNonPrintableName(str))
          {
            pressInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(this.keyTranslator.GetVirtualKey(str), KeyAction.Press, Keyboard.sendKeysDelay));
            releaseInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(this.keyTranslator.GetVirtualKey(str), KeyAction.Release, Keyboard.sendKeysDelay));
            position.Skip(str.Length);
            return true;
          }
        }
      }
      return false;
    }

    private bool ParseCurly(Keyboard.ParsePosition position, List<IInputAction> inputs)
    {
      if (position.CurrentChar != '{')
        return false;
      Keyboard.ParsePosition position1 = (Keyboard.ParsePosition) position.Clone();
      position1.MoveNext();
      List<IInputAction> pressInputs = new List<IInputAction>();
      List<IInputAction> releaseInputs = new List<IInputAction>();
      if (!this.ParseSpecialChar(position1, pressInputs) && !this.ParseNonPrintable(position1, pressInputs, releaseInputs) && !this.ParsePrintable(position1, pressInputs, releaseInputs))
        return false;
      int repetitionCount = 1;
      KeyAction keyAction = KeyAction.PressAndRelease;
      if (this.ParseWhitespaces(position1) && !this.ParseCurlyPostfix(position1, ref repetitionCount, ref keyAction) || position1.CurrentChar != '}')
        return false;
      position1.MoveNext();
      position.Skip(position1.CurrentIndex);
      for (int index = 0; index < repetitionCount; ++index)
      {
        switch (keyAction)
        {
          case KeyAction.Press:
            inputs.AddRange((IEnumerable<IInputAction>) pressInputs);
            break;
          case KeyAction.Release:
            inputs.AddRange((IEnumerable<IInputAction>) releaseInputs);
            break;
          case KeyAction.PressAndRelease:
            inputs.AddRange((IEnumerable<IInputAction>) pressInputs);
            inputs.AddRange((IEnumerable<IInputAction>) releaseInputs);
            break;
        }
      }
      return true;
    }

    private bool ParseBlock(Keyboard.ParsePosition position, List<IInputAction> inputs)
    {
      if (position.CurrentChar != '(')
        return false;
      Keyboard.ParsePosition position1 = (Keyboard.ParsePosition) position.Clone();
      position1.MoveNext();
      List<IInputAction> inputs1 = new List<IInputAction>();
      this.ParseInnerText(position1, inputs1);
      if (position1.CurrentChar != ')')
        return false;
      position1.MoveNext();
      position.Skip(position1.CurrentIndex);
      inputs.AddRange((IEnumerable<IInputAction>) inputs1);
      return true;
    }

    private bool ParseInnerText(Keyboard.ParsePosition position, List<IInputAction> inputs)
    {
      do
      {
        do
          ;
        while (this.ParseModifiers(position, inputs));
        List<IInputAction> pressInputs = new List<IInputAction>();
        List<IInputAction> releaseInputs = new List<IInputAction>();
        if (this.ParsePrintable(position, pressInputs, releaseInputs))
        {
          inputs.AddRange((IEnumerable<IInputAction>) pressInputs);
          inputs.AddRange((IEnumerable<IInputAction>) releaseInputs);
        }
      }
      while (this.ParseCurly(position, inputs) || this.ParseBlock(position, inputs));
      return true;
    }

    private bool ParseModifier(
      Keyboard.ParsePosition position,
      List<IInputAction> pressInputs,
      List<IInputAction> releaseInputs)
    {
      switch (position.CurrentChar)
      {
        case '%':
          pressInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_MENU, KeyAction.Press, Keyboard.sendKeysDelay));
          releaseInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_MENU, KeyAction.Release, Keyboard.sendKeysDelay));
          break;
        case '+':
          pressInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_SHIFT, KeyAction.Press, Keyboard.sendKeysDelay));
          releaseInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_SHIFT, KeyAction.Release, Keyboard.sendKeysDelay));
          break;
        case '^':
          pressInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_CONTROL, KeyAction.Press, Keyboard.sendKeysDelay));
          releaseInputs.AddRange((IEnumerable<IInputAction>) Input.CreateKeyInput(VirtualKey.VK_CONTROL, KeyAction.Release, Keyboard.sendKeysDelay));
          break;
        default:
          return false;
      }
      position.MoveNext();
      return true;
    }

    private bool ParseModifiers(Keyboard.ParsePosition position, List<IInputAction> inputs)
    {
      Keyboard.ParsePosition position1 = (Keyboard.ParsePosition) position.Clone();
      List<IInputAction> inputActionList = new List<IInputAction>();
      List<IInputAction> releaseInputs = new List<IInputAction>();
      if (!this.ParseModifier(position1, inputActionList, releaseInputs))
        return false;
      position.Skip(position1.CurrentIndex);
      do
        ;
      while (this.ParseModifier(position, inputActionList, releaseInputs));
      if (!this.ParsePrintable(position, inputActionList, releaseInputs) && !this.ParseCurly(position, inputActionList))
        this.ParseBlock(position, inputActionList);
      inputs.AddRange((IEnumerable<IInputAction>) inputActionList);
      inputs.AddRange((IEnumerable<IInputAction>) releaseInputs);
      return true;
    }

    internal bool ParseText(string text, out List<IInputAction> inputs)
    {
      using (Keyboard.ParsePosition position = new Keyboard.ParsePosition(text))
      {
        position.MoveNext();
        inputs = new List<IInputAction>();
        this.ParseInnerText(position, inputs);
        return this.ParseEOS(position);
      }
    }

    public enum KeyState
    {
      Up,
      Down,
      Toggled,
    }

    internal class ParsePosition : IEnumerator, IDisposable
    {
      private string parseText = string.Empty;
      private IEnumerator<char> charEnumerator;
      private bool currentValid;
      private int currentIndex = -1;

      private ParsePosition()
      {
      }

      public ParsePosition(string parseText)
      {
        this.parseText = parseText;
        this.charEnumerator = ((IEnumerable<char>) parseText.ToCharArray()).GetEnumerator();
      }

      private ParsePosition(string parseText, bool currentValid, int currentIndex)
        : this(parseText)
      {
        this.currentValid = currentValid;
        if (currentValid)
          this.charEnumerator.MoveNext();
        this.currentIndex = currentIndex == -1 ? -1 : 0;
      }

      public void Dispose() => this.charEnumerator.Dispose();

      public bool MoveNext()
      {
        if (this.currentValid || this.currentIndex == -1)
          ++this.currentIndex;
        this.currentValid = this.charEnumerator.MoveNext();
        return this.currentValid;
      }

      public object Current => (object) this.CurrentChar;

      public char CurrentChar => !this.currentValid ? char.MinValue : this.charEnumerator.Current;

      public int CurrentIndex => this.currentIndex;

      public void Reset()
      {
        this.charEnumerator.Reset();
        this.currentValid = false;
        this.currentIndex = -1;
      }

      public bool ReachedEnd => !this.currentValid;

      public object Clone() => this.currentIndex == -1 ? (object) new Keyboard.ParsePosition(this.parseText, this.currentValid, this.currentIndex) : (object) new Keyboard.ParsePosition(this.parseText.Substring(this.currentIndex), this.currentValid, this.currentIndex);

      public bool Skip(int count)
      {
        for (int index = 0; index < count; ++index)
        {
          if (!this.MoveNext())
            return false;
        }
        return true;
      }

      public string GetRightString(int length) => this.GetRightString(0, length);

      public string GetRightString(int startIndex, int length)
      {
        StringBuilder stringBuilder = new StringBuilder(length);
        Keyboard.ParsePosition parsePosition = (Keyboard.ParsePosition) this.Clone();
        parsePosition.Skip(startIndex);
        for (int index = 0; index < length && !parsePosition.ReachedEnd; ++index)
        {
          stringBuilder.Append(parsePosition.CurrentChar);
          parsePosition.MoveNext();
        }
        return stringBuilder.ToString();
      }

      public char GetRightCharacter(int index)
      {
        Keyboard.ParsePosition parsePosition = (Keyboard.ParsePosition) this.Clone();
        parsePosition.Skip(index);
        return parsePosition.CurrentChar;
      }

      public override string ToString() => this.currentIndex == -1 ? this.parseText.Substring(0) : this.parseText.Substring(this.currentIndex);
    }
  }
}
