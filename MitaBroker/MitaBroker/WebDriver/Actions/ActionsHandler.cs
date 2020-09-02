// Decompiled with JetBrains decompiler
// Type: MitaBroker.WebDriver.Actions.ActionsHandler
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using MitaBroker.WebDriver.Actions.Enums;
using MS.Internal.Mita.Foundation;
using Newtonsoft.Json.Linq;

namespace MitaBroker.WebDriver.Actions {
    internal sealed class ActionsHandler {
        const int interpolationResolutionMs = 50;
        static readonly PointerIdCollections availablePointerIdCollection = new PointerIdCollections();
        List<InputSource> ActiveInputSources = new List<InputSource>();
        ActionSequence InputCancelList = new ActionSequence();
        Dictionary<string, IInputSourceState> InputStateTable = new Dictionary<string, IInputSourceState>();
        List<PointerInputState> pointerIdRecylcleList;
        readonly KnownElements SessionKnownElements;
        IntPtr SessionTopLevelWindowHandle;

        public ActionsHandler(KnownElements sessionKnownElements) {
            this.SessionKnownElements = sessionKnownElements;
        }

        public void ResetHandlerState() {
            this.ActiveInputSources = new List<InputSource>();
            this.InputStateTable = new Dictionary<string, IInputSourceState>();
            this.InputCancelList = new ActionSequence();
        }

        public void SetSessionTopLevelWindowHandle(IntPtr sessionTopLevelWindowHandle) {
            this.SessionTopLevelWindowHandle = sessionTopLevelWindowHandle;
        }

        ActionsByTick ExtractActionSequence(JToken parameters) {
            var parameter = parameters[key: "actions"];
            if (parameter == null || !parameter.HasValues || parameter.Type != JTokenType.Array)
                throw new ArgumentException(message: "\"actions\" in JSON payload is undefined or is not an Array");
            var actionsByTick = new ActionsByTick();
            foreach (var actionSequence1 in parameter) {
                var actionSequence2 = ProcessInputSourceActionSequence(actionSequence: actionSequence1);
                foreach (var actionObject in actionSequence2) {
                    var index = actionSequence2.IndexOf(item: actionObject);
                    if (actionsByTick.Count < index + 1)
                        actionsByTick.Add(item: new ActionSequence());
                    actionsByTick[index: index].Add(item: actionObject);
                }
            }

            return actionsByTick;
        }

        ActionSequence ProcessInputSourceActionSequence(JToken actionSequence) {
            var jtoken1 = actionSequence[key: "type"];
            var str1 = jtoken1 != null && jtoken1.Type == JTokenType.String ? jtoken1.Value<string>() : throw new ArgumentException(message: "\"type\" in JSON payload is undefined or is not the correct type");
            var inputSourceType = (InputSourceType) Enum.Parse(enumType: typeof(InputSourceType), value: str1, ignoreCase: true);
            switch (inputSourceType) {
                case InputSourceType.None:
                case InputSourceType.Key:
                case InputSourceType.Pointer:
                    if (inputSourceType == InputSourceType.Key)
                        throw new NotImplementedException(message: "Currently key input source type is not supported");
                    var jtoken2 = actionSequence[key: "id"];
                    var id = jtoken2 != null && jtoken2.Type == JTokenType.String ? jtoken2.Value<string>() : throw new ArgumentException(message: "\"id\" in JSON payload is undefined or is not a String");
                    JToken parameters = null;
                    if (inputSourceType == InputSourceType.Pointer)
                        parameters = ProcessPointerParameters(parametersData: actionSequence[key: "parameters"]);
                    var inputSource = this.ActiveInputSources.Find(match: s => s.Id == id);
                    if (inputSource == null) {
                        IInputSourceState inputSourceState = null;
                        switch (inputSourceType) {
                            case InputSourceType.None:
                                inputSource = new NullInputSource(id: id);
                                inputSourceState = new NullInputState();
                                break;
                            case InputSourceType.Key:
                                inputSource = new KeyInputSource(id: id);
                                inputSourceState = new KeyInputState();
                                break;
                            case InputSourceType.Pointer:
                                var pointerType = (PointerType) Enum.Parse(enumType: typeof(PointerType), value: parameters[key: "pointerType"].Value<string>(), ignoreCase: true);
                                inputSource = new PointerInputSource(id: id, pointerType: pointerType);
                                inputSourceState = new PointerInputState(subtype: pointerType);
                                break;
                        }

                        this.ActiveInputSources.Add(item: inputSource);
                        this.InputStateTable.Add(key: id, value: inputSourceState);
                    }

                    if (inputSource.Type != inputSourceType)
                        throw new ArgumentException(message: "Input source " + id + " " + inputSource.Type.ToString().ToLower() + " source type does not match the given " + str1 + " type in JSON payload");
                    if (parameters != null && parameters[key: "pointerType"] != null) {
                        var str2 = parameters[key: "pointerType"].Value<string>();
                        var pointerType = (PointerType) Enum.Parse(enumType: typeof(PointerType), value: str2, ignoreCase: true);
                        if (!(inputSource is PointerInputSource pointerInputSource))
                            throw new InternalErrorException(message: "Input source " + id + " is not a PointerInputSource");
                        if (pointerInputSource.PointerType != pointerType)
                            throw new ArgumentException(message: "Input source " + id + " " + pointerInputSource.PointerType.ToString().ToLower() + " source pointer type does not match the given parameters " + str2 + " pointerType in JSON payload");
                    }

                    var jtoken3 = actionSequence[key: "actions"];
                    if (jtoken3 == null || jtoken3.Type != JTokenType.Array)
                        throw new ArgumentException(message: "\"actions\" in actionSequence JSON payload is undefined or is not an Array");
                    var actionSequence1 = new ActionSequence();
                    foreach (var actionItem in jtoken3) {
                        if (actionItem == null || actionItem.Type != JTokenType.Object)
                            throw new ArgumentException(message: "actionItem in \"actions\" array JSON payload is undefined or is not an Object");
                        ActionObject actionObject = null;
                        switch (inputSourceType) {
                            case InputSourceType.None:
                                actionObject = ProcessNullAction(id: id, actionItem: actionItem);
                                break;
                            case InputSourceType.Key:
                                actionObject = ProcessKeyAction(id: id, actionItem: actionItem);
                                break;
                            case InputSourceType.Pointer:
                                actionObject = ProcessPointerAction(id: id, parameters: parameters, actionItem: actionItem);
                                break;
                        }

                        actionSequence1.Add(item: actionObject);
                    }

                    return actionSequence1;
                default:
                    throw new ArgumentException(message: "\"type\" in JSON payload is not \"key\", \"pointer\", or \"none\"");
            }
        }

        JToken ProcessPointerParameters(JToken parametersData) {
            JToken jtoken1 = JObject.Parse(json: "{\"pointerType\" : \"mouse\"}");
            if (parametersData == null)
                return jtoken1;
            var jtoken2 = parametersData.Type == JTokenType.Object ? parametersData[key: "pointerType"] : throw new ArgumentException(message: "\"parameters\" in JSON payload is not an Object");
            var str = jtoken2 != null && jtoken2.Type == JTokenType.String ? jtoken2.Value<string>() : throw new ArgumentException(message: "\"pointerType\" in JSON payload is undefined or is not a String");
            var pointerType = (PointerType) Enum.Parse(enumType: typeof(PointerType), value: str, ignoreCase: true);
            switch (pointerType) {
                case PointerType.Mouse:
                case PointerType.Pen:
                case PointerType.Touch:
                    if (pointerType != PointerType.Pen && pointerType != PointerType.Touch)
                        throw new NotImplementedException(message: "Currently only pen and touch pointer input source types are supported");
                    jtoken1[key: "pointerType"] = str;
                    return jtoken1;
                default:
                    throw new ArgumentException(message: "\"pointerType\" in JSON payload is not \"mouse\", \"pen\", or \"touch\"");
            }
        }

        ActionObject ProcessNullAction(string id, JToken actionItem) {
            var jtoken = actionItem[key: "type"];
            var inputActionType = jtoken != null && jtoken.Type == JTokenType.String ? (InputActionType) Enum.Parse(enumType: typeof(InputActionType), value: jtoken.Value<string>(), ignoreCase: true) : throw new ArgumentException(message: "\"type\" in a null action JSON payload is undefined or is not a String");
            var action = inputActionType == InputActionType.Pause ? new ActionObject(id: id, type: 0, subtype: (int) inputActionType) : throw new ArgumentException(message: "\"type\" in a null action JSON payload is not \"pause\"");
            return ProcessPauseAction(actionItem: actionItem, action: action);
        }

        ActionObject ProcessKeyAction(string id, JToken actionItem) {
            var jtoken1 = actionItem[key: "type"];
            var inputActionType = jtoken1 != null && jtoken1.Type == JTokenType.String ? (InputActionType) Enum.Parse(enumType: typeof(InputActionType), value: jtoken1.Value<string>(), ignoreCase: true) : throw new ArgumentException(message: "\"type\" in a key action JSON payload is undefined or is not a String");
            switch (inputActionType) {
                case InputActionType.Pause:
                case InputActionType.KeyDown:
                case InputActionType.KeyUp:
                    var action = new ActionObject(id: id, type: 1, subtype: (int) inputActionType);
                    if (inputActionType == InputActionType.Pause)
                        return ProcessPauseAction(actionItem: actionItem, action: action);
                    var jtoken2 = actionItem[key: "value"];
                    var str = jtoken2 != null && jtoken2.Type == JTokenType.String ? jtoken2.Value<string>() : throw new ArgumentException(message: "\"value\" in a key action JSON payload is undefined or is not a String");
                    if (!str.IsNormalized())
                        throw new ArgumentException(message: "\"value\" in a key action JSON payload is not a String containing a single unicode code point");
                    action.Set(key: "value", value: str);
                    return action;
                default:
                    throw new ArgumentException(message: "\"type\" in a key action JSON payload is not \"keyUp\", \"keyDown\", or \"pause\"");
            }
        }

        ActionObject ProcessPointerAction(
            string id,
            JToken parameters,
            JToken actionItem) {
            var jtoken = actionItem[key: "type"];
            var inputActionType = jtoken != null && jtoken.Type == JTokenType.String ? (InputActionType) Enum.Parse(enumType: typeof(InputActionType), value: jtoken.Value<string>(), ignoreCase: true) : throw new ArgumentException(message: "\"type\" in a pointer action JSON payload is undefined or is not a String");
            switch (inputActionType) {
                case InputActionType.Pause:
                case InputActionType.PointerDown:
                case InputActionType.PointerUp:
                case InputActionType.PointerMove:
                case InputActionType.PointerCancel:
                    var action = new ActionObject(id: id, type: 2, subtype: (int) inputActionType);
                    if (inputActionType == InputActionType.Pause)
                        return ProcessPauseAction(actionItem: actionItem, action: action);
                    var pointerType = (PointerType) Enum.Parse(enumType: typeof(PointerType), value: parameters[key: "pointerType"].Value<string>(), ignoreCase: true);
                    action.Set(key: "pointerType", value: pointerType);
                    if (inputActionType == InputActionType.PointerUp || inputActionType == InputActionType.PointerDown)
                        ProcessPointerUpOrPointerDownAction(actionItem: actionItem, action: action);
                    if (inputActionType == InputActionType.PointerMove)
                        ProcessPointerMoveAction(actionItem: actionItem, action: action);
                    if (inputActionType == InputActionType.PointerCancel)
                        ProcessPointerCancelAction(actionItem: actionItem, action: action);
                    return action;
                default:
                    throw new ArgumentException(message: "\"type\" in a pointer action JSON payload is not \"pause\", \"pointerUp\", \"pointerDown\", \"pointerMove\", or \"pointerCancel\"");
            }
        }

        ActionObject ProcessPauseAction(JToken actionItem, ActionObject action) {
            var jtoken = actionItem[key: "duration"];
            if (jtoken != null && (jtoken.Type != JTokenType.Integer || jtoken.Value<int>() < 0))
                throw new ArgumentException(message: "\"duration\" in a null action JSON payload is not an Integer greater than or equal to 0");
            action.Set(key: "duration", value: jtoken.Value<uint>());
            return action;
        }

        void ProcessPointerUpOrPointerDownAction(JToken actionItem, ActionObject action) {
            var jtoken = actionItem[key: "button"];
            if (jtoken == null || jtoken.Type != JTokenType.Integer || jtoken.Value<int>() < 0)
                throw new ArgumentException(message: "\"button\" in a pointer action JSON payload is undefined or is not an Integer greater than or equal to 0");
            action.Set(key: "button", value: jtoken.Value<int>());
            ProcessExtraPointerParameters(actionItem: actionItem, action: action);
        }

        void ProcessPointerMoveAction(JToken actionItem, ActionObject action) {
            var jtoken1 = actionItem[key: "duration"];
            if (jtoken1 != null && (jtoken1.Type != JTokenType.Integer || jtoken1.Value<int>() < 0))
                throw new ArgumentException(message: "\"duration\" in a pointer action JSON payload is not an Integer greater than or equal to 0");
            action.Set(key: "duration", value: jtoken1.Value<uint>());
            var jtoken2 = actionItem[key: "origin"] ?? new JValue(value: "viewport");
            string str = null;
            var nullable1 = new OriginType?();
            if (jtoken2.Type == JTokenType.Object) {
                foreach (var child in jtoken2.Children())
                    if (child.Type == JTokenType.Property && child is JProperty jproperty && jproperty.Name.Contains(value: "element")) {
                        str = jproperty.Value.Value<string>();
                        nullable1 = OriginType.Element;
                        action.Set(key: "element", value: str);
                        break;
                    }
            } else if (jtoken2.Type == JTokenType.String) {
                nullable1 = (OriginType) Enum.Parse(enumType: typeof(OriginType), value: jtoken2.Value<string>(), ignoreCase: true);
            }

            var nullable2 = nullable1;
            var originType1 = OriginType.Viewport;
            if (!((nullable2.GetValueOrDefault() == originType1) & nullable2.HasValue)) {
                var nullable3 = nullable1;
                var originType2 = OriginType.Pointer;
                if (!((nullable3.GetValueOrDefault() == originType2) & nullable3.HasValue) && string.IsNullOrEmpty(value: str))
                    throw new ArgumentException(message: "\"origin\" in a action JSON payload is not equal to \"viewport\" or \"pointer\" and element is not an Object that represents a web element");
            }

            action.Set(key: "origin", value: nullable1);
            var jtoken3 = actionItem[key: "x"];
            if (jtoken3 == null && jtoken3.Type != JTokenType.Integer)
                throw new ArgumentException(message: "\"x\" in a pointer action JSON payload is not an Integer");
            action.Set(key: "x", value: jtoken3.Value<int>());
            var jtoken4 = actionItem[key: "y"];
            if (jtoken4 == null && jtoken4.Type != JTokenType.Integer)
                throw new ArgumentException(message: "\"y\" in a pointer action JSON payload is not an Integer");
            action.Set(key: "y", value: jtoken4.Value<int>());
            ProcessExtraPointerParameters(actionItem: actionItem, action: action);
        }

        void ProcessExtraPointerParameters(JToken actionItem, ActionObject action) {
            var jtoken1 = actionItem[key: "width"];
            if (jtoken1 != null) {
                if (jtoken1.Type != JTokenType.Float || jtoken1.Value<float>() < 1.0)
                    throw new ArgumentException(message: "\"width\" attribute is not a floating point value greater or equal to 1");
                action.Set(key: "width", value: jtoken1.Value<float>());
            }

            var jtoken2 = actionItem[key: "height"];
            if (jtoken2 != null) {
                if (jtoken2.Type != JTokenType.Float || jtoken2.Value<float>() < 1.0)
                    throw new ArgumentException(message: "\"height\" attribute is not a floating point value greater or equal to 1");
                action.Set(key: "height", value: jtoken2.Value<float>());
            }

            if (jtoken1 != null && jtoken2 == null || jtoken1 == null && jtoken2 != null)
                throw new ArgumentException(message: "\"width\" and \"height\" attributes need to be specified together");
            var jtoken3 = actionItem[key: "pressure"];
            if (jtoken3 != null) {
                if (jtoken3.Type != JTokenType.Float || jtoken3.Value<float>() < 0.0 || jtoken3.Value<float>() > 1.0)
                    throw new ArgumentException(message: "\"pressure\" attribute is not a floating point value between 0 and 1");
                action.Set(key: "pressure", value: jtoken3.Value<float>());
            }

            var jtoken4 = actionItem[key: "tiltX"];
            if (jtoken4 != null) {
                if (jtoken4.Type != JTokenType.Integer || jtoken4.Value<int>() < -90 || jtoken4.Value<int>() > 90)
                    throw new ArgumentException(message: "\"tiltX\" attribute is not an integer value between -90 and 90");
                action.Set(key: "tiltX", value: jtoken4.Value<int>());
            }

            var jtoken5 = actionItem[key: "tiltY"];
            if (jtoken5 != null) {
                if (jtoken5.Type != JTokenType.Integer || jtoken5.Value<int>() < -90 || jtoken5.Value<int>() > 90)
                    throw new ArgumentException(message: "\"tiltY\" attribute is not an integer value between -90 and 90");
                action.Set(key: "tiltY", value: jtoken5.Value<int>());
            }

            var jtoken6 = actionItem[key: "twist"];
            if (jtoken6 == null)
                return;
            if (jtoken6.Type != JTokenType.Integer || jtoken6.Value<int>() < 0 || jtoken6.Value<int>() >= 360)
                throw new ArgumentException(message: "\"twist\" attribute is not an integer value between 0 and 359");
            action.Set(key: "twist", value: jtoken6.Value<int>());
        }

        void ProcessPointerCancelAction(JToken actionItem, ActionObject action) {
            throw new NotImplementedException(message: "Process a pointer cancel action is not implemented");
        }

        void DispatchActions(ActionsByTick actionsByTick) {
            this.pointerIdRecylcleList = new List<PointerInputState>();
            int xScreen;
            int yScreen;
            PositionAdapter.ConvertClientToScreen(hwnd: this.SessionTopLevelWindowHandle, xClient: 0, yClient: 0, xScreen: out xScreen, yScreen: out yScreen);
            var topLevelWindowPosition = new PointI(x: xScreen, y: yScreen);
            foreach (var tickActions in actionsByTick) {
                var tickDuration = ComputeTickDuration(tickActions: tickActions);
                var stopwatch = Stopwatch.StartNew();
                DispatchTickActions(tickActions: tickActions, tickDuration: tickDuration, topLevelWindowPosition: topLevelWindowPosition);
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds < tickDuration)
                    Thread.Sleep(timeout: TimeSpan.FromMilliseconds(value: tickDuration).Subtract(ts: stopwatch.Elapsed));
            }

            foreach (var pointerIdRecylcle in this.pointerIdRecylcleList)
                if (pointerIdRecylcle.Pressed.Count == 0 && pointerIdRecylcle.PointerId != -1) {
                    var pointerId = pointerIdRecylcle.PointerId;
                    pointerIdRecylcle.PointerId = -1;
                    availablePointerIdCollection.Recycle(pointerId: pointerId);
                }

            this.pointerIdRecylcleList.Clear();
        }

        uint ComputeTickDuration(ActionSequence tickActions) {
            uint num1 = 0;
            foreach (var tickAction in tickActions) {
                var nullable1 = new uint?();
                if (tickAction.Subtype == 0 || tickAction.Type == 2 && tickAction.Subtype == 5)
                    nullable1 = (uint?) tickAction.Get(key: "duration");
                if (nullable1.HasValue) {
                    var nullable2 = nullable1;
                    var num2 = num1;
                    if ((nullable2.GetValueOrDefault() > num2) & nullable2.HasValue)
                        num1 = nullable1.Value;
                }
            }

            return num1;
        }

        void DispatchTickActions(
            ActionSequence tickActions,
            uint tickDuration,
            PointI topLevelWindowPosition) {
            var tickPenList = new List<PointerData>();
            var tickTouchList = new List<PointerData>();
            var pointerDataListList1 = new List<List<PointerData>>();
            var pointerDataListList2 = new List<List<PointerData>>();
            var stopwatch = Stopwatch.StartNew();
            foreach (var tickAction in tickActions) {
                var id = tickAction.Id;
                var type = (InputSourceType) tickAction.Type;
                if (!this.InputStateTable.ContainsKey(key: id))
                    switch (type) {
                        case InputSourceType.None:
                            this.InputStateTable.Add(key: id, value: new NullInputState());
                            break;
                        case InputSourceType.Key:
                            this.InputStateTable.Add(key: id, value: new KeyInputState());
                            break;
                        case InputSourceType.Pointer:
                            this.InputStateTable.Add(key: id, value: new PointerInputState(subtype: (PointerType) tickAction.Get(key: "pointerType")));
                            break;
                    }

                var inputSourceState = this.InputStateTable[key: id];
                switch (tickAction.Subtype) {
                    case 0:
                        DispatchPauseAction(sourceId: id, actionObject: tickAction, inputState: (NullInputState) inputSourceState, tickDuration: tickDuration);
                        continue;
                    case 1:
                        DispatchKeyDownAction(sourceId: id, actionObject: tickAction, inputState: (KeyInputState) inputSourceState, tickDuration: tickDuration);
                        continue;
                    case 2:
                        DispatchKeyUpAction(sourceId: id, actionObject: tickAction, inputState: (KeyInputState) inputSourceState, tickDuration: tickDuration);
                        continue;
                    case 3:
                        DispatchPointerDownAction(sourceId: id, actionObject: tickAction, inputState: (PointerInputState) inputSourceState, tickDuration: tickDuration, topLevelWindowPosition: topLevelWindowPosition, tickPenList: tickPenList, tickTouchList: tickTouchList);
                        continue;
                    case 4:
                        DispatchPointerUpAction(sourceId: id, actionObject: tickAction, inputState: (PointerInputState) inputSourceState, tickDuration: tickDuration, topLevelWindowPosition: topLevelWindowPosition, tickPenList: tickPenList, tickTouchList: tickTouchList);
                        continue;
                    case 5:
                        DispatchPointerMoveAction(sourceId: id, actionObject: tickAction, inputState: (PointerInputState) inputSourceState, tickDuration: tickDuration, topLevelWindowPosition: topLevelWindowPosition, tickPenList: tickPenList, tickTouchList: tickTouchList, tickPenInterpolation: pointerDataListList1, tickTouchInterpolation: pointerDataListList2);
                        continue;
                    case 6:
                        DispatchPointerCancelAction(sourceId: id, actionObject: tickAction, inputState: (PointerInputState) inputSourceState, tickDuration: tickDuration);
                        continue;
                    default:
                        throw new InternalErrorException(message: string.Format(format: "Action object subtype is not known: {0}", arg0: tickAction.Subtype));
                }
            }

            if (tickTouchList.Count > 0)
                using (InputController.Activate(inputType: PointerInputType.MultiTouch)) {
                    var pointerDataArray = new PointerData[tickTouchList.Count];
                    tickTouchList.CopyTo(array: pointerDataArray);
                    PointerInput.InjectPointers(pointerDataArray: pointerDataArray);
                }

            if (tickPenList.Count > 1)
                throw new ArgumentException(message: "Currently only a single (non-concurrent) pen input is supported");
            if (tickPenList.Count > 0)
                using (InputController.Activate(inputType: PointerInputType.Pen)) {
                    var pointerDataArray = new PointerData[tickPenList.Count];
                    tickPenList.CopyTo(array: pointerDataArray);
                    PointerInput.InjectPointers(pointerDataArray: pointerDataArray);
                }

            if (pointerDataListList1.Count <= 0 && pointerDataListList2.Count <= 0)
                return;
            PerformInterpolationFrames(tickPenInterpolationFrames: pointerDataListList1, tickTouchInterpolationFrames: pointerDataListList2, tickElapsedMs: stopwatch.ElapsedMilliseconds);
        }

        void PerformInterpolationFrames(
            List<List<PointerData>> tickPenInterpolationFrames,
            List<List<PointerData>> tickTouchInterpolationFrames,
            long tickElapsedMs) {
            var stopwatch = Stopwatch.StartNew();
            var index1 = 0;
            var index2 = 0;
            while (index1 < tickPenInterpolationFrames.Count || index2 < tickTouchInterpolationFrames.Count) {
                if (index1 < tickPenInterpolationFrames.Count) {
                    using (InputController.Activate(inputType: PointerInputType.Pen)) {
                        var pointerDataArray = new PointerData[tickPenInterpolationFrames[index: index1].Count];
                        tickPenInterpolationFrames[index: index1].CopyTo(array: pointerDataArray);
                        PointerInput.InjectPointers(pointerDataArray: pointerDataArray);
                    }

                    ++index1;
                }

                if (index2 < tickTouchInterpolationFrames.Count) {
                    using (InputController.Activate(inputType: PointerInputType.MultiTouch)) {
                        var pointerDataArray = new PointerData[tickTouchInterpolationFrames[index: index2].Count];
                        tickTouchInterpolationFrames[index: index2].CopyTo(array: pointerDataArray);
                        PointerInput.InjectPointers(pointerDataArray: pointerDataArray);
                    }

                    ++index2;
                }

                if (stopwatch.ElapsedMilliseconds + tickElapsedMs < 50L)
                    Thread.Sleep(millisecondsTimeout: (int) (50L - tickElapsedMs - stopwatch.ElapsedMilliseconds));
                stopwatch.Restart();
                tickElapsedMs = 0L;
            }
        }

        void DispatchPauseAction(
            string sourceId,
            ActionObject actionObject,
            NullInputState inputState,
            uint tickDuration) {
        }

        void DispatchKeyDownAction(
            string sourceId,
            ActionObject actionObject,
            KeyInputState inputState,
            uint tickDuration) {
            var str = (string) actionObject.Get(key: "value");
            var normalisedKey = Keys.GetNormalisedKey(key: str);
            inputState.Pressed.Contains(item: normalisedKey);
            Keys.GetShiftedCharacterCode(key: str);
            Keys.GetKeyLocation(key: str);
            if (normalisedKey == "Alt")
                inputState.Alt = true;
            if (normalisedKey == "Shift")
                inputState.Shift = true;
            if (normalisedKey == "Control")
                inputState.Ctrl = true;
            if (normalisedKey == "Meta")
                inputState.Meta = true;
            inputState.Pressed.Add(item: normalisedKey);
            var text = KeyboardInput.Process(inputKeySequences: str);
            if (text.Length <= 0)
                return;
            TextInput.SendText(text: text);
        }

        void DispatchKeyUpAction(
            string sourceId,
            ActionObject actionObject,
            KeyInputState inputState,
            uint tickDuration) {
            var str = (string) actionObject.Get(key: "value");
            var normalisedKey = Keys.GetNormalisedKey(key: str);
            if (!inputState.Pressed.Contains(item: normalisedKey))
                return;
            Keys.GetShiftedCharacterCode(key: str);
            Keys.GetKeyLocation(key: str);
            if (normalisedKey == "Alt")
                inputState.Alt = false;
            if (normalisedKey == "Shift")
                inputState.Shift = false;
            if (normalisedKey == "Control")
                inputState.Ctrl = false;
            if (normalisedKey == "Meta")
                inputState.Meta = false;
            inputState.Pressed.Remove(item: normalisedKey);
            var text = KeyboardInput.Process(inputKeySequences: str);
            if (text.Length <= 0)
                return;
            TextInput.SendText(text: text);
        }

        void DispatchPointerDownAction(
            string sourceId,
            ActionObject actionObject,
            PointerInputState inputState,
            uint tickDuration,
            PointI topLevelWindowPosition,
            List<PointerData> tickPenList,
            List<PointerData> tickTouchList) {
            var pointerType = (PointerType) actionObject.Get(key: "pointerType");
            var num = (int) actionObject.Get(key: "button");
            if (inputState.Pressed.Contains(item: num))
                return;
            var x = inputState.X;
            var y = inputState.Y;
            inputState.Pressed.Add(item: num);
            var pressed = inputState.Pressed;
            var actionObject1 = actionObject.Copy();
            actionObject1.Subtype = 4;
            this.InputCancelList.Add(item: actionObject1);
            switch (pointerType) {
                case PointerType.Pen:
                case PointerType.Touch:
                    int xScreen;
                    int yScreen;
                    ConvertClientToScreen(clientTopLeftPosition: topLevelWindowPosition, xClient: x, yClient: y, xScreen: out xScreen, yScreen: out yScreen);
                    if (inputState.PointerId == -1)
                        inputState.PointerId = availablePointerIdCollection.Get();
                    var pointerData = new PointerData {
                        location = new PointI(x: xScreen, y: yScreen),
                        flags = POINTER_FLAGS.ContactDown,
                        pointerId = (uint) inputState.PointerId
                    };
                    UpdateInputStateWithExtraPointerParameters(actionObject: actionObject, inputState: inputState);
                    PopulatePointerDataWithExtraPointerParameters(inputState: inputState, pointerData: ref pointerData);
                    if (pointerType == PointerType.Pen) {
                        tickPenList.Add(item: pointerData);
                        break;
                    }

                    if (pointerType != PointerType.Touch)
                        break;
                    tickTouchList.Add(item: pointerData);
                    break;
                default:
                    throw new InternalErrorException(message: string.Format(format: "Unsupported pointer type: {0}", arg0: pointerType));
            }
        }

        void DispatchPointerUpAction(
            string sourceId,
            ActionObject actionObject,
            PointerInputState inputState,
            uint tickDuration,
            PointI topLevelWindowPosition,
            List<PointerData> tickPenList,
            List<PointerData> tickTouchList) {
            var pointerType = (PointerType) actionObject.Get(key: "pointerType");
            var num = (int) actionObject.Get(key: "button");
            if (!inputState.Pressed.Contains(item: num))
                return;
            var x = inputState.X;
            var y = inputState.Y;
            inputState.Pressed.Remove(item: num);
            var pressed = inputState.Pressed;
            switch (pointerType) {
                case PointerType.Pen:
                case PointerType.Touch:
                    int xScreen;
                    int yScreen;
                    ConvertClientToScreen(clientTopLeftPosition: topLevelWindowPosition, xClient: x, yClient: y, xScreen: out xScreen, yScreen: out yScreen);
                    if (inputState.PointerId == -1)
                        throw new InternalErrorException(message: "Pointer Up Action takes place on input state with uninitialized pointer id");
                    var pointerData = new PointerData {
                        location = new PointI(x: xScreen, y: yScreen),
                        flags = POINTER_FLAGS.UP,
                        pointerId = (uint) inputState.PointerId
                    };
                    this.pointerIdRecylcleList.Add(item: inputState);
                    UpdateInputStateWithExtraPointerParameters(actionObject: actionObject, inputState: inputState);
                    PopulatePointerDataWithExtraPointerParameters(inputState: inputState, pointerData: ref pointerData);
                    if (pointerType == PointerType.Pen) {
                        tickPenList.Add(item: pointerData);
                        break;
                    }

                    if (pointerType != PointerType.Touch)
                        break;
                    tickTouchList.Add(item: pointerData);
                    break;
                default:
                    throw new InternalErrorException(message: string.Format(format: "Unsupported pointer type: {0}", arg0: pointerType));
            }
        }

        void DispatchPointerMoveAction(
            string sourceId,
            ActionObject actionObject,
            PointerInputState inputState,
            uint tickDuration,
            PointI topLevelWindowPosition,
            List<PointerData> tickPenList,
            List<PointerData> tickTouchList,
            List<List<PointerData>> tickPenInterpolation,
            List<List<PointerData>> tickTouchInterpolation) {
            var num1 = (int) actionObject.Get(key: "x");
            var num2 = (int) actionObject.Get(key: "y");
            var x = inputState.X;
            var y = inputState.Y;
            var originType = (OriginType) actionObject.Get(key: "origin");
            var targetX = 0;
            var targetY = 0;
            switch (originType) {
                case OriginType.Viewport:
                    targetX = num1;
                    targetY = num2;
                    break;
                case OriginType.Pointer:
                    targetX = x + num1;
                    targetY = y + num2;
                    break;
                case OriginType.Element:
                    var elementId = (string) actionObject.Get(key: "element");
                    switch (this.SessionKnownElements.GetStatus(elementId: elementId)) {
                        case ResponseStatus.NoSuchElement:
                            throw new NoSuchElementException(message: "Element " + elementId + " specified in the Actions origin is unknown or does not exist");
                        case ResponseStatus.StaleElementReference:
                            throw new NoSuchElementException(message: "Element " + elementId + " specified in the Actions origin is no longer valid");
                        default:
                            var uiObject = this.SessionKnownElements.Get(elementId: elementId);
                            if (uiObject == null)
                                throw new InternalErrorException(message: "Internal error is encountered when retrieving element " + elementId + " coordinates specified in the Actions origin");
                            var levelWindowHandle = this.SessionTopLevelWindowHandle;
                            var rectangleTopLeft = uiObject.GetBoundingRectangleTopLeft(appRootWindowHandle: this.SessionTopLevelWindowHandle);
                            var size = uiObject.GetAdjustedBoundingRectangle().Size;
                            var num3 = rectangleTopLeft.X + size.Width / 2;
                            var num4 = rectangleTopLeft.Y + size.Height / 2;
                            targetX = num3 + num1;
                            var num5 = num2;
                            targetY = num4 + num5;
                            break;
                    }

                    break;
            }

            var duration = ((uint?) actionObject.Get(key: "duration")).HasValue ? (uint) actionObject.Get(key: "duration") : tickDuration;
            UpdateInputStateWithExtraPointerParameters(actionObject: actionObject, inputState: inputState);
            PerformPointerMoveAction(sourceId: sourceId, inputState: inputState, duration: duration, startX: x, startY: y, targetX: targetX, targetY: targetY, topLevelWindowPosition: topLevelWindowPosition, tickPenList: tickPenList, tickTouchList: tickTouchList, tickPenInterpolation: tickPenInterpolation, tickTouchInterpolation: tickTouchInterpolation);
        }

        void PerformPointerMoveAction(
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
            List<List<PointerData>> tickTouchInterpolation) {
            var subtype = inputState.Subtype;
            var xClient = duration >= 50U ? startX : targetX;
            var yClient = duration >= 50U ? startY : targetY;
            var x = inputState.X;
            var y = inputState.Y;
            var pressed = inputState.Pressed;
            switch (subtype) {
                case PointerType.Mouse:
                case PointerType.Wheel:
                    throw new NotImplementedException();
                case PointerType.Pen:
                case PointerType.Touch:
                    int xScreen;
                    int yScreen;
                    ConvertClientToScreen(clientTopLeftPosition: topLevelWindowPosition, xClient: xClient, yClient: yClient, xScreen: out xScreen, yScreen: out yScreen);
                    if (inputState.PointerId == -1)
                        inputState.PointerId = availablePointerIdCollection.Get();
                    var pointerData = new PointerData {
                        location = new PointI(x: xScreen, y: yScreen),
                        flags = POINTER_FLAGS.ContactMoves,
                        pointerId = (uint) inputState.PointerId
                    };
                    PopulatePointerDataWithExtraPointerParameters(inputState: inputState, pointerData: ref pointerData);
                    if (inputState.Pressed.Count > 0) {
                        switch (subtype) {
                            case PointerType.Pen:
                                tickPenList.Add(item: pointerData);
                                break;
                            case PointerType.Touch:
                                tickTouchList.Add(item: pointerData);
                                break;
                        }

                        if (duration > 50U) {
                            var startX1 = startX + topLevelWindowPosition.X;
                            var startY1 = startY + topLevelWindowPosition.Y;
                            var targetX1 = targetX + topLevelWindowPosition.X;
                            var targetY1 = targetY + topLevelWindowPosition.Y;
                            switch (subtype) {
                                case PointerType.Pen:
                                    GenerateInterpolationFrames(duration: duration, inputPointerId: inputState.PointerId, startX: startX1, startY: startY1, targetX: targetX1, targetY: targetY1, tickPointerInputInterpolationFrames: tickPenInterpolation);
                                    break;
                                case PointerType.Touch:
                                    GenerateInterpolationFrames(duration: duration, inputPointerId: inputState.PointerId, startX: startX1, startY: startY1, targetX: targetX1, targetY: targetY1, tickPointerInputInterpolationFrames: tickTouchInterpolation);
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

        void DispatchPointerCancelAction(
            string sourceId,
            ActionObject actionObject,
            PointerInputState inputState,
            uint tickDuration) {
            throw new NotImplementedException();
        }

        void UpdateInputStateWithExtraPointerParameters(
            ActionObject actionObject,
            PointerInputState inputState) {
            inputState.Pressure = actionObject.Contains(key: "pressure") ? (float?) actionObject.Get(key: "pressure") : inputState.Pressure;
            inputState.Twist = actionObject.Contains(key: "twist") ? (int?) actionObject.Get(key: "twist") : inputState.Twist;
            inputState.TiltX = actionObject.Contains(key: "tiltX") ? (int?) actionObject.Get(key: "tiltX") : inputState.TiltX;
            inputState.TiltY = actionObject.Contains(key: "tiltY") ? (int?) actionObject.Get(key: "tiltY") : inputState.TiltY;
            var pointerInputState1 = inputState;
            float? nullable1;
            double? nullable2;
            if (!actionObject.Contains(key: "width")) {
                nullable2 = inputState.Width;
            } else {
                nullable1 = (float?) actionObject.Get(key: "width");
                nullable2 = nullable1.HasValue ? nullable1.GetValueOrDefault() : new double?();
            }

            pointerInputState1.Width = nullable2;
            var pointerInputState2 = inputState;
            double? nullable3;
            if (!actionObject.Contains(key: "height")) {
                nullable3 = inputState.Height;
            } else {
                nullable1 = (float?) actionObject.Get(key: "height");
                nullable3 = nullable1.HasValue ? nullable1.GetValueOrDefault() : new double?();
            }

            pointerInputState2.Height = nullable3;
        }

        void PopulatePointerDataWithExtraPointerParameters(
            PointerInputState inputState,
            ref PointerData pointerData) {
            ref var local1 = ref pointerData;
            uint? nullable1;
            if (!inputState.Pressure.HasValue) {
                nullable1 = new uint?();
            } else {
                var pressure = inputState.Pressure;
                var num = 1024f;
                nullable1 = pressure.HasValue ? (uint) (pressure.GetValueOrDefault() * (double) num) : new uint?();
            }

            local1.pressure = nullable1;
            ref var local2 = ref pointerData;
            int? nullable2;
            uint? nullable3;
            if (!inputState.Twist.HasValue) {
                nullable3 = new uint?();
            } else {
                nullable2 = inputState.Twist;
                nullable3 = nullable2.HasValue ? (uint) nullable2.GetValueOrDefault() : new uint?();
            }

            local2.twist = nullable3;
            ref var local3 = ref pointerData;
            nullable2 = inputState.TiltX;
            int? nullable4;
            if (!nullable2.HasValue) {
                nullable2 = new int?();
                nullable4 = nullable2;
            } else {
                nullable4 = inputState.TiltX;
            }

            local3.tiltX = nullable4;
            ref var local4 = ref pointerData;
            nullable2 = inputState.TiltY;
            int? nullable5;
            if (!nullable2.HasValue) {
                nullable2 = new int?();
                nullable5 = nullable2;
            } else {
                nullable5 = inputState.TiltY;
            }

            local4.tiltY = nullable5;
            ref var local5 = ref pointerData;
            double? nullable6;
            int? nullable7;
            if (!inputState.Width.HasValue) {
                nullable2 = new int?();
                nullable7 = nullable2;
            } else {
                nullable6 = inputState.Width;
                if (!nullable6.HasValue) {
                    nullable2 = new int?();
                    nullable7 = nullable2;
                } else {
                    nullable7 = (int) nullable6.GetValueOrDefault();
                }
            }

            local5.width = nullable7;
            ref var local6 = ref pointerData;
            nullable6 = inputState.Height;
            int? nullable8;
            if (!nullable6.HasValue) {
                nullable2 = new int?();
                nullable8 = nullable2;
            } else {
                nullable6 = inputState.Height;
                if (!nullable6.HasValue) {
                    nullable2 = new int?();
                    nullable8 = nullable2;
                } else {
                    nullable8 = (int) nullable6.GetValueOrDefault();
                }
            }

            local6.height = nullable8;
            if (inputState.Pressed.Contains(item: 5))
                pointerData.pressedButton |= POINTER_PRESSED_BUTTON.INVERTED | POINTER_PRESSED_BUTTON.ERASER;
            if (!inputState.Pressed.Contains(item: 2))
                return;
            pointerData.pressedButton |= POINTER_PRESSED_BUTTON.BARREL;
        }

        void GenerateInterpolationFrames(
            uint duration,
            int inputPointerId,
            int startX,
            int startY,
            int targetX,
            int targetY,
            List<List<PointerData>> tickPointerInputInterpolationFrames) {
            var num1 = 50f;
            var num2 = duration > 0U ? num1 / duration : 1f;
            var num3 = (int) (duration / (double) num1);
            for (var index = 0; index < num3; ++index) {
                if (tickPointerInputInterpolationFrames.Count < index + 1)
                    tickPointerInputInterpolationFrames.Add(item: new List<PointerData>());
                var x = (int) Math.Round(a: (index + 1) * (double) num2 * (targetX - startX)) + startX;
                var y = (int) Math.Round(a: (index + 1) * (double) num2 * (targetY - startY)) + startY;
                var pointerData = new PointerData {
                    location = new PointI(x: x, y: y),
                    flags = POINTER_FLAGS.ContactMoves,
                    pointerId = (uint) inputPointerId
                };
                tickPointerInputInterpolationFrames[index: index].Add(item: pointerData);
            }
        }

        void ConvertClientToScreen(
            PointI clientTopLeftPosition,
            int xClient,
            int yClient,
            out int xScreen,
            out int yScreen) {
            xScreen = xClient + clientTopLeftPosition.X;
            yScreen = yClient + clientTopLeftPosition.Y;
        }

        internal void PerformActions(string ActionsJSONString) {
            DispatchActions(actionsByTick: ExtractActionSequence(parameters: JToken.Parse(json: ActionsJSONString)));
        }

        internal void ReleaseActions() {
            var tickActions = new ActionSequence(collection: this.InputCancelList);
            tickActions.Reverse();
            DispatchTickActions(tickActions: tickActions, tickDuration: 0U, topLevelWindowPosition: new PointI());
            this.InputCancelList.Clear();
            this.InputStateTable.Clear();
            this.ActiveInputSources.Clear();
        }
    }
}