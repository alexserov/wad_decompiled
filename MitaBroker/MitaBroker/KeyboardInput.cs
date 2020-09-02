// Decompiled with JetBrains decompiler
// Type: MitaBroker.KeyboardInput
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System.Collections.Generic;
using System.Text;
using MS.Internal.Mita.Foundation;

namespace MitaBroker {
    public sealed class KeyboardInput {
        static readonly Dictionary<char, string> nonPrintableKeyMap = new Dictionary<char, string> {
            {
                '\xE003',
                "{BACKSPACE}"
            }, {
                '\xE004',
                "{TAB}"
            }, {
                '\xE006',
                "{RETURN}"
            }, {
                '\xE007',
                "{ENTER}"
            }, {
                '\xE00C',
                "{ESC}"
            }, {
                '\xE00D',
                "{SPACE}"
            }, {
                '\xE00E',
                "{PGUP}"
            }, {
                '\xE00F',
                "{PGDN}"
            }, {
                '\xE010',
                "{END}"
            }, {
                '\xE011',
                "{HOME}"
            }, {
                '\xE012',
                "{LEFT}"
            }, {
                '\xE013',
                "{UP}"
            }, {
                '\xE014',
                "{RIGHT}"
            }, {
                '\xE015',
                "{DOWN}"
            }, {
                '\xE016',
                "{INSERT}"
            }, {
                '\xE017',
                "{DELETE}"
            }, {
                '\xE018',
                ";"
            }, {
                '\xE019',
                "="
            }, {
                '\xE01A',
                "{NUMPAD0}"
            }, {
                '\xE01B',
                "{NUMPAD1}"
            }, {
                '\xE01C',
                "{NUMPAD2}"
            }, {
                '\xE01D',
                "{NUMPAD3}"
            }, {
                '\xE01E',
                "{NUMPAD4}"
            }, {
                '\xE01F',
                "{NUMPAD5}"
            }, {
                '\xE020',
                "{NUMPAD6}"
            }, {
                '\xE021',
                "{NUMPAD7}"
            }, {
                '\xE022',
                "{NUMPAD8}"
            }, {
                '\xE023',
                "{NUMPAD9}"
            }, {
                '\xE024',
                "{MULTIPLY}"
            }, {
                '\xE025',
                "{ADD}"
            }, {
                '\xE027',
                "{SUBTRACT}"
            }, {
                '\xE028',
                "{DECIMAL}"
            }, {
                '\xE029',
                "{DIVIDE}"
            }, {
                '\xE031',
                "{F1}"
            }, {
                '\xE032',
                "{F2}"
            }, {
                '\xE033',
                "{F3}"
            }, {
                '\xE034',
                "{F4}"
            }, {
                '\xE035',
                "{F5}"
            }, {
                '\xE036',
                "{F6}"
            }, {
                '\xE037',
                "{F7}"
            }, {
                '\xE038',
                "{F8}"
            }, {
                '\xE039',
                "{F9}"
            }, {
                '\xE03A',
                "{F10}"
            }, {
                '\xE03B',
                "{F11}"
            }, {
                '\xE03C',
                "{F12}"
            }
        };

        static bool AltToggled;
        static bool CtrlToggled;
        static bool ShiftToggled;
        static bool WindowsToggled;

        public static string Process(string inputKeySequences) {
            var empty = string.Empty;
            return ConvertNonPrintableCharacters(inputKeySequences: Keyboard.EscapeSpecialCharacters(text: inputKeySequences));
        }

        static string ConvertNonPrintableCharacters(string inputKeySequences) {
            var currentString = new StringBuilder();
            foreach (var inputKeySequence in inputKeySequences)
                switch (inputKeySequence) {
                    case '\xE000':
                    case '\xE008':
                    case '\xE009':
                    case '\xE00A':
                    case '\xE03D':
                        ProcessModifiers(currentString: currentString, inputKey: inputKeySequence);
                        break;
                    default:
                        if (nonPrintableKeyMap.ContainsKey(key: inputKeySequence)) {
                            currentString.Append(value: nonPrintableKeyMap[key: inputKeySequence]);
                            break;
                        }

                        currentString.Append(value: inputKeySequence);
                        break;
                }

            return currentString.ToString();
        }

        static void ProcessModifiers(StringBuilder currentString, char inputKey) {
            switch (inputKey) {
                case '\xE000':
                    if (ShiftToggled)
                        ProcessModifiers(currentString: currentString, inputKey: '\xE008');
                    if (CtrlToggled)
                        ProcessModifiers(currentString: currentString, inputKey: '\xE009');
                    if (AltToggled)
                        ProcessModifiers(currentString: currentString, inputKey: '\xE00A');
                    if (!WindowsToggled)
                        break;
                    ProcessModifiers(currentString: currentString, inputKey: '\xE03D');
                    break;
                case '\xE008':
                    currentString.Append(value: ShiftToggled ? "{SHIFT UP}" : "{SHIFT DOWN}");
                    ShiftToggled = !ShiftToggled;
                    break;
                case '\xE009':
                    currentString.Append(value: CtrlToggled ? "{CONTROL UP}" : "{CONTROL DOWN}");
                    CtrlToggled = !CtrlToggled;
                    break;
                case '\xE00A':
                    currentString.Append(value: AltToggled ? "{ALT UP}" : "{ALT DOWN}");
                    AltToggled = !AltToggled;
                    break;
                case '\xE03D':
                    currentString.Append(value: WindowsToggled ? "{WIN UP}" : "{WIN DOWN}");
                    WindowsToggled = !WindowsToggled;
                    break;
            }
        }
    }
}