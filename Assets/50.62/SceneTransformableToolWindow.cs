using UnityEngine;
using UnityEditor;

public class SceneTransformableToolWindow : EditorWindow
{
    public const string Title = "Transformable Tools";

    private static SceneTransformableTool tools;

    [MenuItem("Tools/Transformable Tools")]
    private static void Init()
    {
        SceneTransformableToolWindow window = (SceneTransformableToolWindow)GetWindow(typeof(SceneTransformableToolWindow));

        window.titleContent = new GUIContent(Title);
        window.Show();

        UpdateTools();
    }

    private static void UpdateTools()
    {
        if (tools == null)
        {
            if (AssetDatabase.IsValidFolder("Assets/Editor") == false)
            {
                AssetDatabase.CreateFolder("Assets", "Editor");
            }

            tools = (SceneTransformableTool)AssetDatabase.LoadAssetAtPath($"Assets/Editor/{Title}.asset", typeof(SceneTransformableTool));

            if (tools == null)
            {
                tools = CreateInstance<SceneTransformableTool>();

                AssetDatabase.CreateAsset(tools, $"Assets/Editor/{Title}.asset");
            }
        }
    }

    private void OnEnable()
    {
        Selection.selectionChanged += Repaint;
    }

    private void OnDestroy()
    {
        Selection.selectionChanged -= Repaint;
    }

    private void OnGUI()
    {
        UpdateTools();

        if (Selection.gameObjects.Length == 0)
        {
            GUILayout.Label("Nothing selected", EditorStyles.boldLabel);
        }
        else
        {
            GUILayout.Label("Selected " + Selection.gameObjects.Length + " objects", EditorStyles.boldLabel);
        }

        EditorGUI.BeginChangeCheck();

        tools.DrawAll();

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RegisterCompleteObjectUndo(tools, "Inspector");
        }
    }
}
