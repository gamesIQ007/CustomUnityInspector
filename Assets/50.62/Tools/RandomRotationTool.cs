using UnityEngine;
using UnityEditor;

[System.Serializable]
public class RandomRotationTool
{
    [SerializeField] private Vector3 minAngle;
    [SerializeField] private Vector3 maxAngle;

    public void Draw()
    {
        GUILayout.Label("Rotation", EditorStyles.boldLabel);

        minAngle = EditorGUILayout.Vector3Field(new GUIContent("Min Angle"), minAngle);
        maxAngle = EditorGUILayout.Vector3Field(new GUIContent("Max Angle"), maxAngle);

        if (GUILayout.Button(new GUIContent("Set Random Rotation")))
        {
            SetRandomRotation();
        }
    }

    private void SetRandomRotation()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            Undo.RecordObject(Selection.gameObjects[i].transform, "Random Rotation");

            Vector3 newEulerAngle = new Vector3(Random.Range(minAngle.x, maxAngle.x), Random.Range(minAngle.y, maxAngle.y), Random.Range(minAngle.z, maxAngle.z));

            Selection.gameObjects[i].transform.eulerAngles = newEulerAngle;
        }
    }
}
