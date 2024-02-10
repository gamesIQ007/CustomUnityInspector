using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFieldTest : MonoBehaviour
{
    public EventHandler OnUpdate;

    private void Update()
    {
        OnUpdate?.Invoke(gameObject);
    }
}
