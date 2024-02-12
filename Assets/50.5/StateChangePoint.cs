using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    MoveTo,
    Stay,
    ExploreAround
}

public class StateChangePoint : MonoBehaviour
{
    [SerializeField] private StateType nextState;
    [SerializeField] private Vector3 movementTarget;
    [SerializeField] private float waitTime;
    [SerializeField] private float exploreAroundRadius;
}
