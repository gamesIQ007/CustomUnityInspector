using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class SerializeExample : MonoBehaviour
{
    [SerializeField] private float value;

    private void Update()
    {
        Debug.Log(value);
    }
}
