// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.KeyTranslator
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MS.Internal.Mita.Foundation {
    internal class KeyTranslator {
        static KeyTranslator singletonInstance;
        static readonly object classLock = new object();
        readonly KeyMap keyMap;
        readonly NonPrintableMap nonPrintableMap;
        readonly IDictionary<char, VirtualKey> specialKeysMap;

        protected KeyTranslator() {
            this.nonPrintableMap = new NonPrintableMap();
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "ALT", virtualKey: VirtualKey.VK_MENU));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "APPS", virtualKey: VirtualKey.VK_APPS));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "BACKSPACE", virtualKey: VirtualKey.VK_BACK));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "BKSP", virtualKey: VirtualKey.VK_BACK));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "BS", virtualKey: VirtualKey.VK_BACK));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "CAPSLOCK", virtualKey: VirtualKey.VK_CAPITAL));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "CONTEXTMENU", virtualKey: VirtualKey.VK_APPS));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "CONTROL", virtualKey: VirtualKey.VK_CONTROL));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "DEL", virtualKey: VirtualKey.VK_DELETE));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "DELETE", virtualKey: VirtualKey.VK_DELETE));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "DOWN", virtualKey: VirtualKey.VK_DOWN));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "END", virtualKey: VirtualKey.VK_END));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "ENTER", virtualKey: VirtualKey.VK_RETURN));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "ESC", virtualKey: VirtualKey.VK_ESCAPE));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F1", virtualKey: VirtualKey.VK_F1));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F2", virtualKey: VirtualKey.VK_F2));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F3", virtualKey: VirtualKey.VK_F3));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F4", virtualKey: VirtualKey.VK_F4));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F5", virtualKey: VirtualKey.VK_F5));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F6", virtualKey: VirtualKey.VK_F6));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F7", virtualKey: VirtualKey.VK_F7));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F8", virtualKey: VirtualKey.VK_F8));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F9", virtualKey: VirtualKey.VK_F9));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F10", virtualKey: VirtualKey.VK_F10));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F11", virtualKey: VirtualKey.VK_F11));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "F12", virtualKey: VirtualKey.VK_F12));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "HOME", virtualKey: VirtualKey.VK_HOME));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "INS", virtualKey: VirtualKey.VK_INSERT));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "INSERT", virtualKey: VirtualKey.VK_INSERT));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "LEFT", virtualKey: VirtualKey.VK_LEFT));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "MENU", virtualKey: VirtualKey.VK_MENU));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMLOCK", virtualKey: VirtualKey.VK_NUMLOCK));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "PGDN", virtualKey: VirtualKey.VK_NEXT));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "PGUP", virtualKey: VirtualKey.VK_PRIOR));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "PRTSC", virtualKey: VirtualKey.VK_PRINT));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "RETURN", virtualKey: VirtualKey.VK_RETURN));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "RIGHT", virtualKey: VirtualKey.VK_RIGHT));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "SHIFT", virtualKey: VirtualKey.VK_SHIFT));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "SPACE", virtualKey: VirtualKey.VK_SPACE));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "SCROLLLOCK", virtualKey: VirtualKey.VK_SCROLL));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "TAB", virtualKey: VirtualKey.VK_TAB));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "UP", virtualKey: VirtualKey.VK_UP));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "WIN", virtualKey: VirtualKey.VK_LWIN));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "WINDOWS", virtualKey: VirtualKey.VK_LWIN));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "ADD", virtualKey: VirtualKey.VK_ADD));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "SUBTRACT", virtualKey: VirtualKey.VK_SUBTRACT));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "MULTIPLY", virtualKey: VirtualKey.VK_MULTIPLY));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "DIVIDE", virtualKey: VirtualKey.VK_DIVIDE));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "DECIMAL", virtualKey: VirtualKey.VK_DECIMAL));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMPAD0", virtualKey: VirtualKey.VK_NUMPAD0));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMPAD1", virtualKey: VirtualKey.VK_NUMPAD1));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMPAD2", virtualKey: VirtualKey.VK_NUMPAD2));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMPAD3", virtualKey: VirtualKey.VK_NUMPAD3));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMPAD4", virtualKey: VirtualKey.VK_NUMPAD4));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMPAD5", virtualKey: VirtualKey.VK_NUMPAD5));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMPAD6", virtualKey: VirtualKey.VK_NUMPAD6));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMPAD7", virtualKey: VirtualKey.VK_NUMPAD7));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMPAD8", virtualKey: VirtualKey.VK_NUMPAD8));
            this.nonPrintableMap.Add(item: new NonPrintableMapItem(name: "NUMPAD9", virtualKey: VirtualKey.VK_NUMPAD9));
            this.keyMap = new KeyMap();
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_BACK, scanCodes: new KeyScanCodes(makeCode: 14, breakCode: 142)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_TAB, scanCodes: new KeyScanCodes(makeCode: 15, breakCode: 143)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_RETURN, scanCodes: new KeyScanCodes(makeCode: 28, breakCode: 156)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_SHIFT, scanCodes: new KeyScanCodes(makeCode: 42, breakCode: 170)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_CONTROL, scanCodes: new KeyScanCodes(makeCode: 29, breakCode: 157)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_MENU, scanCodes: new KeyScanCodes(makeCode: 56, breakCode: 184)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_CAPITAL, scanCodes: new KeyScanCodes(makeCode: 58, breakCode: 186)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_ESCAPE, scanCodes: new KeyScanCodes(makeCode: 1, breakCode: 129)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_SPACE, scanCodes: new KeyScanCodes(makeCode: 57, breakCode: 185)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_PRIOR, scanCodes: new KeyScanCodes(makeCode: 57417, breakCode: 57545)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NEXT, scanCodes: new KeyScanCodes(makeCode: 57425, breakCode: 57553)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_END, scanCodes: new KeyScanCodes(makeCode: 57423, breakCode: 57551)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_HOME, scanCodes: new KeyScanCodes(makeCode: 57415, breakCode: 57543)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_LEFT, scanCodes: new KeyScanCodes(makeCode: 57419, breakCode: 57547)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_UP, scanCodes: new KeyScanCodes(makeCode: 57416, breakCode: 57544)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_RIGHT, scanCodes: new KeyScanCodes(makeCode: 57421, breakCode: 57549)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_DOWN, scanCodes: new KeyScanCodes(makeCode: 57424, breakCode: 57552)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_PRINT, scanCodes: new KeyScanCodes(makeCode: 57399, breakCode: 57527)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_INSERT, scanCodes: new KeyScanCodes(makeCode: 57426, breakCode: 57554)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_DELETE, scanCodes: new KeyScanCodes(makeCode: 57427, breakCode: 57555)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D0, scanCodes: new KeyScanCodes(makeCode: 11, breakCode: 139)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D1, scanCodes: new KeyScanCodes(makeCode: 2, breakCode: 130)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D2, scanCodes: new KeyScanCodes(makeCode: 3, breakCode: 131)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D3, scanCodes: new KeyScanCodes(makeCode: 4, breakCode: 132)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D4, scanCodes: new KeyScanCodes(makeCode: 5, breakCode: 133)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D5, scanCodes: new KeyScanCodes(makeCode: 6, breakCode: 134)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D6, scanCodes: new KeyScanCodes(makeCode: 7, breakCode: 135)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D7, scanCodes: new KeyScanCodes(makeCode: 8, breakCode: 136)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D8, scanCodes: new KeyScanCodes(makeCode: 9, breakCode: 137)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D9, scanCodes: new KeyScanCodes(makeCode: 10, breakCode: 138)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_A, scanCodes: new KeyScanCodes(makeCode: 30, breakCode: 158)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_B, scanCodes: new KeyScanCodes(makeCode: 48, breakCode: 176)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_C, scanCodes: new KeyScanCodes(makeCode: 46, breakCode: 174)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_D, scanCodes: new KeyScanCodes(makeCode: 32, breakCode: 160)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_E, scanCodes: new KeyScanCodes(makeCode: 18, breakCode: 146)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F, scanCodes: new KeyScanCodes(makeCode: 33, breakCode: 161)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_G, scanCodes: new KeyScanCodes(makeCode: 34, breakCode: 162)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_H, scanCodes: new KeyScanCodes(makeCode: 35, breakCode: 163)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_I, scanCodes: new KeyScanCodes(makeCode: 23, breakCode: 151)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_J, scanCodes: new KeyScanCodes(makeCode: 36, breakCode: 164)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_K, scanCodes: new KeyScanCodes(makeCode: 37, breakCode: 165)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_L, scanCodes: new KeyScanCodes(makeCode: 38, breakCode: 166)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_M, scanCodes: new KeyScanCodes(makeCode: 50, breakCode: 178)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_N, scanCodes: new KeyScanCodes(makeCode: 49, breakCode: 177)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_O, scanCodes: new KeyScanCodes(makeCode: 24, breakCode: 152)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_P, scanCodes: new KeyScanCodes(makeCode: 25, breakCode: 153)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_Q, scanCodes: new KeyScanCodes(makeCode: 16, breakCode: 144)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_R, scanCodes: new KeyScanCodes(makeCode: 19, breakCode: 147)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_S, scanCodes: new KeyScanCodes(makeCode: 31, breakCode: 159)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_T, scanCodes: new KeyScanCodes(makeCode: 20, breakCode: 148)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_U, scanCodes: new KeyScanCodes(makeCode: 22, breakCode: 150)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_V, scanCodes: new KeyScanCodes(makeCode: 47, breakCode: 175)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_W, scanCodes: new KeyScanCodes(makeCode: 17, breakCode: 145)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_X, scanCodes: new KeyScanCodes(makeCode: 45, breakCode: 173)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_Y, scanCodes: new KeyScanCodes(makeCode: 21, breakCode: 149)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_Z, scanCodes: new KeyScanCodes(makeCode: 44, breakCode: 172)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_LWIN, scanCodes: new KeyScanCodes(makeCode: 57435, breakCode: 57563)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_RWIN, scanCodes: new KeyScanCodes(makeCode: 57436, breakCode: 57564)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_APPS, scanCodes: new KeyScanCodes(makeCode: 57437, breakCode: 57565)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_SLEEP, scanCodes: new KeyScanCodes(makeCode: 57439, breakCode: 57567)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMPAD0, scanCodes: new KeyScanCodes(makeCode: 82, breakCode: 210)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMPAD1, scanCodes: new KeyScanCodes(makeCode: 79, breakCode: 207)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMPAD2, scanCodes: new KeyScanCodes(makeCode: 80, breakCode: 208)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMPAD3, scanCodes: new KeyScanCodes(makeCode: 81, breakCode: 209)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMPAD4, scanCodes: new KeyScanCodes(makeCode: 75, breakCode: 203)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMPAD5, scanCodes: new KeyScanCodes(makeCode: 76, breakCode: 204)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMPAD6, scanCodes: new KeyScanCodes(makeCode: 77, breakCode: 205)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMPAD7, scanCodes: new KeyScanCodes(makeCode: 71, breakCode: 199)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMPAD8, scanCodes: new KeyScanCodes(makeCode: 72, breakCode: 200)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMPAD9, scanCodes: new KeyScanCodes(makeCode: 73, breakCode: 201)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_MULTIPLY, scanCodes: new KeyScanCodes(makeCode: 55, breakCode: 183)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_ADD, scanCodes: new KeyScanCodes(makeCode: 78, breakCode: 206)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_SUBTRACT, scanCodes: new KeyScanCodes(makeCode: 74, breakCode: 202)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_DECIMAL, scanCodes: new KeyScanCodes(makeCode: 83, breakCode: 211)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_DIVIDE, scanCodes: new KeyScanCodes(makeCode: 57397, breakCode: 57525)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F1, scanCodes: new KeyScanCodes(makeCode: 59, breakCode: 187)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F2, scanCodes: new KeyScanCodes(makeCode: 60, breakCode: 188)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F3, scanCodes: new KeyScanCodes(makeCode: 61, breakCode: 189)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F4, scanCodes: new KeyScanCodes(makeCode: 62, breakCode: 190)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F5, scanCodes: new KeyScanCodes(makeCode: 63, breakCode: 191)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F6, scanCodes: new KeyScanCodes(makeCode: 64, breakCode: 192)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F7, scanCodes: new KeyScanCodes(makeCode: 65, breakCode: 193)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F8, scanCodes: new KeyScanCodes(makeCode: 66, breakCode: 194)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F9, scanCodes: new KeyScanCodes(makeCode: 67, breakCode: 195)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F10, scanCodes: new KeyScanCodes(makeCode: 68, breakCode: 196)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F11, scanCodes: new KeyScanCodes(makeCode: 87, breakCode: 215)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_F12, scanCodes: new KeyScanCodes(makeCode: 88, breakCode: 216)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_NUMLOCK, scanCodes: new KeyScanCodes(makeCode: 69, breakCode: 197)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_SCROLL, scanCodes: new KeyScanCodes(makeCode: 70, breakCode: 198)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_LSHIFT, scanCodes: new KeyScanCodes(makeCode: 42, breakCode: 170)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_RSHIFT, scanCodes: new KeyScanCodes(makeCode: 54, breakCode: 182)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_LCONTROL, scanCodes: new KeyScanCodes(makeCode: 29, breakCode: 157)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_RCONTROL, scanCodes: new KeyScanCodes(makeCode: 57373, breakCode: 57501)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_LMENU, scanCodes: new KeyScanCodes(makeCode: 56, breakCode: 184)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_RMENU, scanCodes: new KeyScanCodes(makeCode: 57400, breakCode: 57528)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_1, scanCodes: new KeyScanCodes(makeCode: 39, breakCode: 167)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_PLUS, scanCodes: new KeyScanCodes(makeCode: 13, breakCode: 141)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_COMMA, scanCodes: new KeyScanCodes(makeCode: 51, breakCode: 179)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_MINUS, scanCodes: new KeyScanCodes(makeCode: 12, breakCode: 140)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_PERIOD, scanCodes: new KeyScanCodes(makeCode: 52, breakCode: 180)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_2, scanCodes: new KeyScanCodes(makeCode: 53, breakCode: 181)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_3, scanCodes: new KeyScanCodes(makeCode: 41, breakCode: 137)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_4, scanCodes: new KeyScanCodes(makeCode: 26, breakCode: 154)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_5, scanCodes: new KeyScanCodes(makeCode: 43, breakCode: 171)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_6, scanCodes: new KeyScanCodes(makeCode: 27, breakCode: 155)));
            this.keyMap.Add(item: new KeyMapItem(virtualKey: VirtualKey.VK_OEM_7, scanCodes: new KeyScanCodes(makeCode: 40, breakCode: 168)));
            this.specialKeysMap = new Dictionary<char, VirtualKey> {
                [key: ';'] = VirtualKey.VK_OEM_1,
                [key: '='] = VirtualKey.VK_OEM_PLUS,
                [key: ','] = VirtualKey.VK_OEM_COMMA,
                [key: '-'] = VirtualKey.VK_OEM_MINUS,
                [key: '.'] = VirtualKey.VK_OEM_PERIOD,
                [key: '/'] = VirtualKey.VK_OEM_2,
                [key: '`'] = VirtualKey.VK_OEM_3,
                [key: '['] = VirtualKey.VK_OEM_4,
                [key: '\\'] = VirtualKey.VK_OEM_5,
                [key: ']'] = VirtualKey.VK_OEM_6,
                [key: '\''] = VirtualKey.VK_OEM_7,
                [key: ' '] = VirtualKey.VK_SPACE
            };
        }

        public static KeyTranslator Instance {
            get {
                if (singletonInstance == null)
                    lock (classLock) {
                        if (singletonInstance == null)
                            singletonInstance = new KeyTranslator();
                    }

                return singletonInstance;
            }
        }

        public ushort GetMakeScanCode(VirtualKey virtualKey) {
            return this.keyMap[key: virtualKey].ScanCodes.MakeCode;
        }

        public ushort GetMakeScanCode(string name) {
            return GetMakeScanCode(virtualKey: GetVirtualKey(keyName: name));
        }

        public VirtualKey GetVirtualKey(string keyName) {
            return this.nonPrintableMap[key: keyName.ToUpperInvariant()].VirtualKey;
        }

        public VirtualKey GetVirtualAlphaNumericKey(char alphaNumKey) {
            var virtualKey = VirtualKey.VK_NONE;
            var regex = new Regex(pattern: "[a-z]", options: RegexOptions.CultureInvariant);
            if (char.IsDigit(c: alphaNumKey))
                virtualKey = (VirtualKey) (48 + (Convert.ToByte(value: alphaNumKey) - Convert.ToByte(value: '0')));
            else if (regex.IsMatch(input: alphaNumKey.ToString()))
                virtualKey = (VirtualKey) (65 + (Convert.ToByte(value: char.ToUpperInvariant(c: alphaNumKey)) - Convert.ToByte(value: 'A')));
            else if (this.specialKeysMap.ContainsKey(key: alphaNumKey))
                virtualKey = this.specialKeysMap[key: alphaNumKey];
            return virtualKey;
        }

        public VirtualKey GetVirtualKeyFromNumpadKey(char numpadKey) {
            if (!char.IsDigit(c: numpadKey))
                throw new ArgumentOutOfRangeException(paramName: "Not a number key");
            return (VirtualKey) (96 + (Convert.ToByte(value: numpadKey) - Convert.ToByte(value: '0')));
        }

        public bool IsNonPrintableName(string name) {
            return this.nonPrintableMap.Contains(key: name.ToUpperInvariant());
        }
    }
}