// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.ActionsHandler
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using MitaBroker.WebDriver.Actions.Enums;
using MS.Internal.Mita.Foundation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MitaBroker.WebDriver.Actions
{
  internal sealed class ActionsHandler
  {
    private List<InputSource> ActiveInputSources = new List<InputSource>();
    private Dictionary<string, IInputSourceState> InputStateTable = new Dictionary<string, IInputSourceState>();
    private ActionSequence InputCancelList = new ActionSequence();
    private List<PointerInputState> pointerIdRecylcleList;
    private static PointerIdCollections availablePointerIdCollection = new PointerIdCollections();
    private const int interpolationResolutionMs = 50;
    private KnownElements SessionKnownElements;
    private IntPtr SessionTopLevelWindowHandle;

    public void ResetHandlerState()
    {
      this.ActiveInputSources = new List<InputSource>();
      this.InputStateTable = new Dictionary<string, IInputSourceState>();
      this.InputCancelList = new ActionSequence();
    }

    public ActionsHandler(KnownElements sessionKnownElements) => this.SessionKnownElements = sessionKnownElements;

    public void SetSessionTopLevelWindowHandle(IntPtr sessionTopLevelWindowHandle) => this.SessionTopLevelWindowHandle = sessionTopLevelWindowHandle;

    private ActionsByTick ExtractActionSequence(JToken parameters)
    {
      JToken parameter = parameters[(object) "actions"];
      if (parameter == null || !parameter.HasValues || parameter.Type != JTokenType.Array)
        throw new ArgumentException("\"actions\" in JSON payload is undefined or is not an Array");
      ActionsByTick actionsByTick = new ActionsByTick();
      foreach (JToken actionSequence1 in (IEnumerable<JToken>) parameter)
      {
        ActionSequence actionSequence2 = this.ProcessInputSourceActionSequence(actionSequence1);
        foreach (ActionObject actionObject in (List<ActionObject>) actionSequence2)
        {
          int index = actionSequence2.IndexOf(actionObject);
          if (actionsByTick.Count < index + 1)
            actionsByTick.Add(new ActionSequence());
          actionsByTick[index].Add(actionObject);
        }
      }
      return actionsByTick;
    }

    private ActionSequence ProcessInputSourceActionSequence(JToken actionSequence)
    {
      JToken jtoken1 = actionSequence[(object) "type"];
      string str1 = jtoken1 != null && jtoken1.Type == JTokenType.String ? jtoken1.Value<string>() : throw new ArgumentException("\"type\" in JSON payload is undefined or is not the correct type");
      InputSourceType inputSourceType = (InputSourceType) Enum.Parse(typeof (InputSourceType), str1, true);
      switch (inputSourceType)
      {
        case InputSourceType.None:
        case InputSourceType.Key:
        case InputSourceType.Pointer:
          if (inputSourceType == InputSourceType.Key)
            throw new NotImplementedException("Currently key input source type is not supported");
          JToken jtoken2 = actionSequence[(object) "id"];
          string id = jtoken2 != null && jtoken2.Type == JTokenType.String ? jtoken2.Value<string>() : throw new ArgumentException("\"id\" in JSON payload is undefined or is not a String");
          JToken parameters = (JToken) null;
          if (inputSourceType == InputSourceType.Pointer)
            parameters = this.ProcessPointerParameters(actionSequence[(object) "parameters"]);
          InputSource inputSource = this.ActiveInputSources.Find((Predicate<InputSource>) (s => s.Id == id));
          if (inputSource == null)
          {
            IInputSourceState inputSourceState = (IInputSourceState) null;
            switch (inputSourceType)
            {
              case InputSourceType.None:
                inputSource = (InputSource) new NullInputSource(id);
                inputSourceState = (IInputSourceState) new NullInputState();
                break;
              case InputSourceType.Key:
                inputSource = (InputSource) new KeyInputSource(id);
                inputSourceState = (IInputSourceState) new KeyInputState();
                break;
              case InputSourceType.Pointer:
                PointerType pointerType = (PointerType) Enum.Parse(typeof (PointerType), parameters[(object) "pointerType"].Value<string>(), true);
                inputSource = (InputSource) new PointerInputSource(id, pointerType);
                inputSourceState = (IInputSourceState) new PointerInputState(pointerType);
                break;
            }
            this.ActiveInputSources.Add(inputSource);
            this.InputStateTable.Add(id, inputSourceState);
          }
          if (inputSource.Type != inputSourceType)
            throw new ArgumentException("Input source " + id + " " + inputSource.Type.ToString().ToLower() + " source type does not match the given " + str1 + " type in JSON payload");
          if (parameters != null && parameters[(object) "pointerType"] != null)
          {
            string str2 = parameters[(object) "pointerType"].Value<string>();
            PointerType pointerType = (PointerType) Enum.Parse(typeof (PointerType), str2, true);
            if (!(inputSource is PointerInputSource pointerInputSource))
              throw new InternalErrorException("Input source " + id + " is not a PointerInputSource");
            if (pointerInputSource.PointerType != pointerType)
              throw new ArgumentException("Input source " + id + " " + pointerInputSource.PointerType.ToString().ToLower() + " source pointer type does not match the given parameters " + str2 + " pointerType in JSON payload");
          }
          JToken jtoken3 = actionSequence[(object) "actions"];
          if (jtoken3 == null || jtoken3.Type != JTokenType.Array)
            throw new ArgumentException("\"actions\" in actionSequence JSON payload is undefined or is not an Array");
          ActionSequence actionSequence1 = new ActionSequence();
          foreach (JToken actionItem in (IEnumerable<JToken>) jtoken3)
          {
            if (actionItem == null || actionItem.Type != JTokenType.Object)
              throw new ArgumentException("actionItem in \"actions\" array JSON payload is undefined or is not an Object");
            ActionObject actionObject = (ActionObject) null;
            switch (inputSourceType)
            {
              case InputSourceType.None:
                actionObject = this.ProcessNullAction(id, actionItem);
                break;
              case InputSourceType.Key:
                actionObject = this.ProcessKeyAction(id, actionItem);
                break;
              case InputSourceType.Pointer:
                actionObject = this.ProcessPointerAction(id, parameters, actionItem);
                break;
            }
            actionSequence1.Add(actionObject);
          }
          return actionSequence1;
        default:
          throw new ArgumentException("\"type\" in JSON payload is not \"key\", \"pointer\", or \"none\"");
      }
    }

    private JToken ProcessPointerParameters(JToken parametersData)
    {
      JToken jtoken1 = (JToken) JObject.Parse("{\"pointerType\" : \"mouse\"}");
      if (parametersData == null)
        return jtoken1;
      JToken jtoken2 = parametersData.Type == JTokenType.Object ? parametersData[(object) "pointerType"] : throw new ArgumentException("\"parameters\" in JSON payload is not an Object");
      string str = jtoken2 != null && jtoken2.Type == JTokenType.String ? jtoken2.Value<string>() : throw new ArgumentException("\"pointerType\" in JSON payload is undefined or is not a String");
      PointerType pointerType = (PointerType) Enum.Parse(typeof (PointerType), str, true);
      switch (pointerType)
      {
        case PointerType.Mouse:
        case PointerType.Pen:
        case PointerType.Touch:
          if (pointerType != PointerType.Pen && pointerType != PointerType.Touch)
            throw new NotImplementedException("Currently only pen and touch pointer input source types are supported");
          jtoken1[(object) "pointerType"] = (JToken) str;
          return jtoken1;
        default:
          throw new ArgumentException("\"pointerType\" in JSON payload is not \"mouse\", \"pen\", or \"touch\"");
      }
    }

    private ActionObject ProcessNullAction(string id, JToken actionItem)
    {
      JToken jtoken = actionItem[(object) "type"];
      MitaBroker.WebDriver.Actions.Enums.InputActionType inputActionType = jtoken != null && jtoken.Type == JTokenType.String ? (MitaBroker.WebDriver.Actions.Enums.InputActionType) Enum.Parse(typeof (MitaBroker.WebDriver.Actions.Enums.InputActionType), jtoken.Value<string>(), true) : throw new ArgumentException("\"type\" in a null action JSON payload is undefined or is not a String");
      ActionObject action = inputActionType == MitaBroker.WebDriver.Actions.Enums.InputActionType.Pause ? new ActionObject(id, 0, (int) inputActionType) : throw new ArgumentException("\"type\" in a null action JSON payload is not \"pause\"");
      return this.ProcessPauseAction(actionItem, action);
    }

    private ActionObject ProcessKeyAction(string id, JToken actionItem)
    {
      JToken jtoken1 = actionItem[(object) "type"];
      MitaBroker.WebDriver.Actions.Enums.InputActionType inputActionType = jtoken1 != null && jtoken1.Type == JTokenType.String ? (MitaBroker.WebDriver.Actions.Enums.InputActionType) Enum.Parse(typeof (MitaBroker.WebDriver.Actions.Enums.InputActionType), jtoken1.Value<string>(), true) : throw new ArgumentException("\"type\" in a key action JSON payload is undefined or is not a String");
      switch (inputActionType)
      {
        case MitaBroker.WebDriver.Actions.Enums.InputActionType.Pause:
        case MitaBroker.WebDriver.Actions.Enums.InputActionType.KeyDown:
        case MitaBroker.WebDriver.Actions.Enums.InputActionType.KeyUp:
          ActionObject action = new ActionObject(id, 1, (int) inputActionType);
          if (inputActionType == MitaBroker.WebDriver.Actions.Enums.InputActionType.Pause)
            return this.ProcessPauseAction(actionItem, action);
          JToken jtoken2 = actionItem[(object) "value"];
          string str = jtoken2 != null && jtoken2.Type == JTokenType.String ? jtoken2.Value<string>() : throw new ArgumentException("\"value\" in a key action JSON payload is undefined or is not a String");
          if (!str.IsNormalized())
            throw new ArgumentException("\"value\" in a key action JSON payload is not a String containing a single unicode code point");
          action.Set("value", (object) str);
          return action;
        default:
          throw new ArgumentException("\"type\" in a key action JSON payload is not \"keyUp\", \"keyDown\", or \"pause\"");
      }
    }

    private ActionObject ProcessPointerAction(
      string id,
      JToken parameters,
      JToken actionItem)
    {
      JToken jtoken = actionItem[(object) "type"];
      MitaBroker.WebDriver.Actions.Enums.InputActionType inputActionType = jtoken != null && jtoken.Type == JTokenType.String ? (MitaBroker.WebDriver.Actions.Enums.InputActionType) Enum.Parse(typeof (MitaBroker.WebDriver.Actions.Enums.InputActionType), jtoken.Value<string>(), true) : throw new ArgumentException("\"type\" in a pointer action JSON payload is undefined or is not a String");
      switch (inputActionType)
      {
        case MitaBroker.WebDriver.Actions.Enums.InputActionType.Pause:
        case MitaBroker.WebDriver.Actions.Enums.InputActionType.PointerDown:
        case MitaBroker.WebDriver.Actions.Enums.InputActionType.PointerUp:
        case MitaBroker.WebDriver.Actions.Enums.InputActionType.PointerMove:
        case MitaBroker.WebDriver.Actions.Enums.InputActionType.PointerCancel:
          ActionObject action = new ActionObject(id, 2, (int) inputActionType);
          if (inputActionType == MitaBroker.WebDriver.Actions.Enums.InputActionType.Pause)
            return this.ProcessPauseAction(actionItem, action);
          PointerType pointerType = (PointerType) Enum.Parse(typeof (PointerType), parameters[(object) "pointerType"].Value<string>(), true);
          action.Set("pointerType", (object) pointerType);
          if (inputActionType == MitaBroker.WebDriver.Actions.Enums.InputActionType.PointerUp || inputActionType == MitaBroker.WebDriver.Actions.Enums.InputActionType.PointerDown)
            this.ProcessPointerUpOrPointerDownAction(actionItem, action);
          if (inputActionType == MitaBroker.WebDriver.Actions.Enums.InputActionType.PointerMove)
            this.ProcessPointerMoveAction(actionItem, action);
          if (inputActionType == MitaBroker.WebDriver.Actions.Enums.InputActionType.PointerCancel)
            this.ProcessPointerCancelAction(actionItem, action);
          return action;
        default:
          throw new ArgumentException("\"type\" in a pointer action JSON payload is not \"pause\", \"pointerUp\", \"pointerDown\", \"pointerMove\", or \"pointerCancel\"");
      }
    }

    private ActionObject ProcessPauseAction(JToken actionItem, ActionObject action)
    {
      JToken jtoken = actionItem[(object) "duration"];
      if (jtoken != null && (jtoken.Type != JTokenType.Integer || jtoken.Value<int>() < 0))
        throw new ArgumentException("\"duration\" in a null action JSON payload is not an Integer greater than or equal to 0");
      action.Set("duration", (object) jtoken.Value<uint>());
      return action;
    }

    private void ProcessPointerUpOrPointerDownAction(JToken actionItem, ActionObject action)
    {
      JToken jtoken = actionItem[(object) "button"];
      if (jtoken == null || jtoken.Type != JTokenType.Integer || jtoken.Value<int>() < 0)
        throw new ArgumentException("\"button\" in a pointer action JSON payload is undefined or is not an Integer greater than or equal to 0");
      action.Set("button", (object) jtoken.Value<int>());
      this.ProcessExtraPointerParameters(actionItem, action);
    }

    private void ProcessPointerMoveAction(JToken actionItem, ActionObject action)
    {
      JToken jtoken1 = actionItem[(object) "duration"];
      if (jtoken1 != null && (jtoken1.Type != JTokenType.Integer || jtoken1.Value<int>() < 0))
        throw new ArgumentException("\"duration\" in a pointer action JSON payload is not an Integer greater than or equal to 0");
      action.Set("duration", (object) jtoken1.Value<uint>());
      JToken jtoken2 = actionItem[(object) "origin"] ?? (JToken) new JValue("viewport");
      string str = (string) null;
      OriginType? nullable1 = new OriginType?();
      if (jtoken2.Type == JTokenType.Object)
      {
        foreach (JToken child in jtoken2.Children())
        {
          if (child.Type == JTokenType.Property && child is JProperty jproperty && jproperty.Name.Contains("element"))
          {
            str = jproperty.Value.Value<string>();
            nullable1 = new OriginType?(OriginType.Element);
            action.Set("element", (object) str);
            break;
          }
        }
      }
      else if (jtoken2.Type == JTokenType.String)
        nullable1 = new OriginType?((OriginType) Enum.Parse(typeof (OriginType), jtoken2.Value<string>(), true));
      OriginType? nullable2 = nullable1;
      OriginType originType1 = OriginType.Viewport;
      if (!(nullable2.GetValueOrDefault() == originType1 & nullable2.HasValue))
      {
        OriginType? nullable3 = nullable1;
        OriginType originType2 = OriginType.Pointer;
        if (!(nullable3.GetValueOrDefault() == originType2 & nullable3.HasValue) && string.IsNullOrEmpty(str))
          throw new ArgumentException("\"origin\" in a action JSON payload is not equal to \"viewport\" or \"pointer\" and element is not an Object that represents a web element");
      }
      action.Set("origin", (object) nullable1);
      JToken jtoken3 = actionItem[(object) "x"];
      if (jtoken3 == null && jtoken3.Type != JTokenType.Integer)
        throw new ArgumentException("\"x\" in a pointer action JSON payload is not an Integer");
      action.Set("x", (object) jtoken3.Value<int>());
      JToken jtoken4 = actionItem[(object) "y"];
      if (jtoken4 == null && jtoken4.Type != JTokenType.Integer)
        throw new ArgumentException("\"y\" in a pointer action JSON payload is not an Integer");
      action.Set("y", (object) jtoken4.Value<int>());
      this.ProcessExtraPointerParameters(actionItem, action);
    }

    private void ProcessExtraPointerParameters(JToken actionItem, ActionObject action)
    {
      JToken jtoken1 = actionItem[(object) "width"];
      if (jtoken1 != null)
      {
        if (jtoken1.Type != JTokenType.Float || (double) jtoken1.Value<float>() < 1.0)
          throw new ArgumentException("\"width\" attribute is not a floating point value greater or equal to 1");
        action.Set("width", (object) jtoken1.Value<float>());
      }
      JToken jtoken2 = actionItem[(object) "height"];
      if (jtoken2 != null)
      {
        if (jtoken2.Type != JTokenType.Float || (double) jtoken2.Value<float>() < 1.0)
          throw new ArgumentException("\"height\" attribute is not a floating point value greater or equal to 1");
        action.Set("height", (object) jtoken2.Value<float>());
      }
      if (jtoken1 != null && jtoken2 == null || jtoken1 == null && jtoken2 != null)
        throw new ArgumentException("\"width\" and \"height\" attributes need to be specified together");
      JToken jtoken3 = actionItem[(object) "pressure"];
      if (jtoken3 != null)
      {
        if (jtoken3.Type != JTokenType.Float || (double) jtoken3.Value<float>() < 0.0 || (double) jtoken3.Value<float>() > 1.0)
          throw new ArgumentException("\"pressure\" attribute is not a floating point value between 0 and 1");
        action.Set("pressure", (object) jtoken3.Value<float>());
      }
      JToken jtoken4 = actionItem[(object) "tiltX"];
      if (jtoken4 != null)
      {
        if (jtoken4.Type != JTokenType.Integer || jtoken4.Value<int>() < -90 || jtoken4.Value<int>() > 90)
          throw new ArgumentException("\"tiltX\" attribute is not an integer value between -90 and 90");
        action.Set("tiltX", (object) jtoken4.Value<int>());
      }
      JToken jtoken5 = actionItem[(object) "tiltY"];
      if (jtoken5 != null)
      {
        if (jtoken5.Type != JTokenType.Integer || jtoken5.Value<int>() < -90 || jtoken5.Value<int>() > 90)
          throw new ArgumentException("\"tiltY\" attribute is not an integer value between -90 and 90");
        action.Set("tiltY", (object) jtoken5.Value<int>());
      }
      JToken jtoken6 = actionItem[(object) "twist"];
      if (jtoken6 == null)
        return;
      if (jtoken6.Type != JTokenType.Integer || jtoken6.Value<int>() < 0 || jtoken6.Value<int>() >= 360)
        throw new ArgumentException("\"twist\" attribute is not an integer value between 0 and 359");
      action.Set("twist", (object) jtoken6.Value<int>());
    }

    private void ProcessPointerCancelAction(JToken actionItem, ActionObject action) => throw new NotImplementedException("Process a pointer cancel action is not implemented");

    private void DispatchActions(ActionsByTick actionsByTick)
    {
      this.pointerIdRecylcleList = new List<PointerInputState>();
      int xScreen;
      int yScreen;
      PositionAdapter.ConvertClientToScreen(this.SessionTopLevelWindowHandle, 0, 0, out xScreen, out yScreen);
      PointI topLevelWindowPosition = new PointI(xScreen, yScreen);
      foreach (ActionSequence tickActions in (List<ActionSequence>) actionsByTick)
      {
        uint tickDuration = this.ComputeTickDuration(tickActions);
        Stopwatch stopwatch = Stopwatch.StartNew();
        this.DispatchTickActions(tickActions, tickDuration, topLevelWindowPosition);
        stopwatch.Stop();
        if (stopwatch.ElapsedMilliseconds < (long) tickDuration)
          Thread.Sleep(TimeSpan.FromMilliseconds((double) tickDuration).Subtract(stopwatch.Elapsed));
      }
      foreach (PointerInputState pointerIdRecylcle in this.pointerIdRecylcleList)
      {
        if (pointerIdRecylcle.Pressed.Count == 0 && pointerIdRecylcle.PointerId != -1)
        {
          int pointerId = pointerIdRecylcle.PointerId;
          pointerIdRecylcle.PointerId = -1;
          ActionsHandler.availablePointerIdCollection.Recycle(pointerId);
        }
      }
      this.pointerIdRecylcleList.Clear();
    }

    private uint ComputeTickDuration(ActionSequence tickActions)
    {
      uint num1 = 0;
      foreach (ActionObject tickAction in (List<ActionObject>) tickActions)
      {
        uint? nullable1 = new uint?();
        if (tickAction.Subtype == 0 || tickAction.Type == 2 && tickAction.Subtype == 5)
          nullable1 = (uint?) tickAction.Get("duration");
        if (nullable1.HasValue)
        {
          uint? nullable2 = nullable1;
          uint num2 = num1;
          if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
            num1 = nullable1.Value;
        }
      }
      return num1;
    }

    private void DispatchTickActions(
      ActionSequence tickActions,
      uint tickDuration,
      PointI topLevelWindowPosition)
    {
      List<PointerData> tickPenList = new List<PointerData>();
      List<PointerData> tickTouchList = new List<PointerData>();
      List<List<PointerData>> pointerDataListList1 = new List<List<PointerData>>();
      List<List<PointerData>> pointerDataListList2 = new List<List<PointerData>>();
      Stopwatch stopwatch = Stopwatch.StartNew();
      foreach (ActionObject tickAction in (List<ActionObject>) tickActions)
      {
        string id = tickAction.Id;
        InputSourceType type = (InputSourceType) tickAction.Type;
        if (!this.InputStateTable.ContainsKey(id))
        {
          switch (type)
          {
            case InputSourceType.None:
              this.InputStateTable.Add(id, (IInputSourceState) new NullInputState());
              break;
            case InputSourceType.Key:
              this.InputStateTable.Add(id, (IInputSourceState) new KeyInputState());
              break;
            case InputSourceType.Pointer:
              this.InputStateTable.Add(id, (IInputSourceState) new PointerInputState((PointerType) tickAction.Get("pointerType")));
              break;
          }
        }
        IInputSourceState inputSourceState = this.InputStateTable[id];
        switch (tickAction.Subtype)
        {
          case 0:
            this.DispatchPauseAction(id, tickAction, (NullInputState) inputSourceState, tickDuration);
            continue;
          case 1:
            this.DispatchKeyDownAction(id, tickAction, (KeyInputState) inputSourceState, tickDuration);
            continue;
          case 2:
            this.DispatchKeyUpAction(id, tickAction, (KeyInputState) inputSourceState, tickDuration);
            continue;
          case 3:
            this.DispatchPointerDownAction(id, tickAction, (PointerInputState) inputSourceState, tickDuration, topLevelWindowPosition, tickPenList, tickTouchList);
            continue;
          case 4:
            this.DispatchPointerUpAction(id, tickAction, (PointerInputState) inputSourceState, tickDuration, topLevelWindowPosition, tickPenList, tickTouchList);
            continue;
          case 5:
            this.DispatchPointerMoveAction(id, tickAction, (PointerInputState) inputSourceState, tickDuration, topLevelWindowPosition, tickPenList, tickTouchList, pointerDataListList1, pointerDataListList2);
            continue;
          case 6:
            this.DispatchPointerCancelAction(id, tickAction, (PointerInputState) inputSourceState, tickDuration);
            continue;
          default:
            throw new InternalErrorException(string.Format("Action object subtype is not known: {0}", (object) tickAction.Subtype));
        }
      }
      if (tickTouchList.Count > 0)
      {
        using (InputController.Activate(PointerInputType.MultiTouch))
        {
          PointerData[] pointerDataArray = new PointerData[tickTouchList.Count];
          tickTouchList.CopyTo(pointerDataArray);
          PointerInput.InjectPointers(pointerDataArray);
        }
      }
      if (tickPenList.Count > 1)
        throw new ArgumentException("Currently only a single (non-concurrent) pen input is supported");
      if (tickPenList.Count > 0)
      {
        using (InputController.Activate(PointerInputType.Pen))
        {
          PointerData[] pointerDataArray = new PointerData[tickPenList.Count];
          tickPenList.CopyTo(pointerDataArray);
          PointerInput.InjectPointers(pointerDataArray);
        }
      }
      if (pointerDataListList1.Count <= 0 && pointerDataListList2.Count <= 0)
        return;
      this.PerformInterpolationFrames(pointerDataListList1, pointerDataListList2, stopwatch.ElapsedMilliseconds);
    }

    private void PerformInterpolationFrames(
      List<List<PointerData>> tickPenInterpolationFrames,
      List<List<PointerData>> tickTouchInterpolationFrames,
      long tickElapsedMs)
    {
      Stopwatch stopwatch = Stopwatch.StartNew();
      int index1 = 0;
      int index2 = 0;
      while (index1 < tickPenInterpolationFrames.Count || index2 < tickTouchInterpolationFrames.Count)
      {
        if (index1 < tickPenInterpolationFrames.Count)
        {
          using (InputController.Activate(PointerInputType.Pen))
          {
            PointerData[] pointerDataArray = new PointerData[tickPenInterpolationFrames[index1].Count];
            tickPenInterpolationFrames[index1].CopyTo(pointerDataArray);
            PointerInput.InjectPointers(pointerDataArray);
          }
          ++index1;
        }
        if (index2 < tickTouchInterpolationFrames.Count)
        {
          using (InputController.Activate(PointerInputType.MultiTouch))
          {
            PointerData[] pointerDataArray = new PointerData[tickTouchInterpolationFrames[index2].Count];
            tickTouchInterpolationFrames[index2].CopyTo(pointerDataArray);
            PointerInput.InjectPointers(pointerDataArray);
          }
          ++index2;
        }
        if (stopwatch.ElapsedMilliseconds + tickElapsedMs < 50L)
          Thread.Sleep((int) (50L - tickElapsedMs - stopwatch.ElapsedMilliseconds));
        stopwatch.Restart();
        tickElapsedMs = 0L;
      }
    }

    private void DispatchPauseAction(
      string sourceId,
      ActionObject actionObject,
      NullInputState inputState,
      uint tickDuration)
    {
    }

    private void DispatchKeyDownAction(
      string sourceId,
      ActionObject actionObject,
      KeyInputState inputState,
      uint tickDuration)
    {
      string str = (string) actionObject.Get("value");
      string normalisedKey = Keys.GetNormalisedKey(str);
      inputState.Pressed.Contains(normalisedKey);
      Keys.GetShiftedCharacterCode(str);
      Keys.GetKeyLocation(str);
      if (normalisedKey == "Alt")
        inputState.Alt = true;
      if (normalisedKey == "Shift")
        inputState.Shift = true;
      if (normalisedKey == "Control")
        inputState.Ctrl = true;
      if (normalisedKey == "Meta")
        inputState.Meta = true;
      inputState.Pressed.Add(normalisedKey);
      string text = MitaBroker.KeyboardInput.Process(str);
      if (text.Length <= 0)
        return;
      TextInput.SendText(text);
    }

    private void DispatchKeyUpAction(
      string sourceId,
      ActionObject actionObject,
      KeyInputState inputState,
      uint tickDuration)
    {
      string str = (string) actionObject.Get("value");
      string normalisedKey = Keys.GetNormalisedKey(str);
      if (!inputState.Pressed.Contains(normalisedKey))
        return;
      Keys.GetShiftedCharacterCode(str);
      Keys.GetKeyLocation(str);
      if (normalisedKey == "Alt")
        inputState.Alt = false;
      if (normalisedKey == "Shift")
        inputState.Shift = false;
      if (normalisedKey == "Control")
        inputState.Ctrl = false;
      if (normalisedKey == "Meta")
        inputState.Meta = false;
      inputState.Pressed.Remove(normalisedKey);
      string text = MitaBroker.KeyboardInput.Process(str);
      if (text.Length <= 0)
        return;
      TextInput.SendText(text);
    }

    private void DispatchPointerDownAction(
      string sourceId,
      ActionObject actionObject,
      PointerInputState inputState,
      uint tickDuration,
      PointI topLevelWindowPosition,
      List<PointerData> tickPenList,
      List<PointerData> tickTouchList)
    {
      PointerType pointerType = (PointerType) actionObject.Get("pointerType");
      int num = (int) actionObject.Get("button");
      if (inputState.Pressed.Contains(num))
        return;
      int x = inputState.X;
      int y = inputState.Y;
      inputState.Pressed.Add(num);
      List<int> pressed = inputState.Pressed;
      ActionObject actionObject1 = actionObject.Copy();
      actionObject1.Subtype = 4;
      this.InputCancelList.Add(actionObject1);
      switch (pointerType)
      {
        case PointerType.Pen:
        case PointerType.Touch:
          int xScreen;
          int yScreen;
          this.ConvertClientToScreen(topLevelWindowPosition, x, y, out xScreen, out yScreen);
          if (inputState.PointerId == -1)
            inputState.PointerId = ActionsHandler.availablePointerIdCollection.Get();
          PointerData pointerData = new PointerData()
          {
            location = new PointI(xScreen, yScreen),
            flags = POINTER_FLAGS.ContactDown,
            pointerId = (uint) inputState.PointerId
          };
          this.UpdateInputStateWithExtraPointerParameters(actionObject, inputState);
          this.PopulatePointerDataWithExtraPointerParameters(inputState, ref pointerData);
          if (pointerType == PointerType.Pen)
          {
            tickPenList.Add(pointerData);
            break;
          }
          if (pointerType != PointerType.Touch)
            break;
          tickTouchList.Add(pointerData);
          break;
        default:
          throw new InternalErrorException(string.Format("Unsupported pointer type: {0}", (object) pointerType));
      }
    }

    private void DispatchPointerUpAction(
      string sourceId,
      ActionObject actionObject,
      PointerInputState inputState,
      uint tickDuration,
      PointI topLevelWindowPosition,
      List<PointerData> tickPenList,
      List<PointerData> tickTouchList)
    {
      PointerType pointerType = (PointerType) actionObject.Get("pointerType");
      int num = (int) actionObject.Get("button");
      if (!inputState.Pressed.Contains(num))
        return;
      int x = inputState.X;
      int y = inputState.Y;
      inputState.Pressed.Remove(num);
      List<int> pressed = inputState.Pressed;
      switch (pointerType)
      {
        case PointerType.Pen:
        case PointerType.Touch:
          int xScreen;
          int yScreen;
          this.ConvertClientToScreen(topLevelWindowPosition, x, y, out xScreen, out yScreen);
          if (inputState.PointerId == -1)
            throw new InternalErrorException("Pointer Up Action takes place on input state with uninitialized pointer id");
          PointerData pointerData = new PointerData()
          {
            location = new PointI(xScreen, yScreen),
            flags = POINTER_FLAGS.UP,
            pointerId = (uint) inputState.PointerId
          };
          this.pointerIdRecylcleList.Add(inputState);
          this.UpdateInputStateWithExtraPointerParameters(actionObject, inputState);
          this.PopulatePointerDataWithExtraPointerParameters(inputState, ref pointerData);
          if (pointerType == PointerType.Pen)
          {
            tickPenList.Add(pointerData);
            break;
          }
          if (pointerType != PointerType.Touch)
            break;
          tickTouchList.Add(pointerData);
          break;
        default:
          throw new InternalErrorException(string.Format("Unsupported pointer type: {0}", (object) pointerType));
      }
    }

    private void DispatchPointerMoveAction(
      string sourceId,
      ActionObject actionObject,
      PointerInputState inputState,
      uint tickDuration,
      PointI topLevelWindowPosition,
      List<PointerData> tickPenList,
      List<PointerData> tickTouchList,
      List<List<PointerData>> tickPenInterpolation,
      List<List<PointerData>> tickTouchInterpolation)
    {
      int num1 = (int) actionObject.Get("x");
      int num2 = (int) actionObject.Get("y");
      int x = inputState.X;
      int y = inputState.Y;
      OriginType originType = (OriginType) actionObject.Get("origin");
      int targetX = 0;
      int targetY = 0;
      switch (originType)
      {
        case OriginType.Viewport:
          targetX = num1;
          targetY = num2;
          break;
        case OriginType.Pointer:
          targetX = x + num1;
          targetY = y + num2;
          break;
        case OriginType.Element:
          string elementId = (string) actionObject.Get("element");
          switch (this.SessionKnownElements.GetStatus(elementId))
          {
            case ResponseStatus.NoSuchElement:
              throw new NoSuchElementException("Element " + elementId + " specified in the Actions origin is unknown or does not exist");
            case ResponseStatus.StaleElementReference:
              throw new NoSuchElementException("Element " + elementId + " specified in the Actions origin is no longer valid");
            default:
              UIObject uiObject = this.SessionKnownElements.Get(elementId);
              if (uiObject == (UIObject) null)
                throw new InternalErrorException("Internal error is encountered when retrieving element " + elementId + " coordinates specified in the Actions origin");
              IntPtr levelWindowHandle = this.SessionTopLevelWindowHandle;
              PointI rectangleTopLeft = uiObject.GetBoundingRectangleTopLeft(this.SessionTopLevelWindowHandle);
              SizeI size = uiObject.GetAdjustedBoundingRectangle().Size;
              int num3 = rectangleTopLeft.X + size.Width / 2;
              int num4 = rectangleTopLeft.Y + size.Height / 2;
              targetX = num3 + num1;
              int num5 = num2;
              targetY = num4 + num5;
              break;
          }
          break;
      }
      uint duration = ((uint?) actionObject.Get("duration")).HasValue ? (uint) actionObject.Get("duration") : tickDuration;
      this.UpdateInputStateWithExtraPointerParameters(actionObject, inputState);
      this.PerformPointerMoveAction(sourceId, inputState, duration, x, y, targetX, targetY, topLevelWindowPosition, tickPenList, tickTouchList, tickPenInterpolation, tickTouchInterpolation);
    }

    private void PerformPointerMoveAction(
      string sourceId,
      PointerInputState inputState,
      uint duration,
      int startX,
      int startY,
      int targetX,
      int targetY,
      PointI topLevelWindowPosition,
      List<PointerData> tickPenList,
      List<PointerData> tickTouchList,
      List<List<PointerData>> tickPenInterpolation,
      List<List<PointerData>> tickTouchInterpolation)
    {
      PointerType subtype = inputState.Subtype;
      int xClient = duration >= 50U ? startX : targetX;
      int yClient = duration >= 50U ? startY : targetY;
      int x = inputState.X;
      int y = inputState.Y;
      List<int> pressed = inputState.Pressed;
      switch (subtype)
      {
        case PointerType.Mouse:
        case PointerType.Wheel:
          throw new NotImplementedException();
        case PointerType.Pen:
        case PointerType.Touch:
          int xScreen;
          int yScreen;
          this.ConvertClientToScreen(topLevelWindowPosition, xClient, yClient, out xScreen, out yScreen);
          if (inputState.PointerId == -1)
            inputState.PointerId = ActionsHandler.availablePointerIdCollection.Get();
          PointerData pointerData = new PointerData()
          {
            location = new PointI(xScreen, yScreen),
            flags = POINTER_FLAGS.ContactMoves,
            pointerId = (uint) inputState.PointerId
          };
          this.PopulatePointerDataWithExtraPointerParameters(inputState, ref pointerData);
          if (inputState.Pressed.Count > 0)
          {
            switch (subtype)
            {
              case PointerType.Pen:
                tickPenList.Add(pointerData);
                break;
              case PointerType.Touch:
                tickTouchList.Add(pointerData);
                break;
            }
            if (duration > 50U)
            {
              int startX1 = startX + topLevelWindowPosition.X;
              int startY1 = startY + topLevelWindowPosition.Y;
              int targetX1 = targetX + topLevelWindowPosition.X;
              int targetY1 = targetY + topLevelWindowPosition.Y;
              switch (subtype)
              {
                case PointerType.Pen:
                  this.GenerateInterpolationFrames(duration, inputState.PointerId, startX1, startY1, targetX1, targetY1, tickPenInterpolation);
                  break;
                case PointerType.Touch:
                  this.GenerateInterpolationFrames(duration, inputState.PointerId, startX1, startY1, targetX1, targetY1, tickTouchInterpolation);
                  break;
              }
              xClient = targetX;
              yClient = targetY;
            }
          }
          inputState.X = xClient;
          inputState.Y = yClient;
          break;
        default:
          throw new NotSupportedException();
      }
    }

    private void DispatchPointerCancelAction(
      string sourceId,
      ActionObject actionObject,
      PointerInputState inputState,
      uint tickDuration) => throw new NotImplementedException();

    private void UpdateInputStateWithExtraPointerParameters(
      ActionObject actionObject,
      PointerInputState inputState)
    {
      inputState.Pressure = actionObject.Contains("pressure") ? (float?) actionObject.Get("pressure") : inputState.Pressure;
      inputState.Twist = actionObject.Contains("twist") ? (int?) actionObject.Get("twist") : inputState.Twist;
      inputState.TiltX = actionObject.Contains("tiltX") ? (int?) actionObject.Get("tiltX") : inputState.TiltX;
      inputState.TiltY = actionObject.Contains("tiltY") ? (int?) actionObject.Get("tiltY") : inputState.TiltY;
      PointerInputState pointerInputState1 = inputState;
      float? nullable1;
      double? nullable2;
      if (!actionObject.Contains("width"))
      {
        nullable2 = inputState.Width;
      }
      else
      {
        nullable1 = (float?) actionObject.Get("width");
        nullable2 = nullable1.HasValue ? new double?((double) nullable1.GetValueOrDefault()) : new double?();
      }
      pointerInputState1.Width = nullable2;
      PointerInputState pointerInputState2 = inputState;
      double? nullable3;
      if (!actionObject.Contains("height"))
      {
        nullable3 = inputState.Height;
      }
      else
      {
        nullable1 = (float?) actionObject.Get("height");
        nullable3 = nullable1.HasValue ? new double?((double) nullable1.GetValueOrDefault()) : new double?();
      }
      pointerInputState2.Height = nullable3;
    }

    private void PopulatePointerDataWithExtraPointerParameters(
      PointerInputState inputState,
      ref PointerData pointerData)
    {
      ref PointerData local1 = ref pointerData;
      uint? nullable1;
      if (!inputState.Pressure.HasValue)
      {
        nullable1 = new uint?();
      }
      else
      {
        float? pressure = inputState.Pressure;
        float num = 1024f;
        nullable1 = pressure.HasValue ? new uint?((uint) ((double) pressure.GetValueOrDefault() * (double) num)) : new uint?();
      }
      local1.pressure = nullable1;
      ref PointerData local2 = ref pointerData;
      int? nullable2;
      uint? nullable3;
      if (!inputState.Twist.HasValue)
      {
        nullable3 = new uint?();
      }
      else
      {
        nullable2 = inputState.Twist;
        nullable3 = nullable2.HasValue ? new uint?((uint) nullable2.GetValueOrDefault()) : new uint?();
      }
      local2.twist = nullable3;
      ref PointerData local3 = ref pointerData;
      nullable2 = inputState.TiltX;
      int? nullable4;
      if (!nullable2.HasValue)
      {
        nullable2 = new int?();
        nullable4 = nullable2;
      }
      else
        nullable4 = inputState.TiltX;
      local3.tiltX = nullable4;
      ref PointerData local4 = ref pointerData;
      nullable2 = inputState.TiltY;
      int? nullable5;
      if (!nullable2.HasValue)
      {
        nullable2 = new int?();
        nullable5 = nullable2;
      }
      else
        nullable5 = inputState.TiltY;
      local4.tiltY = nullable5;
      ref PointerData local5 = ref pointerData;
      double? nullable6;
      int? nullable7;
      if (!inputState.Width.HasValue)
      {
        nullable2 = new int?();
        nullable7 = nullable2;
      }
      else
      {
        nullable6 = inputState.Width;
        if (!nullable6.HasValue)
        {
          nullable2 = new int?();
          nullable7 = nullable2;
        }
        else
          nullable7 = new int?((int) nullable6.GetValueOrDefault());
      }
      local5.width = nullable7;
      ref PointerData local6 = ref pointerData;
      nullable6 = inputState.Height;
      int? nullable8;
      if (!nullable6.HasValue)
      {
        nullable2 = new int?();
        nullable8 = nullable2;
      }
      else
      {
        nullable6 = inputState.Height;
        if (!nullable6.HasValue)
        {
          nullable2 = new int?();
          nullable8 = nullable2;
        }
        else
          nullable8 = new int?((int) nullable6.GetValueOrDefault());
      }
      local6.height = nullable8;
      if (inputState.Pressed.Contains(5))
        pointerData.pressedButton |= POINTER_PRESSED_BUTTON.INVERTED | POINTER_PRESSED_BUTTON.ERASER;
      if (!inputState.Pressed.Contains(2))
        return;
      pointerData.pressedButton |= POINTER_PRESSED_BUTTON.BARREL;
    }

    private void GenerateInterpolationFrames(
      uint duration,
      int inputPointerId,
      int startX,
      int startY,
      int targetX,
      int targetY,
      List<List<PointerData>> tickPointerInputInterpolationFrames)
    {
      float num1 = 50f;
      float num2 = duration > 0U ? num1 / (float) duration : 1f;
      int num3 = (int) ((double) duration / (double) num1);
      for (int index = 0; index < num3; ++index)
      {
        if (tickPointerInputInterpolationFrames.Count < index + 1)
          tickPointerInputInterpolationFrames.Add(new List<PointerData>());
        int x = (int) Math.Round((double) (index + 1) * (double) num2 * (double) (targetX - startX)) + startX;
        int y = (int) Math.Round((double) (index + 1) * (double) num2 * (double) (targetY - startY)) + startY;
        PointerData pointerData = new PointerData()
        {
          location = new PointI(x, y),
          flags = POINTER_FLAGS.ContactMoves,
          pointerId = (uint) inputPointerId
        };
        tickPointerInputInterpolationFrames[index].Add(pointerData);
      }
    }

    private void ConvertClientToScreen(
      PointI clientTopLeftPosition,
      int xClient,
      int yClient,
      out int xScreen,
      out int yScreen)
    {
      xScreen = xClient + clientTopLeftPosition.X;
      yScreen = yClient + clientTopLeftPosition.Y;
    }

    internal void PerformActions(string ActionsJSONString) => this.DispatchActions(this.ExtractActionSequence(JToken.Parse(ActionsJSONString)));

    internal void ReleaseActions()
    {
      ActionSequence tickActions = new ActionSequence((IEnumerable<ActionObject>) this.InputCancelList);
      tickActions.Reverse();
      this.DispatchTickActions(tickActions, 0U, new PointI());
      this.InputCancelList.Clear();
      this.InputStateTable.Clear();
      this.ActiveInputSources.Clear();
    }
  }
}
