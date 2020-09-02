// Decompiled with JetBrains decompiler
// Type: MitaBroker.UIXmlDom
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Automation;
using System.Xml;
using MS.Internal.Mita.Foundation;
using MS.Internal.Mita.Foundation.Patterns;

namespace MitaBroker {
    internal class UIXmlDom {
        static readonly IList<UIPropertyString> UIPropertyStrings = new ReadOnlyCollection<UIPropertyString>(list: new UIPropertyString[21] {
            new UIPropertyString(name: "AcceleratorKey", shouldLowercase: false),
            new UIPropertyString(name: "AccessKey", shouldLowercase: false),
            new UIPropertyString(name: "AutomationId", shouldLowercase: false),
            new UIPropertyString(name: "ClassName", shouldLowercase: false),
            new UIPropertyString(name: "FrameworkId", shouldLowercase: false),
            new UIPropertyString(name: "HasKeyboardFocus", shouldLowercase: true),
            new UIPropertyString(name: "HelpText", shouldLowercase: false),
            new UIPropertyString(name: "IsContentElement", shouldLowercase: true),
            new UIPropertyString(name: "IsControlElement", shouldLowercase: true),
            new UIPropertyString(name: "IsEnabled", shouldLowercase: true),
            new UIPropertyString(name: "IsKeyboardFocusable", shouldLowercase: true),
            new UIPropertyString(name: "IsOffscreen", shouldLowercase: true),
            new UIPropertyString(name: "IsPassword", shouldLowercase: true),
            new UIPropertyString(name: "IsRequiredForForm", shouldLowercase: true),
            new UIPropertyString(name: "ItemStatus", shouldLowercase: false),
            new UIPropertyString(name: "ItemType", shouldLowercase: false),
            new UIPropertyString(name: "LocalizedControlType", shouldLowercase: false),
            new UIPropertyString(name: "Name", shouldLowercase: false),
            new UIPropertyString(name: "Orientation", shouldLowercase: false),
            new UIPropertyString(name: "ProcessId", shouldLowercase: false),
            new UIPropertyString(name: "RuntimeId", shouldLowercase: false)
        });

        readonly UIObject _applicationRoot;
        RectangleI _applicationRootRectangle;
        readonly XmlDocument _xmlDoc;

        public UIXmlDom(UIObject applicationRoot) {
            this._xmlDoc = new XmlDocument();
            this._applicationRoot = applicationRoot;
            this._applicationRootRectangle = this._applicationRoot.GetAdjustedBoundingRectangle();
        }

        public XmlDocument CreateUIXmlDocumentWithDictionary(
            UIObject startNode,
            Dictionary<string, UIObject> objectDictionary) {
            this._xmlDoc.AppendChild(newChild: PopulateXmlNodeForUIObject(uiObjectToAdd: startNode, objectDictionary: objectDictionary));
            AddUIChildrenToXmlNode(rootUIObject: startNode, rootNode: this._xmlDoc.DocumentElement, objectDictionary: objectDictionary);
            return this._xmlDoc;
        }

        public XmlDocument CreateUIXmlDocument(UIObject startNode) {
            return CreateUIXmlDocumentWithDictionary(startNode: startNode, objectDictionary: null);
        }

        XmlNode PopulateXmlNodeForUIObject(
            UIObject uiObjectToAdd,
            Dictionary<string, UIObject> objectDictionary) {
            XmlNode nodeToPopulate = null;
            try {
                var str = uiObjectToAdd.ControlType.ProgrammaticName.Replace(oldValue: "ControlType.", newValue: string.Empty);
                nodeToPopulate = this._xmlDoc.CreateElement(name: str == string.Empty ? "Element" : str);
                if (nodeToPopulate != null) {
                    var elementIdFromElement = Utilities.GetElementIdFromElement(element: uiObjectToAdd);
                    AddObjectToDictionaryIfNeeded(uiObjectToAdd: uiObjectToAdd, key: elementIdFromElement, objectDictionary: objectDictionary);
                    AddUIPropertiesToNode(uiObjectToAdd: uiObjectToAdd, nodeToPopulate: nodeToPopulate);
                    AddCoordsAndDimensionsToNode(uiObjectToAdd: uiObjectToAdd, nodeToPopulate: nodeToPopulate);
                    AddSupportedPatterns(uiObjectToAdd: uiObjectToAdd, nodeToPopulate: nodeToPopulate);
                    if (uiObjectToAdd.RuntimeId == string.Empty)
                        if (objectDictionary != null) {
                            var attribute = this._xmlDoc.CreateAttribute(name: "ElementId");
                            attribute.Value = elementIdFromElement;
                            nodeToPopulate.Attributes.Append(node: attribute);
                        }
                }
            } catch (KeyNotFoundException ex) {
            }

            return nodeToPopulate;
        }

        void AddCoordsAndDimensionsToNode(UIObject uiObjectToAdd, XmlNode nodeToPopulate) {
            var attribute1 = this._xmlDoc.CreateAttribute(name: "x");
            var attribute2 = this._xmlDoc.CreateAttribute(name: "y");
            var attribute3 = this._xmlDoc.CreateAttribute(name: "width");
            var attribute4 = this._xmlDoc.CreateAttribute(name: "height");
            try {
                var boundingRectangle = uiObjectToAdd.GetAdjustedBoundingRectangle();
                attribute3.Value = boundingRectangle.Width.ToString();
                var xmlAttribute1 = attribute4;
                var num = boundingRectangle.Height;
                var str1 = num.ToString();
                xmlAttribute1.Value = str1;
                var relativePoint = PositionAdapter.GetRelativePoint(absolutePoint: boundingRectangle.TopLeft, referencePoint: this._applicationRootRectangle.TopLeft);
                var xmlAttribute2 = attribute1;
                num = relativePoint.X;
                var str2 = num.ToString();
                xmlAttribute2.Value = str2;
                var xmlAttribute3 = attribute2;
                num = relativePoint.Y;
                var str3 = num.ToString();
                xmlAttribute3.Value = str3;
            } catch {
                attribute1.Value = "0";
                attribute2.Value = "0";
                attribute3.Value = "0";
                attribute4.Value = "0";
            }

            nodeToPopulate.Attributes.Append(node: attribute1);
            nodeToPopulate.Attributes.Append(node: attribute2);
            nodeToPopulate.Attributes.Append(node: attribute3);
            nodeToPopulate.Attributes.Append(node: attribute4);
        }

        void AddUIPropertiesToNode(UIObject uiObjectToAdd, XmlNode nodeToPopulate) {
            foreach (var uiPropertyString in UIPropertyStrings)
                try {
                    var attribute = this._xmlDoc.CreateAttribute(name: uiPropertyString.Name);
                    attribute.Value = uiObjectToAdd.GetType().GetProperty(name: uiPropertyString.Name).GetValue(obj: uiObjectToAdd, index: null).ToString();
                    if (uiPropertyString.ShouldLowercase)
                        attribute.Value.ToLower();
                    nodeToPopulate.Attributes.Append(node: attribute);
                } catch (Exception ex) {
                }
        }

        static void AddObjectToDictionaryIfNeeded(
            UIObject uiObjectToAdd,
            string key,
            Dictionary<string, UIObject> objectDictionary) {
            if (objectDictionary == null)
                return;
            if (objectDictionary.ContainsKey(key: key))
                try {
                    Console.WriteLine(value: string.Empty);
                    Console.WriteLine(value: "Exception: Encountered element sharing the same runtimeId " + key);
                    Console.WriteLine(value: string.Empty);
                    Console.WriteLine(format: "    new Object {0}", arg0: uiObjectToAdd);
                    Console.WriteLine(value: "    runtimeId " + uiObjectToAdd.RuntimeId);
                    Console.WriteLine(value: "    className " + uiObjectToAdd.ClassName);
                    Console.WriteLine(value: "    name " + uiObjectToAdd.Name);
                    Console.WriteLine(format: "    NativeWindowHandle {0}", arg0: uiObjectToAdd.NativeWindowHandle);
                    Console.WriteLine(value: string.Empty);
                    var uiObject = objectDictionary[key: key];
                    Console.WriteLine(format: "    existing Object {0}", arg0: uiObject);
                    Console.WriteLine(value: "    runtimeId " + uiObject.RuntimeId);
                    Console.WriteLine(value: "    className " + uiObject.ClassName);
                    Console.WriteLine(value: "    name " + uiObject.Name);
                    Console.WriteLine(format: "    NativeWindowHandle {0}", arg0: uiObject.NativeWindowHandle);
                    Console.WriteLine(value: string.Empty);
                    Console.WriteLine(value: string.Empty);
                    Console.WriteLine(value: "Ignoring new object and keeping existing one");
                    Console.WriteLine(value: string.Empty);
                } catch {
                }
            else
                objectDictionary.Add(key: key, value: uiObjectToAdd);
        }

        void AddSupportedPatterns(UIObject uiObjectToAdd, XmlNode nodeToPopulate) {
            foreach (var supportedPattern in uiObjectToAdd.GetSupportedPatterns())
                if (supportedPattern != null) {
                    if (supportedPattern.ProgrammaticName == DockPatternIdentifiers.Pattern.ProgrammaticName) {
                        IDock dock = new DockImplementation(uiObject: uiObjectToAdd);
                        AddPropertiesAsXmlAttributes(nodeToPopulate: nodeToPopulate, currObject: dock);
                    } else if (supportedPattern.ProgrammaticName == ExpandCollapsePatternIdentifiers.Pattern.ProgrammaticName) {
                        IExpandCollapse expandCollapse = new ExpandCollapseImplementation(uiObject: uiObjectToAdd);
                        AddPropertiesAsXmlAttributes(nodeToPopulate: nodeToPopulate, currObject: expandCollapse);
                    } else if (supportedPattern.ProgrammaticName == RangeValuePatternIdentifiers.Pattern.ProgrammaticName) {
                        IRangeValue rangeValue = new RangeValueImplementation(uiObject: uiObjectToAdd);
                        AddPropertiesAsXmlAttributes(nodeToPopulate: nodeToPopulate, currObject: rangeValue);
                    } else if (supportedPattern.ProgrammaticName == ScrollPatternIdentifiers.Pattern.ProgrammaticName) {
                        IScroll scroll = new ScrollImplementation(uiObject: uiObjectToAdd);
                        AddPropertiesAsXmlAttributes(nodeToPopulate: nodeToPopulate, currObject: scroll);
                    } else if (supportedPattern.ProgrammaticName == TogglePatternIdentifiers.Pattern.ProgrammaticName) {
                        IToggle toggle = new ToggleImplementation(uiObject: uiObjectToAdd);
                        AddPropertiesAsXmlAttributes(nodeToPopulate: nodeToPopulate, currObject: toggle);
                    } else if (supportedPattern.ProgrammaticName == TransformPatternIdentifiers.Pattern.ProgrammaticName) {
                        ITransform transform = new TransformImplementation(uiObject: uiObjectToAdd);
                        AddPropertiesAsXmlAttributes(nodeToPopulate: nodeToPopulate, currObject: transform);
                    } else if (supportedPattern.ProgrammaticName == WindowPatternIdentifiers.Pattern.ProgrammaticName) {
                        IWindow window = new WindowImplementation(uiObject: uiObjectToAdd);
                        AddPropertiesAsXmlAttributes(nodeToPopulate: nodeToPopulate, currObject: window);
                    } else {
                        if (supportedPattern.ProgrammaticName == SelectionItemPattern.Pattern.ProgrammaticName) {
                            ISelectionItem<UIObject> selectionItem = new SelectionItemImplementation<UIObject>(uiObject: uiObjectToAdd, containerFactory: UIObject.Factory);
                            AddPropertiesAsXmlAttributes(nodeToPopulate: nodeToPopulate, currObject: selectionItem);
                            break;
                        }

                        if (supportedPattern.ProgrammaticName == SelectionPattern.Pattern.ProgrammaticName) {
                            var selectionImplementation = new SelectionImplementation<UIObject>(uiObject: uiObjectToAdd, itemFactory: UIObject.Factory);
                            var empty = string.Empty;
                            foreach (var uiObject in selectionImplementation.Selection) {
                                if (empty != string.Empty)
                                    empty += ";";
                                empty += uiObject.RuntimeId;
                            }

                            var attribute = this._xmlDoc.CreateAttribute(name: "Selection");
                            attribute.Value = empty;
                            nodeToPopulate.Attributes.Append(node: attribute);
                        }
                    }
                }
        }

        void AddPropertiesAsXmlAttributes(XmlNode nodeToPopulate, object currObject) {
            foreach (var property in currObject.GetType().GetProperties(bindingAttr: BindingFlags.Instance | BindingFlags.Public)) {
                var attribute = this._xmlDoc.CreateAttribute(name: property.Name);
                try {
                    var obj = property.GetValue(obj: currObject);
                    if (obj != null) {
                        attribute.Value = obj.ToString();
                        nodeToPopulate.Attributes.Append(node: attribute);
                    }
                } catch (TargetInvocationException ex) {
                } catch (TypeInitializationException ex) {
                }
            }
        }

        void AddUIChildrenToXmlNode(
            UIObject rootUIObject,
            XmlNode rootNode,
            Dictionary<string, UIObject> objectDictionary) {
            var staticCollection = rootUIObject.Children.ToStaticCollection();
            var count = staticCollection.Count;
            for (var index = 0; index < count; ++index) {
                var uiObject = staticCollection[index: index];
                var xmlNode = PopulateXmlNodeForUIObject(uiObjectToAdd: uiObject, objectDictionary: objectDictionary);
                if (xmlNode != null) {
                    AddUIChildrenToXmlNode(rootUIObject: uiObject, rootNode: xmlNode, objectDictionary: objectDictionary);
                    if (rootNode != null)
                        rootNode.AppendChild(newChild: xmlNode);
                    else
                        this._xmlDoc.AppendChild(newChild: xmlNode);
                }
            }
        }

        struct UIPropertyString {
            public UIPropertyString(string name, bool shouldLowercase) {
                Name = name;
                ShouldLowercase = shouldLowercase;
            }

            public string Name { get; }

            public bool ShouldLowercase { get; }
        }
    }
}