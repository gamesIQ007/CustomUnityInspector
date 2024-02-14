using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ResetRotationAndScaleTool
{
    public void Draw()
    {
        if (GUILayout.Button(new GUIContent("Reset Rotation And Scale")))
        {
            ResetRotationAndScale();
        }
    }

    private void ResetRotationAndScale()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            Undo.RecordObject(Selection.gameObjects[i].transform, "Random Scale");

            Selection.gameObjects[i].transform.localScale = Vector3.one;
            Selection.gameObjects[i].transform.rotation = Quaternion.identity;
        }
    }
}
