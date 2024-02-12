using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DamageTracker))]
public class DamageTrackerEditor : Editor
{
    private enum ColliderType
    {
        Box,
        Mesh
    }

    private ColliderType type;
    private DamageTracker tracker;

    private void OnEnable()
    {
        tracker = target as DamageTracker;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();

        type = (ColliderType)EditorGUILayout.EnumPopup("Collider Type", type);

        if (GUILayout.Button("Add collider"))
        {
            AddCollidersToChildObjects();
        }
    }

    private void AddCollidersToChildObjects()
    {
        Transform[] allTransforms = tracker.GetComponentsInChildren<Transform>();

        for (int i = 0; i < allTransforms.Length; i++)
        {
            MeshFilter meshFilter = allTransforms[i].GetComponent<MeshFilter>();
            Collider collider = allTransforms[i].GetComponent<Collider>();

            if (meshFilter != null && collider == null)
            {
                if (type == ColliderType.Box)
                {
                    collider = allTransforms[i].gameObject.AddComponent<BoxCollider>();
                }
                if (type == ColliderType.Mesh)
                {
                    collider = allTransforms[i].gameObject.AddComponent<MeshCollider>();
                    (collider as MeshCollider).convex = true;
                }

                collider.isTrigger = true;

            }
        }
    }
}
