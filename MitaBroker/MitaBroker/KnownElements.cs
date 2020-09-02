// Decompiled with JetBrains decompiler
// Type: MitaBroker.KnownElements
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Collections.Generic;
using MS.Internal.Mita.Foundation;

namespace MitaBroker {
    internal class KnownElements {
        readonly Dictionary<string, UIObject> knownElements;

        public KnownElements() {
            this.knownElements = new Dictionary<string, UIObject>();
        }

        public void Clear() {
            this.knownElements.Clear();
        }

        public void Add(UIObject element, string elementId) {
            if (!(element != null))
                return;
            if (!this.knownElements.ContainsKey(key: elementId))
                this.knownElements.Add(key: elementId, value: element);
            else if (this.knownElements[key: elementId] != element)
                throw new Exception(message: "Unexpected state on insertion: another element already exists with this key");
        }

        public string Add(UIObject element) {
            var elementId = string.Empty;
            if (element != null) {
                elementId = Utilities.GetElementIdFromElement(element: element);
                Add(element: element, elementId: elementId);
            }

            return elementId;
        }

        public UIObject Get(string elementId) {
            UIObject uiObject = null;
            this.knownElements.TryGetValue(key: elementId, value: out uiObject);
            return uiObject;
        }

        public ResponseStatus GetStatus(string elementId) {
            var responseStatus = ResponseStatus.NoSuchElement;
            var uiObject = Get(elementId: elementId);
            if (uiObject != null) {
                try {
                    if (uiObject.ProcessId > 0)
                        if (uiObject.LocalizedControlType != null)
                            responseStatus = ResponseStatus.Success;
                } catch {
                }

                if (responseStatus != ResponseStatus.Success) {
                    this.knownElements.Remove(key: elementId);
                    responseStatus = ResponseStatus.StaleElementReference;
                }
            }

            return responseStatus;
        }
    }
}