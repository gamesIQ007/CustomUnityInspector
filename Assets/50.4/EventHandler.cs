using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public sealed class EventHandler
{
    [SerializeReference] private List<ActionBase> actions;

    public void Invoke(GameObject gameObject)
    {
        if (actions == null) return;

        if (actions.Count == 0) return;

        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Execute(gameObject);
        }
    }

    public void AddAction(Type type)
    {
        if (actions == null)
        {
            actions = new List<ActionBase>();
        }

        ActionBase actionBase = (ActionBase)Activator.CreateInstance(type);

        actions.Add(actionBase);
    }

    public void RemoveAction(ActionBase action)
    {
        actions.Remove(action);
    }

    public void RemoveAllActions()
    {
        if (actions == null) return;

        actions.Clear();
    }
}
