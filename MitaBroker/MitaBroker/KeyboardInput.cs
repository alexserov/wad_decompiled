// Decompiled with JetBrains decompiler
// Type: MitaBroker.KeyboardInput
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MS.Internal.Mita.Foundation;
using System.Collections.Generic;
using System.Text;

namespace MitaBroker
{
  public sealed class KeyboardInput
  {
    private static readonly Dictionary<char, string> nonPrintableKeyMap = new Dictionary<char, string>()
    {
      {
        '\xE003',
        "{BACKSPACE}"
      },
      {
        '\xE004',
        "{TAB}"
      },
      {
        '\xE006',
        "{RETURN}"
      },
      {
        '\xE007',
        "{ENTER}"
      },
      {
        '\xE00C',
        "{ESC}"
      },
      {
        '\xE00D',
        "{SPACE}"
      },
      {
        '\xE00E',
        "{PGUP}"
      },
      {
        '\xE00F',
        "{PGDN}"
      },
      {
        '\xE010',
        "{END}"
      },
      {
        '\xE011',
        "{HOME}"
      },
      {
        '\xE012',
        "{LEFT}"
      },
      {
        '\xE013',
        "{UP}"
      },
      {
        '\xE014',
        "{RIGHT}"
      },
      {
        '\xE015',
        "{DOWN}"
      },
      {
        '\xE016',
        "{INSERT}"
      },
      {
        '\xE017',
        "{DELETE}"
      },
      {
        '\xE018',
        ";"
      },
      {
        '\xE019',
        "="
      },
      {
        '\xE01A',
        "{NUMPAD0}"
      },
      {
        '\xE01B',
        "{NUMPAD1}"
      },
      {
        '\xE01C',
        "{NUMPAD2}"
      },
      {
        '\xE01D',
        "{NUMPAD3}"
      },
      {
        '\xE01E',
        "{NUMPAD4}"
      },
      {
        '\xE01F',
        "{NUMPAD5}"
      },
      {
        '\xE020',
        "{NUMPAD6}"
      },
      {
        '\xE021',
        "{NUMPAD7}"
      },
      {
        '\xE022',
        "{NUMPAD8}"
      },
      {
        '\xE023',
        "{NUMPAD9}"
      },
      {
        '\xE024',
        "{MULTIPLY}"
      },
      {
        '\xE025',
        "{ADD}"
      },
      {
        '\xE027',
        "{SUBTRACT}"
      },
      {
        '\xE028',
        "{DECIMAL}"
      },
      {
        '\xE029',
        "{DIVIDE}"
      },
      {
        '\xE031',
        "{F1}"
      },
      {
        '\xE032',
        "{F2}"
      },
      {
        '\xE033',
        "{F3}"
      },
      {
        '\xE034',
        "{F4}"
      },
      {
        '\xE035',
        "{F5}"
      },
      {
        '\xE036',
        "{F6}"
      },
      {
        '\xE037',
        "{F7}"
      },
      {
        '\xE038',
        "{F8}"
      },
      {
        '\xE039',
        "{F9}"
      },
      {
        '\xE03A',
        "{F10}"
      },
      {
        '\xE03B',
        "{F11}"
      },
      {
        '\xE03C',
        "{F12}"
      }
    };
    private static bool AltToggled = false;
    private static bool CtrlToggled = false;
    private static bool ShiftToggled = false;
    private static bool WindowsToggled = false;

    public static string Process(string inputKeySequences)
    {
      string empty = string.Empty;
      return KeyboardInput.ConvertNonPrintableCharacters(Keyboard.EscapeSpecialCharacters(inputKeySequences));
    }

    private static string ConvertNonPrintableCharacters(string inputKeySequences)
    {
      StringBuilder currentString = new StringBuilder();
      foreach (char inputKeySequence in inputKeySequences)
      {
        switch (inputKeySequence)
        {
          case '\xE000':
          case '\xE008':
          case '\xE009':
          case '\xE00A':
          case '\xE03D':
            KeyboardInput.ProcessModifiers(currentString, inputKeySequence);
            break;
          default:
            if (KeyboardInput.nonPrintableKeyMap.ContainsKey(inputKeySequence))
            {
              currentString.Append(KeyboardInput.nonPrintableKeyMap[inputKeySequence]);
              break;
            }
            currentString.Append(inputKeySequence);
            break;
        }
      }
      return currentString.ToString();
    }

    private static void ProcessModifiers(StringBuilder currentString, char inputKey)
    {
      switch (inputKey)
      {
        case '\xE000':
          if (KeyboardInput.ShiftToggled)
            KeyboardInput.ProcessModifiers(currentString, '\xE008');
          if (KeyboardInput.CtrlToggled)
            KeyboardInput.ProcessModifiers(currentString, '\xE009');
          if (KeyboardInput.AltToggled)
            KeyboardInput.ProcessModifiers(currentString, '\xE00A');
          if (!KeyboardInput.WindowsToggled)
            break;
          KeyboardInput.ProcessModifiers(currentString, '\xE03D');
          break;
        case '\xE008':
          currentString.Append(KeyboardInput.ShiftToggled ? "{SHIFT UP}" : "{SHIFT DOWN}");
          KeyboardInput.ShiftToggled = !KeyboardInput.ShiftToggled;
          break;
        case '\xE009':
          currentString.Append(KeyboardInput.CtrlToggled ? "{CONTROL UP}" : "{CONTROL DOWN}");
          KeyboardInput.CtrlToggled = !KeyboardInput.CtrlToggled;
          break;
        case '\xE00A':
          currentString.Append(KeyboardInput.AltToggled ? "{ALT UP}" : "{ALT DOWN}");
          KeyboardInput.AltToggled = !KeyboardInput.AltToggled;
          break;
        case '\xE03D':
          currentString.Append(KeyboardInput.WindowsToggled ? "{WIN UP}" : "{WIN DOWN}");
          KeyboardInput.WindowsToggled = !KeyboardInput.WindowsToggled;
          break;
      }
    }
  }
}
