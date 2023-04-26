using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Registry : MonoBehaviour
{
    Dictionary<Type, List<object>> _registry = new Dictionary<Type, List<object>>();

    public void Register<T>(T instance) where T : class
    {
        if (!_registry.ContainsKey(typeof(T))) _registry.Add(typeof(T), new List<object>() { instance });
        if (_registry[typeof(T)].Contains(instance)) return;
        _registry[typeof(T)].Add(instance);

    }

    public void Deregister<T>(T instance) where T : class
    {
        if (!_registry.ContainsKey(typeof(T))) return;
        if (!_registry[typeof(T)].Contains(instance)) return;
        _registry[typeof(T)].Remove(instance);
    }

    public void Deregister<T>() where T : class
    {
        _registry.Remove(typeof(T));
    }


    public void Loop<T>(Action<T> callback) where T : class
    {
        Type t = typeof(T);
        if (!_registry.ContainsKey(t)) return;
        for (int i = 0 ; i < _registry[t].Count; i++)
            callback?.Invoke(_registry[t][i] as T);
    }
}
