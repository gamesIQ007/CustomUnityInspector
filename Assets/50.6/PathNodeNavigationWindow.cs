using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathNodeNavigationWindow : EditorWindow
{
    public const string FindPathsButtonText = "Bake paths";
    public const string ClearPathsButtonText = "Clear paths";

    [MenuItem("Tools/Path node navigation")]
    private static void Init()
    {
        PathNodeNavigationWindow window = (PathNodeNavigationWindow)GetWindow(typeof(PathNodeNavigationWindow));
        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button(new GUIContent(FindPathsButtonText)))
        {
            FindPath();
        }

        if (GUILayout.Button(new GUIContent(ClearPathsButtonText)))
        {
            ClearPath();
        }
    }

    private void FindPath()
    {
        ClearPath();

        PathNode[] allNodes = GameObject.FindObjectsOfType<PathNode>();

        for (int i = 0; i < allNodes.Length; i++)
        {
            for (int j = 1; j < allNodes.Length; j++)
            {
                if (Physics.Linecast(allNodes[i].transform.position, allNodes[j].transform.position) == false)
                {
                    allNodes[i].AddNextNode(allNodes[j]);

                    EditorUtility.SetDirty(allNodes[i].gameObject);
                }
            }
        }
    }

    private void ClearPath()
    {
        PathNode[] allNodes = GameObject.FindObjectsOfType<PathNode>();

        for (int i = 0; i < allNodes.Length; i++)
        {
            allNodes[i].Reset();

            EditorUtility.SetDirty(allNodes[i].gameObject);
        }
    }
}
