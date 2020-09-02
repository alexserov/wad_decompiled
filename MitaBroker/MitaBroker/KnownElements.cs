// Decompiled with JetBrains decompiler
// Type: MitaBroker.KnownElements
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MS.Internal.Mita.Foundation;
using System;
using System.Collections.Generic;

namespace MitaBroker
{
  internal class KnownElements
  {
    private Dictionary<string, UIObject> knownElements;

    public KnownElements() => this.knownElements = new Dictionary<string, UIObject>();

    public void Clear() => this.knownElements.Clear();

    public void Add(UIObject element, string elementId)
    {
      if (!(element != (UIObject) null))
        return;
      if (!this.knownElements.ContainsKey(elementId))
        this.knownElements.Add(elementId, element);
      else if (this.knownElements[elementId] != element)
        throw new Exception("Unexpected state on insertion: another element already exists with this key");
    }

    public string Add(UIObject element)
    {
      string elementId = string.Empty;
      if (element != (UIObject) null)
      {
        elementId = Utilities.GetElementIdFromElement(element);
        this.Add(element, elementId);
      }
      return elementId;
    }

    public UIObject Get(string elementId)
    {
      UIObject uiObject = (UIObject) null;
      this.knownElements.TryGetValue(elementId, out uiObject);
      return uiObject;
    }

    public ResponseStatus GetStatus(string elementId)
    {
      ResponseStatus responseStatus = ResponseStatus.NoSuchElement;
      UIObject uiObject = this.Get(elementId);
      if (uiObject != (UIObject) null)
      {
        try
        {
          if (uiObject.ProcessId > 0)
          {
            if (uiObject.LocalizedControlType != null)
              responseStatus = ResponseStatus.Success;
          }
        }
        catch
        {
        }
        if (responseStatus != ResponseStatus.Success)
        {
          this.knownElements.Remove(elementId);
          responseStatus = ResponseStatus.StaleElementReference;
        }
      }
      return responseStatus;
    }
  }
}
