using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    [SerializeField] private List<PathNode> nextNode = new();

    public void Reset()
    {
        nextNode.Clear();
    }

    public void AddNextNode(PathNode pathNode)
    {
        nextNode.Add(pathNode);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.25f);

        for (int i = 0; i < nextNode.Count; i++)
        {
            if (nextNode[i] != null)
            {
                Gizmos.DrawLine(transform.position, nextNode[i].transform.position);
            }
        }
    }
#endif
}
