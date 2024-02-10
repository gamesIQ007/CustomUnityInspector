using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ActionPath("Debug/Log")]
public class LogAction : ActionBase
{
    [SerializeField] private string message;

    public override void Execute(GameObject gameObject)
    {
        Debug.Log(message);
    }
}
