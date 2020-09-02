// Decompiled with JetBrains decompiler
// Type: MitaBroker.RequestHandler
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MitaBroker.WebDriver.Actions;
using MS.Internal.Mita.Foundation;
using MS.Internal.Mita.Foundation.Collections;
using MS.Internal.Mita.Foundation.Controls;
using MS.Internal.Mita.Foundation.Patterns;
using MS.Internal.Mita.Foundation.Waiters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace MitaBroker
{
  [ClassInterface(ClassInterfaceType.None)]
  [ComSourceInterfaces(typeof (IRequestHandler))]
  [Guid("6015EC6C-0919-4A39-85F6-F9962A09F5C7")]
  [ComVisible(true)]
  public sealed class RequestHandler : IRequestHandler
  {
    private int processId = -1;
    private string applicationId = string.Empty;
    private UIObject applicationObject;
    private RequestHandler.TargetAppType appType = RequestHandler.TargetAppType.NotSet;
    private Capabilities sessionCapabilities;
    private ActionsHandler sessionActionsHandler;
    private KnownElements sessionKnownElements = new KnownElements();
    private Dictionary<int, PointI> lastPoints = new Dictionary<int, PointI>();
    private string lastErrorMessage = string.Empty;
    private readonly TimeSpan defaultStartupWaitTime = TimeSpan.FromSeconds(5.0);
    private readonly TimeSpan defaultClosingWaitTime = TimeSpan.FromSeconds(5.0);
    private readonly TimeSpan defaultLocationWaitTime = TimeSpan.FromSeconds(10.0);
    private readonly TimeSpan defaultSearchInterval = TimeSpan.FromMilliseconds(500.0);
    private TimeSpan sessionImplicitTimeout = TimeSpan.Zero;
    private int sessionRetryCount = 5;
    private const int defaultWindowMatchingRetryCount = 2;
    private const int UIA_E_ELEMENTNOTAVAILABLE = -2147220991;
    private const string ApplicationFrameWindowClassName = "ApplicationFrameWindow";
    private const string AppIdCreatedFromWindowHandlePrefix = "From window handle: ";
    private const string AppIdRoot = "Root";
    private string TelemetryGuid;
    private EventWaitHandle GeoLocationWaiter = (EventWaitHandle) new ManualResetEvent(false);

    public RequestHandler() => this.sessionActionsHandler = new ActionsHandler(this.sessionKnownElements);

    ~RequestHandler() => this.CloseApplication();

    public int LaunchApplication(string capabilitiesJSONString)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      try
      {
        if (!string.IsNullOrEmpty(capabilitiesJSONString))
          this.sessionCapabilities = new Capabilities(capabilitiesJSONString);
        if (this.sessionCapabilities == null)
          throw new ArgumentException("Missing capabilities on session creation");
        if (this.sessionCapabilities.GetCapabilities("unsupportedRequiredCapabilities") != null)
          throw new Exception("There is an unsupported RequiredCapabilities: " + this.GetCapabilities("unsupportedRequiredCapabilities"));
        if (this.sessionCapabilities.GetCapabilityValue("app", "supportedCapabilities") != null && this.sessionCapabilities.GetCapabilityValue("appTopLevelWindow", "supportedCapabilities") == null)
        {
          string appId = this.sessionCapabilities.GetCapabilityValue("app", "supportedCapabilities").ToString();
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          if (string.IsNullOrEmpty(appId))
            throw new ArgumentException("Capability: app cannot be empty");
          object capabilityValue1 = this.sessionCapabilities.GetCapabilityValue("appArguments", "supportedCapabilities");
          if (capabilityValue1 != null)
            empty1 = capabilityValue1.ToString();
          object capabilityValue2 = this.sessionCapabilities.GetCapabilityValue("appWorkingDir", "supportedCapabilities");
          if (capabilityValue2 != null)
            empty2 = capabilityValue2.ToString();
          object capabilityValue3 = this.sessionCapabilities.GetCapabilityValue("ms:waitForAppLaunch", "supportedCapabilities");
          int initialWaitTime = capabilityValue3 != null ? (int) short.Parse(capabilityValue3.ToString()) : 0;
          responseStatus = this.LaunchApplication(appId, empty1, empty2, initialWaitTime);
        }
        else
        {
          string windowHandle = this.sessionCapabilities.GetCapabilityValue("appTopLevelWindow", "supportedCapabilities") != null && this.sessionCapabilities.GetCapabilityValue("app", "supportedCapabilities") == null ? this.sessionCapabilities.GetCapabilityValue("appTopLevelWindow", "supportedCapabilities").ToString() : throw new ArgumentException("Bad capabilities. Specify either app or appTopLevelWindow to create a session");
          this.applicationObject = !string.IsNullOrEmpty(windowHandle) ? this.GetObjectFromTopLevelWindowHandle(windowHandle, out this.processId) : throw new ArgumentException("Capability: appTopLevelWindow cannot be empty");
          this.applicationId = "From window handle: " + windowHandle;
          this.appType = RequestHandler.TargetAppType.HandleToWindow;
        }
        if (this.applicationObject != (UIObject) null)
        {
          if (this.processId > 0)
          {
            this.applicationObject.SetFocus();
            this.sessionKnownElements.Add(this.applicationObject);
            this.sessionActionsHandler.SetSessionTopLevelWindowHandle(this.applicationObject.NativeWindowHandle);
            responseStatus = ResponseStatus.Success;
          }
        }
      }
      catch (ArgumentException ex)
      {
        responseStatus = ResponseStatus.InvalidArgument;
        this.lastErrorMessage = ex.Message;
      }
      catch (Exception ex) when (ex.HResult == -2147220991)
      {
        responseStatus = ResponseStatus.NoSuchWindow;
        this.lastErrorMessage = "Cannot find active window specified by capabilities: appTopLevelWindow";
      }
      catch (Exception ex)
      {
        this.lastErrorMessage = ex.Message;
      }
      return (int) responseStatus;
    }

    private ResponseStatus LaunchApplication(
      string appId,
      string appArguments,
      string appWorkingDirectory,
      int initialWaitTime = 0)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      Stopwatch stopwatch = Stopwatch.StartNew();
      if (string.IsNullOrEmpty(appId))
        throw new ArgumentException("appId shouldn't be empty");
      if (appId == "Root")
      {
        this.applicationObject = UIObject.Root;
        this.processId = this.applicationObject.ProcessId;
        this.appType = RequestHandler.TargetAppType.Root;
      }
      else if (appId.Contains("!"))
      {
        this.appType = RequestHandler.TargetAppType.Modern;
        using (AppLaunchWaiter appLaunchWaiter = new AppLaunchWaiter(UICondition.CreateFromClassName("ApplicationFrameWindow")))
        {
          uint processId;
          ApplicationActivation.ActivateApplication(appId, appArguments, out processId);
          this.processId = Convert.ToInt32(processId);
          if (this.processId > 0)
          {
            UICollection.Timeout = TimeSpan.Zero;
            if (appLaunchWaiter.TryWait(this.defaultStartupWaitTime) && appLaunchWaiter.Source != (UIObject) null)
            {
              UIObject source = appLaunchWaiter.Source;
              UICollection.Timeout = TimeSpan.FromSeconds(1.0);
              UICollection.RetryCount = 2;
              if (source.Children.Contains(UIProperty.Get("ProcessId"), (object) this.processId))
                this.applicationObject = source;
            }
            if (this.applicationObject == (UIObject) null)
            {
              Thread.Sleep(initialWaitTime * 1000);
              int num = 1;
              while (true)
              {
                this.applicationObject = MultipleWindows.GetTopLevelWindowsModernApp(this.processId).FirstOrDefault<UIObject>();
                if (!(this.applicationObject != (UIObject) null))
                {
                  if (this.defaultStartupWaitTime > stopwatch.Elapsed)
                  {
                    TimeSpan timeSpan = this.defaultSearchInterval;
                    double totalMilliseconds1 = timeSpan.TotalMilliseconds;
                    timeSpan = this.defaultStartupWaitTime - stopwatch.Elapsed;
                    double totalMilliseconds2 = timeSpan.TotalMilliseconds;
                    Thread.Sleep(Math.Max(0, (int) Math.Min(totalMilliseconds1, totalMilliseconds2)));
                    ++num;
                  }
                  else
                    break;
                }
                else
                  break;
              }
            }
          }
          else
            this.lastErrorMessage = "Failed to launch application " + appId + " " + appArguments;
        }
      }
      else
      {
        this.appType = RequestHandler.TargetAppType.Win32;
        using (WindowOpenedWaiter windowOpenedWaiter = new WindowOpenedWaiter())
        {
          Process process1;
          if (string.IsNullOrEmpty(appWorkingDirectory))
            process1 = Process.Start(appId, appArguments);
          else
            process1 = Process.Start(new ProcessStartInfo(appId, appArguments)
            {
              WorkingDirectory = appWorkingDirectory,
              UseShellExecute = false
            });
          if (process1 != null)
          {
            Thread.Sleep(initialWaitTime * 1000);
            this.processId = process1.Id;
            if (windowOpenedWaiter.TryWait(this.defaultStartupWaitTime))
            {
              UIObject source = windowOpenedWaiter.Source;
              if (source != (UIObject) null)
              {
                if (source.ProcessId == process1.Id)
                {
                  this.applicationObject = source;
                }
                else
                {
                  Process processById = Process.GetProcessById(source.ProcessId);
                  string withoutExtension = Path.GetFileNameWithoutExtension(appId);
                  if (string.Equals(processById.ProcessName, withoutExtension, StringComparison.OrdinalIgnoreCase))
                  {
                    this.processId = processById.Id;
                    this.applicationObject = source;
                  }
                }
              }
            }
            if (this.applicationObject == (UIObject) null)
            {
              UICollection.Timeout = TimeSpan.Zero;
              this.applicationObject = MultipleWindows.GetTopLevelWindowsClassicApp(this.processId).FirstOrDefault<UIObject>();
              if (this.applicationObject == (UIObject) null)
              {
                if (!string.IsNullOrEmpty(appId))
                {
                  string withoutExtension = Path.GetFileNameWithoutExtension(appId);
                  foreach (Process process2 in Process.GetProcesses())
                  {
                    if (string.Equals(process2.ProcessName, withoutExtension, StringComparison.OrdinalIgnoreCase))
                    {
                      this.processId = process2.Id;
                      break;
                    }
                  }
                  TimeSpan timeSpan = this.defaultSearchInterval;
                  double totalMilliseconds1 = timeSpan.TotalMilliseconds;
                  timeSpan = this.defaultStartupWaitTime - stopwatch.Elapsed;
                  double totalMilliseconds2 = timeSpan.TotalMilliseconds;
                  UICollection.Timeout = TimeSpan.FromMilliseconds(Math.Max(totalMilliseconds1, totalMilliseconds2));
                  timeSpan = UICollection.Timeout;
                  double totalMilliseconds3 = timeSpan.TotalMilliseconds;
                  timeSpan = this.defaultSearchInterval;
                  double totalMilliseconds4 = timeSpan.TotalMilliseconds;
                  UICollection.RetryCount = (int) (totalMilliseconds3 / totalMilliseconds4);
                  this.applicationObject = MultipleWindows.GetTopLevelWindowsClassicApp(this.processId).FirstOrDefault<UIObject>();
                }
              }
            }
          }
          else
            this.lastErrorMessage = "Failed to launch application " + appId + " " + appArguments;
        }
      }
      if (this.applicationObject != (UIObject) null && this.processId > 0)
      {
        while (this.applicationObject != UIObject.Root && this.applicationObject.Parent != UIObject.Root)
          this.applicationObject = this.applicationObject.Parent;
        this.applicationId = appId;
        this.sessionActionsHandler.SetSessionTopLevelWindowHandle(this.applicationObject.NativeWindowHandle);
        responseStatus = ResponseStatus.Success;
      }
      else
        this.lastErrorMessage = string.Format("Failed to locate opened application window with appId: {0}, and processId: {1}", (object) appId, (object) this.processId);
      stopwatch.Stop();
      return responseStatus;
    }

    private UIObject GetObjectFromTopLevelWindowHandle(
      string windowHandle,
      out int windowProcessId)
    {
      UIObject uiObject1 = UIObject.FromHandle((IntPtr) Convert.ToInt32(windowHandle, 16));
      if (uiObject1 == (UIObject) null)
        throw new ArgumentException("Invalid window handle");
      if (uiObject1.ClassName == "ApplicationFrameWindow")
      {
        UICollection.Timeout = TimeSpan.Zero;
        UIObject uiObject2 = uiObject1.Children.Find(UICondition.CreateFromClassName("Windows.UI.Core.CoreWindow"));
        windowProcessId = uiObject2 != (UIObject) null ? uiObject2.ProcessId : throw new ArgumentException("App identified from " + windowHandle + " window handle is malformed");
      }
      else
      {
        if (uiObject1.Parent != UIObject.Root)
          throw new ArgumentException(windowHandle + " is not a top level window handle");
        windowProcessId = uiObject1.ProcessId;
      }
      return uiObject1;
    }

    public void CloseApplication()
    {
      try
      {
        if (!(this.applicationObject != (UIObject) null) || !((IEnumerable<AutomationPattern>) this.applicationObject.GetSupportedPatterns()).ToList<AutomationPattern>().Contains(WindowPatternIdentifiers.Pattern))
          return;
        bool flag = true;
        Window window = new Window(this.applicationObject);
        using (UIEventWaiter windowClosedWaiter = window.GetWindowClosedWaiter())
        {
          window.Close();
          flag = !windowClosedWaiter.TryWait(this.defaultClosingWaitTime);
        }
        if (flag)
        {
          try
          {
            flag = window.ProcessId > 0 && window.LocalizedControlType != null;
          }
          catch
          {
            flag = false;
          }
        }
        if (flag)
          return;
        this.applicationObject = (UIObject) null;
        this.sessionKnownElements.Clear();
        this.sessionActionsHandler.SetSessionTopLevelWindowHandle(IntPtr.Zero);
      }
      catch
      {
      }
    }

    public void QuitApplication(bool forceQuitApp)
    {
      try
      {
        if (this.applicationId.StartsWith("From window handle: ") || !(this.applicationId != "Root"))
          return;
        this.CloseApplication();
        if (((!(this.applicationObject != (UIObject) null) ? 0 : (this.processId > 0 ? 1 : 0)) & (forceQuitApp ? 1 : 0)) == 0)
          return;
        Process.GetProcessById(this.processId).Kill();
      }
      catch
      {
      }
    }

    public string GetApplicationId() => this.applicationId;

    public string GetCapabilities(string capabilitiesSet)
    {
      string str = string.Empty;
      if (this.sessionCapabilities != null)
        str = JsonConvert.SerializeObject((object) this.sessionCapabilities.GetCapabilities(capabilitiesSet));
      return str;
    }

    public void SetImplicitTimeout(int timeoutMs)
    {
      if (timeoutMs < 0)
        return;
      this.sessionImplicitTimeout = TimeSpan.FromMilliseconds(Convert.ToDouble(timeoutMs));
      this.sessionRetryCount = Math.Max(timeoutMs / (int) this.defaultSearchInterval.TotalMilliseconds, 1);
    }

    private void ResetUICollectionTimeout()
    {
      UICollection.Timeout = this.sessionImplicitTimeout;
      UICollection.RetryCount = this.sessionRetryCount;
    }

    public string GetLastErrorMessage() => this.lastErrorMessage;

    public string GetActiveElement()
    {
      string str = string.Empty;
      try
      {
        if (this.applicationObject != (UIObject) null)
        {
          UIObject focused = UIObject.Focused;
          UICollection.Timeout = TimeSpan.Zero;
          if (focused != (UIObject) null)
          {
            if (!(this.applicationObject == UIObject.Root))
            {
              if (!this.applicationObject.Descendants.Contains(focused.AutomationId))
                goto label_7;
            }
            str = this.sessionKnownElements.Add(focused);
          }
        }
      }
      catch
      {
      }
label_7:
      return str;
    }

    public string GetApplicationObject()
    {
      string str = string.Empty;
      if (this.applicationObject != (UIObject) null)
        str = this.applicationObject.RuntimeId;
      return str;
    }

    private UICondition GetMatchCondition(string searchTarget, string locatorStrategy)
    {
      UICondition uiCondition = (UICondition) null;
      try
      {
        if (locatorStrategy == "id")
          uiCondition = UICondition.Create(UIProperty.Get("RuntimeId"), (object) Utilities.GetRuntimeIdParts(searchTarget));
        else if (locatorStrategy == "accessibility id")
          uiCondition = UICondition.CreateFromId(searchTarget);
        else if (locatorStrategy == "name")
          uiCondition = UICondition.CreateFromName(searchTarget);
        else if (locatorStrategy == "class name")
          uiCondition = UICondition.CreateFromClassName(searchTarget);
      }
      catch
      {
      }
      return uiCondition;
    }

    public string SearchElement(string searchTarget, string locatorStrategy, string startNodeId)
    {
      UIObject element = (UIObject) null;
      string targetId = string.Empty;
      this.TelemetryGuid = Guid.NewGuid().ToString();
      try
      {
        if (locatorStrategy == "id")
        {
          element = this.sessionKnownElements.Get(searchTarget);
          targetId = this.sessionKnownElements.Add(element);
        }
        if (element == (UIObject) null)
          this.sessionKnownElements.Add(this.FindSingleElement(searchTarget, locatorStrategy, startNodeId, out targetId), targetId);
        else
          Telemetry.LogRequest("SearchElement:FoundTargetElementFromCache", locatorStrategy, "", this.TelemetryGuid, targetId);
      }
      catch (XPathException ex)
      {
        targetId = "Invalid XPath expression: " + searchTarget;
        Telemetry.LogRequest("SearchElement: XPathExpressionException", locatorStrategy, "", this.TelemetryGuid, targetId);
      }
      catch
      {
      }
      return targetId;
    }

    private List<UIObject> FindMultipleElementsXPath(
      string searchTarget,
      UIObject startNode)
    {
      List<UIObject> foundList = new List<UIObject>();
      if (this.sessionCapabilities.IsExperimental())
      {
        int startIndex = searchTarget.IndexOf('/', 1);
        searchTarget = searchTarget.Substring(startIndex);
        bool bIsSimpleAttributeExpression = true;
        FindElementByAbsoluteXPath.FindChildrenWithMatchingAttributes(startNode, searchTarget, foundList, ref bIsSimpleAttributeExpression);
      }
      else
      {
        Dictionary<string, UIObject> objectDictionary = new Dictionary<string, UIObject>();
        XmlNodeList xmlNodeList = new UIXmlDom(this.applicationObject).CreateUIXmlDocumentWithDictionary(startNode, objectDictionary).SelectNodes(searchTarget);
        if (xmlNodeList.Count > 0)
        {
          foreach (XmlNode node in xmlNodeList)
            foundList.Add(objectDictionary[Utilities.GetElementIdFromXmlNode(node)]);
        }
      }
      return foundList;
    }

    public string GetSource()
    {
      string empty = string.Empty;
      using (StringWriter stringWriter = new StringWriter())
      {
        using (XmlWriter w = XmlWriter.Create((TextWriter) stringWriter))
        {
          new UIXmlDom(this.applicationObject).CreateUIXmlDocument(this.applicationObject).WriteTo(w);
          w.Flush();
          return stringWriter.GetStringBuilder().ToString();
        }
      }
    }

    private UIObject FindSingleElementXPath(
      string searchTarget,
      UIObject startNode,
      out string targetId)
    {
      UIObject uiObject = (UIObject) null;
      targetId = string.Empty;
      bool flag1 = false;
      bool flag2 = this.sessionCapabilities.IsExperimental();
      if (((!searchTarget.StartsWith("/Pane") ? 0 : (searchTarget.Contains("[@ClassName=\"#32769\"]") ? 1 : 0)) | (flag2 ? 1 : 0)) != 0)
      {
        int startIndex1 = 1;
        int startIndex2;
        if (searchTarget.StartsWith("/Pane") && searchTarget.Contains("[@ClassName=\"#32769\"]"))
        {
          startIndex2 = searchTarget.IndexOf("/", "/Pane".Length);
          startIndex1 = "/Pane".Length;
        }
        else
          startIndex2 = searchTarget.IndexOf("/", startIndex1);
        if (startIndex2 > startIndex1)
        {
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();
          string xpath = searchTarget.Substring(startIndex2);
          flag1 = true;
          ref string local1 = ref targetId;
          ref bool local2 = ref flag1;
          UIObject rootUIObject = startNode;
          int num = flag2 ? 1 : 0;
          uiObject = FindElementByAbsoluteXPath.FindTarget(xpath, out local1, ref local2, rootUIObject, num != 0);
          stopwatch.Stop();
          if (flag1)
            Telemetry.LogRequest("FindSingleElementXPath:UIRecorderXPath", "UIRecorderXPath", stopwatch.Elapsed.TotalMilliseconds.ToString(), this.TelemetryGuid, targetId);
        }
      }
      if (uiObject == (UIObject) null && !flag1)
      {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Dictionary<string, UIObject> objectDictionary = new Dictionary<string, UIObject>();
        XmlNode node = new UIXmlDom(this.applicationObject).CreateUIXmlDocumentWithDictionary(startNode, objectDictionary).SelectSingleNode(searchTarget);
        if (node != null)
        {
          targetId = Utilities.GetElementIdFromXmlNode(node);
          uiObject = objectDictionary[targetId];
        }
        stopwatch.Stop();
        Telemetry.LogRequest("FindSingleElementXPath:LegacyXPath", "LegacyXPath", stopwatch.Elapsed.TotalMilliseconds.ToString(), this.TelemetryGuid, targetId);
      }
      return uiObject;
    }

    private UIObject FindSingleElement(
      string searchTarget,
      string locatorStrategy,
      string startNodeId,
      out string targetId)
    {
      UIObject element1 = (UIObject) null;
      targetId = string.Empty;
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      UIObject uiObject = !string.IsNullOrEmpty(startNodeId) ? this.sessionKnownElements.Get(startNodeId) : this.applicationObject;
      if (uiObject != (UIObject) null)
      {
        if (locatorStrategy == "xpath")
          element1 = this.FindSingleElementXPath(searchTarget, uiObject, out targetId);
        else if (locatorStrategy == "tag name")
        {
          element1 = this.FindSingleElementXPath("//" + searchTarget, uiObject, out targetId);
        }
        else
        {
          UICondition matchCondition = this.GetMatchCondition(searchTarget, locatorStrategy);
          if (matchCondition != null)
          {
            if (uiObject.Matches(matchCondition))
            {
              element1 = uiObject;
            }
            else
            {
              UIObject element2 = (UIObject) null;
              this.ResetUICollectionTimeout();
              if (new UIBreadthFirstDescendants<UIObject>(uiObject, UIObject.Factory).TryFind(matchCondition, out element2))
                element1 = element2;
            }
            if (element1 != (UIObject) null)
              targetId = Utilities.GetElementIdFromElement(element1);
          }
        }
      }
      stopwatch.Stop();
      Telemetry.LogRequest(nameof (FindSingleElement), locatorStrategy, stopwatch.Elapsed.TotalMilliseconds.ToString(), this.TelemetryGuid, targetId);
      return element1;
    }

    public string SearchMultipleElements(
      string searchTarget,
      string locatorStrategy,
      string startNodeId)
    {
      string str = "[]";
      UIObject uiObject = !string.IsNullOrEmpty(startNodeId) ? this.sessionKnownElements.Get(startNodeId.ToString()) : this.applicationObject;
      if (uiObject != (UIObject) null)
      {
        List<UIObject> source = (List<UIObject>) null;
        try
        {
          if (locatorStrategy.ToString() == "xpath")
            source = this.FindMultipleElementsXPath(searchTarget, uiObject);
          else if (locatorStrategy.ToString() == "tag name")
          {
            source = this.FindMultipleElementsXPath("//" + searchTarget, uiObject);
          }
          else
          {
            UICondition matchCondition = this.GetMatchCondition(searchTarget, locatorStrategy);
            if (matchCondition != null)
            {
              this.ResetUICollectionTimeout();
              source = new UIBreadthFirstDescendants<UIObject>(uiObject, UIObject.Factory).FindMultiple(matchCondition).ToList<UIObject>();
              if (uiObject.Matches(matchCondition))
                source.Insert(0, uiObject);
            }
          }
          if (source != null)
            str = new JArray((object) source.Select<UIObject, JObject>((Func<UIObject, JObject>) (entry => new JObject((object) new JProperty("ELEMENT", (object) this.sessionKnownElements.Add(entry)))))).ToString();
        }
        catch (XPathException ex)
        {
          str = "Invalid XPath expression: " + searchTarget;
        }
        catch (Exception ex)
        {
          Console.WriteLine(string.Empty);
          Console.WriteLine("### SearchMultipleElements exception: " + ex.Message);
          Console.WriteLine(string.Empty);
        }
      }
      return str;
    }

    public string GetElementAttribute(string elementId, string attributeName)
    {
      string str = string.Empty;
      UIObject uiObject = this.sessionKnownElements.Get(elementId);
      if (uiObject != (UIObject) null)
      {
        if (UIProperty.Exists(attributeName))
        {
          try
          {
            UIProperty property = UIProperty.Get(attributeName);
            str = !(property.Name == "RuntimeId") ? (!(property.Name == "ClickablePoint") ? uiObject.GetProperty(property).ToString() : uiObject.GetClickablePoint().ToString()) : uiObject.RuntimeId;
          }
          catch
          {
          }
        }
      }
      return str;
    }

    public string GetElementProperty(string elementId, string propertyName)
    {
      string str = string.Empty;
      UIObject uiObject1 = this.sessionKnownElements.Get(elementId);
      if (uiObject1 != (UIObject) null)
      {
        try
        {
          if (!(propertyName == "text"))
          {
            if (!(propertyName == "name"))
            {
              if (!(propertyName == "size"))
              {
                if (!(propertyName == "location"))
                {
                  if (propertyName == "center point")
                  {
                    Dictionary<string, int> dictionary = new Dictionary<string, int>();
                    PointI rectangleTopLeft = uiObject1.GetBoundingRectangleTopLeft(this.applicationObject.NativeWindowHandle);
                    SizeI size = uiObject1.GetAdjustedBoundingRectangle().Size;
                    dictionary.Add("x", rectangleTopLeft.X + size.Width / 2);
                    dictionary.Add("y", rectangleTopLeft.Y + size.Height / 2);
                    str = JsonConvert.SerializeObject((object) dictionary);
                  }
                }
                else
                {
                  Dictionary<string, int> dictionary = new Dictionary<string, int>();
                  PointI rectangleTopLeft = uiObject1.GetBoundingRectangleTopLeft(this.applicationObject.NativeWindowHandle);
                  dictionary.Add("x", rectangleTopLeft.X);
                  dictionary.Add("y", rectangleTopLeft.Y);
                  str = JsonConvert.SerializeObject((object) dictionary);
                }
              }
              else
              {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                SizeI size = uiObject1.GetAdjustedBoundingRectangle().Size;
                dictionary.Add("width", size.Width);
                dictionary.Add("height", size.Height);
                str = JsonConvert.SerializeObject((object) dictionary);
              }
            }
            else
              str = uiObject1.ControlType.ProgrammaticName;
          }
          else
          {
            List<AutomationPattern> list = ((IEnumerable<AutomationPattern>) uiObject1.GetSupportedPatterns()).ToList<AutomationPattern>();
            if (list.Contains(TextPatternIdentifiers.Pattern))
              str = new TextImplementation(uiObject1).DocumentRange.GetText(-1);
            else if (list.Contains(ValuePatternIdentifiers.Pattern))
              str = new ValueImplementation(uiObject1).Value;
            else if (list.Contains(RangeValuePatternIdentifiers.Pattern))
              str = new RangeValueImplementation(uiObject1).Value.ToString();
            else if (list.Contains(SelectionPatternIdentifiers.Pattern))
            {
              foreach (UIObject uiObject2 in new SelectionImplementation<UIObject>(uiObject1, UIObject.Factory).Selection)
              {
                if (str != string.Empty)
                  str += ", ";
                str += uiObject2.Name;
              }
            }
            else if (uiObject1.Name != null)
              str = uiObject1.Name;
            str = str.Normalize();
          }
        }
        catch
        {
        }
      }
      return str;
    }

    public int ActionOnElement(string elementId, string actionName, string data)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      UIObject uiObject;
      if (!string.IsNullOrEmpty(elementId))
      {
        uiObject = this.sessionKnownElements.Get(elementId);
      }
      else
      {
        string activeElement = this.GetActiveElement();
        uiObject = string.IsNullOrEmpty(activeElement) ? this.applicationObject : this.sessionKnownElements.Get(activeElement);
      }
      if (uiObject != (UIObject) null)
      {
        try
        {
          if (((IEnumerable<AutomationPattern>) uiObject.GetSupportedPatterns()).ToList<AutomationPattern>().Contains(ScrollItemPatternIdentifiers.Pattern) && uiObject.IsOffscreen)
          {
            new ScrollItemImplementation(uiObject).ScrollIntoView();
            Thread.Sleep(TimeSpan.FromSeconds(1.0));
          }
          if (!uiObject.IsOffscreen || string.IsNullOrEmpty(elementId) && actionName == "value")
          {
            if (!(actionName == "clear"))
            {
              if (!(actionName == "click"))
              {
                if (actionName == "value")
                {
                  string text = KeyboardInput.Process(data.ToString());
                  if (text.Length > 0)
                    uiObject.SendKeys(text);
                }
              }
              else
              {
                this.applicationObject.SetFocus();
                uiObject.Click();
              }
            }
            else if (uiObject.ControlType == ControlType.Edit)
              new Edit(uiObject).SetValue(string.Empty);
            responseStatus = ResponseStatus.Success;
          }
          else
            responseStatus = ResponseStatus.ElementIsNotInteractable;
        }
        catch
        {
        }
      }
      return (int) responseStatus;
    }

    public bool GetElementState(string elementId, string state)
    {
      bool flag = false;
      UIObject uiObject = this.sessionKnownElements.Get(elementId);
      if (uiObject != (UIObject) null)
      {
        try
        {
          if (!(state == "displayed"))
          {
            if (!(state == "enabled"))
            {
              if (state == "selected")
              {
                foreach (AutomationPattern supportedPattern in uiObject.GetSupportedPatterns())
                {
                  if (supportedPattern != null)
                  {
                    if (supportedPattern.ProgrammaticName == TogglePatternIdentifiers.Pattern.ProgrammaticName)
                    {
                      flag = new ToggleImplementation(uiObject).ToggleState.ToString() == "On";
                      break;
                    }
                    if (supportedPattern.ProgrammaticName == SelectionItemPattern.Pattern.ProgrammaticName)
                    {
                      flag = new SelectionItemImplementation<UIObject>(uiObject, UIObject.Factory).IsSelected;
                      break;
                    }
                  }
                }
              }
            }
            else
              flag = uiObject.IsEnabled;
          }
          else
            flag = !uiObject.IsOffscreen;
        }
        catch
        {
        }
      }
      return flag;
    }

    public int GetElementStatus(string elementId) => (int) this.sessionKnownElements.GetStatus(elementId);

    public IntPtr GetApplicationHwnd()
    {
      IntPtr num = IntPtr.Zero;
      if (this.applicationObject != (UIObject) null)
      {
        try
        {
          num = this.applicationObject.NativeWindowHandle;
        }
        catch
        {
          this.applicationObject = (UIObject) null;
        }
      }
      return num;
    }

    public int Navigate(string navigationType)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      if (navigationType == "back")
      {
        this.applicationObject.SetFocus();
        this.applicationObject.SendKeys("{ALT DOWN}{LEFT}{ALT UP}");
        responseStatus = ResponseStatus.Success;
      }
      else if (navigationType == "forward")
      {
        this.applicationObject.SetFocus();
        this.applicationObject.SendKeys("{ALT DOWN}{RIGHT}{ALT UP}");
        responseStatus = ResponseStatus.Success;
      }
      return (int) responseStatus;
    }

    public string GetProperty(string propertyName)
    {
      string str = string.Empty;
      if (this.applicationObject != (UIObject) null)
      {
        try
        {
          if (!(propertyName == "title"))
          {
            if (propertyName == "orientation")
              str = this.applicationObject.Orientation == OrientationType.Vertical ? "PORTRAIT" : "LANDSCAPE";
          }
          else
            str = this.applicationObject.Name;
        }
        catch
        {
        }
      }
      return str;
    }

    public string GetTopLevelWindows()
    {
      string empty = string.Empty;
      try
      {
        IEnumerable<UIObject> source;
        if (this.applicationObject == (UIObject) null)
        {
          source = Enumerable.Empty<UIObject>();
        }
        else
        {
          string str = string.Empty;
          try
          {
            str = this.applicationObject.ClassName;
          }
          catch
          {
          }
          source = !(str == "ApplicationFrameWindow") ? MultipleWindows.GetTopLevelWindowsClassicApp(this.processId) : MultipleWindows.GetTopLevelWindowsModernApp(this.processId);
        }
        empty = new JArray((object) source.Select<UIObject, JValue>((Func<UIObject, JValue>) (entry => new JValue("0x" + entry.NativeWindowHandle.ToString("X8"))))).ToString();
      }
      catch
      {
      }
      return empty;
    }

    public int SwitchToWindow(string windowHandle)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      try
      {
        int windowProcessId = -1;
        UIObject levelWindowHandle = this.GetObjectFromTopLevelWindowHandle(windowHandle, out windowProcessId);
        if (windowProcessId != this.processId)
          throw new ArgumentException("Window handle does not belong to the same process/application");
        this.applicationObject = levelWindowHandle;
        this.applicationObject.SetFocus();
        this.sessionKnownElements.Add(this.applicationObject);
        responseStatus = ResponseStatus.Success;
      }
      catch (ArgumentException ex)
      {
        responseStatus = ResponseStatus.InvalidArgument;
        this.lastErrorMessage = ex.Message;
      }
      catch (Exception ex) when (ex.HResult == -2147220991)
      {
        responseStatus = ResponseStatus.NoSuchWindow;
      }
      catch
      {
      }
      return (int) responseStatus;
    }

    public string GetWindowProperty(string windowHandle, string propertyName)
    {
      string str = string.Empty;
      try
      {
        UIObject uiObject = UIObject.FromHandle((IntPtr) Convert.ToInt32(windowHandle, 16));
        if (uiObject != (UIObject) null)
        {
          if (uiObject.ControlType == ControlType.Window)
          {
            RectangleI boundingRectangle = uiObject.GetAdjustedBoundingRectangle();
            if (!(propertyName == "size"))
            {
              if (propertyName == "position")
              {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                PointI topLeft = boundingRectangle.TopLeft;
                dictionary.Add("x", topLeft.X);
                dictionary.Add("y", topLeft.Y);
                str = JsonConvert.SerializeObject((object) dictionary);
              }
            }
            else
              str = JsonConvert.SerializeObject((object) new Dictionary<string, int>()
              {
                {
                  "width",
                  boundingRectangle.Width
                },
                {
                  "height",
                  boundingRectangle.Height
                }
              });
          }
        }
      }
      catch
      {
      }
      return str;
    }

    public int SetWindowProperty(string windowHandle, string propertyName, string JSONParameters)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      try
      {
        UIObject uiObject = UIObject.FromHandle((IntPtr) Convert.ToInt32(windowHandle, 16));
        if (uiObject != (UIObject) null)
        {
          if (uiObject.ControlType == ControlType.Window)
          {
            WindowImplementation windowImplementation = new WindowImplementation(uiObject);
            if (!(propertyName == "maximize"))
            {
              if (!(propertyName == "position"))
              {
                if (propertyName == "size")
                {
                  windowImplementation.SetWindowVisualState(WindowVisualState.Normal);
                  TransformImplementation transformImplementation = new TransformImplementation(uiObject);
                  SizeI rectangleOffsetSize = uiObject.GetAdjustedBoundingRectangleOffsetSize();
                  Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(JSONParameters);
                  double num1 = Convert.ToDouble(dictionary["width"].ToString()) + (double) rectangleOffsetSize.Width;
                  double num2 = Convert.ToDouble(dictionary["height"].ToString()) + (double) rectangleOffsetSize.Height;
                  double width = num1;
                  double height = num2;
                  transformImplementation.Resize(width, height);
                }
              }
              else
              {
                windowImplementation.SetWindowVisualState(WindowVisualState.Normal);
                TransformImplementation transformImplementation = new TransformImplementation(uiObject);
                PointI rectangleOffsetPosition = uiObject.GetAdjustedBoundingRectangleOffsetPosition();
                Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(JSONParameters);
                double num1 = Convert.ToDouble(dictionary["x"]) + (double) rectangleOffsetPosition.X;
                double num2 = Convert.ToDouble(dictionary["y"]) + (double) rectangleOffsetPosition.Y;
                double x = num1;
                double y = num2;
                transformImplementation.Move(x, y);
              }
            }
            else
              windowImplementation.SetWindowVisualState(WindowVisualState.Maximized);
            responseStatus = ResponseStatus.Success;
          }
        }
      }
      catch
      {
      }
      return (int) responseStatus;
    }

    public int SendZoom(int centerX, int centerY, int distanceStart, int distanceEnd)
    {
      using (InputController.Activate(PointerInputType.MultiTouch))
      {
        PointerInput.Move(new PointI(centerX, centerY));
        MultiPointGesture.Stretch(MultiPointGesture.DefaultPinchStretchDirection, MultiPointGesture.DefaultPinchStretchDuration, (uint) distanceStart, (uint) distanceEnd);
      }
      return 0;
    }

    public int SendPinch(int centerX, int centerY, int distanceStart, int distanceEnd)
    {
      using (InputController.Activate(PointerInputType.MultiTouch))
      {
        PointerInput.Move(new PointI(centerX, centerY));
        MultiPointGesture.Pinch(MultiPointGesture.DefaultPinchStretchDirection, MultiPointGesture.DefaultPinchStretchDuration, (uint) distanceStart, (uint) distanceEnd);
      }
      return 0;
    }

    public int SendMouse(string actionType, string JSONParameters)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      try
      {
        Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(JSONParameters);
        switch (actionType)
        {
          case "buttondown":
          case "buttonup":
          case "click":
            int buttonNumber = dictionary.ContainsKey("button") ? Convert.ToInt32(dictionary["button"]) : 0;
            responseStatus = MouseHandler.SendMouseAction(actionType, buttonNumber);
            break;
          case "doubleclick":
            responseStatus = MouseHandler.SendMouseDoubleClick();
            break;
          case "moveto_element_center":
            UIObject element1 = this.sessionKnownElements.Get(Convert.ToString(dictionary["element"]));
            responseStatus = MouseHandler.SendMouseMoveToElementCenter(actionType, element1);
            break;
          case "moveto_element_relative":
            int int32_1 = Convert.ToInt32(dictionary["xoffset"]);
            int int32_2 = Convert.ToInt32(dictionary["yoffset"]);
            UIObject element2 = this.sessionKnownElements.Get(Convert.ToString(dictionary["element"]));
            responseStatus = MouseHandler.SendMouseMoveToElementRelative(actionType, element2, int32_1, int32_2);
            break;
          case "moveto_relative":
            int int32_3 = Convert.ToInt32(dictionary["xoffset"]);
            int int32_4 = Convert.ToInt32(dictionary["yoffset"]);
            responseStatus = MouseHandler.SendMouseMoveToRelative(actionType, int32_3, int32_4);
            break;
          default:
            throw new Exception("Unrecognized action type: " + actionType);
        }
      }
      catch (ArgumentException ex)
      {
        responseStatus = ResponseStatus.InvalidArgument;
        this.lastErrorMessage = ex.Message;
      }
      catch (Exception ex)
      {
        this.lastErrorMessage = ex.Message;
      }
      return (int) responseStatus;
    }

        public int SendTouch(string touchType, string JSONParameters) {
            ResponseStatus responseStatus = ResponseStatus.UnknownError;
            try {
                int xoffset;
                int yoffset;
                UIObject uiObject;
                Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(JSONParameters);
                switch (touchType) {
                    case "move":
                    case "down":
                    case "up":
                        int xScreen;
                        int yScreen;
                        this.ConvertClientToScreen(Convert.ToInt32(dictionary["x"]), Convert.ToInt32(dictionary["y"]), out xScreen, out yScreen);
                        responseStatus = TouchHandler.SendTouchTypePress(touchType, xScreen, yScreen);
                        break;
                    case "flick":

                        if (dictionary.ContainsKey("element") && dictionary.ContainsKey("xoffset") && dictionary.ContainsKey("yoffset")) {
                            xoffset = Convert.ToInt32(dictionary["xoffset"]);
                            yoffset = Convert.ToInt32(dictionary["yoffset"]);
                            uiObject = this.sessionKnownElements.Get(Convert.ToString(dictionary["element"]));
                            int flickYOffset = yoffset;
                            UIObject flickElement = uiObject;
                            responseStatus = TouchHandler.SendTouchFlick(xoffset, flickYOffset, flickElement);
                            return (int)responseStatus;
                        } else if (dictionary.ContainsKey("xspeed")) {
                            if (dictionary.ContainsKey("yspeed")) {
                                responseStatus = TouchHandler.SendTouchFlick(this.applicationObject, Convert.ToInt32(dictionary["xspeed"]), Convert.ToInt32(dictionary["yspeed"]));
                                return (int)responseStatus;
                            } else
                                return (int)responseStatus;
                        } else
                            return (int)responseStatus;

                    case "click":
                    case "doubleclick":
                    case "longclick":
                        break;
                    case "scroll":
                        xoffset = Convert.ToInt32(dictionary["xoffset"]);
                        yoffset = Convert.ToInt32(dictionary["yoffset"]);
                        uiObject = !dictionary.ContainsKey("element") ? this.applicationObject : this.sessionKnownElements.Get(Convert.ToString(dictionary["element"]));
                        UIObject scrollElement = uiObject;
                        responseStatus = TouchHandler.SendTouchTypeScroll(xoffset, yoffset, scrollElement);
                        break;
                    default:
                        return (int)responseStatus;
                }
                UIObject clickElement = this.sessionKnownElements.Get(Convert.ToString(dictionary["element"]));
                responseStatus = TouchHandler.SendTouchTypeClick(touchType, clickElement);

            } catch (Exception ex) {
            }
            return (int)responseStatus;
        }

    public int SendMultiTouch(string JSONObject)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      try
      {
        Dictionary<string, object>[][] dictionaryArray1 = JsonConvert.DeserializeObject<Dictionary<string, object>[][]>(JSONObject);
        using (InputController.Activate(PointerInputType.MultiTouch))
        {
          MultiTouchInjectionData[] injectionData = new MultiTouchInjectionData[dictionaryArray1.Length];
          int key = 0;
          foreach (Dictionary<string, object>[] dictionaryArray2 in dictionaryArray1)
          {
            MultiTouchInjectionData touchInjectionData = new MultiTouchInjectionData();
            touchInjectionData.Actions = new MultiAction[dictionaryArray2.Length];
            int num1 = 0;
            foreach (Dictionary<string, object> dictionary1 in dictionaryArray2)
            {
              MultiAction multiAction1 = new MultiAction();
              if (!dictionary1.ContainsKey("action"))
                throw new ArgumentException("action is missing from touch event", "action");
              Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
              if (dictionary1.ContainsKey("options"))
                dictionary2 = dictionary1["options"] is JObject jobject ? jobject.ToObject<Dictionary<string, object>>() : (Dictionary<string, object>) null;
              string str1 = dictionary1["action"].ToString();
              RectangleI boundingRectangle;
              if (!(str1 == "press"))
              {
                if (!(str1 == "moveTo"))
                {
                  if (!(str1 == "release"))
                  {
                    if (!(str1 == "wait"))
                      throw new NotImplementedException("action '" + dictionary1["action"] + "' requested is not implemented.");
                    multiAction1.Action = ActionType.Wait;
                    multiAction1.Duration = dictionary2 != null && dictionary2.ContainsKey("ms") ? Convert.ToUInt32(dictionary2["ms"]) : throw new ArgumentException("wait action missing 'ms' duration");
                  }
                  else
                  {
                    multiAction1.Action = ActionType.Release;
                    multiAction1.Point = this.lastPoints[key];
                    if (dictionary2 != null && dictionary2.ContainsKey("x") && dictionary2.ContainsKey("y"))
                    {
                      int xScreen;
                      int yScreen;
                      this.ConvertClientToScreen(Convert.ToInt32(dictionary2["x"]), Convert.ToInt32(dictionary2["y"]), out xScreen, out yScreen);
                      multiAction1.Point = new PointI(xScreen, yScreen);
                      this.lastPoints[key] = multiAction1.Point;
                    }
                  }
                }
                else
                {
                  multiAction1.Action = ActionType.Move;
                  int xClient;
                  int yClient;
                  if (dictionary2 != null && dictionary2.ContainsKey("x") && dictionary2.ContainsKey("y"))
                  {
                    xClient = Convert.ToInt32(dictionary2["x"]);
                    yClient = Convert.ToInt32(dictionary2["y"]);
                  }
                  else
                  {
                    UIObject uiObject = dictionary2 != null && dictionary2.ContainsKey("element") ? this.sessionKnownElements.Get(Convert.ToString(dictionary2["element"])) : throw new ArgumentException("moveTo action missing coordinates");
                    PointI rectangleTopLeft = uiObject.GetBoundingRectangleTopLeft(this.applicationObject.NativeWindowHandle);
                    int x = rectangleTopLeft.X;
                    boundingRectangle = uiObject.BoundingRectangle;
                    int num2 = boundingRectangle.Width / 2;
                    xClient = x + num2;
                    int y = rectangleTopLeft.Y;
                    boundingRectangle = uiObject.BoundingRectangle;
                    int num3 = boundingRectangle.Height / 2;
                    yClient = y + num3;
                  }
                  string str2 = dictionary2.ContainsKey("origin") ? Convert.ToString(dictionary2["origin"]) : "pointer";
                  if (str2 == "pointer")
                  {
                    int num2 = xClient;
                    PointI lastPoint = this.lastPoints[key];
                    int x1 = lastPoint.X;
                    int x2 = num2 + x1;
                    int num3 = yClient;
                    lastPoint = this.lastPoints[key];
                    int y1 = lastPoint.Y;
                    int y2 = num3 + y1;
                    multiAction1.Point = new PointI(x2, y2);
                  }
                  else if (str2 == "viewport")
                  {
                    int xScreen;
                    int yScreen;
                    this.ConvertClientToScreen(xClient, yClient, out xScreen, out yScreen);
                    multiAction1.Point = new PointI(xScreen, yScreen);
                  }
                  else
                  {
                    responseStatus = ResponseStatus.InvalidArgument;
                    throw new ArgumentException("moveTo action contains invalid origin. Expected 'pointer' or 'viewport'(default)");
                  }
                  this.lastPoints[key] = multiAction1.Point;
                }
              }
              else
              {
                multiAction1.Action = ActionType.Press;
                if (dictionary2 != null && dictionary2.ContainsKey("x") && dictionary2.ContainsKey("y"))
                {
                  int xScreen;
                  int yScreen;
                  this.ConvertClientToScreen(Convert.ToInt32(dictionary2["x"]), Convert.ToInt32(dictionary2["y"]), out xScreen, out yScreen);
                  multiAction1.Point = new PointI(xScreen, yScreen);
                }
                else
                {
                  UIObject uiObject = dictionary2 != null && dictionary2.ContainsKey("element") ? this.sessionKnownElements.Get(Convert.ToString(dictionary2["element"])) : throw new ArgumentException("press action missing coordinates");
                  MultiAction multiAction2 = multiAction1;
                  boundingRectangle = uiObject.BoundingRectangle;
                  PointI location = boundingRectangle.Location;
                  multiAction2.Point = location;
                }
                this.lastPoints[key] = multiAction1.Point;
              }
              touchInjectionData.Actions[num1++] = multiAction1;
            }
            injectionData[key++] = touchInjectionData;
          }
          MultiPointGesture.InjectMultiPointGesture(injectionData);
          responseStatus = ResponseStatus.Success;
        }
      }
      catch (Exception ex)
      {
      }
      return (int) responseStatus;
    }

    public int SendActions(string ActionsJSONString)
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      try
      {
        this.sessionActionsHandler.PerformActions(ActionsJSONString);
        responseStatus = ResponseStatus.Success;
      }
      catch (NoSuchWindowException ex)
      {
        responseStatus = ResponseStatus.NoSuchWindow;
        this.lastErrorMessage = ex.Message;
      }
      catch (NoSuchElementException ex)
      {
        responseStatus = ResponseStatus.NoSuchElement;
        this.lastErrorMessage = ex.Message;
      }
      catch (StaleElementException ex)
      {
        responseStatus = ResponseStatus.StaleElementReference;
        this.lastErrorMessage = ex.Message;
      }
      catch (NotImplementedException ex)
      {
        responseStatus = ResponseStatus.UnsupportedOperation;
        this.lastErrorMessage = ex.Message;
      }
      catch (Exception ex) when (ex is ArgumentException || ex is JsonReaderException)
      {
        responseStatus = ResponseStatus.InvalidArgument;
        this.lastErrorMessage = ex.Message;
      }
      catch (Exception ex)
      {
        this.lastErrorMessage = ex.Message;
      }
      return (int) responseStatus;
    }

    public int ReleaseActions()
    {
      ResponseStatus responseStatus = ResponseStatus.UnknownError;
      try
      {
        this.sessionActionsHandler.ReleaseActions();
        responseStatus = ResponseStatus.Success;
      }
      catch (ArgumentException ex)
      {
        responseStatus = ResponseStatus.InvalidArgument;
        this.lastErrorMessage = ex.Message;
      }
      catch (NoSuchWindowException ex)
      {
        responseStatus = ResponseStatus.NoSuchWindow;
        this.lastErrorMessage = ex.Message;
      }
      catch (Exception ex)
      {
        this.lastErrorMessage = ex.Message;
      }
      return (int) responseStatus;
    }

    public string GetLocation()
    {
      string str = string.Empty;
      GeoCoordinateWatcher coordinateWatcher = new GeoCoordinateWatcher();
      coordinateWatcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(this.GeoLocationWatcherStatusChanged);
      this.GeoLocationWaiter.Reset();
      coordinateWatcher.Start();
      this.GeoLocationWaiter.WaitOne(this.defaultLocationWaitTime);
      coordinateWatcher.StatusChanged -= new EventHandler<GeoPositionStatusChangedEventArgs>(this.GeoLocationWatcherStatusChanged);
      GeoCoordinate location = coordinateWatcher.Position.Location;
      if (!location.IsUnknown)
        str = JsonConvert.SerializeObject((object) new Dictionary<string, double>()
        {
          {
            "altitude",
            location.Altitude
          },
          {
            "latitude",
            location.Latitude
          },
          {
            "longitude",
            location.Longitude
          }
        });
      coordinateWatcher.Stop();
      coordinateWatcher.Dispose();
      return str;
    }

    private void GeoLocationWatcherStatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
    {
      switch (e.Status)
      {
        case GeoPositionStatus.Ready:
        case GeoPositionStatus.Disabled:
          this.GeoLocationWaiter.Set();
          break;
      }
    }

    public void ConvertClientToScreen(int xClient, int yClient, out int xScreen, out int yScreen) => PositionAdapter.ConvertClientToScreen(this.applicationObject.NativeWindowHandle, xClient, yClient, out xScreen, out yScreen);

    public void ConvertScreenToClient(int xScreen, int yScreen, out int xClient, out int yClient) => PositionAdapter.ConvertScreenToClient(this.applicationObject.NativeWindowHandle, xScreen, yScreen, out xClient, out yClient);

    public int GetApplicationType() => (int) this.appType;

    public bool IsExperimentalW3C() => this.sessionCapabilities.IsExperimental();

    private enum TargetAppType
    {
      NotSet = -1, // 0xFFFFFFFF
      Modern = 0,
      Win32 = 1,
      Root = 2,
      HandleToWindow = 3,
    }
  }
}
