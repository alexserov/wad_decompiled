// Decompiled with JetBrains decompiler
// Type: MitaBroker.RequestHandler
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Xml;
using System.Xml.XPath;
using MitaBroker.WebDriver.Actions;
using MS.Internal.Mita.Foundation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Controls;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MitaBroker {
    [ClassInterface(classInterfaceType: ClassInterfaceType.None), ComSourceInterfaces(sourceInterface: typeof(IRequestHandler)), Guid(guid: "6015EC6C-0919-4A39-85F6-F9962A09F5C7"), ComVisible(visibility: true)]
    public sealed class RequestHandler : IRequestHandler {
        const int defaultWindowMatchingRetryCount = 2;
        const int UIA_E_ELEMENTNOTAVAILABLE = -2147220991;
        const string ApplicationFrameWindowClassName = "ApplicationFrameWindow";
        const string AppIdCreatedFromWindowHandlePrefix = "From window handle: ";
        const string AppIdRoot = "Root";
        readonly TimeSpan defaultClosingWaitTime = TimeSpan.FromSeconds(value: 5.0);
        readonly TimeSpan defaultLocationWaitTime = TimeSpan.FromSeconds(value: 10.0);
        readonly TimeSpan defaultSearchInterval = TimeSpan.FromMilliseconds(value: 500.0);
        readonly TimeSpan defaultStartupWaitTime = TimeSpan.FromSeconds(value: 5.0);
        string applicationId = string.Empty;
        UIObject applicationObject;
        TargetAppType appType = TargetAppType.NotSet;
        EventWaitHandle GeoLocationWaiter = new ManualResetEvent(initialState: false);
        string lastErrorMessage = string.Empty;
        Dictionary<int, PointI> lastPoints = new Dictionary<int, PointI>();
        int processId = -1;
        ActionsHandler sessionActionsHandler;
        Capabilities sessionCapabilities;
        TimeSpan sessionImplicitTimeout = TimeSpan.Zero;
        KnownElements sessionKnownElements = new KnownElements();
        int sessionRetryCount = 5;
        string TelemetryGuid;

        public RequestHandler() {
            this.sessionActionsHandler = new ActionsHandler(sessionKnownElements: this.sessionKnownElements);
        }

        public int LaunchApplication(string capabilitiesJSONString) {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                if (!string.IsNullOrEmpty(value: capabilitiesJSONString))
                    this.sessionCapabilities = new Capabilities(requestedCapabilitiesJSONString: capabilitiesJSONString);
                if (this.sessionCapabilities == null)
                    throw new ArgumentException(message: "Missing capabilities on session creation");
                if (this.sessionCapabilities.GetCapabilities(capabilitySet: "unsupportedRequiredCapabilities") != null)
                    throw new Exception(message: "There is an unsupported RequiredCapabilities: " + GetCapabilities(capabilitiesSet: "unsupportedRequiredCapabilities"));
                if (this.sessionCapabilities.GetCapabilityValue(key: "app", capabilitySet: "supportedCapabilities") != null && this.sessionCapabilities.GetCapabilityValue(key: "appTopLevelWindow", capabilitySet: "supportedCapabilities") == null) {
                    var appId = this.sessionCapabilities.GetCapabilityValue(key: "app", capabilitySet: "supportedCapabilities").ToString();
                    var empty1 = string.Empty;
                    var empty2 = string.Empty;
                    if (string.IsNullOrEmpty(value: appId))
                        throw new ArgumentException(message: "Capability: app cannot be empty");
                    var capabilityValue1 = this.sessionCapabilities.GetCapabilityValue(key: "appArguments", capabilitySet: "supportedCapabilities");
                    if (capabilityValue1 != null)
                        empty1 = capabilityValue1.ToString();
                    var capabilityValue2 = this.sessionCapabilities.GetCapabilityValue(key: "appWorkingDir", capabilitySet: "supportedCapabilities");
                    if (capabilityValue2 != null)
                        empty2 = capabilityValue2.ToString();
                    var capabilityValue3 = this.sessionCapabilities.GetCapabilityValue(key: "ms:waitForAppLaunch", capabilitySet: "supportedCapabilities");
                    var initialWaitTime = capabilityValue3 != null ? short.Parse(s: capabilityValue3.ToString()) : 0;
                    responseStatus = LaunchApplication(appId: appId, appArguments: empty1, appWorkingDirectory: empty2, initialWaitTime: initialWaitTime);
                } else {
                    var windowHandle = this.sessionCapabilities.GetCapabilityValue(key: "appTopLevelWindow", capabilitySet: "supportedCapabilities") != null && this.sessionCapabilities.GetCapabilityValue(key: "app", capabilitySet: "supportedCapabilities") == null ? this.sessionCapabilities.GetCapabilityValue(key: "appTopLevelWindow", capabilitySet: "supportedCapabilities").ToString() : throw new ArgumentException(message: "Bad capabilities. Specify either app or appTopLevelWindow to create a session");
                    this.applicationObject = !string.IsNullOrEmpty(value: windowHandle) ? GetObjectFromTopLevelWindowHandle(windowHandle: windowHandle, windowProcessId: out this.processId) : throw new ArgumentException(message: "Capability: appTopLevelWindow cannot be empty");
                    this.applicationId = "From window handle: " + windowHandle;
                    this.appType = TargetAppType.HandleToWindow;
                }

                if (this.applicationObject != null)
                    if (this.processId > 0) {
                        this.applicationObject.SetFocus();
                        this.sessionKnownElements.Add(element: this.applicationObject);
                        this.sessionActionsHandler.SetSessionTopLevelWindowHandle(sessionTopLevelWindowHandle: this.applicationObject.NativeWindowHandle);
                        responseStatus = ResponseStatus.Success;
                    }
            } catch (ArgumentException ex) {
                responseStatus = ResponseStatus.InvalidArgument;
                this.lastErrorMessage = ex.Message;
            } catch (Exception ex) when (ex.HResult == -2147220991) {
                responseStatus = ResponseStatus.NoSuchWindow;
                this.lastErrorMessage = "Cannot find active window specified by capabilities: appTopLevelWindow";
            } catch (Exception ex) {
                this.lastErrorMessage = ex.Message;
            }

            return (int) responseStatus;
        }

        public void CloseApplication() {
            try {
                if (!(this.applicationObject != null) || !this.applicationObject.GetSupportedPatterns().ToList().Contains(item: WindowPatternIdentifiers.Pattern))
                    return;
                var flag = true;
                var window = new Window(uiObject: this.applicationObject);
                using (var windowClosedWaiter = window.GetWindowClosedWaiter()) {
                    window.Close();
                    flag = !windowClosedWaiter.TryWait(timeout: this.defaultClosingWaitTime);
                }

                if (flag)
                    try {
                        flag = window.ProcessId > 0 && window.LocalizedControlType != null;
                    } catch {
                        flag = false;
                    }

                if (flag)
                    return;
                this.applicationObject = null;
                this.sessionKnownElements.Clear();
                this.sessionActionsHandler.SetSessionTopLevelWindowHandle(sessionTopLevelWindowHandle: IntPtr.Zero);
            } catch {
            }
        }

        public void QuitApplication(bool forceQuitApp) {
            try {
                if (this.applicationId.StartsWith(value: "From window handle: ") || !(this.applicationId != "Root"))
                    return;
                CloseApplication();
                if (((!(this.applicationObject != null) ? 0 : this.processId > 0 ? 1 : 0) & (forceQuitApp ? 1 : 0)) == 0)
                    return;
                Process.GetProcessById(processId: this.processId).Kill();
            } catch {
            }
        }

        public string GetApplicationId() {
            return this.applicationId;
        }

        public string GetCapabilities(string capabilitiesSet) {
            var str = string.Empty;
            if (this.sessionCapabilities != null)
                str = JsonConvert.SerializeObject(value: this.sessionCapabilities.GetCapabilities(capabilitySet: capabilitiesSet));
            return str;
        }

        public void SetImplicitTimeout(int timeoutMs) {
            if (timeoutMs < 0)
                return;
            this.sessionImplicitTimeout = TimeSpan.FromMilliseconds(value: Convert.ToDouble(value: timeoutMs));
            this.sessionRetryCount = Math.Max(val1: timeoutMs / (int) this.defaultSearchInterval.TotalMilliseconds, val2: 1);
        }

        public string GetLastErrorMessage() {
            return this.lastErrorMessage;
        }

        public string GetActiveElement() {
            var str = string.Empty;
            try {
                if (this.applicationObject != null) {
                    var focused = UIObject.Focused;
                    UICollection.Timeout = TimeSpan.Zero;
                    if (focused != null) {
                        if (!(this.applicationObject == UIObject.Root))
                            if (!this.applicationObject.Descendants.Contains(automationId: focused.AutomationId))
                                goto label_7;
                        str = this.sessionKnownElements.Add(element: focused);
                    }
                }
            } catch {
            }

            label_7:
            return str;
        }

        public string GetApplicationObject() {
            var str = string.Empty;
            if (this.applicationObject != null)
                str = this.applicationObject.RuntimeId;
            return str;
        }

        public string SearchElement(string searchTarget, string locatorStrategy, string startNodeId) {
            UIObject element = null;
            var targetId = string.Empty;
            this.TelemetryGuid = Guid.NewGuid().ToString();
            try {
                if (locatorStrategy == "id") {
                    element = this.sessionKnownElements.Get(elementId: searchTarget);
                    targetId = this.sessionKnownElements.Add(element: element);
                }

                if (element == null)
                    this.sessionKnownElements.Add(element: FindSingleElement(searchTarget: searchTarget, locatorStrategy: locatorStrategy, startNodeId: startNodeId, targetId: out targetId), elementId: targetId);
                else
                    Telemetry.LogRequest(codePath: "SearchElement:FoundTargetElementFromCache", locatorStrategy: locatorStrategy, timeElapsed: "", guid: this.TelemetryGuid, result: targetId);
            } catch (XPathException ex) {
                targetId = "Invalid XPath expression: " + searchTarget;
                Telemetry.LogRequest(codePath: "SearchElement: XPathExpressionException", locatorStrategy: locatorStrategy, timeElapsed: "", guid: this.TelemetryGuid, result: targetId);
            } catch {
            }

            return targetId;
        }

        public string GetSource() {
            var empty = string.Empty;
            using (var stringWriter = new StringWriter()) {
                using (var w = XmlWriter.Create(output: stringWriter)) {
                    new UIXmlDom(applicationRoot: this.applicationObject).CreateUIXmlDocument(startNode: this.applicationObject).WriteTo(w: w);
                    w.Flush();
                    return stringWriter.GetStringBuilder().ToString();
                }
            }
        }

        public string SearchMultipleElements(
            string searchTarget,
            string locatorStrategy,
            string startNodeId) {
            var str = "[]";
            var uiObject = !string.IsNullOrEmpty(value: startNodeId) ? this.sessionKnownElements.Get(elementId: startNodeId) : this.applicationObject;
            if (uiObject != null) {
                List<UIObject> source = null;
                try {
                    if (locatorStrategy == "xpath") {
                        source = FindMultipleElementsXPath(searchTarget: searchTarget, startNode: uiObject);
                    } else if (locatorStrategy == "tag name") {
                        source = FindMultipleElementsXPath(searchTarget: "//" + searchTarget, startNode: uiObject);
                    } else {
                        var matchCondition = GetMatchCondition(searchTarget: searchTarget, locatorStrategy: locatorStrategy);
                        if (matchCondition != null) {
                            ResetUICollectionTimeout();
                            source = new UIBreadthFirstDescendants<UIObject>(root: uiObject, factory: UIObject.Factory).FindMultiple(condition: matchCondition).ToList();
                            if (uiObject.Matches(condition: matchCondition))
                                source.Insert(index: 0, item: uiObject);
                        }
                    }

                    if (source != null)
                        str = new JArray(content: source.Select(selector: entry => new JObject(content: new JProperty(name: "ELEMENT", content: this.sessionKnownElements.Add(element: entry))))).ToString();
                } catch (XPathException ex) {
                    str = "Invalid XPath expression: " + searchTarget;
                } catch (Exception ex) {
                    Console.WriteLine(value: string.Empty);
                    Console.WriteLine(value: "### SearchMultipleElements exception: " + ex.Message);
                    Console.WriteLine(value: string.Empty);
                }
            }

            return str;
        }

        public string GetElementAttribute(string elementId, string attributeName) {
            var str = string.Empty;
            var uiObject = this.sessionKnownElements.Get(elementId: elementId);
            if (uiObject != null)
                if (UIProperty.Exists(name: attributeName))
                    try {
                        var property = UIProperty.Get(name: attributeName);
                        str = !(property.Name == "RuntimeId") ? !(property.Name == "ClickablePoint") ? uiObject.GetProperty(property: property).ToString() : uiObject.GetClickablePoint().ToString() : uiObject.RuntimeId;
                    } catch {
                    }

            return str;
        }

        public string GetElementProperty(string elementId, string propertyName) {
            var str = string.Empty;
            var uiObject1 = this.sessionKnownElements.Get(elementId: elementId);
            if (uiObject1 != null)
                try {
                    if (!(propertyName == "text")) {
                        if (!(propertyName == "name")) {
                            if (!(propertyName == "size")) {
                                if (!(propertyName == "location")) {
                                    if (propertyName == "center point") {
                                        var dictionary = new Dictionary<string, int>();
                                        var rectangleTopLeft = uiObject1.GetBoundingRectangleTopLeft(appRootWindowHandle: this.applicationObject.NativeWindowHandle);
                                        var size = uiObject1.GetAdjustedBoundingRectangle().Size;
                                        dictionary.Add(key: "x", value: rectangleTopLeft.X + size.Width / 2);
                                        dictionary.Add(key: "y", value: rectangleTopLeft.Y + size.Height / 2);
                                        str = JsonConvert.SerializeObject(value: dictionary);
                                    }
                                } else {
                                    var dictionary = new Dictionary<string, int>();
                                    var rectangleTopLeft = uiObject1.GetBoundingRectangleTopLeft(appRootWindowHandle: this.applicationObject.NativeWindowHandle);
                                    dictionary.Add(key: "x", value: rectangleTopLeft.X);
                                    dictionary.Add(key: "y", value: rectangleTopLeft.Y);
                                    str = JsonConvert.SerializeObject(value: dictionary);
                                }
                            } else {
                                var dictionary = new Dictionary<string, int>();
                                var size = uiObject1.GetAdjustedBoundingRectangle().Size;
                                dictionary.Add(key: "width", value: size.Width);
                                dictionary.Add(key: "height", value: size.Height);
                                str = JsonConvert.SerializeObject(value: dictionary);
                            }
                        } else {
                            str = uiObject1.ControlType.ProgrammaticName;
                        }
                    } else {
                        var list = uiObject1.GetSupportedPatterns().ToList();
                        if (list.Contains(item: TextPatternIdentifiers.Pattern))
                            str = new TextImplementation(uiObject: uiObject1).DocumentRange.GetText(maxLength: -1);
                        else if (list.Contains(item: ValuePatternIdentifiers.Pattern))
                            str = new ValueImplementation(uiObject: uiObject1).Value;
                        else if (list.Contains(item: RangeValuePatternIdentifiers.Pattern))
                            str = new RangeValueImplementation(uiObject: uiObject1).Value.ToString();
                        else if (list.Contains(item: SelectionPatternIdentifiers.Pattern))
                            foreach (var uiObject2 in new SelectionImplementation<UIObject>(uiObject: uiObject1, itemFactory: UIObject.Factory).Selection) {
                                if (str != string.Empty)
                                    str += ", ";
                                str += uiObject2.Name;
                            }
                        else if (uiObject1.Name != null)
                            str = uiObject1.Name;

                        str = str.Normalize();
                    }
                } catch {
                }

            return str;
        }

        public int ActionOnElement(string elementId, string actionName, string data) {
            var responseStatus = ResponseStatus.UnknownError;
            UIObject uiObject;
            if (!string.IsNullOrEmpty(value: elementId)) {
                uiObject = this.sessionKnownElements.Get(elementId: elementId);
            } else {
                var activeElement = GetActiveElement();
                uiObject = string.IsNullOrEmpty(value: activeElement) ? this.applicationObject : this.sessionKnownElements.Get(elementId: activeElement);
            }

            if (uiObject != null)
                try {
                    if (uiObject.GetSupportedPatterns().ToList().Contains(item: ScrollItemPatternIdentifiers.Pattern) && uiObject.IsOffscreen) {
                        new ScrollItemImplementation(uiObject: uiObject).ScrollIntoView();
                        Thread.Sleep(timeout: TimeSpan.FromSeconds(value: 1.0));
                    }

                    if (!uiObject.IsOffscreen || string.IsNullOrEmpty(value: elementId) && actionName == "value") {
                        if (!(actionName == "clear")) {
                            if (!(actionName == "click")) {
                                if (actionName == "value") {
                                    var text = KeyboardInput.Process(inputKeySequences: data);
                                    if (text.Length > 0)
                                        uiObject.SendKeys(text: text);
                                }
                            } else {
                                this.applicationObject.SetFocus();
                                uiObject.Click();
                            }
                        } else if (uiObject.ControlType == ControlType.Edit) {
                            new Edit(uiObject: uiObject).SetValue(value: string.Empty);
                        }

                        responseStatus = ResponseStatus.Success;
                    } else {
                        responseStatus = ResponseStatus.ElementIsNotInteractable;
                    }
                } catch {
                }

            return (int) responseStatus;
        }

        public bool GetElementState(string elementId, string state) {
            var flag = false;
            var uiObject = this.sessionKnownElements.Get(elementId: elementId);
            if (uiObject != null)
                try {
                    if (!(state == "displayed")) {
                        if (!(state == "enabled")) {
                            if (state == "selected")
                                foreach (var supportedPattern in uiObject.GetSupportedPatterns())
                                    if (supportedPattern != null) {
                                        if (supportedPattern.ProgrammaticName == TogglePatternIdentifiers.Pattern.ProgrammaticName) {
                                            flag = new ToggleImplementation(uiObject: uiObject).ToggleState.ToString() == "On";
                                            break;
                                        }

                                        if (supportedPattern.ProgrammaticName == SelectionItemPattern.Pattern.ProgrammaticName) {
                                            flag = new SelectionItemImplementation<UIObject>(uiObject: uiObject, containerFactory: UIObject.Factory).IsSelected;
                                            break;
                                        }
                                    }
                        } else {
                            flag = uiObject.IsEnabled;
                        }
                    } else {
                        flag = !uiObject.IsOffscreen;
                    }
                } catch {
                }

            return flag;
        }

        public int GetElementStatus(string elementId) {
            return (int) this.sessionKnownElements.GetStatus(elementId: elementId);
        }

        public IntPtr GetApplicationHwnd() {
            var num = IntPtr.Zero;
            if (this.applicationObject != null)
                try {
                    num = this.applicationObject.NativeWindowHandle;
                } catch {
                    this.applicationObject = null;
                }

            return num;
        }

        public int Navigate(string navigationType) {
            var responseStatus = ResponseStatus.UnknownError;
            if (navigationType == "back") {
                this.applicationObject.SetFocus();
                this.applicationObject.SendKeys(text: "{ALT DOWN}{LEFT}{ALT UP}");
                responseStatus = ResponseStatus.Success;
            } else if (navigationType == "forward") {
                this.applicationObject.SetFocus();
                this.applicationObject.SendKeys(text: "{ALT DOWN}{RIGHT}{ALT UP}");
                responseStatus = ResponseStatus.Success;
            }

            return (int) responseStatus;
        }

        public string GetProperty(string propertyName) {
            var str = string.Empty;
            if (this.applicationObject != null)
                try {
                    if (!(propertyName == "title")) {
                        if (propertyName == "orientation")
                            str = this.applicationObject.Orientation == OrientationType.Vertical ? "PORTRAIT" : "LANDSCAPE";
                    } else {
                        str = this.applicationObject.Name;
                    }
                } catch {
                }

            return str;
        }

        public string GetTopLevelWindows() {
            var empty = string.Empty;
            try {
                IEnumerable<UIObject> source;
                if (this.applicationObject == null) {
                    source = Enumerable.Empty<UIObject>();
                } else {
                    var str = string.Empty;
                    try {
                        str = this.applicationObject.ClassName;
                    } catch {
                    }

                    source = !(str == "ApplicationFrameWindow") ? MultipleWindows.GetTopLevelWindowsClassicApp(processId: this.processId) : MultipleWindows.GetTopLevelWindowsModernApp(processId: this.processId);
                }

                empty = new JArray(content: source.Select(selector: entry => new JValue(value: "0x" + entry.NativeWindowHandle.ToString(format: "X8")))).ToString();
            } catch {
            }

            return empty;
        }

        public int SwitchToWindow(string windowHandle) {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                var windowProcessId = -1;
                var levelWindowHandle = GetObjectFromTopLevelWindowHandle(windowHandle: windowHandle, windowProcessId: out windowProcessId);
                if (windowProcessId != this.processId)
                    throw new ArgumentException(message: "Window handle does not belong to the same process/application");
                this.applicationObject = levelWindowHandle;
                this.applicationObject.SetFocus();
                this.sessionKnownElements.Add(element: this.applicationObject);
                responseStatus = ResponseStatus.Success;
            } catch (ArgumentException ex) {
                responseStatus = ResponseStatus.InvalidArgument;
                this.lastErrorMessage = ex.Message;
            } catch (Exception ex) when (ex.HResult == -2147220991) {
                responseStatus = ResponseStatus.NoSuchWindow;
            } catch {
            }

            return (int) responseStatus;
        }

        public string GetWindowProperty(string windowHandle, string propertyName) {
            var str = string.Empty;
            try {
                var uiObject = UIObject.FromHandle(hwnd: (IntPtr) Convert.ToInt32(value: windowHandle, fromBase: 16));
                if (uiObject != null)
                    if (uiObject.ControlType == ControlType.Window) {
                        var boundingRectangle = uiObject.GetAdjustedBoundingRectangle();
                        if (!(propertyName == "size")) {
                            if (propertyName == "position") {
                                var dictionary = new Dictionary<string, int>();
                                var topLeft = boundingRectangle.TopLeft;
                                dictionary.Add(key: "x", value: topLeft.X);
                                dictionary.Add(key: "y", value: topLeft.Y);
                                str = JsonConvert.SerializeObject(value: dictionary);
                            }
                        } else {
                            str = JsonConvert.SerializeObject(value: new Dictionary<string, int> {
                                {
                                    "width",
                                    boundingRectangle.Width
                                }, {
                                    "height",
                                    boundingRectangle.Height
                                }
                            });
                        }
                    }
            } catch {
            }

            return str;
        }

        public int SetWindowProperty(string windowHandle, string propertyName, string JSONParameters) {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                var uiObject = UIObject.FromHandle(hwnd: (IntPtr) Convert.ToInt32(value: windowHandle, fromBase: 16));
                if (uiObject != null)
                    if (uiObject.ControlType == ControlType.Window) {
                        var windowImplementation = new WindowImplementation(uiObject: uiObject);
                        if (!(propertyName == "maximize")) {
                            if (!(propertyName == "position")) {
                                if (propertyName == "size") {
                                    windowImplementation.SetWindowVisualState(state: WindowVisualState.Normal);
                                    var transformImplementation = new TransformImplementation(uiObject: uiObject);
                                    var rectangleOffsetSize = uiObject.GetAdjustedBoundingRectangleOffsetSize();
                                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(value: JSONParameters);
                                    var num1 = Convert.ToDouble(value: dictionary[key: "width"].ToString()) + rectangleOffsetSize.Width;
                                    var num2 = Convert.ToDouble(value: dictionary[key: "height"].ToString()) + rectangleOffsetSize.Height;
                                    var width = num1;
                                    var height = num2;
                                    transformImplementation.Resize(width: width, height: height);
                                }
                            } else {
                                windowImplementation.SetWindowVisualState(state: WindowVisualState.Normal);
                                var transformImplementation = new TransformImplementation(uiObject: uiObject);
                                var rectangleOffsetPosition = uiObject.GetAdjustedBoundingRectangleOffsetPosition();
                                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(value: JSONParameters);
                                var num1 = Convert.ToDouble(value: dictionary[key: "x"]) + rectangleOffsetPosition.X;
                                var num2 = Convert.ToDouble(value: dictionary[key: "y"]) + rectangleOffsetPosition.Y;
                                var x = num1;
                                var y = num2;
                                transformImplementation.Move(x: x, y: y);
                            }
                        } else {
                            windowImplementation.SetWindowVisualState(state: WindowVisualState.Maximized);
                        }

                        responseStatus = ResponseStatus.Success;
                    }
            } catch {
            }

            return (int) responseStatus;
        }

        public int SendZoom(int centerX, int centerY, int distanceStart, int distanceEnd) {
            using (InputController.Activate(inputType: PointerInputType.MultiTouch)) {
                PointerInput.Move(point: new PointI(x: centerX, y: centerY));
                MultiPointGesture.Stretch(direction: MultiPointGesture.DefaultPinchStretchDirection, duration: MultiPointGesture.DefaultPinchStretchDuration, startDistance: (uint) distanceStart, endDistance: (uint) distanceEnd);
            }

            return 0;
        }

        public int SendPinch(int centerX, int centerY, int distanceStart, int distanceEnd) {
            using (InputController.Activate(inputType: PointerInputType.MultiTouch)) {
                PointerInput.Move(point: new PointI(x: centerX, y: centerY));
                MultiPointGesture.Pinch(direction: MultiPointGesture.DefaultPinchStretchDirection, duration: MultiPointGesture.DefaultPinchStretchDuration, startDistance: (uint) distanceStart, endDistance: (uint) distanceEnd);
            }

            return 0;
        }

        public int SendMouse(string actionType, string JSONParameters) {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(value: JSONParameters);
                switch (actionType) {
                    case "buttondown":
                    case "buttonup":
                    case "click":
                        var buttonNumber = dictionary.ContainsKey(key: "button") ? Convert.ToInt32(value: dictionary[key: "button"]) : 0;
                        responseStatus = MouseHandler.SendMouseAction(actionType: actionType, buttonNumber: buttonNumber);
                        break;
                    case "doubleclick":
                        responseStatus = MouseHandler.SendMouseDoubleClick();
                        break;
                    case "moveto_element_center":
                        var element1 = this.sessionKnownElements.Get(elementId: Convert.ToString(value: dictionary[key: "element"]));
                        responseStatus = MouseHandler.SendMouseMoveToElementCenter(mouseMoveType: actionType, element: element1);
                        break;
                    case "moveto_element_relative":
                        var int32_1 = Convert.ToInt32(value: dictionary[key: "xoffset"]);
                        var int32_2 = Convert.ToInt32(value: dictionary[key: "yoffset"]);
                        var element2 = this.sessionKnownElements.Get(elementId: Convert.ToString(value: dictionary[key: "element"]));
                        responseStatus = MouseHandler.SendMouseMoveToElementRelative(mouseMoveType: actionType, element: element2, xOffset: int32_1, yOffset: int32_2);
                        break;
                    case "moveto_relative":
                        var int32_3 = Convert.ToInt32(value: dictionary[key: "xoffset"]);
                        var int32_4 = Convert.ToInt32(value: dictionary[key: "yoffset"]);
                        responseStatus = MouseHandler.SendMouseMoveToRelative(mouseMoveType: actionType, xOffset: int32_3, yOffset: int32_4);
                        break;
                    default:
                        throw new Exception(message: "Unrecognized action type: " + actionType);
                }
            } catch (ArgumentException ex) {
                responseStatus = ResponseStatus.InvalidArgument;
                this.lastErrorMessage = ex.Message;
            } catch (Exception ex) {
                this.lastErrorMessage = ex.Message;
            }

            return (int) responseStatus;
        }

        public int SendTouch(string touchType, string JSONParameters) {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                int xoffset;
                int yoffset;
                UIObject uiObject;
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(value: JSONParameters);
                switch (touchType) {
                    case "move":
                    case "down":
                    case "up":
                        int xScreen;
                        int yScreen;
                        ConvertClientToScreen(xClient: Convert.ToInt32(value: dictionary[key: "x"]), yClient: Convert.ToInt32(value: dictionary[key: "y"]), xScreen: out xScreen, yScreen: out yScreen);
                        responseStatus = TouchHandler.SendTouchTypePress(touchType: touchType, x: xScreen, y: yScreen);
                        break;
                    case "flick":

                        if (dictionary.ContainsKey(key: "element") && dictionary.ContainsKey(key: "xoffset") && dictionary.ContainsKey(key: "yoffset")) {
                            xoffset = Convert.ToInt32(value: dictionary[key: "xoffset"]);
                            yoffset = Convert.ToInt32(value: dictionary[key: "yoffset"]);
                            uiObject = this.sessionKnownElements.Get(elementId: Convert.ToString(value: dictionary[key: "element"]));
                            var flickYOffset = yoffset;
                            var flickElement = uiObject;
                            responseStatus = TouchHandler.SendTouchFlick(flickXOffset: xoffset, flickYOffset: flickYOffset, flickElement: flickElement);
                            return (int) responseStatus;
                        } else if (dictionary.ContainsKey(key: "xspeed")) {
                            if (dictionary.ContainsKey(key: "yspeed")) {
                                responseStatus = TouchHandler.SendTouchFlick(mainElement: this.applicationObject, xSpeed: Convert.ToInt32(value: dictionary[key: "xspeed"]), ySpeed: Convert.ToInt32(value: dictionary[key: "yspeed"]));
                                return (int) responseStatus;
                            }

                            return (int) responseStatus;
                        } else {
                            return (int) responseStatus;
                        }

                    case "click":
                    case "doubleclick":
                    case "longclick":
                        break;
                    case "scroll":
                        xoffset = Convert.ToInt32(value: dictionary[key: "xoffset"]);
                        yoffset = Convert.ToInt32(value: dictionary[key: "yoffset"]);
                        uiObject = !dictionary.ContainsKey(key: "element") ? this.applicationObject : this.sessionKnownElements.Get(elementId: Convert.ToString(value: dictionary[key: "element"]));
                        var scrollElement = uiObject;
                        responseStatus = TouchHandler.SendTouchTypeScroll(xOffset: xoffset, yOffset: yoffset, scrollElement: scrollElement);
                        break;
                    default:
                        return (int) responseStatus;
                }

                var clickElement = this.sessionKnownElements.Get(elementId: Convert.ToString(value: dictionary[key: "element"]));
                responseStatus = TouchHandler.SendTouchTypeClick(touchType: touchType, clickElement: clickElement);
            } catch (Exception ex) {
            }

            return (int) responseStatus;
        }

        public int SendMultiTouch(string JSONObject) {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                var dictionaryArray1 = JsonConvert.DeserializeObject<Dictionary<string, object>[][]>(value: JSONObject);
                using (InputController.Activate(inputType: PointerInputType.MultiTouch)) {
                    var injectionData = new MultiTouchInjectionData[dictionaryArray1.Length];
                    var key = 0;
                    foreach (var dictionaryArray2 in dictionaryArray1) {
                        var touchInjectionData = new MultiTouchInjectionData();
                        touchInjectionData.Actions = new MultiAction[dictionaryArray2.Length];
                        var num1 = 0;
                        foreach (var dictionary1 in dictionaryArray2) {
                            var multiAction1 = new MultiAction();
                            if (!dictionary1.ContainsKey(key: "action"))
                                throw new ArgumentException(message: "action is missing from touch event", paramName: "action");
                            var dictionary2 = new Dictionary<string, object>();
                            if (dictionary1.ContainsKey(key: "options"))
                                dictionary2 = dictionary1[key: "options"] is JObject jobject ? jobject.ToObject<Dictionary<string, object>>() : null;
                            var str1 = dictionary1[key: "action"].ToString();
                            RectangleI boundingRectangle;
                            if (!(str1 == "press")) {
                                if (!(str1 == "moveTo")) {
                                    if (!(str1 == "release")) {
                                        if (!(str1 == "wait"))
                                            throw new NotImplementedException(message: "action '" + dictionary1[key: "action"] + "' requested is not implemented.");
                                        multiAction1.Action = ActionType.Wait;
                                        multiAction1.Duration = dictionary2 != null && dictionary2.ContainsKey(key: "ms") ? Convert.ToUInt32(value: dictionary2[key: "ms"]) : throw new ArgumentException(message: "wait action missing 'ms' duration");
                                    } else {
                                        multiAction1.Action = ActionType.Release;
                                        multiAction1.Point = this.lastPoints[key: key];
                                        if (dictionary2 != null && dictionary2.ContainsKey(key: "x") && dictionary2.ContainsKey(key: "y")) {
                                            int xScreen;
                                            int yScreen;
                                            ConvertClientToScreen(xClient: Convert.ToInt32(value: dictionary2[key: "x"]), yClient: Convert.ToInt32(value: dictionary2[key: "y"]), xScreen: out xScreen, yScreen: out yScreen);
                                            multiAction1.Point = new PointI(x: xScreen, y: yScreen);
                                            this.lastPoints[key: key] = multiAction1.Point;
                                        }
                                    }
                                } else {
                                    multiAction1.Action = ActionType.Move;
                                    int xClient;
                                    int yClient;
                                    if (dictionary2 != null && dictionary2.ContainsKey(key: "x") && dictionary2.ContainsKey(key: "y")) {
                                        xClient = Convert.ToInt32(value: dictionary2[key: "x"]);
                                        yClient = Convert.ToInt32(value: dictionary2[key: "y"]);
                                    } else {
                                        var uiObject = dictionary2 != null && dictionary2.ContainsKey(key: "element") ? this.sessionKnownElements.Get(elementId: Convert.ToString(value: dictionary2[key: "element"])) : throw new ArgumentException(message: "moveTo action missing coordinates");
                                        var rectangleTopLeft = uiObject.GetBoundingRectangleTopLeft(appRootWindowHandle: this.applicationObject.NativeWindowHandle);
                                        var x = rectangleTopLeft.X;
                                        boundingRectangle = uiObject.BoundingRectangle;
                                        var num2 = boundingRectangle.Width / 2;
                                        xClient = x + num2;
                                        var y = rectangleTopLeft.Y;
                                        boundingRectangle = uiObject.BoundingRectangle;
                                        var num3 = boundingRectangle.Height / 2;
                                        yClient = y + num3;
                                    }

                                    var str2 = dictionary2.ContainsKey(key: "origin") ? Convert.ToString(value: dictionary2[key: "origin"]) : "pointer";
                                    if (str2 == "pointer") {
                                        var num2 = xClient;
                                        var lastPoint = this.lastPoints[key: key];
                                        var x1 = lastPoint.X;
                                        var x2 = num2 + x1;
                                        var num3 = yClient;
                                        lastPoint = this.lastPoints[key: key];
                                        var y1 = lastPoint.Y;
                                        var y2 = num3 + y1;
                                        multiAction1.Point = new PointI(x: x2, y: y2);
                                    } else if (str2 == "viewport") {
                                        int xScreen;
                                        int yScreen;
                                        ConvertClientToScreen(xClient: xClient, yClient: yClient, xScreen: out xScreen, yScreen: out yScreen);
                                        multiAction1.Point = new PointI(x: xScreen, y: yScreen);
                                    } else {
                                        responseStatus = ResponseStatus.InvalidArgument;
                                        throw new ArgumentException(message: "moveTo action contains invalid origin. Expected 'pointer' or 'viewport'(default)");
                                    }

                                    this.lastPoints[key: key] = multiAction1.Point;
                                }
                            } else {
                                multiAction1.Action = ActionType.Press;
                                if (dictionary2 != null && dictionary2.ContainsKey(key: "x") && dictionary2.ContainsKey(key: "y")) {
                                    int xScreen;
                                    int yScreen;
                                    ConvertClientToScreen(xClient: Convert.ToInt32(value: dictionary2[key: "x"]), yClient: Convert.ToInt32(value: dictionary2[key: "y"]), xScreen: out xScreen, yScreen: out yScreen);
                                    multiAction1.Point = new PointI(x: xScreen, y: yScreen);
                                } else {
                                    var uiObject = dictionary2 != null && dictionary2.ContainsKey(key: "element") ? this.sessionKnownElements.Get(elementId: Convert.ToString(value: dictionary2[key: "element"])) : throw new ArgumentException(message: "press action missing coordinates");
                                    var multiAction2 = multiAction1;
                                    boundingRectangle = uiObject.BoundingRectangle;
                                    var location = boundingRectangle.Location;
                                    multiAction2.Point = location;
                                }

                                this.lastPoints[key: key] = multiAction1.Point;
                            }

                            touchInjectionData.Actions[num1++] = multiAction1;
                        }

                        injectionData[key++] = touchInjectionData;
                    }

                    MultiPointGesture.InjectMultiPointGesture(injectionData: injectionData);
                    responseStatus = ResponseStatus.Success;
                }
            } catch (Exception ex) {
            }

            return (int) responseStatus;
        }

        public int SendActions(string ActionsJSONString) {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                this.sessionActionsHandler.PerformActions(ActionsJSONString: ActionsJSONString);
                responseStatus = ResponseStatus.Success;
            } catch (NoSuchWindowException ex) {
                responseStatus = ResponseStatus.NoSuchWindow;
                this.lastErrorMessage = ex.Message;
            } catch (NoSuchElementException ex) {
                responseStatus = ResponseStatus.NoSuchElement;
                this.lastErrorMessage = ex.Message;
            } catch (StaleElementException ex) {
                responseStatus = ResponseStatus.StaleElementReference;
                this.lastErrorMessage = ex.Message;
            } catch (NotImplementedException ex) {
                responseStatus = ResponseStatus.UnsupportedOperation;
                this.lastErrorMessage = ex.Message;
            } catch (Exception ex) when (ex is ArgumentException || ex is JsonReaderException) {
                responseStatus = ResponseStatus.InvalidArgument;
                this.lastErrorMessage = ex.Message;
            } catch (Exception ex) {
                this.lastErrorMessage = ex.Message;
            }

            return (int) responseStatus;
        }

        public int ReleaseActions() {
            var responseStatus = ResponseStatus.UnknownError;
            try {
                this.sessionActionsHandler.ReleaseActions();
                responseStatus = ResponseStatus.Success;
            } catch (ArgumentException ex) {
                responseStatus = ResponseStatus.InvalidArgument;
                this.lastErrorMessage = ex.Message;
            } catch (NoSuchWindowException ex) {
                responseStatus = ResponseStatus.NoSuchWindow;
                this.lastErrorMessage = ex.Message;
            } catch (Exception ex) {
                this.lastErrorMessage = ex.Message;
            }

            return (int) responseStatus;
        }

        public string GetLocation() {
            var str = string.Empty;
            var coordinateWatcher = new GeoCoordinateWatcher();
            coordinateWatcher.StatusChanged += GeoLocationWatcherStatusChanged;
            this.GeoLocationWaiter.Reset();
            coordinateWatcher.Start();
            this.GeoLocationWaiter.WaitOne(timeout: this.defaultLocationWaitTime);
            coordinateWatcher.StatusChanged -= GeoLocationWatcherStatusChanged;
            var location = coordinateWatcher.Position.Location;
            if (!location.IsUnknown)
                str = JsonConvert.SerializeObject(value: new Dictionary<string, double> {
                    {
                        "altitude",
                        location.Altitude
                    }, {
                        "latitude",
                        location.Latitude
                    }, {
                        "longitude",
                        location.Longitude
                    }
                });
            coordinateWatcher.Stop();
            coordinateWatcher.Dispose();
            return str;
        }

        public void ConvertClientToScreen(int xClient, int yClient, out int xScreen, out int yScreen) {
            PositionAdapter.ConvertClientToScreen(hwnd: this.applicationObject.NativeWindowHandle, xClient: xClient, yClient: yClient, xScreen: out xScreen, yScreen: out yScreen);
        }

        public void ConvertScreenToClient(int xScreen, int yScreen, out int xClient, out int yClient) {
            PositionAdapter.ConvertScreenToClient(hwnd: this.applicationObject.NativeWindowHandle, xScreen: xScreen, yScreen: yScreen, xClient: out xClient, yClient: out yClient);
        }

        public int GetApplicationType() {
            return (int) this.appType;
        }

        public bool IsExperimentalW3C() {
            return this.sessionCapabilities.IsExperimental();
        }

        ~RequestHandler() {
            CloseApplication();
        }

        ResponseStatus LaunchApplication(
            string appId,
            string appArguments,
            string appWorkingDirectory,
            int initialWaitTime = 0) {
            var responseStatus = ResponseStatus.UnknownError;
            var stopwatch = Stopwatch.StartNew();
            if (string.IsNullOrEmpty(value: appId))
                throw new ArgumentException(message: "appId shouldn't be empty");
            if (appId == "Root") {
                this.applicationObject = UIObject.Root;
                this.processId = this.applicationObject.ProcessId;
                this.appType = TargetAppType.Root;
            } else if (appId.Contains(value: "!")) {
                this.appType = TargetAppType.Modern;
                using (var appLaunchWaiter = new AppLaunchWaiter(topLevelWindowCondition: UICondition.CreateFromClassName(className: "ApplicationFrameWindow"))) {
                    uint processId;
                    ApplicationActivation.ActivateApplication(applicationId: appId, arguments: appArguments, processId: out processId);
                    this.processId = Convert.ToInt32(value: processId);
                    if (this.processId > 0) {
                        UICollection.Timeout = TimeSpan.Zero;
                        if (appLaunchWaiter.TryWait(timeout: this.defaultStartupWaitTime) && appLaunchWaiter.Source != null) {
                            var source = appLaunchWaiter.Source;
                            UICollection.Timeout = TimeSpan.FromSeconds(value: 1.0);
                            UICollection.RetryCount = 2;
                            if (source.Children.Contains(uiProperty: UIProperty.Get(name: "ProcessId"), value: this.processId))
                                this.applicationObject = source;
                        }

                        if (this.applicationObject == null) {
                            Thread.Sleep(millisecondsTimeout: initialWaitTime * 1000);
                            var num = 1;
                            while (true) {
                                this.applicationObject = MultipleWindows.GetTopLevelWindowsModernApp(processId: this.processId).FirstOrDefault();
                                if (!(this.applicationObject != null)) {
                                    if (this.defaultStartupWaitTime > stopwatch.Elapsed) {
                                        var timeSpan = this.defaultSearchInterval;
                                        var totalMilliseconds1 = timeSpan.TotalMilliseconds;
                                        timeSpan = this.defaultStartupWaitTime - stopwatch.Elapsed;
                                        var totalMilliseconds2 = timeSpan.TotalMilliseconds;
                                        Thread.Sleep(millisecondsTimeout: Math.Max(val1: 0, val2: (int) Math.Min(val1: totalMilliseconds1, val2: totalMilliseconds2)));
                                        ++num;
                                    } else {
                                        break;
                                    }
                                } else {
                                    break;
                                }
                            }
                        }
                    } else {
                        this.lastErrorMessage = "Failed to launch application " + appId + " " + appArguments;
                    }
                }
            } else {
                this.appType = TargetAppType.Win32;
                using (var windowOpenedWaiter = new WindowOpenedWaiter()) {
                    Process process1;
                    if (string.IsNullOrEmpty(value: appWorkingDirectory))
                        process1 = Process.Start(fileName: appId, arguments: appArguments);
                    else
                        process1 = Process.Start(startInfo: new ProcessStartInfo(fileName: appId, arguments: appArguments) {
                            WorkingDirectory = appWorkingDirectory,
                            UseShellExecute = false
                        });
                    if (process1 != null) {
                        Thread.Sleep(millisecondsTimeout: initialWaitTime * 1000);
                        this.processId = process1.Id;
                        if (windowOpenedWaiter.TryWait(timeout: this.defaultStartupWaitTime)) {
                            var source = windowOpenedWaiter.Source;
                            if (source != null) {
                                if (source.ProcessId == process1.Id) {
                                    this.applicationObject = source;
                                } else {
                                    var processById = Process.GetProcessById(processId: source.ProcessId);
                                    var withoutExtension = Path.GetFileNameWithoutExtension(path: appId);
                                    if (string.Equals(a: processById.ProcessName, b: withoutExtension, comparisonType: StringComparison.OrdinalIgnoreCase)) {
                                        this.processId = processById.Id;
                                        this.applicationObject = source;
                                    }
                                }
                            }
                        }

                        if (this.applicationObject == null) {
                            UICollection.Timeout = TimeSpan.Zero;
                            this.applicationObject = MultipleWindows.GetTopLevelWindowsClassicApp(processId: this.processId).FirstOrDefault();
                            if (this.applicationObject == null)
                                if (!string.IsNullOrEmpty(value: appId)) {
                                    var withoutExtension = Path.GetFileNameWithoutExtension(path: appId);
                                    foreach (var process2 in Process.GetProcesses())
                                        if (string.Equals(a: process2.ProcessName, b: withoutExtension, comparisonType: StringComparison.OrdinalIgnoreCase)) {
                                            this.processId = process2.Id;
                                            break;
                                        }

                                    var timeSpan = this.defaultSearchInterval;
                                    var totalMilliseconds1 = timeSpan.TotalMilliseconds;
                                    timeSpan = this.defaultStartupWaitTime - stopwatch.Elapsed;
                                    var totalMilliseconds2 = timeSpan.TotalMilliseconds;
                                    UICollection.Timeout = TimeSpan.FromMilliseconds(value: Math.Max(val1: totalMilliseconds1, val2: totalMilliseconds2));
                                    timeSpan = UICollection.Timeout;
                                    var totalMilliseconds3 = timeSpan.TotalMilliseconds;
                                    timeSpan = this.defaultSearchInterval;
                                    var totalMilliseconds4 = timeSpan.TotalMilliseconds;
                                    UICollection.RetryCount = (int) (totalMilliseconds3 / totalMilliseconds4);
                                    this.applicationObject = MultipleWindows.GetTopLevelWindowsClassicApp(processId: this.processId).FirstOrDefault();
                                }
                        }
                    } else {
                        this.lastErrorMessage = "Failed to launch application " + appId + " " + appArguments;
                    }
                }
            }

            if (this.applicationObject != null && this.processId > 0) {
                while (this.applicationObject != UIObject.Root && this.applicationObject.Parent != UIObject.Root)
                    this.applicationObject = this.applicationObject.Parent;
                this.applicationId = appId;
                this.sessionActionsHandler.SetSessionTopLevelWindowHandle(sessionTopLevelWindowHandle: this.applicationObject.NativeWindowHandle);
                responseStatus = ResponseStatus.Success;
            } else {
                this.lastErrorMessage = string.Format(format: "Failed to locate opened application window with appId: {0}, and processId: {1}", arg0: appId, arg1: this.processId);
            }

            stopwatch.Stop();
            return responseStatus;
        }

        UIObject GetObjectFromTopLevelWindowHandle(
            string windowHandle,
            out int windowProcessId) {
            var uiObject1 = UIObject.FromHandle(hwnd: (IntPtr) Convert.ToInt32(value: windowHandle, fromBase: 16));
            if (uiObject1 == null)
                throw new ArgumentException(message: "Invalid window handle");
            if (uiObject1.ClassName == "ApplicationFrameWindow") {
                UICollection.Timeout = TimeSpan.Zero;
                var uiObject2 = uiObject1.Children.Find(condition: UICondition.CreateFromClassName(className: "Windows.UI.Core.CoreWindow"));
                windowProcessId = uiObject2 != (UIObject) null ? uiObject2.ProcessId : throw new ArgumentException(message: "App identified from " + windowHandle + " window handle is malformed");
            } else {
                if (uiObject1.Parent != UIObject.Root)
                    throw new ArgumentException(message: windowHandle + " is not a top level window handle");
                windowProcessId = uiObject1.ProcessId;
            }

            return uiObject1;
        }

        void ResetUICollectionTimeout() {
            UICollection.Timeout = this.sessionImplicitTimeout;
            UICollection.RetryCount = this.sessionRetryCount;
        }

        UICondition GetMatchCondition(string searchTarget, string locatorStrategy) {
            UICondition uiCondition = null;
            try {
                if (locatorStrategy == "id")
                    uiCondition = UICondition.Create(property: UIProperty.Get(name: "RuntimeId"), value: Utilities.GetRuntimeIdParts(RuntimeIdString: searchTarget));
                else if (locatorStrategy == "accessibility id")
                    uiCondition = UICondition.CreateFromId(automationId: searchTarget);
                else if (locatorStrategy == "name")
                    uiCondition = UICondition.CreateFromName(name: searchTarget);
                else if (locatorStrategy == "class name")
                    uiCondition = UICondition.CreateFromClassName(className: searchTarget);
            } catch {
            }

            return uiCondition;
        }

        List<UIObject> FindMultipleElementsXPath(
            string searchTarget,
            UIObject startNode) {
            var foundList = new List<UIObject>();
            if (this.sessionCapabilities.IsExperimental()) {
                var startIndex = searchTarget.IndexOf(value: '/', startIndex: 1);
                searchTarget = searchTarget.Substring(startIndex: startIndex);
                var bIsSimpleAttributeExpression = true;
                FindElementByAbsoluteXPath.FindChildrenWithMatchingAttributes(rootObject: startNode, xpath: searchTarget, foundList: foundList, bIsSimpleAttributeExpression: ref bIsSimpleAttributeExpression);
            } else {
                var objectDictionary = new Dictionary<string, UIObject>();
                var xmlNodeList = new UIXmlDom(applicationRoot: this.applicationObject).CreateUIXmlDocumentWithDictionary(startNode: startNode, objectDictionary: objectDictionary).SelectNodes(xpath: searchTarget);
                if (xmlNodeList.Count > 0)
                    foreach (XmlNode node in xmlNodeList)
                        foundList.Add(item: objectDictionary[key: Utilities.GetElementIdFromXmlNode(node: node)]);
            }

            return foundList;
        }

        UIObject FindSingleElementXPath(
            string searchTarget,
            UIObject startNode,
            out string targetId) {
            UIObject uiObject = null;
            targetId = string.Empty;
            var flag1 = false;
            var flag2 = this.sessionCapabilities.IsExperimental();
            if (((!searchTarget.StartsWith(value: "/Pane") ? 0 : searchTarget.Contains(value: "[@ClassName=\"#32769\"]") ? 1 : 0) | (flag2 ? 1 : 0)) != 0) {
                var startIndex1 = 1;
                int startIndex2;
                if (searchTarget.StartsWith(value: "/Pane") && searchTarget.Contains(value: "[@ClassName=\"#32769\"]")) {
                    startIndex2 = searchTarget.IndexOf(value: "/", startIndex: "/Pane".Length);
                    startIndex1 = "/Pane".Length;
                } else {
                    startIndex2 = searchTarget.IndexOf(value: "/", startIndex: startIndex1);
                }

                if (startIndex2 > startIndex1) {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    var xpath = searchTarget.Substring(startIndex: startIndex2);
                    flag1 = true;
                    ref var local1 = ref targetId;
                    ref var local2 = ref flag1;
                    var rootUIObject = startNode;
                    var num = flag2 ? 1 : 0;
                    uiObject = FindElementByAbsoluteXPath.FindTarget(xpath: xpath, runtimeid: out local1, bIsSimpleAttributeExpression: ref local2, rootUIObject: rootUIObject, isPreview: num != 0);
                    stopwatch.Stop();
                    if (flag1)
                        Telemetry.LogRequest(codePath: "FindSingleElementXPath:UIRecorderXPath", locatorStrategy: "UIRecorderXPath", timeElapsed: stopwatch.Elapsed.TotalMilliseconds.ToString(), guid: this.TelemetryGuid, result: targetId);
                }
            }

            if (uiObject == null && !flag1) {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var objectDictionary = new Dictionary<string, UIObject>();
                var node = new UIXmlDom(applicationRoot: this.applicationObject).CreateUIXmlDocumentWithDictionary(startNode: startNode, objectDictionary: objectDictionary).SelectSingleNode(xpath: searchTarget);
                if (node != null) {
                    targetId = Utilities.GetElementIdFromXmlNode(node: node);
                    uiObject = objectDictionary[key: targetId];
                }

                stopwatch.Stop();
                Telemetry.LogRequest(codePath: "FindSingleElementXPath:LegacyXPath", locatorStrategy: "LegacyXPath", timeElapsed: stopwatch.Elapsed.TotalMilliseconds.ToString(), guid: this.TelemetryGuid, result: targetId);
            }

            return uiObject;
        }

        UIObject FindSingleElement(
            string searchTarget,
            string locatorStrategy,
            string startNodeId,
            out string targetId) {
            UIObject element1 = null;
            targetId = string.Empty;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var uiObject = !string.IsNullOrEmpty(value: startNodeId) ? this.sessionKnownElements.Get(elementId: startNodeId) : this.applicationObject;
            if (uiObject != null) {
                if (locatorStrategy == "xpath") {
                    element1 = FindSingleElementXPath(searchTarget: searchTarget, startNode: uiObject, targetId: out targetId);
                } else if (locatorStrategy == "tag name") {
                    element1 = FindSingleElementXPath(searchTarget: "//" + searchTarget, startNode: uiObject, targetId: out targetId);
                } else {
                    var matchCondition = GetMatchCondition(searchTarget: searchTarget, locatorStrategy: locatorStrategy);
                    if (matchCondition != null) {
                        if (uiObject.Matches(condition: matchCondition)) {
                            element1 = uiObject;
                        } else {
                            UIObject element2 = null;
                            ResetUICollectionTimeout();
                            if (new UIBreadthFirstDescendants<UIObject>(root: uiObject, factory: UIObject.Factory).TryFind(condition: matchCondition, element: out element2))
                                element1 = element2;
                        }

                        if (element1 != null)
                            targetId = Utilities.GetElementIdFromElement(element: element1);
                    }
                }
            }

            stopwatch.Stop();
            Telemetry.LogRequest(codePath: nameof(FindSingleElement), locatorStrategy: locatorStrategy, timeElapsed: stopwatch.Elapsed.TotalMilliseconds.ToString(), guid: this.TelemetryGuid, result: targetId);
            return element1;
        }

        void GeoLocationWatcherStatusChanged(object sender, GeoPositionStatusChangedEventArgs e) {
            switch (e.Status) {
                case GeoPositionStatus.Ready:
                case GeoPositionStatus.Disabled:
                    this.GeoLocationWaiter.Set();
                    break;
            }
        }

        enum TargetAppType {
            NotSet = -1, // 0xFFFFFFFF
            Modern = 0,
            Win32 = 1,
            Root = 2,
            HandleToWindow = 3
        }
    }
}