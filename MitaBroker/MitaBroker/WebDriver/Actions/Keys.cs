// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.Keys
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Collections.Generic;

namespace MitaBroker.WebDriver.Actions
{
  internal static class Keys
  {
    private static Dictionary<string, string> normalisedKeys = new Dictionary<string, string>()
    {
      {
        "\xE000",
        "Unidentified"
      },
      {
        "\xE001",
        "Cancel"
      },
      {
        "\xE002",
        "Help"
      },
      {
        "\xE003",
        "Backspace"
      },
      {
        "\xE004",
        "Tab"
      },
      {
        "\xE005",
        "Clear"
      },
      {
        "\xE006",
        "Return"
      },
      {
        "\xE007",
        "Enter"
      },
      {
        "\xE008",
        "Shift"
      },
      {
        "\xE009",
        "Control"
      },
      {
        "\xE00A",
        "Alt"
      },
      {
        "\xE00B",
        "Pause"
      },
      {
        "\xE00C",
        "Escape"
      },
      {
        "\xE00D",
        " "
      },
      {
        "\xE00E",
        "PageUp"
      },
      {
        "\xE00F",
        "PageDown"
      },
      {
        "\xE010",
        "End"
      },
      {
        "\xE011",
        "Home"
      },
      {
        "\xE012",
        "ArrowLeft"
      },
      {
        "\xE013",
        "ArrowUp"
      },
      {
        "\xE014",
        "ArrowRight"
      },
      {
        "\xE015",
        "ArrowDown"
      },
      {
        "\xE016",
        "Insert"
      },
      {
        "\xE017",
        "Delete"
      },
      {
        "\xE018",
        ";"
      },
      {
        "\xE019",
        "="
      },
      {
        "\xE01A",
        "0"
      },
      {
        "\xE01B",
        "1"
      },
      {
        "\xE01C",
        "2"
      },
      {
        "\xE01D",
        "3"
      },
      {
        "\xE01E",
        "4"
      },
      {
        "\xE01F",
        "5"
      },
      {
        "\xE020",
        "6"
      },
      {
        "\xE021",
        "7"
      },
      {
        "\xE022",
        "8"
      },
      {
        "\xE023",
        "9"
      },
      {
        "\xE024",
        "*"
      },
      {
        "\xE025",
        "+"
      },
      {
        "\xE026",
        ","
      },
      {
        "\xE027",
        "-"
      },
      {
        "\xE028",
        "."
      },
      {
        "\xE029",
        "/"
      },
      {
        "\xE031",
        "F1"
      },
      {
        "\xE032",
        "F2"
      },
      {
        "\xE033",
        "F3"
      },
      {
        "\xE034",
        "F4"
      },
      {
        "\xE035",
        "F5"
      },
      {
        "\xE036",
        "F6"
      },
      {
        "\xE037",
        "F7"
      },
      {
        "\xE038",
        "F8"
      },
      {
        "\xE039",
        "F9"
      },
      {
        "\xE03A",
        "F10"
      },
      {
        "\xE03B",
        "F11"
      },
      {
        "\xE03C",
        "F12"
      },
      {
        "\xE03D",
        "Meta"
      },
      {
        "\xE040",
        "ZenkakuHankaku"
      },
      {
        "\xE050",
        "Shift"
      },
      {
        "\xE051",
        "Control"
      },
      {
        "\xE052",
        "Alt"
      },
      {
        "\xE053",
        "Meta"
      },
      {
        "\xE054",
        "PageUp"
      },
      {
        "\xE055",
        "PageDown"
      },
      {
        "\xE056",
        "End"
      },
      {
        "\xE057",
        "Home"
      },
      {
        "\xE058",
        "ArrowLeft"
      },
      {
        "\xE059",
        "ArrowUp"
      },
      {
        "\xE05A",
        "ArrowRight"
      },
      {
        "\xE05B",
        "ArrowDown"
      },
      {
        "\xE05C",
        "Insert"
      },
      {
        "\xE05D",
        "Delete"
      }
    };
    private static List<Keys.ShiftedKey> shiftedKeys = new List<Keys.ShiftedKey>()
    {
      new Keys.ShiftedKey("`", "~", "Backquote"),
      new Keys.ShiftedKey("\\", "|", "Backslash"),
      new Keys.ShiftedKey("\xE003", "Backspace"),
      new Keys.ShiftedKey("[", "{", "BracketLeft"),
      new Keys.ShiftedKey("}", "]", "BracketRight"),
      new Keys.ShiftedKey(",", "<", "Comma"),
      new Keys.ShiftedKey("0", ")", "Digit0"),
      new Keys.ShiftedKey("1", "!", "Digit1"),
      new Keys.ShiftedKey("2", "@", "Digit2"),
      new Keys.ShiftedKey("3", "#", "Digit3"),
      new Keys.ShiftedKey("4", "$", "Digit4"),
      new Keys.ShiftedKey("5", "%", "Digit5"),
      new Keys.ShiftedKey("6", "^", "Digit6"),
      new Keys.ShiftedKey("7", "&", "Digit7"),
      new Keys.ShiftedKey("8", "*", "Digit8"),
      new Keys.ShiftedKey("9", "(", "Digit9"),
      new Keys.ShiftedKey("=", "+", "Equal"),
      new Keys.ShiftedKey("<", ">", "IntlBackslash"),
      new Keys.ShiftedKey("a", "A", "KeyA"),
      new Keys.ShiftedKey("b", "B", "KeyB"),
      new Keys.ShiftedKey("c", "C", "KeyC"),
      new Keys.ShiftedKey("d", "D", "KeyD"),
      new Keys.ShiftedKey("e", "E", "KeyE"),
      new Keys.ShiftedKey("f", "F", "KeyF"),
      new Keys.ShiftedKey("g", "G", "KeyG"),
      new Keys.ShiftedKey("h", "H", "KeyH"),
      new Keys.ShiftedKey("i", "I", "KeyI"),
      new Keys.ShiftedKey("j", "J", "KeyJ"),
      new Keys.ShiftedKey("k", "K", "KeyK"),
      new Keys.ShiftedKey("l", "L", "KeyL"),
      new Keys.ShiftedKey("m", "M", "KeyM"),
      new Keys.ShiftedKey("n", "N", "KeyN"),
      new Keys.ShiftedKey("o", "O", "KeyO"),
      new Keys.ShiftedKey("p", "P", "KeyP"),
      new Keys.ShiftedKey("q", "Q", "KeyQ"),
      new Keys.ShiftedKey("r", "R", "KeyR"),
      new Keys.ShiftedKey("s", "S", "KeyS"),
      new Keys.ShiftedKey("t", "T", "KeyT"),
      new Keys.ShiftedKey("u", "U", "KeyU"),
      new Keys.ShiftedKey("v", "V", "KeyV"),
      new Keys.ShiftedKey("w", "W", "KeyW"),
      new Keys.ShiftedKey("x", "X", "KeyX"),
      new Keys.ShiftedKey("y", "Y", "KeyY"),
      new Keys.ShiftedKey("z", "Z", "KeyZ"),
      new Keys.ShiftedKey("-", "_", "Minus"),
      new Keys.ShiftedKey(".", ">", "Period"),
      new Keys.ShiftedKey("'", "\"", "Quote"),
      new Keys.ShiftedKey(";", ":", "Semicolon"),
      new Keys.ShiftedKey("/", "?", "Slash"),
      new Keys.ShiftedKey("\xE00A", "AltLeft"),
      new Keys.ShiftedKey("\xE052", "AltRight"),
      new Keys.ShiftedKey("\xE009", "ControlLeft"),
      new Keys.ShiftedKey("\xE051", "ControlRight"),
      new Keys.ShiftedKey("\xE006", "Enter"),
      new Keys.ShiftedKey("\xE03D", "OSLeft"),
      new Keys.ShiftedKey("\xE053", "OSRight"),
      new Keys.ShiftedKey("\xE008", "ShiftLeft"),
      new Keys.ShiftedKey("\xE050", "ShiftRight"),
      new Keys.ShiftedKey(" ", "\xE00D", "Space"),
      new Keys.ShiftedKey("\xE004", "Tab"),
      new Keys.ShiftedKey("\xE017", "Delete"),
      new Keys.ShiftedKey("\xE010", "End"),
      new Keys.ShiftedKey("\xE002", "Help"),
      new Keys.ShiftedKey("\xE011", "Home"),
      new Keys.ShiftedKey("\xE016", "Insert"),
      new Keys.ShiftedKey("\xE01E", "PageDown"),
      new Keys.ShiftedKey("\xE01F", "PageUp"),
      new Keys.ShiftedKey("\xE015", "ArrowDown"),
      new Keys.ShiftedKey("\xE012", "ArrowLeft"),
      new Keys.ShiftedKey("\xE014", "ArrowRight"),
      new Keys.ShiftedKey("\xE013", "ArrowUp"),
      new Keys.ShiftedKey("\xE00C", "Escape"),
      new Keys.ShiftedKey("\xE031", "F1"),
      new Keys.ShiftedKey("\xE032", "F2"),
      new Keys.ShiftedKey("\xE033", "F3"),
      new Keys.ShiftedKey("\xE034", "F4"),
      new Keys.ShiftedKey("\xE035", "F5"),
      new Keys.ShiftedKey("\xE036", "F6"),
      new Keys.ShiftedKey("\xE037", "F7"),
      new Keys.ShiftedKey("\xE038", "F8"),
      new Keys.ShiftedKey("\xE039", "F9"),
      new Keys.ShiftedKey("\xE03A", "F10"),
      new Keys.ShiftedKey("\xE03B", "F11"),
      new Keys.ShiftedKey("\xE03C", "F12"),
      new Keys.ShiftedKey("\xE01A", "\xE05C", "Numpad0"),
      new Keys.ShiftedKey("\xE01B", "\xE056", "Numpad1"),
      new Keys.ShiftedKey("\xE01C", "\xE05B", "Numpad2"),
      new Keys.ShiftedKey("\xE01D", "\xE055", "Numpad3"),
      new Keys.ShiftedKey("\xE01E", "\xE058", "Numpad4"),
      new Keys.ShiftedKey("\xE01F", "Numpad5"),
      new Keys.ShiftedKey("\xE020", "\xE05A", "Numpad6"),
      new Keys.ShiftedKey("\xE021", "\xE057", "Numpad7"),
      new Keys.ShiftedKey("\xE022", "\xE059", "Numpad8"),
      new Keys.ShiftedKey("\xE023", "\xE054", "Numpad9"),
      new Keys.ShiftedKey("\xE024", "NumpadAdd"),
      new Keys.ShiftedKey("\xE026", "NumpadComma"),
      new Keys.ShiftedKey("\xE028", "\xE05D", "NumpadDecimal"),
      new Keys.ShiftedKey("\xE029", "NumpadDivide"),
      new Keys.ShiftedKey("\xE007", "NumpadEnter"),
      new Keys.ShiftedKey("\xE024", "NumpadMultiply"),
      new Keys.ShiftedKey("\xE026", "NumpadSubtract")
    };
    private static Dictionary<string, Keys.KeyLocation> keyLocations = new Dictionary<string, Keys.KeyLocation>()
    {
      {
        "\xE007",
        new Keys.KeyLocation("Enter", 1)
      },
      {
        "\xE008",
        new Keys.KeyLocation("Left Shift", 1)
      },
      {
        "\xE009",
        new Keys.KeyLocation("Left Control", 1)
      },
      {
        "\xE00A",
        new Keys.KeyLocation("Left Alt", 1)
      },
      {
        "\xE01A",
        new Keys.KeyLocation("Numpad 0", 3)
      },
      {
        "\xE01B",
        new Keys.KeyLocation("Numpad 1", 3)
      },
      {
        "\xE01C",
        new Keys.KeyLocation("Numpad 2", 3)
      },
      {
        "\xE01D",
        new Keys.KeyLocation("Numpad 3", 3)
      },
      {
        "\xE01E",
        new Keys.KeyLocation("Numpad 4", 3)
      },
      {
        "\xE01F",
        new Keys.KeyLocation("Numpad 5", 3)
      },
      {
        "\xE020",
        new Keys.KeyLocation("Numpad 6", 3)
      },
      {
        "\xE021",
        new Keys.KeyLocation("Numpad 7", 3)
      },
      {
        "\xE022",
        new Keys.KeyLocation("Numpad 8", 3)
      },
      {
        "\xE023",
        new Keys.KeyLocation("Numpad 9", 3)
      },
      {
        "\xE024",
        new Keys.KeyLocation("Numpad *", 3)
      },
      {
        "\xE025",
        new Keys.KeyLocation("Numpad +", 3)
      },
      {
        "\xE026",
        new Keys.KeyLocation("Numpad ,", 3)
      },
      {
        "\xE027",
        new Keys.KeyLocation("Numpad -", 3)
      },
      {
        "\xE028",
        new Keys.KeyLocation("Numpad .", 3)
      },
      {
        "\xE029",
        new Keys.KeyLocation("Numpad /", 3)
      },
      {
        "\xE03D",
        new Keys.KeyLocation("Left Meta", 1)
      },
      {
        "\xE050",
        new Keys.KeyLocation("Right Shift", 2)
      },
      {
        "\xE051",
        new Keys.KeyLocation("Right Control", 2)
      },
      {
        "\xE052",
        new Keys.KeyLocation("Right Alt", 2)
      },
      {
        "\xE053",
        new Keys.KeyLocation("Right Meta", 2)
      },
      {
        "\xE054",
        new Keys.KeyLocation("Numpad PageUp", 3)
      },
      {
        "\xE055",
        new Keys.KeyLocation("Numpad PageDown", 3)
      },
      {
        "\xE056",
        new Keys.KeyLocation("Numpad End", 3)
      },
      {
        "\xE057",
        new Keys.KeyLocation("Numpad Home", 3)
      },
      {
        "\xE058",
        new Keys.KeyLocation("Numpad ArrowLeft", 3)
      },
      {
        "\xE059",
        new Keys.KeyLocation("Numpad ArrowUp", 3)
      },
      {
        "\xE05A",
        new Keys.KeyLocation("Numpad ArrowRight", 3)
      },
      {
        "\xE05B",
        new Keys.KeyLocation("Numpad ArrowDown", 3)
      },
      {
        "\xE05C",
        new Keys.KeyLocation("Numpad Insert", 3)
      },
      {
        "\xE05D",
        new Keys.KeyLocation("Numpad Delete", 3)
      }
    };

    public static string GetNormalisedKey(string key) => !Keys.normalisedKeys.ContainsKey(key) ? key : Keys.normalisedKeys[key];

    public static string GetShiftedCharacterCode(string key)
    {
      if (key == null)
        return (string) null;
      return Keys.shiftedKeys.Find((Predicate<Keys.ShiftedKey>) (sk => sk.Key == key || sk.AlternateKey == key))?.Code;
    }

    public static int? GetKeyLocation(string key)
    {
      if (!Keys.keyLocations.ContainsKey(key))
        return new int?(0);
      return Keys.keyLocations[key]?.Location;
    }

    private class ShiftedKey
    {
      public string Key { get; }

      public string AlternateKey { get; }

      public string Code { get; }

      public ShiftedKey(string key, string alternateKey, string code)
      {
        this.Key = key;
        this.AlternateKey = alternateKey;
        this.Code = code;
      }

      public ShiftedKey(string key, string code)
      {
        this.Key = key;
        this.Code = code;
      }
    }

    private class KeyLocation
    {
      public string Description { get; }

      public int Location { get; }

      public KeyLocation(string description, int location)
      {
        this.Description = description;
        this.Location = location;
      }
    }
  }
}
