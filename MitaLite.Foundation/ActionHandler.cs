// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.ActionHandler
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;
using System.Threading;
using MS.Internal.Mita.Foundation.Utilities;

namespace MS.Internal.Mita.Foundation {
    public static class ActionHandler {
        static bool _halted;
        static readonly object _lock = new object();
        static readonly Dictionary<string, ActionSet> _registeredEvents = new Dictionary<string, ActionSet>(comparer: StringComparer.OrdinalIgnoreCase);
        static ActionEvent _prefixEvents;
        static bool _activeAction;
        static bool _activePrefix;

        public static ICollection<string> RegisteredActions {
            get { return _registeredEvents.Keys; }
        }

        static void InvokeList(ActionEvent handler, UIObject sender, ActionEventArgs args) {
            foreach (ActionEvent invocation in handler.GetInvocationList()) {
                invocation(sender: sender, actionInfo: args);
                if (_halted)
                    break;
            }
        }

        static void RemoveAction(string action) {
            if (_registeredEvents.Count <= 0)
                return;
            _registeredEvents.Remove(key: action);
            if (_registeredEvents.Count != 0)
                return;
            _activeAction = false;
        }

        public static void Halt() {
            if (!Monitor.TryEnter(obj: _lock))
                throw new HaltingException(message: StringResource.Get(id: "IncorrectThreadAttemptingToHalt"));
            _halted = true;
            Monitor.Exit(obj: _lock);
        }

        public static void Subscribe(string action, ActionEvent handler) {
            Subscribe(action: action, handler: handler, addToEnd: true);
        }

        public static void Subscribe(string action, ActionEvent handler, bool addToEnd) {
            Validate.StringNeitherNullNorEmpty(parameter: action, parameterName: nameof(action));
            Validate.ArgumentNotNull(parameter: handler, parameterName: nameof(handler));
            lock (_lock) {
                _activeAction = true;
                ActionSet actionSet;
                if (_registeredEvents.TryGetValue(key: action, value: out actionSet))
                    actionSet.Actions = addToEnd ? actionSet.Actions + handler : handler + actionSet.Actions;
                else
                    _registeredEvents.Add(key: action, value: new ActionSet(actions: handler, overrideDelegate: null, addtoEnd: addToEnd));
            }
        }

        public static void Remove(string action, ActionEvent handler) {
            Validate.StringNeitherNullNorEmpty(parameter: action, parameterName: nameof(action));
            Validate.ArgumentNotNull(parameter: handler, parameterName: nameof(handler));
            lock (_lock) {
                ActionSet actionSet;
                if (!_registeredEvents.TryGetValue(key: action, value: out actionSet))
                    return;
                actionSet.Actions -= handler;
                if (actionSet.Actions != null || actionSet.Override != null)
                    return;
                RemoveAction(action: action);
            }
        }

        public static void SubscribeToAll(ActionEvent handler) {
            SubscribeToAll(handler: handler, addToEnd: true);
        }

        public static void SubscribeToAll(ActionEvent handler, bool addToEnd) {
            lock (_lock) {
                _activePrefix = true;
                _prefixEvents = addToEnd ? _prefixEvents + handler : handler + _prefixEvents;
            }
        }

        public static void RemoveFromAll(ActionEvent handler) {
            Validate.ArgumentNotNull(parameter: handler, parameterName: nameof(handler));
            lock (_lock) {
                _prefixEvents -= handler;
                if (_prefixEvents != null)
                    return;
                _activePrefix = false;
            }
        }

        public static ActionOverrideEvent SubscribeForOverride(
            string action,
            ActionOverrideEvent replacement) {
            return SubscribeForOverride(action: action, replacement: replacement, executeOverrideLast: true);
        }

        public static ActionOverrideEvent SubscribeForOverride(
            string action,
            ActionOverrideEvent replacement,
            bool executeOverrideLast) {
            Validate.StringNeitherNullNorEmpty(parameter: action, parameterName: nameof(action));
            lock (_lock) {
                ActionSet actionSet;
                var flag = _registeredEvents.TryGetValue(key: action, value: out actionSet);
                if (!flag) {
                    if (replacement == null)
                        return null;
                    actionSet = new ActionSet();
                }

                var actionOverrideEvent = actionSet.Override;
                actionSet.Override = replacement;
                actionSet.RunOverrideAtEnd = executeOverrideLast;
                if (actionSet.Actions == null && actionSet.Override == null) {
                    RemoveAction(action: action);
                } else if (!flag) {
                    _activeAction = true;
                    _registeredEvents.Add(key: action, value: actionSet);
                }

                return actionOverrideEvent;
            }
        }

        public static ActionOverrideEvent RemoveFromOverride(string action) {
            return SubscribeForOverride(action: action, replacement: null);
        }

        public static ActionResult Invoke(UIObject sender, ActionEventArgs actionInfo) {
            return Invoke(sender: sender, actionInfo: actionInfo, overridden: out var _);
        }

        public static ActionResult Invoke(
            UIObject sender,
            ActionEventArgs actionInfo,
            out object overridden) {
            Validate.ArgumentNotNull(parameter: actionInfo, parameterName: nameof(actionInfo));
            var actionString = actionInfo.ActionString;
            overridden = null;
            object overridden1 = null;
            if (!_activePrefix && !_activeAction)
                return ActionResult.Unhandled;
            lock (_lock) {
                var actionResult = ActionResult.Unhandled;
                ActionSet actionSet;
                if (_registeredEvents.TryGetValue(key: actionString, value: out actionSet)) {
                    if (!actionSet.RunOverrideAtEnd && actionSet.Override != null)
                        actionResult = actionSet.Override(sender: sender, actionInfo: actionInfo, overridden: out overridden1);
                    if (_activePrefix)
                        InvokeList(handler: _prefixEvents, sender: sender, args: actionInfo);
                    if (!_halted && actionSet.Actions != null)
                        InvokeList(handler: actionSet.Actions, sender: sender, args: actionInfo);
                    if (actionSet.RunOverrideAtEnd && actionSet.Override != null && !_halted)
                        actionResult = actionSet.Override(sender: sender, actionInfo: actionInfo, overridden: out overridden1);
                } else if (!_halted && _activePrefix) {
                    InvokeList(handler: _prefixEvents, sender: sender, args: actionInfo);
                }

                if (overridden1 != null)
                    overridden = overridden1;
                return actionResult;
            }
        }

        public static ActionEvent GetHandler(string action) {
            Validate.StringNeitherNullNorEmpty(parameter: action, parameterName: nameof(action));
            ActionSet actionSet;
            return !_registeredEvents.TryGetValue(key: action, value: out actionSet) ? null : new ActionEvent(actionSet.Actions.Invoke);
        }

        public static ActionOverrideEvent GetOverride(string action) {
            Validate.StringNeitherNullNorEmpty(parameter: action, parameterName: nameof(action));
            ActionSet actionSet;
            return !_registeredEvents.TryGetValue(key: action, value: out actionSet) ? null : new ActionOverrideEvent(actionSet.Override.Invoke);
        }

        class ActionSet {
            public ActionEvent Actions;
            public ActionOverrideEvent Override;
            public bool RunOverrideAtEnd;

            public ActionSet() {
            }

            public ActionSet(ActionEvent actions, ActionOverrideEvent overrideDelegate, bool addtoEnd) {
                this.Actions = actions;
                this.Override = overrideDelegate;
                this.RunOverrideAtEnd = addtoEnd;
            }
        }
    }
}