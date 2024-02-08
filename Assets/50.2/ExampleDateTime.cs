using System;
using UnityEngine;

public class ExampleDateTime : MonoBehaviour
{
    public SerializableDateTime dateTime;

    private void Start()
    {
        Debug.Log(dateTime > DateTime.Now);
    }
}
