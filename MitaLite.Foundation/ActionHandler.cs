// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.ActionHandler
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MS.Internal.Mita.Foundation
{
  public static class ActionHandler
  {
    private static bool _halted;
    private static readonly object _lock = new object();
    private static readonly Dictionary<string, ActionHandler.ActionSet> _registeredEvents = new Dictionary<string, ActionHandler.ActionSet>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private static ActionEvent _prefixEvents = (ActionEvent) null;
    private static bool _activeAction = false;
    private static bool _activePrefix = false;

    private static void InvokeList(ActionEvent handler, UIObject sender, ActionEventArgs args)
    {
      foreach (ActionEvent invocation in handler.GetInvocationList())
      {
        invocation(sender, args);
        if (ActionHandler._halted)
          break;
      }
    }

    private static void RemoveAction(string action)
    {
      if (ActionHandler._registeredEvents.Count <= 0)
        return;
      ActionHandler._registeredEvents.Remove(action);
      if (ActionHandler._registeredEvents.Count != 0)
        return;
      ActionHandler._activeAction = false;
    }

    public static void Halt()
    {
      if (!Monitor.TryEnter(ActionHandler._lock))
        throw new HaltingException(StringResource.Get("IncorrectThreadAttemptingToHalt"));
      ActionHandler._halted = true;
      Monitor.Exit(ActionHandler._lock);
    }

    public static void Subscribe(string action, ActionEvent handler) => ActionHandler.Subscribe(action, handler, true);

    public static void Subscribe(string action, ActionEvent handler, bool addToEnd)
    {
      Validate.StringNeitherNullNorEmpty(action, nameof (action));
      Validate.ArgumentNotNull((object) handler, nameof (handler));
      lock (ActionHandler._lock)
      {
        ActionHandler._activeAction = true;
        ActionHandler.ActionSet actionSet;
        if (ActionHandler._registeredEvents.TryGetValue(action, out actionSet))
          actionSet.Actions = addToEnd ? actionSet.Actions + handler : handler + actionSet.Actions;
        else
          ActionHandler._registeredEvents.Add(action, new ActionHandler.ActionSet(handler, (ActionOverrideEvent) null, addToEnd));
      }
    }

    public static void Remove(string action, ActionEvent handler)
    {
      Validate.StringNeitherNullNorEmpty(action, nameof (action));
      Validate.ArgumentNotNull((object) handler, nameof (handler));
      lock (ActionHandler._lock)
      {
        ActionHandler.ActionSet actionSet;
        if (!ActionHandler._registeredEvents.TryGetValue(action, out actionSet))
          return;
        actionSet.Actions -= handler;
        if (actionSet.Actions != null || actionSet.Override != null)
          return;
        ActionHandler.RemoveAction(action);
      }
    }

    public static void SubscribeToAll(ActionEvent handler) => ActionHandler.SubscribeToAll(handler, true);

    public static void SubscribeToAll(ActionEvent handler, bool addToEnd)
    {
      lock (ActionHandler._lock)
      {
        ActionHandler._activePrefix = true;
        ActionHandler._prefixEvents = addToEnd ? ActionHandler._prefixEvents + handler : handler + ActionHandler._prefixEvents;
      }
    }

    public static void RemoveFromAll(ActionEvent handler)
    {
      Validate.ArgumentNotNull((object) handler, nameof (handler));
      lock (ActionHandler._lock)
      {
        ActionHandler._prefixEvents -= handler;
        if (ActionHandler._prefixEvents != null)
          return;
        ActionHandler._activePrefix = false;
      }
    }

    public static ActionOverrideEvent SubscribeForOverride(
      string action,
      ActionOverrideEvent replacement) => ActionHandler.SubscribeForOverride(action, replacement, true);

    public static ActionOverrideEvent SubscribeForOverride(
      string action,
      ActionOverrideEvent replacement,
      bool executeOverrideLast)
    {
      Validate.StringNeitherNullNorEmpty(action, nameof (action));
      lock (ActionHandler._lock)
      {
        ActionHandler.ActionSet actionSet;
        bool flag = ActionHandler._registeredEvents.TryGetValue(action, out actionSet);
        if (!flag)
        {
          if (replacement == null)
            return (ActionOverrideEvent) null;
          actionSet = new ActionHandler.ActionSet();
        }
        ActionOverrideEvent actionOverrideEvent = actionSet.Override;
        actionSet.Override = replacement;
        actionSet.RunOverrideAtEnd = executeOverrideLast;
        if (actionSet.Actions == null && actionSet.Override == null)
          ActionHandler.RemoveAction(action);
        else if (!flag)
        {
          ActionHandler._activeAction = true;
          ActionHandler._registeredEvents.Add(action, actionSet);
        }
        return actionOverrideEvent;
      }
    }

    public static ActionOverrideEvent RemoveFromOverride(string action) => ActionHandler.SubscribeForOverride(action, (ActionOverrideEvent) null);

    public static ActionResult Invoke(UIObject sender, ActionEventArgs actionInfo) => ActionHandler.Invoke(sender, actionInfo, out object _);

    public static ActionResult Invoke(
      UIObject sender,
      ActionEventArgs actionInfo,
      out object overridden)
    {
      Validate.ArgumentNotNull((object) actionInfo, nameof (actionInfo));
      string actionString = actionInfo.ActionString;
      overridden = (object) null;
      object overridden1 = (object) null;
      if (!ActionHandler._activePrefix && !ActionHandler._activeAction)
        return ActionResult.Unhandled;
      lock (ActionHandler._lock)
      {
        ActionResult actionResult = ActionResult.Unhandled;
        ActionHandler.ActionSet actionSet;
        if (ActionHandler._registeredEvents.TryGetValue(actionString, out actionSet))
        {
          if (!actionSet.RunOverrideAtEnd && actionSet.Override != null)
            actionResult = actionSet.Override(sender, actionInfo, out overridden1);
          if (ActionHandler._activePrefix)
            ActionHandler.InvokeList(ActionHandler._prefixEvents, sender, actionInfo);
          if (!ActionHandler._halted && actionSet.Actions != null)
            ActionHandler.InvokeList(actionSet.Actions, sender, actionInfo);
          if (actionSet.RunOverrideAtEnd && actionSet.Override != null && !ActionHandler._halted)
            actionResult = actionSet.Override(sender, actionInfo, out overridden1);
        }
        else if (!ActionHandler._halted && ActionHandler._activePrefix)
          ActionHandler.InvokeList(ActionHandler._prefixEvents, sender, actionInfo);
        if (overridden1 != null)
          overridden = overridden1;
        return actionResult;
      }
    }

    public static ActionEvent GetHandler(string action)
    {
      Validate.StringNeitherNullNorEmpty(action, nameof (action));
      ActionHandler.ActionSet actionSet;
      return !ActionHandler._registeredEvents.TryGetValue(action, out actionSet) ? (ActionEvent) null : new ActionEvent(actionSet.Actions.Invoke);
    }

    public static ActionOverrideEvent GetOverride(string action)
    {
      Validate.StringNeitherNullNorEmpty(action, nameof (action));
      ActionHandler.ActionSet actionSet;
      return !ActionHandler._registeredEvents.TryGetValue(action, out actionSet) ? (ActionOverrideEvent) null : new ActionOverrideEvent(actionSet.Override.Invoke);
    }

    public static ICollection<string> RegisteredActions => (ICollection<string>) ActionHandler._registeredEvents.Keys;

    private class ActionSet
    {
      public ActionEvent Actions;
      public ActionOverrideEvent Override;
      public bool RunOverrideAtEnd;

      public ActionSet()
      {
      }

      public ActionSet(ActionEvent actions, ActionOverrideEvent overrideDelegate, bool addtoEnd)
      {
        this.Actions = actions;
        this.Override = overrideDelegate;
        this.RunOverrideAtEnd = addtoEnd;
      }
    }
  }
}
