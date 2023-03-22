using System;
using System.Collections.Generic;
using UnityEngine;

public class HolderSO<T> : ScriptableObject
{
    public readonly List<T> Objects = new List<T>();

    public void SetList(List<T> list)
    {
        ClearObjects();
        AddObjects(list.ToArray());
        OnListSet?.Invoke();
    }

    public void AddObjects(params T[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            T obj = objects[i];
            Objects.Add(obj);
            OnObjectAdded?.Invoke(obj);
        }
    }

    public void ClearObjects()
    {
        Objects.Clear();
        OnListCleared?.Invoke();
    }

    public event Action OnListSet;
    public event Action<T> OnObjectAdded;
    public event Action OnListCleared;
}
