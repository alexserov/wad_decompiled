// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.Keys
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System.Collections.Generic;

namespace MitaBroker.WebDriver.Actions {
    internal static class Keys {
        static readonly Dictionary<string, string> normalisedKeys = new Dictionary<string, string> {
            {
                "\xE000",
                "Unidentified"
            }, {
                "\xE001",
                "Cancel"
            }, {
                "\xE002",
                "Help"
            }, {
                "\xE003",
                "Backspace"
            }, {
                "\xE004",
                "Tab"
            }, {
                "\xE005",
                "Clear"
            }, {
                "\xE006",
                "Return"
            }, {
                "\xE007",
                "Enter"
            }, {
                "\xE008",
                "Shift"
            }, {
                "\xE009",
                "Control"
            }, {
                "\xE00A",
                "Alt"
            }, {
                "\xE00B",
                "Pause"
            }, {
                "\xE00C",
                "Escape"
            }, {
                "\xE00D",
                " "
            }, {
                "\xE00E",
                "PageUp"
            }, {
                "\xE00F",
                "PageDown"
            }, {
                "\xE010",
                "End"
            }, {
                "\xE011",
                "Home"
            }, {
                "\xE012",
                "ArrowLeft"
            }, {
                "\xE013",
                "ArrowUp"
            }, {
                "\xE014",
                "ArrowRight"
            }, {
                "\xE015",
                "ArrowDown"
            }, {
                "\xE016",
                "Insert"
            }, {
                "\xE017",
                "Delete"
            }, {
                "\xE018",
                ";"
            }, {
                "\xE019",
                "="
            }, {
                "\xE01A",
                "0"
            }, {
                "\xE01B",
                "1"
            }, {
                "\xE01C",
                "2"
            }, {
                "\xE01D",
                "3"
            }, {
                "\xE01E",
                "4"
            }, {
                "\xE01F",
                "5"
            }, {
                "\xE020",
                "6"
            }, {
                "\xE021",
                "7"
            }, {
                "\xE022",
                "8"
            }, {
                "\xE023",
                "9"
            }, {
                "\xE024",
                "*"
            }, {
                "\xE025",
                "+"
            }, {
                "\xE026",
                ","
            }, {
                "\xE027",
                "-"
            }, {
                "\xE028",
                "."
            }, {
                "\xE029",
                "/"
            }, {
                "\xE031",
                "F1"
            }, {
                "\xE032",
                "F2"
            }, {
                "\xE033",
                "F3"
            }, {
                "\xE034",
                "F4"
            }, {
                "\xE035",
                "F5"
            }, {
                "\xE036",
                "F6"
            }, {
                "\xE037",
                "F7"
            }, {
                "\xE038",
                "F8"
            }, {
                "\xE039",
                "F9"
            }, {
                "\xE03A",
                "F10"
            }, {
                "\xE03B",
                "F11"
            }, {
                "\xE03C",
                "F12"
            }, {
                "\xE03D",
                "Meta"
            }, {
                "\xE040",
                "ZenkakuHankaku"
            }, {
                "\xE050",
                "Shift"
            }, {
                "\xE051",
                "Control"
            }, {
                "\xE052",
                "Alt"
            }, {
                "\xE053",
                "Meta"
            }, {
                "\xE054",
                "PageUp"
            }, {
                "\xE055",
                "PageDown"
            }, {
                "\xE056",
                "End"
            }, {
                "\xE057",
                "Home"
            }, {
                "\xE058",
                "ArrowLeft"
            }, {
                "\xE059",
                "ArrowUp"
            }, {
                "\xE05A",
                "ArrowRight"
            }, {
                "\xE05B",
                "ArrowDown"
            }, {
                "\xE05C",
                "Insert"
            }, {
                "\xE05D",
                "Delete"
            }
        };

        static readonly List<ShiftedKey> shiftedKeys = new List<ShiftedKey> {
            new ShiftedKey(key: "`", alternateKey: "~", code: "Backquote"),
            new ShiftedKey(key: "\\", alternateKey: "|", code: "Backslash"),
            new ShiftedKey(key: "\xE003", code: "Backspace"),
            new ShiftedKey(key: "[", alternateKey: "{", code: "BracketLeft"),
            new ShiftedKey(key: "}", alternateKey: "]", code: "BracketRight"),
            new ShiftedKey(key: ",", alternateKey: "<", code: "Comma"),
            new ShiftedKey(key: "0", alternateKey: ")", code: "Digit0"),
            new ShiftedKey(key: "1", alternateKey: "!", code: "Digit1"),
            new ShiftedKey(key: "2", alternateKey: "@", code: "Digit2"),
            new ShiftedKey(key: "3", alternateKey: "#", code: "Digit3"),
            new ShiftedKey(key: "4", alternateKey: "$", code: "Digit4"),
            new ShiftedKey(key: "5", alternateKey: "%", code: "Digit5"),
            new ShiftedKey(key: "6", alternateKey: "^", code: "Digit6"),
            new ShiftedKey(key: "7", alternateKey: "&", code: "Digit7"),
            new ShiftedKey(key: "8", alternateKey: "*", code: "Digit8"),
            new ShiftedKey(key: "9", alternateKey: "(", code: "Digit9"),
            new ShiftedKey(key: "=", alternateKey: "+", code: "Equal"),
            new ShiftedKey(key: "<", alternateKey: ">", code: "IntlBackslash"),
            new ShiftedKey(key: "a", alternateKey: "A", code: "KeyA"),
            new ShiftedKey(key: "b", alternateKey: "B", code: "KeyB"),
            new ShiftedKey(key: "c", alternateKey: "C", code: "KeyC"),
            new ShiftedKey(key: "d", alternateKey: "D", code: "KeyD"),
            new ShiftedKey(key: "e", alternateKey: "E", code: "KeyE"),
            new ShiftedKey(key: "f", alternateKey: "F", code: "KeyF"),
            new ShiftedKey(key: "g", alternateKey: "G", code: "KeyG"),
            new ShiftedKey(key: "h", alternateKey: "H", code: "KeyH"),
            new ShiftedKey(key: "i", alternateKey: "I", code: "KeyI"),
            new ShiftedKey(key: "j", alternateKey: "J", code: "KeyJ"),
            new ShiftedKey(key: "k", alternateKey: "K", code: "KeyK"),
            new ShiftedKey(key: "l", alternateKey: "L", code: "KeyL"),
            new ShiftedKey(key: "m", alternateKey: "M", code: "KeyM"),
            new ShiftedKey(key: "n", alternateKey: "N", code: "KeyN"),
            new ShiftedKey(key: "o", alternateKey: "O", code: "KeyO"),
            new ShiftedKey(key: "p", alternateKey: "P", code: "KeyP"),
            new ShiftedKey(key: "q", alternateKey: "Q", code: "KeyQ"),
            new ShiftedKey(key: "r", alternateKey: "R", code: "KeyR"),
            new ShiftedKey(key: "s", alternateKey: "S", code: "KeyS"),
            new ShiftedKey(key: "t", alternateKey: "T", code: "KeyT"),
            new ShiftedKey(key: "u", alternateKey: "U", code: "KeyU"),
            new ShiftedKey(key: "v", alternateKey: "V", code: "KeyV"),
            new ShiftedKey(key: "w", alternateKey: "W", code: "KeyW"),
            new ShiftedKey(key: "x", alternateKey: "X", code: "KeyX"),
            new ShiftedKey(key: "y", alternateKey: "Y", code: "KeyY"),
            new ShiftedKey(key: "z", alternateKey: "Z", code: "KeyZ"),
            new ShiftedKey(key: "-", alternateKey: "_", code: "Minus"),
            new ShiftedKey(key: ".", alternateKey: ">", code: "Period"),
            new ShiftedKey(key: "'", alternateKey: "\"", code: "Quote"),
            new ShiftedKey(key: ";", alternateKey: ":", code: "Semicolon"),
            new ShiftedKey(key: "/", alternateKey: "?", code: "Slash"),
            new ShiftedKey(key: "\xE00A", code: "AltLeft"),
            new ShiftedKey(key: "\xE052", code: "AltRight"),
            new ShiftedKey(key: "\xE009", code: "ControlLeft"),
            new ShiftedKey(key: "\xE051", code: "ControlRight"),
            new ShiftedKey(key: "\xE006", code: "Enter"),
            new ShiftedKey(key: "\xE03D", code: "OSLeft"),
            new ShiftedKey(key: "\xE053", code: "OSRight"),
            new ShiftedKey(key: "\xE008", code: "ShiftLeft"),
            new ShiftedKey(key: "\xE050", code: "ShiftRight"),
            new ShiftedKey(key: " ", alternateKey: "\xE00D", code: "Space"),
            new ShiftedKey(key: "\xE004", code: "Tab"),
            new ShiftedKey(key: "\xE017", code: "Delete"),
            new ShiftedKey(key: "\xE010", code: "End"),
            new ShiftedKey(key: "\xE002", code: "Help"),
            new ShiftedKey(key: "\xE011", code: "Home"),
            new ShiftedKey(key: "\xE016", code: "Insert"),
            new ShiftedKey(key: "\xE01E", code: "PageDown"),
            new ShiftedKey(key: "\xE01F", code: "PageUp"),
            new ShiftedKey(key: "\xE015", code: "ArrowDown"),
            new ShiftedKey(key: "\xE012", code: "ArrowLeft"),
            new ShiftedKey(key: "\xE014", code: "ArrowRight"),
            new ShiftedKey(key: "\xE013", code: "ArrowUp"),
            new ShiftedKey(key: "\xE00C", code: "Escape"),
            new ShiftedKey(key: "\xE031", code: "F1"),
            new ShiftedKey(key: "\xE032", code: "F2"),
            new ShiftedKey(key: "\xE033", code: "F3"),
            new ShiftedKey(key: "\xE034", code: "F4"),
            new ShiftedKey(key: "\xE035", code: "F5"),
            new ShiftedKey(key: "\xE036", code: "F6"),
            new ShiftedKey(key: "\xE037", code: "F7"),
            new ShiftedKey(key: "\xE038", code: "F8"),
            new ShiftedKey(key: "\xE039", code: "F9"),
            new ShiftedKey(key: "\xE03A", code: "F10"),
            new ShiftedKey(key: "\xE03B", code: "F11"),
            new ShiftedKey(key: "\xE03C", code: "F12"),
            new ShiftedKey(key: "\xE01A", alternateKey: "\xE05C", code: "Numpad0"),
            new ShiftedKey(key: "\xE01B", alternateKey: "\xE056", code: "Numpad1"),
            new ShiftedKey(key: "\xE01C", alternateKey: "\xE05B", code: "Numpad2"),
            new ShiftedKey(key: "\xE01D", alternateKey: "\xE055", code: "Numpad3"),
            new ShiftedKey(key: "\xE01E", alternateKey: "\xE058", code: "Numpad4"),
            new ShiftedKey(key: "\xE01F", code: "Numpad5"),
            new ShiftedKey(key: "\xE020", alternateKey: "\xE05A", code: "Numpad6"),
            new ShiftedKey(key: "\xE021", alternateKey: "\xE057", code: "Numpad7"),
            new ShiftedKey(key: "\xE022", alternateKey: "\xE059", code: "Numpad8"),
            new ShiftedKey(key: "\xE023", alternateKey: "\xE054", code: "Numpad9"),
            new ShiftedKey(key: "\xE024", code: "NumpadAdd"),
            new ShiftedKey(key: "\xE026", code: "NumpadComma"),
            new ShiftedKey(key: "\xE028", alternateKey: "\xE05D", code: "NumpadDecimal"),
            new ShiftedKey(key: "\xE029", code: "NumpadDivide"),
            new ShiftedKey(key: "\xE007", code: "NumpadEnter"),
            new ShiftedKey(key: "\xE024", code: "NumpadMultiply"),
            new ShiftedKey(key: "\xE026", code: "NumpadSubtract")
        };

        static readonly Dictionary<string, KeyLocation> keyLocations = new Dictionary<string, KeyLocation> {
            {
                "\xE007",
                new KeyLocation(description: "Enter", location: 1)
            }, {
                "\xE008",
                new KeyLocation(description: "Left Shift", location: 1)
            }, {
                "\xE009",
                new KeyLocation(description: "Left Control", location: 1)
            }, {
                "\xE00A",
                new KeyLocation(description: "Left Alt", location: 1)
            }, {
                "\xE01A",
                new KeyLocation(description: "Numpad 0", location: 3)
            }, {
                "\xE01B",
                new KeyLocation(description: "Numpad 1", location: 3)
            }, {
                "\xE01C",
                new KeyLocation(description: "Numpad 2", location: 3)
            }, {
                "\xE01D",
                new KeyLocation(description: "Numpad 3", location: 3)
            }, {
                "\xE01E",
                new KeyLocation(description: "Numpad 4", location: 3)
            }, {
                "\xE01F",
                new KeyLocation(description: "Numpad 5", location: 3)
            }, {
                "\xE020",
                new KeyLocation(description: "Numpad 6", location: 3)
            }, {
                "\xE021",
                new KeyLocation(description: "Numpad 7", location: 3)
            }, {
                "\xE022",
                new KeyLocation(description: "Numpad 8", location: 3)
            }, {
                "\xE023",
                new KeyLocation(description: "Numpad 9", location: 3)
            }, {
                "\xE024",
                new KeyLocation(description: "Numpad *", location: 3)
            }, {
                "\xE025",
                new KeyLocation(description: "Numpad +", location: 3)
            }, {
                "\xE026",
                new KeyLocation(description: "Numpad ,", location: 3)
            }, {
                "\xE027",
                new KeyLocation(description: "Numpad -", location: 3)
            }, {
                "\xE028",
                new KeyLocation(description: "Numpad .", location: 3)
            }, {
                "\xE029",
                new KeyLocation(description: "Numpad /", location: 3)
            }, {
                "\xE03D",
                new KeyLocation(description: "Left Meta", location: 1)
            }, {
                "\xE050",
                new KeyLocation(description: "Right Shift", location: 2)
            }, {
                "\xE051",
                new KeyLocation(description: "Right Control", location: 2)
            }, {
                "\xE052",
                new KeyLocation(description: "Right Alt", location: 2)
            }, {
                "\xE053",
                new KeyLocation(description: "Right Meta", location: 2)
            }, {
                "\xE054",
                new KeyLocation(description: "Numpad PageUp", location: 3)
            }, {
                "\xE055",
                new KeyLocation(description: "Numpad PageDown", location: 3)
            }, {
                "\xE056",
                new KeyLocation(description: "Numpad End", location: 3)
            }, {
                "\xE057",
                new KeyLocation(description: "Numpad Home", location: 3)
            }, {
                "\xE058",
                new KeyLocation(description: "Numpad ArrowLeft", location: 3)
            }, {
                "\xE059",
                new KeyLocation(description: "Numpad ArrowUp", location: 3)
            }, {
                "\xE05A",
                new KeyLocation(description: "Numpad ArrowRight", location: 3)
            }, {
                "\xE05B",
                new KeyLocation(description: "Numpad ArrowDown", location: 3)
            }, {
                "\xE05C",
                new KeyLocation(description: "Numpad Insert", location: 3)
            }, {
                "\xE05D",
                new KeyLocation(description: "Numpad Delete", location: 3)
            }
        };

        public static string GetNormalisedKey(string key) {
            return !normalisedKeys.ContainsKey(key: key) ? key : normalisedKeys[key: key];
        }

        public static string GetShiftedCharacterCode(string key) {
            if (key == null)
                return null;
            return shiftedKeys.Find(match: sk => sk.Key == key || sk.AlternateKey == key)?.Code;
        }

        public static int? GetKeyLocation(string key) {
            if (!keyLocations.ContainsKey(key: key))
                return 0;
            return keyLocations[key: key]?.Location;
        }

        class ShiftedKey {
            public ShiftedKey(string key, string alternateKey, string code) {
                Key = key;
                AlternateKey = alternateKey;
                Code = code;
            }

            public ShiftedKey(string key, string code) {
                Key = key;
                Code = code;
            }

            public string Key { get; }

            public string AlternateKey { get; }

            public string Code { get; }
        }

        class KeyLocation {
            public KeyLocation(string description, int location) {
                Description = description;
                Location = location;
            }

            public string Description { get; }

            public int Location { get; }
        }
    }
}