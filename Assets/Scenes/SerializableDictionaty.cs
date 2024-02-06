using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionaty<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

    /*public SerializableDictionaty(List<TKey> keys, List<TValue> values)
    {
        this.keys = keys;
        this.values = values;

        dictionary = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary.Add(keys[i], values[i]);
        }
    }*/

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var pair in dictionary)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        dictionary.Clear();

        if (keys.Count != values.Count)
        {
            throw new Exception("The number of keys and values does not match.");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            dictionary.Add(keys[i], values[i]);
        }
    }

    public void Add(TKey key, TValue value)
    {
        dictionary.Add(key, value);
    }

    // ещё много чего дописать
}
