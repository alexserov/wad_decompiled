// Decompiled with JetBrains decompiler
// Type: MitaBroker.IRequestHandler
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Runtime.InteropServices;

namespace MitaBroker
{
  [Guid("42A03D14-2BB7-4A56-B25B-C2F61BF46E33")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  public interface IRequestHandler
  {
    int LaunchApplication(string capabilitiesJSONString);

    void CloseApplication();

    void QuitApplication(bool forceQuitApp);

    void SetImplicitTimeout(int timeoutMs);

    string GetCapabilities(string capabilitiesSet);

    string GetLastErrorMessage();

    string GetActiveElement();

    string GetApplicationObject();

    string SearchElement(string searchTarget, string locatorStrategy, string startNodeId);

    string SearchMultipleElements(string searchTarget, string locatorStrategy, string startNodeId);

    string GetElementAttribute(string elementId, string attributeName);

    string GetElementProperty(string elementId, string propertyName);

    int ActionOnElement(string elementId, string actionName, string data);

    bool GetElementState(string elementId, string state);

    int GetElementStatus(string elementId);

    string GetApplicationId();

    IntPtr GetApplicationHwnd();

    int Navigate(string navigationType);

    string GetProperty(string propertyName);

    string GetTopLevelWindows();

    int SwitchToWindow(string windowHandle);

    string GetWindowProperty(string windowHandle, string propertyName);

    int SetWindowProperty(string windowHandle, string propertyName, string JSONParameters);

    string GetSource();

    int SendMouse(string actionType, string JSONParameters);

    int SendTouch(string touchType, string JSONParameters);

    int SendMultiTouch(string JSONObject);

    string GetLocation();

    void ConvertClientToScreen(int xClient, int yClient, out int xScreen, out int yScreen);

    void ConvertScreenToClient(int xScreen, int yScreen, out int xClient, out int yClient);

    int SendZoom(int centerX, int centerY, int distanceStart, int distanceEnd);

    int SendPinch(int centerX, int centerY, int distanceStart, int distanceEnd);

    int SendActions(string JSONParameters);

    int ReleaseActions();

    int GetApplicationType();

    bool IsExperimentalW3C();
  }
}
