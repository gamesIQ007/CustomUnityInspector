using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ActionPath("Transformable/Move")]
public class MoveAction : ActionBase
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private bool userDeltaTime;

    public override void Execute(GameObject gameObject)
    {
        if (userDeltaTime)
        {
            gameObject.transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Translate(direction * speed);
        }
    }
}
