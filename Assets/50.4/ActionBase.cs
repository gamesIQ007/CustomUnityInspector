using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ActionBase
{
    public virtual void Execute(GameObject gameObject) { }
}
