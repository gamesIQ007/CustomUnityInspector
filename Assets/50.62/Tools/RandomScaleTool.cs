using UnityEngine;
using UnityEditor;

[System.Serializable]
public class RandomScaleTool
{
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private bool keepProportions;

    public void Draw()
    {
        GUILayout.Label("Scale", EditorStyles.boldLabel);

        keepProportions = EditorGUILayout.Toggle(new GUIContent("Keep Proportions"), keepProportions);

        if (keepProportions)
        {
            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

            Rect multiRect = GUILayoutUtility.GetLastRect();

            float[] ar = new float[2] { min, max };

            EditorGUI.MultiFloatField(multiRect, new GUIContent[] { new GUIContent("Min"), new GUIContent("Max") }, ar);
            min = ar[0];
            max = ar[1];
        }
        if (keepProportions == false)
        {
            minScale = EditorGUILayout.Vector3Field(new GUIContent("Min Scale"), minScale);
            maxScale = EditorGUILayout.Vector3Field(new GUIContent("Max Scale"), maxScale);
        }

        if (GUILayout.Button(new GUIContent("Set Random Scale")))
        {
            SetRandomScale();
        }
    }

    private void SetRandomScale()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            Undo.RecordObject(Selection.gameObjects[i].transform, "Random Scale");

            if (keepProportions)
            {
                float rand = Random.Range(min, max);
                Selection.gameObjects[i].transform.localScale *= rand;
            }

            if (keepProportions == false)
            {
                Vector3 newEScale = new Vector3(Random.Range(minScale.x, maxScale.x), Random.Range(minScale.y, maxScale.y), Random.Range(minScale.z, maxScale.z));
                Selection.gameObjects[i].transform.localScale = newEScale;
            }
        }
    }
}
