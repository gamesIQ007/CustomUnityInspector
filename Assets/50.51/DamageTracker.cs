using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Default,
    Fire,
    Ice
}

public class DamageTracker : MonoBehaviour
{
    [SerializeField] private DamageType damageType;

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
