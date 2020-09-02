// Decompiled with JetBrains decompiler
// Type: MitaBroker.UIXmlDom
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MS.Internal.Mita.Foundation;
using MS.Internal.Mita.Foundation.Patterns;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Automation;
using System.Xml;

namespace MitaBroker
{
  internal class UIXmlDom
  {
    private static readonly IList<UIXmlDom.UIPropertyString> UIPropertyStrings = (IList<UIXmlDom.UIPropertyString>) new ReadOnlyCollection<UIXmlDom.UIPropertyString>((IList<UIXmlDom.UIPropertyString>) new UIXmlDom.UIPropertyString[21]
    {
      new UIXmlDom.UIPropertyString("AcceleratorKey", false),
      new UIXmlDom.UIPropertyString("AccessKey", false),
      new UIXmlDom.UIPropertyString("AutomationId", false),
      new UIXmlDom.UIPropertyString("ClassName", false),
      new UIXmlDom.UIPropertyString("FrameworkId", false),
      new UIXmlDom.UIPropertyString("HasKeyboardFocus", true),
      new UIXmlDom.UIPropertyString("HelpText", false),
      new UIXmlDom.UIPropertyString("IsContentElement", true),
      new UIXmlDom.UIPropertyString("IsControlElement", true),
      new UIXmlDom.UIPropertyString("IsEnabled", true),
      new UIXmlDom.UIPropertyString("IsKeyboardFocusable", true),
      new UIXmlDom.UIPropertyString("IsOffscreen", true),
      new UIXmlDom.UIPropertyString("IsPassword", true),
      new UIXmlDom.UIPropertyString("IsRequiredForForm", true),
      new UIXmlDom.UIPropertyString("ItemStatus", false),
      new UIXmlDom.UIPropertyString("ItemType", false),
      new UIXmlDom.UIPropertyString("LocalizedControlType", false),
      new UIXmlDom.UIPropertyString("Name", false),
      new UIXmlDom.UIPropertyString("Orientation", false),
      new UIXmlDom.UIPropertyString("ProcessId", false),
      new UIXmlDom.UIPropertyString("RuntimeId", false)
    });
    private XmlDocument _xmlDoc;
    private UIObject _applicationRoot;
    private RectangleI _applicationRootRectangle;

    public UIXmlDom(UIObject applicationRoot)
    {
      this._xmlDoc = new XmlDocument();
      this._applicationRoot = applicationRoot;
      this._applicationRootRectangle = this._applicationRoot.GetAdjustedBoundingRectangle();
    }

    public XmlDocument CreateUIXmlDocumentWithDictionary(
      UIObject startNode,
      Dictionary<string, UIObject> objectDictionary)
    {
      this._xmlDoc.AppendChild(this.PopulateXmlNodeForUIObject(startNode, objectDictionary));
      this.AddUIChildrenToXmlNode(startNode, (XmlNode) this._xmlDoc.DocumentElement, objectDictionary);
      return this._xmlDoc;
    }

    public XmlDocument CreateUIXmlDocument(UIObject startNode) => this.CreateUIXmlDocumentWithDictionary(startNode, (Dictionary<string, UIObject>) null);

    private XmlNode PopulateXmlNodeForUIObject(
      UIObject uiObjectToAdd,
      Dictionary<string, UIObject> objectDictionary)
    {
      XmlNode nodeToPopulate = (XmlNode) null;
      try
      {
        string str = uiObjectToAdd.ControlType.ProgrammaticName.Replace("ControlType.", string.Empty);
        nodeToPopulate = (XmlNode) this._xmlDoc.CreateElement(str == string.Empty ? "Element" : str);
        if (nodeToPopulate != null)
        {
          string elementIdFromElement = Utilities.GetElementIdFromElement(uiObjectToAdd);
          UIXmlDom.AddObjectToDictionaryIfNeeded(uiObjectToAdd, elementIdFromElement, objectDictionary);
          this.AddUIPropertiesToNode(uiObjectToAdd, nodeToPopulate);
          this.AddCoordsAndDimensionsToNode(uiObjectToAdd, nodeToPopulate);
          this.AddSupportedPatterns(uiObjectToAdd, nodeToPopulate);
          if (uiObjectToAdd.RuntimeId == string.Empty)
          {
            if (objectDictionary != null)
            {
              XmlAttribute attribute = this._xmlDoc.CreateAttribute("ElementId");
              attribute.Value = elementIdFromElement;
              nodeToPopulate.Attributes.Append(attribute);
            }
          }
        }
      }
      catch (KeyNotFoundException ex)
      {
      }
      return nodeToPopulate;
    }

    private void AddCoordsAndDimensionsToNode(UIObject uiObjectToAdd, XmlNode nodeToPopulate)
    {
      XmlAttribute attribute1 = this._xmlDoc.CreateAttribute("x");
      XmlAttribute attribute2 = this._xmlDoc.CreateAttribute("y");
      XmlAttribute attribute3 = this._xmlDoc.CreateAttribute("width");
      XmlAttribute attribute4 = this._xmlDoc.CreateAttribute("height");
      try
      {
        RectangleI boundingRectangle = uiObjectToAdd.GetAdjustedBoundingRectangle();
        attribute3.Value = boundingRectangle.Width.ToString();
        XmlAttribute xmlAttribute1 = attribute4;
        int num = boundingRectangle.Height;
        string str1 = num.ToString();
        xmlAttribute1.Value = str1;
        PointI relativePoint = PositionAdapter.GetRelativePoint(boundingRectangle.TopLeft, this._applicationRootRectangle.TopLeft);
        XmlAttribute xmlAttribute2 = attribute1;
        num = relativePoint.X;
        string str2 = num.ToString();
        xmlAttribute2.Value = str2;
        XmlAttribute xmlAttribute3 = attribute2;
        num = relativePoint.Y;
        string str3 = num.ToString();
        xmlAttribute3.Value = str3;
      }
      catch
      {
        attribute1.Value = "0";
        attribute2.Value = "0";
        attribute3.Value = "0";
        attribute4.Value = "0";
      }
      nodeToPopulate.Attributes.Append(attribute1);
      nodeToPopulate.Attributes.Append(attribute2);
      nodeToPopulate.Attributes.Append(attribute3);
      nodeToPopulate.Attributes.Append(attribute4);
    }

    private void AddUIPropertiesToNode(UIObject uiObjectToAdd, XmlNode nodeToPopulate)
    {
      foreach (UIXmlDom.UIPropertyString uiPropertyString in (IEnumerable<UIXmlDom.UIPropertyString>) UIXmlDom.UIPropertyStrings)
      {
        try
        {
          XmlAttribute attribute = this._xmlDoc.CreateAttribute(uiPropertyString.Name);
          attribute.Value = uiObjectToAdd.GetType().GetProperty(uiPropertyString.Name).GetValue((object) uiObjectToAdd, (object[]) null).ToString();
          if (uiPropertyString.ShouldLowercase)
            attribute.Value.ToLower();
          nodeToPopulate.Attributes.Append(attribute);
        }
        catch (Exception ex)
        {
        }
      }
    }

    private static void AddObjectToDictionaryIfNeeded(
      UIObject uiObjectToAdd,
      string key,
      Dictionary<string, UIObject> objectDictionary)
    {
      if (objectDictionary == null)
        return;
      if (objectDictionary.ContainsKey(key))
      {
        try
        {
          Console.WriteLine(string.Empty);
          Console.WriteLine("Exception: Encountered element sharing the same runtimeId " + key);
          Console.WriteLine(string.Empty);
          Console.WriteLine(string.Format("    new Object {0}", (object) uiObjectToAdd));
          Console.WriteLine("    runtimeId " + uiObjectToAdd.RuntimeId);
          Console.WriteLine("    className " + uiObjectToAdd.ClassName);
          Console.WriteLine("    name " + uiObjectToAdd.Name);
          Console.WriteLine(string.Format("    NativeWindowHandle {0}", (object) uiObjectToAdd.NativeWindowHandle));
          Console.WriteLine(string.Empty);
          UIObject uiObject = objectDictionary[key];
          Console.WriteLine(string.Format("    existing Object {0}", (object) uiObject));
          Console.WriteLine("    runtimeId " + uiObject.RuntimeId);
          Console.WriteLine("    className " + uiObject.ClassName);
          Console.WriteLine("    name " + uiObject.Name);
          Console.WriteLine(string.Format("    NativeWindowHandle {0}", (object) uiObject.NativeWindowHandle));
          Console.WriteLine(string.Empty);
          Console.WriteLine(string.Empty);
          Console.WriteLine("Ignoring new object and keeping existing one");
          Console.WriteLine(string.Empty);
        }
        catch
        {
        }
      }
      else
        objectDictionary.Add(key, uiObjectToAdd);
    }

    private void AddSupportedPatterns(UIObject uiObjectToAdd, XmlNode nodeToPopulate)
    {
      foreach (AutomationPattern supportedPattern in uiObjectToAdd.GetSupportedPatterns())
      {
        if (supportedPattern != null)
        {
          if (supportedPattern.ProgrammaticName == DockPatternIdentifiers.Pattern.ProgrammaticName)
          {
            IDock dock = (IDock) new DockImplementation(uiObjectToAdd);
            this.AddPropertiesAsXmlAttributes(nodeToPopulate, (object) dock);
          }
          else if (supportedPattern.ProgrammaticName == ExpandCollapsePatternIdentifiers.Pattern.ProgrammaticName)
          {
            IExpandCollapse expandCollapse = (IExpandCollapse) new ExpandCollapseImplementation(uiObjectToAdd);
            this.AddPropertiesAsXmlAttributes(nodeToPopulate, (object) expandCollapse);
          }
          else if (supportedPattern.ProgrammaticName == RangeValuePatternIdentifiers.Pattern.ProgrammaticName)
          {
            IRangeValue rangeValue = (IRangeValue) new RangeValueImplementation(uiObjectToAdd);
            this.AddPropertiesAsXmlAttributes(nodeToPopulate, (object) rangeValue);
          }
          else if (supportedPattern.ProgrammaticName == ScrollPatternIdentifiers.Pattern.ProgrammaticName)
          {
            IScroll scroll = (IScroll) new ScrollImplementation(uiObjectToAdd);
            this.AddPropertiesAsXmlAttributes(nodeToPopulate, (object) scroll);
          }
          else if (supportedPattern.ProgrammaticName == TogglePatternIdentifiers.Pattern.ProgrammaticName)
          {
            IToggle toggle = (IToggle) new ToggleImplementation(uiObjectToAdd);
            this.AddPropertiesAsXmlAttributes(nodeToPopulate, (object) toggle);
          }
          else if (supportedPattern.ProgrammaticName == TransformPatternIdentifiers.Pattern.ProgrammaticName)
          {
            ITransform transform = (ITransform) new TransformImplementation(uiObjectToAdd);
            this.AddPropertiesAsXmlAttributes(nodeToPopulate, (object) transform);
          }
          else if (supportedPattern.ProgrammaticName == WindowPatternIdentifiers.Pattern.ProgrammaticName)
          {
            IWindow window = (IWindow) new WindowImplementation(uiObjectToAdd);
            this.AddPropertiesAsXmlAttributes(nodeToPopulate, (object) window);
          }
          else
          {
            if (supportedPattern.ProgrammaticName == SelectionItemPattern.Pattern.ProgrammaticName)
            {
              ISelectionItem<UIObject> selectionItem = (ISelectionItem<UIObject>) new SelectionItemImplementation<UIObject>(uiObjectToAdd, UIObject.Factory);
              this.AddPropertiesAsXmlAttributes(nodeToPopulate, (object) selectionItem);
              break;
            }
            if (supportedPattern.ProgrammaticName == SelectionPattern.Pattern.ProgrammaticName)
            {
              SelectionImplementation<UIObject> selectionImplementation = new SelectionImplementation<UIObject>(uiObjectToAdd, UIObject.Factory);
              string empty = string.Empty;
              foreach (UIObject uiObject in selectionImplementation.Selection)
              {
                if (empty != string.Empty)
                  empty += ";";
                empty += uiObject.RuntimeId.ToString();
              }
              XmlAttribute attribute = this._xmlDoc.CreateAttribute("Selection");
              attribute.Value = empty;
              nodeToPopulate.Attributes.Append(attribute);
            }
          }
        }
      }
    }

    private void AddPropertiesAsXmlAttributes(XmlNode nodeToPopulate, object currObject)
    {
      foreach (PropertyInfo property in currObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
      {
        XmlAttribute attribute = this._xmlDoc.CreateAttribute(property.Name);
        try
        {
          object obj = property.GetValue(currObject);
          if (obj != null)
          {
            attribute.Value = obj.ToString();
            nodeToPopulate.Attributes.Append(attribute);
          }
        }
        catch (TargetInvocationException ex)
        {
        }
        catch (TypeInitializationException ex)
        {
        }
      }
    }

    private void AddUIChildrenToXmlNode(
      UIObject rootUIObject,
      XmlNode rootNode,
      Dictionary<string, UIObject> objectDictionary)
    {
      UICollection<UIObject> staticCollection = rootUIObject.Children.ToStaticCollection();
      int count = staticCollection.Count;
      for (int index = 0; index < count; ++index)
      {
        UIObject uiObject = staticCollection[index];
        XmlNode xmlNode = this.PopulateXmlNodeForUIObject(uiObject, objectDictionary);
        if (xmlNode != null)
        {
          this.AddUIChildrenToXmlNode(uiObject, xmlNode, objectDictionary);
          if (rootNode != null)
            rootNode.AppendChild(xmlNode);
          else
            this._xmlDoc.AppendChild(xmlNode);
        }
      }
    }

    private struct UIPropertyString
    {
      private readonly string name;
      private readonly bool shouldLowercase;

      public UIPropertyString(string name, bool shouldLowercase)
      {
        this.name = name;
        this.shouldLowercase = shouldLowercase;
      }

      public string Name => this.name;

      public bool ShouldLowercase => this.shouldLowercase;
    }
  }
}
