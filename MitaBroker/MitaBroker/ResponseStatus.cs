// Decompiled with JetBrains decompiler
// Type: MitaBroker.ResponseStatus
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

namespace MitaBroker
{
  internal enum ResponseStatus
  {
    Success = 0,
    NoSuchDriver = 6,
    NoSuchElement = 7,
    NoSuchFrame = 8,
    UnknownCommand = 9,
    StaleElementReference = 10, // 0x0000000A
    ElementNotVisible = 11, // 0x0000000B
    InvalidElementState = 12, // 0x0000000C
    UnknownError = 13, // 0x0000000D
    ElementIsNotSelectable = 15, // 0x0000000F
    JavaScriptError = 17, // 0x00000011
    XPathLookupError = 19, // 0x00000013
    Timeout = 21, // 0x00000015
    NoSuchWindow = 23, // 0x00000017
    InvalidCookieDomain = 24, // 0x00000018
    UnableToSetCookie = 25, // 0x00000019
    UnexpectedAlertOpen = 26, // 0x0000001A
    NoAlertOpenError = 27, // 0x0000001B
    ScriptTimeout = 28, // 0x0000001C
    InvalidElementCoordinates = 29, // 0x0000001D
    IMENotAvailable = 30, // 0x0000001E
    IMEEngineActivationFailed = 31, // 0x0000001F
    InvalidSelector = 32, // 0x00000020
    SessionNotCreatedException = 33, // 0x00000021
    MoveTargetOutOfBounds = 34, // 0x00000022
    InvalidArgument = 100, // 0x00000064
    InvalidSessionId = 101, // 0x00000065
    UnableToCaptureScreen = 102, // 0x00000066
    UnknownMethod = 103, // 0x00000067
    UnsupportedOperation = 104, // 0x00000068
    ElementIsNotInteractable = 105, // 0x00000069
  }
}
