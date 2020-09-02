// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.KeyTranslator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MS.Internal.Mita.Foundation
{
  internal class KeyTranslator
  {
    private NonPrintableMap nonPrintableMap;
    private KeyMap keyMap;
    private IDictionary<char, VirtualKey> specialKeysMap;
    private static KeyTranslator singletonInstance;
    private static object classLock = new object();

    protected KeyTranslator()
    {
      this.nonPrintableMap = new NonPrintableMap();
      this.nonPrintableMap.Add(new NonPrintableMapItem("ALT", VirtualKey.VK_MENU));
      this.nonPrintableMap.Add(new NonPrintableMapItem("APPS", VirtualKey.VK_APPS));
      this.nonPrintableMap.Add(new NonPrintableMapItem("BACKSPACE", VirtualKey.VK_BACK));
      this.nonPrintableMap.Add(new NonPrintableMapItem("BKSP", VirtualKey.VK_BACK));
      this.nonPrintableMap.Add(new NonPrintableMapItem("BS", VirtualKey.VK_BACK));
      this.nonPrintableMap.Add(new NonPrintableMapItem("CAPSLOCK", VirtualKey.VK_CAPITAL));
      this.nonPrintableMap.Add(new NonPrintableMapItem("CONTEXTMENU", VirtualKey.VK_APPS));
      this.nonPrintableMap.Add(new NonPrintableMapItem("CONTROL", VirtualKey.VK_CONTROL));
      this.nonPrintableMap.Add(new NonPrintableMapItem("DEL", VirtualKey.VK_DELETE));
      this.nonPrintableMap.Add(new NonPrintableMapItem("DELETE", VirtualKey.VK_DELETE));
      this.nonPrintableMap.Add(new NonPrintableMapItem("DOWN", VirtualKey.VK_DOWN));
      this.nonPrintableMap.Add(new NonPrintableMapItem("END", VirtualKey.VK_END));
      this.nonPrintableMap.Add(new NonPrintableMapItem("ENTER", VirtualKey.VK_RETURN));
      this.nonPrintableMap.Add(new NonPrintableMapItem("ESC", VirtualKey.VK_ESCAPE));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F1", VirtualKey.VK_F1));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F2", VirtualKey.VK_F2));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F3", VirtualKey.VK_F3));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F4", VirtualKey.VK_F4));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F5", VirtualKey.VK_F5));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F6", VirtualKey.VK_F6));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F7", VirtualKey.VK_F7));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F8", VirtualKey.VK_F8));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F9", VirtualKey.VK_F9));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F10", VirtualKey.VK_F10));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F11", VirtualKey.VK_F11));
      this.nonPrintableMap.Add(new NonPrintableMapItem("F12", VirtualKey.VK_F12));
      this.nonPrintableMap.Add(new NonPrintableMapItem("HOME", VirtualKey.VK_HOME));
      this.nonPrintableMap.Add(new NonPrintableMapItem("INS", VirtualKey.VK_INSERT));
      this.nonPrintableMap.Add(new NonPrintableMapItem("INSERT", VirtualKey.VK_INSERT));
      this.nonPrintableMap.Add(new NonPrintableMapItem("LEFT", VirtualKey.VK_LEFT));
      this.nonPrintableMap.Add(new NonPrintableMapItem("MENU", VirtualKey.VK_MENU));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMLOCK", VirtualKey.VK_NUMLOCK));
      this.nonPrintableMap.Add(new NonPrintableMapItem("PGDN", VirtualKey.VK_NEXT));
      this.nonPrintableMap.Add(new NonPrintableMapItem("PGUP", VirtualKey.VK_PRIOR));
      this.nonPrintableMap.Add(new NonPrintableMapItem("PRTSC", VirtualKey.VK_PRINT));
      this.nonPrintableMap.Add(new NonPrintableMapItem("RETURN", VirtualKey.VK_RETURN));
      this.nonPrintableMap.Add(new NonPrintableMapItem("RIGHT", VirtualKey.VK_RIGHT));
      this.nonPrintableMap.Add(new NonPrintableMapItem("SHIFT", VirtualKey.VK_SHIFT));
      this.nonPrintableMap.Add(new NonPrintableMapItem("SPACE", VirtualKey.VK_SPACE));
      this.nonPrintableMap.Add(new NonPrintableMapItem("SCROLLLOCK", VirtualKey.VK_SCROLL));
      this.nonPrintableMap.Add(new NonPrintableMapItem("TAB", VirtualKey.VK_TAB));
      this.nonPrintableMap.Add(new NonPrintableMapItem("UP", VirtualKey.VK_UP));
      this.nonPrintableMap.Add(new NonPrintableMapItem("WIN", VirtualKey.VK_LWIN));
      this.nonPrintableMap.Add(new NonPrintableMapItem("WINDOWS", VirtualKey.VK_LWIN));
      this.nonPrintableMap.Add(new NonPrintableMapItem("ADD", VirtualKey.VK_ADD));
      this.nonPrintableMap.Add(new NonPrintableMapItem("SUBTRACT", VirtualKey.VK_SUBTRACT));
      this.nonPrintableMap.Add(new NonPrintableMapItem("MULTIPLY", VirtualKey.VK_MULTIPLY));
      this.nonPrintableMap.Add(new NonPrintableMapItem("DIVIDE", VirtualKey.VK_DIVIDE));
      this.nonPrintableMap.Add(new NonPrintableMapItem("DECIMAL", VirtualKey.VK_DECIMAL));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMPAD0", VirtualKey.VK_NUMPAD0));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMPAD1", VirtualKey.VK_NUMPAD1));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMPAD2", VirtualKey.VK_NUMPAD2));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMPAD3", VirtualKey.VK_NUMPAD3));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMPAD4", VirtualKey.VK_NUMPAD4));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMPAD5", VirtualKey.VK_NUMPAD5));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMPAD6", VirtualKey.VK_NUMPAD6));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMPAD7", VirtualKey.VK_NUMPAD7));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMPAD8", VirtualKey.VK_NUMPAD8));
      this.nonPrintableMap.Add(new NonPrintableMapItem("NUMPAD9", VirtualKey.VK_NUMPAD9));
      this.keyMap = new KeyMap();
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_BACK, new KeyScanCodes((ushort) 14, (ushort) 142)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_TAB, new KeyScanCodes((ushort) 15, (ushort) 143)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_RETURN, new KeyScanCodes((ushort) 28, (ushort) 156)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_SHIFT, new KeyScanCodes((ushort) 42, (ushort) 170)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_CONTROL, new KeyScanCodes((ushort) 29, (ushort) 157)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_MENU, new KeyScanCodes((ushort) 56, (ushort) 184)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_CAPITAL, new KeyScanCodes((ushort) 58, (ushort) 186)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_ESCAPE, new KeyScanCodes((ushort) 1, (ushort) 129)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_SPACE, new KeyScanCodes((ushort) 57, (ushort) 185)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_PRIOR, new KeyScanCodes((ushort) 57417, (ushort) 57545)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NEXT, new KeyScanCodes((ushort) 57425, (ushort) 57553)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_END, new KeyScanCodes((ushort) 57423, (ushort) 57551)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_HOME, new KeyScanCodes((ushort) 57415, (ushort) 57543)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_LEFT, new KeyScanCodes((ushort) 57419, (ushort) 57547)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_UP, new KeyScanCodes((ushort) 57416, (ushort) 57544)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_RIGHT, new KeyScanCodes((ushort) 57421, (ushort) 57549)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_DOWN, new KeyScanCodes((ushort) 57424, (ushort) 57552)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_PRINT, new KeyScanCodes((ushort) 57399, (ushort) 57527)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_INSERT, new KeyScanCodes((ushort) 57426, (ushort) 57554)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_DELETE, new KeyScanCodes((ushort) 57427, (ushort) 57555)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D0, new KeyScanCodes((ushort) 11, (ushort) 139)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D1, new KeyScanCodes((ushort) 2, (ushort) 130)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D2, new KeyScanCodes((ushort) 3, (ushort) 131)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D3, new KeyScanCodes((ushort) 4, (ushort) 132)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D4, new KeyScanCodes((ushort) 5, (ushort) 133)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D5, new KeyScanCodes((ushort) 6, (ushort) 134)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D6, new KeyScanCodes((ushort) 7, (ushort) 135)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D7, new KeyScanCodes((ushort) 8, (ushort) 136)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D8, new KeyScanCodes((ushort) 9, (ushort) 137)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D9, new KeyScanCodes((ushort) 10, (ushort) 138)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_A, new KeyScanCodes((ushort) 30, (ushort) 158)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_B, new KeyScanCodes((ushort) 48, (ushort) 176)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_C, new KeyScanCodes((ushort) 46, (ushort) 174)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_D, new KeyScanCodes((ushort) 32, (ushort) 160)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_E, new KeyScanCodes((ushort) 18, (ushort) 146)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F, new KeyScanCodes((ushort) 33, (ushort) 161)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_G, new KeyScanCodes((ushort) 34, (ushort) 162)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_H, new KeyScanCodes((ushort) 35, (ushort) 163)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_I, new KeyScanCodes((ushort) 23, (ushort) 151)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_J, new KeyScanCodes((ushort) 36, (ushort) 164)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_K, new KeyScanCodes((ushort) 37, (ushort) 165)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_L, new KeyScanCodes((ushort) 38, (ushort) 166)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_M, new KeyScanCodes((ushort) 50, (ushort) 178)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_N, new KeyScanCodes((ushort) 49, (ushort) 177)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_O, new KeyScanCodes((ushort) 24, (ushort) 152)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_P, new KeyScanCodes((ushort) 25, (ushort) 153)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_Q, new KeyScanCodes((ushort) 16, (ushort) 144)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_R, new KeyScanCodes((ushort) 19, (ushort) 147)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_S, new KeyScanCodes((ushort) 31, (ushort) 159)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_T, new KeyScanCodes((ushort) 20, (ushort) 148)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_U, new KeyScanCodes((ushort) 22, (ushort) 150)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_V, new KeyScanCodes((ushort) 47, (ushort) 175)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_W, new KeyScanCodes((ushort) 17, (ushort) 145)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_X, new KeyScanCodes((ushort) 45, (ushort) 173)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_Y, new KeyScanCodes((ushort) 21, (ushort) 149)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_Z, new KeyScanCodes((ushort) 44, (ushort) 172)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_LWIN, new KeyScanCodes((ushort) 57435, (ushort) 57563)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_RWIN, new KeyScanCodes((ushort) 57436, (ushort) 57564)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_APPS, new KeyScanCodes((ushort) 57437, (ushort) 57565)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_SLEEP, new KeyScanCodes((ushort) 57439, (ushort) 57567)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMPAD0, new KeyScanCodes((ushort) 82, (ushort) 210)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMPAD1, new KeyScanCodes((ushort) 79, (ushort) 207)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMPAD2, new KeyScanCodes((ushort) 80, (ushort) 208)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMPAD3, new KeyScanCodes((ushort) 81, (ushort) 209)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMPAD4, new KeyScanCodes((ushort) 75, (ushort) 203)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMPAD5, new KeyScanCodes((ushort) 76, (ushort) 204)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMPAD6, new KeyScanCodes((ushort) 77, (ushort) 205)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMPAD7, new KeyScanCodes((ushort) 71, (ushort) 199)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMPAD8, new KeyScanCodes((ushort) 72, (ushort) 200)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMPAD9, new KeyScanCodes((ushort) 73, (ushort) 201)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_MULTIPLY, new KeyScanCodes((ushort) 55, (ushort) 183)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_ADD, new KeyScanCodes((ushort) 78, (ushort) 206)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_SUBTRACT, new KeyScanCodes((ushort) 74, (ushort) 202)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_DECIMAL, new KeyScanCodes((ushort) 83, (ushort) 211)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_DIVIDE, new KeyScanCodes((ushort) 57397, (ushort) 57525)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F1, new KeyScanCodes((ushort) 59, (ushort) 187)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F2, new KeyScanCodes((ushort) 60, (ushort) 188)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F3, new KeyScanCodes((ushort) 61, (ushort) 189)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F4, new KeyScanCodes((ushort) 62, (ushort) 190)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F5, new KeyScanCodes((ushort) 63, (ushort) 191)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F6, new KeyScanCodes((ushort) 64, (ushort) 192)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F7, new KeyScanCodes((ushort) 65, (ushort) 193)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F8, new KeyScanCodes((ushort) 66, (ushort) 194)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F9, new KeyScanCodes((ushort) 67, (ushort) 195)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F10, new KeyScanCodes((ushort) 68, (ushort) 196)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F11, new KeyScanCodes((ushort) 87, (ushort) 215)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_F12, new KeyScanCodes((ushort) 88, (ushort) 216)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_NUMLOCK, new KeyScanCodes((ushort) 69, (ushort) 197)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_SCROLL, new KeyScanCodes((ushort) 70, (ushort) 198)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_LSHIFT, new KeyScanCodes((ushort) 42, (ushort) 170)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_RSHIFT, new KeyScanCodes((ushort) 54, (ushort) 182)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_LCONTROL, new KeyScanCodes((ushort) 29, (ushort) 157)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_RCONTROL, new KeyScanCodes((ushort) 57373, (ushort) 57501)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_LMENU, new KeyScanCodes((ushort) 56, (ushort) 184)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_RMENU, new KeyScanCodes((ushort) 57400, (ushort) 57528)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_1, new KeyScanCodes((ushort) 39, (ushort) 167)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_PLUS, new KeyScanCodes((ushort) 13, (ushort) 141)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_COMMA, new KeyScanCodes((ushort) 51, (ushort) 179)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_MINUS, new KeyScanCodes((ushort) 12, (ushort) 140)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_PERIOD, new KeyScanCodes((ushort) 52, (ushort) 180)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_2, new KeyScanCodes((ushort) 53, (ushort) 181)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_3, new KeyScanCodes((ushort) 41, (ushort) 137)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_4, new KeyScanCodes((ushort) 26, (ushort) 154)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_5, new KeyScanCodes((ushort) 43, (ushort) 171)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_6, new KeyScanCodes((ushort) 27, (ushort) 155)));
      this.keyMap.Add(new KeyMapItem(VirtualKey.VK_OEM_7, new KeyScanCodes((ushort) 40, (ushort) 168)));
      this.specialKeysMap = (IDictionary<char, VirtualKey>) new Dictionary<char, VirtualKey>()
      {
        [';'] = VirtualKey.VK_OEM_1,
        ['='] = VirtualKey.VK_OEM_PLUS,
        [','] = VirtualKey.VK_OEM_COMMA,
        ['-'] = VirtualKey.VK_OEM_MINUS,
        ['.'] = VirtualKey.VK_OEM_PERIOD,
        ['/'] = VirtualKey.VK_OEM_2,
        ['`'] = VirtualKey.VK_OEM_3,
        ['['] = VirtualKey.VK_OEM_4,
        ['\\'] = VirtualKey.VK_OEM_5,
        [']'] = VirtualKey.VK_OEM_6,
        ['\''] = VirtualKey.VK_OEM_7,
        [' '] = VirtualKey.VK_SPACE
      };
    }

    public static KeyTranslator Instance
    {
      get
      {
        if (KeyTranslator.singletonInstance == null)
        {
          lock (KeyTranslator.classLock)
          {
            if (KeyTranslator.singletonInstance == null)
              KeyTranslator.singletonInstance = new KeyTranslator();
          }
        }
        return KeyTranslator.singletonInstance;
      }
    }

    public ushort GetMakeScanCode(VirtualKey virtualKey) => this.keyMap[virtualKey].ScanCodes.MakeCode;

    public ushort GetMakeScanCode(string name) => this.GetMakeScanCode(this.GetVirtualKey(name));

    public VirtualKey GetVirtualKey(string keyName) => this.nonPrintableMap[keyName.ToUpperInvariant()].VirtualKey;

    public VirtualKey GetVirtualAlphaNumericKey(char alphaNumKey)
    {
      VirtualKey virtualKey = VirtualKey.VK_NONE;
      Regex regex = new Regex("[a-z]", RegexOptions.CultureInvariant);
      if (char.IsDigit(alphaNumKey))
        virtualKey = (VirtualKey) (48 + ((int) Convert.ToByte(alphaNumKey) - (int) Convert.ToByte('0')));
      else if (regex.IsMatch(alphaNumKey.ToString()))
        virtualKey = (VirtualKey) (65 + ((int) Convert.ToByte(char.ToUpperInvariant(alphaNumKey)) - (int) Convert.ToByte('A')));
      else if (this.specialKeysMap.ContainsKey(alphaNumKey))
        virtualKey = this.specialKeysMap[alphaNumKey];
      return virtualKey;
    }

    public VirtualKey GetVirtualKeyFromNumpadKey(char numpadKey)
    {
      if (!char.IsDigit(numpadKey))
        throw new ArgumentOutOfRangeException("Not a number key");
      return (VirtualKey) (96 + ((int) Convert.ToByte(numpadKey) - (int) Convert.ToByte('0')));
    }

    public bool IsNonPrintableName(string name) => this.nonPrintableMap.Contains(name.ToUpperInvariant());
  }
}
