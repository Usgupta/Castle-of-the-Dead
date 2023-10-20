using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameEvent<T> : ScriptableObject
{
    private readonly List<GameEventListener<T>> eventListeners =
        new List<GameEventListener<T>>();

    public void Raise(T data)
    {   
        Debug.Log("raising event bro  "+ this.name);
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(data);
            Debug.Log(eventListeners[i].name);
        }
    }

    public void RegisterListener(GameEventListener<T> listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener<T> listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}