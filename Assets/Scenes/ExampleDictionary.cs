using System.Collections.Generic;
using UnityEngine;

public class ExampleDictionary : MonoBehaviour
{
    public SerializableDictionaty<int, string> dictionary;

    private void Start()
    {
        dictionary = new SerializableDictionaty<int, string>();

        dictionary.Add(1, "test");
    }
}
